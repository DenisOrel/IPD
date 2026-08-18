// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Helpers.UniversalPart
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;

#nullable disable
namespace Intermech.Navigator.Helpers;

public class UniversalPart : ObjectsListPart
{
  private UniversalDescriptor _parent;

  /// <param name="services">Контейнер сервисов</param>
  public UniversalPart(UniversalDescriptor parent, IList objectIDs, IServiceProvider services)
    : base(objectIDs, services)
  {
    this._parent = parent;
  }

  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    if (this._parent.AdditionalConditions != null)
      conditions = ConditionStructure.Join(conditions, this._parent.AdditionalConditions);
    return base.GetQuery(conditions);
  }

  public override NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    Helper.AddObjectTypeColumns(columns, this._parent.TypeID);
    this.GetSupportedColumns(ColumnSetName, columns);
    return columns;
  }

  public override NodeColumnCollection GetDefaultColumns()
  {
    return this._parent.GetDefaultColumns() ?? base.GetDefaultColumns();
  }

  protected override int ObjectTypeID => this._parent.TypeID;
}
