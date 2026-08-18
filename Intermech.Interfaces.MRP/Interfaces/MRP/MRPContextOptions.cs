// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPContextOptions
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Опции контекста (контейнера сервисов) для модуля MRP</summary>
[Serializable]
public enum MRPContextOptions
{
  /// <summary>Никаких опций нет</summary>
  None,
  /// <summary>
  /// Требуется фиксация версий объектов в контекст редактирования
  /// </summary>
  FixToEditingContext,
}
