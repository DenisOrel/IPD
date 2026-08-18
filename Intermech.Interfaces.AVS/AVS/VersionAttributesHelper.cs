// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.VersionAttributesHelper
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>
/// Вспомогательный статический класс для работы с атрибутами, отображаемыми в примечаниях спецификаций
/// </summary>
public class VersionAttributesHelper
{
  /// <summary>
  /// В коллекции хранятся стандартные значения разделителей и их текстовые описания.
  /// Можно использовать метод для получения какого-либо описания для значения разделителя.
  /// </summary>
  public Dictionary<string, string> SeparatorDescriptors = new Dictionary<string, string>();
  /// <summary>Разделитель по умолчанию - " "</summary>
  public const string DefaultSeparator = "\r\n";
  /// <summary>Коллекция атрибутов примечаний в спецификациях</summary>
  private List<VersionAttribute> items;
  /// <summary>Опции</summary>
  public VersionAttributesOptions Options = VersionAttributesOptions.ShowMeasureUnits;
  public string VariableDataCaption = "Переменные данные для исполнений:";
  public const string DefaultVariableDataCaption = "Переменные данные для исполнений:";
  private const char NonBreakSpace = '\u000E';
  private const char NonBreakDash = '\u0017';
  private const char ParagraphChar = '\u0015';

  public List<VersionAttribute> Items
  {
    get => this.items;
    set => this.items = value;
  }

  /// <summary>Статический конструктор</summary>
  public VersionAttributesHelper()
  {
    this.SeparatorDescriptors.Clear();
    this.SeparatorDescriptors.Add("", "\"\"  [без разделителя]");
    this.SeparatorDescriptors.Add(" ", "\" \" [пробел]");
    this.SeparatorDescriptors.Add("\r\n", "\"¶\" [принудительный перенос]");
    this.SeparatorDescriptors.Add('\u000E'.ToString(), "\"º\" [неразрывный пробел]");
  }

  /// <summary>Инициализировать статические поля</summary>
  public void Init()
  {
    if (this.Items != null)
      return;
    this.Items = VersionAttributesHelper.GetDefaultAttributes();
  }

