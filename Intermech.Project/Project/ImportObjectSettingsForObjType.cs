// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ImportObjectSettingsForObjType
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Project;

/// <summary>Класс для настроек импорта объектов в новом окне импорта для определённого типа объекта</summary>
[Serializable]
public class ImportObjectSettingsForObjType : ImportObjectSettingsBase, ICloneable
{
  /// <summary>Настройки по-умолчанию</summary>
  [NotNull]
  [NonSerialized]
  private ImportObjectSettings _defaultSettings;

  /// <summary>Идентификатор типа объекта, к которому привязаны настройки</summary>
  public int ObjType { get; private set; }

  /// <summary>Настройки по-умолчанию</summary>
  [NotNull]
  public override ImportObjectSettings DefaultSettings
  {
    [DebuggerStepThrough] get => this._defaultSettings;
  }

  /// <summary>Перечисление типов объектов, которым назначены специальные настройки</summary>
  [NotNull]
  public override IReadOnlyCollection<int> ObjTypesWithSpecialTypes
  {
    [DebuggerStepThrough] get => this._defaultSettings.ObjTypesWithSpecialTypes;
  }

  /// <summary>Интерфейс "тип объекта =&gt; настройки для него"</summary>
  [NotNull]
  public override SettingsForObjTypeLink SettingsForObjType
  {
    [DebuggerStepThrough] get => this._defaultSettings.SettingsForObjType;
  }

  /// <summary>Импортировать как подзадачи</summary>
  public override bool ImportAsSubTasks
  {
    [DebuggerStepThrough] get => this._defaultSettings.ImportAsSubTasks;
    [DebuggerStepThrough] set => this._defaultSettings.ImportAsSubTasks = value;
  }

  /// <summary>Импортировать ли корневой элемент</summary>
  public override bool ImportRootObjects
  {
    [DebuggerStepThrough] get => this._defaultSettings.ImportRootObjects;
    [DebuggerStepThrough] set => this._defaultSettings.ImportRootObjects = value;
  }

  /// <summary>Ограничить ли глубину импорта</summary>
  public override bool LimitMaxLevels
  {
    [DebuggerStepThrough] get => this._defaultSettings.LimitMaxLevels;
    [DebuggerStepThrough] set => this._defaultSettings.LimitMaxLevels = value;
  }

  /// <summary>Число импортируемых уровней структуры (если глубина импорта ограничена)</summary>
  public override int LimitMaxLevelsCount
  {
    [DebuggerStepThrough] get => this._defaultSettings.LimitMaxLevelsCount;
    [DebuggerStepThrough] set => this._defaultSettings.LimitMaxLevelsCount = value;
  }

  /// <summary>Создавать вложенные копии суммарных задач</summary>
  public override bool CopySummaries
  {
    [DebuggerStepThrough] get => this._defaultSettings.CopySummaries;
    [DebuggerStepThrough] set => this._defaultSettings.CopySummaries = value;
  }

  /// <summary>Создавать задачи на одном уровне, игнорируя иерархию</summary>
  public override bool LinearImport
  {
    [DebuggerStepThrough] get => this._defaultSettings.LinearImport;
    [DebuggerStepThrough] set => this._defaultSettings.LinearImport = value;
  }

  /// <summary>Создавать ли итерации импортируемых объектов</summary>
  public override bool CreateIteration
  {
    [DebuggerStepThrough] get => this._defaultSettings.CreateIteration;
    [DebuggerStepThrough] set => this._defaultSettings.CreateIteration = value;
  }

  /// <summary>Наименование итерации</summary>
  public override string IterationName
  {
    [DebuggerStepThrough] get => this._defaultSettings.IterationName;
    [DebuggerStepThrough] set => this._defaultSettings.IterationName = value;
  }

  /// <summary>Скрипт пост-обработки импортированных задач</summary>
  public override long FinalScriptID
  {
    [DebuggerStepThrough] get => this._defaultSettings.FinalScriptID;
    [DebuggerStepThrough] set => this._defaultSettings.FinalScriptID = value;
  }

  /// <summary>Информировать подписчиков события AfterPrototypeChanged что прототип изменился</summary>
  public override void FireAfterPrototypeChanged([CanBeNull] IDBObject iDbObject)
  {
    this._defaultSettings?.FireAfterPrototypeChanged(iDbObject);
  }

  /// <summary>Информировать подписчиков события AfterPrototypeChanged что прототип изменился</summary>
  public override void FireAfterInitTaskScriptChanged([CanBeNull] IDBObject iDbObject)
  {
    this._defaultSettings?.FireAfterInitTaskScriptChanged(iDbObject);
  }

  public ImportObjectSettingsForObjType(
    [NotNull] ImportObjectSettings defaultSettings,
    [NotEmpty] int objType,
    [CanBeEmpty] long initTaskScriptID = 0,
    [CanBeEmpty] long prototypeObjectVersionID = 0,
    [CanBeNull] Task initTaskSettings = null)
    : base(initTaskScriptID, prototypeObjectVersionID, initTaskSettings)
  {
    this._defaultSettings = defaultSettings;
    this.ObjType = objType;
  }

  public ImportObjectSettingsForObjType(
    [NotNull] ImportObjectSettings defaultSettings,
    [NotNull] ImportObjectSettingsForObjType prototype)
    : base((ImportObjectSettingsBase) prototype)
  {
    this._defaultSettings = defaultSettings;
    this.ObjType = prototype.ObjType;
  }

  public ImportObjectSettingsForObjType(
    [NotNull] ImportObjectSettings defaultSettings,
    [NotEmpty] int objectTypeID,
    [NotNull] ImportObjectSettingsBase prototype)
    : base(prototype)
  {
    this._defaultSettings = defaultSettings;
    this.ObjType = objectTypeID;
  }

  public override void LoadFormDictionary(Dictionary<string, object> dic)
  {
    base.LoadFormDictionary(dic);
    object obj;
    if (!dic.TryGetValue("ObjType", out obj))
      return;
    this.ObjType = (int) obj;
  }

  public override void SaveToDictionary(Dictionary<string, object> dic, bool withLinks = true)
  {
    base.SaveToDictionary(dic, withLinks);
    dic["ObjType"] = (object) this.ObjType;
  }

  internal void LinkToDefaultSettings([NotNull] ImportObjectSettings importObjectSettings)
  {
    this._defaultSettings = importObjectSettings;
  }

  public object Clone() => (object) new ImportObjectSettingsForObjType(this._defaultSettings, this);
}
