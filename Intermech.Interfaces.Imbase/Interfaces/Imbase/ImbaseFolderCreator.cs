// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.ImbaseFolderCreator
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Imbase;

public static class ImbaseFolderCreator
{
  /// <summary>
  /// Создает путь или переходит по иерархии Каталогов и папок Imbase
  /// </summary>
  /// <param name="path">Путь вида \\Имя Каталога\папка1\папка2\..\папка N</param>
  /// <returns>Идентификатор последней папки в указанном пути</returns>
  public static long FindOrCreatePath(
    IUserSession session,
    string path,
    string folderData = null,
    Dictionary<string, string> blobsList = null)
  {
    if (string.IsNullOrEmpty(path) || !path.StartsWith("\\\\"))
      return -1;
    string[] collection = path.Split(new char[1]{ '\\' }, StringSplitOptions.RemoveEmptyEntries);
    if (collection == null || collection.Length < 2)
      return -1;
    Dictionary<int, string> folderAttribures = ImbaseFolderCreator.ExtractFolderAttribures(folderData);
    long catalog = ImbaseFolderCreator.FindCatalog(session, collection[0]);
    if (catalog == -1L)
      return -1;
    bool look = true;
    List<string> pathParts = new List<string>((IEnumerable<string>) collection);
    pathParts.RemoveAt(0);
    return ImbaseFolderCreator.FindOrCreatePath(session, catalog, 0, pathParts, folderAttribures, ref look, blobsList);
  }

  private static Dictionary<int, string> ExtractFolderAttribures(string folderData)
  {
    if (string.IsNullOrWhiteSpace(folderData))
      return (Dictionary<int, string>) null;
    Dictionary<int, string> dictionary1 = new Dictionary<int, string>();
    string[] strArray = folderData.Split(new char[2]
    {
      ']',
      '['
    }, StringSplitOptions.RemoveEmptyEntries);
    int capacity = strArray.Length / 2;
    Dictionary<int, string> dictionary2 = new Dictionary<int, string>(capacity);
    for (int index1 = 0; index1 < capacity; ++index1)
    {
      int index2 = index1 * 2;
      int result;
      if (int.TryParse(strArray[index2], out result))
        dictionary1[result] = strArray[index2 + 1];
    }
    return dictionary1.Count > 0 ? dictionary1 : (Dictionary<int, string>) null;
  }

  /// <summary>
  /// Создает или находит в указанном Каталоге или папке цепочку вложенных папок
  /// </summary>
  /// <param name="ownerId">Идентификатор Каталога или начальной папки</param>
  /// <param name="pathIndex">Индекс в списке имен папок</param>
  /// <param name="pathParts">Имена папок</param>
  /// <param name="folderAttributes">Атрибуты папок</param>
  /// <returns>идентификатор нижней созданной или найденной папки</returns>
  public static long FindOrCreatePath(
    IUserSession session,
    long ownerId,
    int pathIndex,
    List<string> pathParts,
    Dictionary<int, string> folderAttributes,
    ref bool look,
    Dictionary<string, string> blobsList)
  {
    if (pathIndex >= pathParts.Count)
      return ownerId;
    string pathPart = pathParts[pathIndex];
    long orCreateFolder = ImbaseFolderCreator.FindOrCreateFolder(session, ownerId, pathPart, ref look);
    if (folderAttributes != null && folderAttributes.ContainsKey(pathIndex))
      ImbaseFolderCreator.AssignFolderAttributes(session, orCreateFolder, folderAttributes[pathIndex], blobsList);
    return ImbaseFolderCreator.FindOrCreatePath(session, orCreateFolder, pathIndex + 1, pathParts, folderAttributes, ref look, blobsList);
  }

