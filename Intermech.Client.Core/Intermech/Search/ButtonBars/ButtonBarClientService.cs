
// Type: Intermech.Search.ButtonBars.ButtonBarClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;


namespace Intermech.Search.ButtonBars;

public sealed class ButtonBarClientService : IButtonBarClientService
{
  public event EventHandler ButtonBarsForCurrentUserChanged;

  public ButtonBar[] FindButtonBarsForCurrentUser()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ((IButtonBarServerService) sessionKeeper.Session.GetCustomService(typeof (IButtonBarServerService))).FindButtonBarsForCurrentUser(sessionKeeper.Session.SessionGUID);
  }

  public ButtonBar[] FindButtonBarsForRole(long roleVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ((IButtonBarServerService) sessionKeeper.Session.GetCustomService(typeof (IButtonBarServerService))).FindButtonBarsForRole(sessionKeeper.Session.SessionGUID, roleVersionID);
  }

  public ButtonBar[] GetButtonBarsFromRoleConfiguration(long roleConfigurationVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ((IButtonBarServerService) sessionKeeper.Session.GetCustomService(typeof (IButtonBarServerService))).GetButtonBarsFromRoleConfiguration(sessionKeeper.Session.SessionGUID, roleConfigurationVersionID);
  }

  public void SaveButtonBarsForCurrentUser(ButtonBar[] buttonBars, bool onlySettings = false)
  {
    if (buttonBars == null)
      throw new ArgumentNullException(nameof (buttonBars));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ((IButtonBarServerService) sessionKeeper.Session.GetCustomService(typeof (IButtonBarServerService))).SaveButtonBarsForCurrentUser(sessionKeeper.Session.SessionGUID, buttonBars, onlySettings);
      this.OnButtonBarsForCurrentUserChanged();
    }
  }

  public void SaveButtonBarsForRole(long roleVersionID, ButtonBar[] buttonBars)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(roleVersionID))
      throw new ArgumentException();
    if (buttonBars == null)
      throw new ArgumentNullException(nameof (buttonBars));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ((IButtonBarServerService) sessionKeeper.Session.GetCustomService(typeof (IButtonBarServerService))).SaveButtonBarsForRole(sessionKeeper.Session.SessionGUID, roleVersionID, buttonBars);
      this.OnButtonBarsForCurrentUserChanged();
    }
  }

  public void SaveButtonBarsToRoleConfiguration(
    long roleConfigurationVersionID,
    ButtonBar[] buttonBars)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(roleConfigurationVersionID))
      throw new ArgumentException();
    if (buttonBars == null)
      throw new ArgumentNullException(nameof (buttonBars));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ((IButtonBarServerService) sessionKeeper.Session.GetCustomService(typeof (IButtonBarServerService))).SaveButtonBarsToRoleConfiguration(sessionKeeper.Session.SessionGUID, roleConfigurationVersionID, buttonBars);
      this.OnButtonBarsForCurrentUserChanged();
    }
  }

  private void OnButtonBarsForCurrentUserChanged()
  {
    EventHandler currentUserChanged = this.ButtonBarsForCurrentUserChanged;
    if (currentUserChanged == null)
      return;
    currentUserChanged((object) this, EventArgs.Empty);
  }
}
