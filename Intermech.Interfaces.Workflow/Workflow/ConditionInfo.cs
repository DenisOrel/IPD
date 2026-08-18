// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ConditionInfo
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Expert;
using Intermech.Interfaces.Workflow;

#nullable disable
namespace Intermech.Workflow;

public class ConditionInfo
{
  public long LinkID;
  public TempFormula ExpertFormula;

  public override string ToString()
  {
    return this.ExpertFormula != null ? this.ExpertFormula.ToString() : LocalizationHolder.rm.GetString("Workflow.Design_31");
  }
}
