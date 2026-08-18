// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.REGCLS
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System;

#nullable disable
namespace Intermech.TwainScanner;

/// <summary>
/// The REGCLS enumeration defines values used in CoRegisterClassObject to
/// control the type of connections to a class object.
/// </summary>
[Flags]
internal enum REGCLS : uint
{
  SINGLEUSE = 0,
  MULTIPLEUSE = 1,
  MULTI_SEPARATE = 2,
  SUSPENDED = 4,
  SURROGATE = 8,
}
