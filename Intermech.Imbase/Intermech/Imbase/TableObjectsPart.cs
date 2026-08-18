// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TableObjectsPart
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;

#nullable disable
namespace Intermech.Imbase;

internal class TableObjectsPart : ObjectsPart
{
  internal TableReferenceNode _node;

  public TableObjectsPart(
    int objTypeID,
    ConditionStructure[] conditions,
    TableReferenceNode parentNode,
    IServiceProvider services)
    : base(objTypeID, conditions, services)
  {
    this._node = parentNode;
  }

  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    return (INodeQuery) new TableObjectsQuery((INodeQuerySupport) this, this.objTypeID, conditions);
  }
}
