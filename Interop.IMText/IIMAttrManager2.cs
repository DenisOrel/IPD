// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.IIMAttrManager2
// Assembly: Interop.IMText, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 429E38D4-3785-4B44-8CD1-02E4A9CDD7BF
// Assembly location: D:\IPS\Client\Interop.IMText.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.Cadmech;

[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
[Guid("FF83A171-9505-480D-9D99-DE3FB9F7AE79")]
[ComImport]
public interface IIMAttrManager2 : IIMAttrManager
{
  [DispId(1)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  new IMFaceAttr_COM CreateFaceAttrCmd([In] EAttrType signType, [MarshalAs(UnmanagedType.Interface), In] IMFace_COM pFace, [In] int hwndParent);

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  new IMFaceAttr_COM CreateFaceAttr([In] EAttrType signType);

  [DispId(3)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  new IMFaceAttr_COM FindAttr([MarshalAs(UnmanagedType.BStr), In] string bstrGUID);

  [DispId(4)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_DISPATCH)]
  new IMFaceAttr_COM[] GetAllFaceAttrsByType([In] EAttrType Type);

  [DispId(5)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_DISPATCH)]
  new IMFace_COM[] GetFaces();

  [DispId(6)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.IDispatch)]
  new object SelectObject([MarshalAs(UnmanagedType.BStr), In] string prompt, [MarshalAs(UnmanagedType.IDispatch), In] object pAttr, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_I4), In] IM_EntityId[] preFilter);

  [DispId(7)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  IIMFaceAttr2 CreateFaceAttr2([In] EAttrType signType);

  [DispId(8)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  IIMFaceAttr2 FindAttr2([MarshalAs(UnmanagedType.BStr), In] string bstrGUID);
}
