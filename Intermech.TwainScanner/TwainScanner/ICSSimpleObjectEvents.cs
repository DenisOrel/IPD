// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.ICSSimpleObjectEvents
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TwainScanner;

[Guid("2FAF539E-40B7-450B-92B8-2546AAB51DF4")]
[ComVisible(true)]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
public interface ICSSimpleObjectEvents
{
  [DispId(1)]
  void FloatPropertyChanging(float NewValue, ref bool Cancel);

  [DispId(2)]
  void ProgressChanged(int progress);

  [DispId(3)]
  void OnImageTransfer(byte[] image);

  [DispId(4)]
  void OnEndScaning();
}
