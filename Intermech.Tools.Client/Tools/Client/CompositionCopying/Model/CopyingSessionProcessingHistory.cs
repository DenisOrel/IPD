// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CopyingSessionProcessingHistory
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CopyingSessionProcessingHistory
{
  private List<CopyingSessionProcessingStep> internalList;

  public CopyingSessionProcessingHistory()
  {
    this.internalList = new List<CopyingSessionProcessingStep>();
  }

  public IReadOnlyCollection<CopyingSessionProcessingStep> Items
  {
    [DebuggerStepThrough] get
    {
      return (IReadOnlyCollection<CopyingSessionProcessingStep>) this.internalList;
    }
  }

  public bool Contains(string stepName)
  {
    if (stepName == null)
      throw new ArgumentNullException(nameof (stepName));
    return this.internalList.FindIndex((Predicate<CopyingSessionProcessingStep>) (x => x.Name == stepName)) >= 0;
  }

  public void Update(CopyingSessionProcessingStep step)
  {
    if (step == null)
      throw new ArgumentNullException(nameof (step));
    int index = this.internalList.FindIndex((Predicate<CopyingSessionProcessingStep>) (x => x.Name == step.Name));
    if (index >= 0)
      this.internalList.RemoveRange(index, this.internalList.Count - index);
    this.internalList.Add(step);
  }
}
