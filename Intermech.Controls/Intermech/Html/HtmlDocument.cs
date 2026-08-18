
// Type: Intermech.Html.HtmlDocument
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml


namespace Intermech.Html;

public class HtmlDocument
{
  private NativeMethods.IHtmlDocument2 document;

  internal HtmlDocument(NativeMethods.IHtmlDocument2 document) => this.document = document;

  public HtmlElement Body
  {
    get
    {
      NativeMethods.IHtmlElement body = this.document.GetBody();
      return body != null ? new HtmlElement(body) : (HtmlElement) null;
    }
  }
}
