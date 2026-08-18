// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.TableRefsObjectPart
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;

#nullable disable
namespace Intermech.Imbase.Views;

public class TableRefsObjectPart : ObjectsPart
{
  private long _tableId;

  public TableRefsObjectPart(long tableId, IServiceProvider service)
    : base(service)
  {
    this._tableId = tableId;
  }

  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    return (INodeQuery) new TableRefsQuery(this._tableId, (INodeQuerySupport) this);
  }
}
