// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.IMDocObjectTypeSettings
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces;
using System;
using System.Diagnostics;
using System.Xml;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>
/// Класс, в котором хранятся настройки для типа объекта, открываемого инструментом "Редактор документов"
/// </summary>
internal sealed class IMDocObjectTypeSettings : 
  IAssignable,
  IMetaDataSync,
  IDatabaseSync,
  IXMLStorageLoadSave,
  ICloneable,
  IComparable,
  IComparable<IMDocObjectTypeSettings>
{
  /// <summary>
  /// Старое имя главного узела XML, в котором сохраняется список типов объектов и их настройки - [IMDocObjectTypeSettings]
  /// </summary>
  [NonSerialized]
  public const string oldXmlMainNode = "AVSObjectTypeSettings";
  /// <summary>
  /// Главный узел XML, в котором сохраняется список типов объектов и их настройки - [IMDocObjectTypeSettings]
  /// </summary>
  [NonSerialized]
  public const string xmlMainNode = "IMDocObjectTypeSettings";
  /// <summary>
  /// Узел XML, в котором сохраняется описание типа объектов и его настройки - [objtype]
  /// </summary>
  [NonSerialized]
  public const string xmlObjectTypeNode = "objtype";
  /// <summary>Атрибут для guid типа объекта - "guid"</summary>
  [NonSerialized]
  public const string xmlattrGuid = "guid";
  /// <summary>Атрибут для guid шаблона - "template"</summary>
  [NonSerialized]
  public const string xmlattrTemplate = "template";
  /// <summary>
  /// Идентификатор типа объекта, который обрабатывается инструментом "Редактор документов".
  /// Значение Intermech.Consts.UnknownObjectTypeId означает то, что указанный тип объекта более не существует.
  /// </summary>
  public int ObjectType;
  /// <summary>
  /// Guid версии объекта-шаблона, который будет передаваться в редактор документов при создании объектов указанного типа.
  /// Значение Guid.Empty означает отсутствие шаблона.
  /// </summary>
  public Guid TemplateGuid;
  /// <summary>
  /// Идентификатор версии объекта-шаблона, который будет передаваться в редактор документов при создании объектов указанного типа.
  /// </summary>
  public long TemplateID;
  /// <summary>
  /// Идентификатор типа объектов шаблона, который будет передаваться в редактор документов при создании объектов указанного типа.
  /// </summary>
  public int TemplateTypeID;
  /// <summary>
  /// Заголовок объекта-шаблона, который будет передаваться в редактор документов при создании объектов указанного типа.
  /// </summary>
  public string TemplateCaption;

  /// <summary>Создать незаполненный экземпляр класса</summary>
  public IMDocObjectTypeSettings()
    : this(-1, Guid.Empty)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="objectType">Идентификатор типа объекта, который обрабатывается инструментом "Редактор документов"</param>
  /// <param name="templateGuid">Guid версии объекта-шаблона, который будет передаваться в редактор документов при создании объектов указанного типа.
  /// Значение Guid.Empty означает отсутствие шаблона.</param>
  public IMDocObjectTypeSettings(int objectType, Guid templateGuid)
  {
    this.ObjectType = objectType;
    this.TemplateGuid = templateGuid;
    if (!(templateGuid != Guid.Empty))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(templateGuid, false);
      if (dbObject == null)
        return;
      this.TemplateCaption = dbObject.Caption;
      this.TemplateID = dbObject.ObjectID;
      this.TemplateTypeID = dbObject.ObjectType;
    }
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="objectType">Идентификатор типа объекта, который обрабатывается инструментом "Редактор документов"</param>
  /// <param name="templateGuid">Guid версии объекта-шаблона, который будет передаваться в редактор документов при создании объектов указанного типа.
  /// Значение Guid.Empty означает отсутствие шаблона.</param>
  /// <param name="templateID">Идентификатор версии объекта-шаблона, который будет передаваться в редактор документов при создании объектов указанного типа.</param>
  /// <param name="templateTypeID">Идентификатор типа объектов шаблона, который будет передаваться в редактор документов при создании объектов указанного типа.</param>
  /// <param name="templateCaption">Заголовок объекта-шаблона, который будет передаваться в редактор документов при создании объектов указанного типа.</param>
  public IMDocObjectTypeSettings(
    int objectType,
    Guid templateGuid,
    long templateID,
    int templateTypeID,
    string templateCaption)
  {
    this.ObjectType = objectType;
    this.TemplateGuid = templateGuid;
    this.TemplateID = templateID;
    this.TemplateTypeID = templateTypeID;
    this.TemplateCaption = templateCaption;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    return obj is IMDocObjectTypeSettings objectTypeSettings && this.ObjectType == objectTypeSettings.ObjectType;
  }

  /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  [DebuggerStepThrough]
  public override int GetHashCode() => this.ObjectType.GetHashCode();

  /// <summary>Получить строковое представление экземпляра класса</summary>
  /// <returns>Строковое представление экземпляра класса</returns>
  [DebuggerStepThrough]
  public override string ToString()
  {
    return !(this.TemplateGuid != Guid.Empty) ? $"[ ] [{this.ObjectType}] \"{MetaDataHelper.GetObjectTypeName(this.ObjectType)}\"" : $"[x] [{this.ObjectType}] \"{MetaDataHelper.GetObjectTypeName(this.ObjectType)}\"";
  }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this.ObjectType = -1;
    this.TemplateGuid = Guid.Empty;
    this.TemplateID = 0L;
    this.TemplateTypeID = -1;
    this.TemplateCaption = string.Empty;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is IMDocObjectTypeSettings objectTypeSettings))
      return;
    this.ObjectType = objectTypeSettings.ObjectType;
    this.TemplateGuid = objectTypeSettings.TemplateGuid;
    this.TemplateID = objectTypeSettings.TemplateID;
    this.TemplateTypeID = objectTypeSettings.TemplateTypeID;
    this.TemplateCaption = objectTypeSettings.TemplateCaption;
  }

  /// <summary>
  /// Выполнить синхронизацию внутренних коллекций с кэшем метаданных
  /// </summary>
  public void SyncMetaData()
  {
    if (MetaDataHelper.ExistsObjectType(this.ObjectType))
      return;
    this.Clear();
  }

  /// <summary>
  /// Выполнить синхронизацию внутренних коллекций с базой данных
  /// </summary>
  /// <param name="session">Ссылка на сессию, в рамках которой выполняется работа с базой данных и сервером приложений</param>
  public void SyncObjectsData(IUserSession session)
  {
    if (session == null)
      return;
    this.TemplateID = 0L;
    this.TemplateTypeID = -1;
    this.TemplateCaption = string.Empty;
    if (this.TemplateGuid == Guid.Empty)
      return;
    IDBObject dbObject = session.GetObject(this.TemplateGuid, false);
    if (dbObject != null)
    {
      this.TemplateID = dbObject.ObjectID;
      this.TemplateTypeID = dbObject.ObjectType;
      this.TemplateCaption = dbObject.Caption;
    }
    else
      this.TemplateGuid = Guid.Empty;
  }

  /// <summary>Загрузить данные из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public void Load(XMLSettingsStorage xmlStorage, XmlNode node)
  {
    this.Clear();
    if (xmlStorage == null || node == null || node.Name != "objtype")
      return;
    Guid attributeAsGuid = xmlStorage.GetAttributeAsGuid(node, "guid", Guid.Empty);
    if (attributeAsGuid == Guid.Empty)
      return;
    this.ObjectType = MetaDataHelper.GetObjectTypeID(attributeAsGuid);
    this.TemplateGuid = xmlStorage.GetAttributeAsGuid(node, "template", Guid.Empty);
    this.SyncMetaData();
  }

  /// <summary>
  /// Сохранить данные в состав указанного родительского узла
  /// </summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
  public void Save(XMLSettingsStorage xmlStorage, XmlNode parentNode)
  {
    this.SyncMetaData();
    if (this.ObjectType == -1)
    {
      this.Clear();
    }
    else
    {
      if (xmlStorage == null || parentNode == null)
        return;
      string attrValue = MetaDataHelper.GetObjectTypeGuid(this.ObjectType).ToString();
      XmlNode nodeWithAttr1 = xmlStorage.FindNodeWithAttr(parentNode, "objtype", "guid", attrValue, true);
      parentNode.RemoveChild(nodeWithAttr1);
      XmlNode nodeWithAttr2 = xmlStorage.FindNodeWithAttr(parentNode, "objtype", "guid", attrValue, true);
      xmlStorage.SetAttributeValue(nodeWithAttr2, "guid", attrValue);
      if (!(this.TemplateGuid != Guid.Empty))
        return;
      xmlStorage.SetAttributeValue(nodeWithAttr2, "template", this.TemplateGuid.ToString());
    }
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone()
  {
    return (object) new IMDocObjectTypeSettings(this.ObjectType, this.TemplateGuid, this.TemplateID, this.TemplateTypeID, this.TemplateCaption);
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(object obj) => this.CompareTo(obj as IMDocObjectTypeSettings);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(IMDocObjectTypeSettings other)
  {
    return other == null ? 1 : MetaDataHelper.GetObjectTypeName(this.ObjectType).CompareTo(MetaDataHelper.GetObjectTypeName(other.ObjectType));
  }
}
