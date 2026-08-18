
// Type: Intermech.Search.UI.IDefaultCommandsSettingsServerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.UI
{
    public interface IDefaultCommandsSettingsServerService
    {
      DefaultCommandSettings[] FindDefaultCommandsSettingsForRole(
        Guid userSessionGuid,
        long roleVersionID);

      DefaultCommandSettings[] GetDefaultCommandsSettingsFromRoleConfiguration(
        Guid userSessionGuid,
        long roleConfigurationVersionID);

      void SaveDefaultCommandsSettingsForRole(
        Guid userSessionGuid,
        long roleVersionID,
        DefaultCommandSettings[] defaultCommandsSettings);

      void SaveDefaultCommandsSettingsToRoleConfiguration(
        Guid userSessionGuid,
        long roleConfigurationVersionID,
        DefaultCommandSettings[] defaultCommandsSettings);
    }
}
