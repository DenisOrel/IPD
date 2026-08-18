// Decompiled with JetBrains decompiler
// Type: Interop.CADInterface.ICloneProgressSink
// Assembly: Interop.CADInterface, Version=7.4.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 483F07A3-5DB3-4173-82E9-08ADF3509A91
// Assembly location: D:\IPS\Client\Interop.CADInterface.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.CADInterface;

[Guid("D898868F-1140-4ABA-8400-8FF9FB9DDA80")]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
[ComImport]
public interface ICloneProgressSink
{
  [DispId(1)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  bool ItemCompleted([MarshalAs(UnmanagedType.Interface), In] CloneDataFile pDataFile);
}
