// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.ICadmech
// Assembly: Interop.IMText, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 429E38D4-3785-4B44-8CD1-02E4A9CDD7BF
// Assembly location: D:\IPS\Client\Interop.IMText.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.Cadmech;

[Guid("DB9D30D6-251B-44F6-85ED-C50FF114D1FE")]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
[ComImport]
public interface ICadmech
{
  [DispId(1)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.Interface)]
  IMDoc_COM GetDocument([MarshalAs(UnmanagedType.BStr), In] string bstrDocName);
}
