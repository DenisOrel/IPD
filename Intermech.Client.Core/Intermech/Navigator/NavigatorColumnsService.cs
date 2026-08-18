
// Type: Intermech.Navigator.NavigatorColumnsService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;


namespace Intermech.Navigator;

/// <summary>
/// Реализует сервис навигатора, позволяющий его составным частям (закладкам и др.)
/// сохранять свое состояние в поток, а также восстанавливать его из потока.
/// </summary>
public class NavigatorColumnsService : INavigatorColumnsService
{
  /// <summary>Объект для потокобезопасного доступа к службе</summary>
  private object _syncRoot = new object();
  /// <summary>
  /// Словарь, содержащий потоки состояния. Ключем служит имя потока.
  /// </summary>
  private IDictionary _streams;
  /// <summary>
  /// Коллекция настроек вида для различных категорий и типов для текущего пользователя
  /// </summary>
  private Dictionary<NavigatorColumnsKey, NavigatorColumns> _navStreams;
  /// <summary>
  /// Имя потока, в который будет сохранена коллекция настроек видов для различных
  /// категорий и типов - "Navigator.CategoryTypeStreams"
  /// </summary>
  private static readonly string NavigatorColumnsStreamName = "Navigator.CategoryTypeStreams";
  /// <summary>
  /// Сервис глобальной службы уведомлений (для всего IMClient)
  /// </summary>
  private INotificationService _mainNotificationService;

  /// <summary>Создать экземпляр класса</summary>
  public NavigatorColumnsService()
  {
    this._streams = (IDictionary) null;
    this._mainNotificationService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (this._mainNotificationService == null)
      return;
    this._mainNotificationService.Subscribe("ApplicationClosing", new NotificationEventHandler(this.ApplicationClosingEventFired));
  }

  /// <summary>
  /// Создает и возвращает новый пустой поток, который можно использовать для сохранения
  /// состояния. Если поток с таким именем уже существует, то метод вернет null.
  /// </summary>
  /// <param name="name">Имя потока.</param>
  /// <returns>Поток состояния.</returns>
  public Stream Create(string name)
  {
    this.Validate(name);
    this.DownloadStreams();
    if (this._streams.Contains((object) name))
      return (Stream) null;
    Stream stream = (Stream) new MemoryStream();
    this._streams.Add((object) name, (object) stream);
    return stream;
  }

  /// <summary>
  /// Возвращает существующий поток, который можно использовать для восстановления
  /// состояния. Если поток с таким именем не существует, то метод вернет null.
  /// </summary>
  /// <param name="name">Имя потока.</param>
  /// <returns>Поток состояния.</returns>
  public Stream this[string name]
  {
    get
    {
      this.Validate(name);
      this.DownloadStreams();
      Stream stream = (Stream) this._streams[(object) name];
      if (stream != null)
        stream.Position = 0L;
      return stream;
    }
  }

  /// <summary>
  /// Удаляет существующий поток состояния с указанным именем.
  /// </summary>
  /// <param name="name">Имя потока.</param>
  public void Remove(string name)
  {
    this.Validate(name);
    this.DownloadStreams();
    Stream stream = (Stream) this._streams[(object) name];
    if (stream == null)
      return;
    stream.Close();
    this._streams.Remove((object) name);
  }

  /// <summary>Проверяет корректность имени потока.</summary>
  /// <param name="name">Имя потока.</param>
  private void Validate(string name)
  {
    if (name == null)
      throw new ArgumentNullException(sc_4570.ssp_imclient_4571(), LocalizationHolder.rm.GetString("Client.Core_775"));
    if (name == string.Empty)
      throw new ArgumentException(LocalizationHolder.rm.GetString(sc_4570.ssp_imclient_4572()), nameof (name));
  }

  private void DownloadStreams()
  {
    if (this._streams != null)
      return;
    this._streams = (IDictionary) new HybridDictionary();
    this.DownloadFromDatabase();
  }

  private void UploadStreams(object sender, EventArgs e)
  {
    if (this._streams != null)
    {
      this.UploadToDatabase();
      this._streams = (IDictionary) null;
    }
    this.SaveToUserConfig();
  }

