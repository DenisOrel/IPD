
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.BrowserInternalViewerHost
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers;

internal class BrowserInternalViewerHost : InternalViewerHost
{
  private WebBrowser _browser;

  private WebBrowser WebBrowser
  {
    get
    {
      WebBrowser browser = this._browser;
      if (browser != null)
        return browser;
      WebBrowser webBrowser1 = new WebBrowser();
      webBrowser1.Dock = DockStyle.Fill;
      webBrowser1.Name = "webbrowser";
      webBrowser1.ScrollBarsEnabled = true;
      WebBrowser webBrowser2 = webBrowser1;
      this._browser = webBrowser1;
      return webBrowser2;
    }
  }

  public override bool Open(string fileName)
  {
    this.WebBrowser.Navigate(fileName, false);
    if (!((IEnumerable<Control>) this.Controls.Find("webbrowser", true)).Any<Control>())
      this.Controls.Add((Control) this.WebBrowser);
    return true;
  }
}
