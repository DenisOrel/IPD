// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.TraceLoggerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Intermech.Diagnostics;
using Intermech.Interfaces.Kernel;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;


namespace Intermech.Kernel.Services;

public sealed class TraceLoggerService : ITraceLoggerService
{
  private ConcurrentQueue<TraceFileRecord> TraceFilesQueue = new ConcurrentQueue<TraceFileRecord>();
  private Dictionary<string, int> RecordedTraceSize = new Dictionary<string, int>();
  private int HalfLogFileSize = -1;
  private ISystemDiagnosticsSettings _DiagnosticsService;
  private volatile bool _TruncateLogFiles;

  public TraceLoggerService()
  {
    new Thread(new ThreadStart(this.ProcessTraceLog))
    {
      Name = "DelayedUpdaterService add2trace thread",
      IsBackground = true
    }.Start();
  }

  public void AddToTrace(string eventStr, string traceFileName)
  {
    this.TraceFilesQueue.Enqueue(new TraceFileRecord(eventStr, traceFileName));
  }

  public void CheckTruncateLogFiles() => this._TruncateLogFiles = true;

  private ISystemDiagnosticsSettings DiagnosticsService
  {
    get
    {
      if (this._DiagnosticsService == null)
        this._DiagnosticsService = ServerServices.GetService(typeof (ISystemDiagnosticsSettings)) as ISystemDiagnosticsSettings;
      return this._DiagnosticsService;
    }
  }

  private void ProcessTraceLog()
  {
    while (true)
    {
      string key = string.Empty;
      try
      {
        StreamWriter streamWriter = (StreamWriter) null;
        string fileName = string.Empty;
        try
        {
          TraceFileRecord result;
          while (this.TraceFilesQueue.TryDequeue(out result))
          {
            if (result.TraceFileName != string.Empty)
            {
              if (this.HalfLogFileSize == -1)
              {
                ISystemDiagnosticsSettings diagnosticsService = this.DiagnosticsService;
                if (diagnosticsService != null)
                  this.HalfLogFileSize = diagnosticsService.MaxLogFileSizeInBytes <= 0 ? 0 : diagnosticsService.MaxLogFileSizeInBytes / 2;
              }
              if (key != result.TraceFileName)
              {
                streamWriter?.Dispose();
                streamWriter = new StreamWriter(result.TraceFileName, true);
              }
              key = result.TraceFileName;
              int num;
              if (!this.RecordedTraceSize.TryGetValue(key, out num))
                num = 0;
              if (result.EventStr == string.Empty)
              {
                streamWriter.WriteLine(result.EventStr);
              }
              else
              {
                string str = $"{DateTime.Now.ToString()}> {result.EventStr}";
                num += str.Length * 2;
                streamWriter.WriteLine(str);
                if (this.HalfLogFileSize > 0)
                {
                  this.RecordedTraceSize[key] = num;
                  if (num > this.HalfLogFileSize)
                    fileName = key;
                }
              }
            }
          }
        }
        finally
        {
          streamWriter?.Dispose();
        }
        if (this._TruncateLogFiles)
        {
          try
          {
            this.TruncateLogFiles();
          }
          finally
          {
            this._TruncateLogFiles = false;
          }
        }
        else if (fileName != string.Empty)
        {
          FileInfo fileInfo = new FileInfo(fileName);
          if (fileInfo.Length >= (long) this.DiagnosticsService.MaxLogFileSizeInBytes)
            this.TruncateFile(fileName, fileInfo.Length);
        }
        else
          Thread.Sleep(25);
      }
      catch (Exception ex)
      {
        if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
          Console.WriteLine("AddToTrace error for file '{0}': {1}", (object) key, (object) ex.Message);
        else
          EventLogWriters.CreateSystemLogWriter(SystemEventLogType.Application, ServerDiagnosticsConsts.EventLogSourceName).Write($"AddToTrace error for file '{key}': {ex.Message}", EventLogItemType.Error);
        Thread.Sleep(25);
      }
    }
  }

  private string GetZipFileName(string fileName, int index)
  {
    string str = index.ToString();
    while (str.Length < 3)
      str = "0" + str;
    return $"{Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName))}.{str}.zip";
  }

