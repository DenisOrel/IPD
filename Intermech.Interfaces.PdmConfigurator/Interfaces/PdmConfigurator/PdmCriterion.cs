// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmCriterion
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Localization;
using Intermech.Search.Pdm.CompositionsConfigurator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Критерий конфигуратора составов IPS</summary>
[DebuggerDisplay("{DebuggerToString()}")]
[Serializable]
public class PdmCriterion : IPdmCriterion, IAssignable, ICloneable, IEvaluator, IXMLStorageLoadSave
{
  /// <summary>Объект для синхронизации</summary>
  protected object syncRoot = new object();
  /// <summary>Guid опции</summary>
  protected Guid _option = Guid.Empty;
  /// <summary>ID значения опции</summary>
  protected string _value = string.Empty;
  /// <summary>
  /// Оператор для сравнения значений опции конфигуратора составов IPS
  /// </summary>
  protected Operator _operator = Operator.Equals;
  /// <summary>
  /// Логическая функция для объединения данного критерия со следующим критерием
  /// </summary>
  protected LogicalFunction _function;
  /// <summary>Вид критерия</summary>
  protected PdmCriterionType _criterionType = PdmCriterionType.Criterion;
  /// <summary>
  /// Коллекция вложенных критериев конфигуратора составов IPS
  /// </summary>
  protected PdmCriterionsCollection _items;
  /// <summary>Результат последнего вычисления</summary>
  protected TraceEntry _evaluateTrace = new TraceEntry();

  /// <summary>Тип коллекции вложенных элементов</summary>
  public virtual Type CollectionType => typeof (ObjectsApplicabilitiesCriterionsCollection);

