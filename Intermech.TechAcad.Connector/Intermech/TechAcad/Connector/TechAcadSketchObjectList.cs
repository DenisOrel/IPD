// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.TechAcadSketchObjectList
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Runtime.ComInterop.LocalServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

#nullable disable
namespace Intermech.TechAcad.Connector;

public class TechAcadSketchObjectList : SingleThreadedObject, ISketchCollection
{
  private readonly List<TechAcadSketchObject> _items = new List<TechAcadSketchObject>();
  private readonly TechAcadTPObject _tpObject;

  public TechAcadSketchObjectList()
    : this((TechAcadTPObject) null)
  {
  }

  internal TechAcadSketchObjectList(TechAcadTPObject tpObject) => this._tpObject = tpObject;

  public ISketchObject get_Item(int index) => (ISketchObject) this._items[index];

  public int ReadOnly => 0;

  public int Count => this._items.Count;

  public ISketchObject Add(string name, IDraftObject draft, ITPObject tpObject)
  {
    if (this.ReadOnly != 0)
      return (ISketchObject) null;
    if (draft == null || tpObject == null)
      return (ISketchObject) null;
    if (this._tpObject != null && this._tpObject.ObjID != tpObject.ObjID)
      return (ISketchObject) null;
    try
    {
      TechAcadSketchObject sketch = new TechAcadSketchObject(draft, tpObject);
      long objId = tpObject.ObjID;
      int count = this.Count;
      string str;
      while (true)
      {
        str = $"OPR_{Math.Abs(objId):D}_{count:D}";
        bool flag = false;
        foreach (ISketchObject sketchObject in this.Items)
        {
          if (sketchObject.SketchID == str)
          {
            flag = true;
            break;
          }
        }
        if (flag)
          ++count;
        else
          break;
      }
      sketch._sketchID = str;
      long val1_1 = 0;
      foreach (ISketchObject sketchObject in this.Items)
        val1_1 = Math.Max(val1_1, sketchObject.OrderID);
      sketch._orderID = val1_1 + 1000L;
      sketch._name = name;
      sketch.Status = ChangeStatus.Added;
      if (draft.SketchCollection is TechAcadSketchObjectList sketchCollection)
      {
        TechAcadSketchObject acadSketchObject = new TechAcadSketchObject((ISketchObject) sketch);
        long val1_2 = 0;
        foreach (ISketchObject sketchObject in sketchCollection.Items)
          val1_2 = Math.Max(val1_2, sketchObject.OrderID);
        acadSketchObject._orderID = val1_2 + 1000L;
        acadSketchObject._tpObject = (ITPObject) null;
        acadSketchObject.Status = ChangeStatus.Added;
        sketchCollection.Items.Add(acadSketchObject);
        draft.SaveStucture();
      }
      if (sketchCollection != null && this._tpObject != null && this._tpObject.ObjID != tpObject.ObjID)
        this.Items.Add(sketch);
      this.Link(tpObject, (ISketchObject) sketch);
      return (ISketchObject) sketch;
    }
    catch (Exception ex)
    {
      Plugin.LogError(sc_19178.ssp_techacad_19179() + (object) ex);
      throw;
    }
  }

  public void Link(ITPObject tpObject, ISketchObject sketch)
  {
    if (sketch == null || tpObject == null)
      return;
    TechAcadTPObject techAcadTpObject = tpObject as TechAcadTPObject;
    TechAcadSketchObjectList sketchCollection = tpObject.SketchCollection as TechAcadSketchObjectList;
    if (techAcadTpObject == null || sketchCollection == null)
      return;
    if (sketchCollection.ReadOnly != 0)
      return;
    try
    {
      if (sketchCollection.Items.Any<TechAcadSketchObject>((Func<TechAcadSketchObject, bool>) (sketchObject => sketchObject == sketch)))
        return;
      TechAcadSketchObject acadSketchObject = new TechAcadSketchObject(sketch);
      long num = sketchCollection.Items.Select<TechAcadSketchObject, long>((Func<TechAcadSketchObject, long>) (item => item.OrderID)).Concat<long>((IEnumerable<long>) new long[1]).Max();
      acadSketchObject._orderID = num + 1000L;
      acadSketchObject._tpObject = tpObject;
      acadSketchObject.Status = ChangeStatus.Added;
      sketchCollection.Items.Add(acadSketchObject);
      TechAcadDraftObject draftObject = acadSketchObject.DraftObject as TechAcadDraftObject;
      bool flag = false;
      if (tpObject.DraftCollection is TechAcadDraftObjectList draftCollection1 && draftCollection1.Items.Cast<IDraftObject>().Any<IDraftObject>((Func<IDraftObject, bool>) (tpDraft => draftObject != null && draftObject.DraftID == tpDraft.DraftID)))
        flag = true;
      if (!flag && tpObject.DraftCollection is TechAcadDraftObjectList draftCollection2)
        draftCollection2.Items.Add(draftObject);
      techAcadTpObject.SaveDraftInfo();
    }
    catch (Exception ex)
    {
      Plugin.LogError(sc_19178.ssp_techacad_19180() + (object) ex);
      throw;
    }
  }

