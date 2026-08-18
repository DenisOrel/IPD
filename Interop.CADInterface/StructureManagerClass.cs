// Decompiled with JetBrains decompiler
// Type: Interop.CADInterface.StructureManagerClass
// Assembly: Interop.CADInterface, Version=7.4.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 483F07A3-5DB3-4173-82E9-08ADF3509A91
// Assembly location: D:\IPS\Client\Interop.CADInterface.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.CADInterface;

[ClassInterface(ClassInterfaceType.None)]
[Guid("8581D16F-3892-4D5F-B9AB-CB8173CD8EFC")]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[ComImport]
public class StructureManagerClass : IStructureManager, StructureManager
{
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public extern StructureManagerClass();

  [DispId(1)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void SetAttributeLocalizer([MarshalAs(UnmanagedType.Interface)] AttributeLocalizer pLocalizer);

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_DISPATCH)]
  public virtual extern StructureElement[] GetStructureForAVS([MarshalAs(UnmanagedType.Interface), In] ICADDocument pCADDocument);

  [DispId(3)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void CommitChanges();

  [DispId(4)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void AddRecordsFromAVS(
    [MarshalAs(UnmanagedType.Interface), In] ICADDocument pCADDocument,
    [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_DISPATCH), In] IParametersContainer[] pVal);

  [DispId(5)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void ImportFromModel([MarshalAs(UnmanagedType.Interface), In] ICADDrawing pDrawing);

  [DispId(6)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_DISPATCH)]
  public virtual extern PositionNote[] GetMissingNotes();

  [DispId(7)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  public virtual extern StructureElement AddStructureElement(
    [In] bool vbSameForAllConfigurations,
    [MarshalAs(UnmanagedType.BStr), In] string bstrDesignation,
    [MarshalAs(UnmanagedType.BStr), In] string bstrName,
    [MarshalAs(UnmanagedType.BStr), In] string bstrIMBaseKey);
}
