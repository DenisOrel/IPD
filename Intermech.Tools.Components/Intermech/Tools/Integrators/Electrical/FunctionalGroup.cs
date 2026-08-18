// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.FunctionalGroup
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Функциональная группа</summary>
public sealed class FunctionalGroup
{
  /// <summary>Наименование</summary>
  public string Name { get; private set; }

  /// <summary>Обозначение</summary>
  public string Designation { get; private set; }

  /// <summary>Позиционное обозначение</summary>
  public string PosDesignation { get; private set; }

  public FunctionalGroup(string name, string designation, string posDesignation)
  {
    this.Name = name;
    this.Designation = designation;
    this.PosDesignation = posDesignation;
  }
}
