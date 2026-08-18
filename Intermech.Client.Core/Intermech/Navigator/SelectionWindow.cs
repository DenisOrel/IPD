
// Type: Intermech.Navigator.SelectionWindow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>
/// Окно, позволяющее выбирать информационные объекты IPS, связи и прочие объекты системы.
/// Экземпляры данного окна кэшируются системой. В кэше допускается не более 15 таких окон.
/// </summary>
public class SelectionWindow
{
  /// <summary>Максимальное количество кэшируемых форм</summary>
  internal static readonly int CachedFormsMaxCount = 15;
  /// <summary>
  /// Кэш форм (в кэше хранится до CachedFormsMaxCount разнотипных форм)
  /// </summary>
  internal static List<Intermech.Navigator.Controls.SelectionWindow> SelWinCache = new List<Intermech.Navigator.Controls.SelectionWindow>(SelectionWindow.CachedFormsMaxCount);
  private static Dictionary<IDescriptor, Intermech.Navigator.Controls.SelectionWindow.SelectionWindowMemento> _selectionWindowMementoDictionary = new Dictionary<IDescriptor, Intermech.Navigator.Controls.SelectionWindow.SelectionWindowMemento>();
  private const string SelectionWindowSettingsConfigurationName = "SelectionWindowSettings";
  private const string SelectionWindowSettignsPropertyName = "Data";
  /// <summary>Список постоянных анализаторов</summary>
  internal static List<ISelectedItemsAnalyzer> _analyzers = new List<ISelectedItemsAnalyzer>();
  /// <summary>Список временных анализаторов</summary>
  internal static List<ISelectedItemsAnalyzer> _temporaryAnalyzers = new List<ISelectedItemsAnalyzer>();
  /// <summary>Список временных анализаторов</summary>
  internal static List<IToSelectItemsAnalyzer> _temporaryToSelAnalyzers = new List<IToSelectItemsAnalyzer>();
  private static bool _okButtonEnabled;

