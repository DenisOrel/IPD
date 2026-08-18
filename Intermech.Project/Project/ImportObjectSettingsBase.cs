// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ImportObjectSettingsBase
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Project;

/// <summary>Базовый класс для настроек импорта объектов в новом окне импорта</summary>
[Serializable]
public abstract class ImportObjectSettingsBase
{
  private long _prototypeObjectVersionID;
  [CanBeEmpty]
  private long _initTaskScriptID;
  [CanBeNull]
  private Task _initTaskParams;

  /// <summary>Импортировать как подзадачи</summary>
  public abstract bool ImportAsSubTasks { get; set; }

  /// <summary>Импортировать ли корневой элемент</summary>
  public abstract bool ImportRootObjects { get; set; }

  /// <summary>Ограничить ли глубину импорта</summary>
  public abstract bool LimitMaxLevels { get; set; }

  /// <summary>Число импортируемых уровней структуры (если глубина импорта ограничена)</summary>
  public abstract int LimitMaxLevelsCount { get; set; }

  /// <summary>Создавать вложенные копии суммарных задач</summary>
  public abstract bool CopySummaries { get; set; }

  /// <summary>Создавать задачи на одном уровне, игнорируя иерархию</summary>
  public abstract bool LinearImport { get; set; }

  /// <summary>Создавать ли итерации импортируемых объектов</summary>
  public abstract bool CreateIteration { get; set; }

  /// <summary>Наименование итерации</summary>
  [NotNull]
  public abstract string IterationName { get; set; }

  /// <summary>Настройки по-умолчанию</summary>
  [NotNull]
  public abstract ImportObjectSettings DefaultSettings { get; }

  /// <summary>Перечисление типов объектов, которым назначены специальные настройки</summary>
  [NotNull]
  public abstract IReadOnlyCollection<int> ObjTypesWithSpecialTypes { get; }

  /// <summary>Интерфейс "тип объекта =&gt; настройки для него"</summary>
  [NotNull]
  public abstract SettingsForObjTypeLink SettingsForObjType { get; }

  /// <summary>Скрипт пост-обработки импортированных задач</summary>
  [CanBeEmpty]
  public abstract long FinalScriptID { get; set; }

  /// <summary>Событие вызывается после того, как изменится прототип</summary>
  public event Action<IDBObject> AfterPrototypeChanged;

