
// Type: Intermech.Search.ButtonBars.IButtonBarServerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.ButtonBars
{
    public interface IButtonBarServerService
    {
      ButtonBar[] FindButtonBarsForCurrentUser(Guid userSessionGuid);

      ButtonBar[] FindButtonBarsForRole(Guid userSessionGuid, long roleVersionID);

      ButtonBar[] GetButtonBarsFromRoleConfiguration(
        Guid userSessionGuid,
        long roleConfigurationVersionID);

      void SaveButtonBarsForCurrentUser(
        Guid userSessionGuid,
        ButtonBar[] buttonBars,
        bool onlySettings = false);

      void SaveButtonBarsForRole(Guid userSessionGuid, long roleVersionID, ButtonBar[] buttonBars);

      void SaveButtonBarsToRoleConfiguration(
        Guid userSessionGuid,
        long roleConfigurationVersionID,
        ButtonBar[] buttonBars);

      bool CheckButtonBarsEditRightsForRole(Guid userSessionGuid, long roleVersionID);

      bool CheckButtonBarsEditRightsForRoleConfiguration(
        Guid userSessionGuid,
        long roleConfigurationVersionID);
    }
}
