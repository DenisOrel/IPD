// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DefaultModelArticleParametersReadTargetStrategy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Data;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class DefaultModelArticleParametersReadTargetStrategy : 
  ModelArticleParametersReadTargetStrategy
{
  public override bool AllowReadMissingValuesFromDocument(
    IValueBagContainer modelConfigurationContainer)
  {
    return this.GetConfiguration(modelConfigurationContainer).Document != null;
  }

  public override IValueBagContainer GetDocumentContainer(
    IValueBagContainer modelConfigurationContainer)
  {
    return (IValueBagContainer) CADInterfaceAdapters.AsValueBagContainer(this.GetConfiguration(modelConfigurationContainer).Document);
  }

  private ModelConfigurationProxy GetConfiguration(IValueBagContainer container)
  {
    return (ModelConfigurationProxy) ((CADInterfaceValueBagContainer) container).CADInterfaceObject;
  }
}
