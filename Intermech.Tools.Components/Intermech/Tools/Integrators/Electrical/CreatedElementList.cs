// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.CreatedElementList
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

internal sealed class CreatedElementList : IdentifiedEntity
{
  /// <summary>Тип перечня</summary>
  public int Type { get; set; }

  /// <summary>Список составоотбразующих сборок</summary>
  public List<Tuple<long, string, string, string>> Assemblies { get; set; }

  public CreatedElementList(string designation, string name)
    : base(designation, name)
  {
    this.Assemblies = new List<Tuple<long, string, string, string>>();
  }

  public override string ToString()
  {
    if (!string.IsNullOrEmpty(this.Designation) && !string.IsNullOrEmpty(this.Name))
      return $"{this.Designation}({this.Name})";
    if (!string.IsNullOrEmpty(this.Designation))
      return this.Designation;
    return !string.IsNullOrEmpty(this.Name) ? this.Name : base.ToString();
  }
}
