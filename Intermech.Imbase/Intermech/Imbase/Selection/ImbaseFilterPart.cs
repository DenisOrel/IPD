// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseFilterPart
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Selection;

internal class ImbaseFilterPart(
  int objTypeID,
  long objID,
  RelatedObjectsRole role,
  int relTypeID,
  IServiceProvider services) : RelatedObjectsPart(objTypeID, objID, role, relTypeID, services)
{
  private ConditionStructure[] _conditions;
  private DataTable _dtFilter;

  private INodeQuery Query
  {
    get
    {
      ImbaseFilterNodeQuery query = new ImbaseFilterNodeQuery((INodeQuerySupport) this, this._objID, this._objTypeID, this._role, this._relTypeID, this._conditions);
      query.SetFilter(this._dtFilter);
      return (INodeQuery) query;
    }
  }

  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    this._conditions = conditions;
    return this.Query;
  }

  public void SetFilter(DataTable dt) => this._dtFilter = dt;
}