  public static void RestoreMementos()
  {
    try
    {
      IConfiguration configuration = (ServicesManager.GetService(typeof (IConfigurationManager)) as IConfigurationManager).Open("SelectionWindowSettings");
      if (configuration == null)
        return;
      string property = configuration.GetProperty("Data");
      if (string.IsNullOrEmpty(property))
        return;
      using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(property)))
      {
        if (!(new BinaryFormatter().Deserialize((Stream) serializationStream) is Intermech.Navigator.Controls.SelectionWindow.SelectionWindowMemento[] selectionWindowMementoArray))
          return;
        SelectionWindow._selectionWindowMementoDictionary.Clear();
        for (int index = 0; index < selectionWindowMementoArray.Length; ++index)
        {
          Intermech.Navigator.Controls.SelectionWindow.SelectionWindowMemento selectionWindowMemento = selectionWindowMementoArray[index];
          if (selectionWindowMemento.NavigatorTreeViewFocusedPath != null)
            SelectionWindow._selectionWindowMementoDictionary[selectionWindowMemento.NavigatorTreeViewFocusedPath.Path.RootDescriptor] = selectionWindowMemento;
        }
      }
    }
    catch (Exception ex)
    {
    }
  }

  public static void SaveMementos()
  {
    try
    {
      List<Intermech.Navigator.Controls.SelectionWindow.SelectionWindowMemento> list = SelectionWindow.SelWinCache.Select<Intermech.Navigator.Controls.SelectionWindow, Intermech.Navigator.Controls.SelectionWindow.SelectionWindowMemento>((Func<Intermech.Navigator.Controls.SelectionWindow, Intermech.Navigator.Controls.SelectionWindow.SelectionWindowMemento>) (o => o.GetMemento())).ToList<Intermech.Navigator.Controls.SelectionWindow.SelectionWindowMemento>();
      list.AddRange((IEnumerable<Intermech.Navigator.Controls.SelectionWindow.SelectionWindowMemento>) SelectionWindow._selectionWindowMementoDictionary.Values);
      string str = string.Empty;
      using (MemoryStream serializationStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) list.ToArray());
        str = Convert.ToBase64String(serializationStream.ToArray());
      }
      IConfigurationManager service = ServicesManager.GetService(typeof (IConfigurationManager)) as IConfigurationManager;
      (service.Open("SelectionWindowSettings") ?? service.Create("SelectionWindowSettings")).SetProperty("Data", str);
    }
    catch (Exception ex)
    {
    }
  }

  /// <summary>Удалить из кэша указанную форму</summary>
  /// <param name="win">Кэшированная форма</param>
  internal static void RemoveWin(Intermech.Navigator.Controls.SelectionWindow win)
  {
    if (win == null)
      return;
    lock (SelectionWindow.SelWinCache)
    {
      for (int index = SelectionWindow.SelWinCache.Count - 1; index >= 0; --index)
      {
        if (SelectionWindow.SelWinCache[index] == null)
          SelectionWindow.SelWinCache.RemoveAt(index);
        else if (SelectionWindow.SelWinCache[index] == win)
          SelectionWindow.SelWinCache.RemoveAt(index);
      }
    }
  }

  /// <summary>
  /// Впендюрить указанную форму в кэш, удалив при этом самые старые формы
  /// </summary>
  /// <param name="win">Форма</param>
  internal static void InsertSelWin(Intermech.Navigator.Controls.SelectionWindow win)
  {
    if (win == null)
      return;
    lock (SelectionWindow.SelWinCache)
    {
      if (SelectionWindow.SelWinCache.Count < SelectionWindow.CachedFormsMaxCount)
      {
        SelectionWindow.SelWinCache.Insert(0, win);
      }
      else
      {
        Intermech.Navigator.Controls.SelectionWindow win1 = SelectionWindow.SelWinCache[SelectionWindow.SelWinCache.Count - 1];
        DateTime accessTime = win.accessTime;
        for (int index = 0; index < SelectionWindow.SelWinCache.Count; ++index)
        {
          if (!(SelectionWindow.SelWinCache[index].accessTime >= accessTime) || SelectionWindow.SelWinCache[index].IsDisposed)
          {
            win1 = SelectionWindow.SelWinCache[index];
            accessTime = win1.accessTime;
          }
        }
        SelectionWindow.RemoveWin(win1);
        SelectionWindow.SelWinCache.Insert(0, win);
      }
    }
  }

  /// <summary>Найти в кэше указанную форму</summary>
  /// <param name="rootDescriptor">Дескриптор формы</param>
  /// <param name="options">Опции</param>
  /// <returns>Форма или null</returns>
  internal static Intermech.Navigator.Controls.SelectionWindow GetSelWin(
    IDescriptor rootDescriptor,
    SelectionOptions options)
  {
    if (SelectionWindow.SelWinCache.Count == 0 || rootDescriptor == null)
      return (Intermech.Navigator.Controls.SelectionWindow) null;
    for (int index = 0; index < SelectionWindow.SelWinCache.Count; ++index)
    {
      IDescriptor rootDescriptor1 = SelectionWindow.SelWinCache[index].NavTreeView.RootDescriptor;
      SelectionOptions options1 = SelectionWindow.SelWinCache[index].options;
      if (rootDescriptor1 != null && rootDescriptor.Equals((object) rootDescriptor1) && options1 == options && !SelectionWindow.SelWinCache[index].IsDisposed)
        return SelectionWindow.SelWinCache[index];
    }
    return (Intermech.Navigator.Controls.SelectionWindow) null;
  }

  /// <summary>
  /// Событие генерируется перед отображением формы по выбору объектов
  /// </summary>
  public static event SelectionWindowBeforeShow OnSelectionWindowBeforeShow;

  /// <summary>
  /// Событие генерируется после закрытия формы по выбору объектов
  /// </summary>
  public static event SelectionWindowAfterClose OnSelectionWindowAfterClose;

  /// <summary>Добавить анализатор в указанный список</summary>
  /// <param name="list">Список анализаторов</param>
  /// <param name="analyzer">Анализатор</param>
  /// <returns>true - анализатор был успешно добавлен в список</returns>
  private static bool _addAnalyzer(
    List<ISelectedItemsAnalyzer> list,
    ISelectedItemsAnalyzer analyzer)
  {
    if (list == null || analyzer == null)
      return false;
    for (int index = 0; index < list.Count; ++index)
    {
      if (list[index].Guid == analyzer.Guid)
        return false;
    }
    list.Add(analyzer);
    return true;
  }

  /// <summary>Удалить анализатор из указанного списка</summary>
  /// <param name="list">Список анализаторов</param>
  /// <param name="analyzer">Анализатор</param>
  /// <returns>true - анализатор был успешно удалён из списка</returns>
  private static bool _deleteAnalyzer(
    List<ISelectedItemsAnalyzer> list,
    ISelectedItemsAnalyzer analyzer)
  {
    if (list == null || analyzer == null)
      return false;
    for (int index = 0; index < list.Count; ++index)
    {
      if (list[index].Guid == analyzer.Guid)
      {
        list.RemoveAt(index);
        return true;
      }
    }
    return false;
  }

  /// <summary>
  /// Зарегистрировать анализатор для проверки выделенных элементов
  /// </summary>
  /// <param name="analyzer">Анализатор</param>
  /// <param name="temporary">true - анализатор будет применяться только для
  /// одного окна выбора, после чего будет удалён из списка</param>
  /// <returns>true - анализатор был успешно добавлен,
  /// false - анализатор с таким Guid уже зарегистрирован в соответствующем
  /// списке (постоянных или временных анализаторов)</returns>
  public static bool RegisterAnalyze(ISelectedItemsAnalyzer analyzer, bool temporary)
  {
    return SelectionWindow._addAnalyzer(!temporary ? SelectionWindow._analyzers : SelectionWindow._temporaryAnalyzers, analyzer);
  }

  /// <summary>Удалить анализатор из списков</summary>
  /// <param name="analyzer">Анализатор</param>
  /// <returns>true - анализатор был успешно удалён, false - не был найден в списках</returns>
  public static bool UnregisterAnalyze(ISelectedItemsAnalyzer analyzer)
  {
    return SelectionWindow._deleteAnalyzer(SelectionWindow._analyzers, analyzer) | SelectionWindow._deleteAnalyzer(SelectionWindow._temporaryAnalyzers, analyzer);
  }

  /// <summary>Добавить анализатор в указанный список</summary>
  /// <param name="list">Список анализаторов</param>
  /// <param name="analyzer">Анализатор</param>
  /// <returns>true - анализатор был успешно добавлен в список</returns>
  private static bool _addAnalyzer(
    List<IToSelectItemsAnalyzer> list,
    IToSelectItemsAnalyzer analyzer)
  {
    if (list == null || analyzer == null)
      return false;
    for (int index = 0; index < list.Count; ++index)
    {
      if (list[index].Guid == analyzer.Guid)
        return false;
    }
    list.Add(analyzer);
    return true;
  }

  /// <summary>Удалить анализатор из указанного списка</summary>
  /// <param name="list">Список анализаторов</param>
  /// <param name="analyzer">Анализатор</param>
  /// <returns>true - анализатор был успешно удалён из списка</returns>
  private static bool _deleteAnalyzer(
    List<IToSelectItemsAnalyzer> list,
    IToSelectItemsAnalyzer analyzer)
  {
    if (list == null || analyzer == null)
      return false;
    for (int index = 0; index < list.Count; ++index)
    {
      if (list[index].Guid == analyzer.Guid)
      {
        list.RemoveAt(index);
        return true;
      }
    }
    return false;
  }

  /// <summary>
  /// Зарегистрировать анализатор для проверки выделенных элементов
  /// </summary>
  /// <param name="analyzer">Анализатор</param>
  /// <param name="temporary">true - анализатор будет применяться только для
  /// одного окна выбора, после чего будет удалён из списка</param>
  /// <returns>true - анализатор был успешно добавлен,
  /// false - анализатор с таким Guid уже зарегистрирован в соответствующем
  /// списке (постоянных или временных анализаторов)</returns>
  public static bool RegisterAnalyze(IToSelectItemsAnalyzer analyzer)
  {
    return SelectionWindow._addAnalyzer(SelectionWindow._temporaryToSelAnalyzers, analyzer);
  }

  /// <summary>Удалить анализатор из списков</summary>
  /// <param name="analyzer">Анализатор</param>
  /// <returns>true - анализатор был успешно удалён, false - не был найден в списках</returns>
  public static bool UnregisterAnalyze(IToSelectItemsAnalyzer analyzer)
  {
    return SelectionWindow._deleteAnalyzer(SelectionWindow._temporaryToSelAnalyzers, analyzer);
  }

  /// <summary>
  /// Выбрать объекты из базы данных на основании указанного дескриптора
  /// </summary>
  /// <param name="description">Текст-пояснение</param>
  /// <param name="rootDescriptor">Дескриптор корневого узла</param>
  /// <param name="dataFormat">Формат данных</param>
  /// <param name="options">Внешний вид и поведение окна</param>
  /// <returns>Коллекция выбранных объектов</returns>
  public static object[] Select(
    string description,
    IDescriptor rootDescriptor,
    System.Type dataFormat,
    SelectionOptions options)
  {
    return SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_598"), description, rootDescriptor, dataFormat, (System.IServiceProvider) null, options);
  }

  /// <summary>
  /// Выбрать объекты из базы данных на основании указанного дескриптора
  /// </summary>
  /// <param name="description">Текст-пояснение</param>
  /// <param name="rootDescriptor">Дескриптор корневого узла</param>
  /// <param name="dataFormat">Формат данных</param>
  /// <param name="options">Внешний вид и поведение окна</param>
  /// <param name="enableTypes">Допустимые типы выбираемых объектов</param>
  /// <returns>Коллекция выбранных объектов</returns>
  public static object[] Select(
    string description,
    IDescriptor rootDescriptor,
    System.Type dataFormat,
    SelectionOptions options,
    int[] enableTypes)
  {
    return SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_598"), description, rootDescriptor, dataFormat, (DynamicSelectionEventHandler) null, (System.IServiceProvider) null, options, enableTypes);
  }

  /// <summary>
  /// Выбрать объекты из базы данных на основании указанного дескриптора
  /// </summary>
  /// <param name="description">Текст-пояснение</param>
  /// <param name="rootDescriptor">Дескриптор корневого узла</param>
  /// <param name="dataFormat">Формат данных</param>
  /// <param name="nodesContext">Контекст для узлов дерева и списков</param>
  /// <param name="options">Внешний вид и поведение окна</param>
  /// <returns>Коллекция выбранных объектов</returns>
  public static object[] Select(
    string description,
    IDescriptor rootDescriptor,
    System.Type dataFormat,
    System.IServiceProvider nodesContext,
    SelectionOptions options)
  {
    return SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_598"), description, rootDescriptor, dataFormat, (DynamicSelectionEventHandler) null, nodesContext, options, (int[]) null);
  }

  /// <summary>
  /// Выбрать объекты из базы данных на основании указанного дескриптора (+ заголовок окна)
  /// </summary>
  /// <param name="caption">Заголовок</param>
  /// <param name="description">Текст-пояснение</param>
  /// <param name="rootDescriptor">Дескриптор корневого узла</param>
  /// <param name="dataFormat">Формат данных</param>
  /// <param name="options">Внешний вид и поведение окна</param>
  /// <returns>Коллекция выбранных объектов</returns>
  public static object[] Select(
    string caption,
    string description,
    IDescriptor rootDescriptor,
    System.Type dataFormat,
    SelectionOptions options)
  {
    return SelectionWindow.Select(caption, description, rootDescriptor, dataFormat, (System.IServiceProvider) null, options);
  }

  /// <summary>
  /// Выбрать объекты из базы данных на основании указанного дескриптора (+ заголовок окна)
  /// </summary>
  /// <param name="caption">Заголовок</param>
  /// <param name="description">Текст-пояснение</param>
  /// <param name="rootDescriptor">Дескриптор корневого узла</param>
  /// <param name="dataFormat">Формат данных</param>
  /// <param name="nodesContext">Контекст для узлов дерева и списков</param>
  /// <param name="options">Внешний вид и поведение окна</param>
  /// <returns>Коллекция выбранных объектов</returns>
  public static object[] Select(
    string caption,
    string description,
    IDescriptor rootDescriptor,
    System.Type dataFormat,
    System.IServiceProvider nodesContext,
    SelectionOptions options)
  {
    return SelectionWindow.Select(caption, description, rootDescriptor, dataFormat, (DynamicSelectionEventHandler) null, nodesContext, options, (int[]) null);
  }

  /// <summary>
  /// Выбрать объекты из базы данных на основании указанного дескриптора (+ заголовок окна, + обработка нажатия кнопки)
  /// </summary>
  /// <param name="caption">Заголовок</param>
  /// <param name="description">Текст-пояснение</param>
  /// <param name="rootDescriptor">Дескриптор корневого узла</param>
  /// <param name="dataFormat">Формат данных</param>
  /// <param name="dynamicHandler">Обработчик нажатия кнопки</param>
  /// <param name="options">Внешний вид и поведение окна</param>
  /// <returns>Коллекция выбранных объектов</returns>
  public static object[] Select(
    string caption,
    string description,
    IDescriptor rootDescriptor,
    System.Type dataFormat,
    DynamicSelectionEventHandler dynamicHandler,
    SelectionOptions options)
  {
    return SelectionWindow.Select(caption, description, rootDescriptor, dataFormat, dynamicHandler, (System.IServiceProvider) null, options, (int[]) null);
  }

  /// <summary>
  /// Выбрать объекты из базы данных на основании указанного дескриптора (+ заголовок окна, + обработка нажатия кнопки)
  /// </summary>
  /// <param name="caption">Заголовок</param>
  /// <param name="description">Текст-пояснение</param>
  /// <param name="rootDescriptor">Дескриптор корневого узла</param>
  /// <param name="dataFormat">Формат данных</param>
  /// <param name="dynamicHandler">Обработчик нажатия кнопки</param>
  /// <param name="nodesContext">Контекст для узлов дерева и списков</param>
  /// <param name="options">Внешний вид и поведение окна</param>
  /// <param name="enableTypes">Допустимые типы выбираемых объектов</param>
  /// <returns>Коллекция выбранных объектов</returns>
  public static object[] Select(
    string caption,
    string description,
    IDescriptor rootDescriptor,
    System.Type dataFormat,
    DynamicSelectionEventHandler dynamicHandler,
    System.IServiceProvider nodesContext,
    SelectionOptions options,
    int[] enableTypes)
  {
    Intermech.Navigator.Controls.SelectionWindow win = SelectionWindow.GetSelWin(rootDescriptor, options);
    bool flag1 = win != null && SelectionWindow.IsAllreadyShown(win);
    bool flag2 = win == null | flag1;
    if (flag2)
      win = new Intermech.Navigator.Controls.SelectionWindow(options);
    win.Text = caption;
    win.lbDescription.Text = description;
    win.DialogResult = DialogResult.None;
    win.EnableTypes = enableTypes;
    if (dynamicHandler != null)
    {
      win.btOK.DialogResult = DialogResult.None;
      win.btOK.Tag = (object) dynamicHandler;
      win.btOK.Click += new EventHandler(SelectionWindow.btOk_Click);
      win.btOK.Text = LocalizationHolder.rm.GetString("Client.Core_599");
      win.btCancel.Text = LocalizationHolder.rm.GetString("Client.Core_217");
    }
    else
    {
      if (win.btOK.Tag != null)
        win.btOK.Click -= new EventHandler(SelectionWindow.btOk_Click);
      win.btOK.DialogResult = DialogResult.OK;
      win.btOK.Tag = (object) null;
      win.btOK.Text = LocalizationHolder.rm.GetString("Client.Core_218");
      win.btCancel.Text = LocalizationHolder.rm.GetString("Client.Core_166");
    }
    win.accessTime = DateTime.UtcNow;
    DialogResult dialogResult = DialogResult.None;
    EventHandler eventHandler = (EventHandler) null;
    if (SelectionWindow._analyzers.Count > 0 || SelectionWindow._temporaryAnalyzers.Count > 0)
    {
      ISelectedItemsAnalyzer[] temporaryAnalyzers = SelectionWindow._temporaryAnalyzers.ToArray();
      eventHandler = (EventHandler) ((s, e) => SelectionWindow.AnalyzeSelectedItems((ISelectionWindow) win, temporaryAnalyzers));
      SelectionWindow._temporaryAnalyzers.Clear();
    }
    try
    {
      if (eventHandler != null)
        win.SelectedItemsChanged += eventHandler;
      win.services.AdvancedProvider = nodesContext;
      win.options = options;
      if (flag2 || (options & SelectionOptions.ForceRebuildNavTree) != (SelectionOptions) 0)
      {
        win.TreeViewsBridge.UseDelay = false;
        try
        {
          win.NavTreeView.SetColumns(Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
          win.NavTreeView.Build(rootDescriptor);
        }
        finally
        {
          win.TreeViewsBridge.UseDelay = true;
        }
        if (!flag1)
          SelectionWindow.InsertSelWin(win);
      }
      if (win.services.GetService(typeof (INotificationServiceStatesHolder)) is INotificationServiceStatesHolder service)
        service.States &= ~NotificationServiceStates.InactiveDialog;
      if (!flag2)
        win.Update();
      if (SelectionWindow.OnSelectionWindowBeforeShow != null)
        SelectionWindow.OnSelectionWindowBeforeShow((object) win, new EventArgs());
      if (!flag2)
      {
        win.InternalFireNotification((object) win, new NotificationEventArgs("ToSelectItemsChanges"));
        if (eventHandler != null)
          eventHandler((object) win, EventArgs.Empty);
      }
      Intermech.Navigator.Controls.SelectionWindow.SelectionWindowMemento memento = (Intermech.Navigator.Controls.SelectionWindow.SelectionWindowMemento) null;
      SelectionWindow._selectionWindowMementoDictionary.TryGetValue(rootDescriptor, out memento);
      if (memento != null)
      {
        win.SetMemento(memento);
        SelectionWindow._selectionWindowMementoDictionary.Remove(rootDescriptor);
      }
      win.OKButtonEnabledFunc = (Func<bool>) (() => SelectionWindow._okButtonEnabled);
      SelectionWindow._okButtonEnabled = true;
      dialogResult = win.ShowDialog();
    }
    finally
    {
      win.OKButtonEnabledFunc = (Func<bool>) null;
      if (eventHandler != null)
        win.SelectedItemsChanged -= eventHandler;
      if (SelectionWindow.OnSelectionWindowAfterClose != null)
        SelectionWindow.OnSelectionWindowAfterClose((object) win, new EventArgs());
      SelectionWindow._temporaryToSelAnalyzers.Clear();
      win.services.AdvancedProvider = (System.IServiceProvider) null;
      if (win.services.GetService(typeof (INotificationServiceStatesHolder)) is INotificationServiceStatesHolder service)
        service.States |= NotificationServiceStates.InactiveDialog;
    }
    if (dynamicHandler != null)
    {
      win.btOK.Tag = (object) null;
      win.btOK.Click -= new EventHandler(SelectionWindow.btOk_Click);
      win.btOK.DialogResult = DialogResult.OK;
      win.btOK.Text = LocalizationHolder.rm.GetString("Client.Core_218");
      win.btCancel.Text = LocalizationHolder.rm.GetString("Client.Core_166");
    }
    if (dialogResult == DialogResult.OK && dynamicHandler == null)
    {
      object[] data = SelectionWindow.ExtractData(SelectionWindow.GetSelectedItems(win), dataFormat);
      if (data.Length != 0)
        return data;
    }
    return (object[]) null;
  }

  /// <summary>
  /// Выбрать объекты из базы данных на основании указанного дескриптора
  /// </summary>
  /// <param name="caption">Заголовок</param>
  /// <param name="description">Текст-пояснение</param>
  /// <param name="rootDescriptor">Дескриптор корневого узла</param>
  /// <param name="options">Внешний вид и поведение окна</param>
  /// <returns>Коллекция выбранных объектов</returns>
  public static long[] SelectObjects(
    string caption,
    string description,
    IDescriptor rootDescriptor,
    SelectionOptions options)
  {
    return SelectionWindow.SelectObjects(caption, description, rootDescriptor, (System.IServiceProvider) null, options);
  }

  /// <summary>
  /// Выбрать объекты из базы данных на основании указанного дескриптора
  /// </summary>
  /// <param name="caption">Заголовок</param>
  /// <param name="description">Текст-пояснение</param>
  /// <param name="rootDescriptor">Дескриптор корневого узла</param>
  /// <param name="nodesContext">Контекст для узлов дерева и списков</param>
  /// <param name="options">Внешний вид и поведение окна</param>
  /// <returns>Коллекция выбранных объектов</returns>
  public static long[] SelectObjects(
    string caption,
    string description,
    IDescriptor rootDescriptor,
    System.IServiceProvider nodesContext,
    SelectionOptions options)
  {
    object[] objArray = SelectionWindow.Select(caption, description, rootDescriptor, typeof (IDBObjectID), (DynamicSelectionEventHandler) null, nodesContext, options, (int[]) null);
    if (objArray == null)
      return (long[]) null;
    long[] numArray = new long[objArray.Length];
    for (int index = 0; index < objArray.Length; ++index)
      numArray[index] = (objArray[index] as IDBObjectID).Value;
    return numArray;
  }

  /// <summary>Выбрать объекты указанного типа из базы данных</summary>
  /// <param name="caption">Заголовок</param>
  /// <param name="description">Текст-пояснение</param>
  /// <param name="objTypeID">Идентификатор типа объектов.</param>
  /// <param name="options">Внешний вид и поведение окна</param>
  /// <returns>Коллекция выбранных объектов</returns>
  public static long[] SelectObjects(
    string caption,
    string description,
    int objTypeID,
    SelectionOptions options)
  {
    return SelectionWindow.SelectObjects(caption, description, objTypeID, (System.IServiceProvider) null, options);
  }

  /// <summary>Выбрать объекты указанного типа из базы данных</summary>
  /// <param name="caption">Заголовок</param>
  /// <param name="description">Текст-пояснение</param>
  /// <param name="objTypeID">Идентификатор типа объектов.</param>
  /// <param name="nodesContext">Контекст для узлов дерева и списков</param>
  /// <param name="options">Внешний вид и поведение окна</param>
  /// <returns>Коллекция выбранных объектов</returns>
  public static long[] SelectObjects(
    string caption,
    string description,
    int objTypeID,
    System.IServiceProvider nodesContext,
    SelectionOptions options)
  {
    IDescriptor rootDescriptor = (IDescriptor) new Descriptor(objTypeID);
    return SelectionWindow.SelectObjects(caption, description, rootDescriptor, nodesContext, options);
  }

  /// <summary>Выбрать объекты произвольного типа из базы данных</summary>
  /// <param name="caption">Заголовок</param>
  /// <param name="description">Текст-пояснение</param>
  /// <param name="options">Внешний вид и поведение окна</param>
  /// <returns>Коллекция выбранных объектов</returns>
  public static long[] SelectObjects(string caption, string description, SelectionOptions options)
  {
    return SelectionWindow.SelectObjects(caption, description, (System.IServiceProvider) null, options);
  }

  /// <summary>Выбрать объекты произвольного типа из базы данных</summary>
  /// <param name="caption">Заголовок</param>
  /// <param name="description">Текст-пояснение</param>
  /// <param name="nodesContext">Контекст для узлов дерева и списков</param>
  /// <param name="options">Внешний вид и поведение окна</param>
  /// <returns>Коллекция выбранных объектов</returns>
  public static long[] SelectObjects(
    string caption,
    string description,
    System.IServiceProvider nodesContext,
    SelectionOptions options)
  {
    IDescriptor rootDescriptor = (IDescriptor) new ObjectTypesNodeDescriptor();
    return SelectionWindow.SelectObjects(caption, description, rootDescriptor, nodesContext, options);
  }

  /// <summary>
  /// Выбрать объекты произвольного типа из базы данных (+ пользовательский обработчик событий)
  /// </summary>
  /// <param name="caption">Заголовок</param>
  /// <param name="description">Текст-пояснение</param>
  /// <param name="rootDescriptor">Дескриптор корневого узла</param>
  /// <param name="dynamicHandler">Пользовательский обработчик</param>
  /// <param name="options">Внешний вид и поведение окна</param>
  public static void DynamicSelectObjects(
    string caption,
    string description,
    IDescriptor rootDescriptor,
    DynamicSelectionEventHandler dynamicHandler,
    SelectionOptions options)
  {
    SelectionWindow.DynamicSelectObjects(caption, description, rootDescriptor, dynamicHandler, (System.IServiceProvider) null, options);
  }

  /// <summary>
  /// Выбрать объекты произвольного типа из базы данных (+ пользовательский обработчик событий)
  /// </summary>
  /// <param name="caption">Заголовок</param>
  /// <param name="description">Текст-пояснение</param>
  /// <param name="rootDescriptor">Дескриптор корневого узла</param>
  /// <param name="dynamicHandler">Пользовательский обработчик</param>
  /// <param name="nodesContext">Контекст для узлов дерева и списков</param>
  /// <param name="options">Внешний вид и поведение окна</param>
  public static void DynamicSelectObjects(
    string caption,
    string description,
    IDescriptor rootDescriptor,
    DynamicSelectionEventHandler dynamicHandler,
    System.IServiceProvider nodesContext,
    SelectionOptions options)
  {
    SelectionWindow.Select(caption, description, rootDescriptor, typeof (IDBObjectID), dynamicHandler, nodesContext, options, (int[]) null);
  }

  public static Intermech.Navigator.Controls.SelectionWindow CreateForm(
    string caption,
    string description,
    IDescriptor rootDescriptor,
    System.Type dataFormat,
    DynamicSelectionEventHandler dynamicHandler,
    SelectionOptions options)
  {
    Intermech.Navigator.Controls.SelectionWindow win = SelectionWindow.GetSelWin(rootDescriptor, options);
    int num = win == null ? 1 : 0;
    if (num != 0)
      win = new Intermech.Navigator.Controls.SelectionWindow(options);
    win.Text = caption;
    win.lbDescription.Text = description;
    win.DialogResult = DialogResult.None;
    win.btOK.DialogResult = DialogResult.OK;
    win.btOK.Tag = (object) dynamicHandler;
    win.btOK.Click += new EventHandler(SelectionWindow.btOk_Click);
    win.btCancel.Click += new EventHandler(SelectionWindow.btCancel_Click);
    win.btOK.Text = LocalizationHolder.rm.GetString("Client.Core_599");
    win.btCancel.Text = LocalizationHolder.rm.GetString("Client.Core_217");
    win.accessTime = DateTime.UtcNow;
    win.services.AdvancedProvider = (System.IServiceProvider) null;
    win.options = options;
    if (num != 0 || (options & SelectionOptions.ForceRebuildNavTree) != (SelectionOptions) 0)
    {
      win.TreeViewsBridge.UseDelay = false;
      try
      {
        win.NavTreeView.SetColumns(Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
        win.NavTreeView.Build(rootDescriptor);
      }
      finally
      {
        win.TreeViewsBridge.UseDelay = true;
      }
      SelectionWindow.InsertSelWin(win);
    }
    if (win.services.GetService(typeof (INotificationServiceStatesHolder)) is INotificationServiceStatesHolder service)
      service.States &= ~NotificationServiceStates.InactiveDialog;
    return win;
  }

  public static void CloseWindow(Intermech.Navigator.Controls.SelectionWindow win)
  {
    win.services.AdvancedProvider = (System.IServiceProvider) null;
    if (win.services.GetService(typeof (INotificationServiceStatesHolder)) is INotificationServiceStatesHolder service)
      service.States |= NotificationServiceStates.InactiveDialog;
    win.btOK.Tag = (object) null;
    win.btOK.Click -= new EventHandler(SelectionWindow.btOk_Click);
    win.btCancel.Click -= new EventHandler(SelectionWindow.btCancel_Click);
    win.btOK.Text = LocalizationHolder.rm.GetString("Client.Core_218");
    win.btCancel.Text = LocalizationHolder.rm.GetString("Client.Core_166");
  }

  private static void AnalyzeSelectedItems(
    ISelectionWindow selectionWindow,
    ISelectedItemsAnalyzer[] temporaryAnalyzers)
  {
    bool flag = true;
    ISelectedItemsHost itemsHost = selectionWindow is ICurrentSelectedItemsHost selectedItemsHost ? selectedItemsHost.ItemsHost : (ISelectedItemsHost) null;
    for (int index = 0; index < SelectionWindow._analyzers.Count; ++index)
    {
      if (SelectionWindow._analyzers[index].Analyze(selectionWindow, itemsHost) == SelectedItemsAnalyzerResult.Disabled)
      {
        flag = false;
        break;
      }
    }
    if (flag)
    {
      for (int index = 0; index < temporaryAnalyzers.Length; ++index)
      {
        if (temporaryAnalyzers[index].Analyze(selectionWindow, itemsHost) == SelectedItemsAnalyzerResult.Disabled)
        {
          flag = false;
          break;
        }
      }
    }
    if (selectionWindow == null || selectionWindow.OkButton == null || !selectionWindow.OkButton.Enabled)
      return;
    SelectionWindow._okButtonEnabled = flag;
    selectionWindow.OkButton.Enabled = flag;
  }

  /// <summary>Получить от окна список выделенных элементов</summary>
  /// <param name="win">Окно выбора объектов, у которого получается список</param>
  /// <returns>Список выделенных элементов</returns>
  private static ISelectedItems GetSelectedItems(Intermech.Navigator.Controls.SelectionWindow win)
  {
    ISelectedItemsHost itemsHost = win.ItemsHost;
    if (itemsHost != null && itemsHost.SelectedItems.Count > 0)
      return itemsHost.SelectedItems;
    return win.ViewsManager.ActiveViewPage != null && win.ViewsManager.ActiveViewPage.View is ISelectedItemsHost view && view.SelectedItems.Count > 0 ? view.SelectedItems : win.NavTreeView.SelectedItems;
  }

  /// <summary>
  /// Получить у выделенных элементов данные указанного типа
  /// </summary>
  /// <param name="items">Выделенные элементы</param>
  /// <param name="dataFormat">Извлекаемый тип данных</param>
  /// <returns>Данные указанного типа</returns>
  private static object[] ExtractData(ISelectedItems items, System.Type dataFormat)
  {
    ArrayList arrayList = new ArrayList();
    for (int index = 0; index < items.Count; ++index)
    {
      object itemData = items.GetItemData(index, dataFormat);
      if (itemData != null)
        arrayList.Add(itemData);
    }
    return (object[]) arrayList.ToArray(dataFormat);
  }

  /// <summary>Нажата кнопка "ОК"</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private static void btOk_Click(object sender, EventArgs e)
  {
    Button button = (Button) sender;
    Intermech.Navigator.Controls.SelectionWindow form = (Intermech.Navigator.Controls.SelectionWindow) button.FindForm();
    DynamicSelectionEventHandler tag = (DynamicSelectionEventHandler) button.Tag;
    if (tag == null)
    {
      form.DialogResult = button.DialogResult;
    }
    else
    {
      foreach (IDBObjectID dbObjectId in SelectionWindow.ExtractData(SelectionWindow.GetSelectedItems(form), typeof (IDBObjectID)))
      {
        if (!tag(dbObjectId.Value, DynamicSelectionMode.PreSelect))
          form.Close();
      }
    }
  }

  /// <summary>Нажата кнопка "Закрыть"</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private static void btCancel_Click(object sender, EventArgs e)
  {
    SelectionWindow.CloseWindow((Intermech.Navigator.Controls.SelectionWindow) ((Control) sender).FindForm());
  }

  private static bool IsAllreadyShown(Intermech.Navigator.Controls.SelectionWindow selectionWindow)
  {
    return Application.OpenForms.Cast<Form>().Any<Form>((Func<Form, bool>) (o => o.Handle == selectionWindow.Handle));
  }

  [Serializable]
  private sealed class SelectionWindowStorageState
  {
    public string NavigatorTreeViewNodeIDPathAsString { get; set; }
  }
}
