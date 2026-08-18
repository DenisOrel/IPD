// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.AttributeInterfaceAddInServerClass
// Assembly: Interop.Cadmech, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: A1A71B56-DCA0-4F89-92FC-C18B5CDBF5DD
// Assembly location: D:\IPS\Client\Interop.Cadmech.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.Cadmech;

[ClassInterface(0)]
[TypeLibType(2)]
[Guid("F5302EBF-B181-4EC0-8865-97CC84E3A5BA")]
[ComImport]
public class AttributeInterfaceAddInServerClass : 
  IAttributeInterfaceAddInServer,
  AttributeInterfaceAddInServer
{
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public extern AttributeInterfaceAddInServerClass();

  [DispId(50336257)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void Activate([MarshalAs(UnmanagedType.IDispatch), In] object pDisp, [In] bool FirstTime);

  [DispId(50336258)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void Deactivate();

  [DispId(50336259)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public virtual extern void ExecuteCommand([In] int CommandID);

  [DispId(50336260)]
  public virtual extern object Automation { [DispId(50336260), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }
}
