// Decompiled with JetBrains decompiler
// Type: Interop.CADInterface.IStructureManager
// Assembly: Interop.CADInterface, Version=7.4.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 483F07A3-5DB3-4173-82E9-08ADF3509A91
// Assembly location: D:\IPS\Client\Interop.CADInterface.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.CADInterface;

[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
[Guid("2011F2F2-6204-4584-B4D7-6C82B6A5AABD")]
[ComImport]
public interface IStructureManager
{
  [DispId(1)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void SetAttributeLocalizer([MarshalAs(UnmanagedType.Interface)] AttributeLocalizer pLocalizer);

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_DISPATCH)]
  StructureElement[] GetStructureForAVS([MarshalAs(UnmanagedType.Interface), In] ICADDocument pCADDocument);

  [DispId(3)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void CommitChanges();

  [DispId(4)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void AddRecordsFromAVS([MarshalAs(UnmanagedType.Interface), In] ICADDocument pCADDocument, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_DISPATCH), In] IParametersContainer[] pVal);

  [DispId(5)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void ImportFromModel([MarshalAs(UnmanagedType.Interface), In] ICADDrawing pDrawing);

  [DispId(6)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_DISPATCH)]
  PositionNote[] GetMissingNotes();

  [DispId(7)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  StructureElement AddStructureElement(
    [In] bool vbSameForAllConfigurations,
    [MarshalAs(UnmanagedType.BStr), In] string bstrDesignation,
    [MarshalAs(UnmanagedType.BStr), In] string bstrName,
    [MarshalAs(UnmanagedType.BStr), In] string bstrIMBaseKey);
}
