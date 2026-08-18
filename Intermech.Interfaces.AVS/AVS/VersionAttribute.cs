// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.VersionAttribute
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.Kernel.Search;
using System;
using System.Diagnostics;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>
/// Класс, в котором хранятся идентификатор типа атрибута, источник атрибута, разделитель для него, тип данных для атрибута
/// </summary>
[Serializable]
public sealed class VersionAttribute : 
  ICloneable,
  IComparable,
  IComparable<VersionAttribute>,
  IMetaDataSync,
  IXMLStorageLoadSave
{
  /// <summary>
  /// Главный узел XML, в котором сохраняется список кратких описаний типов атрибутов примечания и их разделителей
  /// </summary>
  [NonSerialized]
  public const string xmlMainNode = "VersionAttributes";
  /// <summary>В данном узле сохраняются опции</summary>
  [NonSerialized]
  public const string xmlOptionsNode = "VersionAttributesOptions";
  /// <summary>
  /// Узел XML, в котором сохраняется краткое описание типа атрибута примечания
  /// </summary>
  [NonSerialized]
  public const string xmlAttrNode = "attr";
  /// <summary>Атрибут для guid типа атрибута - "guid"</summary>
  [NonSerialized]
  public const string xmlattrGuid = "guid";
  /// <summary>Атрибут для источника атрибута - "source"</summary>
  [NonSerialized]
  public const string xmlattrSource = "source";
  /// <summary>
  /// Атрибут для источника атрибута - "onlyforwithoutdrawing"
  /// </summary>
  [NonSerialized]
  public const string xmlattrWithoutDrawing = "withoutdrawing";
  /// <summary>
  /// Атрибут для разделителя между атрибутами - "separator"
  /// </summary>
  [NonSerialized]
  public const string xmlattrSeparator = "separator";
  /// <summary>Атрибут для текста переменных данных"</summary>
  [NonSerialized]
  public const string xmlattrVariableDataCaptionText = "variabledatacaption";
  /// <summary>Атрибут для хранения опций - "options"</summary>
  [NonSerialized]
  public const string xmlattrOptions = "options";
  /// <summary>Идентификатор типа атрибута</summary>
  private int _id;
  /// <summary>Источник атрибута</summary>
  private AttributeSourceTypes _attrSource = AttributeSourceTypes.Relation;
  /// <summary>Разделитель</summary>
  private string _separator = "\r\n";
  /// <summary>Тип данных атрибута</summary>
  private FieldTypes _attrType;

  /// <summary>Идентификатор типа атрибута</summary>
  public int ID
  {
    [DebuggerStepThrough] get => this._id;
    set
    {
      this._id = value;
      this.Update();
    }
  }

  /// <summary>Источник атрибута</summary>
  public AttributeSourceTypes AttrSource
  {
    [DebuggerStepThrough] get => this._attrSource;
    [DebuggerStepThrough] set => this._attrSource = value;
  }

  /// <summary>Разделитель</summary>
  public string Separator
  {
    [DebuggerStepThrough] get => this._separator;
    [DebuggerStepThrough] set => this._separator = value;
  }

  /// <summary>Тип данных атрибута</summary>
  public FieldTypes AttrType
  {
    [DebuggerStepThrough] get => this._attrType;
  }

  /// <summary>
  /// Обновить внутренние поля, если изменился идентификатор атрибута
  /// </summary>
  private void Update()
  {
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this._id);
    if (attributeType != null)
      this._attrType = attributeType.RealFieldType;
    else
      this._attrType = FieldTypes.ftUnknown;
  }

  /// <summary>Создать краткое описание типа атрибута</summary>
  /// <param name="attrID">Идентификатор типа атрибута</param>
  /// <param name="attrSource">Источник атрибута</param>
  public VersionAttribute(int attrID, AttributeSourceTypes attrSource)
  {
    this.ID = attrID;
    this.AttrSource = attrSource;
  }

  /// <summary>Создать краткое описание типа атрибута</summary>
  /// <param name="attrID">Идентификатор типа атрибута</param>
  /// <param name="attrSource">Источник атрибута</param>
  /// <param name="separator">Разделитель</param>
  public VersionAttribute(int attrID, AttributeSourceTypes attrSource, string separator)
    : this(attrID, attrSource)
  {
    this.Separator = separator;
  }

  /// <summary>Создать краткое описание типа атрибута по прототипу</summary>
  /// <param name="template">Прототип краткого описания типа атрибута</param>
  public VersionAttribute(VersionAttribute template)
  {
    if (template == null)
      return;
    this.ID = template.ID;
    this.AttrSource = template.AttrSource;
    this.Separator = template.Separator;
  }

  /// <summary>Выполнить сравнение с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is VersionAttribute versionAttribute))
      return base.Equals(obj);
    return this.ID == versionAttribute.ID && this.AttrSource == versionAttribute.AttrSource;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.ID.GetHashCode() << 2 | this.AttrSource.GetHashCode();

  /// <summary>Вернуть строковое представление экземпляра класса</summary>
  /// <returns>Строковое представление экземпляра класса</returns>
  public override string ToString()
  {
    return $"[{this.ID}.{this.AttrSource}] \"{this.Separator}\" - \"{MetaDataHelper.GetAttributeTypeName(this.ID)}\"";
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => (object) new VersionAttribute(this);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(object obj) => this.CompareTo(obj as VersionAttribute);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(VersionAttribute other)
  {
    return other == null ? 1 : MetaDataHelper.GetAttributeTypeName(this.ID).CompareTo(MetaDataHelper.GetAttributeTypeName(other.ID));
  }

  /// <summary>Выполнить синхронизацию с кэшем метаданных</summary>
  public void SyncMetaData()
  {
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.ID);
    if (attributeType == null || !VersionAttributesHelper.IsAcceptableAttrType(attributeType.RealFieldType))
      this.ID = 0;
    else
      this.ID = attributeType.AttributeID;
  }

  /// <summary>Загрузить данные из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public void Load(XMLSettingsStorage xmlStorage, XmlNode node)
  {
    this.ID = 0;
    if (xmlStorage == null || node == null || node.Name != "attr")
      return;
    Guid attributeAsGuid = xmlStorage.GetAttributeAsGuid(node, "guid", Guid.Empty);
    if (!MetaDataHelper.ExistsAttributeType(attributeAsGuid))
      return;
    this.ID = MetaDataHelper.GetAttributeTypeID(attributeAsGuid);
    this.AttrSource = (AttributeSourceTypes) xmlStorage.GetAttributeAsInt32(node, "source", 2);
    xmlStorage.GetAttributeValue(node, "separator", "\r\n");
    try
    {
      this.Separator = Encoding.UTF8.GetString(Convert.FromBase64String(xmlStorage.GetAttributeValue(node, "separator", Convert.ToBase64String(Encoding.UTF8.GetBytes("\r\n")))));
    }
    catch
    {
      this.Separator = "\r\n";
    }
  }

  /// <summary>
  /// Сохранить данные в состав указанного родительского узла
  /// </summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
  public void Save(XMLSettingsStorage xmlStorage, XmlNode parentNode)
  {
    this.SyncMetaData();
    if (this.ID == 0 || xmlStorage == null || parentNode == null)
      return;
    string attrValue = MetaDataHelper.GetAttributeTypeGuid(this.ID).ToString();
    XmlNode nodeWithAttr1 = xmlStorage.FindNodeWithAttr(parentNode, "attr", "guid", attrValue, true);
    parentNode.RemoveChild(nodeWithAttr1);
    XmlNode nodeWithAttr2 = xmlStorage.FindNodeWithAttr(parentNode, "attr", "guid", attrValue, true);
    xmlStorage.SetAttributeValue(nodeWithAttr2, "guid", attrValue);
    xmlStorage.SetAttributeValue(nodeWithAttr2, "source", ((int) this.AttrSource).ToString());
    byte[] bytes = Encoding.UTF8.GetBytes(this.Separator);
    xmlStorage.SetAttributeValue(nodeWithAttr2, "separator", Convert.ToBase64String(bytes));
  }
}
