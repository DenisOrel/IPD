// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertTable
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Expert.Table;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Expert.Server;

public class ExpertTable : 
  ExpertObject,
  IExpertTable,
  IExpertObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  private Hashtable _hash = new Hashtable();

  public ExpertTable(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this._objType = ExpertObjType.Table;
  }

  public IList AttributesList
  {
    get
    {
      if (!(this._hash[(object) ExpertConsts.Consts.attrAttrGUIDs] is AttributeValues attributeValues))
        return (IList) new ArrayList();
      ArrayList attributesList = new ArrayList((ICollection) attributeValues.Values);
      if (attributesList.Count.Equals(1) && attributesList[0].GetType().Equals(typeof (DBNull)))
        attributesList.Clear();
      return (IList) attributesList;
    }
    set
    {
      IDBAttribute dbAttribute = this.Attributes.AddAttribute(ExpertConsts.Consts.attrAttrGUIDs, false);
      object[] objArray;
      if (value.Count <= 0)
        objArray = new object[1]{ (object) DBNull.Value };
      else
        objArray = new ArrayList((ICollection) value).ToArray();
      dbAttribute.Values = objArray;
    }
  }

  public IList Roles
  {
    get
    {
      if (!(this._hash[(object) ExpertConsts.Consts.attrAttrRoles] is AttributeValues attributeValues))
        return (IList) new ArrayList();
      ArrayList roles = new ArrayList((ICollection) attributeValues.Values);
      if (roles.Count.Equals(1) && roles[0].GetType().Equals(typeof (DBNull)))
        roles.Clear();
      for (int index = 0; index < roles.Count; ++index)
      {
        AttributeRoles enumValue = (AttributeRoles) EnumTypeHelper.GetEnumValue(typeof (AttributeRoles), Convert.ToString(roles[index]), (object) AttributeRoles.Result);
        roles[index] = (object) enumValue;
      }
      return (IList) roles;
    }
    set
    {
      IDBAttribute dbAttribute = this.Attributes.AddAttribute(ExpertConsts.Consts.attrAttrRoles, false);
      object[] objArray;
      if (value.Count <= 0)
        objArray = new object[1]{ (object) DBNull.Value };
      else
        objArray = new ArrayList((ICollection) value).ToArray();
      dbAttribute.Values = objArray;
    }
  }

  public int ColumnsCount
  {
    get
    {
      return this._hash[(object) ExpertConsts.Consts.attrTableCols] is AttributeValues attributeValues && attributeValues.Values[0].GetType().Equals(typeof (int)) ? Convert.ToInt32(attributeValues.Values[0]) : 0;
    }
    set
    {
      this.Attributes.AddAttribute(ExpertConsts.Consts.attrTableCols, false).Values = new object[1]
      {
        (object) value
      };
    }
  }

  public int RowsCount
  {
    get
    {
      return this._hash[(object) ExpertConsts.Consts.attrTableRows] is AttributeValues attributeValues && attributeValues.Values[0].GetType().Equals(typeof (int)) ? Convert.ToInt32(attributeValues.Values[0]) : 0;
    }
    set
    {
      this.Attributes.AddAttribute(ExpertConsts.Consts.attrTableRows, false).Values = new object[1]
      {
        (object) value
      };
    }
  }

  public string esName
  {
    get
    {
      AttributeValues attributeValues = !this._hash.ContainsKey((object) ExpertConsts.Consts.attrObjectName) ? this._hash[(object) ExpertConsts.Consts._attrObjName] as AttributeValues : this._hash[(object) ExpertConsts.Consts.attrObjectName] as AttributeValues;
      return attributeValues != null && attributeValues.Values[0].GetType().Equals(typeof (string)) ? Convert.ToString(attributeValues.Values[0]) : string.Empty;
    }
    set
    {
      this.Attributes.AddAttribute(ExpertConsts.Consts._attrObjName, false).Values = new object[1]
      {
        (object) value
      };
      this.Attributes.AddAttribute(ExpertConsts.Consts.attrObjectName, false).Values = new object[1]
      {
        (object) value
      };
    }
  }

  public eTableCollection LoadTableData()
  {
    if (ExpertServer.es != null && ExpertServer.es.expertTables.ContainsKey(this.ObjectID))
      return ExpertServer.es.GetValueFromCache<long, eTableCollection>(this.ObjectID, ExpertServer.es.expertTables);
    if ((this.GetAttributeByID(ExpertConsts.Consts.attrObjData) ?? this.GetAttributeByID(ExpertConsts.Consts.attrLongBlob)) is IBlobReader blobReader)
    {
      BlobInformation blobInformation = blobReader.OpenBlob(-1);
      if (blobInformation.RealFileSize > 0L)
      {
        blobReader.OpenBlob(0);
        byte[] numArray = blobReader.ReadDataBlock((int) blobInformation.RealFileSize);
        return new BinaryFormatter().Deserialize(numArray.Length == 0 || !blobInformation.ArcMethod.Equals((object) ArcMethods.ZLibPacked) ? (Stream) new MemoryStream(numArray) : ZlibHelper.UnpackBuffer(numArray)) as eTableCollection;
      }
    }
    return (eTableCollection) null;
  }

  public IList ObjectLinksList
  {
    get
    {
      if (!(this._hash[(object) ExpertConsts.Consts.attrObjLinkIDs] is AttributeValues attributeValues))
        return (IList) new ArrayList();
      ArrayList objectLinksList = new ArrayList((ICollection) attributeValues.Values);
      if (objectLinksList.Count.Equals(1) && objectLinksList[0].GetType().Equals(typeof (DBNull)))
        objectLinksList.Clear();
      return (IList) objectLinksList;
    }
    set
    {
      IDBAttribute dbAttribute1 = this.Attributes.AddAttribute(ExpertConsts.Consts.attrObjLinkIDs, false);
      ArrayList arrayList = new ArrayList();
      foreach (object obj in (IEnumerable) value)
      {
        if (obj != null && !obj.Equals((object) -1L))
          arrayList.Add(obj);
      }
      IDBAttribute dbAttribute2 = dbAttribute1;
      object[] objArray;
      if (arrayList.Count <= 0)
        objArray = new object[1]{ (object) DBNull.Value };
      else
        objArray = arrayList.ToArray();
      dbAttribute2.Values = objArray;
    }
  }

  public IList ObjectTypesList
  {
    get
    {
      if (!(this._hash[(object) ExpertConsts.Consts.attrObjTypeGUIDs] is AttributeValues attributeValues))
        return (IList) new ArrayList();
      ArrayList objectTypesList = new ArrayList((ICollection) attributeValues.Values);
      if (objectTypesList.Count.Equals(1) && objectTypesList[0].GetType().Equals(typeof (DBNull)))
        objectTypesList.Clear();
      return (IList) objectTypesList;
    }
    set
    {
      IDBAttribute dbAttribute = this.Attributes.AddAttribute(ExpertConsts.Consts.attrObjTypeGUIDs, false);
      object[] objArray;
      if (value.Count <= 0)
        objArray = new object[1]{ (object) DBNull.Value };
      else
        objArray = new ArrayList((ICollection) value).ToArray();
      dbAttribute.Values = objArray;
    }
  }

  public void SaveTableData(eTableCollection tableCollection)
  {
    Stream stream = (Stream) new MemoryStream();
    new BinaryFormatter().Serialize(stream, (object) tableCollection);
    byte[] data = ZlibHelper.PackBuffer(stream);
    int num = data.Length > 100000 ? 1 : 0;
    IBlobWriter blobWriter = (num == 0 ? this.Attributes.AddAttribute(ExpertConsts.Consts.attrObjData, false) : this.Attributes.AddAttribute(ExpertConsts.Consts.attrLongBlob, false)) as IBlobWriter;
    BlobInformation blobInfo = new BlobInformation(stream.Length, (long) data.Length, DateTime.Now, "Blob", ArcMethods.ZLibPacked, string.Empty);
    if (blobWriter.OpenBlob(blobInfo, false))
      blobWriter.WriteDataBlock(data);
    if (ExpertServer.es != null)
    {
      ExpertServer.es.SetValueToCache<long, eTableCollection>(this.ObjectID, tableCollection, ExpertServer.es.expertTables);
      ((IExpertServerSynchronizer) ServerServices.GetService(typeof (IExpertServerSynchronizer)))?.AddEvent(ExpServerCache.cacheTables, this.ObjectID, 0L, this.UserSession.DataManager);
    }
    if (num != 0)
      this.Attributes.FindByID(ExpertConsts.Consts.attrObjData)?.Delete(0L);
    else
      this.Attributes.FindByID(ExpertConsts.Consts.attrLongBlob)?.Delete(0L);
  }

  public void SaveCondition() => this.WriteCond();

  public int EntrysCount
  {
    get
    {
      return this._hash[(object) ExpertConsts.Consts.attrTableEntries] is AttributeValues attributeValues && attributeValues.Values[0].GetType().Equals(typeof (int)) ? Convert.ToInt32(attributeValues.Values[0]) : 0;
    }
    set
    {
      this.Attributes.AddAttribute(ExpertConsts.Consts.attrTableEntries, false).Values = new object[1]
      {
        (object) value
      };
    }
  }

  public int LayersCount
  {
    get
    {
      return this._hash[(object) ExpertConsts.Consts.attrTableLayers] is AttributeValues attributeValues && attributeValues.Values[0].GetType().Equals(typeof (int)) ? Convert.ToInt32(attributeValues.Values[0]) : 0;
    }
    set
    {
      this.Attributes.AddAttribute(ExpertConsts.Consts.attrTableLayers, false).Values = new object[1]
      {
        (object) value
      };
    }
  }

  public override void Load()
  {
    foreach (AttributeValues attributesValue in this.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeAlias | GetAttributeValuesModes.IncludeBlobs | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeGroupName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.CheckVisibility))
      this._hash[(object) attributesValue.AttributeID] = (object) attributesValue;
    this.ReadCond();
  }

  public override bool ReplaceAttr(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode)
  {
    if (this.AttributesList is ArrayList attributesList)
    {
      for (int index1 = 0; index1 < attributesList.Count; ++index1)
      {
        string str1 = (string) attributesList[index1];
        Guid guid = fromAttribute.GUID;
        string str2 = guid.ToString();
        if (str1 == str2)
        {
          ArrayList arrayList = attributesList;
          int index2 = index1;
          guid = toAttribute.GUID;
          string str3 = guid.ToString();
          arrayList[index2] = (object) str3;
        }
      }
    }
    eTableCollection tableCollection = this.LoadTableData();
    int num = tableCollection.PerformAttrCombine(fromAttribute, toAttribute, session) ? 1 : 0;
    if (num == 0)
      return num != 0;
    this.SaveTableData(tableCollection);
    return num != 0;
  }
}
