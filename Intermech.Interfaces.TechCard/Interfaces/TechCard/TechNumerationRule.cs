// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechNumerationRule
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Класс правила нумерации</summary>
[Serializable]
public class TechNumerationRule : ITechNumerationRule, ICloneable
{
  /// <summary>Идентификатор версии объекта</summary>
  private long _objectID;
  /// <summary>Метод нумерации</summary>
  private TechNumerationMethods _numerationMethod;
  /// <summary>Тип нумерации</summary>
  private TechNumerationTypes _numerationType;
  /// <summary>Длина номера</summary>
  private int _numberLength;
  /// <summary>Список символов, для нумерации</summary>
  private string _charList;
  /// <summary>Первый номер</summary>
  private string _numberFirst;
  /// <summary>Шаг номеров</summary>
  private int _numberStep;
  /// <summary>Область нумерации</summary>
  private TechNumerationAreas _numerationArea;
  /// <summary>Разделитель номера</summary>
  private char _numberSeparator;
  /// <summary>Типы нумерации вариантов</summary>
  private TechNumerationTypes _numerationTypeVariant;
  /// <summary>
  /// Использование номера основного объекта, при нумерации вариантов/заменителей
  /// </summary>
  private TechNumerationBool _useBaseObjectNumber;
  /// <summary>Режим перенумерации при удалении</summary>
  private bool _renumOnDelete;

  /// <summary>Инициализация данных класса</summary>
  private void InitializeData()
  {
    this._objectID = 0L;
    this._numerationMethod = TechNumerationMethods.Auto;
    this._numerationType = TechNumerationTypes.Number;
    this._numberLength = 3;
    this._charList = "abcdfeg";
    this._numberFirst = "005";
    this._numberStep = 5;
    this._numerationArea = TechNumerationAreas.Parent;
    this._numberSeparator = '.';
    this._numerationTypeVariant = TechNumerationTypes.Number;
    this._useBaseObjectNumber = TechNumerationBool.Yes;
  }

  /// <summary>Конструктор</summary>
  public TechNumerationRule() => this.InitializeData();

  /// <summary>Конструктор</summary>
  /// <param name="numRule"></param>
  public TechNumerationRule(ITechNumerationRule numRule)
    : this()
  {
    this.CopyFrom(numRule);
  }

  /// <summary>Загрузка параметров</summary>
  /// <param name="obj"></param>
  /// <param name="session"></param>
  public virtual void Load(IDBObject obj, IUserSession session)
  {
    if (obj == null || session == null)
      return;
    this._objectID = obj.ObjectID;
    foreach (AttributeValues attributesValue in obj.GetAttributesValues(GetAttributeValuesModes.IncludeGuid))
      this.Load(attributesValue, session);
  }

  /// <summary>Загрузка параметров</summary>
  /// <param name="attrValues"></param>
  /// <param name="session"></param>
  public virtual void Load(AttributeValues attrValues, IUserSession session)
  {
    if (attrValues == null || attrValues.Values.Length == 0)
      return;
    if (attrValues.AttributeGuid == TechCardConsts.AttributeTypes.NumerationMethodAttrGuid)
      this._numerationMethod = (TechNumerationMethods) EnumTypeHelper.GetEnumValue(typeof (TechNumerationMethods), Convert.ToString(attrValues.Values[0]), (object) TechNumerationMethods.Auto);
    else if (attrValues.AttributeGuid == TechCardConsts.AttributeTypes.NumerationTypeAttrGuid)
      this._numerationType = (TechNumerationTypes) EnumTypeHelper.GetEnumValue(typeof (TechNumerationTypes), Convert.ToString(attrValues.Values[0]), (object) TechNumerationTypes.Number);
    else if (attrValues.AttributeGuid == TechCardConsts.AttributeTypes.NumerationAreaAttrGuid)
      this._numerationArea = (TechNumerationAreas) EnumTypeHelper.GetEnumValue(typeof (TechNumerationAreas), Convert.ToString(attrValues.Values[0]), (object) TechNumerationAreas.Parent);
    else if (attrValues.AttributeGuid == TechCardConsts.AttributeTypes.NumerationCharListAttrGuid)
      this._charList = Convert.ToString(attrValues.Values[0]);
    else if (attrValues.AttributeGuid == TechCardConsts.AttributeTypes.NumerationNumberLengthAttrGuid)
      this._numberLength = Convert.ToInt32(attrValues.Values[0]);
    else if (attrValues.AttributeGuid == TechCardConsts.AttributeTypes.NumerationFirtNumberAttrGuid)
      this._numberFirst = Convert.ToString(attrValues.Values[0]);
    else if (attrValues.AttributeGuid == TechCardConsts.AttributeTypes.NumerationStepAttrGuid)
      this._numberStep = Convert.ToInt32(attrValues.Values[0]);
    else if (attrValues.AttributeGuid == TechCardConsts.AttributeTypes.NumerationSeparatorAttrGuid)
      this._numberSeparator = Convert.ToChar(attrValues.Values[0]);
    else if (attrValues.AttributeGuid == TechCardConsts.AttributeTypes.NumerationTypeVariantAttrGuid)
    {
      this._numerationTypeVariant = (TechNumerationTypes) EnumTypeHelper.GetEnumValue(typeof (TechNumerationTypes), Convert.ToString(attrValues.Values[0]), (object) TechNumerationTypes.Number);
    }
    else
    {
      if (attrValues.AttributeGuid == TechCardConsts.AttributeTypes.NumerationUseBaseNumberAttrGuid)
        this._useBaseObjectNumber = (TechNumerationBool) EnumTypeHelper.GetEnumValue(typeof (TechNumerationBool), Convert.ToString(attrValues.Values[0]), (object) TechNumerationBool.No);
      if (!(attrValues.AttributeGuid == TechCardConsts.AttributeTypes.NumerationOnDeleteAttrGuid))
        return;
      this._renumOnDelete = Convert.ToInt64(attrValues.Values[0]) != 0L;
    }
  }

