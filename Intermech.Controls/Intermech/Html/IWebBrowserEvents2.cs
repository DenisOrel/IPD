
// Type: Intermech.Html.IWebBrowserEvents2
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.InteropServices;


namespace Intermech.Html;

[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D")]
[ComImport]
public interface IWebBrowserEvents2
{
  [DispId(259)]
  void DocumentComplete([MarshalAs(UnmanagedType.IDispatch), In] object dispatch, [In] ref string url);
}
