
// Type: Intermech.Files.FileManagerModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using Intermech.Bars;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Search;
using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.Files;

/// <summary>
/// Реализует модуль, добавляющий команду "Файловый менеджер" в меню "Приложения".
/// </summary>
internal sealed class FileManagerModule : InitializerModule
{
  private const string FileManagerCommand = "ShowFileManager";
  private IMainMenuService mainMenuService;
  private BarManager barManager;
  private MenuButtonItem filemanItem;

  public FileManagerModule(IMainMenuService mainMenuService, BarManager barManager)
  {
    if (mainMenuService == null)
      throw new ArgumentNullException(nameof (mainMenuService));
    if (barManager == null)
      throw new ArgumentNullException(nameof (barManager));
    this.mainMenuService = mainMenuService;
    this.barManager = barManager;
  }

  /// <summary>
  /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем.
  /// </summary>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.filemanItem = new MenuButtonItem();
    this.filemanItem.Text = LocalizationHolder.rm.GetString("Client.Core_1302");
    this.filemanItem.CommandName = "ShowFileManager";
    this.filemanItem.Click += new EventHandler(FileManagerModule.OnShowFileManager);
    this.filemanItem.ShortcutActive = true;
    this.filemanItem.Shortcut = Shortcut.CtrlShiftF;
    this.mainMenuService.RegisterMenuItems(MainMenuItemSite.Applications, MainMenuItemPosition.Default, this.filemanItem);
  }

  /// <summary>
  /// Завершает работу объектов и сервисов, предоставленных модулем.
  /// Если свойство модуля IsInitialized возвращает false, то DoShutdown вызван как реакция на необработанное исключение при инициализации модуля.
  /// </summary>
  protected override void DoShutdown()
  {
    base.DoShutdown();
    if (this.filemanItem != null)
      this.filemanItem.Dispose();
    this.filemanItem = (MenuButtonItem) null;
  }

  private static void OnShowFileManager(object sender, EventArgs e)
  {
    Process.Start("explorer.exe", $"/e,/root,{ClientContext.FileVault.WorkArea.AreaPath}");
  }
}
