// Decompiled with JetBrains decompiler
// Type: Intermech.Project.RemoteSettings
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Project;

public class RemoteSettings
{
  [NotNull]
  [NotEmpty]
  public static string SectionName = "ImProject.Portal";
  [CanBeNull]
  private static Dictionary<char, Guid> _siteSchemes;

  [CanBeNull]
  public static Dictionary<char, Guid> SiteSchemes
  {
    get
    {
      if (!RemoteSettings.Loaded)
        RemoteSettings.LoadSettings();
      return RemoteSettings._siteSchemes;
    }
  }

  public static void SaveSettings([CanBeNull] IUserSession session = null)
  {
    if (!RemoteSettings.Loaded)
      return;
    SessionKeeper sessionKeeper = session == null ? new SessionKeeper() : (SessionKeeper) null;
    try
    {
      session = session ?? sessionKeeper.Session;
      IDBConfigurations configurations = session.Configurations;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        XmlIni xmlIni = new XmlIni();
        foreach (KeyValuePair<char, Guid> siteScheme in RemoteSettings._siteSchemes)
        {
          char key;
          Guid guid1;
          siteScheme.Deconstruct<char, Guid>(out key, out guid1);
          char ch = key;
          Guid guid2 = guid1;
          xmlIni.WriteString("Schemes", ch.ToString(), guid2.ToString());
        }
        xmlIni.Save((Stream) memoryStream);
        BlobInformation config_info = new BlobInformation(memoryStream.Length, memoryStream.Length, DateTime.Now, RemoteSettings.SectionName, ArcMethods.NotPacked, string.Empty);
        configurations.WriteConfigData(config_info, memoryStream.ToArray(), 0L);
        memoryStream.Close();
      }
    }
    finally
    {
      session = (IUserSession) null;
      sessionKeeper?.Dispose();
    }
  }

  public static bool Loaded => RemoteSettings._siteSchemes != null;

  public static void LoadSettings([CanBeNull] IUserSession session = null)
  {
    SessionKeeper sessionKeeper = session == null ? new SessionKeeper() : (SessionKeeper) null;
    RemoteSettings._siteSchemes = new Dictionary<char, Guid>();
    try
    {
      session = session ?? sessionKeeper.Session;
      IDBConfigurations configurations = session.Configurations;
      byte[] config_file = Array.Empty<byte>();
      try
      {
        configurations.LoadConfigData(RemoteSettings.SectionName, out BlobInformation _, out config_file, 0L);
      }
      catch
      {
      }
      if (config_file.Length == 0)
        return;
      using (MemoryStream memoryStream = new MemoryStream(config_file))
      {
        memoryStream.Position = 0L;
        XmlIni xmlIni = new XmlIni();
        xmlIni.Load((Stream) memoryStream);
        ISitesCacheService customService = session.GetCustomService<ISitesCacheService>(false);
        if (customService == null)
          return;
        foreach (SiteInfo site in customService.Sites)
        {
          string g = xmlIni.ReadString("Schemes", site.Code.ToString());
          if (g != string.Empty)
            RemoteSettings._siteSchemes[site.Code] = new Guid(g);
        }
      }
    }
    finally
    {
      session = (IUserSession) null;
      sessionKeeper?.Dispose();
    }
  }
}
