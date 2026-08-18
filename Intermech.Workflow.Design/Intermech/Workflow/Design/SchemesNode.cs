// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.SchemesNode
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Workflow.Design;

internal class SchemesNode : CompositeNode, IContextAware
{
  private long _objectID;
  private int _folderTypeID;
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider services;

  public SchemesNode(int typeID, long objectID)
  {
    this._objectID = objectID;
    this._folderTypeID = typeID;
    this.options = NodeOptions.CanContainsComposition;
  }

  /// <summary>Контейнер сервисов</summary>
  public virtual IServiceProvider Services
  {
    [DebuggerStepThrough] get => this.services;
    set => this.services = value;
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    ConditionStructure condition = new ConditionStructure(-7, RelationalOperators.Equal, (object) this._folderTypeID, LogicalOperators.NONE, 0, false);
    return this.SlotsFromSinglePart((INodePart) new RelatedObjectsPart(this._folderTypeID, this._objectID, RelatedObjectsRole.Composition, wfConsts.SimpleLinkTypeID, condition, this.Services));
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    if (this.Services.GetService(typeof (ValidSchemesOnlyFlag)) == null)
    {
      ConditionStructure[] array = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.NotEqual, (object) this._folderTypeID, LogicalOperators.AND, 0, false)
      };
      if (Holder.ShowOnlyBaseVersion)
      {
        Array.Resize<ConditionStructure>(ref array, array.Length + 1);
        array[array.Length - 1] = new ConditionStructure(-16, RelationalOperators.Equal, (object) true, LogicalOperators.AND, 0, true);
      }
      return this.SlotsFromSinglePart((INodePart) new RelatedObjectsPart(this._folderTypeID, this._objectID, RelatedObjectsRole.Composition, wfConsts.SimpleLinkTypeID, array, this.Services));
    }
    ConditionStructure[] array1 = new ConditionStructure[3]
    {
      new ConditionStructure(-7, RelationalOperators.Equal, (object) wfConsts.SchemesTypeID, LogicalOperators.AND, 0, false),
      new ConditionStructure(0, RelationalOperators.EntersIn, (object) this._objectID, LogicalOperators.AND, 0, false),
      new ConditionStructure(wfConsts.AttrActivityStatusID, RelationalOperators.Equal, (object) -1, LogicalOperators.AND, 0, false)
    };
    if (GlobalMailSettings.Cfg.LaunchBaseSchemesOnly && !Holder.IsAdmin && !Holder.СanShowAllVersions)
    {
      Array.Resize<ConditionStructure>(ref array1, array1.Length + 2);
      array1[array1.Length - 2] = new ConditionStructure(-16, RelationalOperators.Equal, (object) true, LogicalOperators.AND, 0, true);
      array1[array1.Length - 1] = new ConditionStructure(wfConsts.AttrIsDebugID, RelationalOperators.Equal, (object) false, LogicalOperators.AND, 0, true);
    }
    if (Holder.IsAdmin && Holder.ShowOnlyBaseVersionInStartProcess)
    {
      Array.Resize<ConditionStructure>(ref array1, array1.Length + 1);
      array1[array1.Length - 1] = new ConditionStructure(-16, RelationalOperators.Equal, (object) true, LogicalOperators.AND, 0, true);
    }
    return this.SlotsFromSinglePart((INodePart) new ObjectsPart(wfConsts.SchemesTypeID, array1, this.Services));
  }
}
