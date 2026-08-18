// Decompiled with JetBrains decompiler
// Type: Interop.CADInterface.ICADSystem
// Assembly: Interop.CADInterface, Version=7.4.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 483F07A3-5DB3-4173-82E9-08ADF3509A91
// Assembly location: D:\IPS\Client\Interop.CADInterface.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.CADInterface;

[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
[Guid("7ACAF2B6-0BF0-4E1E-A120-EAF2346D0519")]
[ComImport]
public interface ICADSystem
{
  [DispId(1)]
  bool IsCADLoaded { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
  string[] GetLoadedFiles([In] bool bOnlyVisible);

  [DispId(5)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  bool GetDocument(
    [MarshalAs(UnmanagedType.BStr), In] string bstrFullPath,
    [In] bool bOpenVisible,
    [MarshalAs(UnmanagedType.Interface), In] IFileResolver pFileResolver,
    [MarshalAs(UnmanagedType.Interface)] out ICADDocument ppDocument);

  [DispId(6)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  ICADDocument CreateDocument(
    [MarshalAs(UnmanagedType.BStr), In] string bstrFullPath,
    [In] ECADDocType iDocType,
    [MarshalAs(UnmanagedType.BStr), In] string bstrTemplate,
    [In] bool bOpenVisible);

  [DispId(7)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.BStr)]
  string GetLastErrorMessage();

  [DispId(8)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  int GetVersion();

  [DispId(9)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.BStr)]
  string GetCADSystemName();

  [DispId(10)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.BStr)]
  string GetFileExtensions([In] ECADDocType iDocType);

  [DispId(11)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Struct)]
  object GetCADProperty([In] ECADProperty iProperty);

  [DispId(12)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
  string[] GetDocumentVersions([MarshalAs(UnmanagedType.BStr), In] string bstrDocFullPath);

  [DispId(13)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void SetAttributeLocalizer([MarshalAs(UnmanagedType.Interface)] AttributeLocalizer pLocalizer);
}
