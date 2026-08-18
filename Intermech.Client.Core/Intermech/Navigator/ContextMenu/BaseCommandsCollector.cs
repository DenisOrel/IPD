
// Type: Intermech.Navigator.ContextMenu.BaseCommandsCollector
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Windows.Forms;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Базовый класс для сбора команд контекстных меню Навигатора
/// </summary>
internal class BaseCommandsCollector
{
  /// <summary>Требуется ли показать сообщение об ошибке</summary>
  private static bool _showErrorMsg = true;

  /// <summary>Отобразить сообщение об ошибке</summary>
  /// <param name="e">Исключение</param>
  internal void ShowError(Exception e)
  {
    if (e == null)
      return;
    if (ServicesManager.GetService(typeof (IOutputView)) is IOutputView service)
    {
      string text = string.Format(LocalizationHolder.rm.GetString("Client.Core_1370"), (object) e.Message);
      service.WriteString(LocalizationHolder.rm.GetString("IMClient_51"), text);
      service.WriteString(LocalizationHolder.rm.GetString("IMClient_51"), e.StackTrace);
    }
    if (!BaseCommandsCollector._showErrorMsg)
      return;
    BaseCommandsCollector._showErrorMsg = false;
    switch (IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1371"), LocalizationHolder.rm.GetString("Client.Core_1372") + LocalizationHolder.rm.GetString("Client.Core_1373"), new IMMessageBoxButton[3]
    {
      new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1374"), DialogResult.No),
      new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1375"), DialogResult.Yes),
      new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1376"), DialogResult.Abort)
    }, IMMessageBoxImage.Information))
    {
      case DialogResult.Abort:
        ExceptionHelper.ExceptionService.ShowException(e);
        break;
      case DialogResult.Yes:
        service?.ShowView();
        break;
    }
  }
}
