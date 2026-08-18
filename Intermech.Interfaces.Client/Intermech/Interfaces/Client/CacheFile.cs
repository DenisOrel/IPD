// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CacheFile
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Файл с клиентским кэшем.</summary>
internal class CacheFile
{
  /// <summary>Имя файла на локальном диске</summary>
  private string _fileFullName = string.Empty;
  /// <summary>Имя бэкапа кэша на локальном диске</summary>
  private string _fileBakFullName = string.Empty;
  /// <summary>
  /// Имя временного файла (флага об правильной записи на диск кэша) на локальном диске
  /// </summary>
  private string _tempFileFullName = string.Empty;
  /// <summary>
  /// Имя временного файла (флага об правильной копировании бэкапа кэша на диск) на локальном диске
  /// </summary>
  private string _temp2FileFullName = string.Empty;

  public CacheFile()
  {
    string appSetting = ConfigurationManager.AppSettings["LogPath"];
    string path1 = string.IsNullOrEmpty(appSetting) ? Path.GetDirectoryName(this.GetType().Module.FullyQualifiedName) : Environment.ExpandEnvironmentVariables(appSetting);
    this._fileFullName = Path.Combine(path1, "clientcache.dat");
    this._fileBakFullName = Path.Combine(path1, "clientcache.bak");
    this._tempFileFullName = Path.Combine(path1, "_rec.tmp");
    this._temp2FileFullName = Path.Combine(path1, "_rec2.tmp");
  }

  /// <summary>Сохранение DataSet в файл.</summary>
  public bool SaveData(DataSet dataSet)
  {
    try
    {
      File.Create(this._tempFileFullName).Close();
      File.Create(this._temp2FileFullName).Close();
      if (File.Exists(this._fileFullName))
        File.Copy(this._fileFullName, this._fileBakFullName, true);
      File.Delete(this._temp2FileFullName);
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      dataSet.RemotingFormat = SerializationFormat.Binary;
      using (FileStream serializationStream = new FileStream(this._fileFullName, FileMode.Create, FileAccess.Write))
      {
        binaryFormatter.Serialize((Stream) serializationStream, (object) dataSet);
        serializationStream.Flush();
        serializationStream.Close();
      }
      File.Delete(this._tempFileFullName);
      return true;
    }
    catch
    {
      return false;
    }
  }

  private DataSet EmptyDataSet()
  {
    return new DataSet()
    {
      RemotingFormat = SerializationFormat.Binary
    };
  }

  /// <summary>Извлечение данных из файла в DataSet</summary>
  public DataSet LoadData()
  {
    try
    {
      DataSet dataSet = new DataSet();
      dataSet.RemotingFormat = SerializationFormat.Binary;
      if (File.Exists(this._tempFileFullName))
      {
        File.Delete(this._tempFileFullName);
        if (!File.Exists(this._temp2FileFullName))
        {
          if (!File.Exists(this._fileBakFullName))
            return this.EmptyDataSet();
          File.Copy(this._fileBakFullName, this._fileFullName, true);
        }
        else
        {
          File.Delete(this._temp2FileFullName);
          if (!File.Exists(this._fileFullName))
            return this.EmptyDataSet();
        }
      }
      if (File.Exists(this._fileFullName))
      {
        if (new FileInfo(this._fileFullName).Length > 0L)
        {
          using (FileStream serializationStream = File.OpenRead(this._fileFullName))
          {
            if (serializationStream.Length > 0L)
              dataSet = (DataSet) new BinaryFormatter().Deserialize((Stream) serializationStream);
            serializationStream.Close();
          }
          if (dataSet != null)
            dataSet.RemotingFormat = SerializationFormat.Binary;
          return dataSet;
        }
      }
    }
    catch (Exception ex)
    {
    }
    return this.EmptyDataSet();
  }
}
