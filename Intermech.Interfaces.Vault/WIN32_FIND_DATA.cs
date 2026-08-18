// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.WIN32_FIND_DATA
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Vault.Interfaces;

[BestFitMapping(false)]
[Serializable]
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
internal class WIN32_FIND_DATA
{
  internal int dwFileAttributes;
  internal int ftCreationTime_dwLowDateTime;
  internal int ftCreationTime_dwHighDateTime;
  internal int ftLastAccessTime_dwLowDateTime;
  internal int ftLastAccessTime_dwHighDateTime;
  internal int ftLastWriteTime_dwLowDateTime;
  internal int ftLastWriteTime_dwHighDateTime;
  internal int nFileSizeHigh;
  internal int nFileSizeLow;
  internal int dwReserved0;
  internal int dwReserved1;
  [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
  internal string cFileName;
  [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
  internal string cAlternateFileName;
}
