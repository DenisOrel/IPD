// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.SchemesRootNode
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for SchemesRootNode.</summary>
public class SchemesRootNode(int objTypeID) : TopObjectsNode(objTypeID)
{
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    RelationalOperators relationalOperator = RelationalOperators.NOP;
    if (this.Services.GetService(typeof (ValidSchemesOnlyFlag)) != null)
      relationalOperator = RelationalOperators.Equal;
    ConditionStructure[] array = new ConditionStructure[3]
    {
      new ConditionStructure(-7, RelationalOperators.Equal, (object) wfConsts.SchemesTypeID, LogicalOperators.AND, 0, false),
      new ConditionStructure(0, RelationalOperators.NotEntersInType, (object) wfConsts.SchemeCategoriesID, LogicalOperators.AND, 0, false),
      new ConditionStructure(wfConsts.AttrActivityStatusID, relationalOperator, (object) -1, LogicalOperators.AND, 0, false)
    };
    if (relationalOperator == RelationalOperators.Equal && GlobalMailSettings.Cfg.LaunchBaseSchemesOnly && !Holder.IsAdmin && !Holder.СanShowAllVersions)
    {
      Array.Resize<ConditionStructure>(ref array, array.Length + 2);
      array[array.Length - 2] = new ConditionStructure(-16, RelationalOperators.Equal, (object) true, LogicalOperators.AND, 0, true);
      array[array.Length - 1] = new ConditionStructure(wfConsts.AttrIsDebugID, RelationalOperators.Equal, (object) false, LogicalOperators.AND, 0, true);
    }
    else if (relationalOperator == RelationalOperators.NOP && Holder.ShowOnlyBaseVersion)
    {
      Array.Resize<ConditionStructure>(ref array, array.Length + 1);
      array[array.Length - 1] = new ConditionStructure(-16, RelationalOperators.Equal, (object) true, LogicalOperators.AND, 0, true);
    }
    else if (relationalOperator == RelationalOperators.Equal && Holder.IsAdmin && Holder.ShowOnlyBaseVersionInStartProcess)
    {
      Array.Resize<ConditionStructure>(ref array, array.Length + 1);
      array[array.Length - 1] = new ConditionStructure(-16, RelationalOperators.Equal, (object) true, LogicalOperators.AND, 0, true);
    }
    return this.SlotsFromSinglePart((INodePart) new ObjectsPart(wfConsts.SchemesTypeID, array, this.Services));
  }
}
