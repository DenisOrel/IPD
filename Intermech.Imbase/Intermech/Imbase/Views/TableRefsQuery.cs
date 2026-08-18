// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.TableRefsQuery
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Queries;
using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Views;

public class TableRefsQuery : ObjectsQuery
{
  private long _tableId;

  public TableRefsQuery(long tableId, INodeQuerySupport support)
    : base(support, Intermech.Imbase.Consts.ImbaseTableRefTypeID, (ConditionStructure[]) null, (IServiceProvider) null)
  {
    this._tableId = tableId;
  }

  protected override DataTable GetDataTable(DBRecordSetParams queryParams)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer).GetTableRefs(sessionKeeper.Session.SessionGUID, this._tableId, queryParams);
  }
}
