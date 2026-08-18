// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.Settings.CommonSettingsHolder
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server.Settings;

[Serializable]
public class CommonSettingsHolder : LongLifeObject, ICommonSettingsHolder, ISerializable
{
  private string _InputFiles;
  private string _OutputFiles;
  private string _DoneFiles;
  private string _ErrorFiles;
  private CommonSettingsSyncronizer _paramsServerSynchronizer;

  public CommonSettingsHolder()
  {
    this._paramsServerSynchronizer = new CommonSettingsSyncronizer((ICommonSettingsHolder) this);
    ApplicationServices.Container.GetService<IServerSynchronizersManager>().RegisterSynchronizer((IServerSynchronizer) this._paramsServerSynchronizer);
  }

  protected CommonSettingsHolder(SerializationInfo info, StreamingContext context)
  {
    this._InputFiles = info.GetString(nameof (InputFiles));
    this._OutputFiles = info.GetString(nameof (OutputFiles));
    this._DoneFiles = info.GetString(nameof (DoneFiles));
    this._ErrorFiles = info.GetString(nameof (ErrorFiles));
  }

  public string OutputFiles
  {
    get => this._OutputFiles;
    set => this._OutputFiles = value;
  }

  public string InputFiles
  {
    get => this._InputFiles;
    set => this._InputFiles = value;
  }

  public string DoneFiles
  {
    get => this._DoneFiles;
    set => this._DoneFiles = value;
  }

  public string ErrorFiles
  {
    get => this._ErrorFiles;
    set => this._ErrorFiles = value;
  }

  public void ReadSettings(Guid sessionGuid)
  {
    byte[] config_file;
    UserSession.GetSessionByID(sessionGuid).Configurations.LoadConfigData(Const.ConfigName, out BlobInformation _, out config_file);
    if (config_file.Length == 0)
      return;
    MemoryStream serializationStream = new MemoryStream(config_file);
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    try
    {
      object obj = binaryFormatter.Deserialize((Stream) serializationStream);
      if (!(obj is ICommonSettingsHolder))
        return;
      this.DoneFiles = (obj as ICommonSettingsHolder).DoneFiles;
      this.ErrorFiles = (obj as ICommonSettingsHolder).ErrorFiles;
      this.InputFiles = (obj as ICommonSettingsHolder).InputFiles;
      this.OutputFiles = (obj as ICommonSettingsHolder).OutputFiles;
    }
    catch (Exception ex)
    {
    }
  }

  public void WriteSettings(Guid sessionGuid)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    IDBConfigurations configurations = sessionById.Configurations;
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    MemoryStream memoryStream = new MemoryStream();
    MemoryStream serializationStream = memoryStream;
    binaryFormatter.Serialize((Stream) serializationStream, (object) this);
    byte[] array = memoryStream.ToArray();
    BlobInformation config_info = new BlobInformation((long) array.Length, (long) array.Length, DateTime.Now, Const.ConfigName, ArcMethods.NotPacked, string.Empty);
    byte[] config_file = array;
    configurations.WriteConfigData(config_info, config_file, 0L);
    if (this._paramsServerSynchronizer == null)
      return;
    this._paramsServerSynchronizer.AddEvent("common", ((UserSession) sessionById).DataManager);
  }

  public void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    if (info == null)
      throw new ArgumentNullException(nameof (info));
    info.AddValue("InputFiles", (object) this._InputFiles);
    info.AddValue("OutputFiles", (object) this._OutputFiles);
    info.AddValue("DoneFiles", (object) this._DoneFiles);
    info.AddValue("ErrorFiles", (object) this._ErrorFiles);
  }
}
