// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.NoteFieldSettings
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
using System.Linq;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>
/// Вспомогательный статический класс для работы с атрибутами, отображаемыми в примечаниях спецификаций
/// </summary>
public class NoteFieldSettings
{
  /// <summary>
  /// В коллекции хранятся стандартные значения разделителей и их текстовые описания.
  /// Можно использовать метод для получения какого-либо описания для значения разделителя.
  /// </summary>
  public static Dictionary<string, string> SeparatorDescriptors = new Dictionary<string, string>();
  /// <summary>Разделитель по умолчанию - " "</summary>
  public const string DefaultSeparator = " ";
  /// <summary>Коллекция атрибутов примечаний в спецификациях</summary>
  public List<RemarkAttribute> Items;
  /// <summary>Опции</summary>
  public NoteFieldOptions Options = NoteFieldOptions.ShowMeasureUnits;
  private const char NonBreakSpace = '\u000E';

  public NoteFieldSettings()
  {
    if (this.Items != null)
      return;
    this.Items = NoteFieldSettings.GetDefaultAttributes(Guid.Empty);
  }

  /// <summary>Статический конструктор</summary>
  static NoteFieldSettings()
  {
    NoteFieldSettings.SeparatorDescriptors.Add("", "\"\"  [без разделителя]");
    NoteFieldSettings.SeparatorDescriptors.Add(" ", "\" \" [пробел]");
    NoteFieldSettings.SeparatorDescriptors.Add("\r\n", "\"¶\" [принудительный перенос]");
    NoteFieldSettings.SeparatorDescriptors.Add('\u000E'.ToString(), "\"º\" [неразрывный пробел]");
  }

