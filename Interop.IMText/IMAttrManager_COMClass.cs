// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.IMAttrManager_COMClass
// Assembly: Interop.IMText, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 429E38D4-3785-4B44-8CD1-02E4A9CDD7BF
// Assembly location: D:\IPS\Client\Interop.IMText.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.Cadmech;

[ComSourceInterfaces("Interop.Cadmech.IIMAttrManagerEvents\0\0")]
[Guid("388250B6-ADAD-42A2-BD9A-DF5450931AFA")]
[ClassInterface(ClassInterfaceType.None)]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[ComImport]
public class IMAttrManager_COMClass : 
  IIMAttrManager,
  IMAttrManager_COM,
  IIMAttrManagerEvents_Event,
  IIMAttrManager2
{
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public extern IMAttrManager_COMClass();

  [DispId(1)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  public virtual extern IMFaceAttr_COM CreateFaceAttrCmd(
    [In] EAttrType signType,
    [MarshalAs(UnmanagedType.Interface), In] IMFace_COM pFace,
    [In] int hwndParent);

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  public virtual extern IMFaceAttr_COM CreateFaceAttr([In] EAttrType signType);

  [DispId(3)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  public virtual extern IMFaceAttr_COM FindAttr([MarshalAs(UnmanagedType.BStr), In] string bstrGUID);

  [DispId(4)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_DISPATCH)]
  public virtual extern IMFaceAttr_COM[] GetAllFaceAttrsByType([In] EAttrType Type);

  [DispId(5)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_DISPATCH)]
  public virtual extern IMFace_COM[] GetFaces();

  [DispId(6)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.IDispatch)]
  public virtual extern object SelectObject([MarshalAs(UnmanagedType.BStr), In] string prompt, [MarshalAs(UnmanagedType.IDispatch), In] object pAttr, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_I4), In] IM_EntityId[] preFilter);

  public virtual extern event IIMAttrManagerEvents_OnAttributeAddedEventHandler OnAttributeAdded;

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void add_OnAttributeAdded(
    [In] IIMAttrManagerEvents_OnAttributeAddedEventHandler obj0);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void remove_OnAttributeAdded(
    [In] IIMAttrManagerEvents_OnAttributeAddedEventHandler obj0);

  public virtual extern event IIMAttrManagerEvents_OnAttributeDeletedEventHandler OnAttributeDeleted;

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void add_OnAttributeDeleted(
    [In] IIMAttrManagerEvents_OnAttributeDeletedEventHandler obj0);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void remove_OnAttributeDeleted(
    [In] IIMAttrManagerEvents_OnAttributeDeletedEventHandler obj0);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void add_OnAttributeChanged(
    [In] IIMAttrManagerEvents_OnAttributeChangedEventHandler obj0);

  public virtual extern event IIMAttrManagerEvents_OnAttributeChangedEventHandler OnAttributeChanged;

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void remove_OnAttributeChanged(
    [In] IIMAttrManagerEvents_OnAttributeChangedEventHandler obj0);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  public virtual extern IMFaceAttr_COM IIMAttrManager2_CreateFaceAttrCmd(
    [In] EAttrType signType,
    [MarshalAs(UnmanagedType.Interface), In] IMFace_COM pFace,
    [In] int hwndParent);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  public virtual extern IMFaceAttr_COM IIMAttrManager2_CreateFaceAttr([In] EAttrType signType);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  public virtual extern IMFaceAttr_COM IIMAttrManager2_FindAttr([MarshalAs(UnmanagedType.BStr), In] string bstrGUID);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_DISPATCH)]
  public virtual extern IMFaceAttr_COM[] IIMAttrManager2_GetAllFaceAttrsByType([In] EAttrType Type);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_DISPATCH)]
  public virtual extern IMFace_COM[] IIMAttrManager2_GetFaces();

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.IDispatch)]
  public virtual extern object IIMAttrManager2_SelectObject(
    [MarshalAs(UnmanagedType.BStr), In] string prompt,
    [MarshalAs(UnmanagedType.IDispatch), In] object pAttr,
    [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_I4), In] IM_EntityId[] preFilter);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  public virtual extern IIMFaceAttr2 CreateFaceAttr2([In] EAttrType signType);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  public virtual extern IIMFaceAttr2 FindAttr2([MarshalAs(UnmanagedType.BStr), In] string bstrGUID);
}
