// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ApplicabilityValue
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Imbase.Server;

[AttributeUsage(AttributeTargets.Field)]
public class ApplicabilityValue : Attribute
{
  public string Value { get; set; }

  public override string ToString() => this.Value;

  public ApplicabilityValue(string val) => this.Value = val;
}
