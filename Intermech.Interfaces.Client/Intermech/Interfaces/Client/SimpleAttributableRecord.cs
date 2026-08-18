// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.SimpleAttributableRecord
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Простая запись набором произвольных атрибутов для ПЭ не связанная с объектом,
/// например контактная площадка, капля припоя и т.п.
/// </summary>
[Serializable]
public sealed class SimpleAttributableRecord : SimpleRecord
{
  public SimpleAttributableRecord(
    string description,
    string posDesignation,
    List<Tuple<string, object>> attributes)
    : base(description, posDesignation)
  {
    this.Attributes = attributes;
  }

  public List<Tuple<string, object>> Attributes { get; }
}
