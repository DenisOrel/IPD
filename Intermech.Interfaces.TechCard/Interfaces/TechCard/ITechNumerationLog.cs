// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.ITechNumerationLog
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Interfaces.TechCard.TechNumeration;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Интерфейс лога нумерации</summary>
public interface ITechNumerationLog
{
  /// <summary>Сессия</summary>
  ITechNumerationSession NumerationSession { get; }

  /// <summary>Список ид. пронумерованных объектов</summary>
  IList<long> ObjectsLog { get; }

  /// <summary>Список ид. пронумерованных связей</summary>
  IList<long> RelationsLog { get; }
}
