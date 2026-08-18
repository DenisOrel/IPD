
// Type: Intermech.Html.IWebBrowserEvents
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Intermech.Html;

[Guid("EAB22AC2-30C1-11CF-A7EB-0000C05BAE0B")]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[ComImport]
public interface IWebBrowserEvents
{
  [DispId(100)]
  void BeforeNavigate(
    string url,
    int flags,
    string targetFrameName,
    ref object postData,
    string headers,
    ref bool cancel);

  [DispId(101)]
  void NavigateComplete(string url);

  [DispId(106)]
  void DownloadBegin();

  [DispId(105)]
  [MethodImpl(MethodImplOptions.PreserveSig)]
  void CommandStateChange([In] int Command, [In] bool Enable);

  [DispId(108)]
  [MethodImpl(MethodImplOptions.PreserveSig)]
  void ProgressChange([In] int Progress, [In] int ProgressMax);
}
