
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.WindowStyles
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView;

/// <summary>
/// Window Styles.
/// The following styles can be specified wherever a window style is required. After the control has been created, these styles cannot be modified, except as noted.
/// </summary>
[Flags]
internal enum WindowStyles : long
{
  /// <summary>The window has a thin-line border.</summary>
  WS_BORDER = 8388608, // 0x0000000000800000
  /// <summary>The window has a title bar (includes the WS_BORDER style).</summary>
  WS_CAPTION = 12582912, // 0x0000000000C00000
  /// <summary>The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.</summary>
  WS_CHILD = 1073741824, // 0x0000000040000000
  /// <summary>Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.</summary>
  WS_CLIPCHILDREN = 33554432, // 0x0000000002000000
  /// <summary>
  /// Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated.
  /// If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.
  /// </summary>
  WS_CLIPSIBLINGS = 67108864, // 0x0000000004000000
  /// <summary>The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function.</summary>
  WS_DISABLED = 134217728, // 0x0000000008000000
  /// <summary>The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.</summary>
  WS_DLGFRAME = 4194304, // 0x0000000000400000
  /// <summary>
  /// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style.
  /// The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
  /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
  /// </summary>
  WS_GROUP = 131072, // 0x0000000000020000
  /// <summary>The window has a horizontal scroll bar.</summary>
  WS_HSCROLL = 1048576, // 0x0000000000100000
  /// <summary>The window is initially maximized.</summary>
  WS_MAXIMIZE = 16777216, // 0x0000000001000000
  /// <summary>The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
  WS_MAXIMIZEBOX = 65536, // 0x0000000000010000
  /// <summary>The window is initially minimized.</summary>
  WS_MINIMIZE = 536870912, // 0x0000000020000000
  /// <summary>The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
  WS_MINIMIZEBOX = WS_GROUP, // 0x0000000000020000
  /// <summary>The window is an overlapped window. An overlapped window has a title bar and a border.</summary>
  WS_OVERLAPPED = 0,
  /// <summary>The window is an overlapped window.</summary>
  WS_OVERLAPPEDWINDOW = 13565952, // 0x0000000000CF0000
  /// <summary>The window is a pop-up window. This style cannot be used with the WS_CHILD style.</summary>
  WS_POPUP = 2147483648, // 0x0000000080000000
  /// <summary>The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.</summary>
  WS_POPUPWINDOW = 2156396544, // 0x0000000080880000
  /// <summary>The window has a sizing border.</summary>
  WS_SIZEFRAME = 262144, // 0x0000000000040000
  /// <summary>The window has a window menu on its title bar. The WS_CAPTION style must also be specified.</summary>
  WS_SYSMENU = 524288, // 0x0000000000080000
  /// <summary>
  /// The window is a control that can receive the keyboard focus when the user presses the TAB key.
  /// Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.
  /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
  /// For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.
  /// </summary>
  WS_TABSTOP = WS_MAXIMIZEBOX, // 0x0000000000010000
  /// <summary>The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function.</summary>
  WS_VISIBLE = 268435456, // 0x0000000010000000
  /// <summary>The window has a vertical scroll bar.</summary>
  WS_VSCROLL = 2097152, // 0x0000000000200000
}