  private static void AssignFolderAttributes(
    IUserSession session,
    long folderId,
    string attValues,
    Dictionary<string, string> blobData)
  {
    if (string.IsNullOrWhiteSpace(attValues))
      return;
    IDBObject dbObject = session.GetObject(folderId);
    IDBAttributeCollection attributes1 = dbObject.Attributes;
    IDBAttribute4TypeCollection attributes2 = session.GetObjectType(dbObject.ObjectType).Attributes;
    string str1 = attValues;
    char[] chArray = new char[1]{ '@' };
    foreach (string str2 in str1.Split(chArray))
    {
      int length = str2.IndexOf('=');
      if (length != -1)
      {
        string anAttributeName = str2.Substring(0, length).Trim();
        if (!string.IsNullOrEmpty(anAttributeName))
        {
          string str3 = str2.Substring(length + 1, str2.Length - length - 1).Trim();
          if (!string.IsNullOrEmpty(str3))
          {
            IDBAttributeType attributeType = session.GetAttributeType(anAttributeName, false);
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
                    if (str3.StartsWith("F_BLOB"))
                    {
                      int result;
                      str3 = blobData == null || !blobData.ContainsKey(str3) ? (!int.TryParse(str3.Substring("F_BLOB".Length), out result) ? "0" : result.ToString()) : ImbaseFolderCreator.FindOrAddBlob(session, str3, blobData[str3]).ToString();
                    }
                    object obj = (object) str3;
                    dbAttribute.Value = obj;
                  }
                }
              }
              catch (Exception ex)
              {
              }
            }
          }
        }
      }
    }
  }

  public static byte[] ExtractNameAndData(string paramValue, out string fileName, out string memo)
  {
    byte[] nameAndData = (byte[]) null;
    fileName = string.Empty;
    memo = (string) null;
    byte[] buffer = Convert.FromBase64String(paramValue);
    int length = buffer.Length;
    int index1 = 0;
    MemoryStream memoryStream1 = new MemoryStream();
    while (true)
    {
      byte num = buffer[index1++];
      --length;
      if (num != (byte) 0)
        memoryStream1.WriteByte(num);
      else
        break;
    }
    memoryStream1.Position = 0L;
    using (StreamReader streamReader = new StreamReader((Stream) memoryStream1, Encoding.Default))
    {
      fileName = streamReader.ReadToEnd();
      fileName = Path.GetFileName(fileName);
    }
    bool flag = Path.GetExtension(fileName).Equals(".rtf", StringComparison.InvariantCultureIgnoreCase) || fileName.Length == 0;
    if ((length <= 4 || buffer[index1] != (byte) 90 || buffer[index1 + 1] != (byte) 76 || buffer[index1 + 2] != (byte) 73 ? 0 : (buffer[index1 + 3] == (byte) 66 ? 1 : 0)) != 0)
    {
      int index2 = index1 + 4;
      int count = length - 4;
      using (MemoryStream inStream = new MemoryStream(buffer, index2, count))
      {
        using (MemoryStream outStream = new MemoryStream())
        {
          ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) outStream, (Stream) inStream);
          outStream.Position = 0L;
          nameAndData = outStream.ToArray();
          if (flag)
          {
            using (StreamReader streamReader = new StreamReader((Stream) outStream, Encoding.Default))
              memo = streamReader.ReadToEnd();
          }
        }
      }
    }
    else
    {
      using (MemoryStream memoryStream2 = new MemoryStream(buffer, index1, length))
      {
        nameAndData = memoryStream2.ToArray();
        if (flag)
        {
          using (StreamReader streamReader = new StreamReader((Stream) memoryStream2, Encoding.Default))
            memo = streamReader.ReadToEnd();
        }
      }
    }
    return nameAndData;
  }

  internal static long FindOrAddBlob(
    IUserSession session,
    string oldImbaseBlobsName,
    string blobData)
  {
    long orAddBlob = 0;
    int result;
    if (oldImbaseBlobsName.StartsWith("F_BLOB") && int.TryParse(oldImbaseBlobsName.Substring("F_BLOB".Length), out result))
      orAddBlob = ImbaseFolderCreator.FindOrAddBlob(session, result, blobData);
    return orAddBlob;
  }

  internal static long FindOrAddBlob(IUserSession session, int oldImbaseBlobId, string blobData)
  {
    IDBObject dbObject = (IDBObject) null;
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseBLOBTypeID);
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(Convert.ToInt32(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID), RelationalOperators.Equal, (object) oldImbaseBlobId, LogicalOperators.NONE, 0, false)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      }, (object[]) null, (SortOrders[]) null)
      {
        ColumnNames = new ColumnNameMapping[1]
        {
          ColumnNameMapping.ID
        },
        TableName = "f",
        FailIfNotFound = false
      };
      DataTable dataTable = objectCollection.Select(paramSet);
      if (dataTable.Rows.Count > 0)
        dbObject = session.GetObject(Convert.ToInt64(dataTable.Rows[0][0]), false);
      if (dbObject == null)
      {
        dbObject = objectCollection.Create();
        flag = true;
      }
      IDBAttribute dbAttribute = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseNoteAttID) ?? dbObject.Attributes.AddAttribute(Intermech.Imbase.Consts.ImbaseNoteAttID, false);
      string memo;
      ImbaseFolderCreator.ExtractNameAndData(blobData, out string _, out memo);
      dbAttribute.Value = (object) memo;
      (dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID) ?? dbObject.Attributes.AddAttribute(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID, false)).Value = (object) oldImbaseBlobId;
      string str = $"Imbase BLOB {Math.Abs(dbObject.ObjectID)}";
      dbObject.Caption = str;
      if (flag)
        dbObject.CommitCreation(true);
      return dbObject.ObjectID;
    }
  }

  /// <summary>
  /// Создает или находит папку на определенном уровне иерархии
  /// </summary>
  /// <param name="ownerId">Идентификатор корневого объекта ( Каталог или папка)</param>
  /// <param name="folderName">Имя папки</param>
  /// <returns>Идентификатор новой или найденной папки</returns>
  public static long FindOrCreateFolder(
    IUserSession session,
    long ownerId,
    string folderName,
    ref bool look)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFolderTypeID);
    DBRecordSetParams paramsSet = ImbaseFolderCreator.CreateParamsSet(new ConditionStructure[2]
    {
      new ConditionStructure((string) null, RelationalOperators.EntersIn, (object) ownerId, LogicalOperators.AND, 0, false),
      new ConditionStructure(Convert.ToInt32((object) ObligatoryObjectAttributes.CAPTION), RelationalOperators.Equal, (object) folderName, LogicalOperators.NONE, 0, false)
    });
    DataTable dataTable = objectCollection.Select(paramsSet);
    if (dataTable.Rows.Count > 0)
      return Convert.ToInt64(dataTable.Rows[0][0]);
    look = false;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(Intermech.Imbase.Consts.ImbaseFolderTypeID);
    IDBRelationCollection relationCollection = session.GetRelationCollection(objectType.DefaultRelation);
    IDBObject dbObject = objectCollection.Create();
    dbObject.Caption = folderName;
    long projectID = ownerId;
    long objectId = dbObject.ObjectID;
    relationCollection.Create(projectID, objectId);
    dbObject.CommitCreation(true);
    return dbObject.ObjectID;
  }

  public static long FindCatalog(IUserSession session, string catalogName)
  {
    DataTable dataTable = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID).Select(ImbaseFolderCreator.CreateParamsSet(new ConditionStructure[1]
    {
      new ConditionStructure(Convert.ToInt32((object) ObligatoryObjectAttributes.CAPTION), RelationalOperators.Equal, (object) catalogName, LogicalOperators.NONE, 0, false)
    }));
    return dataTable.Rows.Count > 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : -1L;
  }

  internal static DBRecordSetParams CreateParamsSet(ConditionStructure[] conds)
  {
    object[] columns = new object[5]
    {
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID),
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_TYPE),
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.CAPTION),
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_LC_STEP),
      (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId
    };
    object[] sortColumns = new object[2]
    {
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_TYPE),
      (object) Convert.ToInt32((object) ObligatoryObjectAttributes.CAPTION)
    };
    SortOrders[] orders = new SortOrders[2]
    {
      SortOrders.ASC,
      SortOrders.ASC
    };
    return new DBRecordSetParams(conds, columns, sortColumns, orders)
    {
      ColumnNames = new ColumnNameMapping[5]
      {
        ColumnNameMapping.ID,
        ColumnNameMapping.ID,
        ColumnNameMapping.ID,
        ColumnNameMapping.ID,
        ColumnNameMapping.ID
      },
      TableName = "f",
      FailIfNotFound = false
    };
  }
}
