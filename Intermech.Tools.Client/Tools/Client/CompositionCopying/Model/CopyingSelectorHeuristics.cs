// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CopyingSelectorHeuristics
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal abstract class CopyingSelectorHeuristics
{
  private bool isAllowing;
  private Intermech.Tools.Client.CompositionCopying.Model.ErrorsBuilder<OperationError> errorsBuilder;

  public CopyingSelectorHeuristics(bool isAllowing)
  {
    this.isAllowing = isAllowing;
    this.errorsBuilder = new Intermech.Tools.Client.CompositionCopying.Model.ErrorsBuilder<OperationError>();
  }

  public bool IsAllowing => this.isAllowing;

  public ICollection<OperationError> Errors => this.errorsBuilder.Items;

  protected Intermech.Tools.Client.CompositionCopying.Model.ErrorsBuilder<OperationError> ErrorsBuilder
  {
    [DebuggerStepThrough] get => this.errorsBuilder;
  }

  public void Apply(CopyingSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    this.ErrorsBuilder.Clear();
    this.DoApply(session);
  }

  protected virtual void DoApply(CopyingSession session)
  {
  }
}
