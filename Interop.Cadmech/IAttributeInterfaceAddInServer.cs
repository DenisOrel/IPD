// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.IAttributeInterfaceAddInServer
// Assembly: Interop.Cadmech, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: A1A71B56-DCA0-4F89-92FC-C18B5CDBF5DD
// Assembly location: D:\IPS\Client\Interop.Cadmech.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.Cadmech;

[TypeLibType(4160)]
[Guid("144B99FC-11C8-42C1-BDBC-8A55F3D4EDCB")]
[ComImport]
public interface IAttributeInterfaceAddInServer
{
  [DispId(50336257)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void Activate([MarshalAs(UnmanagedType.IDispatch), In] object pDisp, [In] bool FirstTime);

  [DispId(50336258)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void Deactivate();

  [DispId(50336259)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void ExecuteCommand([In] int CommandID);

  [DispId(50336260)]
  object Automation { [DispId(50336260), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }
}
