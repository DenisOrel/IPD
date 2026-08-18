// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.MailSettings
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Workflow.Client;

[Serializable]
public class MailSettings
{
  private int _refreshInterval = 5;
  private int _markReadInterval = 3;
  private bool _warnOnDeletion = true;
  private ProcessPriority _notifyPriority = ProcessPriority.Low;
  private bool _clearTrashOnExit;
  private bool _showTabs = true;
  private string _soundFileName = "";
  private static MailSettings _cfg;
  private int _mailTabsHeight = 200;
  private bool _confirmSendBack;
  private bool _confirmSendNext;
  private bool _disableAllNotify;

  public int RefreshInterval
  {
    get => this._refreshInterval;
    set => this._refreshInterval = value;
  }

  public int MarkReadInterval
  {
    get => this._markReadInterval;
    set => this._markReadInterval = value;
  }

  public bool WarnOnDeletion
  {
    get => this._warnOnDeletion;
    set => this._warnOnDeletion = value;
  }

  public ProcessPriority NotifyPriority
  {
    get => this._notifyPriority;
    set => this._notifyPriority = value;
  }

  public bool ClearTrashOnExit
  {
    get => this._clearTrashOnExit;
    set => this._clearTrashOnExit = value;
  }

  public bool ShowTabs
  {
    get => this._showTabs;
    set => this._showTabs = value;
  }

  public string SoundFileName
  {
    get => this._soundFileName;
    set => this._soundFileName = value.Trim();
  }

  public static MailSettings Cfg => MailSettings._cfg;

  public int MailTabsHeight
  {
    get => this._mailTabsHeight;
    set => this._mailTabsHeight = value;
  }

  public bool ConfirmSendBack
  {
    get => this._confirmSendBack;
    set => this._confirmSendBack = value;
  }

  public bool ConfirmSendNext
  {
    get => this._confirmSendNext;
    set => this._confirmSendNext = value;
  }

  public bool DisableAllNotify
  {
    get => this._disableAllNotify;
    set => this._disableAllNotify = value;
  }

  public void Save()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBConfigurations configurations = sessionKeeper.Session.Configurations;
      using (MemoryStream serializationStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, (object) this);
        BlobInformation config_info = new BlobInformation(serializationStream.Length, serializationStream.Length, DateTime.Now, nameof (MailSettings), ArcMethods.NotPacked, "b");
        configurations.WriteConfigData(config_info, serializationStream.ToArray());
      }
    }
  }

  public static void Init() => MailSettings._cfg = MailSettings.Load();

  protected static MailSettings Load()
  {
    MailSettings mailSettings = (MailSettings) null;
    try
    {
      IDBConfigurations service = ApplicationServices.Container.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
      byte[] config_file = new byte[0];
      service?.LoadConfigData(nameof (MailSettings), out BlobInformation _, out config_file);
      if (config_file.Length != 0)
      {
        MemoryStream memoryStream = new MemoryStream(config_file);
        memoryStream.Position = 0L;
        using (MemoryStream serializationStream = memoryStream)
          mailSettings = new BinaryFormatter().Deserialize((Stream) serializationStream) as MailSettings;
      }
    }
    catch (Exception ex)
    {
      if (ApplicationServices.Container.GetService(typeof (IOutputView)) is IOutputView service)
        service.WriteString("Ошибки", "При загрузке настроек почты произошла ошибка: " + ex.Message);
    }
    return mailSettings ?? new MailSettings();
  }
}
