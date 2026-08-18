// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Helpers.FiltrationStatisticsElement
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;


namespace Intermech.Kernel.Helpers;

internal class FiltrationStatisticsElement
{
  internal long UserID;
  internal DateTime SelectBeginDate;
  internal DateTime SelectEndDate;
  internal DateTime BeginDate;
  internal DateTime EndDate;
  internal int SourceRows;
  internal int FilteredRows;
  internal int RelationType = -1;
  internal int ColumnsAdded;
  internal long FiltrationDuration;
  internal long SelectDuration;

  public FiltrationStatisticsElement()
  {
  }

  public FiltrationStatisticsElement(
    long AnUserID,
    int ASourceRows,
    int AFilteredRows,
    int ARelationType,
    int AColumnsAddeed)
  {
    this.UserID = AnUserID;
    this.SourceRows = ASourceRows;
    this.FilteredRows = AFilteredRows;
    this.RelationType = ARelationType;
    this.ColumnsAdded = AColumnsAddeed;
    this.BeginDate = DateTime.UtcNow;
    this.EndDate = this.BeginDate;
    this.SelectBeginDate = this.BeginDate;
    this.SelectEndDate = this.BeginDate;
    this.FiltrationDuration = 0L;
    this.SelectDuration = 0L;
  }

  public virtual void StartFiltration()
  {
    this.BeginDate = DateTime.UtcNow;
    this.EndDate = this.BeginDate;
    this.FiltrationDuration = 0L;
  }

  public virtual void StopFiltration()
  {
    this.EndDate = DateTime.UtcNow;
    this.FiltrationDuration = (long) (this.EndDate - this.BeginDate).Milliseconds;
  }

  public virtual void StartSelect()
  {
    this.SelectBeginDate = DateTime.UtcNow;
    this.SelectEndDate = this.SelectBeginDate;
    this.SelectDuration = 0L;
  }

  public virtual void StopSelect()
  {
    this.SelectEndDate = DateTime.UtcNow;
    this.SelectDuration = (long) (this.SelectEndDate - this.SelectBeginDate).Milliseconds;
  }
}
