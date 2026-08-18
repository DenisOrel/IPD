// Decompiled with JetBrains decompiler
// Type: Intermech.Search.ButtonBars.ButtonBarServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;


namespace Intermech.Search.ButtonBars;

public sealed class ButtonBarServerService : LongLifeObject, IButtonBarServerService
{
  private const string ButtonBarsUserConfigurationFileName = "TechCardBarsSettings";
  private static readonly ButtonBarsSerializer _buttonBarsSerializer = new ButtonBarsSerializer();

  public ButtonBar[] FindButtonBarsForCurrentUser(Guid userSessionGuid)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this.FindButtonBarsForCurrentUserInternal();
  }

  public ButtonBar[] FindButtonBarsForRole(Guid userSessionGuid, long roleVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(roleVersionID) ? this.FindButtonBarsForRoleInternal(roleVersionID) : throw new ArgumentException();
  }

  public ButtonBar[] GetButtonBarsFromRoleConfiguration(
    Guid userSessionGuid,
    long roleConfigurationVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(roleConfigurationVersionID) ? this.GetButtonBarsFromRoleConfigurationInternal(roleConfigurationVersionID) : throw new ArgumentException();
  }

  public void SaveButtonBarsForCurrentUser(
    Guid userSessionGuid,
    ButtonBar[] buttonBars,
    bool onlySettings = false)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (buttonBars == null)
        throw new ArgumentNullException("buttonPanelSettingsPack");
      this.SaveButtonBarsForCurrentUserInternal(buttonBars, onlySettings);
    }
  }

  public void SaveButtonBarsForRole(
    Guid userSessionGuid,
    long roleVersionID,
    ButtonBar[] buttonBars)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(roleVersionID))
        throw new ArgumentException();
      if (buttonBars == null)
        throw new ArgumentNullException("buttonPanelSettingsPack");
      this.SaveButtonBarsForRoleInternal(roleVersionID, buttonBars);
    }
  }

  public void SaveButtonBarsToRoleConfiguration(
    Guid userSessionGuid,
    long roleConfigurationVersionID,
    ButtonBar[] buttonBars)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(roleConfigurationVersionID))
        throw new ArgumentException();
      if (buttonBars == null)
        throw new ArgumentNullException(nameof (buttonBars));
      this.SaveButtonBarsToRoleConfigurationInternal(roleConfigurationVersionID, buttonBars);
    }
  }

  public bool CheckButtonBarsEditRightsForRole(Guid userSessionGuid, long roleVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(roleVersionID) ? this.CheckButtonBarsEditRightsForRoleInternal(roleVersionID) : throw new ArgumentException();
  }

  public bool CheckButtonBarsEditRightsForRoleConfiguration(
    Guid userSessionGuid,
    long roleConfigurationVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(roleConfigurationVersionID) ? this.CheckButtonBarsEditRightsForRoleConfigurationInternal(roleConfigurationVersionID) : throw new ArgumentException();
  }

  private ButtonBar[] FindButtonBarsForCurrentUserInternal()
  {
    bool onlySettings = false;
    ButtonBar[] userConfiguration = this.GetButtonBarsForCurrentUserFromUserConfiguration(out onlySettings);
    if (!onlySettings && userConfiguration.Length != 0)
      return userConfiguration;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ButtonBar[] barsForRoleInternal = this.FindButtonBarsForRoleInternal(sessionKeeper.Session.RoleID);
      foreach (ButtonBar buttonBar1 in barsForRoleInternal)
      {
        ButtonBar buttonBarForRole = buttonBar1;
        ButtonBar buttonBar2 = ((IEnumerable<ButtonBar>) userConfiguration).FirstOrDefault<ButtonBar>((Func<ButtonBar, bool>) (o => o.Guid == buttonBarForRole.Guid));
        if (buttonBar2 != null)
        {
          buttonBarForRole.ContainerGuid = buttonBar2.ContainerGuid;
          buttonBarForRole.DockLine = buttonBar2.DockLine;
          buttonBarForRole.DockOffset = buttonBar2.DockOffset;
          buttonBarForRole.Visible = buttonBar2.Visible;
          foreach (ButtonBarButton button in (Collection<ButtonBarButton>) buttonBarForRole.Buttons)
          {
            ButtonBarButton buttonBarButtonForRole = button;
            ButtonBarButton buttonBarButton = buttonBar2.Buttons.FirstOrDefault<ButtonBarButton>((Func<ButtonBarButton, bool>) (o => o.CommandName == buttonBarButtonForRole.CommandName));
            if (buttonBarButton != null)
              buttonBarButtonForRole.Visible = buttonBarButton.Visible;
          }
        }
      }
      return barsForRoleInternal;
    }
  }

  private ButtonBar[] GetButtonBarsForCurrentUserFromUserConfiguration(out bool onlySettings)
  {
    onlySettings = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      byte[] config_file = (byte[]) null;
      BlobInformation config_info;
      sessionKeeper.Session.Configurations.LoadConfigData("TechCardBarsSettings", out config_info, out config_file);
      if (config_info.RealFileSize > 0L)
      {
        using (MemoryStream inStream = new MemoryStream(config_file))
        {
          if (config_info.ArcMethod != ArcMethods.ZLibPacked)
            return ButtonBarServerService._buttonBarsSerializer.Deserialize((Stream) inStream, out onlySettings);
          using (MemoryStream outStream = new MemoryStream())
          {
            ZLibStreamHelper.UnpackStream((Stream) inStream, (Stream) outStream);
            return ButtonBarServerService._buttonBarsSerializer.Deserialize((Stream) outStream, out onlySettings);
          }
        }
      }
    }
    return new ButtonBar[0];
  }

  private ButtonBar[] FindButtonBarsForRoleInternal(long roleVersionID)
  {
    long versionIdForRole = this.GetRoleConfigurationVersionIDForRole(roleVersionID);
    return !ObjectHelper.IsUnknownObjectVersionID(versionIdForRole) ? this.GetButtonBarsFromRoleConfigurationInternal(versionIdForRole) : new ButtonBar[0];
  }

  private ButtonBar[] GetButtonBarsFromRoleConfigurationInternal(long roleConfigurationVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (MemoryStream aDestStream = new MemoryStream())
      {
        new BlobProcReader(sessionKeeper.Session.GetObject(roleConfigurationVersionID).Attributes.FindByID(Constants.ButtonBarsSettignsAttributeTypeID), 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(sessionKeeper.Session);
        aDestStream.Seek(0L, SeekOrigin.Begin);
        return aDestStream.Length > 0L ? ButtonBarServerService._buttonBarsSerializer.Deserialize((Stream) aDestStream, out bool _) : new ButtonBar[0];
      }
    }
  }

  private void SaveButtonBarsForCurrentUserInternal(ButtonBar[] buttonBars, bool onlySettings)
  {
    if (buttonBars.Length != 0)
    {
      bool onlySettings1;
      ButtonBar[] userConfiguration = this.GetButtonBarsForCurrentUserFromUserConfiguration(out onlySettings1);
      if (!onlySettings1 && userConfiguration.Length != 0)
        onlySettings = false;
    }
    else
      onlySettings = false;
    using (MemoryStream inStream = new MemoryStream())
    {
      using (MemoryStream outStream = new MemoryStream())
      {
        ButtonBarServerService._buttonBarsSerializer.Serialize(buttonBars, (Stream) inStream, onlySettings);
        inStream.Seek(0L, SeekOrigin.Begin);
        ZLibStreamHelper.PackStream((Stream) inStream, ZLibCompressLevels.LevelNormal, (Stream) outStream);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          BlobInformation config_info = new BlobInformation()
          {
            ArcMethod = ArcMethods.ZLibPacked,
            FileName = "TechCardBarsSettings",
            ModifyDate = DateTime.Now,
            PackedFileSize = outStream.Length,
            RealFileSize = inStream.Length
          };
          byte[] numArray = new byte[outStream.Length];
          outStream.Seek(0L, SeekOrigin.Begin);
          outStream.Read(numArray, 0, (int) outStream.Length);
          sessionKeeper.Session.Configurations.WriteConfigData(config_info, numArray);
        }
      }
    }
  }

  private void SaveButtonBarsForRoleInternal(long roleVersionID, ButtonBar[] buttonBars)
  {
    this.SaveButtonBarsToRoleConfigurationInternal(this.GetRoleConfigurationVersionIDForRole(roleVersionID), buttonBars);
  }

  private void SaveButtonBarsToRoleConfigurationInternal(
    long roleConfigurationVersionID,
    ButtonBar[] buttonBars)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute byId = sessionKeeper.Session.GetObject(roleConfigurationVersionID).Attributes.FindByID(Constants.ButtonBarsSettignsAttributeTypeID);
      using (MemoryStream aSourceStream = new MemoryStream())
      {
        ButtonBarServerService._buttonBarsSerializer.Serialize(buttonBars, (Stream) aSourceStream);
        aSourceStream.Seek(0L, SeekOrigin.Begin);
        BlobInformation aBlobInformation = new BlobInformation()
        {
          ArcMethod = ArcMethods.ZLibPacked,
          ModifyDate = DateTime.Now,
          RealFileSize = aSourceStream.Length
        };
        new BlobProcWriter(byId, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData(sessionKeeper.Session);
      }
    }
  }

  private bool CheckButtonBarsEditRightsForRoleInternal(long roleVersionID)
  {
    return this.CheckButtonBarsEditRightsForRoleConfigurationInternal(this.GetRoleConfigurationVersionIDForRole(roleVersionID));
  }

  private bool CheckButtonBarsEditRightsForRoleConfigurationInternal(long roleConfigurationVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(roleConfigurationVersionID);
      return (dbObject as IDBSecurity).CheckAccess(ActionType.Edit) && dbObject.ObjectModifyMode != ObjectModifyModes.CantModify;
    }
  }

  private long GetRoleConfigurationVersionIDForRole(long roleVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(roleVersionID).Attributes.FindByID(Constants.RoleConfigurationAttributeTypeID).AsInteger;
  }
}
