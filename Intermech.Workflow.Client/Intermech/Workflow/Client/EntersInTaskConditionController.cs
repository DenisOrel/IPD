// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.EntersInTaskConditionController
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Kernel.Search;
using Intermech.Navigator.Conditions;

#nullable disable
namespace Intermech.Workflow.Client;

internal sealed class EntersInTaskConditionController : 
  ConditionController<EntersInTaskConditionEditor>
{
  public override string VisibleName => "Входит в действия процессов";

  public override bool HandleConditionCaption(
    ConditionStructure conditionStructure,
    out string conditionText,
    out string valueText)
  {
    if (!this.IsHandleConditionStructure(conditionStructure))
    {
      conditionText = string.Empty;
      valueText = string.Empty;
      return false;
    }
    ConditionWorkflowTemplate workflowTemplate = conditionStructure.Value as ConditionWorkflowTemplate;
    conditionText = $"Входит в действия процессов по шаблону \"{this.dataProvider.GetObjectCaption((object) workflowTemplate.TemplateObjectID)}\"";
    if (workflowTemplate.ActivitiesID != null && workflowTemplate.ActivitiesID.Length != 0)
    {
      valueText = "Действия:";
      for (int index = 0; index < workflowTemplate.ActivitiesID.Length; ++index)
      {
        valueText += $" \"{this.dataProvider.GetObjectCaption((object) workflowTemplate.ActivitiesID[index])}\"";
        if (index == 2)
        {
          valueText += "...";
          break;
        }
      }
    }
    else
      valueText = $"Типы действий: \"{this.dataProvider.GetObjectTypeCaption((object) workflowTemplate.ActivityTypeID)}\"";
    return true;
  }

  public override bool IsHandleConditionStructure(ConditionStructure conditionStructure)
  {
    return conditionStructure.Value != null && conditionStructure.Value is ConditionWorkflowTemplate;
  }
}
