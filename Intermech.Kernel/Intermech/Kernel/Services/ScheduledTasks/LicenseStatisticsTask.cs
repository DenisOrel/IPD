// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ScheduledTasks.LicenseStatisticsTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;


namespace Intermech.Kernel.Services.ScheduledTasks;

internal class LicenseStatisticsTask : DBCustomManualScheduledService
{
  private string[] _Applications;
  private bool _inited;
  private int _LicensePort = 8995;
  private string _LicenseMachineName;
  private WebClient _WebClient;
  private DateTime _LastSaveTime = DateTime.Now;
  private bool _MonthlyReport;
  private int _reportIndex;
  private string _FilesPath;

  private string CurrentStatFileName
  {
    get
    {
      string path2 = "LicenseReport";
      if (this._MonthlyReport)
      {
        string str = DateTime.Now.Month.ToString();
        if (str.Length == 1)
          str = "0" + str;
        path2 += str;
      }
      return $"{Path.Combine(this._FilesPath, path2)}_{DateTime.Now.Year.ToString()}";
    }
  }

  private void Init()
  {
    if (this._inited)
      return;
    string name = ConfigurationManager.AppSettings.Get("ServerName");
    if (!string.IsNullOrEmpty(name))
      this._LicenseMachineName = Environment.ExpandEnvironmentVariables(name);
    if (string.IsNullOrEmpty(this._LicenseMachineName))
      this._LicenseMachineName = EnvironmentConsts.MachineName;
    this._FilesPath = ConfigurationManager.AppSettings.Get("LicenseReportPath");
    if (this._FilesPath == null || this._FilesPath == string.Empty)
    {
      this._FilesPath = ConfigurationManager.AppSettings.Get("LogPath");
      if (this._FilesPath == null || this._FilesPath == string.Empty)
        this._FilesPath = Path.GetTempPath();
    }
    string str = ConfigurationManager.AppSettings.Get("MonthlyReport");
    if (str != null && str != string.Empty)
      this._MonthlyReport = str == "1" || str.ToLower() == "true";
    this._WebClient = new WebClient();
    this.LoadApplicationsList();
    this._inited = true;
  }

  private void LoadApplicationsList()
  {
    string str1 = this.CurrentStatFileName + "_Busy";
    int num = 0;
    while (System.IO.File.Exists($"{str1}{num.ToString()}.csv"))
      ++num;
    if (num <= 0)
      return;
    this._reportIndex = num - 1;
    string str2;
    using (StreamReader streamReader = new StreamReader(this.GetStatFileName("Busy")))
      str2 = streamReader.ReadLine();
    string[] strArray = str2.Split(';');
    this._Applications = new string[strArray.Length - 1];
    for (int index = 0; index < this._Applications.Length; ++index)
      this._Applications[index] = strArray[index + 1];
  }

  private string GetStatFileName(string suffix)
  {
    return $"{this.CurrentStatFileName}_{suffix}{this._reportIndex}{".csv"}";
  }

  private void SaveStatFile(List<int> lics, string fileName, bool appendHeader)
  {
    if (!appendHeader)
      appendHeader = !System.IO.File.Exists(fileName);
    StringBuilder stringBuilder = new StringBuilder();
    if (appendHeader)
    {
      stringBuilder.Append("Дата и время");
      for (int index = 0; index < this._Applications.Length; ++index)
        stringBuilder.Append(";" + this._Applications[index]);
      stringBuilder.AppendLine();
    }
    stringBuilder.Append($"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
    for (int index = 0; index < lics.Count; ++index)
      stringBuilder.Append(";" + lics[index].ToString());
    using (StreamWriter streamWriter = new StreamWriter(fileName, true))
      streamWriter.WriteLine(stringBuilder.ToString());
  }

  private void RequestToTable(string requestStr)
  {
    int capacity = this._Applications == null ? 100 : this._Applications.Length;
    List<string> newColumns = new List<string>(capacity);
    List<int> lics1 = new List<int>(capacity);
    List<int> lics2 = new List<int>(capacity);
    int startIndex1 = 0;
    int num1 = requestStr.IndexOf("Список распределенных лицензий");
    while (true)
    {
      int num2 = requestStr.IndexOf("<tr><td>", startIndex1);
      if (num2 <= num1 && num2 >= 0)
      {
        int startIndex2 = num2 + 8;
        int startIndex3 = requestStr.IndexOf("</td>", startIndex2);
        string str1 = requestStr.Substring(startIndex2, startIndex3 - startIndex2);
        newColumns.Add(str1);
        int startIndex4 = requestStr.IndexOf("</td><td>", startIndex3) + 9;
        int startIndex5 = requestStr.IndexOf("</td><td>", startIndex4) + 9;
        int startIndex6 = requestStr.IndexOf("</td>", startIndex5);
        string str2 = requestStr.Substring(startIndex5, startIndex6 - startIndex5);
        lics1.Add(Convert.ToInt32(str2));
        startIndex1 = requestStr.IndexOf("</td><td>", startIndex6) + 9;
        int num3 = requestStr.IndexOf("</td>", startIndex1);
        string str3 = requestStr.Substring(startIndex1, num3 - startIndex1);
        lics2.Add(Convert.ToInt32(str3));
      }
      else
        break;
    }
    bool appendHeader = false;
    if (this._Applications == null)
    {
      appendHeader = true;
      this._Applications = newColumns.ToArray();
    }
    else if (!this.IsSameLicenses(newColumns))
    {
      ++this._reportIndex;
      appendHeader = true;
      this._Applications = newColumns.ToArray();
    }
    this.SaveStatFile(lics1, this.GetStatFileName("Busy"), appendHeader);
    this.SaveStatFile(lics2, this.GetStatFileName("Free"), appendHeader);
  }

  private bool IsSameLicenses(List<string> newColumns)
  {
    if (this._Applications == null || newColumns.Count != this._Applications.Length)
      return false;
    for (int index = 0; index < newColumns.Count; ++index)
    {
      if (newColumns[index] != this._Applications[index])
        return false;
    }
    return true;
  }

  public override Guid GUID => new Guid("7aa57f28-b1e1-40d6-9c14-58d1d023f32d");

  public override string ServiceName => "Сбор статистики использования лицензий";

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    try
    {
      this.Init();
      string requestStr = this._WebClient.DownloadString($"http://{this._LicenseMachineName}:{this._LicensePort}/");
      if (requestStr != string.Empty)
        this.RequestToTable(requestStr);
    }
    catch (Exception ex)
    {
      this.Session.EventLogHelper.AddToTrace($"Фоновая задача сбора статистики использования лицензий прервана с ошибкой: {ex.Message}{Environment.NewLine}{ex.StackTrace}", Consts.traceAlways, string.Empty);
    }
    return true;
  }
}
