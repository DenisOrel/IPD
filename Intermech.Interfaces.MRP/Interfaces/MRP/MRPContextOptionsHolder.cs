// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPContextOptionsHolder
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Набор опций контекста (контейнера сервисов) для модуля MRP
/// </summary>
public sealed class MRPContextOptionsHolder
{
  /// <summary>Опции контекста (контейнера сервисов) для модуля MRP</summary>
  public volatile MRPContextOptions Options;

  /// <summary>Создать набор опций для контекста MRP</summary>
  /// <param name="options">Опции контекста (контейнера сервисов) для модуля MRP</param>
  public MRPContextOptionsHolder(MRPContextOptions options) => this.Options = options;
}
