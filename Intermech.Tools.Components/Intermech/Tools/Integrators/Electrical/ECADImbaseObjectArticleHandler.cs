// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ECADImbaseObjectArticleHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

public sealed class ECADImbaseObjectArticleHandler : ImbaseObjectArticleHandler
{
  private ECADIntegratorSettings _settings;

  public ECADImbaseObjectArticleHandler(
    MechanicalDriver driver,
    CaptureChangesDriverContext ctx,
    SectionEntity articleItem,
    ECADIntegratorSettings settings)
    : base(driver, ctx, articleItem)
  {
    this._settings = settings;
  }

  protected override Tuple<long, int, string> FindOrCreateImbaseObject(ValueBag attributes)
  {
    ImbaseSyncInfo imbaseSyncInfo = this.articleItem.Sections.Get<ImbaseSyncInfo>();
    return imbaseSyncInfo == null ? base.FindOrCreateImbaseObject(attributes) : ImbaseHelper.CreateImbaseObject(imbaseSyncInfo.TableID, imbaseSyncInfo.RecordID);
  }
}
