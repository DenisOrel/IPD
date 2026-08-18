
// Type: Kompas6API5.KompasObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Kompas6API5;

[CompilerGenerated]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[Guid("E36BC97C-39D6-4402-9C25-C7008A217E02")]
[TypeIdentifier]
[ComImport]
public interface KompasObject
{
  [SpecialName]
  [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
  sealed extern void _VtblGap1_84();

  [DispId(82)]
  [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
  void Quit();
}
