// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.TechAcadUtils
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TechAcad.Connector;

internal abstract class TechAcadUtils
{
  [DllImport("user32.DLL")]
  public static extern bool ShowWindow([In] int hwnd, [In] int nCmdShow);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool IsWindow(HandleRef hWnd);
}
