// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Server.AVSParametersService
// Assembly: Intermech.AVS.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DD9587A9-B8FC-4A8A-AB7E-E4D2C61BABE8
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AVS.Server.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Kernel;
using System;

#nullable disable
namespace Intermech.AVS.Server;

internal class AVSParametersService : LongLifeObject, IAppSettingsService<AvsSettings>
{
  private const int DB_PROPNAME_MAXSIZE = 32 /*0x20*/;

  private bool LoadSettingsSection(IUserSession session, AvsSettingsSection section)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (section == null)
      throw new ArgumentNullException(nameof (section));
    IDBConfigurations configurations = session.Configurations;
    string sectionName = section.SectionName;
    bool flag1 = false;
    foreach (string name in section.Names)
    {
      bool flag2 = section.IsAdmin(name);
      string ParamName = name.Truncate(31 /*0x1F*/);
      Type enumType = section.TypeOf(name);
      DBConfigMode configMode = flag2 ? DBConfigMode.GlobalOnly : DBConfigMode.UserAndGlobal;
      if (!(enumType != typeof (byte[])) || configurations.ParameterPresent("CLIENT", sectionName, ParamName, configMode))
      {
        if (enumType != typeof (byte[]))
          flag1 = true;
        object obj;
        if (enumType == typeof (Enum))
        {
          int DefaultValue = (int) (section[name] ?? (object) 0);
          obj = Enum.Parse(enumType, Convert.ToString(configurations.ReadInteger("CLIENT", sectionName, ParamName, (long) DefaultValue, flag2 ? DBConfigMode.GlobalOnly : DBConfigMode.UserAndGlobal)));
        }
        else if (enumType == typeof (bool))
        {
          bool flag3 = (bool) (section[name] ?? (object) false);
          obj = (object) (Convert.ToInt32(configurations.ReadInteger("CLIENT", sectionName, ParamName, flag3 ? 1L : 0L, flag2 ? DBConfigMode.GlobalOnly : DBConfigMode.UserAndGlobal)) > 0);
        }
        else if (enumType == typeof (int))
        {
          int DefaultValue = (int) (section[name] ?? (object) 0);
          obj = (object) Convert.ToInt32(configurations.ReadInteger("CLIENT", sectionName, ParamName, (long) DefaultValue, flag2 ? DBConfigMode.GlobalOnly : DBConfigMode.UserAndGlobal));
        }
        else if (enumType == typeof (string))
        {
          string DefaultValue = (string) (section[name] ?? (object) string.Empty);
          obj = (object) configurations.ReadString("CLIENT", sectionName, ParamName, DefaultValue, flag2 ? DBConfigMode.GlobalOnly : DBConfigMode.UserAndGlobal);
        }
        else if (enumType == typeof (byte[]))
        {
          string config_name = "AVS" + ParamName.Truncate(28);
          byte[] config_file;
          configurations.LoadConfigData(config_name, out BlobInformation _, out config_file, flag2 ? 0L : session.UserID);
          obj = (object) config_file;
        }
        else
          continue;
        section[name] = obj;
      }
    }
    return flag1;
  }

  private bool SaveSettingsSection(IUserSession session, AvsSettingsSection section)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (section == null)
      throw new ArgumentNullException(nameof (section));
    IDBConfigurations configurations = session.Configurations;
    string sectionName = section.SectionName;
    foreach (string name in section.Names)
    {
      bool flag = section.IsAdmin(name);
      string ParamName = name.Truncate(31 /*0x1F*/);
      Type type = section.TypeOf(name);
      if (!(!session.IsAdmin & flag))
      {
        object obj = section[name];
        if (type == typeof (Enum))
          configurations.WriteInteger("CLIENT", sectionName, ParamName, (long) (int) obj, flag ? 0L : session.UserID);
        else if (type == typeof (bool))
          configurations.WriteInteger("CLIENT", sectionName, ParamName, (bool) obj ? 1L : 0L, flag ? 0L : session.UserID);
        else if (type == typeof (int))
          configurations.WriteInteger("CLIENT", sectionName, ParamName, (long) (int) obj, flag ? 0L : session.UserID);
        else if (type == typeof (string))
          configurations.WriteString("CLIENT", sectionName, ParamName, (string) obj, flag ? 0L : session.UserID);
        else if (type == typeof (byte[]))
        {
          byte[] config_file = obj as byte[];
          string fileName = "AVS" + ParamName.Truncate(28);
          BlobInformation config_info = new BlobInformation((long) config_file.Length, (long) config_file.Length, DateTime.Now, fileName, ArcMethods.NotPacked, string.Empty);
          configurations.WriteConfigData(config_info, config_file, flag ? 0L : session.UserID);
        }
      }
    }
    return true;
  }

  public bool SaveSettings(Guid sessionGuid, AvsSettings avsParams)
  {
    if (sessionGuid == Guid.Empty || avsParams == null)
      return false;
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    return sessionById != null && this.SaveSettingsSection(sessionById, avsParams.General) && this.SaveSettingsSection(sessionById, avsParams.Podbor) && this.SaveSettingsSection(sessionById, avsParams.PosDesignation) && this.SaveSettingsSection(sessionById, avsParams.CheckSPec) && this.SaveSettingsSection(sessionById, avsParams.CheckEList);
  }

  public bool LoadSettings(Guid sessionGuid, ref AvsSettings avsParams)
  {
    if (sessionGuid == Guid.Empty)
      throw new ArgumentValueEmptyException(nameof (sessionGuid));
    if (avsParams == null)
      throw new ArgumentNullException(nameof (avsParams));
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (sessionById == null)
      throw new UserSessionLostException($"Ошибка получения экземпляра сессии по ключу '{sessionGuid}'.");
    return this.LoadSettingsSection(sessionById, avsParams.General) && this.LoadSettingsSection(sessionById, avsParams.Podbor) && this.LoadSettingsSection(sessionById, avsParams.PosDesignation) && this.LoadSettingsSection(sessionById, avsParams.CheckSPec) && this.LoadSettingsSection(sessionById, avsParams.CheckEList);
  }
}