  public void Remove(int index)
  {
    if (this.ReadOnly != 0)
      return;
    try
    {
      TechAcadSketchObject acadSketchObject = this.Items[index];
      acadSketchObject.Status |= ChangeStatus.Deleted;
      if (this._tpObject != null)
        this._tpObject.SaveDraftInfo();
      else
        acadSketchObject.DraftObject?.SaveStucture();
      this.Items.Remove(acadSketchObject);
    }
    catch (Exception ex)
    {
      Plugin.LogError(sc_19178.ssp_techacad_19181() + (object) ex);
      throw;
    }
  }

  internal void LoadSketchCollection(
    TechAcadDraftObject draftObject,
    TechAcadTPObject tpObject,
    IDBAttributable dbAttributable)
  {
    IDBAttribute attributeByGuid = dbAttributable?.GetAttributeByGuid(TechCardConsts.AttributeTypes.SketchListGUIDAttrGuid, false);
    if (attributeByGuid == null)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      BlobProcReader blobProcReader = new BlobProcReader(attributeByGuid, 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
      blobProcReader.ReadData();
      if (!blobProcReader.Result || memoryStream.Length == 0L)
        return;
      XmlDocument xmlDocument = new XmlDocument();
      memoryStream.Position = 0L;
      xmlDocument.Load((Stream) memoryStream);
      XmlNode firstChild = xmlDocument.FirstChild;
      if (firstChild.Name.Equals("TechAcadSketchObject"))
      {
        TechAcadSketchObject acadSketchObject = new TechAcadSketchObject((IDraftObject) draftObject, (ITPObject) tpObject);
        acadSketchObject.Load(firstChild);
        acadSketchObject.Status = ChangeStatus.None;
        this.Items.Add(acadSketchObject);
      }
      else
      {
        if (!firstChild.Name.Equals(nameof (TechAcadSketchObjectList)) || firstChild.ChildNodes.Count == 0)
          return;
        foreach (XmlNode childNode in firstChild.ChildNodes)
        {
          TechAcadSketchObject acadSketchObject = new TechAcadSketchObject((IDraftObject) draftObject, (ITPObject) tpObject);
          acadSketchObject.Load(childNode);
          acadSketchObject.Status = ChangeStatus.None;
          this.Items.Add(acadSketchObject);
        }
      }
    }
  }

  public bool SaveSketchCollection(XmlDocument xmlDoc)
  {
    if (xmlDoc == null)
      return false;
    XmlNode element = (XmlNode) xmlDoc.CreateElement(nameof (TechAcadSketchObjectList));
    xmlDoc.AppendChild(element);
    foreach (TechAcadSketchObject acadSketchObject in this.Items)
    {
      if ((acadSketchObject.Status & ChangeStatus.Deleted) != ChangeStatus.Deleted)
        element.AppendChild(acadSketchObject.Save(xmlDoc));
    }
    return true;
  }

  internal void SaveSketchCollection(IDBAttributable dbAttributable)
  {
    if (dbAttributable == null)
      return;
    XmlDocument xmlDoc = new XmlDocument();
    this.SaveSketchCollection(xmlDoc);
    using (MemoryStream memoryStream = new MemoryStream())
    {
      xmlDoc.Save((Stream) memoryStream);
      memoryStream.Position = 0L;
      IDBAttribute aIDBAttribute = dbAttributable.GetAttributeByGuid(TechCardConsts.AttributeTypes.SketchListGUIDAttrGuid, false);
      if (aIDBAttribute == null)
      {
        int attributeTypeId = MetaDataHelper.GetAttributeTypeID(TechCardConsts.AttributeTypes.SketchListGUIDAttrGuid);
        if (attributeTypeId != 0)
          aIDBAttribute = dbAttributable.Attributes.AddAttribute(attributeTypeId, false);
      }
      if (aIDBAttribute != null)
      {
        BlobInformation aBlobInformation = new BlobInformation(memoryStream.Length, 0L, DateTime.Now, "TechAcadSketchObjectList.xml", ArcMethods.ZLibPacked, string.Empty);
        new BlobProcWriter(aIDBAttribute, 0, aBlobInformation, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      }
      else
      {
        switch (dbAttributable)
        {
          case IDBObject dbObject:
            throw new AttributeNotFoundException("", TechCardConsts.AttributeTypes.SketchListGUIDAttrGuid.ToString(), dbObject.ObjectID);
          case IDBRelation dbRelation:
            throw new AttributeNotFoundException("", TechCardConsts.AttributeTypes.SketchListGUIDAttrGuid.ToString(), dbRelation.RelationID);
        }
      }
    }
  }

  public int GetIndexByID(string sketchId)
  {
    int indexById = -1;
    for (int index = 0; index < this.Count; ++index)
    {
      if (this.get_Item(index).SketchID == sketchId)
      {
        indexById = index;
        break;
      }
    }
    return indexById;
  }

  public List<TechAcadSketchObject> Items => this._items;

  public void ClearChangeStatus()
  {
    for (int index = this.Count - 1; index >= 0; --index)
    {
      if (this.Items[index].Status != ChangeStatus.None)
      {
        if ((this.Items[index].Status & ChangeStatus.Deleted) != ChangeStatus.Deleted)
          this.Items[index].Status = ChangeStatus.None;
        else
          this.Items.RemoveAt(index);
      }
    }
  }
}
