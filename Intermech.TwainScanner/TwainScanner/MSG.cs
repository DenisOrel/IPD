// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.MSG
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System;

#nullable disable
namespace Intermech.TwainScanner;

internal struct MSG
{
  public IntPtr hWnd;
  public uint message;
  public IntPtr wParam;
  public IntPtr lParam;
  public uint time;
  public POINT pt;
}
