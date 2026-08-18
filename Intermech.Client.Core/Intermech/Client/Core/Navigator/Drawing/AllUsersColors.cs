
// Type: Intermech.Client.Core.Navigator.Drawing.AllUsersColors
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Client.Core.Navigator.Drawing;

/// <summary>все схемы данного пользователя</summary>
[Serializable]
public class AllUsersColors : ICloneable
{
  /// <summary>Guid настроек пользователя</summary>
  public static string UserSettingsGuid = "{11FB5123-4A88-4564-89D1-E7F977A51A42}";
  /// <summary>все схемы пользователя</summary>
  public List<ColorsSchemeProperties> schemes = new List<ColorsSchemeProperties>();
  /// <summary>текущая цветовая схема</summary>
  public ColorsSchemeProperties сurrentColorsScheme;
  /// <summary>стандартная схема</summary>
  public ColorsSchemeProperties defColorsScheme = new ColorsSchemeProperties(AllUsersColors.defSchemeName, string.Empty, new UIColorsScheme());
  /// <summary>название стандартной схемы</summary>
  public static string defSchemeName = LocalizationHolder.rm.GetString("Client.Core_1443");

  /// <summary>Сохранить цветовые схемы</summary>
  /// <param name="UserID">Идентификатор пользователя</param>
  public void SaveToUserSettings(long UserID)
  {
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (MemoryStream outStream = new MemoryStream())
      {
        try
        {
          new BinaryFormatter().Serialize((Stream) memoryStream, (object) this);
          ZLibStreamHelper.PackStream((Stream) memoryStream, ZLibCompressLevels.LevelMax, (Stream) outStream);
          customService[UserID, (object) AllUsersColors.UserSettingsGuid] = (object) outStream.ToArray();
        }
        catch
        {
        }
      }
    }
  }

  /// <summary>Выполнить синхронизацию</summary>
  /// <param name="UserID">Идентификатор пользователя</param>
  public void LoadFromUserSettings(long UserID)
  {
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService))
      return;
    byte[] buffer = customService[UserID, (object) AllUsersColors.UserSettingsGuid] as byte[];
    AllUsersColors allUsersColors = (AllUsersColors) null;
    if (buffer == null)
      return;
    try
    {
      MemoryStream memoryStream = new MemoryStream(buffer);
      MemoryStream outStream = new MemoryStream();
      long num = ZLibStreamHelper.UnpackStream((Stream) memoryStream, (Stream) outStream);
      if (num > 0L)
      {
        memoryStream.Close();
        memoryStream = outStream;
      }
      else
        memoryStream.Seek(0L, SeekOrigin.Begin);
      try
      {
        this.schemes = (new BinaryFormatter().Deserialize((Stream) memoryStream) as AllUsersColors).schemes;
      }
      catch
      {
        allUsersColors = (AllUsersColors) null;
      }
      finally
      {
        if (num > 0L)
        {
          outStream.Close();
        }
        else
        {
          memoryStream.Close();
          outStream.Close();
        }
      }
    }
    catch
    {
      allUsersColors = (AllUsersColors) null;
    }
  }

  /// <summary>текущая цветовая схема</summary>
  /// <returns></returns>
  public ColorsSchemeProperties CurrentColorsScheme
  {
    get
    {
      if (this.сurrentColorsScheme == null)
      {
        this.сurrentColorsScheme = this.defColorsScheme;
        string str = (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadString("CLIENT", "INTERFACE", "COLOR_SCHEME", Guid.Empty.ToString(), DBConfigMode.UserOnly);
        foreach (ColorsSchemeProperties scheme in this.schemes)
        {
          if (scheme.SchemeGuid == str)
          {
            this.сurrentColorsScheme = scheme;
            break;
          }
        }
      }
      return this.сurrentColorsScheme;
    }
    set => this.сurrentColorsScheme = value;
  }

  public AllUsersColors(List<ColorsSchemeProperties> schemes) => this.schemes = schemes;

  public AllUsersColors()
  {
  }

  public object Clone()
  {
    List<ColorsSchemeProperties> schemes = new List<ColorsSchemeProperties>();
    foreach (ColorsSchemeProperties scheme in this.schemes)
      schemes.Add(scheme.Clone() as ColorsSchemeProperties);
    return (object) new AllUsersColors(schemes);
  }
}
