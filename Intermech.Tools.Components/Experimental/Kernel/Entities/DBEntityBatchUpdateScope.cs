// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBEntityBatchUpdateScope
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBEntityBatchUpdateScope : EntityBatchUpdateScope
{
  private DBModelRoot modelRoot;

  public DBEntityBatchUpdateScope(
    DBModelRoot modelRoot,
    IEntityBatchUpdateService service,
    IEntityChangeTrackerBase changeTracker)
    : base(service, changeTracker)
  {
    this.modelRoot = modelRoot != null ? modelRoot : throw new ArgumentNullException(nameof (modelRoot));
  }

  protected override void DoCloseScope()
  {
    base.DoCloseScope();
    this.modelRoot.StopBatchUpdate();
  }
}
