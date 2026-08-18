// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.TypeSettingsForUnitsCache
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Office.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Office.Server;

internal sealed class TypeSettingsForUnitsCache
{
  [NotNull]
  private Dictionary<long, Dictionary<int, OfficeDocumentTypeSettingsForUnit>> _cache;

  public void Reload()
  {
    using (SystemSessionKeeper systemSessionKeeper = new SystemSessionKeeper("OfficeServer.Reload"))
    {
      DataTable dataTable1 = systemSessionKeeper.Session.GetObjectCollection(new Guid("cadd9235-306c-11d8-b4e9-00304f19f545")).SelectWithLocalObjects(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(OfficeConsts.AttrSelfOfficeID, RelationalOperators.Equal, (object) true, LogicalOperators.NONE, 0, true)
      }, new object[1]{ (object) -2 }));
      if (dataTable1.Rows.Count > 0)
      {
        List<long> longList = new List<long>();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
          longList.Add(Convert.ToInt64(row[0]));
        DataTable dataTable2 = systemSessionKeeper.Session.GetObjectCollection(OfficeConsts.ObjtypeContainerID).Select(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(OfficeConsts.AttrUnitLinkID, RelationalOperators.In, (object) longList.ToArray(), LogicalOperators.NONE, 0, true)
          {
            Content = ColumnContents.ID
          }
        }, new ColumnDescriptor[2]
        {
          new ColumnDescriptor((object) -2, SortOrders.NONE, 0),
          new ColumnDescriptor((object) OfficeConsts.AttrUnitLinkID, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0)
        }));
        this._cache = new Dictionary<long, Dictionary<int, OfficeDocumentTypeSettingsForUnit>>(dataTable2.Rows.Count);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
        {
          Dictionary<int, OfficeDocumentTypeSettingsForUnit> dictionary = UnitSettingsFile.Read(systemSessionKeeper.Session.GetObject(Convert.ToInt64(row[0])));
          if (dictionary != null)
            this._cache.Add(Convert.ToInt64(row[1]), dictionary);
        }
      }
      else
        this._cache = new Dictionary<long, Dictionary<int, OfficeDocumentTypeSettingsForUnit>>(0);
    }
  }

  [CanBeNull]
  public OfficeDocumentTypeSettingsForUnit GetTypeSettingsForUnit(long unitID, int documentType)
  {
    Dictionary<int, OfficeDocumentTypeSettingsForUnit> dictionary;
    if (!this._cache.TryGetValue(unitID, out dictionary))
      return (OfficeDocumentTypeSettingsForUnit) null;
    OfficeDocumentTypeSettingsForUnit typeSettingsForUnit;
    dictionary.TryGetValue(documentType, out typeSettingsForUnit);
    return typeSettingsForUnit;
  }

  [CanBeNull]
  public Dictionary<int, OfficeDocumentTypeSettingsForUnit> GetSettingsForUnit(long unitID)
  {
    Dictionary<int, OfficeDocumentTypeSettingsForUnit> settingsForUnit;
    this._cache.TryGetValue(unitID, out settingsForUnit);
    return settingsForUnit;
  }

  public void SetSettingsForUnit(
    long unitID,
    [NotNull] Dictionary<int, OfficeDocumentTypeSettingsForUnit> settings)
  {
    if (this.GetSettingsForUnit(unitID) == null)
      this._cache.Add(unitID, settings);
    else
      this._cache[unitID] = settings;
    this.SaveSettingsToBase(unitID, settings);
  }

  private void SaveSettingsToBase(
    long unitID,
    [NotNull] Dictionary<int, OfficeDocumentTypeSettingsForUnit> settings)
  {
    using (SystemSessionKeeper systemSessionKeeper = new SystemSessionKeeper("OfficeServer.SaveSettingsToBase"))
    {
      IDBObject container = UnitContainer.GetContainer(systemSessionKeeper.Session, unitID);
      UnitSettingsFile.Save(settings, container);
    }
  }
}
