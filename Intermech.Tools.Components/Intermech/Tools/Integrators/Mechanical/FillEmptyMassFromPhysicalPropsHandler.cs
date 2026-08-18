// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.FillEmptyMassFromPhysicalPropsHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.Components.Properties;
using Intermech.Tools.Data;
using Intermech.UI;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal sealed class FillEmptyMassFromPhysicalPropsHandler : IAction
{
  private readonly MechanicalDriver driver;
  private readonly SectionEntity articleItem;
  private readonly bool forcedFill;
  private AttributesSection articleAttrs;
  private ValueRecord existingMassItem;

  public FillEmptyMassFromPhysicalPropsHandler(
    MechanicalDriver driver,
    SectionEntity articleItem,
    bool forcedFill)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    this.driver = driver;
    this.articleItem = articleItem;
    this.forcedFill = forcedFill;
  }

  public void Perform()
  {
    this.Prepare();
    if (!this.CanFill())
      return;
    IArticlePhysicalPropertiesService propertiesService = this.driver.TryGetArticlePhysicalPropertiesService(this.articleItem);
    if (propertiesService == null)
      return;
    MeasuredValue mass = propertiesService.CalculateMass(this.articleItem);
    if (mass == null)
      return;
    this.articleAttrs.WorkingSet.Update((StringKey) IDCache.Default.Mass.Text, (object) this.RoundMass(mass));
  }

  private void Prepare()
  {
    this.articleAttrs = this.articleItem.Sections.Get<AttributesSection>();
    this.existingMassItem = this.articleAttrs.WorkingSet.Find((StringKey) IDCache.Default.Mass.Text);
  }

  private bool CanFill()
  {
    return this.existingMassItem == null || this.existingMassItem.IsNull || this.forcedFill;
  }

  private MeasuredValue RoundMass(MeasuredValue mass)
  {
    int num = this.articleAttrs.WorkingSet.Read<int>((StringKey) CADDocumentResources.EMB_MassFormat, 0);
    if (num > 0)
    {
      switch (num)
      {
        case 10:
          return this.RoundValue(mass, IDCache.Default.GramMeasure.Id, 0);
        case 11:
          return this.RoundValue(mass, IDCache.Default.GramMeasure.Id, 1);
        case 12:
          return this.RoundValue(mass, IDCache.Default.GramMeasure.Id, 2);
        case 13:
          return this.RoundValue(mass, IDCache.Default.GramMeasure.Id, 3);
        case 20:
          return this.RoundValue(mass, IDCache.Default.KilogramMeasure.Id, 0);
        case 21:
          return this.RoundValue(mass, IDCache.Default.KilogramMeasure.Id, 1);
        case 22:
          return this.RoundValue(mass, IDCache.Default.KilogramMeasure.Id, 2);
        case 23:
          return this.RoundValue(mass, IDCache.Default.KilogramMeasure.Id, 3);
        case 24:
          return this.RoundValue(mass, IDCache.Default.KilogramMeasure.Id, 4);
        case 25:
          return this.RoundValue(mass, IDCache.Default.KilogramMeasure.Id, 5);
        case 28:
          return this.RoundValueToFactoredInteger(mass, IDCache.Default.KilogramMeasure.Id, 100);
        case 29:
          return this.RoundValueToFactoredInteger(mass, IDCache.Default.KilogramMeasure.Id, 10);
        case 30:
          return this.RoundValue(mass, IDCache.Default.TonMeasure.Id, 0);
        case 31 /*0x1F*/:
          return this.RoundValue(mass, IDCache.Default.TonMeasure.Id, 1);
        case 32 /*0x20*/:
          return this.RoundValue(mass, IDCache.Default.TonMeasure.Id, 2);
        case 33:
          return this.RoundValue(mass, IDCache.Default.TonMeasure.Id, 3);
        default:
          if (UIReport.Enabled)
          {
            UIReport.ReportEvent($"Задан неподдерживаемый формат массы. Проверьте значение атрибута '{CADDocumentResources.EMB_MassFormat}' = '{num}'.", TraceLevel.Warning);
            break;
          }
          break;
      }
    }
    if (this.existingMassItem == null || this.existingMassItem.IsNull)
      return mass;
    long measureId = this.existingMassItem.Read<MeasuredValue>((MeasuredValue) null).MeasureID;
    return MeasureHelper.ConvertToMeasuredValue(mass, measureId);
  }

  private MeasuredValue RoundValue(MeasuredValue value, long measureId, int digits)
  {
    MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(value, measureId);
    return new MeasuredValue(Math.Round(measuredValue.Value, digits), measuredValue.MeasureID);
  }

  private MeasuredValue RoundValueToFactoredInteger(
    MeasuredValue value,
    long measureId,
    int factor)
  {
    MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(value, measureId);
    return new MeasuredValue(Math.Round(measuredValue.Value / (double) factor) * (double) factor, measuredValue.MeasureID);
  }
}
