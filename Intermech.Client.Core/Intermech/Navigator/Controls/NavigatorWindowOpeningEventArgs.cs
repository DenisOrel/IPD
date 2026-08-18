
// Type: Intermech.Navigator.Controls.NavigatorWindowOpeningEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Controls;

public sealed class NavigatorWindowOpeningEventArgs : NotificationEventArgs
{
  public NavigatorWindowOpeningEventArgs(
    NavWindowBase navigatorWindow,
    IDescriptor descriptor,
    NodeIDPath path,
    IServiceProvider serviceProvider)
    : base("NavigatorWindowOpening")
  {
    if (descriptor == null && path == null)
      throw new ArgumentNullException("descriptor/path");
    this.NavigatorWindow = navigatorWindow;
    this.ServiceProvider = serviceProvider;
    this.Path = path;
    this.Descriptor = descriptor;
  }

  public NodeIDPath Path { get; }

  public IDescriptor Descriptor { get; }

  public NavWindowBase NavigatorWindow { get; }

  public IServiceProvider ServiceProvider { get; private set; }
}
