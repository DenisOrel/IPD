
// Type: Intermech.Search.UI.MainWindowClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.Search.UI;

public sealed class MainWindowClientService : IMainWindowClientService
{
  private Form _mainWindow;

  public MainWindowClientService(Form mainWindow)
  {
    this._mainWindow = mainWindow != null ? mainWindow : throw new ArgumentNullException(nameof (mainWindow));
  }

  public Form GetMainWindow() => this._mainWindow;
}
