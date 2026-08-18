
// Type: Intermech.PropertyEditors.ConfigCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.PropertyEditors;

/// <summary>
/// кэш MemoryStream - в частности для хранения конфигурации колонок для GridControl (отображение списка) - категория как key
/// </summary>
public class ConfigCache
{
  public static readonly Guid ObjTypes4AttrConfigKey = new Guid("{52281C51-9816-4652-838B-B56E1B342393}");
  public static readonly Guid RelTypes4AttrConfigKey = new Guid("{5F9E233F-2F42-42a3-AB28-1F6206E45936}");
  private static Hashtable cache = (Hashtable) null;
  private static bool loaded = false;
  private static bool changed = false;
  private static string filename = ClientConsts.ListLayoutConfig.ConfigFile;

  public static bool Loaded => ConfigCache.loaded;

  public static bool Changed => ConfigCache.changed;

  public static void Clear()
  {
    ConfigCache.loaded = false;
    ConfigCache.changed = false;
    ConfigCache.cache = (Hashtable) null;
  }

  public static void Empty()
  {
    ConfigCache.loaded = true;
    ConfigCache.changed = true;
    ConfigCache.cache = new Hashtable();
    ConfigCache.SaveConfig();
  }

  public static MemoryStream GetConfig(Guid key)
  {
    return !ConfigCache.loaded ? (MemoryStream) null : (MemoryStream) ConfigCache.cache[(object) key];
  }

  public static void SetConfig(Guid key, MemoryStream ms)
  {
    if (!ConfigCache.loaded)
      return;
    if (ConfigCache.cache.Contains((object) key))
      ConfigCache.cache[(object) key] = (object) ms;
    else
      ConfigCache.cache.Add((object) key, (object) ms);
    ConfigCache.changed = true;
  }

  public static void LoadConfig()
  {
    using (ImChunkedStream imChunkedStream = new ImChunkedStream())
    {
      ConfigCache.Clear();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        BlobProcReader blobProcReader = new BlobProcReader(sessionKeeper.Session.Configurations.GetConfigAttribute(ConfigCache.filename), 0, (Stream) imChunkedStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
        bool flag = false;
        try
        {
          blobProcReader.ReadData();
        }
        catch
        {
          flag = true;
        }
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        if (imChunkedStream.Length > 0L && !flag)
        {
          imChunkedStream.Position = 0L;
          try
          {
            ConfigCache.cache = (Hashtable) binaryFormatter.Deserialize((Stream) imChunkedStream);
          }
          catch
          {
            ConfigCache.cache = new Hashtable();
            ConfigCache.loaded = true;
            ConfigCache.changed = false;
          }
        }
        else
          ConfigCache.cache = new Hashtable();
      }
      ConfigCache.loaded = true;
      ConfigCache.changed = false;
    }
  }

  public static void SaveConfig()
  {
    if (!ConfigCache.loaded || !ConfigCache.changed)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (ImChunkedStream imChunkedStream = new ImChunkedStream())
      {
        new BinaryFormatter().Serialize((Stream) imChunkedStream, (object) ConfigCache.cache);
        BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, ConfigCache.filename, ArcMethods.ZLibPacked, string.Empty);
        new BlobProcWriter(sessionKeeper.Session.Configurations.GetConfigAttribute(ConfigCache.filename), 0, aBlobInformation, (Stream) imChunkedStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      }
    }
    ConfigCache.changed = false;
  }
}
