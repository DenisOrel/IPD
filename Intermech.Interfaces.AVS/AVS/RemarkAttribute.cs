// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.RemarkAttribute
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.Interfaces.Attributes;
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
public sealed class RemarkAttribute : 
  ICloneable,
  IComparable,
  IComparable<RemarkAttribute>,
  IMetaDataSync,
  IXMLStorageLoadSave
{
  /// <summary>
  /// Главный узел XML, в котором сохраняется список кратких описаний типов атрибутов примечания и их разделителей
  /// </summary>
  [NonSerialized]
  public const string xmlMainNode = "RemarkAttributes";
  /// <summary>В данном узле сохраняются опции</summary>
  [NonSerialized]
  public const string xmlOptionsNode = "RemarkAttributesOptions";
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
  /// <summary>Атрибут для хранения опций - "options"</summary>
  [NonSerialized]
  public const string xmlattrOptions = "options";
  /// <summary>Идентификатор типа атрибута</summary>
  private int _id;
  /// <summary>Источник атрибута</summary>
  private AttributeSourceTypes _attrSource = AttributeSourceTypes.Relation;
  /// <summary>Разделитель</summary>
  private string _separator = " ";
  /// <summary>Тип данных атрибута</summary>
  private FieldTypes _attrType;
  /// <summary>Аттрибут только для бесчертежных деталей</summary>
  private bool _withoutDrawing;
  private string _name;
  private Guid _guid = Guid.Empty;

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

  public bool IsVirtual => this._id <= -50000;

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

  /// <summary>Только для бесчертежных деталей</summary>
  public bool WithoutDrawing
  {
    [DebuggerStepThrough] get => this._withoutDrawing;
    [DebuggerStepThrough] set => this._withoutDrawing = value;
  }

  /// <summary>Тип данных атрибута</summary>
  public FieldTypes AttrType
  {
    [DebuggerStepThrough] get => this._attrType;
  }

  /// <summary>Имя атрибута. Может не подгружаться, тогда получение через ID</summary>
  public string Name
  {
    get => this._name != null ? this._name : MetaDataHelper.GetAttributeTypeName(this.ID);
  }

  /// <summary>Глобальный идентификатор атрибута. Может не подгружаться, тогда получение через ID</summary>
  public Guid Guid
  {
    get => this._guid != Guid.Empty ? this._guid : MetaDataHelper.GetAttributeTypeGuid(this.ID);
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
  public RemarkAttribute(int attrID, AttributeSourceTypes attrSource)
  {
    this.ID = attrID;
    this.AttrSource = attrSource;
  }

  /// <summary>Создать краткое описание типа атрибута</summary>
  /// <param name="attrInfo">Информация об атрибуте в формате AVS</param>
  public RemarkAttribute(AvsRowAttributeInfo attrInfo)
  {
    if (attrInfo == null)
      throw new ArgumentNullException(nameof (attrInfo));
    this.AssignData(attrInfo);
  }

  /// <summary>Назначить данные полям из источника</summary>
  /// <param name="srcAttrInfo">Информация об атрибуте в формате AVS</param>
  private void AssignData(AvsRowAttributeInfo srcAttrInfo)
  {
    this._id = srcAttrInfo != null ? srcAttrInfo.AttributeId : throw new ArgumentNullException(nameof (srcAttrInfo));
    switch (srcAttrInfo.AttrSrc)
    {
      case FieldSource.Relation:
        this._attrSource = AttributeSourceTypes.Relation;
        break;
      case FieldSource.Object:
        this._attrSource = AttributeSourceTypes.Object;
        break;
      default:
        this._attrSource = AttributeSourceTypes.Other;
        break;
    }
    this._guid = srcAttrInfo.AttributeGuid;
    this._name = srcAttrInfo.Name;
    this._attrType = srcAttrInfo.FieldType;
  }

  /// <summary>Создать краткое описание типа атрибута</summary>
  /// <param name="attrID">Идентификатор типа атрибута</param>
  /// <param name="attrSource">Источник атрибута</param>
  /// <param name="guid">Guid атрибута</param>
  /// <param name="name">Имя атрибута</param>
  /// <param name="attrType">Тип данных атрибута</param>
  public RemarkAttribute(
    int attrID,
    AttributeSourceTypes attrSource,
    Guid guid,
    string name,
    FieldTypes attrType)
    : this(attrID, attrSource)
  {
    this._guid = guid;
    this._name = name;
    this._attrType = attrType;
  }

  /// <summary>Создать краткое описание типа атрибута</summary>
  /// <param name="attrID">Идентификатор типа атрибута</param>
  /// <param name="attrSource">Источник атрибута</param>
  /// <param name="separator">Разделитель</param>
  public RemarkAttribute(int attrID, AttributeSourceTypes attrSource, string separator)
    : this(attrID, attrSource)
  {
    this.Separator = separator;
  }

  /// <summary>Создать краткое описание типа атрибута по прототипу</summary>
  /// <param name="template">Прототип краткого описания типа атрибута</param>
  public RemarkAttribute(RemarkAttribute template)
  {
    if (template == null)
      return;
    this.ID = template.ID;
    this.AttrSource = template.AttrSource;
    this.Separator = template.Separator;
    this.WithoutDrawing = template.WithoutDrawing;
    this._name = template._name;
    this._guid = template._guid;
    this._attrType = template._attrType;
  }

  /// <summary>Конструктор</summary>
  public RemarkAttribute()
  {
  }

  public AvsRowAttributeInfo CreateRowAttrInfo()
  {
    FieldSource attrSrc;
    switch (this.AttrSource)
    {
      case AttributeSourceTypes.Object:
        attrSrc = FieldSource.Object;
        break;
      case AttributeSourceTypes.Relation:
        attrSrc = FieldSource.Relation;
        break;
      default:
        attrSrc = FieldSource.DocumentRowField;
        break;
    }
    return new AvsRowAttributeInfo(attrSrc, this.Guid, this.ID, this.Name, ColumnContents.Text, new FieldTypes?(this.AttrType));
  }

  /// <summary>Выполнить сравнение с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is RemarkAttribute remarkAttribute))
      return base.Equals(obj);
    return this.ID == remarkAttribute.ID && this.AttrSource == remarkAttribute.AttrSource;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.ID.GetHashCode() << 2 | this.AttrSource.GetHashCode();

  /// <summary>Вернуть строковое представление экземпляра класса</summary>
  /// <returns>Строковое представление экземпляра класса</returns>
  public override string ToString()
  {
    return $"[{this.ID}.{this.AttrSource}] \"{this.Separator}\" - \"{this.Name}\"";
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => (object) new RemarkAttribute(this);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(object obj) => this.CompareTo(obj as RemarkAttribute);

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(RemarkAttribute other)
  {
    return other == null ? 1 : this.Name.CompareTo(other.Name);
  }

  /// <summary>Выполнить синхронизацию с кэшем метаданных</summary>
  public void SyncMetaData()
  {
    if (this.IsVirtual)
      return;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.ID);
    if (attributeType == null || !NoteFieldSettings.IsAcceptableAttrType(attributeType.RealFieldType))
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
    {
      AvsRowAttributeInfo virtualAttributInfo = AvsIDCache.GetVirtualAttributInfo(attributeAsGuid);
      if (virtualAttributInfo == null)
        return;
      this.AssignData(virtualAttributInfo);
    }
    else
      this.ID = MetaDataHelper.GetAttributeTypeID(attributeAsGuid);
    this.AttrSource = (AttributeSourceTypes) xmlStorage.GetAttributeAsInt32(node, "source", 2);
    xmlStorage.GetAttributeValue(node, "separator", " ");
    try
    {
      this.Separator = Encoding.UTF8.GetString(Convert.FromBase64String(xmlStorage.GetAttributeValue(node, "separator", Convert.ToBase64String(Encoding.UTF8.GetBytes(" ")))));
    }
    catch
    {
      this.Separator = " ";
    }
    this.WithoutDrawing = Convert.ToBoolean(xmlStorage.GetAttributeValue(node, "withoutdrawing", false.ToString()));
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
    string attrValue = this.Guid.ToString();
    XmlNode nodeWithAttr1 = xmlStorage.FindNodeWithAttr(parentNode, "attr", "guid", attrValue, true);
    parentNode.RemoveChild(nodeWithAttr1);
    XmlNode nodeWithAttr2 = xmlStorage.FindNodeWithAttr(parentNode, "attr", "guid", attrValue, true);
    xmlStorage.SetAttributeValue(nodeWithAttr2, "guid", attrValue);
    xmlStorage.SetAttributeValue(nodeWithAttr2, "source", ((int) this.AttrSource).ToString());
    byte[] bytes = Encoding.UTF8.GetBytes(this.Separator);
    xmlStorage.SetAttributeValue(nodeWithAttr2, "separator", Convert.ToBase64String(bytes));
    xmlStorage.SetAttributeValue(nodeWithAttr2, "withoutdrawing", this.WithoutDrawing.ToString());
  }
}
