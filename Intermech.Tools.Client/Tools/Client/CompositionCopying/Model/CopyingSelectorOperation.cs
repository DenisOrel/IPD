// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CopyingSelectorOperation
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal abstract class CopyingSelectorOperation
{
  private Intermech.Tools.Client.CompositionCopying.Model.ErrorsBuilder<OperationError> errorsBuilder;

  protected CopyingSelectorOperation() => this.errorsBuilder = new Intermech.Tools.Client.CompositionCopying.Model.ErrorsBuilder<OperationError>();

  public ICollection<OperationError> Errors
  {
    [DebuggerStepThrough] get => this.errorsBuilder.Items;
  }

  protected Intermech.Tools.Client.CompositionCopying.Model.ErrorsBuilder<OperationError> ErrorsBuilder
  {
    [DebuggerStepThrough] get => this.errorsBuilder;
  }

  public void Invoke(
    CopyingSession session,
    DBObjectGraphVertex startVertex,
    CopyingSelectorEntry entry)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (startVertex == null)
      throw new ArgumentNullException(nameof (startVertex));
    if (entry == null)
      throw new ArgumentNullException(nameof (entry));
    this.ErrorsBuilder.Clear();
    try
    {
      this.DoInvoke(session, startVertex, entry);
    }
    catch
    {
      this.ErrorsBuilder.Clear();
      throw;
    }
  }

  protected abstract void DoInvoke(
    CopyingSession session,
    DBObjectGraphVertex startVertex,
    CopyingSelectorEntry entry);
}
