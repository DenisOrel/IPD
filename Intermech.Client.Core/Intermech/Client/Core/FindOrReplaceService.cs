
// Type: Intermech.Client.Core.FindOrReplaceService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Docking;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using System;


namespace Intermech.Client.Core;

/// <summary> Класс для реализации поиска и замены чего-либо </summary>
public abstract class FindOrReplaceService
{
  private static readonly string configId = "FindSetupDialog";
  private static bool _firstShow = true;
  private static bool _isFindWindowVisible1 = false;
  private static IFindController _iFindController = (IFindController) null;
  private static IWindowWithFind _iWindowWithFind = (IWindowWithFind) null;

  public static bool _isFindWindowVisible
  {
    get => FindOrReplaceService._isFindWindowVisible1;
    set => FindOrReplaceService._isFindWindowVisible1 = value;
  }

  public static void dockManager_DockControlActivated(object sender, DockControlEventArgs e)
  {
    if (!FindOrReplaceService._isFindWindowVisible || (e.DockControl.AllowedStates & DockLocation.Document) == DockLocation.Unknown || e.DockControl is IWindowWithFind || FindOrReplaceService._iFindController == null)
      return;
    FindOrReplaceService._iFindController.Hide();
  }

  /// <summary> Показать форму настройки поиска </summary>
  /// <param name="iWindowWithFind"> Интерфейс окна, в содержимом которого должен производиться поиск </param>
  /// <param name="makeWindowVisible"> Делать ли окно настройки поиска активным </param>
  /// <param name="replaceOrFindMode"> True, в окне поиска и замены нужно включить режим замены, False если поиска, Null, если надо оставить "как есть" </param>
  /// <returns> Интерфейс окна настройки поиска </returns>
  private static IFindController ShowFindWindow(
    IWindowWithFind iWindowWithFind,
    bool makeWindowVisible,
    bool? replaceOrFindMode)
  {
    FindOrReplaceService._iWindowWithFind = iWindowWithFind;
    if (FindOrReplaceService._firstShow)
    {
      DockManager service = (DockManager) ServicesManager.GetService(typeof (DockManager));
      if (service != null)
        service.DockControlActivated += new DockControlEventHandler(FindOrReplaceService.dockManager_DockControlActivated);
      FindOrReplaceService._firstShow = false;
    }
    Type findSetupFormClass = iWindowWithFind?.GetFindSetupFormClass();
    if (FindOrReplaceService._iFindController != null)
    {
      FindOrReplaceService.SaveFindWindowConfig();
      FindOrReplaceService._iFindController.Hide();
    }
    if (findSetupFormClass != (Type) null)
    {
      FindOrReplaceService._iFindController = Activator.CreateInstance(findSetupFormClass) as IFindController;
      if (FindOrReplaceService._iFindController == null)
        throw new Exception($"{sc_2789.ssp_imclient_2790()}{findSetupFormClass.ToString()}' must implement interface IFindController to be used as form for setup find operation");
    }
    if (FindOrReplaceService._iFindController != null)
    {
      FindOrReplaceService.LoadFindWindowConfig();
      FindOrReplaceService._iFindController.AttachToWindow(iWindowWithFind);
      if (makeWindowVisible)
      {
        FindOrReplaceService._iFindController.Show();
        FindOrReplaceService._isFindWindowVisible = true;
      }
      if (replaceOrFindMode.HasValue && FindOrReplaceService._iFindController is IFindOrReplaceController iFindController)
      {
        bool? nullable = replaceOrFindMode;
        bool flag = true;
        int num = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
        iFindController.IsReplaceMode = num != 0;
      }
    }
    return FindOrReplaceService._iFindController;
  }

  /// <summary> Показать форму настройки поиска </summary>
  /// <param name="iWindowWithFind"> Интерфейс окна, в содержимом которого должен производиться поиск </param>
  /// <returns> Интерфейс окна настройки поиска </returns>
  public static IFindController ShowFindWindow(IWindowWithFind iWindowWithFind)
  {
    return FindOrReplaceService.ShowFindWindow(iWindowWithFind, true, new bool?(false));
  }

  /// <summary> Показать форму настройки поиска с заменой </summary>
  /// <param name="iWindowWithFindAndReplace"> Интерфейс окна, в содержимом которого должен производиться поиск с заменой </param>
  /// <returns> Интерфейс окна настройки поиска с заменой </returns>
  public static IFindOrReplaceController ShowReplaceWindow(
    IWindowWithFindAndReplace iWindowWithFindAndReplace)
  {
    return FindOrReplaceService.ShowFindWindow((IWindowWithFind) iWindowWithFindAndReplace, true, new bool?(true)) as IFindOrReplaceController;
  }

