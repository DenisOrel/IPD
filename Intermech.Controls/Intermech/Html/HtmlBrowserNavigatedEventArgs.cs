
// Type: Intermech.Html.HtmlBrowserNavigatedEventArgs
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;


namespace Intermech.Html;

public class HtmlBrowserNavigatedEventArgs : EventArgs
{
  private string url;

  public HtmlBrowserNavigatedEventArgs(string url) => this.url = url;

  public string Url => this.url;
}
