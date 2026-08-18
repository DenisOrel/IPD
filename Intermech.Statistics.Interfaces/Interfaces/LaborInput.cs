// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.LaborInput
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Statistics.Interfaces;

/// <summary>Настройки трудоемкости</summary>
[Serializable]
public class LaborInput : ICloneable
{
  [XmlElement(ElementName = "Formula")]
  public string Formula { get; set; }

  public LaborInput() => this.Formula = string.Empty;

  public LaborInput(string formula) => this.Formula = formula;

  public bool HasFormula() => this.Formula != string.Empty;

  object ICloneable.Clone() => (object) this.Clone();

  public LaborInput Clone() => new LaborInput(this.Formula);
}
