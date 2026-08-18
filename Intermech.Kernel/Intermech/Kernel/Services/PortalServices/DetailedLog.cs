// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.DetailedLog
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;
using System.Configuration;
using System.IO;


namespace Intermech.Kernel.Services.PortalServices;

public sealed class DetailedLog : IDisposable
{
  private StreamWriter _writer;

  public DetailedLog(string name, string taskName) => this.CreateFile(name, taskName);

  public void Close()
  {
    if (this._writer == null)
      return;
    this._writer.Flush();
    this._writer.Close();
  }

  public void Dispose() => this.Close();

  private void CreateFile(string name, string taskName)
  {
    this._writer = new StreamWriter(Path.Combine(ConfigurationManager.AppSettings.Get("LogPath") ?? string.Empty, $"{name}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.log"));
  }

  public void Write(string message)
  {
    if (this._writer == null)
      return;
    this._writer.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fffffff")} > {message}");
  }
}
