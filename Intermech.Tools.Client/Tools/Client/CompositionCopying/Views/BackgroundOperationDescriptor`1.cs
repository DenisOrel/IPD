// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.BackgroundOperationDescriptor`1
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Runtime;
using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal sealed class BackgroundOperationDescriptor<TOperationContext> : FreezableObject where TOperationContext : BackgroundOperationContext
{
  public Func<TOperationContext> OnCreateOperationContext { get; set; }

  public RunBackgroundOperation<TOperationContext> OnRunInBackground { get; set; }

  public ProcessBackgroundOperationResult<TOperationContext> OnResult { get; set; }

  protected override void DoValidate()
  {
    base.DoValidate();
    if (this.OnCreateOperationContext == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "OnCreateOperationContext");
    if (this.OnRunInBackground == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "OnRunInBackground");
  }
}
