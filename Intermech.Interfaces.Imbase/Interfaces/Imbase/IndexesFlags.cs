// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IndexesFlags
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// 
/// </summary>
[Flags]
public enum IndexesFlags
{
  /// <summary>Пусто</summary>
  None = 0,
  /// <summary>Автообновление</summary>
  Auto = 1,
  /// <summary>Уникальный индекс</summary>
  Unique = 16, // 0x00000010
  /// <summary>Уникальный и автообновление</summary>
  UniqueValue = Unique | Auto, // 0x00000011
  /// <summary>Применяемый - Применяемость = '+'</summary>
  [Obsolete] Using = 256, // 0x00000100
}
