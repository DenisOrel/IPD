// Decompiled with JetBrains decompiler
// Type: Intermech.Project.SettingsForObjTypeLink
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Project;

/// <summary>Readonly оболочка над Dictionary [int, ImportObjectSettingsForObjType]</summary>
public class SettingsForObjTypeLink
{
  [NotNull]
  private readonly ImportObjectSettings _defaultImportObjectSettings;
  [NotNull]
  private readonly Dictionary<int, ImportObjectSettingsForObjType> _settingsForObjTypes;
  [NotNull]
  private Dictionary<int, ImportObjectSettingsBase> _cacheSettingsForObjTypes;

  public SettingsForObjTypeLink(
    [NotNull] ImportObjectSettings defaultImportObjectSettings,
    [NotNull] Dictionary<int, ImportObjectSettingsForObjType> settingsForObjTypes)
  {
    this._defaultImportObjectSettings = defaultImportObjectSettings;
    this._settingsForObjTypes = settingsForObjTypes;
    this.ClearCache();
  }

  [NotNull]
  public ImportObjectSettingsBase this[int objTypeID]
  {
    get
    {
      if (objTypeID == -1)
        return (ImportObjectSettingsBase) this._defaultImportObjectSettings;
      ImportObjectSettingsBase objectSettingsBase1 = (ImportObjectSettingsBase) null;
      foreach (int key in Enumeration.Create<int>(objTypeID).Concat<int>((IEnumerable<int>) MetaDataHelperService.Instance.GetObjectTypeParentsID(objTypeID)))
      {
        if (this._cacheSettingsForObjTypes.TryGetValue(key, out objectSettingsBase1))
          break;
      }
      ImportObjectSettingsBase objectSettingsBase2 = objectSettingsBase1 ?? (ImportObjectSettingsBase) this._defaultImportObjectSettings;
      if (!this._cacheSettingsForObjTypes.ContainsKey(objTypeID))
        this._cacheSettingsForObjTypes[objTypeID] = objectSettingsBase2;
      return objectSettingsBase2;
    }
  }

  public void ClearCache()
  {
    this._cacheSettingsForObjTypes = this._settingsForObjTypes.ToDictionary<KeyValuePair<int, ImportObjectSettingsForObjType>, int, ImportObjectSettingsBase>((Func<KeyValuePair<int, ImportObjectSettingsForObjType>, int>) (pair => pair.Key), (Func<KeyValuePair<int, ImportObjectSettingsForObjType>, ImportObjectSettingsBase>) (pair => (ImportObjectSettingsBase) pair.Value));
  }
}
