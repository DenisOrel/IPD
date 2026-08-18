// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.SimpleRecord
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Простая запись для ПЭ не связанная с объектом,
/// например контактная площадка, капля припоя и т.п.
/// </summary>
[Serializable]
public class SimpleRecord
{
  /// <summary>Описание</summary>
  public string Description { get; private set; }

  /// <summary>Позиционное обозначение</summary>
  public string PosDesignation { get; private set; }

  public SimpleRecord(string description, string posDesignation)
  {
    this.Description = description;
    this.PosDesignation = posDesignation;
  }
}
