// Decompiled with JetBrains decompiler
// Type: Intermech.Search.UI.DefaultCommandsSettingsServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Search.Utilities;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Search.UI;

public sealed class DefaultCommandsSettingsServerService : 
  LongLifeObject,
  IDefaultCommandsSettingsServerService
{
  public DefaultCommandSettings[] FindDefaultCommandsSettingsForRole(
    Guid userSessionGuid,
    long roleVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(roleVersionID) ? this.FindDefaultCommandsSettingsForRoleInternal(roleVersionID) : throw new ArgumentException();
  }

  public DefaultCommandSettings[] GetDefaultCommandsSettingsFromRoleConfiguration(
    Guid userSessionGuid,
    long roleConfigurationVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(roleConfigurationVersionID) ? this.GetDefaultCommandsSettingsFromRoleConfigurationInternal(roleConfigurationVersionID) : throw new ArgumentException();
  }

  public void SaveDefaultCommandsSettingsForRole(
    Guid userSessionGuid,
    long roleVersionID,
    DefaultCommandSettings[] defaultCommandsSettings)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(roleVersionID))
        throw new ArgumentException();
      if (defaultCommandsSettings == null)
        throw new ArgumentNullException("defaultCommandSettings");
      this.SaveDefaultCommandsSettingsForRoleInternal(roleVersionID, defaultCommandsSettings);
    }
  }

  public void SaveDefaultCommandsSettingsToRoleConfiguration(
    Guid userSessionGuid,
    long roleConfigurationVersionID,
    DefaultCommandSettings[] defaultCommandsSettings)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(roleConfigurationVersionID))
        throw new ArgumentException();
      if (defaultCommandsSettings == null)
        throw new ArgumentNullException(nameof (defaultCommandsSettings));
      this.SaveDefaultCommandsSettingsToRoleConfigurationInternal(roleConfigurationVersionID, defaultCommandsSettings);
    }
  }

  private DefaultCommandSettings[] FindDefaultCommandsSettingsForRoleInternal(long roleVersionID)
  {
    return this.GetDefaultCommandsSettingsFromRoleConfigurationInternal(this.GetRoleConfigurationVersionIDForRole(roleVersionID));
  }

  private DefaultCommandSettings[] GetDefaultCommandsSettingsFromRoleConfigurationInternal(
    long roleConfigurationVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DefaultCommandSettings[] defaultCommandSettingsArray = (DefaultCommandSettings[]) null;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new BlobProcReader(this.GetDefaultCommandsSettingsAttributeForRoleConfiguration(sessionKeeper.Session, roleConfigurationVersionID), 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(sessionKeeper.Session);
        if (memoryStream.Length > 0L)
        {
          memoryStream.Seek(0L, SeekOrigin.Begin);
          defaultCommandSettingsArray = new BinaryFormatter().Deserialize((Stream) memoryStream) as DefaultCommandSettings[];
        }
      }
      return defaultCommandSettingsArray ?? new DefaultCommandSettings[0];
    }
  }

  private void SaveDefaultCommandsSettingsForRoleInternal(
    long roleVersionID,
    DefaultCommandSettings[] defaultCommandsSettings)
  {
    this.SaveDefaultCommandsSettingsToRoleConfigurationInternal(this.GetRoleConfigurationVersionIDForRole(roleVersionID), defaultCommandsSettings);
  }

  private void SaveDefaultCommandsSettingsToRoleConfigurationInternal(
    long roleConfigurationVersionID,
    DefaultCommandSettings[] defaultCommandsSettings)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) memoryStream, (object) defaultCommandsSettings);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        BlobInformation aBlobInformation = new BlobInformation()
        {
          ArcMethod = ArcMethods.ZLibPacked,
          FileType = FileTypes.ftNormal,
          ModifyDate = DateTime.Now,
          RealFileSize = memoryStream.Length
        };
        new BlobProcWriter(this.GetDefaultCommandsSettingsAttributeForRoleConfiguration(sessionKeeper.Session, roleConfigurationVersionID), 0, aBlobInformation, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData(sessionKeeper.Session);
      }
    }
  }

  private long GetRoleConfigurationVersionIDForRole(long roleVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttribute = sessionKeeper.Session.GetObjectAttribute(roleVersionID, (object) Constants.RoleConfigurationAttributeTypeID, false, false);
      return objectAttribute != null ? objectAttribute.AsInteger : 0L;
    }
  }

  private IDBAttribute GetDefaultCommandsSettingsAttributeForRoleConfiguration(
    IUserSession userSession,
    long roleConfigurationVersionID)
  {
    return userSession.GetObjectAttribute(roleConfigurationVersionID, (object) Constants.DefaultCommandsSettingsAttributeTypeID, true, false);
  }
}
