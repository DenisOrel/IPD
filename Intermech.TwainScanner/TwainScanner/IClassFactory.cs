// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.IClassFactory
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TwainScanner;

/// <summary>
/// You must implement this interface for every class that you register in
/// the system registry and to which you assign a CLSID, so objects of that
/// class can be created.
/// http://msdn.microsoft.com/en-us/library/ms694364.aspx
/// </summary>
[ComVisible(false)]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("00000001-0000-0000-C000-000000000046")]
[ComImport]
internal interface IClassFactory
{
  /// <summary>Creates an uninitialized object.</summary>
  /// <param name="pUnkOuter"></param>
  /// <param name="riid">
  /// Reference to the identifier of the interface to be used to
  /// communicate with the newly created object. If pUnkOuter is NULL, this
  /// parameter is frequently the IID of the initializing interface.
  /// </param>
  /// <param name="ppvObject">
  /// Address of pointer variable that receives the interface pointer
  /// requested in riid.
  /// </param>
  /// <returns>S_OK means success.</returns>
  [MethodImpl(MethodImplOptions.PreserveSig)]
  int CreateInstance(IntPtr pUnkOuter, ref Guid riid, out IntPtr ppvObject);

  /// <summary>Locks object application open in memory.</summary>
  /// <param name="fLock">
  /// If TRUE, increments the lock count;
  /// if FALSE, decrements the lock count.
  /// </param>
  /// <returns>S_OK means success.</returns>
  [MethodImpl(MethodImplOptions.PreserveSig)]
  int LockServer(bool fLock);
}
