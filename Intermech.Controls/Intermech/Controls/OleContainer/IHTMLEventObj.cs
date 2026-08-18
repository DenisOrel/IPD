
// Type: Intermech.Controls.OleContainer.IHTMLEventObj
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Runtime.InteropServices;
using System.Security;


namespace Intermech.Controls.OleContainer;

[InterfaceType(ComInterfaceType.InterfaceIsDual)]
[SuppressUnmanagedCodeSecurity]
[ComVisible(true)]
[Guid("3050F32D-98B5-11CF-BB82-00AA00BDCE0B")]
public interface IHTMLEventObj
{
  [return: MarshalAs(UnmanagedType.Interface)]
  UnsafeMethods.IHTMLElement GetSrcElement();

  bool GetAltKey();

  bool GetCtrlKey();

  bool GetShiftKey();

  void SetReturnValue(bool p);

  bool GetReturnValue();

  void SetCancelBubble(bool p);

  bool GetCancelBubble();

  [return: MarshalAs(UnmanagedType.Interface)]
  UnsafeMethods.IHTMLElement GetFromElement();

  [return: MarshalAs(UnmanagedType.Interface)]
  UnsafeMethods.IHTMLElement GetToElement();

  void SetKeyCode([In] int p);

  int GetKeyCode();

  int GetButton();

  string GetEventType();

  string GetQualifier();

  int GetReason();

  int GetX();

  int GetY();

  int GetClientX();

  int GetClientY();

  int GetOffsetX();

  int GetOffsetY();

  int GetScreenX();

  int GetScreenY();

  object GetSrcFilter();
}
