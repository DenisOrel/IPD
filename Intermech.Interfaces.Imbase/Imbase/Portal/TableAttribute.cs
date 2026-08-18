// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Portal.TableAttribute
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Imbase.Portal;

internal class TableAttribute
{
  public Guid AttributeGuid = Guid.Empty;
  public RequiredModes AddMode = RequiredModes.Auto;
  public ComputeValueModes ComputeMode;
  public OptimizationModes InViewMode;
  public AttributeOptions MaskFlag;
  public string ImFormula = string.Empty;
  public string DefVal = string.Empty;
  public Guid Measure = Guid.Empty;
  public string Display = string.Empty;
  public bool IsTableRecRef;
  public bool IsGuid;

  public TableAttribute()
  {
  }

  public TableAttribute(
    Guid attributeGuid,
    RequiredModes addMode,
    ComputeValueModes computeMode,
    OptimizationModes inViewMode,
    AttributeOptions maskFlag,
    string imFormula,
    string defVal,
    Guid measure,
    string display,
    bool isTableRecRef,
    bool isGuid)
  {
    this.AttributeGuid = attributeGuid;
    this.AddMode = addMode;
    this.ComputeMode = computeMode;
    this.InViewMode = inViewMode;
    this.MaskFlag = maskFlag;
    this.ImFormula = imFormula;
    this.DefVal = defVal;
    this.Measure = measure;
    this.Display = display;
    this.IsTableRecRef = isTableRecRef;
    this.IsGuid = isGuid;
  }
}
