// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.NotifySamples.NotifySamplesProcessor
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.NotifySamples;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Threading;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.NotifySamples;

public class NotifySamplesProcessor : MarshalByRefObject, INotifySamplesProcessor
{
  private UserSession _session;
  private List<NotifySampleProperties> _samples;
  private ISelectionsService _selectionsSrv;
  private AtomicBoolean _IsModified = new AtomicBoolean(false);

  public NotifySamplesProcessor(UserSession session)
  {
    this._session = session;
    this.ReloadSamples();
    this._selectionsSrv = ServerServices.GetService(typeof (ISelectionsService)) as ISelectionsService;
  }

  public void ReloadSamples()
  {
    this._IsModified.Value = false;
    DataTable dataTable = this._session.GetObjectCollection(NotifySamplesConst.NotifySamplesType).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-8, RelationalOperators.Equal, (object) this._session.UserID, LogicalOperators.NONE, 0, false)
    }, new object[1]{ (object) -2 }));
    this._samples = new List<NotifySampleProperties>(dataTable.Rows.Count);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      this._samples.Add(new NotifySampleProperties(this._session.GetObject(Convert.ToInt64(dataTable.Rows[index][0]))));
  }

  public NSResult ProcessSamples()
  {
    if (this._IsModified.Value)
      this.ReloadSamples();
    NSResult dif = new NSResult();
    for (int index = 0; index < this._samples.Count; ++index)
    {
      DateTime dateTime = this._samples[index].LastSearchTime + TimeSpan.FromMinutes((double) this._samples[index].SearchPeriod);
      if (dateTime < DateTime.UtcNow + this._session.TimeZoneOffset)
      {
        if (this._samples[index].Conditions == null)
          this._samples[index].Conditions = this._selectionsSrv.GetConditionStructures((object) this._session.SessionGUID, this._samples[index].SampleID);
        this._samples[index].ProcessSample(dif, (IUserSession) this._session);
        this._samples[index].LastSearchTime = DateTime.UtcNow + this._session.TimeZoneOffset;
        dateTime = this._samples[index].LastSearchTime + TimeSpan.FromMinutes((double) this._samples[index].SearchPeriod);
      }
      if (dif.NextProcessTime > dateTime)
        dif.NextProcessTime = dateTime;
    }
    return dif;
  }

  public void SaveSamplesState()
  {
    for (int index = 0; index < this._samples.Count; ++index)
      this._samples[index].SaveToObject((IUserSession) this._session);
  }

  internal void SetIsModified() => this._IsModified.Value = true;

  internal void RaceSetIsModified() => this._IsModified.TryModify(false, true);
}