  /// <summary>Информировать подписчиков события AfterPrototypeChanged что прототип изменился</summary>
  public void FireAfterPrototypeChanged()
  {
    if (this._prototypeObjectVersionID != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._prototypeObjectVersionID, false);
        if (objectActualCopy == null)
          this._prototypeObjectVersionID = 0L;
        this.FireAfterPrototypeChanged(objectActualCopy);
      }
    }
    else
      this.FireAfterPrototypeChanged((IDBObject) null);
  }

  /// <summary>Информировать подписчиков события AfterPrototypeChanged что прототип изменился</summary>
  public virtual void FireAfterPrototypeChanged([CanBeNull] IDBObject iDbObject)
  {
    Action<IDBObject> prototypeChanged = this.AfterPrototypeChanged;
    if (prototypeChanged == null)
      return;
    prototypeChanged(iDbObject);
  }

  /// <summary>Идентификатор объекта проекта, который содержит шаблон задачи (если назначены, иначе Intermech.Consts.UnknownObjectId)</summary>
  public long PrototypeObjectVersionID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._prototypeObjectVersionID;
    }
    set
    {
      if (this._prototypeObjectVersionID == Math.Abs(value))
        return;
      this._prototypeObjectVersionID = Math.Abs(value);
      this.FireAfterPrototypeChanged();
    }
  }

  /// <summary>Идентификатор объекта проекта, который содержит шаблон задачи (если назначены, иначе Intermech.Consts.UnknownObjectId)</summary>
  public QuickObjectInfo PrototypeObject
  {
    get
    {
      return Session.Invoke<QuickObjectInfo>((Session.SessionHandler<QuickObjectInfo>) (session => session.GetObjectInfo(this._prototypeObjectVersionID)));
    }
  }

  /// <summary>Идентификатор скрипта инициализации задачи (если назначены, иначе Intermech.Consts.UnknownObjectId)</summary>
  [CanBeEmpty]
  public long InitTaskScriptID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._initTaskScriptID;
    }
    set
    {
      if (this._initTaskScriptID == Math.Abs(value))
        return;
      this._initTaskScriptID = Math.Abs(value);
      this.FireAfterInitTaskScriptChanged();
    }
  }

  /// <summary>Событие вызывается после того, как изменится прототипов</summary>
  public event Action<IDBObject> AfterInitTaskScriptChanged;

  /// <summary>Информировать подписчиков события AfterPrototypeChanged что прототип изменился</summary>
  public void FireAfterInitTaskScriptChanged()
  {
    if (this._initTaskScriptID != 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._initTaskScriptID, false);
        if (objectActualCopy == null)
          this._initTaskScriptID = 0L;
        this.FireAfterInitTaskScriptChanged(objectActualCopy);
      }
    }
    else
      this.FireAfterInitTaskScriptChanged((IDBObject) null);
  }

  /// <summary>Информировать подписчиков события AfterPrototypeChanged что прототип изменился</summary>
  public virtual void FireAfterInitTaskScriptChanged([CanBeNull] IDBObject iDbObject)
  {
    Action<IDBObject> taskScriptChanged = this.AfterInitTaskScriptChanged;
    if (taskScriptChanged == null)
      return;
    taskScriptChanged(iDbObject);
  }

  /// <summary>Параметры задачи, которые будут назначаться всем создаваемым задачам при импорте-синхронизации</summary>
  [CanBeNull]
  public Task InitTaskParams
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._initTaskParams;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      if (this._initTaskParams == value)
        return;
      this._initTaskParams = value;
      this.FireAfterInitTaskParamsChanged(value);
    }
  }

  /// <summary>Событие, информирующее о том, что параметры задачи, которые будут назначаться всем создаваемым задачам при импорте-
  /// синхронизации, изменились</summary>
  public event Action<Task> AfterInitTaskParamsChanged;

  /// <summary>Информировать подписчиков события AfterInitTaskParamsChanged что параметры задачи, которые будут назначаться всем создаваемым задачам при импорте-
  /// синхронизации, изменились</summary>
  public virtual void FireAfterInitTaskParamsChanged([CanBeNull] Task task)
  {
    Action<Task> taskParamsChanged = this.AfterInitTaskParamsChanged;
    if (taskParamsChanged == null)
      return;
    taskParamsChanged(task);
  }

  protected ImportObjectSettingsBase(
    [CanBeEmpty] long initTaskScriptID = 0,
    [CanBeEmpty] long prototypeObjectVersionID = 0,
    [CanBeNull] Task initTaskParams = null)
  {
    this.InitTaskScriptID = initTaskScriptID;
    this.PrototypeObjectVersionID = prototypeObjectVersionID;
    if (initTaskParams != null)
    {
      using (MemoryStream serializationStream = new MemoryStream())
      {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize((Stream) serializationStream, (object) initTaskParams);
        serializationStream.Position = 0L;
        this.InitTaskParams = (Task) binaryFormatter.Deserialize((Stream) serializationStream);
      }
    }
    else
      this.InitTaskParams = (Task) null;
  }

  protected ImportObjectSettingsBase([NotNull] ImportObjectSettingsBase prototype)
  {
    this.InitTaskScriptID = prototype.InitTaskScriptID;
    this.PrototypeObjectVersionID = prototype.PrototypeObjectVersionID;
    if (prototype.InitTaskParams != null)
    {
      using (MemoryStream serializationStream = new MemoryStream())
      {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize((Stream) serializationStream, (object) prototype.InitTaskParams);
        serializationStream.Position = 0L;
        this.InitTaskParams = (Task) binaryFormatter.Deserialize((Stream) serializationStream);
      }
    }
    else
      this.InitTaskParams = (Task) null;
  }

  public virtual void LoadFormDictionary([NotNull] Dictionary<string, object> dic)
  {
    object obj;
    if (dic.TryGetValue("PrototypeObjectVersionID", out obj) && obj is long num1)
      this.PrototypeObjectVersionID = num1;
    if (dic.TryGetValue("InitTaskScriptID", out obj) && obj is long num2)
      this.InitTaskScriptID = num2;
    if (!dic.TryGetValue("InitTaskSettings", out obj) || !(obj is Task task))
      return;
    this.InitTaskParams = task;
  }

  public virtual void SaveToDictionary([NotNull] Dictionary<string, object> dic, bool withLinks = true)
  {
    if (withLinks)
    {
      dic["PrototypeObjectVersionID"] = (object) this.PrototypeObjectVersionID;
      dic["InitTaskScriptID"] = (object) this.InitTaskScriptID;
    }
    dic["InitTaskSettings"] = (object) this._initTaskParams;
  }

  public delegate void AfterValueChangedDelegate<T>(T oldValue);
}
