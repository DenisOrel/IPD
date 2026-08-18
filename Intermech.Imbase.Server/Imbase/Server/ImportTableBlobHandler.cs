// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImportTableBlobHandler
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Portal;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.IO;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Imbase.Server;

internal static class ImportTableBlobHandler
{
  public static long LinkParentId = -1;
  public static bool Forced = false;

  public static void SpecHandleObjectAttributeEvent(object sender, SpecHandleAttributeEventArgs e)
  {
    if (e.Handled)
      return;
    if (e.TypeID == Intermech.Imbase.Consts.ImbaseTableTypeID && e.AttributeGuid.ToString().Equals("cad0004b-306c-11d8-b4e9-00304f19f545"))
    {
      string str = Convert.ToString(e.Value.StringValue);
      switch (str)
      {
        case "data.xml":
          e.Value.StringValue = (object) $"{str}.{Math.Abs(e.AttributableID).ToString()}";
          break;
        case "structure.xml":
          e.Value.InlistId = 1;
          goto case "data.xml";
        case "info.xml":
          e.Value.InlistId = 2;
          goto case "data.xml";
        case "data.txt":
          e.Value.InlistId = 3;
          goto case "data.xml";
      }
    }
    else
    {
      if (e.TypeID != Intermech.Imbase.Consts.ImbaseTableTypeID || e.AttributeID != Intermech.Imbase.Consts.ImbaseTableDataAttID && e.AttributeID != TableLoadHelper.LongBlobTableDataAttId || e.Value.FileSize == null || (long) e.Value.FileSize <= 0L)
        return;
      e.NewAttributeID = MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeImportedTableData);
    }
  }

  private static bool CheckAttributes(IUserSession session, DataTable tableAttributes)
  {
    foreach (DataRow row in (InternalDataCollectionBase) tableAttributes.Rows)
    {
      string g = Convert.ToString(row["F_ATTRIBUTE_GUID"]);
      if (session.GetAttributeType(new Guid(g), false) == null)
        return false;
    }
    return true;
  }

  private static void WriteFile(DataSet data, AttributeRecord record)
  {
    FileStream outStream = File.Create(record.Path2File);
    try
    {
      using (ImChunkedStream imChunkedStream = new ImChunkedStream())
      {
        new BinaryFormatter().Serialize((Stream) imChunkedStream, (object) data);
        record.IntegerValue = (object) imChunkedStream.Length;
        record.FileSize = (object) imChunkedStream.Length;
        imChunkedStream.Position = 0L;
        ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).PackStream((Stream) outStream, (Stream) imChunkedStream, 9);
      }
    }
    finally
    {
      outStream.Flush();
      outStream.Close();
    }
  }

  private static DataSet ReadFile(string fileName)
  {
    FileStream inStream = File.OpenRead(fileName);
    try
    {
      using (ImChunkedStream imChunkedStream = new ImChunkedStream())
      {
        ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) imChunkedStream, (Stream) inStream);
        imChunkedStream.Position = 0L;
        return (DataSet) new BinaryFormatter().Deserialize((Stream) imChunkedStream);
      }
    }
    finally
    {
      inStream.Flush();
      inStream.Close();
    }
  }

  public static void ImportTaskCompletedEvent(object sender, ImportTaskCompletedEventArgs e)
  {
    IImportRulesService service;
    if ((service = ServiceUtils.GetService<IImportRulesService>((object) ServerServices.ServiceContainer, false)) == null || service.DefaultImbaseFolder == 0L)
      return;
    IDBRelationCollection relationCollection = e.Session.GetRelationCollection(e.Session.IdentHelper.SortedRelationTypeID);
    relationCollection.LocalTypesMode = true;
    List<long> longList = new List<long>();
    foreach (Tuple<long, int> objectId in e.ObjectIDs)
    {
      if (objectId.Item2.Equals(Intermech.Imbase.Consts.ImbaseTableRefTypeID) || objectId.Item2.Equals(Intermech.Imbase.Consts.ImbaseFolderTypeID))
      {
        if (relationCollection.EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
        {
          (object) -2
        }), objectId.Item1).Rows.Count <= 0)
        {
          relationCollection.Create(service.DefaultImbaseFolder, objectId.Item1);
          longList.Add(objectId.Item1);
        }
      }
    }
    if (longList.Count <= 0)
      return;
    DBClassifier.RebuildKeys(e.Session, longList.ToArray());
  }

  public static void ObjectImportedEvent(object sender, ObjectImportedEventArgs e)
  {
    if (e.Handled || e.ImportedObject.ObjectType != Intermech.Imbase.Consts.ImbaseTableTypeID)
      return;
    ITablesMergingService customService = (ITablesMergingService) e.Session.GetCustomService(typeof (ITablesMergingService));
    IDBAttribute attributeByGuid1 = e.ImportedObject.GetAttributeByGuid(PortalConsts.attributeTableAttributes, false);
    if (attributeByGuid1 == null)
    {
      IDBAttribute attributeByGuid2 = e.ImportedObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"), false);
      if (attributeByGuid2 == null)
        return;
      if (TableLoadHelper.GetTablesInternal(e.Session, e.ImportedObject.ObjectID, false) == null)
      {
        DataSet dataSet = TableLoadHelper.CreateDataSet();
        TableLoadHelper.StoreData(e.Session, e.ImportedObject.ObjectID, dataSet, (ITablesIndexer) null);
      }
      DataSet dataSet1 = (DataSet) null;
      if (!ImportTableBlobHandler.TryImportImbaseData(e.Session, e.ImportedObject, attributeByGuid2, out dataSet1))
        return;
      customService.Merge(e.Session.SessionGUID, e.ImportedObject.ObjectID, dataSet1, true);
      e.Handled = true;
    }
    else
    {
      DataSet importData = TablesMergingHelper.UnpackDataSetFromAttribute(e.ImportedObject.GetAttributeByGuid(PortalConsts.attributeImportedTableData, false));
      DataSet dataSet = TablesMergingHelper.UnpackDataSetFromAttribute(attributeByGuid1);
      List<AttributesComparison> attributesComparisonList = AttributesComparisonHelper.ReadFromAttribute(e.ImportedObject.GetAttributeByGuid(PortalConsts.attributeComparisonAttributes, false));
      bool flag1 = false;
      DataTable table = importData.Tables["IMS_ATTR_TYPES"];
      bool flag2 = false;
      string errorMessage = string.Empty;
      foreach (DataRow row in (InternalDataCollectionBase) importData.Tables["IMS_ATTR_TYPES"].Rows)
      {
        Guid guid = new Guid(Convert.ToString(row["F_ATTRIBUTE_GUID"]));
        if (attributesComparisonList != null)
        {
          AttributesComparison attributesComparison = attributesComparisonList.Find((Predicate<AttributesComparison>) (x => x.SourceGuid.Equals(guid)));
          if (attributesComparison != null)
          {
            row["F_ATTRIBUTE_GUID"] = (object) attributesComparison.DestinationGuid.ToString();
            importData.Tables["IMS_DATA"].Columns[guid.ToString()].ColumnName = attributesComparison.DestinationGuid.ToString();
            flag2 = true;
            continue;
          }
        }
        IDBAttributeType attributeType = e.Session.GetAttributeType(guid, false);
        if (attributeType == null)
        {
          flag1 = true;
          break;
        }
        DataRow[] dataRowArray = dataSet.Tables["IMS_ATTRIBUTES"].Select($"F_GUID ='{guid}'");
        Convert.ToString(dataRowArray[0]["F_NAME"]);
        int int32_1 = Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]);
        MultiValueModes int32_2 = (MultiValueModes) Convert.ToInt32(dataRowArray[0]["F_MULTIPLE_VALUED"]);
        FieldTypes int32_3 = (FieldTypes) Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_TYPE"]);
        long int64 = dataRowArray[0]["F_SIZE_TYPE"] != DBNull.Value ? Convert.ToInt64(dataRowArray[0]["F_SIZE_TYPE"]) : 0L;
        if (!customService.CheckAttribute(e.Session.SessionGUID, attributeType.AttributeID, dataSet.Tables["IMS_POSSIBLE_VALUES"], int32_1, int32_3, int64.ToString(), int32_2, out errorMessage))
        {
          flag1 = true;
          break;
        }
      }
      if (flag2)
        importData.AcceptChanges();
      if (!flag1)
        customService.Merge(e.Session.SessionGUID, e.ImportedObject.ObjectID, importData, true);
      else
        (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace($"Ошибка при восстановлении таблицы {e.ImportedObject.Caption}({e.ImportedObject.ObjectID}): {errorMessage}", 0, string.Empty);
    }
  }

  private static bool TryImportImbaseData(
    IUserSession session,
    IDBObject tableObject,
    IDBAttribute fileAtt,
    out DataSet dataSet)
  {
    dataSet = (DataSet) null;
    bool flag = false;
    if (tableObject is IImbaseDBObject imbaseDbObject)
    {
      flag = imbaseDbObject.AllowSkipSiteCheck;
      imbaseDbObject.AllowSkipSiteCheck = true;
    }
    try
    {
      DataTableImporter dataTableImporter = new DataTableImporter(session, tableObject);
      int num = dataTableImporter.TryCreateDataTable(session, tableObject, fileAtt, out dataSet) ? 1 : 0;
      ImportTableBlobHandler.CreateTableLink(session, tableObject, dataTableImporter.AttList);
      return num != 0;
    }
    finally
    {
      if (imbaseDbObject != null)
        imbaseDbObject.AllowSkipSiteCheck = flag;
    }
  }

  private static void CreateTableLink(IUserSession session, IDBObject tableObject, string attList)
  {
    long objectID = 0;
    bool flag1 = false;
    bool flag2 = false;
    if (!ImportTableBlobHandler.Forced)
    {
      DataTable tableRefIdsByTableId = TableLoadHelper.GetTableRefIDsByTableID(session, tableObject.ObjectID);
      if (tableRefIdsByTableId != null && tableRefIdsByTableId.Rows.Count > 0)
        objectID = Convert.ToInt64(tableRefIdsByTableId.Rows[0][0]);
    }
    IDBObject tableLink;
    if (objectID == 0L)
    {
      tableLink = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID).Create();
      flag2 = true;
    }
    else
    {
      tableLink = session.GetObject(objectID);
      if (tableLink.ObjectModifyMode == ObjectModifyModes.CantModify)
        throw new Exception("Нельзя модифицировать объект " + tableLink.Caption);
      if (tableLink.CheckoutBy != 0L && tableLink.CheckoutBy != session.UserID)
        throw new Exception("Объект взят на изменение другим пользователем " + tableLink.Caption);
      if (tableLink.ObjectModifyMode != ObjectModifyModes.InBase)
      {
        flag1 = tableLink.CheckoutBy == 0L;
        if (flag1)
          tableLink = tableLink.CheckOut(false);
      }
    }
    try
    {
      tableLink.Attributes.AddAttribute(Intermech.Imbase.Consts.ImbaseTableRefAttID, false).Value = (object) tableObject.ObjectID;
      tableLink.OwnerID = tableObject.OwnerID;
      (tableLink as DBObject).SetSiteID(tableObject.SiteID);
      string[] strArray = attList.Split(new string[1]
      {
        Environment.NewLine
      }, StringSplitOptions.RemoveEmptyEntries);
      IDBAttributeCollection attributes1 = tableLink.Attributes;
      IDBAttribute4TypeCollection attributes2 = session.GetObjectType(tableLink.ObjectType).Attributes;
      string path = string.Empty;
      string folderData = string.Empty;
      Dictionary<string, string> blobsList = new Dictionary<string, string>(32 /*0x20*/);
      foreach (string str1 in strArray)
      {
        int length = str1.IndexOf('=');
        if (length != -1)
        {
          string str2 = str1.Substring(0, length).Trim();
          if (!string.IsNullOrEmpty(str2))
          {
            string paramValue = str1.Substring(length + 1, str1.Length - length - 1).Trim();
            if (!string.IsNullOrEmpty(paramValue))
            {
              switch (str2)
              {
                case "F_IMAGE":
                  ImportTableBlobHandler.ExtractImage(session, tableObject, paramValue);
                  continue;
                case "F_TEXT":
                  ImportTableBlobHandler.ExtractMemo(session, tableObject, paramValue);
                  continue;
                case "F_PATH":
                  path = paramValue;
                  continue;
                case "F_FOLDERDATA":
                  folderData = paramValue;
                  continue;
                default:
                  if (str2.StartsWith("F_BLOB"))
                  {
                    if (!blobsList.ContainsKey(str2))
                    {
                      blobsList.Add(str2, paramValue);
                      continue;
                    }
                    continue;
                  }
                  IDBAttributeType attributeType = session.GetAttributeType(str2, false);
                  if (attributeType != null && attributeType.AttributeID >= 0)
                  {
                    IDBAttribute dbAttribute = attributes1.FindByID(attributeType.AttributeID);
                    if (dbAttribute == null)
                    {
                      IDBAttributeType4 attributeById = attributes2.GetAttributeByID(attributeType.AttributeID, false);
                      if (attributeById != null)
                        dbAttribute = attributes1.AddAttribute(attributeById.AttributeID, false);
                    }
                    try
                    {
                      if (dbAttribute == null && attributeType.Computed == ComputeValueModes.NotComputableValue)
                        dbAttribute = attributes1.AddAttribute(attributeType.AttributeID, false);
                      if (dbAttribute != null)
                      {
                        if (!dbAttribute.IsSystem)
                        {
                          object obj = ImportTableBlobHandler.TryConvertReferenceValue(session, paramValue);
                          dbAttribute.Value = obj;
                          continue;
                        }
                        continue;
                      }
                      continue;
                    }
                    catch (Exception ex)
                    {
                      continue;
                    }
                  }
                  else
                    continue;
              }
            }
          }
        }
      }
      if (flag1)
      {
        tableLink.CheckIn();
      }
      else
      {
        if (!flag2)
          return;
        tableLink.Caption = tableObject.Caption;
        ImportTableBlobHandler.PlaceNewLink(session, tableLink, path, folderData, blobsList);
        tableLink.CommitCreation(true);
      }
    }
    catch (Exception ex)
    {
      if (flag1)
        tableLink.CancelChanges();
      if (flag2)
        ;
      throw;
    }
  }

  private static object TryConvertReferenceValue(IUserSession session, string paramValue)
  {
    if (!string.IsNullOrEmpty(paramValue))
    {
      int num1 = paramValue.IndexOf('|');
      if (num1 != -1)
      {
        int num2 = paramValue.IndexOf('.');
        if (num2 != -1 && num2 < num1)
        {
          string path = paramValue.Substring(num1 + 1);
          if (path.Length > 2 && path[0] == '\\' && path[1] == '\\')
            return (object) ImbaseFolderCreator.FindOrCreatePath(session, path);
        }
        else if (num1 == 20 && (paramValue[0] == 'i' || paramValue[0] == 'I') && paramValue[1] == '6')
        {
          string request = paramValue.Substring(num1 + 1);
          if (ServerServices.GetService(typeof (IImbaseIndexingService)) is IImbaseIndexingService service)
          {
            long tableRefID = 0;
            long recID = 0;
            if (service.FindByIndex(session.SessionGUID, MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), request, out tableRefID, out recID))
              return (object) ImbaseHelper.MakeInternalImbaseKey(tableRefID, recID);
          }
        }
      }
    }
    return (object) paramValue;
  }

  private static void ExtractImage(IUserSession session, IDBObject tableObject, string paramValue)
  {
    string fileName;
    byte[] nameAndData = ImbaseFolderCreator.ExtractNameAndData(paramValue, out fileName, out string _);
    if (nameAndData == null)
      return;
    using (MemoryStream inStream = new MemoryStream(nameAndData))
    {
      using (MemoryStream outStream = new MemoryStream())
      {
        long length = inStream.Length;
        ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).PackStream((Stream) outStream, (Stream) inStream, Convert.ToInt32((object) ZLibCompressLevels.LevelMax));
        if (outStream.Length > (long) Intermech.Consts.MaxShortBlobSize)
          return;
        int attributeId1 = MetaDataHelper.GetAttributeID((object) new Guid("cad0013e-306c-11d8-b4e9-00304f19f545"));
        IDBAttribute attributeById = tableObject.GetAttributeByID(attributeId1);
        IDBObject dbObject = (IDBObject) null;
        if (attributeById != null)
          dbObject = session.GetObject(attributeById.AsInteger, false);
        bool flag = false;
        if (dbObject == null)
        {
          dbObject = session.GetObjectCollection(new Guid("cad00140-306c-11d8-b4e9-00304f19f545")).Create();
          dbObject.Attributes.AddAttribute(session.GetAttributeType(new Guid("cad00020-306c-11d8-b4e9-00304f19f545")).AttributeID, false, new object[1]
          {
            (object) fileName
          });
          flag = true;
        }
        int attributeId2 = MetaDataHelper.GetAttributeID((object) new Guid("cad0013d-306c-11d8-b4e9-00304f19f545"));
        IBlobWriter blobWriter = dbObject.Attributes.AddAttribute(attributeId2, false) as IBlobWriter;
        blobWriter.OpenBlob(new BlobInformation(length, outStream.Length, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, fileName), false);
        outStream.Position = 0L;
        blobWriter.WriteDataBlock(outStream.ToArray());
        if (flag)
          dbObject.CommitCreation(true);
        if (attributeById != null)
          return;
        IDBAttribute dbAttribute = tableObject.Attributes.AddAttribute(attributeId1, false);
        if (dbAttribute == null)
          return;
        dbAttribute.Value = (object) dbObject.ObjectID;
      }
    }
  }

  private static void ExtractMemo(IUserSession session, IDBObject tableObject, string paramValue)
  {
    string memo;
    ImbaseFolderCreator.ExtractNameAndData(paramValue, out string _, out memo);
    if (string.IsNullOrEmpty(memo))
      return;
    tableObject.Attributes.AddAttribute(Intermech.Imbase.Consts.ImbaseNoteAttID, false).AsString = memo;
  }

  private static void PlaceNewLink(
    IUserSession session,
    IDBObject tableLink,
    string path,
    string folderData,
    Dictionary<string, string> blobsList)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(session.IdentHelper.SortedRelationTypeID);
    long num = ImportTableBlobHandler.LinkParentId;
    IImportRulesService service;
    if (num == -1L && (service = ServiceUtils.GetService<IImportRulesService>((object) ServerServices.ServiceContainer, false)) != null && service.DefaultImbaseFolder != 0L)
      num = service.DefaultImbaseFolder;
    if ((num == -1L || ImportTableBlobHandler.Forced) && !string.IsNullOrEmpty(path))
      num = ImbaseFolderCreator.FindOrCreatePath(session, path, folderData, blobsList);
    if (num == -1L)
      return;
    relationCollection.Create(num, tableLink.ObjectID);
    IDBAttribute attributeById = session.GetObject(num).GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
    string nextClassifierKey = (session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService).GenerateNextClassifierKey((object) session.SessionGUID, Intermech.Imbase.Consts.ImbaseCatalogTypeID, attributeById.AsString, tableLink.ObjectType);
    (tableLink.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId) ?? tableLink.Attributes.AddAttribute(Intermech.Imbase.Consts.ClassifFolderKeyAttId, false)).AsString = nextClassifierKey;
  }
}
