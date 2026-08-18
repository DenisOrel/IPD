// Decompiled with JetBrains decompiler
// Type: ConsoleServer.DbManagerLogger
// Assembly: ConsoleServer, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A2572001-4A8A-44C7-AECE-87B2080D6C9F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\ConsoleServer.exe

using Intermech.Interfaces.Server;
using System;
using System.IO;
using System.Threading;

#nullable disable
namespace ConsoleServer;

internal sealed class DbManagerLogger : IDbManagerLogger
{
  private string _fileName = string.Empty;

  public string FileName
  {
    get => this._fileName;
    set => Interlocked.Exchange<string>(ref this._fileName, value != null ? value : string.Empty);
  }

  public void AddToLog(string[] data)
  {
    if (data == null || data.Length == 0)
      return;
    StreamWriter streamWriter = (StreamWriter) null;
    try
    {
      if (this._fileName.Length > 0)
        streamWriter = new StreamWriter(this._fileName, true);
      foreach (string str in data)
      {
        if (streamWriter != null)
          streamWriter.WriteLine(str);
        else
          Console.WriteLine(str);
      }
      if (streamWriter == null)
        return;
      streamWriter.Flush();
      streamWriter.Close();
    }
    catch (Exception ex)
    {
      this._fileName = string.Empty;
      Console.WriteLine(ex.Message);
    }
  }
}
