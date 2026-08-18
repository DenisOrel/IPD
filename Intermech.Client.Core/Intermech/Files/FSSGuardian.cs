
// Type: Intermech.Files.FSSGuardian
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.FSS;
using Intermech.IO;
using Intermech.Localization;
using System;
using System.Configuration;
using System.IO;
using System.Threading;


namespace Intermech.Files;

/// <summary>
/// Реализует взаимодействие с сервисом защиты файлового хранилища.
/// </summary>
internal sealed class FSSGuardian : IFileVaultGuardian, IDisposable
{
  private string homePath;
  private string userFolder;
  private IPSClient fssClient;
  private IIPS_FSS_Server fss;
  private Timer fssTimer;
  private bool disposed;
  private const int HeartBeatPeriod = 10000;

  /// <summary>Включает защиту файлового хранилища.</summary>
  /// <param name="homePath">Путь к корню файлового хранилища</param>
  /// <param name="userFolder">Имя папки пользователя внутри файлового хранилища</param>
  /// <exception cref="T:System.Exception">В процессе включения защиты произошла ошибка</exception>
  public void Initialize(string homePath, string userFolder)
  {
    this.CheckNotDisposed();
    try
    {
      this.homePath = FSSGuardian.GetRealHomePath(homePath);
      this.userFolder = userFolder;
      this.fssClient = new IPSClient(Guid.NewGuid());
      this.fss = (IIPS_FSS_Server) Activator.GetObject(typeof (IIPS_FSS_Server), $"tcp://{FSSGuardian.GetFssHost()}:{FSSGuardian.GetFssPort()}/IPS_FSS_Server");
      this.fss.Login((IIPSClient) this.fssClient);
      this.fss.CreateFileStorage((IIPSClient) this.fssClient, this.homePath, true);
      this.fss.ConnectFileStorage((IIPSClient) this.fssClient, this.homePath);
      if (!Directory.Exists(Path.Combine(this.homePath, this.userFolder)))
        this.fss.CreateSubfolder((IIPSClient) this.fssClient, this.userFolder);
      this.fss.ConnectSubfolder((IIPSClient) this.fssClient, this.userFolder);
      this.fssTimer = new Timer(new TimerCallback(this.FssHeartBeat), (object) null, 10000, 10000);
    }
    catch (Exception ex)
    {
      if (this.fssTimer != null)
        this.fssTimer.Dispose();
      throw new Exception(LocalizationHolder.rm.GetString("Client.Core_1293"), ex);
    }
  }

  private static string GetRealHomePath(string homePath)
  {
    string pathRoot = Path.GetPathRoot(homePath);
    if (string.IsNullOrEmpty(pathRoot) || pathRoot.StartsWith("\\\\"))
      return homePath;
    string mappedPath = DriveUtils.GetMappedPath(pathRoot[0]);
    return string.IsNullOrEmpty(mappedPath) || mappedPath.StartsWith("\\\\") ? homePath : Path.Combine(mappedPath, homePath.Substring(pathRoot.Length, homePath.Length - pathRoot.Length));
  }

  private static string GetFssHost() => "localhost";

  private static string GetFssPort()
  {
    string fssPort = ConfigurationManager.AppSettings.Get("FssPort");
    if (string.IsNullOrEmpty(fssPort))
      fssPort = "7887";
    return fssPort;
  }

  public void Dispose()
  {
    if (this.disposed)
      return;
    this.disposed = true;
    using (ManualResetEvent notifyObject = new ManualResetEvent(false))
    {
      this.fssTimer.Dispose((WaitHandle) notifyObject);
      notifyObject.WaitOne(10000, true);
    }
    try
    {
      this.fss.DisconnectSubfolder((IIPSClient) this.fssClient, this.userFolder);
      this.fss.DisconnectFileStorage((IIPSClient) this.fssClient);
    }
    catch
    {
    }
  }

  private void CheckNotDisposed()
  {
    if (this.disposed)
      throw new ObjectDisposedException(nameof (FSSGuardian));
  }

  private void FssHeartBeat(object state)
  {
    try
    {
      this.fss.Login((IIPSClient) this.fssClient);
      if (!string.IsNullOrEmpty(this.fss.CurrentFileStorage((IIPSClient) this.fssClient)))
        return;
      this.fss.ConnectFileStorage((IIPSClient) this.fssClient, this.homePath);
      this.fss.ConnectSubfolder((IIPSClient) this.fssClient, this.userFolder);
    }
    catch
    {
    }
  }
}
