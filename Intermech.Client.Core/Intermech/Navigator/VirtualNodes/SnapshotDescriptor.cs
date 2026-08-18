
// Type: Intermech.Navigator.VirtualNodes.SnapshotDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Client.Snapshots;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.Snapshots;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Navigator.VirtualNodes;

/// <summary>ID и фабрика виртуального элемента дерева навигатора, описывающего элемент сохранённого ранее состава изделия</summary>
public class SnapshotDescriptor : 
  HiveDescriptor,
  IDescriptor,
  INodeItems,
  IPersistable,
  IContextAware,
  ISnapshotContext
{
  /// <summary>Свойство XML для записи нормализованного идентификатора итерации</summary>
  private const string PropSnapshotID = "SnapshotId";
  /// <summary>Свойство XML для записи способа отображения содержимого структуры итерации</summary>
  private const string PropContent = "Content";
  /// <summary>Свойство XML для записи способа отображения сравнения составов</summary>
  private const string PropCompareNodes = "CompareNodes";
  /// <summary>Тип enum настроек что будет содержаться в итерации - сохранённый состав или сравнение с актуальным</summary>
  public static Type ContentType = typeof (SnapshotDescriptor.Content);
  /// <summary>Контейнер сервисов</summary>
  [NotNull]
  protected readonly AdvancedServiceContainer _Services = new AdvancedServiceContainer();
  /// <summary>Что показывать для отображения сравнения составов</summary>
  private SnapshotDescriptor.CompareNodes _shownCompareNodes = SnapshotDescriptor.CompareNodes.All;

  /// <summary>Получить тип отображаемого в итерации содержимого по контексту</summary>
  public static SnapshotDescriptor.Content GetContentFromContext([NotNull] IServiceProvider context)
  {
    SnapshotDescriptor.Content service;
    return !context.TryGetService<SnapshotDescriptor.Content>(out service) ? SnapshotDescriptor.Content.SavedStructure : service;
  }

  /// <summary>Базовая инициализация параметров, вызывается из конструкторов. Требуется для вызова из конструкторов,
  /// где инициализация передаваемых параметров происходит в коде (напр в конструкторе десериализации
  /// SnapshotDescriptor(PersistentState state))</summary>
  /// <param name="snapshot">Ленивое хранилище атрибутов итерации</param>
  /// <param name="shownContent">Что показывать в содержимом итерации</param>
  private void BaseInit(
    [NotNull] ISnapshot snapshot,
    SnapshotDescriptor.Content shownContent,
    SnapshotDescriptor.CompareNodes shownCompareNodes)
  {
    this.ShownContent = shownContent;
    this._shownCompareNodes = shownCompareNodes;
    this._Services.AddService<ISnapshot>(snapshot);
    this._Services.AddService<SnapshotDescriptor.Content>(this.ShownContent);
  }

  /// <summary>Приватный конструктор, создающий дескриптор итерации по готовому её ленивому контейнеру атрибутов</summary>
  /// <param name="snapshot">Ленивое хранилище атрибутов итерации</param>
  /// <param name="shownContent">Что показывать в содержимом итерации</param>
  /// <param name="shownCompareNodes">Что показывать для отображения сравнения составов</param>
  protected SnapshotDescriptor(
    [NotNull] ISnapshot snapshot,
    SnapshotDescriptor.Content shownContent,
    SnapshotDescriptor.CompareNodes shownCompareNodes = SnapshotDescriptor.CompareNodes.All)
    : base(Intermech.Navigator.Consts.CategoryVirtualObjectNode, 0, snapshot.Name)
  {
    this.Snapshot = snapshot;
    this.BaseInit(snapshot, shownContent, shownContent == SnapshotDescriptor.Content.CompareWithActual ? shownCompareNodes : SnapshotDescriptor.CompareNodes.HideNotChanged);
  }

  /// <summary>Приватный конструктор, создающий дескриптор итерации по готовому её ленивому контейнеру атрибутов</summary>
  /// <param name="state"></param>
  protected SnapshotDescriptor([NotNull] PersistentState state)
    : base(state)
  {
    ISnapshot snapshot = !(state.GetValue("SnapshotId") is long snapshotID) ? (ISnapshot) null : Repository.Snapshots.Create(snapshotID, failIfNotFound: false);
    if (snapshot == null)
      throw new Exception("SnapshotId property was not found in SnapshotDescriptor state");
    SnapshotDescriptor.Content shownContent = !(state.GetValue("Content") is int num1) ? SnapshotDescriptor.Content.SavedStructure : (SnapshotDescriptor.Content) num1;
    SnapshotDescriptor.CompareNodes shownCompareNodes = shownContent != SnapshotDescriptor.Content.CompareWithActual ? SnapshotDescriptor.CompareNodes.HideNotChanged : (!(state.GetValue("CompareNodes") is int num2) ? SnapshotDescriptor.CompareNodes.All : (SnapshotDescriptor.CompareNodes) num2);
    this.Snapshot = snapshot;
    this.BaseInit(snapshot, shownContent, shownCompareNodes);
  }

  /// <summary>Статический метод-конструктор</summary>
  /// <param name="snapshotID">Идентификатор итерации</param>
  /// <param name="shownContent">(Optional) Что показывать в содержимом итерации</param>
  /// <param name="failIfNotExist">(Optional) Выбрасывать ли исключительную ситуацию если итерация с переданным
  /// идентификатором недоступна на сервере (удалена, или вышла из зоны видимости)</param>
  /// <returns>Созданный дескриптор итерации</returns>
  [CanBeNull]
  public static SnapshotDescriptor Create(
    [NotEmpty] long snapshotID,
    SnapshotDescriptor.Content shownContent = SnapshotDescriptor.Content.SavedStructure,
    bool failIfNotExist = true)
  {
    return SnapshotDescriptor.Create(snapshotID, SnapshotAttributes.Default, shownContent, failIfNotExist);
  }

  /// <summary>Статический метод-конструктор</summary>
  /// <param name="snapshotID">Идентификатор итерации</param>
  /// <param name="preLoadAttributes">Набор флагов, перечисляющий атрибуты, которые должны быть закэшированы ещё при создании итерации</param>
  /// <param name="snapshotContent">Что показывать в содержимом итерации</param>
  /// <param name="failIfNotExist">Выбрасывать ли исключительную ситуацию если итерация с переданным идентификатором недоступна на сервере
  /// (удалена, или вышла из зоны видимости)</param>
  /// <returns>Созданный дескриптор итерации</returns>
  [CanBeNull]
  public static SnapshotDescriptor Create(
    [NotEmpty] long snapshotID,
    SnapshotAttributes preLoadAttributes,
    SnapshotDescriptor.Content shownContent = SnapshotDescriptor.Content.SavedStructure,
    bool failIfNotExist = true)
  {
    ISnapshot snapshot = Repository.Snapshots.Create(snapshotID, preLoadAttributes, failIfNotExist);
    return snapshot == null || snapshot.ExistanceStatus == ExistanceStatuses.NotExistOnServer ? (SnapshotDescriptor) null : new SnapshotDescriptor(snapshot, shownContent);
  }

  /// <summary>Статический метод-конструктор</summary>
  /// <param name="snapshotID">Идентификатор итерации</param>
  /// <param name="snapshotDescriptor">[out] Созданный дескриптор итерации</param>
  /// <param name="snapshotContent">Что показывать в содержимом итерации</param>
  /// <returns>true if it succeeds, false if it fails</returns>
  [ContractAnnotation("=> true, snapshotDescriptor: notnull; => false, snapshotDescriptor: null")]
  public static bool TryCreate(
    long snapshotID,
    out SnapshotDescriptor snapshotDescriptor,
    SnapshotDescriptor.Content shownContent = SnapshotDescriptor.Content.SavedStructure)
  {
    snapshotDescriptor = SnapshotDescriptor.Create(snapshotID, SnapshotAttributes.Default, shownContent, false);
    return snapshotDescriptor != null;
  }

  /// <summary>Статический метод-конструктор</summary>
  /// <param name="snapshotID">Идентификатор итерации</param>
  /// <param name="snapshotDescriptor">[out] Созданный дескриптор итерации</param>
  /// <param name="preLoadAttributes">Набор флагов, перечисляющий атрибуты, которые должны быть закэшированы ещё при создании итерации</param>
  /// <param name="snapshotContent">Что показывать в содержимом итерации</param>
  /// <returns>true if it succeeds, false if it fails</returns>
  [ContractAnnotation("=> true, snapshotDescriptor: notnull; => false, snapshotDescriptor: null")]
  public static bool TryCreate(
    [NotEmpty] long snapshotID,
    out SnapshotDescriptor snapshotDescriptor,
    SnapshotAttributes preLoadAttributes,
    SnapshotDescriptor.Content shownContent = SnapshotDescriptor.Content.SavedStructure)
  {
    snapshotDescriptor = SnapshotDescriptor.Create(snapshotID, preLoadAttributes, shownContent, false);
    return snapshotDescriptor != null;
  }

  /// <summary>Идентификатор итерации</summary>
  public long ID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Snapshot.ID;
  }

  /// <summary>Имя итерации</summary>
  [NotNull]
  public string Name
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Snapshot.Name;
  }

  /// <summary>Идентификатор головного объекта итерации</summary>
  public long RootObjectID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Snapshot.RootObjectID;
  }

  /// <summary>Идентификатор версии головного объекта итерации</summary>
  public long RootObjectVersionID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Snapshot.RootObjectVersionID;
  }

  /// <summary>Что показывать в содержимом итерации</summary>
  private SnapshotDescriptor.Content ShownContent { get; set; }

  /// <summary>Создать ноду итерации</summary>
  /// <param name="nodeID">Идентификатор ноды</param>
  /// <returns>Интерфейс ноды</returns>
  public override INode GetChild(INodeID nodeID)
  {
    return !(nodeID is SnapshotsNodeID) ? (INode) null : (INode) new SnapshotTreeNode(this.Services, this.Snapshot);
  }

  /// <summary>Gets a data</summary>
  /// <param name="nodeID"></param>
  /// <param name="dataFormat"></param>
  /// <returns>The data</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (SnapshotsNodeID) || dataFormat == typeof (INodeID))
      return (object) this.GetRecordNodeID();
    if (dataFormat == typeof (ISnapshot))
      return (object) this.Snapshot;
    if (dataFormat == typeof (INode))
      return (object) this.GetChild(this.GetRecordNodeID());
    if (dataFormat == typeof (IDescriptor))
      return (object) new SnapshotDescriptor(this.Snapshot, this.ShownContent);
    return !(dataFormat == typeof (ICanOpenInNewWindow)) ? base.GetData(nodeID, dataFormat) : (object) new CanOpenInNewWindow();
  }

  /// <summary>Вернуть описание корневого узла для текущего дескриптора</summary>
  /// <returns>The record node identifier</returns>
  [NotNull]
  public override INodeID GetRecordNodeID()
  {
    int objectTypeId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      objectTypeId = sessionKeeper.Session.GetObjectInfo(this.Snapshot.RootObjectVersionID).ObjectTypeID;
    return (INodeID) new SnapshotsNodeID(this.Snapshot.ID, this.Snapshot.RootObjectVersionID, this.Snapshot.RootObjectID, objectTypeId, this.Snapshot.Name, this.Snapshot.OwnerID, this.Snapshot.ModifyDate);
  }

  /// <summary>Отразить указанную колонку в идентификатор атрибута</summary>
  /// <param name="column">Колонка "Навигатора"</param>
  /// <returns>Идентификатор атрибута</returns>
  public override object MapColumnToField(NodeColumn column)
  {
    return column.ID == null || !(column.SchemeGuid == SnapshotConsts.SNAPSHOT_SCHEME_GUID) ? base.MapColumnToField(column) : column.ID;
  }

  /// <summary>Вернуть массив данных для указанного описания узла</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="fields">Поля, загруженные из базы данных</param>
  /// <returns>массив данных для указанного описания узла</returns>
  public override object[] GetRecordValues(INodeID nodeID, object[] fields)
  {
    if (!(nodeID is SnapshotsNodeID snapshotsNodeId))
      return base.GetRecordValues(nodeID, fields);
    object[] recordValues = new object[fields.Length];
    for (int index = 0; index < fields.Length; ++index)
    {
      object field = fields[index];
      if (field is int num)
      {
        if (num == SnapshotConsts.SNAPSHOT_ID)
          recordValues[index] = (object) snapshotsNodeId.SnapshotID;
        else if (num == SnapshotConsts.SNAPSHOT_DATE)
          recordValues[index] = (object) snapshotsNodeId.snapDate;
        else if (num == SnapshotConsts.F_NAME)
          recordValues[index] = (object) snapshotsNodeId.name;
      }
      if (field is string && field.Equals((object) "F_CAPTION") || field is ObligatoryObjectAttributes && field.Equals((object) ObligatoryObjectAttributes.CAPTION))
        recordValues[index] = (object) snapshotsNodeId.name;
    }
    return recordValues;
  }

  /// <summary>Сериализация состояния дескриптора</summary>
  /// <param name="state">Хранилище состояния объекта</param>
  public override void GetObjectData(PersistentState state)
  {
    base.GetObjectData(state);
    state.AddValue("SnapshotId", (object) this.SnapshotID);
    state.AddValue("Content", (object) (int) this.ShownContent);
    state.AddValue("CompareNodes", (object) (int) this._shownCompareNodes);
  }

  /// <summary>Контейнер сервисов</summary>
  /// <value>The services</value>
  [NotNull]
  public virtual IServiceProvider Services
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IServiceProvider) this._Services;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Services.AdvancedProvider = value;
    }
  }

  /// <summary>Интерфейс итерации</summary>
  public ISnapshot Snapshot { get; }

  /// <summary>Идентификатор итерации</summary>
  public long SnapshotID => this.Snapshot.ID;

  /// <summary>Что показывать в содержимом итерации</summary>
  public enum Content
  {
    /// <summary>Сохранённый в ней состав объекта</summary>
    SavedStructure,
    /// <summary>Сравнение актуального состава объекта с сохранённым в итерации</summary>
    CompareWithActual,
  }

  /// <summary>Что показывать для отображения сравнения составов</summary>
  [Flags]
  public enum CompareNodes
  {
    /// <summary>"Нулевое" значение</summary>
    None = 0,
    /// <summary>Показывать ли не изменившиеся с момента создания итерации элементы состава</summary>
    NotChangedElements = 1,
    /// <summary>Показывать элементы состава, чьи атрибуты были изменены с момента создания итерации</summary>
    ChangedElements = 2,
    /// <summary>Показывать элементы состава, присутствующие в актуальном составе, однако отсутствующие в составе, сохранённом в итерацию</summary>
    NewElements = 4,
    /// <summary>Показывать элементы состава, присутствовавшие в составе, сохранённом в итерацию, однако отсутствующие в актуальном составе</summary>
    DeletedElements = 8,
    /// <summary>Показывать все элементы сравнения составов, не изменившиеся, удалённые, новые и т.д.</summary>
    All = DeletedElements | NewElements | ChangedElements | NotChangedElements, // 0x0000000F
    /// <summary>Показывать все элементы сравнения составов, кроме не изменившихся</summary>
    HideNotChanged = DeletedElements | NewElements | ChangedElements, // 0x0000000E
  }
}
