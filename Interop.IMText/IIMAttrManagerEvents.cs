// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.IIMAttrManagerEvents
// Assembly: Interop.IMText, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 429E38D4-3785-4B44-8CD1-02E4A9CDD7BF
// Assembly location: D:\IPS\Client\Interop.IMText.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.Cadmech;

[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[TypeLibType(TypeLibTypeFlags.FDual)]
[Guid("9121260F-2BB2-41B4-BCC9-C23DAB26F635")]
[ComImport]
public interface IIMAttrManagerEvents
{
  [DispId(1610678272 /*0x60010000*/)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnAttributeAdded([MarshalAs(UnmanagedType.Interface), In] IMFaceAttr_COM pAttr);

  [DispId(1610678273 /*0x60010001*/)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnAttributeDeleted([MarshalAs(UnmanagedType.Interface), In] IMFaceAttr_COM pAttr);

  [DispId(1610678274 /*0x60010002*/)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnAttributeChanged([MarshalAs(UnmanagedType.Interface), In] IMFaceAttr_COM pAttr);
}