  private void TruncateFile(string fileName, long fileLength)
  {
    if (this.DiagnosticsService.MaxLogFileCopies > 0)
    {
      int maxLogFileCopies = this.DiagnosticsService.MaxLogFileCopies;
      string zipFileName1 = this.GetZipFileName(fileName, maxLogFileCopies);
      if (File.Exists(zipFileName1))
        File.Delete(zipFileName1);
      while (--maxLogFileCopies > 0)
      {
        string zipFileName2 = this.GetZipFileName(fileName, maxLogFileCopies);
        if (File.Exists(zipFileName2))
          File.Move(zipFileName2, this.GetZipFileName(fileName, maxLogFileCopies + 1));
      }
    }
    string[] strArray;
    try
    {
      strArray = File.ReadAllLines(fileName);
    }
    catch (OutOfMemoryException ex)
    {
      using (FileStream baseOutputStream = new FileStream(this.GetZipFileName(fileName, 1), FileMode.OpenOrCreate, FileAccess.Write))
      {
        using (ZipOutputStream destination = new ZipOutputStream((Stream) baseOutputStream))
        {
          destination.SetLevel(9);
          ZipEntry entry = new ZipEntry(ZipEntry.CleanName(Path.GetFileName(fileName)));
          destination.PutNextEntry(entry);
          using (FileStream source = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read))
          {
            byte[] buffer = new byte[8192 /*0x2000*/];
            StreamUtils.Copy((Stream) source, (Stream) destination, buffer);
          }
        }
      }
      File.Delete(fileName);
      return;
    }
    int index = 0;
    long num1 = fileLength;
    long num2 = (long) (this.DiagnosticsService.MaxLogFileSizeInBytes / 2);
    if (this.DiagnosticsService.MaxLogFileCopies > 0)
    {
      string path = $"{Path.GetFileNameWithoutExtension(fileName)}.001{Path.GetExtension(fileName)}";
      using (StreamWriter streamWriter = new StreamWriter(path, false))
      {
        while (num1 > num2)
        {
          if (index < strArray.Length)
          {
            num1 = num1 - (long) streamWriter.Encoding.GetBytes(strArray[index]).Length - (long) Environment.NewLine.Length;
            streamWriter.WriteLine(strArray[index++]);
          }
          else
            break;
        }
      }
      using (FileStream baseOutputStream = new FileStream(this.GetZipFileName(fileName, 1), FileMode.OpenOrCreate, FileAccess.Write))
      {
        using (ZipOutputStream destination = new ZipOutputStream((Stream) baseOutputStream))
        {
          destination.SetLevel(9);
          ZipEntry entry = new ZipEntry(ZipEntry.CleanName(Path.GetFileName(path)));
          destination.PutNextEntry(entry);
          using (FileStream source = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
          {
            byte[] buffer = new byte[8192 /*0x2000*/];
            StreamUtils.Copy((Stream) source, (Stream) destination, buffer);
          }
        }
      }
      File.Delete(path);
    }
    else
    {
      while (num1 > num2 && index < strArray.Length)
        num1 = num1 - (long) Encoding.UTF8.GetBytes(strArray[index++]).Length - (long) Environment.NewLine.Length;
    }
    string str = fileName + ".bkp";
    using (StreamWriter streamWriter = new StreamWriter(str, false))
    {
      while (index < strArray.Length)
        streamWriter.WriteLine(strArray[index++]);
    }
    File.Delete(fileName);
    File.Move(str, fileName);
    this.RecordedTraceSize[fileName] = Convert.ToInt32(num2);
  }

  private void TruncateLogFiles()
  {
    if (this.DiagnosticsService == null || !(this.DiagnosticsService.ServerLogPath != string.Empty) || this.DiagnosticsService.MaxLogFileSize <= 0)
      return;
    foreach (FileInfo file in new DirectoryInfo(this.DiagnosticsService.ServerLogPath).GetFiles("*.log"))
    {
      if (file.Length >= (long) this.DiagnosticsService.MaxLogFileSizeInBytes)
        this.TruncateFile(file.FullName, file.Length);
      else
        this.RecordedTraceSize[file.FullName] = Convert.ToInt32(file.Length);
    }
  }
}
