
// Type: Intermech.Navigator.Controls.NavigatorWindowOpenedEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;


namespace Intermech.Navigator.Controls;

public sealed class NavigatorWindowOpenedEventArgs : NotificationEventArgs
{
  public NavigatorWindowOpenedEventArgs(NavWindow navigatorWindow, IServiceProvider serviceProvider)
    : base("NavigatorWindowOpened")
  {
    this.NavigatorWindow = navigatorWindow != null ? navigatorWindow : throw new ArgumentNullException(nameof (navigatorWindow));
    this.ServiceProvider = serviceProvider;
  }

  public NavWindow NavigatorWindow { get; private set; }

  public IServiceProvider ServiceProvider { get; private set; }
}
