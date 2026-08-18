// Decompiled with JetBrains decompiler
// Type: Interop.CADInterface.AttributeLocalizerClass
// Assembly: Interop.CADInterface, Version=7.4.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 483F07A3-5DB3-4173-82E9-08ADF3509A91
// Assembly location: D:\IPS\Client\Interop.CADInterface.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Interop.CADInterface;

[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[Guid("2D8D2CE8-D5C8-44EC-93DE-6EC517AD953F")]
[ClassInterface(ClassInterfaceType.None)]
[ComImport]
public class AttributeLocalizerClass : IAttributeLocalizer, AttributeLocalizer
{
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  public extern AttributeLocalizerClass();

  [DispId(1)]
  [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  [return: MarshalAs(UnmanagedType.BStr)]
  public virtual extern string GetAttributeNameByID(EAttributeID ID);
}
