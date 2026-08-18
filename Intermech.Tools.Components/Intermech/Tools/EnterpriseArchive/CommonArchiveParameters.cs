// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.CommonArchiveParameters
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Settings;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

public sealed class CommonArchiveParameters : PersistentSettingsObject, ICloneable
{
  private const string ModuleName = "EnterpriseArchive";
  private const string GlobalSection = "Globals";
  private const string LocationParameter = "Location";
  private const string ImportBatchSizeParameter = "ImportBatchSize";
  private SettingsCell<string> location;
  private SettingsCell<int> importBatchSize;

  protected override void CreateCells(ICollection<ISettingsCell> cells)
  {
    base.CreateCells(cells);
    this.location = new SettingsCell<string>((object) this, LocalizationHolder.rm.GetString("SR_523"), (string) null);
    cells.Add((ISettingsCell) this.location);
    this.importBatchSize = new SettingsCell<int>((object) this, LocalizationHolder.rm.GetString("SR_524"), 100);
    cells.Add((ISettingsCell) this.importBatchSize);
  }

  protected override void CreateValidators(ICollection<object> validators)
  {
    base.CreateValidators(validators);
    validators.Add((object) new DirectoryPathValidator(this.location));
    validators.Add((object) new RangeValidator<int>(this.importBatchSize, 10, 1000));
  }

  public SettingsCell<string> Location => this.location;

  public SettingsCell<int> ImportBatchSize => this.importBatchSize;

  public void Assign(object obj)
  {
    if (!(obj is CommonArchiveParameters archiveParameters))
      return;
    lock (this)
    {
      this.location.RawValue = archiveParameters.location.RawValue;
      this.importBatchSize.RawValue = archiveParameters.importBatchSize.RawValue;
    }
  }

  public CommonArchiveParameters Clone()
  {
    lock (this)
    {
      CommonArchiveParameters archiveParameters = new CommonArchiveParameters();
      archiveParameters.Assign((object) this);
      return archiveParameters;
    }
  }

  object ICloneable.Clone() => (object) this.Clone();

  public override void Load()
  {
    lock (this)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this.location.RawValue = CommonArchiveParameters.ReadGlobalString(sessionKeeper.Session, "Location");
        int result;
        if (!int.TryParse(CommonArchiveParameters.ReadGlobalString(sessionKeeper.Session, "ImportBatchSize"), out result))
          return;
        this.importBatchSize.RawValue = result;
      }
    }
  }

  public override void Save()
  {
    lock (this)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        CommonArchiveParameters.WriteGlobalString(sessionKeeper.Session, "Location", this.location.RawValue);
        CommonArchiveParameters.WriteGlobalString(sessionKeeper.Session, "ImportBatchSize", this.importBatchSize.RawValue.ToString());
      }
    }
  }

  private static string ReadGlobalString(IUserSession session, string parameterName)
  {
    return session.Configurations.ReadStringNoCache("EnterpriseArchive", "Globals", parameterName, true);
  }

  private static void WriteGlobalString(
    IUserSession session,
    string parameterName,
    string parameterValue)
  {
    session.Configurations.WriteString("EnterpriseArchive", "Globals", parameterName, parameterValue, 0L);
  }
}
