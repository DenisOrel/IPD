
// Type: Intermech.Client.Core.Visualizers.PagerEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Visualizers;

public class PagerEventArgs : EventArgs
{
  private object _page;

  public PagerEventArgs(object page) => this._page = page;

  public object Page => this._page;
}
