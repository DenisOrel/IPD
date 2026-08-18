// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.AttachmentList
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Workflow;

[Serializable]
public class AttachmentList : List<Attachment>
{
  private bool _modified;
  public int RelationTypeID = wfConsts.AttachmentRelationTypeID;
  private bool _hasInvisibleItems;
  /// <summary>
  /// Дополнительные условия, которые могут использоваться при загрузке списка в Load
  /// </summary>
  public ConditionStructure[] Conditions;
  protected object[] AddColumns;

  public event EventHandler OnModified;

  public bool Modified
  {
    get => this._modified;
    set
    {
      if (this._modified == value)
        return;
      this._modified = value;
      if (!this._modified || this.OnModified == null)
        return;
      this.OnModified((object) this, (EventArgs) null);
    }
  }

  /// <summary>
  /// Указывает, есть ли в полученном списке невидимые объекты, которые не были загружены из-за недостаточного уровня доступа текущего пользователя
  /// </summary>
  public bool HasInvisibleItems => this._hasInvisibleItems;

  public void Load(IDBObject obj)
  {
    IDBRelationCollection relationCollection = obj.Session.GetRelationCollection(this.RelationTypeID);
    this.Load(obj, relationCollection);
  }

  public virtual Attachment NewAttachment() => new Attachment();

  protected virtual Attachment NewAttachment(DataRow row) => this.NewAttachment();

