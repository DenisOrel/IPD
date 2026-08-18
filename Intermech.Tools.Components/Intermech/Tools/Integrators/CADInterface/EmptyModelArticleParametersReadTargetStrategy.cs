// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.EmptyModelArticleParametersReadTargetStrategy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public sealed class EmptyModelArticleParametersReadTargetStrategy : 
  ModelArticleParametersReadTargetStrategy
{
  public override bool AllowReadMissingValuesFromDocument(
    IValueBagContainer modelConfigurationContainer)
  {
    return false;
  }

  public override IValueBagContainer GetDocumentContainer(
    IValueBagContainer modelConfigurationContainer)
  {
    throw new NotSupportedException();
  }
}