  private void DownloadFromDatabase()
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        new BlobProcReader(sessionKeeper.Session.Configurations.GetConfigAttribute("Navigator.StateStreams"), 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
        memoryStream.Position = 0L;
        if (memoryStream.Length <= 0L)
          return;
        using (BinaryReader binaryReader = new BinaryReader((Stream) memoryStream))
        {
          int num1 = binaryReader.ReadInt32();
          for (int index1 = 0; index1 < num1; ++index1)
          {
            string key = binaryReader.ReadString();
            long num2 = binaryReader.ReadInt64();
            Stream stream = (Stream) new MemoryStream();
            for (long index2 = 0; index2 < num2; ++index2)
              stream.WriteByte(binaryReader.ReadByte());
            this._streams.Add((object) key, (object) stream);
          }
        }
      }
    }
  }

  public void UploadToDatabase()
  {
    MemoryStream memoryStream = new MemoryStream();
    BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream);
    try
    {
      binaryWriter.Write(this._streams.Keys.Count);
      foreach (DictionaryEntry stream1 in this._streams)
      {
        binaryWriter.Write((string) stream1.Key);
        Stream stream2 = (Stream) stream1.Value;
        binaryWriter.Write(stream2.Length);
        stream2.Position = 0L;
        while (stream2.Position < stream2.Length)
          binaryWriter.Write((byte) stream2.ReadByte());
      }
      memoryStream.Position = 0L;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, "Navigator.StateStreams", ArcMethods.ZLibPacked, string.Empty);
        new BlobProcWriter(sessionKeeper.Session.Configurations.GetConfigAttribute("Navigator.StateStreams"), 0, aBlobInformation, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      }
    }
    finally
    {
      binaryWriter.Close();
      memoryStream.Close();
    }
  }

  public event EventHandler<NavigatorColumnsChangedEventArgs> ColumnsChanged;

  /// <summary>Событие "Найти родительскую категорию и тип"</summary>
  public event GetCategoryTypeParentEventHandler OnGetCategoryTypeParentEventHandler;

  /// <summary>
  /// Найти родительские категорию и тип для указанных категории и типа
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  /// <returns>Описание родительских категории и типа или null</returns>
  private GetCategoryTypeParentEventArgs FindParentCategoryType(
    int category,
    int type,
    string suffix)
  {
    if (this.OnGetCategoryTypeParentEventHandler == null)
      return (GetCategoryTypeParentEventArgs) null;
    GetCategoryTypeParentEventArgs e = new GetCategoryTypeParentEventArgs(category, type, suffix);
    foreach (GetCategoryTypeParentEventHandler invocation in this.OnGetCategoryTypeParentEventHandler.GetInvocationList())
    {
      invocation((object) this, e);
      if (e.Processed)
        return e;
    }
    return (GetCategoryTypeParentEventArgs) null;
  }

  /// <summary>Полностью очистить все настройки отображения</summary>
  public void Reset()
  {
    lock (this._syncRoot)
    {
      this._navStreams = this._navStreams ?? new Dictionary<NavigatorColumnsKey, NavigatorColumns>();
      this._navStreams.Clear();
      this.SetDefaultColumns();
    }
  }

  /// <summary>Создать (перезаписать) настройки вида</summary>
  /// <param name="columns">Новые настройки вида</param>
  /// <returns>true - настройки вида были успешно добавлены в словарик</returns>
  public bool CreateNavigatorColumns(NavigatorColumns columns)
  {
    if (columns == null)
      return false;
    NavigatorColumnsKey navigatorColumnsKey = new NavigatorColumnsKey((object) columns);
    bool flag = false;
    lock (this._syncRoot)
    {
      columns.Inherited = false;
      this._navStreams = this._navStreams ?? new Dictionary<NavigatorColumnsKey, NavigatorColumns>();
      if (this._navStreams.ContainsKey(navigatorColumnsKey))
      {
        NavigatorColumns navStream = this._navStreams[navigatorColumnsKey];
        if (navStream != null && (navStream == null || navStream.Columns != null) && columns.Columns != null)
        {
          if (navStream != null)
          {
            if (navStream.Columns != null)
            {
              if (columns.Columns != null)
              {
                if (navStream.Equals((object) columns))
                  goto label_14;
              }
              else
                goto label_14;
            }
            else
              goto label_14;
          }
          else
            goto label_14;
        }
        this._navStreams[navigatorColumnsKey] = columns;
        flag = true;
      }
      else
      {
        this._navStreams[navigatorColumnsKey] = columns;
        flag = true;
      }
    }
label_14:
    if (flag)
      this.OnColumnsChanged(navigatorColumnsKey);
    return true;
  }

  /// <summary>Создать (перезаписать) настройки вида</summary>
  /// <param name="columns">Новые настройки вида</param>
  /// <param name="navStreams">Словарь, в котором хранятся настройки видов</param>
  /// <returns>true - настройки вида были успешно добавлены в словарик</returns>
  public bool CreateNavigatorColumns(
    NavigatorColumns columns,
    Dictionary<NavigatorColumnsKey, NavigatorColumns> navStreams)
  {
    if (columns == null || navStreams == null)
      return false;
    NavigatorColumnsKey navigatorColumnsKey = new NavigatorColumnsKey((object) columns);
    lock (navStreams)
    {
      columns.Inherited = false;
      navStreams[navigatorColumnsKey] = columns;
    }
    this.OnColumnsChanged(navigatorColumnsKey);
    return true;
  }

  /// <summary>
  /// Создать (перезаписать) настройки вида для указанной категории
  /// </summary>
  /// <param name="category">Категория</param>
  /// <returns>Настройки вида для указанной категории</returns>
  public NavigatorColumns CreateNavigatorColumns(int category)
  {
    return this.CreateNavigatorColumns(category, 0, string.Empty);
  }

  /// <summary>
  /// Создать (перезаписать) настройки вида для указанных категории и типа
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <returns>Настройки вида для указанных категории и типа</returns>
  public NavigatorColumns CreateNavigatorColumns(int category, int type)
  {
    return this.CreateNavigatorColumns(category, type, string.Empty);
  }

  /// <summary>
  /// Создать (перезаписать) настройки вида для указанных категории, типа и дополнительного имени
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  /// <returns>Настройки вида для указанных категории, типа и дополнительного имени</returns>
  public NavigatorColumns CreateNavigatorColumns(int category, int type, string suffix)
  {
    if (this._navStreams == null)
      this.LoadFromUserConfig();
    NavigatorColumnsKey navigatorColumnsKey = new NavigatorColumnsKey(category, type, suffix);
    NavigatorColumns navigatorColumns = new NavigatorColumns(category, type, suffix);
    lock (this._syncRoot)
      this._navStreams[navigatorColumnsKey] = navigatorColumns;
    this.OnColumnsChanged(navigatorColumnsKey);
    return navigatorColumns;
  }

  /// <summary>
  /// Создать (перезаписать) настройки вида для указанных категории, типа и дополнительного имени
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  /// <param name="navStreams"></param>
  /// <returns>Настройки вида для указанных категории, типа и дополнительного имени</returns>
  public NavigatorColumns CreateNavigatorColumns(
    int category,
    int type,
    string suffix,
    Dictionary<NavigatorColumnsKey, NavigatorColumns> navStreams)
  {
    if (navStreams == null)
      return (NavigatorColumns) null;
    NavigatorColumnsKey navigatorColumnsKey = new NavigatorColumnsKey(category, type, suffix);
    NavigatorColumns navigatorColumns = new NavigatorColumns(category, type, suffix);
    lock (navStreams)
      navStreams[navigatorColumnsKey] = navigatorColumns;
    this.OnColumnsChanged(navigatorColumnsKey);
    return navigatorColumns;
  }

  /// <summary>
  /// Получить настройки вида для указанной категории. Если поток не
  /// существует, будет возвращен null
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="useInheritance">Использовать наследование схем</param>
  /// <returns>Настройки вида для указанной категории или null</returns>
  public NavigatorColumns GetNavigatorColumns(int category, bool useInheritance)
  {
    return this.GetNavigatorColumns(category, 0, string.Empty, useInheritance);
  }

  /// <summary>
  /// Получить настройки вида для указанных категории и типа. Если поток не
  /// существует, будет возвращен null
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="useInheritance">Использовать наследование схем</param>
  /// <returns>Настройки вида для указанных категории и типа, или null</returns>
  public NavigatorColumns GetNavigatorColumns(int category, int type, bool useInheritance)
  {
    return this.GetNavigatorColumns(category, type, string.Empty, useInheritance);
  }

  /// <summary>
  /// Получить настройки вида для указанных категории, типа и дополнительного имени. Если поток не
  /// существует, будет возвращен null
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  /// <param name="useInheritance">Использовать наследование схем</param>
  /// <returns>Настройки вида для указанных категории, типа и дополнительного имени, или null</returns>
  private NavigatorColumns InternalGetNavigatorColumns(
    int category,
    int type,
    string suffix,
    bool useInheritance)
  {
    if (this._navStreams == null)
      this.LoadFromUserConfig();
    lock (this._syncRoot)
    {
      NavigatorColumnsKey key = new NavigatorColumnsKey(category, type, suffix);
      if (this._navStreams.ContainsKey(key))
        return this._navStreams[key].Clone() as NavigatorColumns;
    }
    if (!MetaDataHelper.IsObjectTypeChildOf(type, MetaDataHelper.GetObjectTypeID("cad0011e-306c-11d8-b4e9-00304f19f545")))
    {
      NodeColumnCollection columnCollection = ((ICurrentUserAndRole) ServicesManager.GetService(typeof (ICurrentUserAndRole))).DefaultColumnPack[new NavigatorColumnsKey(category, type, suffix == string.Empty ? (string) null : suffix)];
      if (columnCollection != null && columnCollection.Count > 0)
        return new NavigatorColumns(category, type, suffix)
        {
          Columns = columnCollection
        };
    }
    if (!useInheritance)
      return (NavigatorColumns) null;
    if ((category == 4 || category == Consts.CategoryObjectTypes) && type != -1 && suffix == "TreeView")
      return this.InternalGetNavigatorColumns(category, MetaDataHelper.GetObjectTypeParentID(type), "TreeView", useInheritance);
    if (suffix == "TreeView" && type != 0 && type != -1)
      return this.InternalGetNavigatorColumns(category, 0, "TreeView", useInheritance);
    if (suffix == "TreeView")
      return this.InternalGetNavigatorColumns(0, 0, "", useInheritance);
    if (!string.IsNullOrEmpty(suffix))
      return this.InternalGetNavigatorColumns(category, type, string.Empty, useInheritance);
    GetCategoryTypeParentEventArgs parentCategoryType = this.FindParentCategoryType(category, type, suffix);
    return parentCategoryType == null ? (NavigatorColumns) null : this.InternalGetNavigatorColumns(parentCategoryType.ParentCategory, parentCategoryType.ParentType, parentCategoryType.ParentSuffix, useInheritance);
  }

  /// <summary>
  /// Получить настройки вида для указанных категории, типа и дополнительного имени. Если поток не
  /// существует, будет возвращен null
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  /// <param name="useInheritance">Использовать наследование схем</param>
  /// <param name="navStreams">Словарь, в котором хранятся настройки видов</param>
  /// <returns>Настройки вида для указанных категории, типа и дополнительного имени, или null</returns>
  private NavigatorColumns InternalGetNavigatorColumns(
    int category,
    int type,
    string suffix,
    bool useInheritance,
    Dictionary<NavigatorColumnsKey, NavigatorColumns> navStreams)
  {
    if (navStreams == null)
      return (NavigatorColumns) null;
    lock (navStreams)
    {
      NavigatorColumnsKey key = new NavigatorColumnsKey(category, type, suffix);
      if (navStreams.ContainsKey(key))
        return navStreams[key];
    }
    if (!MetaDataHelper.IsObjectTypeChildOf(type, MetaDataHelper.GetObjectTypeID("cad0011e-306c-11d8-b4e9-00304f19f545")))
    {
      NodeColumnCollection columnCollection = ((ICurrentUserAndRole) ServicesManager.GetService(typeof (ICurrentUserAndRole))).DefaultColumnPack[new NavigatorColumnsKey(category, type, suffix == string.Empty ? (string) null : suffix)];
      if (columnCollection != null && columnCollection.Count > 0)
        return new NavigatorColumns(category, type, suffix)
        {
          Columns = columnCollection
        };
    }
    if (!useInheritance)
      return (NavigatorColumns) null;
    if (!string.IsNullOrEmpty(suffix))
      return this.InternalGetNavigatorColumns(category, type, string.Empty, useInheritance);
    GetCategoryTypeParentEventArgs parentCategoryType = this.FindParentCategoryType(category, type, suffix);
    return parentCategoryType == null ? (NavigatorColumns) null : this.InternalGetNavigatorColumns(parentCategoryType.ParentCategory, parentCategoryType.ParentType, parentCategoryType.ParentSuffix, useInheritance, navStreams);
  }

  /// <summary>
  /// Получить настройки вида для указанных категории, типа и дополнительного имени. Если поток не
  /// существует, будет возвращен null
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  /// <param name="useInheritance">Использовать наследование схем</param>
  /// <returns>Настройки вида для указанных категории, типа и дополнительного имени, или null</returns>
  public NavigatorColumns GetNavigatorColumns(
    int category,
    int type,
    string suffix,
    bool useInheritance)
  {
    NavigatorColumns navigatorColumns1 = this.InternalGetNavigatorColumns(category, type, suffix, useInheritance);
    ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    NavigatorColumns navigatorColumns2 = navigatorColumns1;
    if (service.BlockedCompositions && category == 4 && string.IsNullOrEmpty(suffix) && navigatorColumns1 != null && type != navigatorColumns1.Type)
      navigatorColumns1 = (NavigatorColumns) null;
    lock (this._syncRoot)
    {
      if (navigatorColumns1 != null & useInheritance)
      {
        if (navigatorColumns1.Category == category && navigatorColumns1.Type == type)
        {
          if (!(navigatorColumns1.Suffix != suffix))
            goto label_10;
        }
        NavigatorColumns navigatorColumns3 = navigatorColumns1.Clone() as NavigatorColumns;
        navigatorColumns3.Category = category;
        navigatorColumns3.Type = type;
        navigatorColumns3.Suffix = suffix;
        navigatorColumns3.Inherited = true;
        return navigatorColumns3;
      }
    }
label_10:
    if (navigatorColumns1 == null)
    {
      navigatorColumns1 = this.GetNavigatorColumns(category, type, suffix, useInheritance, service.RoleNavStreams) ?? navigatorColumns2;
      if (navigatorColumns1 != null)
      {
        navigatorColumns1 = navigatorColumns1.Clone() as NavigatorColumns;
        navigatorColumns1.Inherited = true;
      }
    }
    return navigatorColumns1;
  }

  /// <summary>
  /// Получить настройки вида для указанных категории, типа и дополнительного имени. Если поток не
  /// существует, будет возвращен null
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  /// <param name="useInheritance">Использовать наследование схем</param>
  /// <param name="navStreams">Словарь, в котором хранятся настройки видов</param>
  /// <returns>Настройки вида для указанных категории, типа и дополнительного имени, или null</returns>
  public NavigatorColumns GetNavigatorColumns(
    int category,
    int type,
    string suffix,
    bool useInheritance,
    Dictionary<NavigatorColumnsKey, NavigatorColumns> navStreams)
  {
    if (navStreams == null)
      return (NavigatorColumns) null;
    NavigatorColumns navigatorColumns1 = this.InternalGetNavigatorColumns(category, type, suffix, useInheritance, navStreams);
    lock (navStreams)
    {
      if (navigatorColumns1 != null & useInheritance)
      {
        if (navigatorColumns1.Category == category && navigatorColumns1.Type == type)
        {
          if (!(navigatorColumns1.Suffix != suffix))
            goto label_10;
        }
        NavigatorColumns navigatorColumns2 = navigatorColumns1.Clone() as NavigatorColumns;
        navigatorColumns2.Category = category;
        navigatorColumns2.Type = type;
        navigatorColumns2.Suffix = suffix;
        navigatorColumns2.Inherited = true;
        return navigatorColumns2;
      }
    }
label_10:
    return navigatorColumns1;
  }

  /// <summary>Удалить настройки вида для указанной категории</summary>
  /// <param name="category">Категория</param>
  /// <returns>true - настройки вида для указанной категории удалён</returns>
  public bool RemoveNavigatorColumns(int category)
  {
    return this.RemoveNavigatorColumns(category, 0, string.Empty);
  }

  /// <summary>Удалить настройки вида для указанных категории и типа</summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <returns>true - настройки вида для указанных категории и типа удалён</returns>
  public bool RemoveNavigatorColumns(int category, int type)
  {
    return this.RemoveNavigatorColumns(category, type, string.Empty);
  }

  /// <summary>
  /// Удалить настройки вида для указанных категории, типа и дополнительного имени
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  /// <returns>true - настройки вида для указанных категории, типа и дополнительного имени удалён</returns>
  public bool RemoveNavigatorColumns(int category, int type, string suffix)
  {
    bool flag = false;
    NavigatorColumnsKey navigatorColumnsKey = new NavigatorColumnsKey(category, type, suffix);
    lock (this._syncRoot)
    {
      this._navStreams = this._navStreams ?? new Dictionary<NavigatorColumnsKey, NavigatorColumns>();
      if (this._navStreams.ContainsKey(navigatorColumnsKey))
      {
        this._navStreams.Remove(navigatorColumnsKey);
        flag = true;
      }
    }
    if (flag)
      this.OnColumnsChanged(navigatorColumnsKey);
    return flag;
  }

  /// <summary>
  /// Удалить настройки вида для указанных категории, типа и дополнительного имени
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="type">Тип</param>
  /// <param name="suffix">Дополнительное имя</param>
  /// <param name="navStreams">Словарь, в котором хранятся настройки видов</param>
  /// <returns>true - настройки вида для указанных категории, типа и дополнительного имени удалён</returns>
  public bool RemoveNavigatorColumns(
    int category,
    int type,
    string suffix,
    Dictionary<NavigatorColumnsKey, NavigatorColumns> navStreams)
  {
    if (navStreams == null)
      return false;
    bool flag = false;
    NavigatorColumnsKey navigatorColumnsKey = new NavigatorColumnsKey(category, type, suffix);
    lock (navStreams)
    {
      if (navStreams.ContainsKey(navigatorColumnsKey))
      {
        navStreams.Remove(navigatorColumnsKey);
        flag = true;
      }
    }
    if (flag)
      this.OnColumnsChanged(navigatorColumnsKey);
    return flag;
  }

  /// <summary>
  /// Загрузить настройки из конфигурации текущего пользователя
  /// </summary>
  public void LoadFromUserConfig()
  {
    this.InternalLoadNavColumns((Stream) this.InternalLoadFromUserConfiguration());
    this.SetDefaultColumns();
  }

  /// <summary>
  /// Сохранить настройки в конфигурацию текущего пользователя
  /// </summary>
  public void SaveToUserConfig()
  {
    lock (this._syncRoot)
    {
      this._navStreams = this._navStreams ?? new Dictionary<NavigatorColumnsKey, NavigatorColumns>();
      this.InternalSaveToUserConfiguration((Stream) this.InternalGetNavColumnsStream(this._navStreams), this._navStreams != null ? (long) this._navStreams.Count : 0L);
    }
  }

  /// <summary>
  /// Загрузить настройки видов Навигатора из атрибута указанного объекта
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <returns>Настройки видов Навигатора или пустой словарик</returns>
  public Dictionary<NavigatorColumnsKey, NavigatorColumns> LoadFromObject(
    long objectID,
    int attributeID)
  {
    return this.InternalLoadNavColumnsAdv((Stream) this.InternalLoadFromObject(objectID, attributeID));
  }

  /// <summary>
  /// Сохранить настройки видов Навигатора в атрибут указанного объекта
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <param name="navStreams">Настройки видов Навигатора</param>
  /// <returns>true - сохранение выполнено успешно</returns>
  public bool SaveToObject(
    long objectID,
    int attributeID,
    Dictionary<NavigatorColumnsKey, NavigatorColumns> navStreams)
  {
    return this.InternalSaveToObject((Stream) this.InternalGetNavColumnsStream(navStreams), objectID, attributeID, navStreams != null ? (long) navStreams.Count : 0L);
  }

  /// <summary>Загрузить настройки из указанного файла</summary>
  /// <param name="fileName">Файл, в котором находятся настройки</param>
  /// <returns>true - настройки успешно загружены</returns>
  public bool LoadFromFile(string fileName)
  {
    if (!new FileInfo(fileName).Exists)
      return false;
    try
    {
      FileStream ms = new FileStream(fileName, FileMode.Open);
      try
      {
        Dictionary<NavigatorColumnsKey, NavigatorColumns> dictionary = this.InternalLoadNavColumnsAdv((Stream) ms);
        if (dictionary == null)
          return false;
        lock (this._syncRoot)
          this._navStreams = dictionary;
        return true;
      }
      finally
      {
        ms.Close();
        ms.Dispose();
      }
    }
    catch
    {
      return false;
    }
  }

  /// <summary>Сохранить настройки в указанный файл</summary>
  /// <param name="fileName">Файл, в который будут записаны настройки</param>
  /// <returns>true - настройки успешно сохранены</returns>
  public bool SaveToFile(string fileName)
  {
    try
    {
      FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
      try
      {
        MemoryStream memoryStream = (MemoryStream) null;
        lock (this._syncRoot)
          memoryStream = this.InternalGetNavColumnsStream(this._navStreams);
        if (memoryStream == null)
          return false;
        memoryStream.Seek(0L, SeekOrigin.Begin);
        memoryStream.WriteTo((Stream) fileStream);
        return true;
      }
      finally
      {
        fileStream.Close();
        fileStream.Dispose();
      }
    }
    catch
    {
      return false;
    }
  }

  /// <summary>
  /// Загрузить настройки видов Навигатора из атрибута указанного объекта
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <returns>Настройки видов Навигатора в виде потока</returns>
  private MemoryStream InternalLoadFromObject(long objectID, int attributeID)
  {
    MemoryStream aDestStream = new MemoryStream();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(objectID, attributeID);
        if (objectAttributeById == null)
          return aDestStream;
        new BlobProcReader(objectAttributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
      }
    }
    catch
    {
    }
    aDestStream.Seek(0L, SeekOrigin.Begin);
    return aDestStream;
  }

  /// <summary>
  /// Считать настройки видов Навигатора из конфигурации текущего пользователя
  /// </summary>
  /// <returns>Настройки видов Навигатора из конфигурации текущего пользователя</returns>
  private MemoryStream InternalLoadFromUserConfiguration()
  {
    MemoryStream aDestStream = new MemoryStream();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        new BlobProcReader(sessionKeeper.Session.Configurations.GetConfigAttribute(NavigatorColumnsService.NavigatorColumnsStreamName), 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
    }
    catch
    {
    }
    aDestStream.Seek(0L, SeekOrigin.Begin);
    return aDestStream;
  }

  /// <summary>Загрузить настройки из указанного потока</summary>
  /// <param name="ms">Поток с настройками</param>
  private void InternalLoadNavColumns(Stream ms)
  {
    lock (this._syncRoot)
    {
      Dictionary<NavigatorColumnsKey, NavigatorColumns> dictionary = this.InternalLoadNavColumnsAdv(ms);
      if (dictionary != null)
        this._navStreams = dictionary;
      else
        this._navStreams = this._navStreams ?? new Dictionary<NavigatorColumnsKey, NavigatorColumns>();
    }
  }

  /// <summary>Загрузить настройки из указанного потока</summary>
  /// <param name="ms">Поток с настройками</param>
  /// <returns>Настройки видов Навигатора или null при ошибке</returns>
  private Dictionary<NavigatorColumnsKey, NavigatorColumns> InternalLoadNavColumnsAdv(Stream ms)
  {
    ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    Dictionary<NavigatorColumnsKey, NavigatorColumns> dictionary = new Dictionary<NavigatorColumnsKey, NavigatorColumns>();
    if (ms != null)
    {
      if (ms.Length > 4L)
      {
        try
        {
          using (BinaryReader binaryReader = new BinaryReader(ms))
          {
            int num1 = binaryReader.ReadInt32();
            for (int index1 = 0; index1 < num1; ++index1)
            {
              long num2 = binaryReader.ReadInt64();
              using (Stream stream = (Stream) new MemoryStream())
              {
                for (long index2 = 0; index2 < num2; ++index2)
                  stream.WriteByte(binaryReader.ReadByte());
                stream.Seek(0L, SeekOrigin.Begin);
                NavigatorColumns source = new NavigatorColumns();
                source.DeserializeFromStream(stream);
                if (!source.Empty)
                {
                  if (service.BlockedCompositions)
                  {
                    if (service.BlockedCompositions)
                    {
                      if (source.Category == 1 && source.Category == 4)
                      {
                        if (string.IsNullOrEmpty(source.Suffix))
                          continue;
                      }
                    }
                    else
                      continue;
                  }
                  dictionary[new NavigatorColumnsKey((object) source)] = source;
                }
              }
            }
          }
        }
        catch
        {
          return (Dictionary<NavigatorColumnsKey, NavigatorColumns>) null;
        }
      }
    }
    return dictionary;
  }

  /// <summary>Сохранить настройки в поток</summary>
  /// <param name="navStreams">Настройки</param>
  private MemoryStream InternalGetNavColumnsStream(
    Dictionary<NavigatorColumnsKey, NavigatorColumns> navStreams)
  {
    ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    MemoryStream navColumnsStream = new MemoryStream();
    if (navStreams == null)
      return navColumnsStream;
    MemoryStream output = new MemoryStream();
    BinaryWriter binaryWriter = new BinaryWriter((Stream) output);
    try
    {
      int count = navStreams.Count;
      foreach (KeyValuePair<NavigatorColumnsKey, NavigatorColumns> navStream in navStreams)
      {
        if (service.BlockedCompositions && (navStream.Value.Category == 1 || navStream.Value.Category == 4) && string.IsNullOrEmpty(navStream.Value.Suffix))
          --count;
      }
      binaryWriter.Write(count);
      foreach (KeyValuePair<NavigatorColumnsKey, NavigatorColumns> navStream in navStreams)
      {
        if (!service.BlockedCompositions || navStream.Value.Category != 1 && navStream.Value.Category != 4 || !string.IsNullOrEmpty(navStream.Value.Suffix))
        {
          MemoryStream stream = navStream.Value.SerializeToStream(ZLibCompressLevels.LevelMax);
          if (stream != null && stream.Length != 0L)
          {
            binaryWriter.Write(stream.Length);
            while (stream.Position < stream.Length)
              binaryWriter.Write((byte) stream.ReadByte());
            stream.Close();
          }
        }
      }
      output.Position = 0L;
      output.WriteTo((Stream) navColumnsStream);
    }
    finally
    {
      binaryWriter.Close();
      navColumnsStream.Seek(0L, SeekOrigin.Begin);
    }
    return navColumnsStream;
  }

  /// <summary>Сохранить указанный поток в атрибут объекта</summary>
  /// <param name="ms">Поток с настройками</param>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <param name="count">Количество настроек видов Навигатора</param>
  /// <returns>true - информация успешно записана</returns>
  private bool InternalSaveToObject(Stream ms, long objectID, int attributeID, long count)
  {
    if (ms == null)
      return false;
    ms.Seek(0L, SeekOrigin.Begin);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeById = sessionKeeper.Session.GetObject(objectID, false)?.GetAttributeByID(attributeID);
      if (attributeById == null)
        return false;
      try
      {
        BlobInformation aBlobInformation = new BlobInformation(ms.Length, ms.Length, DateTime.Now, NavigatorColumnsService.NavigatorColumnsStreamName, ArcMethods.ZLibPacked, string.Format(LocalizationHolder.rm.GetString("Client.Core_1531"), (object) count));
        new BlobProcWriter(attributeById, 0, aBlobInformation, ms, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      }
      catch
      {
        return false;
      }
    }
    return true;
  }

  /// <summary>Сохранить указанный поток в конфигурацию пользователя</summary>
  /// <param name="ms">Поток с настройками</param>
  /// <param name="count">Количество настроек</param>
  /// <returns>true - информация успешно записана</returns>
  private bool InternalSaveToUserConfiguration(Stream ms, long count)
  {
    if (ms == null)
      return false;
    ms.Seek(0L, SeekOrigin.Begin);
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        BlobInformation aBlobInformation = new BlobInformation(ms.Length, ms.Length, DateTime.Now, NavigatorColumnsService.NavigatorColumnsStreamName, ArcMethods.ZLibPacked, string.Format(LocalizationHolder.rm.GetString("Client.Core_1531"), (object) count));
        new BlobProcWriter(sessionKeeper.Session.Configurations.GetConfigAttribute(NavigatorColumnsService.NavigatorColumnsStreamName), 0, aBlobInformation, ms, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      }
    }
    catch
    {
      return false;
    }
    return true;
  }

  /// <summary>Обработчик события "Закрывается IPS"</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  public void ApplicationClosingEventFired(object sender, NotificationEventArgs e)
  {
    if (e.EventName != "ApplicationClosing")
      return;
    if (this._streams != null)
      this.UploadToDatabase();
    this.SaveToUserConfig();
  }

  private void SetDefaultColumns()
  {
    NavigatorColumnsKey key1 = new NavigatorColumnsKey(Consts.CategoryGlobalNode, 0, "");
    if (!this._navStreams.ContainsKey(key1))
      this._navStreams[key1] = new NavigatorColumns(key1.Category, key1.Type, key1.Suffix)
      {
        Columns = Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending)
      };
    NavigatorColumnsKey key2 = new NavigatorColumnsKey(Consts.CategoryObjectTypes, -1, "TreeView");
    if (!this._navStreams.ContainsKey(key2))
      this._navStreams[key2] = new NavigatorColumns(Consts.CategoryObjectTypes, -1, "TreeView")
      {
        Columns = Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending)
      };
    NavigatorColumnsKey key3 = new NavigatorColumnsKey(4, -1, "TreeView");
    if (!this._navStreams.ContainsKey(key3))
      this._navStreams[key3] = new NavigatorColumns(Consts.CategoryObjectTypes, -1, "TreeView")
      {
        Columns = Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending)
      };
    NavigatorColumnsKey key4 = new NavigatorColumnsKey(0, 0, "");
    if (this._navStreams.ContainsKey(key4))
      return;
    this._navStreams[key4] = new NavigatorColumns(key4.Category, key4.Type, key4.Suffix)
    {
      Columns = Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending)
    };
  }

  private void OnColumnsChanged(NavigatorColumnsKey columnsKey)
  {
    EventHandler<NavigatorColumnsChangedEventArgs> columnsChanged = this.ColumnsChanged;
    if (columnsChanged == null)
      return;
    columnsChanged((object) this, new NavigatorColumnsChangedEventArgs(columnsKey));
  }
}
