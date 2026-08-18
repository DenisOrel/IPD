// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Receptures.RecepturesCacheSyncronizer
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase.Receptures;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Services;
using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server.Receptures;

internal class RecepturesCacheSyncronizer : CustomServerSynchronizer
{
  private IRecepturesService _recepturesService;

  public RecepturesCacheSyncronizer(IRecepturesService recepturesService)
    : base(new Guid("2868943F-24DE-4EAD-9DAC-156571CDE2FC"), "Служба синхронизации кэша таблиц рецептур")
  {
    this._recepturesService = recepturesService;
  }

  public override void ExecuteEvent(SynchonizerEventProperties eventProps, IUserSession session)
  {
    string stringInfo = eventProps.StringInfo;
    long result;
    if (string.IsNullOrEmpty(stringInfo) || !long.TryParse(stringInfo, out result))
      return;
    DataSet tables = TableLoadHelper.GetTables(session, result, true);
    if (tables == null || !tables.Tables.Contains("IMS_ATTR_TYPES") || !tables.Tables.Contains("IMS_DATA"))
      return;
    DataTable table = tables.Tables["IMS_DATA"];
    this._recepturesService.UpdateCacheAfterTableMixEdit(session, result, table);
  }

  public void AddEvent(string strInfo, IDbManager db)
  {
    if (string.IsNullOrEmpty(strInfo) || db == null || !this.IsRegistered)
      return;
    this.Manager.AddSynchronizerEvent(this.GetEventProps(strInfo), db);
  }
}
