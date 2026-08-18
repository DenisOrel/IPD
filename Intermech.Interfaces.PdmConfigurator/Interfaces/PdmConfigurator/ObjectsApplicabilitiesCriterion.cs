// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.ObjectsApplicabilitiesCriterion
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Localization;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Критерий условия применения объекта в конфигураторе составов IPS
/// </summary>
[Serializable]
public sealed class ObjectsApplicabilitiesCriterion : PdmCriterion
{
  /// <summary>Создать пустой критерий</summary>
  public ObjectsApplicabilitiesCriterion()
  {
  }

  /// <summary>
  /// Создать заполненный критерий-заглушку (содержит только дочерние критерии)
  /// </summary>
  /// <param name="function">Логическая функция для объединения данного критерия со следующим критерием</param>
  /// <param name="items">Коллекция вложенных критериев условий примерения объекта в конфигураторе составов IPS</param>
  public ObjectsApplicabilitiesCriterion(
    LogicalFunction function,
    ObjectsApplicabilitiesCriterionsCollection items)
    : base(function, (PdmCriterionsCollection) items)
  {
  }

  /// <summary>Создать заполненный критерий</summary>
  /// <param name="option">Guid опции. Значение Guid.Empty позволяет создать критерий-заглушку, который
  /// служит для объединения нескольких дочерних критериев, но не учитывает своё значение</param>
  /// <param name="value">ID значения опции</param>
  /// <param name="operat">Оператор для сравнения значений опции конфигуратора составов IPS</param>
  /// <param name="function">Логическая функция для объединения данного критерия со следующим критерием</param>
  /// <param name="items">Коллекция вложенных критериев условий примерения объекта в конфигураторе составов IPS</param>
  public ObjectsApplicabilitiesCriterion(
    Guid option,
    string value,
    Operator operat,
    LogicalFunction function,
    ObjectsApplicabilitiesCriterionsCollection items)
    : base(option, value, operat, function, (PdmCriterionsCollection) items)
  {
  }

  /// <summary>Создать заполненный критерий</summary>
  /// <param name="context">Контекст, из которого будет получена вся недостающая информация</param>
  /// <param name="option">Идентификатор версии объекта опции (Guid будет получен из контекста).
  /// Значение Intermech.Consts.UnknownObjectID позволяет создать критерий-заглушку, который
  /// служит для объединения нескольких дочерних критериев, но не учитывает своё значение</param>
  /// <param name="value">Порядковый номер значения опции (Guid будет получен из контекста)</param>
  /// <param name="operat">Оператор для сравнения значений опции конфигуратора составов IPS</param>
  /// <param name="function">Логическая функция для объединения данного критерия со следующим критерием</param>
  /// <param name="items">Коллекция вложенных критериев условий примерения объекта в конфигураторе составов IPS</param>
  public ObjectsApplicabilitiesCriterion(
    PdmConfiguratorContext context,
    long option,
    int value,
    Operator operat,
    LogicalFunction function,
    ObjectsApplicabilitiesCriterionsCollection items)
    : base(context, option, value, operat, function, (PdmCriterionsCollection) items)
  {
  }

  /// <summary>Создать критерий на основе указанного объекта</summary>
  /// <param name="source">Объект-источник</param>
  public ObjectsApplicabilitiesCriterion(object source)
    : base(source)
  {
  }

  /// <summary>
  /// Значение по умолчанию, если вычисление не может быть выполнено или не требуется
  /// </summary>
  public override PdmConfiguratorResult DefaultEvaluatorValue
  {
    [DebuggerStepThrough] get => PdmConfiguratorResult.False;
  }

  /// <summary>
  /// Метод вызывается перед сохранением критерия в XML-документ. При возникновении ошибки следует сгенерировать исключение
  /// </summary>
  /// <param name="holder">Контейнер, которому принадлежит данный критерий</param>
  public override void BeforeSave(object holder)
  {
    ObjectOptionsHolder objectOptionsHolder = holder as ObjectOptionsHolder;
    base.BeforeSave(holder);
    if (this.CriterionType != PdmCriterionType.Stub)
    {
      OptionHolder option = PdmConfiguratorCache.CacheFindOption(this.Option);
      string str1 = option == null ? this._option.ToString() : option.OptionCaption;
      if (objectOptionsHolder != null)
      {
        if (option != null && objectOptionsHolder.Options.IndexOf(option.OptionObjectID) < 0)
          throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_32"), (object) str1));
        OptionValue optionValue = option.OptionValues.FindValue(this.Value);
        string str2 = optionValue != null ? optionValue.Value : string.Empty;
        if (!objectOptionsHolder.VisibleOptionValues.GetVisibleOptionValue(this._option, this._value))
          throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_33"), (object) str1, (object) str2));
      }
    }
    this.Items.BeforeSave(holder);
  }
}
