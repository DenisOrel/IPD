// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Helpers.FiltrationStatistics
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using System.Collections;
using System.Data;


namespace Intermech.Kernel.Helpers;

internal class FiltrationStatistics
{
  internal ArrayList Statistics = new ArrayList();

  public FiltrationStatisticsElement AddElement(
    long AnUserID,
    int ASourceRows,
    int ARelationType,
    int AColumnsAddeed)
  {
    FiltrationStatisticsElement statisticsElement = new FiltrationStatisticsElement(AnUserID, ASourceRows, ASourceRows, ARelationType, AColumnsAddeed);
    this.Statistics.Add((object) statisticsElement);
    return statisticsElement;
  }

  public void SaveToBase(IDbManager db)
  {
    if (db == null || this.Statistics == null || this.Statistics.Count <= 0)
      return;
    for (int index = 0; index < this.Statistics.Count; ++index)
    {
      if (this.Statistics[index] is FiltrationStatisticsElement statistic)
      {
        IDbDataParameter dbDataParameter1 = db.Parameter("F_USER_ID", (object) statistic.UserID);
        IDbDataParameter dbDataParameter2 = db.Parameter("F_BEGIN_DATE", (object) statistic.BeginDate);
        IDbDataParameter dbDataParameter3 = db.Parameter("F_END_DATE", (object) statistic.EndDate);
        IDbDataParameter dbDataParameter4 = db.Parameter("F_SELECT_BEGIN_DATE", (object) statistic.SelectBeginDate);
        IDbDataParameter dbDataParameter5 = db.Parameter("F_SELECT_END_DATE", (object) statistic.SelectEndDate);
        IDbDataParameter dbDataParameter6 = db.Parameter("F_SOURCE_ROWS", (object) statistic.SourceRows);
        IDbDataParameter dbDataParameter7 = db.Parameter("F_FILTERED_ROWS", (object) statistic.FilteredRows);
        IDbDataParameter dbDataParameter8 = db.Parameter("F_RELATION_TYPE", (object) statistic.RelationType);
        IDbDataParameter dbDataParameter9 = db.Parameter("F_COLUMNS_ADDED", (object) statistic.ColumnsAdded);
        IDbDataParameter dbDataParameter10 = db.Parameter("F_FILTRATION_DURATION", (object) statistic.FiltrationDuration);
        IDbDataParameter dbDataParameter11 = db.Parameter("F_SELECT_DURATION", (object) statistic.SelectDuration);
        db.ExecuteNonQuery("INSERT INTO IMS_FILTRATION_STAT (F_USER_ID, F_BEGIN_DATE, F_END_DATE, F_SELECT_BEGIN_DATE, F_SELECT_END_DATE, F_SOURCE_ROWS, F_FILTERED_ROWS, F_RELATION_TYPE, F_COLUMNS_ADDED, F_FILTRATION_DURATION, F_SELECT_DURATION) VALUES (:F_USER_ID, :F_BEGIN_DATE, :F_END_DATE, :F_SELECT_BEGIN_DATE, :F_SELECT_END_DATE, :F_SOURCE_ROWS, :F_FILTERED_ROWS, :F_RELATION_TYPE, :F_COLUMNS_ADDED, :F_FILTRATION_DURATION, :F_SELECT_DURATION)", dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4, dbDataParameter5, dbDataParameter6, dbDataParameter7, dbDataParameter8, dbDataParameter9, dbDataParameter10, dbDataParameter11);
      }
    }
    this.Statistics.Clear();
  }
}
