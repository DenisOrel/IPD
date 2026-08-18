// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.AttributeTable
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Memoization;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class AttributeTable
{
  private readonly SettingsService settingsSvc;
  private readonly AttributesTableKind tableKind;
  private readonly StateMonitorCacheGuard renameTableGuard;
  private List<Tuple<StringKey, StringKey, bool>> renameTableCache;

  public AttributeTable(SettingsService settingsSvc, AttributesTableKind tableKind)
  {
    this.settingsSvc = settingsSvc != null ? settingsSvc : throw new ArgumentNullException(nameof (settingsSvc));
    this.tableKind = tableKind;
    this.renameTableGuard = new StateMonitorCacheGuard(settingsSvc.GetSettingsStateMonitor());
    this.renameTableGuard.ResetCache += new EventHandler(this.OnRebuildRenameTableCache);
  }

  private void OnRebuildRenameTableCache(object sender, EventArgs e)
  {
    this.renameTableCache = AttributeTable.GetTable(this.settingsSvc.GetSettings(), this.tableKind);
  }

  private static List<Tuple<StringKey, StringKey, bool>> GetTable(
    ADIntegratorSettings settings,
    AttributesTableKind tableKind)
  {
    switch (tableKind)
    {
      case AttributesTableKind.SchemaDocumentAttributes:
        return settings.DocumentAttributesTable;
      case AttributesTableKind.AssemblyAttributes:
        return settings.AssemblyAttributesTable;
      case AttributesTableKind.ComponentAttributes:
        return settings.PartAttributesTable;
      case AttributesTableKind.ProjectAttributes:
        return settings.ProjectAttributes;
      default:
        throw new NotImplementedException();
    }
  }

  public AttributesTableKind Kind => this.tableKind;

  public List<Tuple<StringKey, StringKey, bool>> Rows
  {
    get
    {
      lock (this)
      {
        this.renameTableGuard.CheckCache();
        return this.renameTableCache;
      }
    }
  }

  public StringKey GetFormatterValueKey(StringKey attributeKey, StringKey defaultValueKey)
  {
    if (attributeKey == (StringKey) null)
      throw new ArgumentNullException(nameof (attributeKey));
    if (defaultValueKey == (StringKey) null)
      throw new ArgumentNullException(nameof (defaultValueKey));
    Tuple<StringKey, StringKey, bool> tuple = this.Rows.Find((Predicate<Tuple<StringKey, StringKey, bool>>) (item => item.Item1 == attributeKey));
    return tuple != null ? tuple.Item2 : defaultValueKey;
  }
}
