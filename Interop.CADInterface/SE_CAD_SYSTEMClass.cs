// Decompiled with JetBrains decompiler
// Type: Interop.CADInterface.SE_CAD_SYSTEMClass
// Assembly: Interop.CADInterface, Version=7.4.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 483F07A3-5DB3-4173-82E9-08ADF3509A91
// Assembly location: D:\IPS\Client\Interop.CADInterface.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.CADInterface;

[Guid("F909FC49-93BB-4A37-BE91-6809783852EF")]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[ClassInterface(ClassInterfaceType.None)]
[ComImport]
public class SE_CAD_SYSTEMClass : ICADSystem7, SE_CAD_SYSTEM
{
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public extern SE_CAD_SYSTEMClass();

  [DispId(1)]
  public virtual extern bool IsCADLoaded { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
  public virtual extern string[] GetLoadedFiles([In] bool bOnlyVisible);

  [DispId(5)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern bool GetDocument(
    [MarshalAs(UnmanagedType.BStr), In] string bstrFullPath,
    [In] bool bOpenVisible,
    [MarshalAs(UnmanagedType.Interface), In] IFileResolver pFileResolver,
    [MarshalAs(UnmanagedType.Interface)] out ICADDocument ppDocument);

  [DispId(6)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  public virtual extern ICADDocument CreateDocument(
    [MarshalAs(UnmanagedType.BStr), In] string bstrFullPath,
    [In] ECADDocType iDocType,
    [MarshalAs(UnmanagedType.BStr), In] string bstrTemplate,
    [In] bool bOpenVisible);

  [DispId(7)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.BStr)]
  public virtual extern string GetLastErrorMessage();

  [DispId(8)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern int GetVersion();

  [DispId(9)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.BStr)]
  public virtual extern string GetCADSystemName();

  [DispId(10)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.BStr)]
  public virtual extern string GetFileExtensions([In] ECADDocType iDocType);

  [DispId(11)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Struct)]
  public virtual extern object GetCADProperty([In] ECADProperty iProperty);

  [DispId(12)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
  public virtual extern string[] GetDocumentVersions([MarshalAs(UnmanagedType.BStr), In] string bstrDocFullPath);

  [DispId(13)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void SetAttributeLocalizer([MarshalAs(UnmanagedType.Interface)] AttributeLocalizer pLocalizer);

  [DispId(14)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void GetActiveDocument([MarshalAs(UnmanagedType.Interface)] out ICADDocument2 ppDocument);

  [DispId(15)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void Activate();

  [DispId(16 /*0x10*/)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void GetDocument2(
    [MarshalAs(UnmanagedType.BStr), In] string bstrFullPath,
    bool bOpenVisible,
    [MarshalAs(UnmanagedType.Interface)] out ICADDocument2 ppDocument);

  [DispId(17)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern Guid GetGUID();

  [DispId(18)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void SetWorkingFolder([MarshalAs(UnmanagedType.BStr), In] string bstrWorkingFolder);

  [DispId(19)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern EOpenStatus GetDocumentStatus([MarshalAs(UnmanagedType.BStr), In] string bstrFullPath);

  [DispId(20)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void GetActiveMode(out ECADSystemModeType pRetVal);

  [DispId(21)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void GroupOperation([In] EGroupOperationTypes eOperationType, [In] bool vbStart);

  [DispId(22)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void GetDocumentWithReplaceArray(
    [MarshalAs(UnmanagedType.BStr), In] string bstrFullPath,
    [In] bool bOpenVisible,
    [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR), In] string[] saWhatToReplace,
    [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR), In] string[] saToReplaceWith,
    [MarshalAs(UnmanagedType.Interface)] out ICADDocument3 ppDocument);

  [DispId(23)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
  public virtual extern string[] GetExportFormats([In] ECADDocType iDocType);

  [DispId(24)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void SetActiveMode([In] ECADSystemModeType ModeType);

  [DispId(25)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void Clone([MarshalAs(UnmanagedType.Interface), In] CloneData pCloneData);
}
