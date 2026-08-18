// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Filters.ImbaseObjFilterDataHelper
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase.Filters;
using Intermech.Localization;
using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Imbase.Server.Filters;

internal class ImbaseObjFilterDataHelper
{
  public static bool LoadFilterData(
    long objectId,
    IUserSession session,
    out ImbaseObjFilterData filterData)
  {
    filterData = (ImbaseObjFilterData) null;
    if (objectId == 0L)
      return false;
    IDBObject filterObject = session.GetObject(objectId, false);
    return filterObject != null && ImbaseObjFilterDataHelper.LoadFilterData(filterObject, out filterData);
  }

  public static bool LoadFilterData(IDBObject filterObject, out ImbaseObjFilterData filterData)
  {
    filterData = (ImbaseObjFilterData) null;
    if (filterObject == null)
      return false;
    IDBAttribute attributeByGuid = filterObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid == null)
    {
      filterData = new ImbaseObjFilterData();
      return true;
    }
    IBlobReader blobReader = (IBlobReader) attributeByGuid;
    if (blobReader == null)
      return false;
    BlobInformation blobInformation = blobReader.OpenBlob(0);
    try
    {
      if (blobInformation.RealFileSize > 0L)
      {
        byte[] buffer = blobReader.ReadDataBlock(0);
        if (buffer != null)
        {
          MemoryStream memoryStream = (MemoryStream) null;
          try
          {
            using (MemoryStream inStream = new MemoryStream(buffer))
            {
              memoryStream = new MemoryStream(Convert.ToInt32(blobInformation.RealFileSize));
              ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) memoryStream, (Stream) inStream);
              memoryStream.Position = 0L;
              BinaryFormatter binaryFormatter = new BinaryFormatter();
              filterData = (ImbaseObjFilterData) binaryFormatter.Deserialize((Stream) memoryStream);
            }
          }
          catch (Exception ex)
          {
            IOutputView service = ServiceUtils.GetService<IOutputView>((object) ApplicationServices.Container, false);
            if (service != null)
            {
              string text = string.Format(LocalizationHolder.rm.GetString("Imbase.Server_25"), (object) filterObject.ObjectID);
              service.WriteString("IMBASE", text);
              service.WriteString("IMBASE", ex.Message);
            }
          }
          finally
          {
            memoryStream?.Close();
          }
        }
      }
    }
    finally
    {
      blobReader.CloseBlob();
    }
    return filterData != null;
  }

  public static bool SaveFilterData(IDBObject filterObject, ImbaseObjFilterData filterData)
  {
    if (filterObject == null)
      return false;
    Guid guid = new Guid("cad0004b-306c-11d8-b4e9-00304f19f545");
    IDBAttribute dbAttribute = filterObject.GetAttributeByGuid(guid);
    if (filterData != null && filterData.Items.Count > 0)
    {
      if (dbAttribute == null)
        dbAttribute = filterObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(guid), false);
      using (MemoryStream memoryStream = new MemoryStream(32000))
      {
        new BinaryFormatter().Serialize((Stream) memoryStream, (object) filterData);
        using (MemoryStream outStream = new MemoryStream(Convert.ToInt32(memoryStream.Length / 2L)))
        {
          ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).PackStream((Stream) outStream, (Stream) memoryStream, Convert.ToInt32((object) ZLibCompressLevels.LevelMax));
          IBlobWriter blobWriter = dbAttribute as IBlobWriter;
          string fileName1 = "Im_ObjFilter";
          IFileNamesService service = ServiceUtils.GetService<IFileNamesService>((object) filterObject.Session, true);
          string fileName2 = service == null ? $"{fileName1}_{(object) filterObject.ObjectID}" : service.GetUniqueFileName(fileName1, filterObject.ObjectID, filterObject.Session.SessionGUID);
          if (blobWriter != null)
          {
            blobWriter.OpenBlob(new BlobInformation(memoryStream.Length, outStream.Length, DateTime.Now, fileName2, ArcMethods.ZLibPacked, "Imbase Object's Filter"), false);
            blobWriter.WriteDataBlock(outStream.ToArray());
          }
        }
      }
    }
    else
      dbAttribute?.Delete(0L);
    return true;
  }

  public static ImbaseObjFilterInfo GetFilterInfo(IDBObject dbObject)
  {
    if (dbObject == null)
      return (ImbaseObjFilterInfo) null;
    if (!MetaDataHelper.IsObjectTypeChildOf(dbObject.ObjectType, Intermech.Imbase.Consts.ImbaseObjFilterTypeID))
      return (ImbaseObjFilterInfo) null;
    ImbaseObjFilterInfo filterInfo = new ImbaseObjFilterInfo(dbObject.ObjectID, -1, dbObject.Caption);
    IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(new Guid("cad001a0-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid1 != null && attributeByGuid1.Value != null && attributeByGuid1.Value != DBNull.Value && GuidHelper.IsGuid(attributeByGuid1.Value.ToString()))
      filterInfo.RefObjTypeID = MetaDataHelper.GetObjectTypeID(attributeByGuid1.Value.ToString());
    IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(Intermech.Imbase.Consts.ImbaseFilterOwnerAttrGuid);
    if (attributeByGuid2 != null && attributeByGuid2.Value != null && attributeByGuid2.Value != DBNull.Value)
      filterInfo.Owner = attributeByGuid2.Value.ToString();
    return filterInfo;
  }

  public static DataTable GetFilterItemData(ImbaseObjFilterItem filterItem)
  {
    if (filterItem == null || filterItem.FilterData == null)
      return (DataTable) null;
    if (filterItem.ExtInfo is DataTable extInfo)
      return extInfo;
    DataTable filterTableAll = FolderFilterService.CreateFilterTableAll("filter_fx");
    filterItem.ExtInfo = (object) filterTableAll;
    DataTable filterData = filterItem.FilterData;
    int columnIndex1 = filterData.Columns.IndexOf("F_GUID");
    int columnIndex2 = filterData.Columns.IndexOf("F_OWNER");
    foreach (DataRow row in (InternalDataCollectionBase) filterData.Rows)
    {
      if (row != null)
      {
        string str1 = row[columnIndex1].ToString();
        string str2 = row[columnIndex2].ToString();
        filterTableAll.Rows.Add((object) DBNull.Value, (object) DBNull.Value, (object) DBNull.Value, (object) DBNull.Value, (object) str1, (object) str2);
      }
    }
    return filterTableAll;
  }
}
