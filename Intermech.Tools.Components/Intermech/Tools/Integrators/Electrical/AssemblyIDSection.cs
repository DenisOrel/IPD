// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.AssemblyIDSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Данные для связывания до и после записи в базу</summary>
[Serializable]
public class AssemblyIDSection
{
  public Guid Guid { get; private set; }

  public AssemblyIDSection(Guid guid) => this.Guid = guid;
}
