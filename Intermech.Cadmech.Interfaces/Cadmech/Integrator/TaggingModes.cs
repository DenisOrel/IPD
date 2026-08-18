// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.TaggingModes
// Assembly: Intermech.Cadmech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A35B043F-5773-4DBE-81D3-C3E493F8C825
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Interfaces.xml

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// Описывает способы идентификации объекта в базе данных.
/// </summary>
public enum TaggingModes
{
  /// <summary>Объект можно найти по обозначению объекта</summary>
  Designation,
  /// <summary>Фиктивное обозначение</summary>
  FakeDesignation,
  /// <summary>Объект можно найти по ключу Imbase</summary>
  ImbaseKey,
}
