
// Type: Intermech.Navigator.CreateObjByTypeMRU
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Navigator;

/// <summary>
/// Класс, помогающий управлять списком команд по созданию типов объектов в подменю "Файл\Создать"
/// </summary>
[Serializable]
public class CreateObjByTypeMRU : List<IMRUItem>, ICreateObjByTypeMRU
{
  /// <summary>Guid настроек</summary>
  private const string UserSettingsGuid = "{144404C4-2028-41EF-9208-58236E68D6C1}";
  /// <summary>Максимальная ёмкость списка элементов</summary>
  protected int _maxCapacity = 25;

  /// <summary>Создать коллекцию</summary>
  public CreateObjByTypeMRU()
  {
    if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service))
      return;
    service.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(this.NewObjectCreated);
  }

  /// <summary>Создать коллекцию</summary>
  /// <param name="capacity">Ёмкость коллекции</param>
  public CreateObjByTypeMRU(int capacity)
    : base(capacity)
  {
    if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service))
      return;
    service.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(this.NewObjectCreated);
  }

  /// <summary>Максимальная ёмкость списка элементов</summary>
  public int MaxCapacity
  {
    get => this._maxCapacity;
    set
    {
      if (value <= 0)
        return;
      this._maxCapacity = value;
      if (this.Count <= this._maxCapacity)
        return;
      for (int index = this.Count - 1; index >= this._maxCapacity; --index)
        this.RemoveAt(index);
    }
  }

  /// <summary>
  /// Добавить элемент в коллекцию (с учётом значения MaxCapacity)
  /// </summary>
  /// <param name="Caption">Текстовое пояснение элемента</param>
  /// <param name="Value">Основное значение элемента</param>
  /// <param name="Tag">Дополнительное значение элемента</param>
  /// <returns>Вновь добавленный элемент</returns>
  public IMRUItem Add(string Caption, object Value, object Tag)
  {
    MRUItem mruItem = new MRUItem(Caption, Value, Tag);
    this.Add((IMRUItem) mruItem);
    return (IMRUItem) mruItem;
  }

  /// <summary>
  /// Добавить элемент в коллекцию (с учётом значения MaxCapacity)
  /// </summary>
  /// <param name="value">Элемент, который требуется добавить</param>
  /// <returns>Вновь добавленный элемент</returns>
  public IMRUItem Add(IMRUItem value)
  {
    if (value == null)
      return (IMRUItem) null;
    int index = this.IndexOf((object) value);
    if (index >= 0)
    {
      value = this[index];
      this.RemoveAt(index);
      ++value.HintCount;
      value.LastAccess = DateTime.UtcNow;
    }
    this.Insert(0, value);
    this.MaxCapacity = this.MaxCapacity;
    return value;
  }

  /// <summary>
  /// Отыскать порядковый номер указанного значения (IMRUItem.Value, не равно null!!!) в коллекции
  /// </summary>
  /// <param name="value">Значение (IMRUItem.Value) (не равно null!!!)</param>
  /// <returns>-1, если элемент не найден</returns>
  public int IndexOf(object value)
  {
    for (int index = 0; index < this.Count; ++index)
    {
      if (this[index].Value != null && this[index].Value.Equals(value))
        return index;
    }
    return -1;
  }

  /// <summary>Полное присваивание другого списка команд</summary>
  /// <param name="source">Источник</param>
  public virtual void Assign(CreateObjByTypeMRU source)
  {
    this.Clear();
    if (source == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < source.Count; ++index)
      {
        IMRUItem mruItem = source[index];
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType((int) mruItem.Value, false);
        if (objectType != null && (objectType.Options & ObjectTypeOptions.DisableManualCreate) != ObjectTypeOptions.DisableManualCreate)
        {
          mruItem.Caption = objectType.ObjectInstanceName;
          if (this.IndexOf(mruItem) < 0)
            base.Add(mruItem);
        }
      }
    }
    this.MaxCapacity = this.MaxCapacity;
  }

  /// <summary>Загрузить список MRU из настроек пользователя</summary>
  /// <param name="UserID">Идентификатор пользователя</param>
  public virtual void LoadMRU(long UserID)
  {
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
      return;
    byte[] buffer = customService[UserID, (object) "{144404C4-2028-41EF-9208-58236E68D6C1}"] as byte[];
    CreateObjByTypeMRU source = (CreateObjByTypeMRU) null;
    if (buffer != null)
    {
      try
      {
        using (MemoryStream serializationStream = new MemoryStream(buffer))
        {
          try
          {
            source = new BinaryFormatter().Deserialize((Stream) serializationStream) as CreateObjByTypeMRU;
          }
          catch
          {
            source = (CreateObjByTypeMRU) null;
          }
        }
      }
      catch
      {
        source = (CreateObjByTypeMRU) null;
      }
    }
    this.Assign(source);
  }

  /// <summary>
  /// Сохранить список MRU в настройки указанного пользователя
  /// </summary>
  /// <param name="UserID">Идентификатор пользователя</param>
  public virtual void SaveMRU(long UserID)
  {
    this.MaxCapacity = this.MaxCapacity;
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
      return;
    using (MemoryStream serializationStream = new MemoryStream())
    {
      try
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) this);
        customService[UserID, (object) "{144404C4-2028-41EF-9208-58236E68D6C1}"] = (object) serializationStream.ToArray();
      }
      catch
      {
      }
    }
  }

  /// <summary>Создан новый экземпляр объекта</summary>
  /// <param name="objectID">ID вновь созданного экземпляра объекта</param>
  public void NewObjectCreated(object sender, AfterObjectCreatedEventArgs e)
  {
    ICreateObjectButton service = ServicesManager.GetService(typeof (ICreateObjectButton)) as ICreateObjectButton;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(e.ObjectTypeID);
    RecentObjectsNode.MRUObjects.Add(e.ObjectID, ObjectAction.Create, DateTime.UtcNow);
    if (objectType == null || objectType.Options.HasFlag((Enum) ObjectTypeOptions.DisableManualCreate))
      return;
    MRUItem MRUItem = new MRUItem(objectType.ObjectName, (object) e.ObjectTypeID, (object) 0);
    this.Remove((IMRUItem) MRUItem);
    this.Insert(0, (IMRUItem) MRUItem);
    service.BtnNewObjTypeIcon(e.ObjectTypeID, (IMRUItem) MRUItem);
    this.MaxCapacity = this.MaxCapacity;
  }

  void ICreateObjByTypeMRU.Sort() => this.Sort();

  void ICreateObjByTypeMRU.Sort(IComparer<IMRUItem> comparer) => this.Sort(comparer);

  IMRUItem[] ICreateObjByTypeMRU.ToArray() => this.ToArray();
}
