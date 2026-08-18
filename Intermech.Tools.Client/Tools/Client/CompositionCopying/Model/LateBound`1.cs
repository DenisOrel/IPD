// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.LateBound`1
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class LateBound<T>
{
  private readonly Func<T> valueProvider;
  private T value;
  private bool isVaueCreated;

  public LateBound(Func<T> valueProvider)
  {
    this.valueProvider = valueProvider != null ? valueProvider : throw new ArgumentNullException(nameof (valueProvider));
  }

  public bool IsValueCreated
  {
    [DebuggerStepThrough] get => this.isVaueCreated;
  }

  public T Value
  {
    get
    {
      if (!this.isVaueCreated)
      {
        this.value = this.valueProvider();
        this.isVaueCreated = true;
      }
      return this.value;
    }
  }
}
