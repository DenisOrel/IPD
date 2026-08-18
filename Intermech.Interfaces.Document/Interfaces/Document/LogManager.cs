// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.LogManager
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Configuration;
using System.IO;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Класс для сохранения данных в лог</summary>
public class LogManager
{
  /// <summary>Поле только для внутреннего использования</summary>
  public static bool CreateLog = false;
  public static string FileName = "ImDocBase.log";
  private static bool _logStarted = false;
  private static string folder = (string) null;
  private static StreamWriter sw = (StreamWriter) null;

  /// <summary>Добавить строку в лог</summary>
  /// <param name="text">Текст</param>
  /// <param name="force">Сохранять не зависимо от настройки сохранения в лог, для исключительных случаев</param>
  public static void AddLine(Exception exeption, bool force = false)
  {
    if (!LogManager.CreateLog && !force)
      return;
    LogManager.AddLine(exeption.Message + Environment.NewLine + exeption.StackTrace, force);
  }

  public static string Folder
  {
    get
    {
      if (LogManager.folder == null)
      {
        LogManager.folder = ConfigurationManager.AppSettings.Get("LogPath");
        if (string.IsNullOrEmpty(LogManager.folder))
          LogManager.folder = "%temp%\\IMClient.Debug";
        LogManager.folder = Environment.ExpandEnvironmentVariables(LogManager.folder);
      }
      return LogManager.folder;
    }
  }

  /// <summary>Добавить строку в лог</summary>
  /// <param name="text">Текст</param>
  /// <param name="force">Сохранять не зависимо от настройки сохранения в лог, для исключительных случаев</param>
  public static void AddLine(string text, bool force = false)
  {
    if (!LogManager.CreateLog && !force)
      return;
    try
    {
      if (LogManager.sw == null)
      {
        string path = $"{LogManager.Folder}\\{LogManager.FileName}";
        if (File.Exists(path))
        {
          if (DateTime.Now - File.GetCreationTime(path) > new TimeSpan(4, 0, 0, 0))
          {
            File.Delete(path);
            File.Create(path).Close();
            File.SetCreationTime(path, DateTime.Now);
          }
        }
        else if (!Directory.Exists(LogManager.Folder))
          Directory.CreateDirectory(LogManager.Folder);
        LogManager.sw = new StreamWriter(path, true);
        if (!LogManager._logStarted)
          LogManager.sw.WriteLine("Start log " + DateTime.Now.ToString());
        LogManager._logStarted = true;
      }
      LogManager.sw.WriteLine(text);
      LogManager.sw.Flush();
    }
    catch
    {
    }
  }

  /// <summary>
  /// Принудительно закрыть файл лога, чтобы можно было прочитать лог не закрывая приложение
  /// </summary>
  public static void CloseFile()
  {
    if (LogManager.sw == null)
      return;
    LogManager.sw.Flush();
  }

  public static void LogStreamToFile(Stream stream, string fileName)
  {
    FileStream destination = new FileStream(fileName, FileMode.Create, FileAccess.Write);
    long position = stream.Position;
    if (stream.CanSeek)
      stream.Position = 0L;
    stream.CopyTo((Stream) destination);
    if (stream.CanSeek)
      stream.Position = position;
    destination.Close();
  }
}
