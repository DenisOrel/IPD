// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.IIMFaceAttrTT2
// Assembly: Interop.IMText, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 429E38D4-3785-4B44-8CD1-02E4A9CDD7BF
// Assembly location: D:\IPS\Client\Interop.IMText.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.Cadmech;

[Guid("5F67A5F1-68AB-4D15-962C-22B17A965EAF")]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
[ComImport]
public interface IIMFaceAttrTT2
{
  [DispId(1)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  int GetItemIndex([MarshalAs(UnmanagedType.BStr), In] string item);

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.BStr)]
  string GetItemText([MarshalAs(UnmanagedType.BStr), In] string item);

  [DispId(3)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
  string[] GetItems();

  [DispId(4)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
  string[] GetExtRefs([MarshalAs(UnmanagedType.BStr), In] string text);
}
