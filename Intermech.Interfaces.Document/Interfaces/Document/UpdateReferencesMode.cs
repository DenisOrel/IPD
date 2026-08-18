// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.UpdateReferencesMode
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Способ обновления ссылок</summary>
[Flags]
public enum UpdateReferencesMode
{
  None = 0,
  /// <summary>Ссылки на аттрибуты</summary>
  Attributes = 1,
  /// <summary>Ссылки на подписи</summary>
  Signes = 2,
  Checksum = 4,
  /// <summary>Все</summary>
  All = Checksum | Signes | Attributes, // 0x00000007
  /// <summary>Все кроме контрольной суммы</summary>
  NotChecksum = Signes | Attributes, // 0x00000003
}
