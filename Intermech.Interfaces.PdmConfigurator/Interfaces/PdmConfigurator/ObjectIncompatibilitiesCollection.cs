// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.ObjectIncompatibilitiesCollection
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;
using System.Diagnostics;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Коллекция критериев условий несовместимости опций в конфигураторе составов IPS
/// 
/// [!] Информация хранится в атрибуте объекта "Условия несовместимости опций",
/// назначаемому конфигурируемым типам объектов
/// </summary>
[Serializable]
public sealed class ObjectIncompatibilitiesCollection : PdmCriterionsCollection
{
  /// <summary>Список связанных значений опций</summary>
  public LinkedOptions LinkedOptions = new LinkedOptions();
  /// <summary>Шифр объекта</summary>
  public ConfigurationCode ConfigurationCode = new ConfigurationCode();

  /// <summary>
  /// Значение по умолчанию, если вычисление не может быть выполнено или не требуется
  /// </summary>
  public override PdmConfiguratorResult DefaultEvaluatorValue
  {
    [DebuggerStepThrough] get => PdmConfiguratorResult.False;
  }

  /// <summary>Логическая функция по умолчанию</summary>
  public override LogicalFunction DefaultFunction
  {
    [DebuggerStepThrough] get => LogicalFunction.Or;
  }

  /// <summary>
  /// Идентификатор атрибута, в котором хранится содержимое данной коллекции
  /// </summary>
  public override int LoadSaveAttributeID
  {
    [DebuggerStepThrough] get => Consts.attributeOptionsIncompatibilityID;
  }

  /// <summary>Является ли элемент пустым или нет</summary>
  public override bool Empty
  {
    get
    {
      lock (this.syncRoot)
      {
        if (this.Count == 0 && this.LinkedOptions.Empty && this.ConfigurationCode.Empty)
          return true;
        bool empty1 = this.LinkedOptions.Empty;
        if (!empty1)
          return empty1;
        bool empty2 = this.ConfigurationCode.Empty;
        if (!empty2)
          return empty2;
        for (int index = 0; index < this.Count; ++index)
          empty2 &= this[index].Empty;
        return empty2;
      }
    }
  }

  /// <summary>
  /// Создать пустую коллекцию критериев условия применения объекта в конфигураторе составов IPS
  /// </summary>
  public ObjectIncompatibilitiesCollection()
  {
  }

  /// <summary>
  /// Создать пустую коллекцию критериев условия применения объекта в конфигураторе составов IPS
  /// </summary>
  /// <param name="function">Логическая функция для объединения данной коллекции со следующим критерием/коллекцией</param>
  public ObjectIncompatibilitiesCollection(LogicalFunction function)
    : base(function)
  {
  }

  /// <summary>
  /// Создать коллекцию критериев условия применения объекта в конфигураторе составов IPS на основе указанного объекта
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public ObjectIncompatibilitiesCollection(object source)
    : base(source)
  {
  }

  /// <summary>
  /// Вернуть тип данных элементов коллекции (классов, которые реализуют узлы-критерии)
  /// </summary>
  /// <returns>Тип данных элементов коллекции (классов, которые реализуют узлы-критерии)</returns>
  public override Type GetElementType() => typeof (ObjectIncompatibilityCriterion);

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    this.LinkedOptions.Clear();
    this.ConfigurationCode.Clear();
    base.Clear();
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    base.Assign(source);
    if (!(source is ObjectIncompatibilitiesCollection incompatibilitiesCollection))
      return;
    lock (this.syncRoot)
    {
      this.LinkedOptions.Assign((object) incompatibilitiesCollection.LinkedOptions);
      this.ConfigurationCode.Assign((object) incompatibilitiesCollection.ConfigurationCode);
    }
  }

  /// <summary>Выполнить вычисление значений критериев из коллекции</summary>
  /// <param name="context">Контекст конфигуратора составов IPS</param>
  /// <returns>Результат вычисления значений критериев,
  /// исключение, если значение опции/критерия не найдено в контексте, либо принадлежат разным опциям</returns>
  public override PdmConfiguratorResult Evalute(PdmConfiguratorContext context)
  {
    PdmConfiguratorResult configuratorResult1 = this.DefaultEvaluatorValue;
    lock (this.syncRoot)
    {
      if (this.Count == 0)
        return configuratorResult1;
      LogicalFunction func = this.DefaultFunction;
      for (int index = 0; index < this.Count; ++index)
      {
        IEvaluator evaluator = (IEvaluator) this[index];
        PdmConfiguratorResult configuratorResult2 = evaluator.Evalute(context);
        switch (configuratorResult2)
        {
          case PdmConfiguratorResult.OptionValueNotFound:
          case PdmConfiguratorResult.ConflictOptionValueNotFound:
            configuratorResult2 = PdmConfiguratorResult.False;
            break;
        }
        this.EvaluateTrace.Assign((object) evaluator.EvaluateTrace);
        if (configuratorResult2 != PdmConfiguratorResult.False)
          return configuratorResult2;
        configuratorResult1 = index > 0 ? Helper.Combine(configuratorResult1, configuratorResult2, func) : configuratorResult2;
        func = evaluator.Function;
      }
    }
    if (this.Not)
    {
      if (this.EvaluateTrace.Flags == PdmConfiguratorResult.False)
        this.EvaluateTrace.Flags = PdmConfiguratorResult.True;
      else if (this.EvaluateTrace.Flags == PdmConfiguratorResult.True)
        this.EvaluateTrace.Flags = PdmConfiguratorResult.False;
    }
    return configuratorResult1;
  }

  /// <summary>Выполнена загрузка данных из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public override void XMLAfterLoad(XMLSettingsStorage xmlStorage, XmlNode node)
  {
    base.XMLAfterLoad(xmlStorage, node);
    node = node == null || node.ParentNode == null ? (XmlNode) null : node.ParentNode;
    if (node == null || node.ChildNodes.Count == 0)
      return;
    for (int i = 0; i < node.ChildNodes.Count; ++i)
    {
      XmlNode childNode = node.ChildNodes[i];
      if (childNode.Name == "f")
        this.LinkedOptions.Load(xmlStorage, childNode);
      else if (childNode.Name == "i")
        this.ConfigurationCode.Load(xmlStorage, childNode);
    }
  }

  /// <summary>
  /// Выполнено сохранение данных в состав указанного родительского узла
  /// </summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
  public override void XMLAfterSave(XMLSettingsStorage xmlStorage, XmlNode parentNode)
  {
    base.XMLAfterSave(xmlStorage, parentNode);
    this.LinkedOptions.Save(xmlStorage, parentNode);
    this.ConfigurationCode.Save(xmlStorage, parentNode);
  }
}