  /// <summary>
  /// Обработать таблицу с описаниями типов атрибутов, добавить в список (без дублирования) подходящие типы атрибутов
  /// </summary>
  /// <param name="attributes">Таблица с описаниями типов атрибутов</param>
  /// <param name="result">Список подходящий типов атрибутов</param>
  /// <param name="attrSource">Источник атрибутов</param>
  private static void ParseAttrDataTable(
    DataTable attributes,
    List<RemarkAttribute> result,
    AttributeSourceTypes attrSource)
  {
    if (attributes == null || attributes.Rows.Count == 0 || result == null)
      return;
    for (int index = 0; index < attributes.Rows.Count; ++index)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Convert.ToInt32(attributes.Rows[index]["F_ATTRIBUTE_ID"]));
      if (NoteFieldSettings.IsAcceptableAttrType(attributeType.RealFieldType))
      {
        RemarkAttribute remarkAttribute = new RemarkAttribute(attributeType.AttributeID, attrSource);
        if (!result.Contains(remarkAttribute))
          result.Add(remarkAttribute);
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
        NoteFieldSettings.Swap(items, index1, index1 + 1);
    }
    else
    {
      delta = Math.Abs(delta) > index ? -index : delta;
      for (int index1 = index; index1 > index + delta; --index1)
        NoteFieldSettings.Swap(items, index1, index1 - 1);
    }
  }

  /// <summary>Получить описание для указанного значения разделителя</summary>
  /// <param name="value">Значение разделителя</param>
  /// <returns>Описание для указанного значения разделителя</returns>
  public static string GetSeparatorDescription(string value)
  {
    return NoteFieldSettings.SeparatorDescriptors.ContainsKey(value) ? NoteFieldSettings.SeparatorDescriptors[value] : value;
  }

  /// <summary>Получить список атрибутов по умолчанию</summary>
  /// <returns>Список атрибутов по умолчанию</returns>
  public static List<RemarkAttribute> GetDefaultAttributes(Guid settingsObjectGuid)
  {
    List<RemarkAttribute> defaultAttributes = new List<RemarkAttribute>();
    if (settingsObjectGuid == AvsIDCache.StdTemplateElementList)
    {
      defaultAttributes.Add(new RemarkAttribute(AvsIDCache.Attr_NotePE, AttributeSourceTypes.Relation));
      defaultAttributes.Add(new RemarkAttribute(MetaDataHelper.GetAttributeTypeID("cad00274-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation));
    }
    else
    {
      defaultAttributes.Add(new RemarkAttribute(MetaDataHelper.GetAttributeTypeID("cad00255-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object));
      defaultAttributes.Add(new RemarkAttribute(MetaDataHelper.GetAttributeTypeID("cad0027a-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation));
      defaultAttributes.Add(new RemarkAttribute(MetaDataHelper.GetAttributeTypeID("cad00021-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation));
      defaultAttributes.Add(new RemarkAttribute(MetaDataHelper.GetAttributeTypeID("cad00274-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation));
    }
    return defaultAttributes;
  }

  /// <summary>
  /// Проверить, можно ли использовать атрибут указанного типа в примечаниях спецификаций
  /// </summary>
  /// <param name="attrType">Тип данных атрибута</param>
  /// <returns>true, если можно использовать атрибут указанного типа в примечаниях спецификаций</returns>
  public static bool IsAcceptableAttrType(FieldTypes attrType)
  {
    return attrType == FieldTypes.ftAutoInc || attrType == FieldTypes.ftBoolean || attrType == FieldTypes.ftDateTime || attrType == FieldTypes.ftDouble || attrType == FieldTypes.ftInteger || attrType == FieldTypes.ftMeasured || attrType == FieldTypes.ftString || attrType == FieldTypes.ftObjectLink || attrType == FieldTypes.ftMemo;
  }

  /// <summary>
  /// Получить список всех типов атрибутов в системе, отфильтровав их по типам данных и указав их источник
  /// </summary>
  /// <param name="attrSource">Источник атрибутов</param>
  /// <returns>Список всех типов атрибутов в системе, отфильтрованный по типам данных и с указанным источником</returns>
  public static List<RemarkAttribute> GetAllAttributes(AttributeSourceTypes attrSource)
  {
    List<RemarkAttribute> allAttributes = new List<RemarkAttribute>();
    List<IMSAttributeType> attributeTypesList = MetaDataHelper.GetAttributeTypesList();
    for (int index = 0; index < attributeTypesList.Count; ++index)
    {
      if (NoteFieldSettings.IsAcceptableAttrType(attributeTypesList[index].RealFieldType))
        allAttributes.Add(new RemarkAttribute(attributeTypesList[index].AttributeID, attrSource));
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
  public static List<RemarkAttribute> GetRelTypeAttributes(
    IUserSession session,
    int relTypeID,
    bool includeAllAttrs)
  {
    List<RemarkAttribute> result = new List<RemarkAttribute>();
    if (session == null || MetaDataHelper.GetRelationType(relTypeID) == null)
      return result;
    if (includeAllAttrs)
    {
      result = NoteFieldSettings.GetAllAttributes(AttributeSourceTypes.Relation);
    }
    else
    {
      IDBRelationType relationType = session.GetRelationType(relTypeID, false);
      if (relationType == null)
        return result;
      DataTable attributes = relationType.Attributes.Select("F_ATTRIBUTE_ID");
      NoteFieldSettings.ParseAttrDataTable(attributes, result, AttributeSourceTypes.Relation);
      attributes?.Dispose();
    }
    result.AddRange(((IEnumerable<AvsRowAttributeInfo>) AvsIDCache.VirtualAttributes).Where<AvsRowAttributeInfo>((System.Func<AvsRowAttributeInfo, bool>) (a => a.IsRelationAttribute)).Select<AvsRowAttributeInfo, RemarkAttribute>((System.Func<AvsRowAttributeInfo, RemarkAttribute>) (a => new RemarkAttribute(a))));
    result.Sort();
    return result;
  }

  /// <summary>Вернуть список допустимых типов атрибутов для указанных типов объектов</summary>
  /// <param name="session">Сессия</param>
  /// <param name="objTypeIDs">Идентификаторы типов объектов</param>
  /// <param name="includeAllAttrs">Если true, то в список попадают все атрибуты, которые есть в системе</param>
  /// <returns>Список допустимых типов атрибутов для указанных типов объектов</returns>
  public static List<RemarkAttribute> GetObjTypesAttributes(
    IUserSession session,
    List<int> objTypeIDs,
    bool includeAllAttrs)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    List<RemarkAttribute> result = new List<RemarkAttribute>();
    if (objTypeIDs == null || objTypeIDs.Count == 0)
      return result;
    if (includeAllAttrs)
      return NoteFieldSettings.GetAllAttributes(AttributeSourceTypes.Object);
    for (int index = 0; index < objTypeIDs.Count; ++index)
    {
      IDBObjectType objectType = session.GetObjectType(objTypeIDs[index], false);
      if (objectType == null)
        return result;
      DataTable attributes = objectType.Attributes.Select("F_ATTRIBUTE_ID");
      NoteFieldSettings.ParseAttrDataTable(attributes, result, AttributeSourceTypes.Object);
      attributes?.Dispose();
    }
    RemarkAttribute remarkAttribute1 = new RemarkAttribute(AvsIDCache.Attr_Format, AttributeSourceTypes.Object);
    if (!result.Contains(remarkAttribute1))
      result.Add(remarkAttribute1);
    RemarkAttribute remarkAttribute2 = new RemarkAttribute(AvsIDCache.Attr_FirstApplicability, AttributeSourceTypes.Object);
    if (!result.Contains(remarkAttribute2))
      result.Add(remarkAttribute2);
    result.AddRange(((IEnumerable<AvsRowAttributeInfo>) AvsIDCache.VirtualAttributes).Where<AvsRowAttributeInfo>((System.Func<AvsRowAttributeInfo, bool>) (a => a.IsObjectAttribute)).Select<AvsRowAttributeInfo, RemarkAttribute>((System.Func<AvsRowAttributeInfo, RemarkAttribute>) (a => new RemarkAttribute(a))));
    result.Sort();
    return result;
  }

  /// <summary>Получить список атрибутов, которые можно отображать в примечаниях спецификаций
  /// (изучается весь список типов объектов, которые могут оказаться в спецификации)</summary>
  /// <param name="session">Сессия</param>
  /// <returns>Список атрибутов, которые можно отображать в примечаниях спецификаций</returns>
  public static List<RemarkAttribute> GetSpecAcceptableAttributes(IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    List<RemarkAttribute> remarkAttributeList = new List<RemarkAttribute>();
    List<int> objTypeIDs = NoteFieldSettings.AcceptableObjectTypes(session);
    return NoteFieldSettings.GetObjTypesAttributes(session, objTypeIDs, false);
  }

  public RemarkAttribute FindAttribute(bool isRelationAttribute, int attributeID)
  {
    for (int index = 0; index < this.Items.Count; ++index)
    {
      if (this.Items[index].ID == attributeID && (isRelationAttribute && this.Items[index].AttrSource == AttributeSourceTypes.Relation || !isRelationAttribute && this.Items[index].AttrSource == AttributeSourceTypes.Object))
        return this.Items[index];
    }
    return (RemarkAttribute) null;
  }

  /// <summary>Получить список типов объектов, атрибуты которых можно показать в редакторе списка атрибутов</summary>
  /// <param name="session">Сессия</param>
  /// <returns>Список типов объектов, атрибуты которых можно показать в редакторе списка атрибутов</returns>
  public static List<int> AcceptableObjectTypes(IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    List<int> intList1 = new List<int>();
    int relationTypeId1 = MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545");
    int relationTypeId2 = MetaDataHelper.GetRelationTypeID("cad00023-306c-11d8-b4e9-00304f19f545");
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
    DataTable applicabilitiesList = session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(relationTypeId1, objectTypeId, -1);
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
      List<int> childObjectTypesId = MetaDataHelper.GetApplicabilityChildObjectTypesID(intList2[index1], relationTypeId2);
      for (int index2 = 0; index2 < childObjectTypesId.Count; ++index2)
      {
        if (!intList1.Contains(childObjectTypesId[index2]))
          intList1.Add(childObjectTypesId[index2]);
      }
    }
    return intList1;
  }

  /// <summary>Загрузить настройки из потока</summary>
  /// <param name="stream">Поток, в который хранятся данные</param>
  public void LoadFromXML(Stream stream)
  {
    stream.Position = 0L;
    XMLSettingsStorage xmlStorage = new XMLSettingsStorage(stream);
    this.Items.Clear();
    XmlNode node = xmlStorage.document != null ? xmlStorage.FindNode((XmlNode) xmlStorage.document.DocumentElement, "RemarkAttributes", false) : (XmlNode) null;
    if (node != null)
    {
      foreach (XmlNode childNode in node.ChildNodes)
      {
        if (!(childNode.Name != "attr"))
        {
          RemarkAttribute remarkAttribute = new RemarkAttribute();
          remarkAttribute.Load(xmlStorage, childNode);
          if (remarkAttribute.ID != 0 && !this.Items.Contains(remarkAttribute))
            this.Items.Add(remarkAttribute);
        }
      }
    }
    this.Options = (NoteFieldOptions) xmlStorage.GetAttributeAsInt32(node, "options", 1);
  }

  /// <summary>Загрузить из объекта БД список атрибутов, отображаемых в графе Примечание документов AVS</summary>
  /// <param name="settingsObjectID">Идентификатор владельца настроек</param>
  /// <param name="settingsAttributeID">Идентификатор атрибута с настройками</param>
  /// <param name="session">Сессия</param>
  public void LoadFromDBObjectAttribute(
    long settingsObjectID,
    int settingsAttributeID,
    IUserSession session)
  {
    if (Consts.IsUndefinedObjectId(settingsObjectID))
      throw new ArgumentException("Не задан идентификатор объекта настроек", nameof (settingsObjectID));
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    Guid settingsObjectGuid = Guid.Empty;
    if (settingsObjectID != -1L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        settingsObjectGuid = sessionKeeper.Session.GetObjectInfo(settingsObjectID).VersionGuid;
    }
    this.Items = NoteFieldSettings.GetDefaultAttributes(settingsObjectGuid);
    IDBAttribute attributeById = session.GetObjectActual(settingsObjectID, true).GetAttributeByID(settingsAttributeID);
    if (attributeById == null)
      return;
    using (MemoryStream aDestStream = new MemoryStream())
    {
      new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(session);
      aDestStream.Position = 0L;
      if (aDestStream.Length == 0L)
        return;
      this.LoadFromXML((Stream) aDestStream);
    }
  }

  /// <summary>Сохранить настройки в поток</summary>
  /// <param name="stream">Поток, в который сохраняются данные</param>
  public void SaveToXML(Stream stream)
  {
    XMLSettingsStorage xmlStorage = new XMLSettingsStorage();
    XmlNode documentElement = (XmlNode) xmlStorage.document.DocumentElement;
    XmlNode node1 = xmlStorage.FindNode(documentElement, "RemarkAttributes", true);
    documentElement.RemoveChild(node1);
    XmlNode node2 = xmlStorage.FindNode(documentElement, "RemarkAttributes", true);
    for (int index = 0; index < this.Items.Count; ++index)
      this.Items[index].Save(xmlStorage, node2);
    xmlStorage.SetAttributeValue(node2, "options", ((int) this.Options).ToString());
    xmlStorage.Save(stream);
  }

  /// <summary>Сохранить в объект БД список атрибутов, отображаемых в графе Примечание документов AVS</summary>
  /// <param name="settingsObjectID">Идентификатор владельца настроек</param>
  /// <param name="settingsAttributeID">Идентификатор атрибута с настройками</param>
  /// <param name="session">Сессия</param>
  public void SaveToDBObjectAttribute(
    long settingsObjectID,
    int settingsAttributeID,
    IUserSession session)
  {
    IDBObject dbObject = session != null ? session.GetObject(settingsObjectID) : throw new ArgumentNullException(nameof (session));
    if (dbObject.GetAttributeByID(settingsAttributeID) == null)
      dbObject.Attributes.AddAttribute(settingsAttributeID, false);
    using (MemoryStream aSourceStream = new MemoryStream())
    {
      this.SaveToXML((Stream) aSourceStream);
      aSourceStream.Position = 0L;
      BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, string.Empty);
      new BlobProcWriter(dbObject.ObjectID, AttributableElements.Object, settingsAttributeID, 0, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      aSourceStream.Position = 0L;
    }
  }
}
