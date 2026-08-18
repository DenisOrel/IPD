
// Type: KGAXLib._DKGAXEvents
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace KGAXLib;

[CompilerGenerated]
[Guid("464F746A-AC6D-4919-82E9-A7363E661ECF")]
[InterfaceType(2)]
[TypeIdentifier]
[ComImport]
public interface _DKGAXEvents
{
  [DispId(1)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnKgMouseDown([In] short nButton, [In] short nShiftState, [In] int x, [In] int y, out bool proceed);

  [DispId(2)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnKgMouseUp([In] short nButton, [In] short nShiftState, [In] int x, [In] int y, out bool proceed);

  [DispId(3)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnKgMouseDblClick([In] short nButton, [In] short nShiftState, [In] int x, [In] int y, out bool proceed);

  [DispId(4)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnKgStopCurrentProcess();

  [DispId(5)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnKgCreate([In] int docID);

  [DispId(6)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnKgPaint([MarshalAs(UnmanagedType.Interface), In] PaintObject paintObj);

  [DispId(7)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnKgCreateGLList([MarshalAs(UnmanagedType.Interface), In] GLObject glObj, [In] KDocument3DDrawMode drawMode);

  [DispId(8)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnKgAddGabatit([MarshalAs(UnmanagedType.Interface), In] GabaritObject gabObj);

  [DispId(9)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnKgErrorLoadDocument([In] int docID, [MarshalAs(UnmanagedType.BStr), In] string fileName, [In] int errorID);

  [DispId(10)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnKgKeyDown([In, Out] ref int key, [In] short nShiftState);

  [DispId(11)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnKgKeyUp([In, Out] ref int key, [In] short nShiftState);

  [DispId(12)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void OnKgKeyPress([In, Out] ref int key);
}
