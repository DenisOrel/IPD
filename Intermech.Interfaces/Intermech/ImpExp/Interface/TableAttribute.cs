
// Type: Intermech.ImpExp.Interface.TableAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.ImpExp.Interface
{
    public class TableAttribute
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
      public ImEnterMode EnterMode = ImEnterMode.IEM_SIMPLE;

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
        ImEnterMode enterMode)
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
        this.IsTableRecRef = enterMode == ImEnterMode.IEM_RECORD;
        this.IsGuid = enterMode == ImEnterMode.IEM_GUID;
        this.EnterMode = enterMode;
      }
    }
}
