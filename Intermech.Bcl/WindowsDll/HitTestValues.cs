
// Type: Intermech.WindowsDll.HitTestValues
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.WindowsDll
{
    public enum HitTestValues
    {
      /// <summary>
      /// On the screen background or on a dividing line between windows
      /// (same as <see cref="F:Intermech.WindowsDll.HitTestValues.HTNOWHERE" />, except that the DefWindowProc function produces a system beep to indicate an error).
      /// </summary>
      HTERROR = -2, // 0xFFFFFFFE
      /// <summary>
      /// In a window currently covered by another window in the same thread
      /// (the message will be sent to underlying windows in the same thread until one of them returns a code that is not <see cref="F:Intermech.WindowsDll.HitTestValues.HTTRANSPARENT" />).
      /// </summary>
      HTTRANSPARENT = -1, // 0xFFFFFFFF
      /// <summary>On the screen background or on a dividing line between windows</summary>
      HTNOWHERE = 0,
      /// <summary>In a client area</summary>
      HTCLIENT = 1,
      /// <summary>In a title bar</summary>
      HTCAPTION = 2,
      /// <summary>In a window menu or in a Close button in a child window</summary>
      HTSYSMENU = 3,
      /// <summary>In a size box (same as <see cref="F:Intermech.WindowsDll.HitTestValues.HTSIZE" />)</summary>
      HTGROWBOX = 4,
      /// <summary>In a size box (same as <see cref="F:Intermech.WindowsDll.HitTestValues.HTGROWBOX" />)</summary>
      HTSIZE = 4,
      /// <summary>In a menu</summary>
      HTMENU = 5,
      /// <summary>In a horizontal scroll bar</summary>
      HTHSCROLL = 6,
      /// <summary>In the vertical scroll bar</summary>
      HTVSCROLL = 7,
      /// <summary>In a Minimize button</summary>
      HTMINBUTTON = 8,
      /// <summary>In a Minimize button</summary>
      HTREDUCE = 8,
      /// <summary>In a Maximize button</summary>
      HTMAXBUTTON = 9,
      /// <summary>In a Maximize button</summary>
      HTZOOM = 9,
      /// <summary>In the left border of a resizable window (the user can click the mouse to resize the window horizontally)</summary>
      HTLEFT = 10, // 0x0000000A
      /// <summary>In the right border of a resizable window (the user can click the mouse to resize the window horizontally)</summary>
      HTRIGHT = 11, // 0x0000000B
      /// <summary>In the upper-horizontal border of a window</summary>
      HTTOP = 12, // 0x0000000C
      /// <summary>In the upper-left corner of a window border</summary>
      HTTOPLEFT = 13, // 0x0000000D
      /// <summary>In the upper-right corner of a window border</summary>
      HTTOPRIGHT = 14, // 0x0000000E
      /// <summary>In the lower-horizontal border of a resizable window (the user can click the mouse to resize the window vertically)</summary>
      HTBOTTOM = 15, // 0x0000000F
      /// <summary>In the lower-left corner of a border of a resizable window (the user can click the mouse to resize the window diagonally)</summary>
      HTBOTTOMLEFT = 16, // 0x00000010
      /// <summary>In the lower-right corner of a border of a resizable window (the user can click the mouse to resize the window diagonally)</summary>
      HTBOTTOMRIGHT = 17, // 0x00000011
      /// <summary>In the border of a window that does not have a sizing border</summary>
      HTBORDER = 18, // 0x00000012
      /// <summary>In a Close button</summary>
      HTCLOSE = 20, // 0x00000014
      /// <summary>In a Help button</summary>
      HTHELP = 21, // 0x00000015
    }
}
