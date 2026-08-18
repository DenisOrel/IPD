// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.CompositionVariantsProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

public class CompositionVariantsProxy
{
  public CompositionVariantsProxy(CompositionVariants val) => this.Value = val;

  public CompositionVariants Value { get; private set; }

  public override bool Equals(object obj)
  {
    return obj is CompositionVariantsProxy ? ((CompositionVariantsProxy) obj).Value == this.Value : base.Equals(obj);
  }

  public override int GetHashCode() => ((int) this.Value).GetHashCode();

  public override string ToString() => EnumDescConverter.GetEnumDescription((Enum) this.Value);
}
