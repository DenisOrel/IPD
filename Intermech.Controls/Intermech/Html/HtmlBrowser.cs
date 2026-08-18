
// Type: Intermech.Html.HtmlBrowser
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;


namespace Intermech.Html;

public class HtmlBrowser : Control
{
  private string desiredDocumentText;
  private string desiredUrl;
  private bool firstActivation;
  private HtmlSite site;
  private HtmlBrowser.ConnectionPointCookie webBrowserEvents2Cookie;
  private HtmlBrowser.ConnectionPointCookie webBrowserEventsCookie;

  public event HtmlBrowserNavigatedEventHandler DocumentLoaded;

  public event HtmlBrowserNavigatedEventHandler Navigated;

  public event HtmlBrowserNavigatingEventHandler Navigating;

  public HtmlBrowser()
  {
    this.desiredUrl = string.Empty;
    this.desiredDocumentText = string.Empty;
    this.firstActivation = true;
    this.site = (HtmlSite) null;
    this.site = new HtmlSite(this);
    this.Dock = DockStyle.Fill;
    this.TabStop = false;
    this.SetStyle(ControlStyles.UserPaint, true);
  }

  public void Copy()
  {
    if ((this.site.Browser.QueryStatus(NativeMethods.CommandName.Copy) & NativeMethods.CommandStatus.Enabled) == (NativeMethods.CommandStatus) 0)
      return;
    object arguments = (object) null;
    object results = (object) null;
    this.site.Browser.Execute(NativeMethods.CommandName.Copy, NativeMethods.CommandExecuteOptions.PromptUser, ref arguments, ref results);
  }

  private void Copy_Click(object sender, EventArgs e) => this.Copy();

  protected override void Dispose(bool disposing)
  {
    this.site = (HtmlSite) null;
    base.Dispose(disposing);
  }

  private string GenerateTempFileName()
  {
    string path;
    do
    {
      path = Path.ChangeExtension(Path.GetTempFileName(), ".htm");
    }
    while (File.Exists(path));
    return path;
  }

  protected override bool IsInputKey(Keys keyData)
  {
    return keyData != Keys.Escape && base.IsInputKey(keyData);
  }

  public void Navigate(string url)
  {
    if (this.IsHandleCreated)
    {
      object flags = (object) 0;
      object empty1 = (object) string.Empty;
      object empty2 = (object) string.Empty;
      object empty3 = (object) string.Empty;
      this.site.Browser.Navigate(url, ref flags, ref empty1, ref empty2, ref empty3);
    }
    else
      this.desiredUrl = url;
  }

  protected virtual void OnDocumentLoaded(HtmlBrowserNavigatedEventArgs e)
  {
    if (this.desiredDocumentText.Length != 0)
    {
      if (File.Exists(this.desiredUrl))
        File.Delete(this.desiredUrl);
      this.desiredDocumentText = string.Empty;
    }
    if (this.DocumentLoaded == null)
      return;
    this.DocumentLoaded((object) this, e);
  }

  protected override void OnHandleCreated(EventArgs e)
  {
    base.OnHandleCreated(e);
    if (!this.firstActivation)
      return;
    this.firstActivation = false;
    this.site.CreateBrowser();
    this.webBrowserEventsCookie = new HtmlBrowser.ConnectionPointCookie((object) this.site.Browser, (object) new HtmlBrowser.WebBrowserEvents(this), typeof (IWebBrowserEvents));
    this.webBrowserEvents2Cookie = new HtmlBrowser.ConnectionPointCookie((object) this.site.Browser, (object) new HtmlBrowser.WebBrowserEvents2(this), typeof (IWebBrowserEvents2));
    if (this.desiredUrl.Length == 0)
    {
      this.desiredUrl = this.GenerateTempFileName();
      using (StreamWriter text = File.CreateText(this.desiredUrl))
        text.WriteLine(this.desiredDocumentText);
    }
    this.Navigate(this.desiredUrl);
  }

  protected override void OnHandleDestroyed(EventArgs e)
  {
    this.webBrowserEventsCookie.Disconnect();
    this.webBrowserEventsCookie = (HtmlBrowser.ConnectionPointCookie) null;
    this.webBrowserEvents2Cookie.Disconnect();
    this.webBrowserEvents2Cookie = (HtmlBrowser.ConnectionPointCookie) null;
    this.site.DestroyBrowser();
    this.site = (HtmlSite) null;
    base.OnHandleDestroyed(e);
  }

  protected virtual void OnNavigated(HtmlBrowserNavigatedEventArgs e)
  {
    if (this.Navigated == null)
      return;
    this.Navigated((object) this, e);
  }

  protected virtual void OnNavigating(HtmlBrowserNavigatingEventArgs e)
  {
    if (this.Navigating == null)
      return;
    this.Navigating((object) this, e);
  }

  private void PerformDocumentLoaded(HtmlBrowserNavigatedEventArgs e) => this.OnDocumentLoaded(e);

  private void PerformNavigated(HtmlBrowserNavigatedEventArgs e) => this.OnNavigated(e);

  private void PerformNavigating(HtmlBrowserNavigatingEventArgs e) => this.OnNavigating(e);

