// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientFileLog
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Потокобезопасный файловый протокол для клиентского приложения
/// </summary>
public sealed class ClientFileLog : IClientFileLog
{
  /// <summary>Файл общего протокола</summary>
  private StreamWriter logFileStream;
  /// <summary>Путь к файлу протокола</summary>
  private string logFilePath = string.Empty;
  /// <summary>Имя файла с протоколом</summary>
  private string logFileName = string.Empty;

  /// <summary>Создать пустой экземпляр класса</summary>
  public ClientFileLog()
  {
  }

  /// <summary>Создать/открыть файловый протокол</summary>
  /// <param name="logFilePath">Путь к файлу протокола</param>
  /// <param name="logFileName">Имя файла с протоколом</param>
  public ClientFileLog(string logFilePath, string logFileName)
  {
    this.OpenLog(logFilePath, logFileName);
  }

  /// <summary>
  /// Добавить при необходимости дату и время в текст сообщения
  /// </summary>
  /// <param name="writeDateTime">true - добавлять дату и время в текст сообщения</param>
  /// <param name="msg">Сообщение</param>
  /// <returns>Обработанное сообщение</returns>
  private string AddDateTime(bool writeDateTime, string msg)
  {
    if (!writeDateTime)
      return msg;
    DateTime utcNow = DateTime.UtcNow;
    string shortDateString = utcNow.ToShortDateString();
    utcNow = DateTime.UtcNow;
    string shortTimeString = utcNow.ToShortTimeString();
    string str = msg;
    return $"{shortDateString} {shortTimeString} {str}";
  }

  /// <summary>Задать значения по умолчанию</summary>
  private void SetDefaults()
  {
    this.logFilePath = Environment.ExpandEnvironmentVariables("%temp%\\IMClient");
    this.logFileName = "imclient.log";
    string name = ConfigurationManager.AppSettings["LogPath"];
    if (name != null)
      name = Environment.ExpandEnvironmentVariables(name);
    if (!string.IsNullOrEmpty(name))
      this.logFilePath = name;
    try
    {
      if (Directory.Exists(this.logFilePath))
        return;
      Directory.CreateDirectory(this.logFilePath);
    }
    catch
    {
    }
  }

  /// <summary>Является ли протокол открытым</summary>
  public bool IsLogOpen
  {
    [MethodImpl(MethodImplOptions.Synchronized)] get => this.logFileStream != null;
  }

  /// <summary>Закрыть файл с протоколом</summary>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public void CloseLog()
  {
    if (!this.IsLogOpen)
      return;
    try
    {
      this.logFileStream.Flush();
      this.logFileStream.Close();
      this.logFileStream = (StreamWriter) null;
    }
    catch
    {
    }
  }

  /// <summary>Открыть файл с протоколом</summary>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public void OpenLog()
  {
    if (this.IsLogOpen)
      return;
    try
    {
      this.SetDefaults();
      string path = Path.Combine(this.logFilePath, this.logFileName);
      try
      {
        File.SetAttributes(path, FileAttributes.Normal);
      }
      catch (FileNotFoundException ex)
      {
      }
      this.logFileStream = new StreamWriter(path, true, Encoding.UTF8);
    }
    catch
    {
      this.logFileStream = (StreamWriter) null;
    }
  }

  /// <summary>Открыть файл с протоколом</summary>
  /// <param name="logFilePath">Путь к файлу протокола</param>
  /// <param name="logFileName">Имя файла с протоколом</param>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public void OpenLog(string logFilePath, string logFileName)
  {
    if (this.IsLogOpen)
      return;
    this.SetDefaults();
    if (!string.IsNullOrEmpty(logFilePath))
      this.logFilePath = logFilePath;
    if (!string.IsNullOrEmpty(logFileName))
      this.logFileName = logFileName;
    try
    {
      this.logFileStream = new StreamWriter(Path.Combine(logFilePath, logFileName), true, Encoding.UTF8);
    }
    catch
    {
      this.logFileStream = (StreamWriter) null;
    }
  }

  /// <summary>Записать в протокол указанный текст</summary>
  /// <param name="text">Текст</param>
  /// <param name="withDateTime">true - в начало строки добавить текущие дату и время (UTC)</param>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public void Write(string text, bool withDateTime)
  {
    try
    {
      if (!this.IsLogOpen)
        return;
      this.logFileStream.Write(this.AddDateTime(withDateTime, text));
    }
    catch
    {
    }
  }

  /// <summary>Записать в протокол указанный текст</summary>
  /// <param name="text">Текст</param>
  /// <param name="withDateTime">true - в начало строки добавить текущие дату и время (UTC)</param>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public void WriteLn(string text, bool withDateTime)
  {
    try
    {
      if (!this.IsLogOpen)
        return;
      this.logFileStream.WriteLine(this.AddDateTime(withDateTime, text));
    }
    catch
    {
    }
  }
}
