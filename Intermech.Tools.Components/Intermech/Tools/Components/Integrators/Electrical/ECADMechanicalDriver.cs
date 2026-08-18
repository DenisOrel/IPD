// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Components.Integrators.Electrical.ECADMechanicalDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Electrical;
using Intermech.Tools.Integrators.Mechanical;
using System;

#nullable disable
namespace Intermech.Tools.Components.Integrators.Electrical;

public abstract class ECADMechanicalDriver : AppMechanicalDriver
{
  public ECADMechanicalDriver(IIntegrator integrator)
    : base(integrator)
  {
  }

  protected override void SetupArticleHandler(SectionEntity articleEntity, IAction articleHandler)
  {
    base.SetupArticleHandler(articleEntity, articleHandler);
    switch (articleHandler)
    {
      case NormalArticleHandler normalArticleHandler:
        normalArticleHandler.Finished += new EventHandler<ArticleEntityEventArgs>(this.OnArticleHandlerFinished);
        break;
      case ImbaseObjectArticleHandler objectArticleHandler:
        objectArticleHandler.Finished += new EventHandler<ArticleEntityEventArgs>(this.OnArticleHandlerFinished);
        break;
    }
  }

  private void OnArticleHandlerFinished(object sender, ArticleEntityEventArgs e)
  {
    if (!this.IsCADBuiltInStandardPart(e.ArticleEntity))
      return;
    e.ArticleEntity.Sections.Set((object) new ObjectKeepCheckedOutSection()
    {
      KeepCheckedOut = false
    });
  }

  private bool IsCADBuiltInStandardPart(SectionEntity articleEntity)
  {
    ElectricalArticleCache electricalArticleCache = articleEntity.Sections.Get<ElectricalArticleCache>((ElectricalArticleCache) null);
    return electricalArticleCache != null && electricalArticleCache.ArticleType == ArticleTypes.Component;
  }
}
