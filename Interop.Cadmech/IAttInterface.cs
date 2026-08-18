// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.IAttInterface
// Assembly: Interop.Cadmech, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: A1A71B56-DCA0-4F89-92FC-C18B5CDBF5DD
// Assembly location: D:\IPS\Client\Interop.Cadmech.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.Cadmech;

[Guid("FB581978-B214-493F-A1CA-584AA076671A")]
[TypeLibType(4288)]
[ComImport]
public interface IAttInterface
{
  [DispId(1)]
  bool Ready { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

  [DispId(2)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.BStr)]
  string GetCommonAttributes();

  [DispId(3)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.BStr)]
  string GetFaceAttributes();
}
