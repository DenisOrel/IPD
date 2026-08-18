// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.IMFaceAttr_COMClass
// Assembly: Interop.IMText, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 429E38D4-3785-4B44-8CD1-02E4A9CDD7BF
// Assembly location: D:\IPS\Client\Interop.IMText.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.Cadmech;

[Guid("366E421A-464D-4A01-9B34-0AB65F8F31EE")]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[ClassInterface(ClassInterfaceType.None)]
[ComImport]
public class IMFaceAttr_COMClass : IIMFaceAttr, IMFaceAttr_COM, IIMFaceAttr2
{
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public extern IMFaceAttr_COMClass();

  [DispId(1)]
  public virtual extern string GUID { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void Edit([In] HandleMode hm, [In] int hwndParent, [MarshalAs(UnmanagedType.Interface), In] ref IMFace_COM ppFace);

  [DispId(3)]
  public virtual extern eLeaderStyle LeaderStyle { [DispId(3), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

  [DispId(4)]
  public virtual extern string[] Properties { [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] get; }

  [DispId(5)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Struct)]
  public virtual extern object get_Property([MarshalAs(UnmanagedType.BStr), In] string propName);

  [DispId(5)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void set_Property([MarshalAs(UnmanagedType.BStr), In] string propName, [MarshalAs(UnmanagedType.Struct), In] object pVal);

  [DispId(6)]
  public virtual extern IMFace_COM[] Faces { [DispId(6), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_DISPATCH)] get; }

  [DispId(7)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void Erase();

  [DispId(8)]
  public virtual extern EAttrType Type { [DispId(8), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

  public virtual extern string IIMFaceAttr2_GUID { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void IIMFaceAttr2_Edit(
    [In] HandleMode hm,
    [In] int hwndParent,
    [MarshalAs(UnmanagedType.Interface), In] ref IMFace_COM ppFace);

  public virtual extern eLeaderStyle IIMFaceAttr2_LeaderStyle { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

  public virtual extern string[] IIMFaceAttr2_Properties { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] get; }

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Struct)]
  public virtual extern object IIMFaceAttr2_get_Property([MarshalAs(UnmanagedType.BStr), In] string propName);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void IIMFaceAttr2_set_Property([MarshalAs(UnmanagedType.BStr), In] string propName, [MarshalAs(UnmanagedType.Struct), In] object pVal);

  public virtual extern IMFace_COM[] IIMFaceAttr2_Faces { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_DISPATCH)] get; }

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void IIMFaceAttr2_Erase();

  public virtual extern EAttrType IIMFaceAttr2_Type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void Highlight([In] bool vbHighlight);
}
