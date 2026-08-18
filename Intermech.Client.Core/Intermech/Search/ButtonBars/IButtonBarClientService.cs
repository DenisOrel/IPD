
// Type: Intermech.Search.ButtonBars.IButtonBarClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Search.ButtonBars;

public interface IButtonBarClientService
{
  event EventHandler ButtonBarsForCurrentUserChanged;

  ButtonBar[] FindButtonBarsForCurrentUser();

  ButtonBar[] FindButtonBarsForRole(long roleVersionID);

  ButtonBar[] GetButtonBarsFromRoleConfiguration(long roleConfigurationVersionID);

  void SaveButtonBarsForCurrentUser(ButtonBar[] buttonBars, bool onlySettings = false);

  void SaveButtonBarsForRole(long roleVersionID, ButtonBar[] buttonBars);

  void SaveButtonBarsToRoleConfiguration(long roleConfigurationVersionID, ButtonBar[] buttonBars);
}
