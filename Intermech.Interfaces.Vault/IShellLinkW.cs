// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.IShellLinkW
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Intermech.Vault.Interfaces;

/// <summary>The IShellLink interface allows Shell links to be created, modified, and resolved</summary>
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("000214F9-0000-0000-C000-000000000046")]
[ComImport]
internal interface IShellLinkW
{
  /// <summary>Retrieves the path and file name of a Shell link object</summary>
  void GetPath([MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder pszFile, int cchMaxPath, out WIN32_FIND_DATA pfd, SLGP_FLAGS fFlags);

  /// <summary>Retrieves the list of item identifiers for a Shell link object</summary>
  void GetIDList(out IntPtr ppidl);

  /// <summary>Sets the pointer to an item identifier list (PIDL) for a Shell link object.</summary>
  void SetIDList(IntPtr pidl);

  /// <summary>Retrieves the description string for a Shell link object</summary>
  void GetDescription([MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder pszName, int cchMaxName);

  /// <summary>Sets the description for a Shell link object. The description can be any application-defined string</summary>
  void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

  /// <summary>Retrieves the name of the working directory for a Shell link object</summary>
  void GetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder pszDir, int cchMaxPath);

  /// <summary>Sets the name of the working directory for a Shell link object</summary>
  void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

  /// <summary>Retrieves the command-line arguments associated with a Shell link object</summary>
  void GetArguments([MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder pszArgs, int cchMaxPath);

  /// <summary>Sets the command-line arguments for a Shell link object</summary>
  void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

  /// <summary>Retrieves the hot key for a Shell link object</summary>
  void GetHotkey(out short pwHotkey);

  /// <summary>Sets a hot key for a Shell link object</summary>
  void SetHotkey(short wHotkey);

  /// <summary>Retrieves the show command for a Shell link object</summary>
  void GetShowCmd(out int piShowCmd);

  /// <summary>Sets the show command for a Shell link object. The show command sets the initial show state of the window.</summary>
  void SetShowCmd(int iShowCmd);

  /// <summary>Retrieves the location (path and index) of the icon for a Shell link object</summary>
  void GetIconLocation([MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder pszIconPath, int cchIconPath, out int piIcon);

  /// <summary>Sets the location (path and index) of the icon for a Shell link object</summary>
  void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

  /// <summary>Sets the relative path to the Shell link object</summary>
  void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);

  /// <summary>Attempts to find the target of a Shell link, even if it has been moved or renamed</summary>
  void Resolve(IntPtr hwnd, SLR_FLAGS fFlags);

  /// <summary>Sets the path and file name of a Shell link object</summary>
  void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
}
