// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADSchSheetSymbol
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Data;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADSchSheetSymbol(ISchSheetSymbol schSheetSymbol) : 
  TypedParametersContainer<ISchSheetSymbol>(schSheetSymbol),
  ISchSheetSymbol,
  ISchComponent,
  IParametrable,
  IValueBagContainer,
  IIdentification
{
  protected override string GetInternalId() => this.FileName;

  public string FileName => this.Instance.FileName;

  public string DesignatorText => this.Instance.DesignatorText;
}
