// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.ObjectSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Localization;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Содержит основные сведения о версии объекта - ее тип и уникальный идентификатор.
/// </summary>
[DebuggerDisplay("ObjectSection: [{ObjectType}]{ObjectId} (NewObject: {NewObject})")]
public sealed class ObjectSection
{
  /// <summary>
  /// Статус существования объекта в базе данных на момент начала анализа изменений.
  /// </summary>
  private ObjectExistenceStatus existenceStatus;
  /// <summary>Возвращает или задает идентификатор версии объекта</summary>
  private long objectId;
  /// <summary>Возвращает или задает идентификатор типа объекта</summary>
  private int objectType;
  /// <summary>
  /// Хранит признак того, что тип документа выбран автоматически и требует проверки правильности от пользователя
  /// </summary>
  private bool requireTypeCheck;
  public static readonly SectionPropertyReference NewObjectRef = new SectionPropertyReference(typeof (ObjectSection), nameof (NewObject));
  public static readonly SectionPropertyReference ExistenceStatusRef = new SectionPropertyReference(typeof (ObjectSection), nameof (ExistenceStatus));
  public static readonly SectionPropertyReference ObjectIdRef = new SectionPropertyReference(typeof (ObjectSection), nameof (ObjectId));
  public static readonly SectionPropertyReference ObjectTypeRef = new SectionPropertyReference(typeof (ObjectSection), nameof (ObjectType));

  /// <summary>
  /// Возвращает или задает признак того, что это новый объект, который будет добавлен в базу IPS в
  /// процессе захвата изменений.
  /// </summary>
  [Indexable(IndexType.Equality, false)]
  public bool NewObject
  {
    [DebuggerStepThrough] get => this.ExistenceStatus != 0;
    set
    {
      if (this.NewObject == value)
        return;
      this.ExistenceStatus = value ? ObjectExistenceStatus.NewObject : ObjectExistenceStatus.ExistingObject;
    }
  }

  /// <summary>
  /// Возвращает или задает статус существования объекта IPS в базе данных на момент начала анализа изменений.
  /// </summary>
  [Indexable(IndexType.Equality, false)]
  [Comparer(typeof (ServiceObjectAttribute.NewObject), new object[] {typeof (ObjectExistenceStatusComparer)})]
  public ObjectExistenceStatus ExistenceStatus
  {
    [DebuggerStepThrough] get => this.existenceStatus;
    set
    {
      if (this.existenceStatus == value)
        return;
      this.existenceStatus = value;
      if (this.ExistenceStatusChanged != null)
        this.ExistenceStatusChanged((object) this, EventArgs.Empty);
      if (this.NewObjectChanged == null)
        return;
      this.NewObjectChanged((object) this, EventArgs.Empty);
    }
  }

  /// <summary>Возвращает или задает идентификатор версии объекта.</summary>
  [Indexable]
  public long ObjectId
  {
    [DebuggerStepThrough] get => this.objectId;
    [DebuggerStepThrough] set
    {
      if (this.objectId == value)
        return;
      this.objectId = value;
      if (this.ObjectIdChanged == null)
        return;
      this.ObjectIdChanged((object) this, EventArgs.Empty);
    }
  }

  /// <summary>Возвращает или задает идентификатор типа объекта.</summary>
  [Indexable(IndexType.Auto, false)]
  public int ObjectType
  {
    [DebuggerStepThrough] get => this.objectType;
    [DebuggerStepThrough] set
    {
      if (this.objectType == value)
        return;
      this.objectType = value;
      if (this.ObjectTypeChanged == null)
        return;
      this.ObjectTypeChanged((object) this, EventArgs.Empty);
    }
  }

  /// <summary>
  /// Возвращает или задает признак, что тип документа выбран автоматически и требует проверки правильности от пользователя.
  /// </summary>
  public bool RequireTypeCheck
  {
    [DebuggerStepThrough] get => this.requireTypeCheck;
    [DebuggerStepThrough] set => this.requireTypeCheck = value;
  }

  public event EventHandler NewObjectChanged;

  public event EventHandler ExistenceStatusChanged;

  public event EventHandler ObjectIdChanged;

  public event EventHandler ObjectTypeChanged;

  /// <summary>Создает объект.</summary>
  public ObjectSection()
  {
    this.objectId = 0L;
    this.objectType = -1;
  }

  public static SectionEntity FindByObjectId(
    CaptureChangesDatabase db,
    long objectId,
    bool exactMatch)
  {
    if (db == null)
      throw new ArgumentNullException(nameof (db));
    IQueryCondition condition = objectId != 0L ? (IQueryCondition) new BinaryCondition((object) ObjectSection.ObjectIdRef, BinaryOperator.Equal, (object) objectId) : throw new ArgumentException();
    if (!exactMatch)
      condition = (IQueryCondition) new CompoundSetCondition(CompoundSetOperator.Union, new IQueryCondition[2]
      {
        condition,
        (IQueryCondition) new BinaryCondition((object) ObjectSection.ObjectIdRef, BinaryOperator.Equal, (object) -objectId)
      });
    return db.QueryFirst(condition);
  }

  /// <summary>
  /// Позволяет проверить, является ли указанный объект новым для базы данных PDM-системы.
  /// </summary>
  /// <param name="dbItem">Объект базы данных анализатора</param>
  /// <returns>true, если объект является новым</returns>
  public static bool IsNewObject(SectionEntity dbItem)
  {
    if (dbItem == null)
      throw new ArgumentNullException(nameof (dbItem));
    return dbItem.Sections.Get<ObjectSection>().NewObject;
  }

  /// <summary>
  /// Получить идентификатор версии объекта из объекта базы данных анализатора.
  /// </summary>
  /// <param name="dbItem">Объект базы данных анализатора</param>
  /// <returns>Идентификатор версии объекта или исключение при ошибке</returns>
  public static long GetObjectId(SectionEntity dbItem)
  {
    ObjectSection objectSection = dbItem != null ? dbItem.Sections.Get<ObjectSection>() : throw new ArgumentNullException(nameof (dbItem));
    return objectSection.ObjectId != 0L ? objectSection.ObjectId : throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Components_440"));
  }

  /// <summary>
  /// Получить идентификатор типа объекта из объекта базы данных анализатора.
  /// </summary>
  /// <param name="dbItem">Объект базы данных анализатора</param>
  /// <returns>Идентификатор типа объекта или исключение при ошибке</returns>
  public static int GetObjectType(SectionEntity dbItem)
  {
    ObjectSection objectSection = dbItem != null ? dbItem.Sections.Get<ObjectSection>() : throw new ArgumentNullException(nameof (dbItem));
    return objectSection.ObjectType != -1 ? objectSection.ObjectType : throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Components_441"));
  }

  /// <summary>
  /// Пытается получить идентификатор типа объекта из объекта базы данных анализатора. Если он не задан, то метод вернет Intermech.Consts.UnknownObjectTypeId.
  /// </summary>
  /// <param name="dbItem">Объект базы данных анализатора</param>
  /// <returns>Идентификатор типа объекта</returns>
  public static int TryGetObjectType(SectionEntity dbItem)
  {
    ObjectSection objectSection = dbItem != null ? dbItem.Sections.Get<ObjectSection>((ObjectSection) null) : throw new ArgumentNullException(nameof (dbItem));
    return objectSection == null ? -1 : objectSection.ObjectType;
  }
}
