// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.DocumentTypeWeightHelper
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Статический класс, который позволяет создавать коллекции DocumentTypeWeightCollection,
/// сохранять их в указанном атрибуте указанного объекта, а также загружать их обратно.
/// В классе можно создать статическую коллекцию, в которой будут храниться глобальные
/// настройки.
/// </summary>
public static class DocumentTypeWeightHelper
{
  /// <summary>Идентификатор атрибута "Веса типов документов"
  /// (отыскивается по его системному Guid)</summary>
  public static int attrDocumentTypesWeights;
  /// <summary>Идентификатор версии объекта "Общий шаблон спецификаций"
  /// (отыскивается по его системному Guid)</summary>
  public static long objectCommonSpecificationsTemplate;
  /// <summary>Общая коллекция "весов" для типов объектов и документов</summary>
  public static DocumentTypeWeightCollection items;

  /// <summary>Статический конструктор</summary>
  static DocumentTypeWeightHelper()
  {
    DocumentTypeWeight.specTypeID = MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
    DocumentTypeWeight.partDrawType = MetaDataHelper.GetObjectTypeID("cad00261-306c-11d8-b4e9-00304f19f545");
    DocumentTypeWeight.disabledObjTypes = new List<int>();
  }

  /// <summary>Инициализировать статические поля</summary>
  /// <param name="session">Сессия</param>
  public static void InitStaticFields(IUserSession session)
  {
    DocumentTypeWeightHelper.attrDocumentTypesWeights = MetaDataHelper.GetAttributeTypeID("cad00292-306c-11d8-b4e9-00304f19f545");
    DocumentTypeWeightHelper.objectCommonSpecificationsTemplate = 0L;
    if (session == null)
      return;
    IDBObject dbObject = session.GetObject(new Guid("cad0026f-306c-11d8-b4e9-00304f19f545"), false);
    if (dbObject == null)
      return;
    DocumentTypeWeightHelper.objectCommonSpecificationsTemplate = dbObject.ObjectID;
  }

  /// <summary>Получить список допустимых к вставке в раздел "Документация" спецификаций типов объектов-документов</summary>
  /// <param name="session">Сессия</param>
  /// <returns>Коллекция типов документов, допустимых для добавления в спецификации</returns>
  public static List<int> AcceptableDocumentTypes(IUserSession session)
  {
    List<int> intList1 = new List<int>();
    if (session == null)
      return intList1;
    int relationTypeId = MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545");
    int objectTypeId1 = MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
    int objectTypeId2 = MetaDataHelper.GetObjectTypeID("cad00261-306c-11d8-b4e9-00304f19f545");
    DataTable applicabilitiesList = session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(relationTypeId, objectTypeId1, -1);
    if (applicabilitiesList == null)
      return intList1;
    List<int> intList2 = new List<int>();
    for (int index = 0; index < applicabilitiesList.Rows.Count; ++index)
    {
      object obj1 = applicabilitiesList.Rows[index]["F_INOBJECT_TYPE"];
      int result;
      if (obj1 != null && obj1 != DBNull.Value && int.TryParse(obj1.ToString(), out result))
      {
        object obj2 = applicabilitiesList.Rows[index]["F_OBJECT_TYPE"];
        if (obj2 != null && obj2 != DBNull.Value && int.TryParse(obj2.ToString(), out int _) && !intList2.Contains(result))
          intList2.Add(result);
      }
    }
    if (intList2.Count == 0)
      return intList1;
    for (int index1 = 0; index1 < intList2.Count; ++index1)
    {
      List<int> childObjectTypesId = MetaDataHelper.GetApplicabilityChildObjectTypesID(intList2[index1], relationTypeId);
      for (int index2 = 0; index2 < childObjectTypesId.Count; ++index2)
      {
        if (!intList1.Contains(childObjectTypesId[index2]) && !MetaDataHelper.IsObjectTypeChildOf(childObjectTypesId[index2], objectTypeId1) && !MetaDataHelper.IsObjectTypeChildOf(childObjectTypesId[index2], objectTypeId2))
          intList1.Add(childObjectTypesId[index2]);
      }
    }
    if (intList1.Count == 0)
      return intList1;
    List<int> intList3 = new List<int>();
    for (int index3 = 0; index3 < intList1.Count; ++index3)
    {
      for (int index4 = 0; index4 < intList1.Count; ++index4)
      {
        if (index3 != index4 && MetaDataHelper.IsObjectTypeChildOf(intList1[index4], intList1[index3]) && !intList3.Contains(intList1[index4]))
          intList3.Add(intList1[index4]);
      }
    }
    for (int index = 0; index < intList3.Count; ++index)
      intList1.Remove(intList3[index]);
    return intList1;
  }

