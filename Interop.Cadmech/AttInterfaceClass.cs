// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.AttInterfaceClass
// Assembly: Interop.Cadmech, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: A1A71B56-DCA0-4F89-92FC-C18B5CDBF5DD
// Assembly location: D:\IPS\Client\Interop.Cadmech.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.Cadmech;

[ClassInterface(0)]
[Guid("578B84D2-BCF9-49C6-9F25-6D4643366E3D")]
[TypeLibType(2)]
[ComImport]
public class AttInterfaceClass : IAttInterface, AttInterface
{
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public extern AttInterfaceClass();

  [DispId(1)]
  public virtual extern bool Ready { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.BStr)]
  public virtual extern string GetCommonAttributes();

  [DispId(3)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.BStr)]
  public virtual extern string GetFaceAttributes();
}