  private void Load(IDBObject obj, IDBRelationCollection relcol)
  {
    this.Clear();
    List<int> intList = MetaDataHelper.OptimizeChildObjectTypes((IEnumerable<int>) (obj.Session.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService).GetPresentCompositionTypes((object) obj.Session.SessionGUID, (IEnumerable<long>) new long[1]
    {
      obj.ObjectID
    }, this.RelationTypeID, true));
    relcol.ChildObjectTypes = (IList<int>) intList;
    ColumnDescriptor[] array = new ColumnDescriptor[6]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID),
      new ColumnDescriptor((object) wfConsts.AttrRelationOwnerID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_CHKOUT_BY)
    };
    if (this.AddColumns != null)
    {
      int length = array.Length;
      Array.Resize<ColumnDescriptor>(ref array, length + this.AddColumns.Length);
      for (int index = 0; index < this.AddColumns.Length; ++index)
        array[length + index] = !(this.AddColumns[index] is ColumnDescriptor) ? new ColumnDescriptor(this.AddColumns[index]) : (ColumnDescriptor) this.AddColumns[index];
    }
    DBRecordSetParams paramSet = new DBRecordSetParams(this.Conditions, array);
    foreach (DataRow row in (InternalDataCollectionBase) (relcol.Session.IsSystemSession ? relcol.ConsistFrom(paramSet, obj.ObjectID) : relcol.ConsistFrom(paramSet, obj.ObjectID, out this._hasInvisibleItems)).Rows)
    {
      Attachment attachment = this.NewAttachment(row);
      attachment._objectID = Convert.ToInt64(row[0]);
      attachment._id = Convert.ToInt64(row[1]);
      attachment._typeID = Convert.ToInt32(row[2]);
      attachment._relationID = Convert.ToInt64(row[3]);
      attachment._relationOwnerID = row.IsNull(4) ? 0L : Convert.ToInt64(row[4]);
      attachment.CheckOutBy = Convert.ToInt64(row[5]);
      this.Add(attachment, false);
    }
    this._modified = false;
  }

  public void Assign(AttachmentList src)
  {
    this.Clear();
    this.AddList(src);
  }

  public void AddList(AttachmentList src, bool AllowDuplicates)
  {
    foreach (Attachment att in (List<Attachment>) src)
    {
      if (AllowDuplicates || !this.Contains(att))
      {
        Attachment attachment = this.NewAttachment((DataRow) null);
        attachment.Assign(att);
        this.Add(attachment, false);
      }
    }
  }

  public void AddList(AttachmentList src) => this.AddList(src, true);

  public void Assign(List<long> src)
  {
    this.Clear();
    foreach (long ObjectID in src)
      this.AddAttachment(ObjectID);
  }

  public void CopyTo(List<long> dst)
  {
    dst.Clear();
    foreach (Attachment attachment in (List<Attachment>) this)
      dst.Add(attachment.ObjectID);
  }

  public event SaveAttachmentHandler OnSaveAttachment;

  public bool Save(IDBObject obj)
  {
    if (!this.Modified)
      return false;
    IUserSession session = obj.Session;
    IDBRelationCollection relationCollection1 = (IDBRelationCollection) null;
    if (!(session.GetRelationCollection(this.RelationTypeID) is IWFAttachmentRelationCollection relationCollection2))
      relationCollection1 = session.GetRelationCollection(this.RelationTypeID);
    long objectId = obj.ObjectID;
    AttachmentList attachmentList1 = new AttachmentList();
    attachmentList1.RelationTypeID = this.RelationTypeID;
    attachmentList1.Conditions = this.Conditions;
    attachmentList1.OnSaveAttachment += this.OnSaveAttachment;
    attachmentList1.Load(obj, (IDBRelationCollection) relationCollection2 ?? relationCollection1);
    List<long> longList = new List<long>();
    List<Attachment> attachmentList2 = new List<Attachment>();
    foreach (Attachment attachment in (List<Attachment>) attachmentList1)
    {
      if (this.Contains(attachment))
        attachmentList2.Add(attachment);
      else
        longList.Add(attachment.RelationID);
    }
    List<Attachment> attachmentList3 = new List<Attachment>();
    foreach (Attachment att in (List<Attachment>) this)
    {
      IDBRelation rel = (IDBRelation) null;
      if (!attachmentList2.Contains(att))
      {
        try
        {
          rel = relationCollection2 != null ? relationCollection2.Create(objectId, att.ObjectID, false) : relationCollection1.Create(objectId, att.ObjectID);
        }
        catch (ObjectNotFoundException ex)
        {
          attachmentList3.Add(att);
          continue;
        }
        catch (Exception ex)
        {
          this.Remove(att);
          throw;
        }
        if (att.RelationOwnerID == 0L)
          att.RelationOwnerID = obj.Session.UserID;
        IDBAttribute attributeById = rel.GetAttributeByID(wfConsts.AttrRelationOwnerID);
        if (attributeById != null)
          attributeById.AsInteger = att.RelationOwnerID;
      }
      else if (this.OnSaveAttachment != null)
      {
        if (att.RelationID == 0L)
        {
          int index = attachmentList1.IndexOf(att);
          if (index != -1)
            att._relationID = attachmentList1[index].RelationID;
        }
        rel = session.GetRelation(att.RelationID);
      }
      SaveAttachmentHandler onSaveAttachment = this.OnSaveAttachment;
      if (onSaveAttachment != null)
        onSaveAttachment(att, rel);
    }
    if (attachmentList3.Count > 0)
    {
      foreach (Attachment attachment in attachmentList3)
        this.Remove(attachment);
      attachmentList3.Clear();
    }
    if (longList.Count > 0)
    {
      foreach (long aRelationID in longList)
      {
        IDBRelation relation = session.GetRelation(aRelationID);
        if (relation != null)
        {
          try
          {
            relation.Delete(0L);
          }
          catch
          {
            this.Load(obj, (IDBRelationCollection) relationCollection2 ?? relationCollection1);
            throw;
          }
        }
      }
    }
    this._modified = false;
    return true;
  }

  public Attachment AddAttachment(long ObjectID)
  {
    Attachment attachment = this.NewAttachment((DataRow) null);
    attachment._objectID = ObjectID;
    this.Add(attachment);
    return attachment;
  }

  public new void Add(Attachment item) => this.Add(item, true);

  public void Add(Attachment item, bool checkDuplicates)
  {
    if (checkDuplicates && this.Contains(item))
      return;
    base.Add(item);
    this._modified = true;
  }

  public new void Clear()
  {
    if (this.Count > 0)
    {
      base.Clear();
      this._modified = true;
    }
    this._hasInvisibleItems = false;
  }

  private void AddWorkCopies(AttachmentList list, Attachment att)
  {
    if (att.ObjectID < 0L)
      list.Add(att);
    if (att.InnerList == null)
      return;
    foreach (Attachment inner in (List<Attachment>) att.InnerList)
      this.AddWorkCopies(list, inner);
  }

  private void AddWorkCopiesOtherUser(AttachmentList list, Attachment att)
  {
    if (att.ObjectID > 0L && att.CheckOutBy != 0L)
      list.Add(att);
    if (att.InnerList == null)
      return;
    foreach (Attachment inner in (List<Attachment>) att.InnerList)
      this.AddWorkCopiesOtherUser(list, inner);
  }

  public AttachmentList WorkCopies
  {
    get
    {
      AttachmentList list = new AttachmentList();
      foreach (Attachment att in (List<Attachment>) this)
        this.AddWorkCopies(list, att);
      return list;
    }
  }

  public AttachmentList CheckOutByOtherUser
  {
    get
    {
      AttachmentList list = new AttachmentList();
      foreach (Attachment att in (List<Attachment>) this)
        this.AddWorkCopiesOtherUser(list, att);
      return list;
    }
  }

  public int IndexOfID(long objectID)
  {
    for (int index = 0; index < this.Count; ++index)
    {
      if (this[index].ObjectID == objectID)
        return index;
    }
    return -1;
  }

  public bool Remove(long ObjectID)
  {
    int index = this.IndexOfID(ObjectID);
    if (index != -1)
      this.RemoveAt(index);
    return index != -1;
  }

  public new void RemoveAt(int index)
  {
    base.RemoveAt(index);
    if (index < 0 || index > this.Count)
      return;
    this._modified = true;
  }

  public new bool Remove(Attachment item)
  {
    int num = base.Remove(item) ? 1 : 0;
    if (num == 0)
      return num != 0;
    this._modified = true;
    return num != 0;
  }

  [System.Runtime.Serialization.OnDeserialized]
  private void OnDeserialized(StreamingContext context) => this._modified = true;
}
