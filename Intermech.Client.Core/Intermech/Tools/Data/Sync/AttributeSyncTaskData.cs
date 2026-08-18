
// Type: Intermech.Tools.Data.Sync.AttributeSyncTaskData
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Data;
using Intermech.Localization;
using System;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Реализует контейнер для основных параметров переноса атрибутов из одной системы в другую.
/// </summary>
public class AttributeSyncTaskData
{
  private string entityDisplayName;
  private ValueBag sourceTable;
  private IAttributeSyncHelper sourceSyncHelper;
  private ValueBag targetTable;
  private IAttributeSyncHelper targetSyncHelper;
  private readonly AttributeSyncOptions options;

  /// <summary>Создает объект.</summary>
  public AttributeSyncTaskData()
  {
    this.entityDisplayName = string.Empty;
    this.options = new AttributeSyncOptions();
  }

  /// <summary>Задает передающую сторону.</summary>
  /// <param name="table">Таблица атрибутов</param>
  /// <param name="syncHelper">Сервисный объект</param>
  public void SetSource(ValueBag table, IAttributeSyncHelper syncHelper)
  {
    if (table == null)
      throw new ArgumentNullException(nameof (table));
    if (syncHelper == null)
      throw new ArgumentNullException(nameof (syncHelper));
    this.sourceTable = table;
    this.sourceSyncHelper = syncHelper;
  }

  /// <summary>Задает принимающую сторону.</summary>
  /// <param name="table">Таблица атрибутов</param>
  /// <param name="syncHelper">Сервисный объект</param>
  public void SetTarget(ValueBag table, IAttributeSyncHelper syncHelper)
  {
    if (table == null)
      throw new ArgumentNullException(nameof (table));
    if (syncHelper == null)
      throw new ArgumentNullException(nameof (syncHelper));
    this.targetTable = table;
    this.targetSyncHelper = syncHelper;
  }

  /// <summary>Меняет местами принимающую и передающую стороны.</summary>
  public void SwapSides()
  {
    if (this.sourceTable == null)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1626"));
    if (this.targetTable == null)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1627"));
    ValueBag sourceTable = this.sourceTable;
    this.sourceTable = this.targetTable;
    this.targetTable = sourceTable;
    IAttributeSyncHelper sourceSyncHelper = this.sourceSyncHelper;
    this.sourceSyncHelper = this.targetSyncHelper;
    this.targetSyncHelper = sourceSyncHelper;
  }

  /// <summary>
  /// Заполняет все поля данных текущего объекта, копируя их у указанного объекта.
  /// </summary>
  /// <param name="sourceObject">Объект, чьи поля следует скопировать</param>
  public void Assign(AttributeSyncTaskData sourceObject)
  {
    if (sourceObject == null)
      throw new ArgumentNullException(nameof (sourceObject));
    if (this == sourceObject)
      return;
    this.DoAssign(sourceObject);
  }

  /// <summary>
  /// Реализует заполнение всех полей данных текущего объекта, копируя их у указанного объекта.
  /// </summary>
  /// <param name="sourceObject">Объект, чьи поля следует скопировать</param>
  protected virtual void DoAssign(AttributeSyncTaskData sourceObject)
  {
    this.EntityDisplayName = sourceObject.EntityDisplayName;
    this.SetSource(sourceObject.SourceTable, sourceObject.SourceSyncHelper);
    this.SetTarget(sourceObject.TargetTable, sourceObject.TargetSyncHelper);
    this.Options.Assign(sourceObject.Options);
  }

  /// <summary>
  /// Возвращает или задает выводимое имя элемента, чьи атрибуты участвуют в переносе из одной системы в другую.
  /// </summary>
  public string EntityDisplayName
  {
    get => this.entityDisplayName;
    set => this.entityDisplayName = value;
  }

  /// <summary>Возвращает таблицу атрибутов передающей стороны.</summary>
  public ValueBag SourceTable => this.sourceTable;

  /// <summary>Возвращает сервисный объект передающей стороны.</summary>
  public IAttributeSyncHelper SourceSyncHelper => this.sourceSyncHelper;

  /// <summary>Возвращает таблицу атрибутов принимающей стороны.</summary>
  public ValueBag TargetTable => this.targetTable;

  /// <summary>Возвращает сервисный объект принимающей стороны.</summary>
  public IAttributeSyncHelper TargetSyncHelper => this.targetSyncHelper;

  /// <summary>
  /// Возвращает параметры округления значений атрибутов при сравнении.
  /// </summary>
  public AttributeSyncOptions Options => this.options;

  /// <summary>Проверяет корректность исходных параметров задачи.</summary>
  /// <exception cref="T:System.InvalidOperationException">Исходные параметры задачи заданы неверно</exception>
  public virtual void ValidateParameters()
  {
    if (string.IsNullOrEmpty(this.EntityDisplayName))
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1625"));
    if (this.SourceTable == null || this.SourceSyncHelper == null)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1626"));
    if (this.TargetTable == null || this.TargetSyncHelper == null)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1627"));
    this.Options.Validate();
  }
}
