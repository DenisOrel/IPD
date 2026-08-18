// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.ObjectIncompatibilityCriterion
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Localization;
using System;
using System.Diagnostics;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Критерий условия несовместимости опций в конфигураторе составов IPS
/// </summary>
[DebuggerDisplay("{DebuggerToString()}")]
[Serializable]
public sealed class ObjectIncompatibilityCriterion : PdmCriterion
{
  /// <summary>Guid опции, с которой может быть конфликт значений</summary>
  private Guid _optionConflict = Guid.Empty;
  /// <summary>ID значения опции, с которой может быть конфликт</summary>
  private string _valueConflict = string.Empty;

  /// <summary>Guid опции, с которой может быть конфликт значений</summary>
  public Guid OptionConflict
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this._optionConflict;
    }
    set
    {
      lock (this.syncRoot)
        this._optionConflict = value;
    }
  }

  /// <summary>ID значения опции, с которой может быть конфликт</summary>
  public string ValueConflict
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this._valueConflict;
    }
    set
    {
      lock (this.syncRoot)
        this._valueConflict = value;
    }
  }

  /// <summary>
  /// Значение по умолчанию, если вычисление не может быть выполнено или не требуется
  /// </summary>
  public override PdmConfiguratorResult DefaultEvaluatorValue
  {
    [DebuggerStepThrough] get => PdmConfiguratorResult.False;
  }

  /// <summary>Тип коллекции вложенных элементов</summary>
  public override Type CollectionType
  {
    [DebuggerStepThrough] get => typeof (ObjectIncompatibilitiesCollection);
  }

  /// <summary>Является ли элемент пустым или нет</summary>
  public override bool Empty
  {
    get
    {
      lock (this.syncRoot)
      {
        if (this._option != Guid.Empty && this._optionConflict != Guid.Empty && !string.IsNullOrEmpty(this._value) && !string.IsNullOrEmpty(this._valueConflict) && this.CriterionType != PdmCriterionType.Stub)
          return false;
        return this._criterionType == PdmCriterionType.Stub ? this._items == null || this._items.Empty : this._items == null || this._items.Empty;
      }
    }
  }

  /// <summary>Создать пустой критерий</summary>
  public ObjectIncompatibilityCriterion()
  {
  }

  /// <summary>
  /// Создать заполненный критерий-заглушку (содержит только дочерние критерии)
  /// </summary>
  /// <param name="function">Логическая функция для объединения данного критерия со следующим критерием</param>
  /// <param name="items">Коллекция вложенных критериев конфигуратора составов IPS</param>
  public ObjectIncompatibilityCriterion(LogicalFunction function, PdmCriterionsCollection items)
    : base(function, items)
  {
  }

  /// <summary>Создать заполненный критерий</summary>
  /// <param name="optionMain">Guid главной опции (для которой задан критерий). Значение Guid.Empty позволяет создать критерий-заглушку, который
  /// служит для объединения нескольких дочерних критериев, но не учитывает своё значение</param>
  /// <param name="valueMain">ID значения главной опции</param>
  /// <param name="optionConflict">Guid опции, с которой может быть конфликт значений</param>
  /// <param name="valueConflict">ID значения опции, с которой может быть конфликт</param>
  /// <param name="operat">Оператор для сравнения значения конфиликтной опции со значением, которое пришло по составу от родительского объекта</param>
  /// <param name="function">Логическая функция для объединения данного критерия со следующим критерием</param>
  /// <param name="items">Коллекция вложенных критериев конфигуратора составов IPS</param>
  public ObjectIncompatibilityCriterion(
    Guid optionMain,
    string valueMain,
    Guid optionConflict,
    string valueConflict,
    Operator operat,
    LogicalFunction function,
    PdmCriterionsCollection items)
    : base(optionMain, valueMain, operat, function, items)
  {
    this.OptionConflict = optionConflict;
    this.ValueConflict = valueConflict;
  }

  /// <summary>Создать заполненный критерий</summary>
  /// <param name="context">Контекст, из которого будет получена вся недостающая информация</param>
  /// <param name="optionMain">Идентификатор версии объекта главной опции (для которой задан критерий), Guid будет получен из контекста.
  /// Значение Intermech.Consts.UnknownObjectID позволяет создать критерий-заглушку, который
  /// служит для объединения нескольких дочерних критериев, но не учитывает своё значение</param>
  /// <param name="valueMain">Порядковый номер значения опции (Guid будет получен из контекста)</param>
  /// <param name="optionConflict">ID опции, с которой может быть конфликт значений</param>
  /// <param name="valueConflict">ID значения опции, с которой может быть конфликт</param>
  /// <param name="operat">Оператор для сравнения значений опции конфигуратора составов IPS</param>
  /// <param name="function">Оператор для сравнения значения конфиликтной опции со значением, которое пришло по составу от родительского объекта</param>
  /// <param name="items">Коллекция вложенных критериев конфигуратора составов IPS</param>
  public ObjectIncompatibilityCriterion(
    PdmConfiguratorContext context,
    long optionMain,
    int valueMain,
    long optionConflict,
    int valueConflict,
    Operator operat,
    LogicalFunction function,
    PdmCriterionsCollection items)
    : base(context, optionMain, valueMain, operat, function, items)
  {
    if (context == null)
      return;
    this.OptionConflict = PdmConfiguratorCache.CacheFindOptionGuid(optionConflict);
    this.ValueConflict = PdmConfiguratorCache.CacheFindOptionValueGuid(optionConflict, valueConflict);
  }

  /// <summary>Создать критерий на основе указанного объекта</summary>
  /// <param name="source">Объект-источник</param>
  public ObjectIncompatibilityCriterion(object source)
    : base(source)
  {
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    lock (this.syncRoot)
    {
      this._optionConflict = Guid.Empty;
      this._valueConflict = string.Empty;
    }
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    base.Assign(source);
    if (source == null || !(source is ObjectIncompatibilityCriterion incompatibilityCriterion))
      return;
    this.OptionConflict = incompatibilityCriterion.OptionConflict;
    this.ValueConflict = incompatibilityCriterion.ValueConflict;
  }

  /// <summary>
  /// Выполнить вычисление критерия согласно указанному контексту конфигуратора составов IPS
  /// </summary>
  /// <param name="context">Контекст конфигуратора составов IPS</param>
  /// <returns>true - оператор критерия вернул true,
  /// исключение, если значение какой-то опции/критерия не найдено в контексте, либо принадлежат разным опциям</returns>
  public override PdmConfiguratorResult Evalute(PdmConfiguratorContext context)
  {
    this.EvaluateTrace.Clear();
    if (context == null)
    {
      this.EvaluateTrace.Flags = PdmConfiguratorResult.ContextNotFound;
      this.EvaluateTrace.Message = LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_12");
      return PdmConfiguratorResult.ContextNotFound;
    }
    if (this.CriterionType == PdmCriterionType.Stub)
    {
      this.EvaluateTrace.Flags = this.Items.Evalute(context);
      this.EvaluateTrace.Message = this.Items.EvaluateTrace.Message;
      return this.EvaluateTrace.Flags;
    }
    OptionHolder option1 = PdmConfiguratorCache.CacheFindOption(this.Option);
    if (option1 == null)
    {
      this.EvaluateTrace.Flags = PdmConfiguratorResult.OptionNotFound;
      this.EvaluateTrace.Message = string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_13"), (object) this.Option);
      return PdmConfiguratorResult.OptionNotFound;
    }
    if (this.OptionConflict == Guid.Empty)
    {
      this.EvaluateTrace.Flags = PdmConfiguratorResult.ConflictOptionNotFound;
      this.EvaluateTrace.Message = LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_14");
      return PdmConfiguratorResult.ConflictOptionNotFound;
    }
    OptionHolder option2 = PdmConfiguratorCache.CacheFindOption(this.OptionConflict);
    if (option2 == null)
    {
      this.EvaluateTrace.Flags = PdmConfiguratorResult.ConflictOptionNotFound;
      this.EvaluateTrace.Message = string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_15"), (object) this.OptionConflict);
      return PdmConfiguratorResult.ConflictOptionNotFound;
    }
    string valueID = context[this.Option];
    if (valueID == string.Empty)
    {
      this.EvaluateTrace.Flags = PdmConfiguratorResult.OptionValueNotFound;
      this.EvaluateTrace.Message = string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_16"), (object) option1.OptionCaption);
      return PdmConfiguratorResult.OptionValueNotFound;
    }
    string str = context[this.OptionConflict];
    if (this.Value != valueID)
    {
      this.EvaluateTrace.Flags = PdmConfiguratorResult.False;
      this.EvaluateTrace.Message = LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_17");
      return PdmConfiguratorResult.False;
    }
    this.EvaluateTrace.Flags = Helper.Combine(context.CacheEqualsValues(this.OptionConflict, this.ValueConflict, this.OptionConflict, str, this.Operator), this.Items.Evalute(context), this.Items.DefaultFunction);
    if (this.Not)
    {
      if (this.EvaluateTrace.Flags == PdmConfiguratorResult.False)
        this.EvaluateTrace.Flags = PdmConfiguratorResult.True;
      else if (this.EvaluateTrace.Flags == PdmConfiguratorResult.True)
        this.EvaluateTrace.Flags = PdmConfiguratorResult.False;
    }
    this.EvaluateTrace.Message = this.Items.EvaluateTrace.Message;
    if (this.Items.Empty && (this.EvaluateTrace.Flags == PdmConfiguratorResult.True || this.EvaluateTrace.Flags == PdmConfiguratorResult.Incompatibles))
    {
      this.EvaluateTrace.Flags = PdmConfiguratorResult.Incompatibles;
      this.EvaluateTrace.Message = string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_18"), (object) option1.OptionCaption, (object) option1.GetAsString(valueID), (object) option2.OptionCaption, !string.IsNullOrEmpty(str) ? (object) option2.GetAsString(str) : (object) string.Empty);
    }
    return this.EvaluateTrace.Flags;
  }

  /// <summary>Загрузить данные из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public override void Load(XMLSettingsStorage xmlStorage, XmlNode node)
  {
    this.Clear();
    if (node == null || node.Name != "e" || !(xmlStorage.Services.GetService(typeof (PdmGuidMapper)) is PdmGuidMapper service))
      return;
    string str1 = xmlStorage.GetAttributeValue(node, "a", "");
    string attributeValue = xmlStorage.GetAttributeValue(node, "d", "");
    if (!string.IsNullOrEmpty(str1) && str1.Length >= 4 && str1.IndexOf("-") > 0)
    {
      if (str1.StartsWith("!"))
      {
        str1 = str1.Substring(1);
        this.Not = true;
      }
      long int64_1 = StringsHelper.HexToInt64(str1.Substring(0, str1.IndexOf("-")));
      this.Option = service[int64_1];
      string str2 = str1.Substring(str1.IndexOf("-") + 1);
      this.Operator = OperatorHelper.FromString(str2.Substring(0, 1));
      this.Function = LogicalFunctionHelper.FromString(str2.Substring(1, 1));
      if (str2.Length > 2)
        str2 = str2.Substring(2);
      if (!string.IsNullOrEmpty(str2) && (int) str2[0] == (int) Helper.AsteriskChar)
      {
        str2 = str2.Length > Helper.AsteriskString.Length ? str2.Substring(Helper.AsteriskString.Length) : string.Empty;
        this.CriterionType = PdmCriterionType.Stub;
      }
      this.Value = str2;
      if (!string.IsNullOrEmpty(attributeValue) && attributeValue.Length >= 2 && attributeValue.IndexOf("-") > 0)
      {
        long int64_2 = StringsHelper.HexToInt64(attributeValue.Substring(0, attributeValue.IndexOf("-")));
        this.OptionConflict = service[int64_2];
        this.ValueConflict = attributeValue.Substring(attributeValue.IndexOf("-") + 1);
      }
    }
    for (int i = 0; i < node.ChildNodes.Count; ++i)
    {
      XmlNode childNode = node.ChildNodes[i];
      if (childNode.Name == "d")
      {
        this.Items.Load(xmlStorage, childNode);
        break;
      }
    }
    lock (this.syncRoot)
      this.XMLAfterLoad(xmlStorage, node);
  }

  /// <summary>
  /// Сохранить данные в состав указанного родительского узла
  /// </summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
  public override void Save(XMLSettingsStorage xmlStorage, XmlNode parentNode)
  {
    lock (this.syncRoot)
      this.BeforeSave(xmlStorage.Services.GetService(typeof (object)));
    if (!this.Empty)
    {
      if (!(xmlStorage.Services.GetService(typeof (PdmGuidMapper)) is PdmGuidMapper serviceInstance))
      {
        serviceInstance = new PdmGuidMapper();
        xmlStorage.Services.AddService(typeof (PdmGuidMapper), (object) serviceInstance);
      }
      long num1 = serviceInstance[this.Option];
      long num2 = serviceInstance[this.OptionConflict];
      XmlNode xmlNode = xmlStorage.AddNode(parentNode, "e");
      string str = this.CriterionType == PdmCriterionType.Stub ? Helper.AsteriskString : string.Empty;
      xmlStorage.SetAttributeValue(xmlNode, "a", $"{(this.Not ? "!" : string.Empty)}{StringsHelper.IntToHex(num1)}-{OperatorHelper.ToString(this.Operator)}{LogicalFunctionHelper.ToString(this.Function)}{str}{this.Value}");
      if (num2 != 0L)
        xmlStorage.SetAttributeValue(xmlNode, "d", $"{StringsHelper.IntToHex(num2)}-{this.ValueConflict}");
      this.Items.Save(xmlStorage, xmlNode);
    }
    lock (this.syncRoot)
      this.XMLAfterSave(xmlStorage, parentNode);
  }

  /// <summary>Преобразовать критерий в строку</summary>
  /// <param name="isLastItem"> является ли критерий последним в родительской коллекции</param>
  /// <param name="isSingleItem"> является ли критерий единственным в родительской коллекции</param>
  /// <returns></returns>
  public override string GenerateStringComments(bool isLastItem, bool isSingleItem)
  {
    string stringComments = string.Empty;
    lock (this.syncRoot)
    {
      string str1 = isLastItem ? string.Empty : (this.Function == LogicalFunction.And ? LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_19") : LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_20"));
      OptionHolder option = PdmConfiguratorCache.CacheFindOption(this._optionConflict);
      string str2 = option == null ? string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_21"), (object) this._optionConflict) : option.OptionCaption;
      string lower = EnumDescConverter.GetEnumDescription((Enum) this.Operator).ToLower();
      string str3 = option == null ? string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_22"), (object) this._valueConflict) : option.GetAsString(this._valueConflict);
      if (this.CriterionType != PdmCriterionType.Stub)
      {
        stringComments = $"{str2} {lower} {str3}";
        stringComments = isSingleItem ? stringComments : $"({stringComments})";
        stringComments += str1;
      }
    }
    return stringComments;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты идентичны</returns>
  public override bool Equals(object obj)
  {
    return obj is ObjectIncompatibilityCriterion incompatibilityCriterion && this.Option.Equals(incompatibilityCriterion.Option) && this.Value.Equals(incompatibilityCriterion.Value) && this.Not.Equals(incompatibilityCriterion.Not) && this.OptionConflict.Equals(incompatibilityCriterion.OptionConflict) && this.ValueConflict.Equals(incompatibilityCriterion.ValueConflict) && this.Operator == incompatibilityCriterion.Operator && this.Items.Equals((object) incompatibilityCriterion.Items);
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode()
  {
    Guid guid = this.Option;
    int num1 = guid.GetHashCode() << 24 ^ this.Value.GetHashCode() << 18;
    guid = this.OptionConflict;
    int num2 = guid.GetHashCode() << 12;
    return num1 ^ num2 ^ this.ValueConflict.GetHashCode() << 6 ^ this.Operator.GetHashCode();
  }

  /// <summary>Представление экземпляра класса в виде строки</summary>
  /// <returns>Представление экземпляра класса в виде строки</returns>
  protected override string DebuggerToString()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    lock (this.syncRoot)
    {
      OptionHolder option1 = PdmConfiguratorCache.CacheFindOption(this.Option);
      string str1 = option1 == null || string.IsNullOrEmpty(this._value) ? $"[{this._value}]" : $"[{this._value}]\"{option1.GetAsString(this._value)}\"";
      OptionHolder option2 = PdmConfiguratorCache.CacheFindOption(this.OptionConflict);
      string str2 = option2 == null || string.IsNullOrEmpty(this._valueConflict) ? $"[{this._valueConflict}]" : $"[{this._valueConflict}]\"{option2.GetAsString(this._valueConflict)}\"";
      if (this.CriterionType == PdmCriterionType.Stub)
        stringBuilder1.Append("([Заглушка] ");
      else
        stringBuilder1.Append("(");
      StringBuilder stringBuilder2 = stringBuilder1;
      Guid guid;
      string str3;
      if (option1 == null)
      {
        guid = this.Option;
        str3 = $"\"{guid.ToString()}\" значение {str1}) => (";
      }
      else
        str3 = $"[{option1.OptionObjectID}]\"{option1.OptionCaption}\" значение {str1}) => (";
      stringBuilder2.Append(str3);
      StringBuilder stringBuilder3 = stringBuilder1;
      string str4;
      if (option2 == null)
      {
        guid = this.OptionConflict;
        str4 = string.Format("\"{0}\" [{2}] значение {1}) ", (object) guid.ToString(), (object) str2, (object) this.Operator);
      }
      else
        str4 = string.Format("[{0}]\"{1}\" [{3}] значение {2}) ", (object) option2.OptionObjectID, (object) option2.OptionCaption, (object) str2, (object) this.Operator);
      stringBuilder3.Append(str4);
      stringBuilder1.Append((object) this.Function);
      stringBuilder1.Append(" ");
    }
    return stringBuilder1.ToString();
  }

  /// <summary>
  /// Метод вызывается перед сохранением критерия в XML-документ. При возникновении ошибки следует сгенерировать исключение
  /// </summary>
  /// <param name="holder">Контейнер, которому принадлежит данный критерий</param>
  public override void BeforeSave(object holder)
  {
    base.BeforeSave(holder);
    ObjectOptionsHolder objectOptionsHolder = holder as ObjectOptionsHolder;
    if (this.CriterionType != PdmCriterionType.Stub)
    {
      OptionHolder option1 = PdmConfiguratorCache.CacheFindOption(this.Option);
      OptionHolder option2 = PdmConfiguratorCache.CacheFindOption(this.OptionConflict);
      string str1 = option1 == null ? this._option.ToString() : option1.OptionCaption;
      string str2 = option2 == null ? this._optionConflict.ToString() : option2.OptionCaption;
      if (this.OptionConflict == Guid.Empty)
        throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_28"), (object) str1));
      if (string.IsNullOrEmpty(this.ValueConflict))
        throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_29"), (object) str1));
      if (objectOptionsHolder != null)
      {
        OptionValue optionValue1 = option1.OptionValues.FindValue(this.Value);
        string str3 = optionValue1 != null ? optionValue1.Value : string.Empty;
        if (option2 != null && objectOptionsHolder.Options.IndexOf(option2.OptionObjectID) < 0)
          throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_30"), (object) str3, (object) str1, (object) str2));
        OptionValue optionValue2 = option2.OptionValues.FindValue(this.ValueConflict);
        string str4 = optionValue2 != null ? optionValue2.Value : this.ValueConflict;
        if (!objectOptionsHolder.VisibleOptionValues.GetVisibleOptionValue(this.OptionConflict, this.ValueConflict))
          throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_31"), (object) str3, (object) str1, (object) str2, (object) str4));
      }
    }
    this.Items.BeforeSave(holder);
  }
}
