// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmCompositionBrowserEventArgs
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;
using System.Collections;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Аргументы для работы механизма по рекурсивной раскрутке конфигурируемых составов
/// </summary>
[Serializable]
public class PdmCompositionBrowserEventArgs : EventArgs, IAssignable, ICloneable
{
  /// <summary>
  /// Тип связи, по которому будут раскручиваться составы. Если требуется раскрутка по
  /// всем видимым типам связей, следует указать значение Intermech.Consts.UnknownRelationTypeId
  /// </summary>
  public int RelTypeID = -1;
  /// <summary>
  /// Уникальный ключ настроек фильтрации состава. Если фильтрация состава не требуется,
  /// можно указать константу Intermech.SystemGUIDs.filtrationAllVersions. Если требуется фильтрация по
  /// конкретному правилу подбора версий, следует указать значение String.Empty
  /// </summary>
  public string FiltrationOwnerID = "cad005aa-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// Правило подбора версий, по которому требуется выполнять подбор. Если подбор
  /// выполняется по настройкам фильтрации (заполнено свойство filtrationOwnerID), следует указать значение null
  /// </summary>
  public VersionsRule Rule;
  /// <summary>Дополнительные параметры фильтрации составов или null</summary>
  public HybridDictionary Tags;
  /// <summary>
  /// Остановить рекурсивную трассировку состава при первой же найденной ошибке
  /// </summary>
  public bool BeforeFirstError;
  /// <summary>
  /// Выполнять полную трассировку каждого элемента состава
  /// (сбор статистики в виде PdmCompositionTraceResult)
  /// </summary>
  public bool FullTrace;

  /// <summary>Создать пустые аргументы для поиска</summary>
  public PdmCompositionBrowserEventArgs()
  {
  }

  /// <summary>
  /// Создать аргументы для поиска, заполнить их информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public PdmCompositionBrowserEventArgs(object source) => this.Assign(source);

  /// <summary>
  /// Создать аргументы для сервиса, позволяющего выполнять рекурсивную раскрутку конфигурируемых составов
  /// </summary>
  /// <param name="relTypeID">Тип связи, по которому будут раскручиваться составы. Если требуется раскрутка по
  /// всем видимым типам связей, следует указать значение Intermech.Consts.UnknownRelationTypeId</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава. Если фильтрация состава не требуется,
  /// можно указать константу Intermech.SystemGUIDs.filtrationAllVersions. Если требуется фильтрация по
  /// конкретному правилу подбора версий, следует указать значение String.Empty</param>
  /// <param name="rule">Правило подбора версий, по которому требуется выполнять подбор. Если подбор
  /// выполняется по настройкам фильтрации (заполнено свойство filtrationOwnerID), следует указать значение null</param>
  /// <param name="tags">Дополнительные параметры фильтрации составов или null</param>
  /// <param name="fullTrace">Выполнять полную трассировку каждого элемента состава
  /// (сбор статистики в виде PdmCompositionTraceResult)</param>
  public PdmCompositionBrowserEventArgs(
    int relTypeID,
    string filtrationOwnerID,
    VersionsRule rule,
    HybridDictionary tags,
    bool fullTrace)
  {
    this.RelTypeID = relTypeID;
    this.FiltrationOwnerID = filtrationOwnerID;
    this.Rule = rule;
    this.Tags = tags;
    this.FullTrace = fullTrace;
  }

  /// <summary>
  /// Создать аргументы для сервиса, позволяющего выполнять рекурсивную раскрутку конфигурируемых составов
  /// </summary>
  /// <param name="relTypeID">Тип связи, по которому будут раскручиваться составы. Если требуется раскрутка по
  /// всем видимым типам связей, следует указать значение Intermech.Consts.UnknownRelationTypeId</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава. Если фильтрация состава не требуется,
  /// можно указать константу Intermech.SystemGUIDs.filtrationAllVersions. Если требуется фильтрация по
  /// конкретному правилу подбора версий, следует указать значение String.Empty</param>
  /// <param name="rule">Правило подбора версий, по которому требуется выполнять подбор. Если подбор
  /// выполняется по настройкам фильтрации (заполнено свойство filtrationOwnerID), следует указать значение null</param>
  /// <param name="beforeFirstError">Остановить трассировку состава при нахождении первой ошибки </param>
  /// <param name="tags">Дополнительные параметры фильтрации составов или null</param>
  /// <param name="fullTrace">Выполнять полную трассировку каждого элемента состава
  /// (сбор статистики в виде PdmCompositionTraceResult)</param>
  public PdmCompositionBrowserEventArgs(
    int relTypeID,
    string filtrationOwnerID,
    VersionsRule rule,
    HybridDictionary tags,
    bool beforeFirstError,
    bool fullTrace)
    : this(relTypeID, filtrationOwnerID, rule, tags, fullTrace)
  {
    this.BeforeFirstError = beforeFirstError;
  }

  public PdmCompositionBrowserJobStatus Status { get; set; }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this.RelTypeID = -1;
    this.FiltrationOwnerID = "cad005aa-306c-11d8-b4e9-00304f19f545";
    this.Rule = (VersionsRule) null;
    this.Tags = new HybridDictionary();
    this.BeforeFirstError = false;
    this.FullTrace = false;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is PdmCompositionBrowserEventArgs browserEventArgs))
      return;
    this.RelTypeID = browserEventArgs.RelTypeID;
    this.FiltrationOwnerID = browserEventArgs.FiltrationOwnerID;
    this.Rule = browserEventArgs.Rule;
    this.BeforeFirstError = browserEventArgs.BeforeFirstError;
    this.FullTrace = browserEventArgs.FullTrace;
    if (browserEventArgs.Tags == null)
      return;
    foreach (object key in (IEnumerable) browserEventArgs.Tags.Keys)
    {
      object tag = browserEventArgs.Tags[key];
      this.Tags.Add(key is ICloneable ? ((ICloneable) key).Clone() : key, tag is ICloneable ? ((ICloneable) tag).Clone() : tag);
    }
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);
}
