// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.IMCadSettingsService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using System;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>Сервис настроек интеграции с CAD-системой</summary>
internal class IMCadSettingsService : IIMCadSettingsService
{
  /// <summary>Загрузка настроек</summary>
  /// <param name="session"></param>
  /// <param name="settings"></param>
  /// <returns></returns>
  private int LoadSettings(IUserSession session, out IIMCadSettings settings)
  {
    settings = (IIMCadSettings) null;
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    settings = (IIMCadSettings) new IMCadSettings();
    BlobInformation config_info;
    byte[] config_file;
    session.Configurations.LoadConfigData(nameof (IMCadSettingsService), out config_info, out config_file, 0L);
    if (config_info.RealFileSize > 0L)
    {
      XmlDocument xmlDoc = new XmlDocument();
      using (MemoryStream inStream = new MemoryStream(config_file))
        xmlDoc.Load((Stream) inStream);
      ((IMCadSettings) settings).LoadFromXml(xmlDoc);
    }
    else
      ((IMCadSettings) settings).LoadDefaultSettings();
    return 0;
  }

  /// <summary>Сохранение настроек</summary>
  /// <param name="session"></param>
  /// <param name="settings"></param>
  /// <returns></returns>
  private int SaveSettings(IUserSession session, IIMCadSettings settings)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (settings == null)
      throw new ArgumentNullException(nameof (settings));
    if (!session.IsAdmin)
      return 0;
    using (MemoryStream outStream = new MemoryStream())
    {
      if (settings is IMCadSettings imCadSettings)
        imCadSettings.SaveToXml().Save((Stream) outStream);
      session.Configurations.WriteConfigData(new BlobInformation(outStream.Length, outStream.Length, DateTime.Now, nameof (IMCadSettingsService), ArcMethods.NotPacked, string.Empty), outStream.ToArray(), 0L);
    }
    return 0;
  }

  /// <summary>Сохранение настроек</summary>
  /// <param name="settings"></param>
  /// <returns></returns>
  public int SaveSettings(IIMCadSettings settings)
  {
    if (settings == null)
      throw new ArgumentNullException(nameof (settings));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.SaveSettings(sessionKeeper.Session, settings);
  }

  /// <summary>Загрузка настроек</summary>
  /// <param name="settings"></param>
  /// <returns></returns>
  public int LoadSettings(out IIMCadSettings settings)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.LoadSettings(sessionKeeper.Session, out settings);
  }
}
