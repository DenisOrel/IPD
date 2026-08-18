// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Substitutes.SubstituteGroupNumberEventArgs
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Search.Pdm.Substitutes;

public sealed class SubstituteGroupNumberEventArgs : EventArgs
{
  public SubstituteGroupNumberEventArgs(long newNumber) => this.NewNumber = newNumber;

  public long NewNumber { get; private set; }
}