  public override bool PreProcessMessage(ref System.Windows.Forms.Message msg)
  {
    return this.site.TranslateAccelarator(new NativeMethods.Message()
    {
      Code = msg.Msg,
      WParam = msg.WParam,
      LParam = msg.LParam,
      WindowHandle = msg.HWnd
    }) || base.PreProcessMessage(ref msg);
  }

  public void Print()
  {
    if ((this.site.Browser.QueryStatus(NativeMethods.CommandName.Print) & NativeMethods.CommandStatus.Enabled) == (NativeMethods.CommandStatus) 0)
      return;
    object arguments = (object) null;
    object results = (object) null;
    this.site.Browser.Execute(NativeMethods.CommandName.Print, NativeMethods.CommandExecuteOptions.PromptUser, ref arguments, ref results);
  }

  private void Print_Click(object sender, EventArgs e) => this.Print();

  public void PrintPreview()
  {
    if ((this.site.Browser.QueryStatus(NativeMethods.CommandName.PrintPreview) & NativeMethods.CommandStatus.Enabled) == (NativeMethods.CommandStatus) 0)
      return;
    object arguments = (object) null;
    object results = (object) null;
    this.site.Browser.Execute(NativeMethods.CommandName.PrintPreview, NativeMethods.CommandExecuteOptions.PromptUser, ref arguments, ref results);
  }

  private void PrintPreview_Click(object sender, EventArgs e) => this.PrintPreview();

  protected override void SetBoundsCore(
    int x,
    int y,
    int width,
    int height,
    BoundsSpecified specified)
  {
    base.SetBoundsCore(x, y, width, height, specified);
    System.Drawing.Rectangle clientRectangle = this.ClientRectangle;
    this.site.SetBounds(clientRectangle.Left, clientRectangle.Top, clientRectangle.Right, clientRectangle.Bottom);
  }

  protected override Size DefaultSize => new Size(0, 0);

  public HtmlDocument Document
  {
    get
    {
      return this.site.Browser == null ? (HtmlDocument) null : new HtmlDocument(this.site.Browser.Document);
    }
  }

  public string DocumentText
  {
    get => this.desiredDocumentText;
    set => this.desiredDocumentText = value;
  }

  [PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
  [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
  private class ConnectionPointCookie
  {
    private NativeMethods.IConnectionPoint connectionPoint;
    private int cookie;

    public ConnectionPointCookie(object source, object sink, System.Type eventInterface)
    {
      Exception exception = (Exception) null;
      if (source is NativeMethods.IConnectionPointContainer connectionPointContainer)
      {
        try
        {
          Guid guid = eventInterface.GUID;
          connectionPointContainer.FindConnectionPoint(ref guid, out this.connectionPoint);
        }
        catch (Exception ex)
        {
          this.connectionPoint = (NativeMethods.IConnectionPoint) null;
        }
        if (this.connectionPoint == null)
          exception = (Exception) new ArgumentException($"The source object does not expose '{eventInterface.Name}'.");
        else if (sink == null || !eventInterface.IsInstanceOfType(sink))
          exception = (Exception) new InvalidCastException($"The given 'sink' object does not implement '{eventInterface.Name}'.");
        else
          this.connectionPoint.Advise(sink, out this.cookie);
      }
      else
        exception = (Exception) new InvalidCastException("The source object does not expose IConnectionPointContainer.");
      if (this.connectionPoint != null && this.cookie != 0)
        return;
      if (this.connectionPoint != null)
        Marshal.ReleaseComObject((object) this.connectionPoint);
      if (exception == null)
        throw new ArgumentException($"Could not create connection point for event interface '{eventInterface.Name}'.");
      throw exception;
    }

    public void Disconnect()
    {
      if (this.connectionPoint == null || this.cookie == 0)
        return;
      this.connectionPoint.Unadvise(this.cookie);
      this.cookie = 0;
      Marshal.ReleaseComObject((object) this.connectionPoint);
      this.connectionPoint = (NativeMethods.IConnectionPoint) null;
    }

    ~ConnectionPointCookie() => this.Disconnect();
  }

  private class WebBrowserEvents : IWebBrowserEvents
  {
    private HtmlBrowser browser;

    public WebBrowserEvents(HtmlBrowser browser) => this.browser = browser;

    public void BeforeNavigate(
      string url,
      int flags,
      string targetFrameName,
      ref object postData,
      string headers,
      ref bool cancel)
    {
      HtmlBrowserNavigatingEventArgs e = new HtmlBrowserNavigatingEventArgs(url, false);
      this.browser.PerformNavigating(e);
      cancel = e.Cancel;
    }

    public void DownloadBegin()
    {
    }

    public void CommandStateChange([In] int Command, [In] bool Enable)
    {
    }

    public void ProgressChange([In] int Progress, [In] int ProgressMax)
    {
    }

    public void NavigateComplete(string url)
    {
      this.browser.PerformNavigated(new HtmlBrowserNavigatedEventArgs(url));
    }
  }

  private class WebBrowserEvents2 : IWebBrowserEvents2
  {
    private HtmlBrowser browser;

    public WebBrowserEvents2(HtmlBrowser browser) => this.browser = browser;

    public void DocumentComplete(object dispatch, ref string url)
    {
      this.browser.PerformDocumentLoaded(new HtmlBrowserNavigatedEventArgs(url));
    }
  }
}