  /// <summary>Guid опции</summary>
  public virtual Guid Option
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this._option;
    }
    set
    {
      lock (this.syncRoot)
        this._option = value;
    }
  }

  /// <summary>ID значения опции</summary>
  public virtual string Value
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this._value;
    }
    set
    {
      lock (this.syncRoot)
        this._value = value;
    }
  }

  /// <summary>
  /// Оператор для сравнения значений опции конфигуратора составов IPS
  /// </summary>
  public virtual Operator Operator
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this._operator;
    }
    set
    {
      lock (this.syncRoot)
        this._operator = value;
    }
  }

  /// <summary>
  /// Коллекция вложенных критериев конфигуратора составов IPS
  /// </summary>
  public PdmCriterionsCollection Items
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this._items;
    }
    set
    {
      lock (this.syncRoot)
        this._items = value ?? this._items;
    }
  }

  /// <summary>Создать пустой критерий</summary>
  public PdmCriterion()
  {
    this._items = Activator.CreateInstance(this.CollectionType) as PdmCriterionsCollection;
  }

  /// <summary>
  /// Создать заполненный критерий-заглушку (содержит только дочерние критерии)
  /// </summary>
  /// <param name="function">Логическая функция для объединения данного критерия со следующим критерием</param>
  /// <param name="items">Коллекция вложенных критериев конфигуратора составов IPS</param>
  public PdmCriterion(LogicalFunction function, PdmCriterionsCollection items)
    : this()
  {
    this.Function = function;
    this.Items.Assign((object) items);
  }

  /// <summary>Создать заполненный критерий</summary>
  /// <param name="option">Guid опции. Значение Guid.Empty позволяет создать критерий-заглушку, который
  /// служит для объединения нескольких дочерних критериев, но не учитывает своё значение</param>
  /// <param name="value">ID значения опции</param>
  /// <param name="operat">Оператор для сравнения значений опции конфигуратора составов IPS</param>
  /// <param name="function">Логическая функция для объединения данного критерия со следующим критерием</param>
  /// <param name="items">Коллекция вложенных критериев конфигуратора составов IPS</param>
  public PdmCriterion(
    Guid option,
    string value,
    Operator operat,
    LogicalFunction function,
    PdmCriterionsCollection items)
    : this()
  {
    this.Option = option;
    this.Value = value;
    this.Operator = operat;
    this.Function = function;
    this.Items.Assign((object) items);
  }

  /// <summary>Создать заполненный критерий</summary>
  /// <param name="context">Контекст, из которого будет получена вся недостающая информация</param>
  /// <param name="option">Идентификатор версии объекта опции (Guid будет получен из контекста).
  /// Значение Intermech.Consts.UnknownObjectID позволяет создать критерий-заглушку, который
  /// служит для объединения нескольких дочерних критериев, но не учитывает своё значение</param>
  /// <param name="value">Порядковый номер значения опции (Guid будет получен из контекста)</param>
  /// <param name="operat">Оператор для сравнения значений опции конфигуратора составов IPS</param>
  /// <param name="function">Логическая функция для объединения данного критерия со следующим критерием</param>
  /// <param name="items">Коллекция вложенных критериев конфигуратора составов IPS</param>
  public PdmCriterion(
    PdmConfiguratorContext context,
    long option,
    int value,
    Operator operat,
    LogicalFunction function,
    PdmCriterionsCollection items)
    : this()
  {
    if (context != null)
    {
      this.Option = PdmConfiguratorCache.CacheFindOptionGuid(option);
      this.Value = PdmConfiguratorCache.CacheFindOptionValueGuid(option, value);
    }
    this.Operator = operat;
    this.Function = function;
    this.Items.Assign((object) items);
  }

  /// <summary>Создать критерий на основе указанного объекта</summary>
  /// <param name="source">Объект-источник</param>
  public PdmCriterion(object source)
    : this()
  {
    this.Assign(source);
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public virtual object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

  /// <summary>Очистить поля класса</summary>
  public virtual void Clear()
  {
    lock (this.syncRoot)
    {
      this._option = Guid.Empty;
      this._value = string.Empty;
      this._operator = Operator.Equals;
      this._function = this.Items.DefaultFunction;
      this._criterionType = PdmCriterionType.Criterion;
      this._items.Clear();
    }
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public virtual void Assign(object source)
  {
    this.Clear();
    if (source == null || !(source is PdmCriterion pdmCriterion))
      return;
    lock (this.syncRoot)
    {
      this.Option = pdmCriterion.Option;
      this.Value = pdmCriterion.Value;
      this.Operator = pdmCriterion.Operator;
      this.Function = pdmCriterion.Function;
      this.Not = pdmCriterion.Not;
      this.CriterionType = pdmCriterion.CriterionType;
      this.Items.Assign((object) pdmCriterion.Items);
    }
  }

  /// <summary>
  /// Значение по умолчанию, если вычисление не может быть выполнено или не требуется
  /// </summary>
  public virtual PdmConfiguratorResult DefaultEvaluatorValue
  {
    [DebuggerStepThrough] get => PdmConfiguratorResult.True;
  }

  /// <summary>
  /// Логическая функция для объединения данного критерия со следующим критерием
  /// </summary>
  public virtual LogicalFunction Function
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this._function;
    }
    set
    {
      lock (this.syncRoot)
        this._function = value;
    }
  }

  public bool Not { get; set; }

  /// <summary>
  /// Выполнить сравнение значения опции в критерии со значением данной опции из контекста конфигуратора составов
  /// </summary>
  /// <param name="context">Контекст конфигуратора составов IPS</param>
  /// <returns>true - оператор критерия вернул true при сравнении значений опции и критерия,
  /// исключение, если значение опции/критерия не найдено в контексте, либо принадлежат разным опциям</returns>
  public virtual PdmConfiguratorResult Evalute(PdmConfiguratorContext context)
  {
    this.EvaluateTrace.Clear();
    if (context == null)
    {
      this.EvaluateTrace.Flags = PdmConfiguratorResult.ContextNotFound;
      this.EvaluateTrace.Message = LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_12");
      return PdmConfiguratorResult.ContextNotFound;
    }
    lock (this.syncRoot)
    {
      if (!(this.Option == Guid.Empty))
      {
        if (this.CriterionType != PdmCriterionType.Stub)
          goto label_9;
      }
      this.EvaluateTrace.Flags = this.Items.Evalute(context);
      this.EvaluateTrace.Message = this.Items.EvaluateTrace.Message;
      return this.EvaluateTrace.Flags;
    }
label_9:
    OptionHolder option = PdmConfiguratorCache.CacheFindOption(this.Option);
    if (option == null)
    {
      this.EvaluateTrace.Flags = PdmConfiguratorResult.OptionNotFound;
      this.EvaluateTrace.Message = string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_34"), (object) this.Option);
      return PdmConfiguratorResult.OptionNotFound;
    }
    string value1ID = context[this.Option];
    if (value1ID == string.Empty)
    {
      this.EvaluateTrace.Flags = PdmConfiguratorResult.ApplOptionValueNotFound;
      string str = option == null ? $"c GUID={this.Option}" : $"\"{option.OptionCaption}\"";
      this.EvaluateTrace.Message = string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_35"), (object) str);
      return PdmConfiguratorResult.ApplOptionValueNotFound;
    }
    this.EvaluateTrace.Flags = Helper.Combine(context.CacheEqualsValues(this.Option, value1ID, this.Option, this.Value, this.Operator), this.Items.Evalute(context), this.Items.DefaultFunction);
    if (this.Not)
    {
      if (this.EvaluateTrace.Flags == PdmConfiguratorResult.False)
        this.EvaluateTrace.Flags = PdmConfiguratorResult.True;
      else if (this.EvaluateTrace.Flags == PdmConfiguratorResult.True)
        this.EvaluateTrace.Flags = PdmConfiguratorResult.False;
    }
    this.EvaluateTrace.Message = this.Items.EvaluateTrace.Message;
    return this.EvaluateTrace.Flags;
  }

  /// <summary>Результат последнего вычисления</summary>
  public virtual TraceEntry EvaluateTrace
  {
    [DebuggerStepThrough] get => this._evaluateTrace;
  }

  /// <summary>Преобразовать коллекцию в строку</summary>
  /// <param name="isLastItem"> является ли критерий последним в родительской коллекции</param>
  /// <param name="isSingleItem"> является ли критерий единственным в родительской коллекции</param>
  /// <returns></returns>
  public virtual string GenerateStringComments(bool isLastItem, bool isSingleItem)
  {
    string stringComments = string.Empty;
    string str1 = isLastItem ? string.Empty : (this.Function == LogicalFunction.And ? LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_19") : LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_20"));
    OptionHolder option = PdmConfiguratorCache.CacheFindOption(this._option);
    ApplicationConditionsDisplaySettings settings = CompositionsConfiguratorConfigurationOptions.ApplicationConditionsDisplaySettings ?? new ApplicationConditionsDisplaySettings();
    string str2 = option == null ? string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_21"), (object) this._option) : CompositionsConfiguratorHelper.GetOptionNameReplacemenetForDisplayApplicationConditions(option, settings);
    string applicationConditions = CompositionsConfiguratorHelper.GetOperatorForDisplayApplicationConditions(this.Operator, settings);
    string str3;
    if (option != null)
    {
      OptionValue optionValue = option.OptionValues != null ? option.OptionValues.FindValue(this._value) : (OptionValue) null;
      str3 = optionValue == null ? string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_22"), (object) this._value) : CompositionsConfiguratorHelper.GetOptionValueReplacementForDisplayApplicationConditions(option, optionValue, settings);
    }
    else
      str3 = string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_22"), (object) this._value);
    if (this.CriterionType != PdmCriterionType.Stub)
    {
      string str4 = $"{str2} {applicationConditions} {str3}";
      if (this.Not)
        str4 = $"НЕ({str4})";
      else if (!isSingleItem)
        str4 = $"({str4})";
      stringComments = str4 + str1;
    }
    return stringComments;
  }

  /// <summary>Является ли элемент пустым или нет</summary>
  public virtual bool Empty
  {
    get
    {
      lock (this.syncRoot)
      {
        if (!(this._option != Guid.Empty) || string.IsNullOrEmpty(this._value))
          return this._option == Guid.Empty && (this._items == null || this._items.Empty);
        return this.CriterionType == PdmCriterionType.Stub && (this._items == null || this._items.Empty);
      }
    }
  }

  /// <summary>Вид критерия конфигуратора составов IPS</summary>
  public virtual PdmCriterionType CriterionType
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this._criterionType;
    }
    set
    {
      lock (this.syncRoot)
        this._criterionType = value;
    }
  }

  /// <summary>Сгенерировать пустой критерий</summary>
  /// <returns>Пустой критерий</returns>
  public virtual IPdmCriterion GenerateEmptyCriterion()
  {
    return Activator.CreateInstance(this.GetType()) as IPdmCriterion;
  }

  /// <summary>
  /// Сгенерировать пустой критерий и добавить его в коллекцию дочерних элементов
  /// </summary>
  /// <returns>Добавленный пустой критерий</returns>
  public virtual IPdmCriterion AddEmptyCriterion()
  {
    IPdmCriterion emptyCriterion = this.GenerateEmptyCriterion();
    lock (this.syncRoot)
      this._items.Add(emptyCriterion);
    return emptyCriterion;
  }

  /// <summary>
  /// Сгенерировать критерий-"заглушку" и добавить его в коллекцию дочерних элементов
  /// </summary>
  /// <returns>Добавленный критерий-"заглушка"</returns>
  public virtual IPdmCriterion AddStubCriterion()
  {
    IPdmCriterion pdmCriterion = this.AddEmptyCriterion();
    lock (this.syncRoot)
      pdmCriterion.CriterionType = PdmCriterionType.Stub;
    return pdmCriterion;
  }

  /// <summary>Удалить критерий из коллекции дочерних элементов</summary>
  /// <returns>true - удаление было выполнено успешно</returns>
  public virtual bool RemoveCriterion(IPdmCriterion criterion)
  {
    lock (this.syncRoot)
    {
      if (criterion != null)
      {
        if (this._items.IndexOf(criterion) >= 0)
        {
          this._items.Remove(criterion);
          return true;
        }
      }
    }
    return false;
  }

  /// <summary>
  /// Проверить наличие критерия в коллекции дочерних элементов
  /// </summary>
  /// <param name="criterion">Искомый критерий</param>
  /// <returns>true - критерий найден в коллекции дочерних элементов</returns>
  public virtual bool ExistsCriterion(IPdmCriterion criterion)
  {
    return this.Items.ExistsCriterion(criterion);
  }

  /// <summary>
  /// Отыскать первый критерий, у которого Guid опции равно указанному. Поиск выполняется в самом элементе и всех его вложенных элементах
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <returns>Первый критерий, у которого Guid опции равно указанному</returns>
  public virtual IPdmCriterion FindCriterion(Guid option)
  {
    return this.Option == option ? (IPdmCriterion) this : this.Items.FindCriterion(option);
  }

  /// <summary>
  /// Отыскать все критерии не заглушки, у которых Guid опции равно указанному.
  /// Поиск выполняется в самом элементе и всех его вложенных элементах
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <returns>Все критерии, у которых Guid опции равно указанному</returns>
  public virtual List<IPdmCriterion> FindCriterionEx(Guid option)
  {
    List<IPdmCriterion> criterionEx = new List<IPdmCriterion>();
    if (this._criterionType != PdmCriterionType.Stub && this.Option == option)
      criterionEx.Add((IPdmCriterion) this);
    criterionEx.AddRange((IEnumerable<IPdmCriterion>) this.Items.FindCriterionEx(option));
    return criterionEx;
  }

  /// <summary>
  /// Отыскать критерий, у которого Guid и значение опции и равны указанным значениям. Поиск выполняется в самом элементе и всех его вложенных элементах
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <param name="optionValue">ID значения опции</param>
  /// <returns>Критерий, у которого Guid и значение опции и равны указанным значениям</returns>
  public virtual IPdmCriterion FindCriterion(Guid option, string optionValue)
  {
    return this.Option == option && this.Value == optionValue ? (IPdmCriterion) this : this.Items.FindCriterion(option, optionValue);
  }

  /// <summary>
  /// Метод вызывается перед сохранением критерия в XML-документ. При возникновении ошибки следует сгенерировать исключение
  /// </summary>
  /// <param name="holder">Контейнер, которому принадлежит данный критерий</param>
  public virtual void BeforeSave(object holder)
  {
    if (this.CriterionType != PdmCriterionType.Stub)
    {
      OptionHolder optionHolder = !(this._option == Guid.Empty) ? PdmConfiguratorCache.CacheFindOption(this._option) : throw new PdmConfiguratorExeption(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_36"));
      string str = optionHolder == null ? this._option.ToString() : optionHolder.OptionCaption;
      if (this._operator == Operator.Undefined)
        throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_37"), (object) str));
      if (string.IsNullOrEmpty(this._value))
        throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_38"), (object) str));
    }
    for (int index = 0; index < this._items.Count; ++index)
      this._items[index].BeforeSave(holder);
  }

  /// <summary>Выполнена загрузка данных из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public virtual void XMLAfterLoad(XMLSettingsStorage xmlStorage, XmlNode node)
  {
  }

  /// <summary>
  /// Выполнено сохранение данных в состав указанного родительского узла
  /// </summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
  public virtual void XMLAfterSave(XMLSettingsStorage xmlStorage, XmlNode parentNode)
  {
  }

  /// <summary>Загрузить данные из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public virtual void Load(XMLSettingsStorage xmlStorage, XmlNode node)
  {
    this.Clear();
    if (node == null || node.Name != "e" || !(xmlStorage.Services.GetService(typeof (PdmGuidMapper)) is PdmGuidMapper service))
      return;
    string str1 = xmlStorage.GetAttributeValue(node, "a", "");
    if (!string.IsNullOrEmpty(str1) && str1.Length >= 4 && str1.IndexOf("-") > 0)
    {
      if (str1.StartsWith("!"))
      {
        str1 = str1.Substring(1);
        this.Not = true;
      }
      long int64 = StringsHelper.HexToInt64(str1.Substring(0, str1.IndexOf("-")));
      this.Option = service[int64];
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
  public virtual void Save(XMLSettingsStorage xmlStorage, XmlNode parentNode)
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
      long num = serviceInstance[this.Option];
      XmlNode xmlNode = xmlStorage.AddNode(parentNode, "e");
      string str = this.CriterionType == PdmCriterionType.Stub ? Helper.AsteriskString : string.Empty;
      xmlStorage.SetAttributeValue(xmlNode, "a", $"{(this.Not ? "!" : string.Empty)}{StringsHelper.IntToHex(num)}-{OperatorHelper.ToString(this.Operator)}{LogicalFunctionHelper.ToString(this.Function)}{str}{this.Value}");
      this.Items.Save(xmlStorage, xmlNode);
    }
    lock (this.syncRoot)
      this.XMLAfterSave(xmlStorage, parentNode);
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты идентичны</returns>
  public override bool Equals(object obj)
  {
    return obj is PdmCriterion pdmCriterion && this.Option.Equals(pdmCriterion.Option) && this.Value.Equals(pdmCriterion.Value) && this.Operator == pdmCriterion.Operator && this.Items.Equals((object) pdmCriterion.Items) && this.Not == pdmCriterion.Not;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode()
  {
    return this.Option.GetHashCode() << 17 ^ this.Value.GetHashCode() << 2 ^ this.Operator.GetHashCode();
  }

  /// <summary>Представление экземпляра класса в виде строки</summary>
  /// <returns>Представление экземпляра класса в виде строки</returns>
  protected virtual string DebuggerToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    lock (this.syncRoot)
    {
      OptionHolder option = PdmConfiguratorCache.CacheFindOption(this.Option);
      string str = option == null || string.IsNullOrEmpty(this._value) ? $"[{this._value}]" : $"[{this._value}]\"{option.GetAsString(this._value)}\"";
      if (this.CriterionType == PdmCriterionType.Stub)
        stringBuilder.Append("([Заглушка] ");
      else
        stringBuilder.Append("(");
      stringBuilder.Append(option != null ? string.Format("[{2}]\"{0}\" {1} ", (object) option.OptionCaption, (object) this.Operator, (object) option.OptionObjectID) : $"\"{this.Option.ToString()}\" {this.Operator} ");
      if (option != null)
        stringBuilder.Append($"{str}) ");
      else
        stringBuilder.Append("\"\") ");
      stringBuilder.Append((object) this.Function);
      stringBuilder.Append(" ");
    }
    return stringBuilder.ToString();
  }
}
