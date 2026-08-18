// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.IMDoc_COMClass
// Assembly: Interop.IMText, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 429E38D4-3785-4B44-8CD1-02E4A9CDD7BF
// Assembly location: D:\IPS\Client\Interop.IMText.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.Cadmech;

[ClassInterface(ClassInterfaceType.None)]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[Guid("7239CC0A-0B7B-4A17-9371-4C1CC234F85C")]
[ComImport]
public class IMDoc_COMClass : IIMDoc, IMDoc_COM, IIMDoc2, IIMDoc3
{
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public extern IMDoc_COMClass();

  [DispId(1)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  public virtual extern IMAttrManager_COM GetAttrManager();

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void ImportOldStructureInfo(
    [MarshalAs(UnmanagedType.IUnknown), In] object pStructureManager,
    [MarshalAs(UnmanagedType.IUnknown), In] object pAttributeLocalizer);

  [DispId(3)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void Activate();

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  public virtual extern IMAttrManager_COM IIMDoc2_GetAttrManager();

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void IIMDoc2_ImportOldStructureInfo(
    [MarshalAs(UnmanagedType.IUnknown), In] object pStructureManager,
    [MarshalAs(UnmanagedType.IUnknown), In] object pAttributeLocalizer);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void IIMDoc2_Activate();

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void AutoSRLeaderCmd([In] int hwndParent, [MarshalAs(UnmanagedType.IUnknown)] object pStructureManager);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  public virtual extern IMAttrManager_COM IIMDoc3_GetAttrManager();

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void IIMDoc3_ImportOldStructureInfo(
    [MarshalAs(UnmanagedType.IUnknown), In] object pStructureManager,
    [MarshalAs(UnmanagedType.IUnknown), In] object pAttributeLocalizer);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void IIMDoc3_Activate();

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void IIMDoc3_AutoSRLeaderCmd([In] int hwndParent, [MarshalAs(UnmanagedType.IUnknown)] object pStructureManager);

  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void SaveViewerFile(
    [MarshalAs(UnmanagedType.BStr), In] string sOutputFolder,
    [In] bool bOnlyThisFile,
    [In] bool bInsideOneFile,
    [MarshalAs(UnmanagedType.BStr)] out string psViewerFileName);
}
