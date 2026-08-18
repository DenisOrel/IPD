// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.ErrorsBuilder`1
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class ErrorsBuilder<TError>
{
  private ICollection<TError> items;
  private static readonly TError[] noErrors = new TError[0];

  public void AddError(TError error)
  {
    if ((object) error == null)
      throw new ArgumentNullException(nameof (error));
    if (this.items == null)
      this.items = (ICollection<TError>) new List<TError>();
    this.items.Add(error);
  }

  public void AddErrors(ICollection<TError> errors)
  {
    if (errors == null)
      throw new ArgumentNullException(nameof (errors));
    if (errors.Count == 0)
      return;
    foreach (TError error in (IEnumerable<TError>) errors)
      this.AddError(error);
  }

  public void Clear()
  {
    if (this.items == null)
      return;
    this.items = (ICollection<TError>) null;
  }

  public ICollection<TError> Items
  {
    get => this.items == null ? (ICollection<TError>) ErrorsBuilder<TError>.noErrors : this.items;
  }
}
