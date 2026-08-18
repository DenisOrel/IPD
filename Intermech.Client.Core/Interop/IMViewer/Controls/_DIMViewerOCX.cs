
// Type: Interop.IMViewer.Controls._DIMViewerOCX
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Interop.IMViewer.Controls;

[CompilerGenerated]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[Guid("121CD459-50CB-45FF-B099-85358D4E9444")]
[TypeIdentifier]
[ComImport]
public interface _DIMViewerOCX
{
  [DispId(-552)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void AboutBox();

  [DispId(1)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  bool Open([MarshalAs(UnmanagedType.BStr)] string sFullPath);

  [DispId(2)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  bool GetCadmechCOM([MarshalAs(UnmanagedType.IUnknown)] ref object ppCadmech);

  [DispId(3)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  bool GetIMViewerApp([MarshalAs(UnmanagedType.IUnknown)] ref object ppApp);

  [DispId(4)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  bool OpenConfig([MarshalAs(UnmanagedType.BStr)] string sFullPath, [MarshalAs(UnmanagedType.BStr)] string sConfigName);
}
