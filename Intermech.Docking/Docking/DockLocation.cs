
// Type: Intermech.Docking.DockLocation
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;


namespace Intermech.Docking;

[Flags]
public enum DockLocation
{
  Unknown = 0,
  Left = 1,
  Right = 2,
  Top = 4,
  Bottom = 8,
  Center = 16, // 0x00000010
  Float = 32, // 0x00000020
  Document = 64, // 0x00000040
  All = Document | Float | Bottom | Top | Right | Left, // 0x0000006F
}
