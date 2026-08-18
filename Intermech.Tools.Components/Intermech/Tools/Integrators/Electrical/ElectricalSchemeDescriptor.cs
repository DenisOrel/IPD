// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ElectricalSchemeDescriptor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Описатель схемы ECAD</summary>
[Serializable]
public sealed class ElectricalSchemeDescriptor : IdentifiedEntity
{
  /// <summary>Список сборок (плат) на схему</summary>
  public List<PrintBoardDescriptor> PrintBoards { get; set; }

  /// <summary>
  /// Список элементов перечня, по которым не создаются прочие изделия (пайки, контактные площадки и проч.)
  /// </summary>
  public List<SimpleRecord> SimpleRecords { get; set; }

  public ElectricalSchemeDescriptor(string designation, string name)
    : base(designation, name)
  {
    this.PrintBoards = new List<PrintBoardDescriptor>();
    this.SimpleRecords = new List<SimpleRecord>();
  }
}
