
// Type: Intermech.Search.UI.Commands.CommandStatisticsRepository
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Search.UI.Commands;

public sealed class CommandStatisticsRepository : ICommandStatisticsRepository
{
  private bool _isLoaded;
  private Dictionary<string, CommandStatistics> _dictionary = new Dictionary<string, CommandStatistics>();
  private const string ConfigurationFileName = "UI.CommandsStatistics";

  public void AddOrUpdate(string commandName, CommandStatistics statistics)
  {
    if (string.IsNullOrEmpty(commandName))
      throw new ArgumentException();
    if (statistics == null)
      throw new ArgumentNullException(nameof (statistics));
    if (!this._isLoaded)
      throw new InvalidOperationException();
    lock (this._dictionary)
    {
      if (this.Find(commandName) == null)
        this._dictionary.Add(commandName, statistics);
      else
        this._dictionary[commandName] = statistics;
    }
  }

  public CommandStatistics Find(string commandName)
  {
    if (string.IsNullOrEmpty(commandName))
      throw new ArgumentException();
    if (!this._isLoaded)
      throw new InvalidOperationException();
    CommandStatistics commandStatistics = (CommandStatistics) null;
    this._dictionary.TryGetValue(commandName, out commandStatistics);
    return commandStatistics == null ? (CommandStatistics) null : (CommandStatistics) commandStatistics.Clone();
  }

  public void Load()
  {
    if (this._isLoaded)
      return;
    lock (this._dictionary)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        byte[] config_file = (byte[]) null;
        sessionKeeper.Session.Configurations.LoadConfigData("UI.CommandsStatistics", out BlobInformation _, out config_file);
        this._dictionary = config_file == null || config_file.Length == 0 ? new Dictionary<string, CommandStatistics>() : this.UnpackAndDeserializeCommandsStatistics(config_file);
      }
    }
    this._isLoaded = true;
  }

  public void Save()
  {
    if (!this._isLoaded)
      throw new InvalidOperationException();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      byte[] bytes = this.SerializeCommandsStatistics(this._dictionary);
      byte[] config_file = this.PackData(bytes);
      BlobInformation config_info = new BlobInformation()
      {
        ArcMethod = ArcMethods.ZLibPacked,
        FileName = "UI.CommandsStatistics",
        ModifyDate = DateTime.Now,
        PackedFileSize = (long) config_file.Length,
        RealFileSize = (long) bytes.Length
      };
      sessionKeeper.Session.Configurations.WriteConfigData(config_info, config_file);
    }
  }

  private byte[] SerializeCommandsStatistics(Dictionary<string, CommandStatistics> dictionary)
  {
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) dictionary);
      return serializationStream.GetBuffer();
    }
  }

  private byte[] PackData(byte[] bytes)
  {
    using (MemoryStream inStream = new MemoryStream(bytes))
    {
      using (MemoryStream outStream = new MemoryStream())
      {
        ZLibStreamHelper.PackStream((Stream) inStream, ZLibCompressLevels.LevelMax, (Stream) outStream);
        return outStream.GetBuffer();
      }
    }
  }

  private Dictionary<string, CommandStatistics> UnpackAndDeserializeCommandsStatistics(byte[] bytes)
  {
    using (MemoryStream inStream = new MemoryStream(bytes))
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        ZLibStreamHelper.UnpackStream((Stream) inStream, (Stream) memoryStream);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        return new BinaryFormatter().Deserialize((Stream) memoryStream) as Dictionary<string, CommandStatistics>;
      }
    }
  }
}
