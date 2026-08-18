// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CopyingSessionProcessingStep
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CopyingSessionProcessingStep
{
  public CopyingSessionProcessingStep(string name)
  {
    this.Name = name != null ? name : throw new ArgumentNullException(nameof (name));
  }

  public string Name { get; }

  public override string ToString() => this.Name;
}