  /// <summary>Получить коллекцию "весов" для допустимых типов объектов-документов по умолчанию</summary>
  /// <param name="session">Сессия</param>
  /// <returns>Коллекция "весов" для допустимых типов объектов-документов по умолчанию</returns>
  public static DocumentTypeWeightCollection AcceptableDocumentTypesCollection(IUserSession session)
  {
    DocumentTypeWeightCollection weightCollection = new DocumentTypeWeightCollection();
    List<int> intList = DocumentTypeWeightHelper.AcceptableDocumentTypes(session);
    for (int index = 0; index < intList.Count; ++index)
    {
      DocumentTypeWeight documentTypeWeight = new DocumentTypeWeight(intList[index]);
      documentTypeWeight.SyncMetaData();
      if (documentTypeWeight.DocumentTypeID != -1)
        weightCollection.Add(documentTypeWeight);
    }
    weightCollection.Sort();
    return weightCollection;
  }

  /// <summary>Загрузить коллекцию из системного объекта "Общий шаблон спецификаций".
  /// Коллекция будет размещена в статическом свойстве DocumentTypeWeightHelper.items
  /// </summary>
  /// <param name="session">Сессия</param>
  public static void LoadSystemCollection(IUserSession session)
  {
    DocumentTypeWeightHelper.InitStaticFields(session);
    DocumentTypeWeightHelper.items = DocumentTypeWeightHelper.LoadFromObject(session, DocumentTypeWeightHelper.objectCommonSpecificationsTemplate, DocumentTypeWeightHelper.attrDocumentTypesWeights);
    DocumentTypeWeightHelper.items.UpdateWeights(DocumentTypeWeight.StartWeight, DocumentTypeWeight.WeightDelta);
  }

  /// <summary>
  /// Сохранить коллекцию DocumentTypeWeightHelper.items в системном объекте "Общий шаблон спецификаций"
  /// </summary>
  /// <param name="session">Сессия</param>
  public static void SaveSystemCollection(IUserSession session)
  {
    DocumentTypeWeightHelper.InitStaticFields(session);
    DocumentTypeWeightHelper.items.UpdateWeights(DocumentTypeWeight.StartWeight, DocumentTypeWeight.WeightDelta);
    DocumentTypeWeightHelper.SaveToObject(session, DocumentTypeWeightHelper.objectCommonSpecificationsTemplate, DocumentTypeWeightHelper.attrDocumentTypesWeights, DocumentTypeWeightHelper.items);
  }

