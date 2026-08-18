// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.IIMDoc3
// Assembly: Interop.IMText, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 429E38D4-3785-4B44-8CD1-02E4A9CDD7BF
// Assembly location: D:\IPS\Client\Interop.IMText.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.Cadmech;

[Guid("D9AAF7F8-E8AD-4374-8A41-4933338820D6")]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
[ComImport]
public interface IIMDoc3 : IIMDoc2
{
  [DispId(1)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  new IMAttrManager_COM GetAttrManager();

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  new void ImportOldStructureInfo([MarshalAs(UnmanagedType.IUnknown), In] object pStructureManager, [MarshalAs(UnmanagedType.IUnknown), In] object pAttributeLocalizer);

  [DispId(3)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  new void Activate();

  [DispId(4)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  new void AutoSRLeaderCmd([In] int hwndParent, [MarshalAs(UnmanagedType.IUnknown)] object pStructureManager);

  [DispId(5)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void SaveViewerFile(
    [MarshalAs(UnmanagedType.BStr), In] string sOutputFolder,
    [In] bool bOnlyThisFile,
    [In] bool bInsideOneFile,
    [MarshalAs(UnmanagedType.BStr)] out string psViewerFileName);
}