  /// <summary>
  /// Создать форму настройки поиска, но не показывать её
  /// (для случаем, когда поиск должен производиться, но окно пользователю показывать не надо)
  /// </summary>
  /// <param name="iWindowWithFind"> Интерфейс окна, в содержимом которого должен производиться поиск </param>
  /// <returns> Интерфейс окна настройки поиска </returns>
  public static IFindController CreateFindWindowHidden(IWindowWithFind iWindowWithFind)
  {
    return FindOrReplaceService.ShowFindWindow(iWindowWithFind, false, new bool?(false));
  }

  /// <summary>
  /// Создать форму настройки поиска с заменой, но не показывать её
  /// (для случаем, когда поиск с заменой должен производиться, но окно пользователю показывать не надо)
  /// </summary>
  /// <param name="iWindowWithFindAndReplace"> Интерфейс окна, в содержимом которого должен производиться поиск с заменой </param>
  /// <returns> Интерфейс окна настройки поиска с заменой </returns>
  public static IFindOrReplaceController CreateReplaceWindowHidden(
    IWindowWithFindAndReplace iWindowWithFindAndReplace)
  {
    return FindOrReplaceService.ShowFindWindow((IWindowWithFind) iWindowWithFindAndReplace, false, new bool?(true)) as IFindOrReplaceController;
  }

  /// <summary> Возвращает True, если окно настройки поиска активизировано </summary>
  public static bool IsFindWindowVisible
  {
    get => FindOrReplaceService._isFindWindowVisible;
    set
    {
      if (FindOrReplaceService._isFindWindowVisible == value)
        return;
      FindOrReplaceService._isFindWindowVisible = value;
      if (FindOrReplaceService._iFindController == null || FindOrReplaceService._iFindController.IsVisible == value)
        return;
      if (value)
        FindOrReplaceService._iFindController.Show();
      else
        FindOrReplaceService._iFindController.Hide();
    }
  }

  /// <summary> Сохраняет конфигурацию активного окна настройки поиска </summary>
  public static void SaveFindWindowConfig()
  {
    if (FindOrReplaceService._iFindController == null || !(ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service))
      return;
    IConfiguration iConfiguration = service.Open(FindOrReplaceService.configId) ?? service.Create(FindOrReplaceService.configId);
    if (iConfiguration == null)
      return;
    FindOrReplaceService._iFindController.SaveConfiguration(iConfiguration);
  }

  /// <summary> Востанавливает конфигурацию активного окна настройки поиска </summary>
  public static void LoadFindWindowConfig()
  {
    if (FindOrReplaceService._iFindController == null || !(ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service))
      return;
    IConfiguration iConfiguration = service.Open(FindOrReplaceService.configId) ?? service.Create(FindOrReplaceService.configId);
    if (iConfiguration == null)
      return;
    FindOrReplaceService._iFindController.LoadConfiguration(iConfiguration);
  }

  /// <summary> Вызывать у окна по содержимому которого производиться поиск функцию FindNext () "Найти далее" </summary>
  internal static void CallFindNext()
  {
    if (FindOrReplaceService._iWindowWithFind != null)
      FindOrReplaceService._iWindowWithFind.FindNext(FindOrReplaceService._iFindController);
    FindOrReplaceService._iFindController.Show();
  }

  /// <summary> Вызывать у окна по содержимому которого производиться поиск функцию FindNext () "Заменить" </summary>
  internal static void CallReplace()
  {
    if (FindOrReplaceService._iWindowWithFind != null && FindOrReplaceService._iWindowWithFind is IWindowWithFindAndReplace)
      ((IWindowWithFindAndReplace) FindOrReplaceService._iWindowWithFind).Replace(FindOrReplaceService._iFindController);
    FindOrReplaceService._iFindController.Show();
  }

  /// <summary> Вызывать у окна по содержимому которого производиться поиск функцию ReplaceAll () "Заменить все" </summary>
  internal static void CallReplaceAll()
  {
    if (FindOrReplaceService._iWindowWithFind != null && FindOrReplaceService._iWindowWithFind is IWindowWithFindAndReplace)
      ((IWindowWithFindAndReplace) FindOrReplaceService._iWindowWithFind).ReplaceAll(FindOrReplaceService._iFindController);
    FindOrReplaceService._iFindController.Show();
  }
}