  /// <summary>
  /// Обработать таблицу с описаниями типов атрибутов, добавить в список (без дублирования) подходящие типы атрибутов
  /// </summary>
  /// <param name="attributes">Таблица с описаниями типов атрибутов</param>
  /// <param name="result">Список подходящий типов атрибутов</param>
  /// <param name="attrSource">Источник атрибутов</param>
  private static void ParseAttrDataTable(
    DataTable attributes,
    List<VersionAttribute> result,
    AttributeSourceTypes attrSource)
  {
    if (attributes == null || attributes.Rows.Count == 0 || result == null)
      return;
    for (int index = 0; index < attributes.Rows.Count; ++index)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Convert.ToInt32(attributes.Rows[index]["F_ATTRIBUTE_ID"]));
      if (VersionAttributesHelper.IsAcceptableAttrType(attributeType.RealFieldType))
      {
        VersionAttribute versionAttribute = new VersionAttribute(attributeType.AttributeID, attrSource);
        if (!result.Contains(versionAttribute))
          result.Add(versionAttribute);
      }
    }
  }

  /// <summary>Обменять местами два указанных элемента в коллекции</summary>
  /// <param name="items">Коллекция элементов</param>
  /// <param name="index1">Первый элемент коллекции</param>
  /// <param name="index2">Второй элемент коллекции</param>
  public static void Swap(IList items, int index1, int index2)
  {
    if (items == null)
      return;
    object obj1 = items[index1];
    object obj2 = items[index2];
    items[index1] = obj2;
    items[index2] = obj1;
  }

  /// <summary>Передвинуть элемент коллекции с указанным индексом на delta позиций в списке</summary>
  /// <param name="items">Коллекция элементов</param>
  /// <param name="index">Индекс передвигаемого элемента коллекции</param>
  /// <param name="delta">На сколько позиций в списке передвинуть указанный элемент</param>
  public static void Shift(IList items, int index, int delta)
  {
    if (items == null || delta == 0)
      return;
    object obj = items[index];
    if (delta > 0)
    {
      int num = items.Count - index - 1;
      delta = delta > num ? num : delta;
      for (int index1 = index; index1 < index + delta; ++index1)
        VersionAttributesHelper.Swap(items, index1, index1 + 1);
    }
    else
    {
      delta = Math.Abs(delta) > index ? -index : delta;
      for (int index1 = index; index1 > index + delta; --index1)
        VersionAttributesHelper.Swap(items, index1, index1 - 1);
    }
  }

  /// <summary>Получить описание для указанного значения разделителя</summary>
  /// <param name="value">Значение разделителя</param>
  /// <returns>Описание для указанного значения разделителя</returns>
  public string GetSeparatorDescription(string value)
  {
    return this.SeparatorDescriptors.ContainsKey(value) ? this.SeparatorDescriptors[value] : value;
  }

  /// <summary>Получить список атрибутов по умолчанию</summary>
  /// <returns>Список атрибутов по умолчанию</returns>
  public static List<VersionAttribute> GetDefaultAttributes()
  {
    return new List<VersionAttribute>()
    {
      new VersionAttribute(MetaDataHelper.GetAttributeTypeID("cad0038b-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object)
    };
  }

  /// <summary>
  /// Проверить, можно ли использовать атрибут указанного типа в примечаниях спецификаций
  /// </summary>
  /// <param name="attrType">Тип данных атрибута</param>
  /// <returns>true, если можно использовать атрибут указанного типа в примечаниях спецификаций</returns>
  public static bool IsAcceptableAttrType(FieldTypes attrType)
  {
    return attrType == FieldTypes.ftAutoInc || attrType == FieldTypes.ftBoolean || attrType == FieldTypes.ftDateTime || attrType == FieldTypes.ftDouble || attrType == FieldTypes.ftInteger || attrType == FieldTypes.ftMeasured || attrType == FieldTypes.ftString || attrType == FieldTypes.ftMemo;
  }

  /// <summary>
  /// Получить список всех типов атрибутов в системе, отфильтровав их по типам данных и указав их источник
  /// </summary>
  /// <param name="attrSource">Источник атрибутов</param>
  /// <returns>Список всех типов атрибутов в системе, отфильтрованный по типам данных и с указанным источником</returns>
  public static List<VersionAttribute> GetAllAttributes(AttributeSourceTypes attrSource)
  {
    List<VersionAttribute> allAttributes = new List<VersionAttribute>();
    List<IMSAttributeType> attributeTypesList = MetaDataHelper.GetAttributeTypesList();
    for (int index = 0; index < attributeTypesList.Count; ++index)
    {
      if (VersionAttributesHelper.IsAcceptableAttrType(attributeTypesList[index].RealFieldType))
        allAttributes.Add(new VersionAttribute(attributeTypesList[index].AttributeID, attrSource));
    }
    allAttributes.Sort();
    return allAttributes;
  }

  /// <summary>
  /// Вернуть список допустимых типов атрибутов для указанного типа связи
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <param name="includeAllAttrs">Если true, то в список попадают все атрибуты, которые есть в системе</param>
  /// <returns>Список допустимых типов атрибутов для указанного типа связи</returns>
  public static List<VersionAttribute> GetRelTypeAttributes(
    IUserSession session,
    int relTypeID,
    bool includeAllAttrs)
  {
    List<VersionAttribute> result = new List<VersionAttribute>();
    if (session == null || MetaDataHelper.GetRelationType(relTypeID) == null)
      return result;
    if (includeAllAttrs)
      return VersionAttributesHelper.GetAllAttributes(AttributeSourceTypes.Relation);
    IDBRelationType relationType = session.GetRelationType(relTypeID, false);
    if (relationType == null)
      return result;
    DataTable attributes = relationType.Attributes.Select("F_ATTRIBUTE_ID");
    VersionAttributesHelper.ParseAttrDataTable(attributes, result, AttributeSourceTypes.Relation);
    attributes?.Dispose();
    result.Sort();
    return result;
  }

  /// <summary>
  /// Получить список атрибутов, которые можно отображать в примечаниях спецификаций
  /// (изучается весь список типов объектов, которые могут оказаться в спецификации)
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <returns>Список атрибутов, которые можно отображать в примечаниях спецификаций</returns>
  public static List<VersionAttribute> GetSpecAcceptableAttributes(IUserSession session)
  {
    List<VersionAttribute> acceptableAttributes = new List<VersionAttribute>();
    if (session == null)
      return acceptableAttributes;
    List<int> objTypeIDs = VersionAttributesHelper.AcceptableObjectTypes(session);
    return VersionAttributesHelper.GetObjTypesAttributes(session, objTypeIDs, false);
  }

  /// <summary>
  /// Вернуть список допустимых типов атрибутов для указанных типов объектов
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="objTypeIDs">Идентификаторы типов объектов</param>
  /// <param name="includeAllAttrs">Если true, то в список попадают все атрибуты, которые есть в системе</param>
  /// <returns>Список допустимых типов атрибутов для указанных типов объектов</returns>
  public static List<VersionAttribute> GetObjTypesAttributes(
    IUserSession session,
    List<int> objTypeIDs,
    bool includeAllAttrs)
  {
    List<VersionAttribute> result = new List<VersionAttribute>();
    if (session == null || objTypeIDs == null || objTypeIDs.Count == 0)
      return result;
    if (includeAllAttrs)
      return VersionAttributesHelper.GetAllAttributes(AttributeSourceTypes.Object);
    for (int index = 0; index < objTypeIDs.Count; ++index)
    {
      IDBObjectType objectType = session.GetObjectType(objTypeIDs[index], false);
      if (objectType == null)
        return result;
      DataTable attributes = objectType.Attributes.Select("F_ATTRIBUTE_ID");
      VersionAttributesHelper.ParseAttrDataTable(attributes, result, AttributeSourceTypes.Object);
      attributes?.Dispose();
    }
    result.Sort();
    return result;
  }

  /// <summary>
  /// Получить список типов объектов, атрибуты которых можно показать в редакторе списка атрибутов
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <returns>Список типов объектов, атрибуты которых можно показать в редакторе списка атрибутов</returns>
  public static List<int> AcceptableObjectTypes(IUserSession session)
  {
    List<int> intList1 = new List<int>();
    if (session == null)
      return intList1;
    int relationTypeId = MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545");
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
    DataTable applicabilitiesList = session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(relationTypeId, objectTypeId, -1);
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
    return intList2;
  }

  /// <summary>
  /// Загрузить из системного объекта "Общий шаблон спецификаций" список атрибутов,
  /// отображаемых в примечаниях спецификаций
  /// </summary>
  /// <param name="session">Сессия</param>
  public void LoadVersionsAttributes(long settingsObjectID, IUserSession session)
  {
    this.Init();
    this.Items = VersionAttributesHelper.GetDefaultAttributes();
    if (session == null)
      return;
    IDBObject objectActual = session.GetObjectActual(settingsObjectID, true);
    if (objectActual == null)
      return;
    IDBAttribute attributeById = objectActual.GetAttributeByID(AvsIDCache.Attr_VariableDataProductCaption);
    if (attributeById == null)
      return;
    MemoryStream outStream = new MemoryStream();
    try
    {
      if (!(attributeById is IBlobReader blobReader))
        return;
      BlobInformation blobInformation = blobReader.OpenBlob(0);
      if (blobInformation.RealFileSize <= 0L)
        return;
      byte[] buffer = blobReader.ReadDataBlock(0);
      if (buffer == null || buffer.Length == 0)
        return;
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
          if (num <= 0L)
            return;
          outStream.Seek(0L, SeekOrigin.Begin);
          try
          {
            outStream.Position = 0L;
            XMLSettingsStorage xmlStorage = new XMLSettingsStorage((Stream) outStream);
            XmlNode node = xmlStorage.document != null ? xmlStorage.FindNode((XmlNode) xmlStorage.document.DocumentElement, "VersionAttributes", false) : (XmlNode) null;
            if (node == null)
              return;
            this.Items.Clear();
            for (int i = 0; i < node.ChildNodes.Count; ++i)
            {
              XmlNode childNode = node.ChildNodes[i];
              if (!(childNode.Name != "attr"))
              {
                VersionAttribute versionAttribute = new VersionAttribute((VersionAttribute) null);
                versionAttribute.Load(xmlStorage, childNode);
                if (versionAttribute.ID != 0 && !this.Items.Contains(versionAttribute))
                  this.Items.Add(versionAttribute);
              }
            }
            this.Options = (VersionAttributesOptions) xmlStorage.GetAttributeAsInt32(node, "options", 1);
            this.VariableDataCaption = xmlStorage.GetAttributeValue(node, "variabledatacaption", "Переменные данные для исполнений:");
          }
          catch
          {
          }
        }
        catch
        {
        }
      }
    }
    finally
    {
      outStream.Close();
    }
  }

  /// <summary>
  /// Сохранить в системном объекте "Общий шаблон спецификаций" список атрибутов,
  /// отображаемых в примечаниях спецификаций
  /// </summary>
  /// <param name="session">Сессия</param>
  public void SaveVersionsAttributes(long settingsObjectID, IUserSession session)
  {
    this.Init();
    if (session == null)
      return;
    IDBObject dbObject = session.GetObject(settingsObjectID, false);
    if (dbObject == null || (dbObject.ObjectModifyMode == ObjectModifyModes.InBase ? 1 : (dbObject.ObjectModifyMode != ObjectModifyModes.Checkout ? 0 : (dbObject.CheckoutBy == session.UserID ? 1 : 0))) == 0)
      return;
    if (dbObject.GetAttributeByID(AvsIDCache.Attr_VariableDataProductCaption) == null)
    {
      if (dbObject.CheckoutBy == 0L && dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
        dbObject = dbObject.CheckOut();
      if (dbObject != null && (dbObject.CheckoutBy == session.UserID || dbObject.ObjectModifyMode == ObjectModifyModes.InBase))
        dbObject.Attributes.AddAttribute(AvsIDCache.Attr_VariableDataProductCaption, false);
    }
    IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_VariableDataProductCaption);
    if (attributeById == null || attributeById.ReadOnly || attributeById == null)
      return;
    MemoryStream inStream = new MemoryStream();
    MemoryStream outStream = new MemoryStream();
    try
    {
      XMLSettingsStorage xmlStorage = new XMLSettingsStorage();
      XmlNode documentElement = (XmlNode) xmlStorage.document.DocumentElement;
      XmlNode node1 = xmlStorage.FindNode(documentElement, "VersionAttributes", true);
      documentElement.RemoveChild(node1);
      XmlNode node2 = xmlStorage.FindNode(documentElement, "VersionAttributes", true);
      xmlStorage.SetAttributeValue(node2, "version", "6.0");
      for (int index = 0; index < this.Items.Count; ++index)
        this.Items[index].Save(xmlStorage, node2);
      xmlStorage.SetAttributeValue(node2, "options", ((int) this.Options).ToString());
      xmlStorage.SetAttributeValue(node2, "variabledatacaption", this.VariableDataCaption);
      inStream.Position = 0L;
      xmlStorage.Save((Stream) inStream);
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
      BlobInformation blobInfo = new BlobInformation(length1, length2, DateTime.Now, "VersionAttributes.xml", arcMethod, string.Empty);
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