  /// <summary>
  /// Загрузить коллекцию "весов" типов объектов-документов из указанного атрибута указанного объекта.
  /// В случае ошибки будет возвращена коллекция по умолчанию (из метода AcceptableDocumentTypesCollection)
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="attrID">Идентификатор атрибута (тип "Короткие двоичные данные")</param>
  /// <returns>Коллекция "весов" типов объектов-документов</returns>
  public static DocumentTypeWeightCollection LoadFromObject(
    IUserSession session,
    long objectID,
    int attrID)
  {
    DocumentTypeWeightCollection weightCollection1 = DocumentTypeWeightHelper.AcceptableDocumentTypesCollection(session);
    if (session == null || objectID == 0L || attrID == 0)
      return weightCollection1;
    IDBObject dbObject = session.GetObject(objectID, false);
    if (dbObject == null)
      return weightCollection1;
    IDBAttribute attributeById = dbObject.GetAttributeByID(attrID);
    if (attributeById == null)
      return weightCollection1;
    MemoryStream outStream = new MemoryStream();
    try
    {
      if (attributeById is IBlobReader blobReader)
      {
        BlobInformation blobInformation = blobReader.OpenBlob(0);
        if (blobInformation.RealFileSize > 0L)
        {
          byte[] buffer = blobReader.ReadDataBlock(0);
          if (buffer != null)
          {
            if (buffer.Length != 0)
            {
              using (MemoryStream inStream = new MemoryStream(buffer))
              {
                try
                {
                  long num;
                  if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
                  {
                    outStream = new MemoryStream();
                    num = ZLibStreamHelper.UnpackStream((Stream) inStream, (Stream) outStream);
                  }
                  else
                  {
                    num = inStream.Length;
                    outStream = inStream;
                  }
                  if (num > 0L)
                  {
                    outStream.Seek(0L, SeekOrigin.Begin);
                    try
                    {
                      DocumentTypeWeightCollection weightCollection2 = new DocumentTypeWeightCollection();
                      weightCollection2.LoadFromStream(session, (Stream) outStream, true);
                      weightCollection1 = weightCollection2;
                    }
                    catch
                    {
                    }
                  }
                }
                catch
                {
                }
              }
            }
          }
        }
      }
    }
    finally
    {
      outStream.Close();
    }
    return weightCollection1;
  }

  /// <summary>
  /// Сохранить коллекцию "весов" типов объектов-документов в указанный атрибут указанного объекта.
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="attrID">Идентификатор атрибута (тип "Короткие двоичные данные")</param>
  /// <param name="collection">Коллекция "весов" типов объектов-документов</param>
  public static void SaveToObject(
    IUserSession session,
    long objectID,
    int attrID,
    DocumentTypeWeightCollection collection)
  {
    if (session == null || objectID == 0L || attrID == 0 || collection == null)
      return;
    IDBObject dbObject = session.GetObject(objectID, false);
    if (dbObject == null || (dbObject.ObjectModifyMode == ObjectModifyModes.InBase ? 1 : (dbObject.ObjectModifyMode != ObjectModifyModes.Checkout ? 0 : (dbObject.CheckoutBy == session.UserID ? 1 : 0))) == 0)
      return;
    IDBAttribute attributeById = dbObject.GetAttributeByID(attrID);
    if (attributeById == null || attributeById.ReadOnly)
      return;
    MemoryStream inStream = new MemoryStream();
    MemoryStream outStream = new MemoryStream();
    try
    {
      collection.SaveToStream(session, (Stream) inStream, false);
      inStream.Position = 0L;
      if (!(attributeById is IBlobWriter blobWriter))
        return;
      long num = ZLibStreamHelper.PackStream((Stream) inStream, ZLibCompressLevels.LevelNormal, (Stream) outStream);
      long length1 = inStream.Length;
      ArcMethods arcMethod = ArcMethods.ZLibPacked;
      byte[] array;
      long length2;
      if (num > 0L)
      {
        array = outStream.ToArray();
        length2 = outStream.Length;
      }
      else
      {
        array = inStream.ToArray();
        length2 = inStream.Length;
        arcMethod = ArcMethods.NotPacked;
      }
      BlobInformation blobInfo = new BlobInformation(length1, length2, DateTime.Now, "DocumentTypesWeights.xml", arcMethod, string.Empty);
      blobWriter.OpenBlob(blobInfo, false);
      blobWriter.WriteDataBlock(array);
    }
    finally
    {
      inStream.Close();
      outStream.Close();
    }
  }
}
