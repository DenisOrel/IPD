// Decompiled with JetBrains decompiler
// Type: Interop.CADInterface.IPDMEvents
// Assembly: Interop.CADInterface, Version=7.4.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 483F07A3-5DB3-4173-82E9-08ADF3509A91
// Assembly location: D:\IPS\Client\Interop.CADInterface.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.CADInterface;

[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
[Guid("DF0F9D94-BA82-45A2-89BB-38D3FFD75605")]
[ComImport]
public interface IPDMEvents
{
  [DispId(1)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnBeforeCheckIn([MarshalAs(UnmanagedType.BStr), In] string bstrDocFullPath);

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnAfterCheckIn([MarshalAs(UnmanagedType.BStr), In] string bstrDocFullPath);

  [DispId(3)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnBeforeCheckOut([MarshalAs(UnmanagedType.BStr), In] string bstrDocFullPath);

  [DispId(4)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnAfterCheckOut([MarshalAs(UnmanagedType.BStr), In] string bstrDocFullPath);

  [DispId(5)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnBeforeCancelChanges([MarshalAs(UnmanagedType.BStr), In] string bstrDocFullPath);

  [DispId(6)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnAfterCancelChanges([MarshalAs(UnmanagedType.BStr), In] string bstrDocFullPath);
}
