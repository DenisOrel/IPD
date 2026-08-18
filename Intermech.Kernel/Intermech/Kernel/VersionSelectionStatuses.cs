// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.VersionSelectionStatuses
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Search.ObjectsVisiblity;
using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.Kernel;

public static class VersionSelectionStatuses
{
  public static byte[] LoadResurce(string ResourceName)
  {
    Stream stream = (Stream) null;
    try
    {
      stream = typeof (VersionSelectionStatuses).Assembly.GetManifestResourceStream(ResourceName);
      if (stream == null)
        return new byte[0];
      byte[] buffer = new byte[stream.Length];
      stream.Read(buffer, 0, buffer.Length);
      return buffer;
    }
    finally
    {
      stream?.Close();
    }
  }

  public static void AddVersionSelectionStatuses(
    IUserSession session,
    IPluginStatusesTable pluginStatusesTable)
  {
    if (pluginStatusesTable == null)
      return;
    string str = "Intermech.Kernel.Resources.VersionSelection.";
    pluginStatusesTable.AddStatus("cad005f2-306c-11d8-b4e9-00304f19f545", 0, EnumDescConverter.GetEnumDescription((Enum) ObjectFiltrationState.fsNotRequired), (byte[]) null);
    pluginStatusesTable.AddStatus("cad005f2-306c-11d8-b4e9-00304f19f545", 1, EnumDescConverter.GetEnumDescription((Enum) ObjectFiltrationState.fsCompositeVersionNotFound), VersionSelectionStatuses.LoadResurce(str + "fsCompositeVersionNotFound.ico"));
    pluginStatusesTable.AddStatus("cad005f2-306c-11d8-b4e9-00304f19f545", 2, EnumDescConverter.GetEnumDescription((Enum) ObjectFiltrationState.fsFiltrationStopped), VersionSelectionStatuses.LoadResurce(str + "fsFiltrationStopped.ico"));
    pluginStatusesTable.AddStatus("cad005f2-306c-11d8-b4e9-00304f19f545", 3, EnumDescConverter.GetEnumDescription((Enum) ObjectFiltrationState.fsVersionNotFound), VersionSelectionStatuses.LoadResurce(str + "fsVersionNotFound.ico"));
    pluginStatusesTable.AddStatus("cad005f2-306c-11d8-b4e9-00304f19f545", 4, EnumDescConverter.GetEnumDescription((Enum) ObjectFiltrationState.fsInvalidRule), VersionSelectionStatuses.LoadResurce(str + "fsInvalidRule.ico"));
    pluginStatusesTable.AddStatus("cad005f2-306c-11d8-b4e9-00304f19f545", 5, EnumDescConverter.GetEnumDescription((Enum) ObjectFiltrationState.fsVariance), VersionSelectionStatuses.LoadResurce(str + "fsVariance.ico"));
    pluginStatusesTable.AddStatus("cad005f2-306c-11d8-b4e9-00304f19f545", 6, string.Empty, (byte[]) null);
    pluginStatusesTable.AddStatus("cad005f2-306c-11d8-b4e9-00304f19f545", 7, string.Empty, (byte[]) null);
    pluginStatusesTable.AddStatus("cad005f2-306c-11d8-b4e9-00304f19f545", 8, EnumDescConverter.GetEnumDescription((Enum) ObjectFiltrationState.fsCorresponding), VersionSelectionStatuses.LoadResurce(str + "fsCorresponding.ico"));
    pluginStatusesTable.AddStatus("cad005f2-306c-11d8-b4e9-00304f19f545", 9, EnumDescConverter.GetEnumDescription((Enum) ObjectFiltrationState.fsCompositeVersion), VersionSelectionStatuses.LoadResurce(str + "Hard concretised.ico"));
    pluginStatusesTable.AddStatus("cad005f2-306c-11d8-b4e9-00304f19f545", 17, EnumDescConverter.GetEnumDescription((Enum) ObjectFiltrationState.fsSoftConcretised), VersionSelectionStatuses.LoadResurce(str + "Soft concretised.ico"));
    pluginStatusesTable.AddStatus("cad005f2-306c-11d8-b4e9-00304f19f545", 10, EnumDescConverter.GetEnumDescription((Enum) ObjectFiltrationState.fsVersionFromMainContext), VersionSelectionStatuses.LoadResurce(str + "fsVersionFromMainContext.ico"));
    pluginStatusesTable.AddStatus("cad005f2-306c-11d8-b4e9-00304f19f545", 11, EnumDescConverter.GetEnumDescription((Enum) ObjectFiltrationState.fsVersionFromLinkedContext), VersionSelectionStatuses.LoadResurce(str + "fsVersionFromLinkedContext.ico"));
    pluginStatusesTable.AddStatus("cad005f2-306c-11d8-b4e9-00304f19f545", 12, LocalizationHolder.rm.GetString("Kernel_1006"), VersionSelectionStatuses.LoadResurce(str + "fsVersionConflictsWithContext.ico"));
    pluginStatusesTable.AddStatus(ObjectsVisibilityConstants.ObjectsVisiblityModuleGuid, 1, ObjectVisiblityStatuses.HasVisibilitySettings.GetDescription<ObjectVisiblityStatuses>(), VersionSelectionStatuses.LoadResurce(str + "Eye_128x128.ico"));
    pluginStatusesTable.AddStatus(ObjectsVisibilityConstants.ObjectsVisiblityModuleGuid, 2, ObjectVisiblityStatuses.HasVisibilityHiddenForAll.GetDescription<ObjectVisiblityStatuses>(), VersionSelectionStatuses.LoadResurce(str + "RedLock.ico"));
    List<IMSLifeCycleLevel> lcLevelsList = MetaDataHelper.GetLCLevelsList();
    lcLevelsList.Sort();
    for (int index = 0; index < lcLevelsList.Count; ++index)
    {
      IMSLifeCycleLevel imsLifeCycleLevel = lcLevelsList[index];
      IDBLifecycleLevelType lifecycleLevel = session.GetLifecycleLevel(imsLifeCycleLevel.LevelID);
      pluginStatusesTable.AddStatus("{7074E0E4-B3AB-4B3E-AD56-050CD256AF10}", index + 1, $"{LocalizationHolder.rm.GetString("Kernel_1007")}{imsLifeCycleLevel.Name}\"", lifecycleLevel.LevelIcon);
    }
  }

  public static void ReloadLevelsStatuses(
    IUserSession session,
    IPluginStatusesTable pluginStatusesTable)
  {
    if (pluginStatusesTable == null)
      return;
    pluginStatusesTable.RemoveStatuses("{7074E0E4-B3AB-4B3E-AD56-050CD256AF10}");
    List<IMSLifeCycleLevel> lcLevelsList = MetaDataHelper.GetLCLevelsList();
    lcLevelsList.Sort();
    for (int index = 0; index < lcLevelsList.Count; ++index)
    {
      IMSLifeCycleLevel imsLifeCycleLevel = lcLevelsList[index];
      try
      {
        IDBLifecycleLevelType lifecycleLevel = session.GetLifecycleLevel(imsLifeCycleLevel.LevelID);
        pluginStatusesTable.AddStatus("{7074E0E4-B3AB-4B3E-AD56-050CD256AF10}", index + 1, $"{LocalizationHolder.rm.GetString("Kernel_1007")}{imsLifeCycleLevel.Name}\"", lifecycleLevel.LevelIcon);
      }
      catch
      {
      }
    }
  }
}