  /// <summary>Сохранение параметров</summary>
  /// <param name="obj"></param>
  /// <param name="session"></param>
  public virtual void Save(IDBObject obj, IUserSession session)
  {
    if (obj == null || session == null)
      return;
    AttributeValues[] valuesList = new AttributeValues[11]
    {
      new AttributeValues(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.NumerationTypeAttrGuid), (object) EnumTypeHelper.GetCaption((Enum) this.NumerationType)),
      new AttributeValues(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.NumerationAreaAttrGuid), (object) EnumTypeHelper.GetCaption((Enum) this.NumerationArea)),
      new AttributeValues(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.NumerationCharListAttrGuid), (object) this.CharList),
      new AttributeValues(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.NumerationNumberLengthAttrGuid), (object) this.NumberLength),
      new AttributeValues(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.NumerationFirtNumberAttrGuid), (object) this.NumberFirst),
      new AttributeValues(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.NumerationStepAttrGuid), (object) this.NumberStep),
      new AttributeValues(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.NumerationSeparatorAttrGuid), (object) this.NumberSeparator),
      new AttributeValues(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.NumerationTypeVariantAttrGuid), (object) EnumTypeHelper.GetCaption((Enum) this.NumerationTypeVariant)),
      new AttributeValues(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.NumerationUseBaseNumberAttrGuid), (object) EnumTypeHelper.GetCaption((Enum) this.UseBaseObjectNumber)),
      new AttributeValues(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.NumerationMethodAttrGuid), (object) EnumTypeHelper.GetCaption((Enum) this.NumerationMethod)),
      new AttributeValues(MetaDataHelper.GetAttributeID((object) TechCardConsts.AttributeTypes.NumerationOnDeleteAttrGuid), (object) (this.RenumOnDelete ? 1 : 0))
    };
    obj.SetAttributesValues(valuesList);
  }

  /// <summary>Копирование параметров правила</summary>
  /// <param name="numRule"></param>
  public void CopyFrom(ITechNumerationRule numRule)
  {
    if (numRule == null)
      return;
    this._objectID = numRule.ObjectID;
    this.NumerationMethod = numRule.NumerationMethod;
    this.NumerationType = numRule.NumerationType;
    this.NumberLength = numRule.NumberLength;
    this.CharList = numRule.CharList;
    this.NumberFirst = numRule.NumberFirst;
    this.NumberStep = numRule.NumberStep;
    this.NumerationArea = numRule.NumerationArea;
    this.NumberSeparator = numRule.NumberSeparator;
    this.NumerationTypeVariant = numRule.NumerationTypeVariant;
    this.UseBaseObjectNumber = numRule.UseBaseObjectNumber;
    this.RenumOnDelete = numRule.RenumOnDelete;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public object Clone() => this.MemberwiseClone();

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectID => this._objectID;

  /// <summary>Метод нумерации</summary>
  public TechNumerationMethods NumerationMethod
  {
    get => this._numerationMethod;
    set => this._numerationMethod = value;
  }

  /// <summary>Тип нумерации</summary>
  public TechNumerationTypes NumerationType
  {
    get => this._numerationType;
    set => this._numerationType = value;
  }

  /// <summary>Длина номера</summary>
  public int NumberLength
  {
    get => this._numberLength;
    set
    {
      if (this._numberLength == value)
        return;
      this._numberLength = value;
      this.NumberFirst = this.NumberFirst;
    }
  }

  /// <summary>Список символов, для нумерации</summary>
  public string CharList
  {
    get => this._charList;
    set => this._charList = value;
  }

  /// <summary>Первый номер</summary>
  public string NumberFirst
  {
    get => this._numberFirst;
    set
    {
      if (this._numberFirst == value)
        return;
      if (this.NumerationType == TechNumerationTypes.Literal)
      {
        this._numberFirst = value;
      }
      else
      {
        string s = value;
        if (s.Length != this.NumberLength)
        {
          int result;
          int.TryParse(s, out result);
          s = string.Format($"{{0:D{(object) this.NumberLength}}}", (object) result);
        }
        this._numberFirst = s;
      }
    }
  }

  /// <summary>Шаг номеров</summary>
  public int NumberStep
  {
    get => this._numberStep;
    set => this._numberStep = value;
  }

  /// <summary>Область нумерации</summary>
  public TechNumerationAreas NumerationArea
  {
    get => this._numerationArea;
    set => this._numerationArea = value;
  }

  /// <summary>Разделитель номера</summary>
  public char NumberSeparator
  {
    get => this._numberSeparator;
    set => this._numberSeparator = value;
  }

  /// <summary>Типы нумерации вариантов</summary>
  public TechNumerationTypes NumerationTypeVariant
  {
    get => this._numerationTypeVariant;
    set => this._numerationTypeVariant = value;
  }

  /// <summary>
  /// Использование номера основного объекта, при нумерации вариантов/заменителей
  /// </summary>
  public TechNumerationBool UseBaseObjectNumber
  {
    get => this._useBaseObjectNumber;
    set => this._useBaseObjectNumber = value;
  }

  /// <summary>Вызов перенумерации при удалении объекта / связи</summary>
  public bool RenumOnDelete
  {
    get => this._renumOnDelete;
    set => this._renumOnDelete = value;
  }
}
