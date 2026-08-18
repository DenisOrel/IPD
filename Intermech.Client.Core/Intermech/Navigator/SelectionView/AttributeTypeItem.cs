
// Type: Intermech.Navigator.SelectionView.AttributeTypeItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;


namespace Intermech.Navigator.SelectionView;

/// <summary>
/// Локальный класс для атрибутов (для реализации элементов в ComboBox)
/// </summary>
internal sealed class AttributeTypeItem
{
  /// <summary>Идентификатор атрибута</summary>
  public readonly int id;
  /// <summary>Поле для хранения наименовния атрибута</summary>
  private string _name;
  /// <summary>
  /// Признак, что тип данных, которому соответствует атрибут, был определен (чтоб не определять повторно)
  /// </summary>
  private bool _isFieldTypeReaded;
  /// <summary>
  /// Поле для хранения типа данных, которому соответствует атрибут
  /// </summary>
  private FieldTypes _fieldType;
  /// <summary>
  /// Признак того, что режим работы с множеством значений был определен (чтоб не определять повторно)
  /// </summary>
  private bool _isMultiValueModeReaded;
  /// <summary>
  /// Поле для хранения режима работы с множеством значений
  /// </summary>
  private MultiValueModes _multiValueMode;

  /// <summary>Наименовние атрибута</summary>
  private string name
  {
    get
    {
      if (this._name == null)
        this._name = this.id == 0 ? Convert.ToString(this.id) : (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(this.id).Name;
      return this._name;
    }
  }

  /// <summary>Тип данных, которому соответствует атрибут</summary>
  public FieldTypes fieldType
  {
    get
    {
      if (!this._isFieldTypeReaded)
      {
        this._fieldType = this.id == 0 ? FieldTypes.ftUnknown : (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(this.id).AttributeType;
        this._isFieldTypeReaded = true;
      }
      return this._fieldType;
    }
  }

  /// <summary>
  /// Режим работы с множеством значений (одно из списка, список и т.д.)
  /// </summary>
  public MultiValueModes multiValueMode
  {
    get
    {
      if (!this._isMultiValueModeReaded)
      {
        this._multiValueMode = this.id == 0 ? MultiValueModes.SingleValue : (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(this.id).MultipleValued;
        this._isMultiValueModeReaded = true;
      }
      return this._multiValueMode;
    }
  }

  /// <summary>Конструктор</summary>
  /// <param name="aAttributeTypeID">Идентификатор типа атрибута</param>
  public AttributeTypeItem(int aAttributeTypeID) => this.id = aAttributeTypeID;

  /// <summary>Конструктор</summary>
  /// <param name="aAttributeTypeID">Идентификатор типа атрибута</param>
  /// <param name="aAttributeTypeName">Наименование типа атрибута</param>
  public AttributeTypeItem(int aAttributeTypeID, string aAttributeTypeName)
    : this(aAttributeTypeID)
  {
    this._name = aAttributeTypeName;
  }

  /// <summary>Конструктор</summary>
  /// <param name="aAttributeTypeID">Идентификатор типа атрибута</param>
  /// <param name="aAttributeTypeName">Наименование типа атрибута</param>
  /// <param name="aFieldType">Тип данных, которому соответствует атрибут</param>
  public AttributeTypeItem(int aAttributeTypeID, string aAttributeTypeName, FieldTypes aFieldType)
    : this(aAttributeTypeID, aAttributeTypeName)
  {
    this._fieldType = aFieldType;
    this._isFieldTypeReaded = true;
  }

  /// <summary>Конструктор</summary>
  /// <param name="aAttributeTypeID">Идентификатор типа атрибута</param>
  /// <param name="aAttributeTypeName">Наименование типа атрибута</param>
  /// <param name="aFieldType">Тип данных, которому соответствует атрибут</param>
  /// <param name="aMultiValueMode">Режим работы с множеством значений (одно из списка, список и т.д.)</param>
  public AttributeTypeItem(
    int aAttributeTypeID,
    string aAttributeTypeName,
    FieldTypes aFieldType,
    MultiValueModes aMultiValueMode)
    : this(aAttributeTypeID, aAttributeTypeName, aFieldType)
  {
    this._multiValueMode = aMultiValueMode;
    this._isMultiValueModeReaded = true;
  }

  /// <summary>
  /// Перекрытый метод для строкового представления атрибута
  /// </summary>
  /// <returns></returns>
  public override string ToString() => this.name;
}
