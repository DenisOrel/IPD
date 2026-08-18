// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.Client.Kernel.Tasks.XmlExchangeExportTask
// Assembly: Intermech.XmlExchange.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 60313882-D426-47E0-8CD2-E15037D75FF2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.XmlExchange.Client.xml

using Intermech.Client.Core;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.XmlExchange;
using Intermech.Localization;
using System;
using System.IO;
using System.Threading;

#nullable disable
namespace Intermech.XmlExchange.Client.Kernel.Tasks;

/// <summary>Класс фоновой задачи для экспорта данных</summary>
internal sealed class XmlExchangeExportTask : XmlExchangeTask
{
  /// <summary>
  /// 
  /// </summary>
  private readonly System.Windows.Forms.Timer _progressTimer;
  /// <summary>Данные для экспорта</summary>
  private readonly ExportAttribute[] _exportData;
  /// <summary>Параметры экспорта</summary>
  private readonly object[] _exportParams;
  /// <summary>Директория для экспорта данных на клиенте</summary>
  private readonly string _exportDir;

  /// <summary>Конструктор</summary>
  /// <param name="exportData"></param>
  /// <param name="exportDir"></param>
  /// <param name="exportParams"></param>
  public XmlExchangeExportTask(
    [NotNull] ExportAttribute[] exportData,
    string exportDir,
    object[] exportParams)
  {
    this._exportData = exportData;
    this._exportDir = exportDir;
    this._exportParams = exportParams;
    using (FixEditingContext fixEditingContext = new FixEditingContext())
    {
      this._thread = new Thread(fixEditingContext.SendEditingContextToThread(new ThreadStart(((CustomThreadBackgroundTask) this).ThreadProc)));
      this.Start();
    }
    this._progressTimer = new System.Windows.Forms.Timer() { Interval = 12000 };
    this._progressTimer.Tick += new EventHandler(this.ProgressTimer_Tick);
    this._progressTimer.Start();
  }

  /// <summary>Инициализация параметров класса</summary>
  protected override void InitializeData()
  {
    base.InitializeData();
    this._canStop = false;
    this._canPause = false;
    this._canResume = false;
    this._value = 0;
    this._minValue = 0;
    this._maxValue = 100;
    this._name = this._category = LocalizationHolder.rm.GetString("XmlExchange.Client_8");
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void CustomThreadProc()
  {
    IOutputView service1 = ServiceUtils.GetService<IOutputView>((object) ApplicationServices.Container, false);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IXmlExchangeService service2 = ServiceUtils.GetService<IXmlExchangeService>((object) sessionKeeper.Session, true);
      service1?.WriteString(LocalizationHolder.rm.GetString("XmlExchange.Client_8"), LocalizationHolder.rm.GetString("XmlExchange.Client_24"));
      IXmlExchangeExportTask exportTask = service2.CreateExportTask(sessionKeeper.Session.SessionGUID);
      try
      {
        object exportParam = this._exportParams[0];
        service1?.WriteString(LocalizationHolder.rm.GetString("XmlExchange.Client_8"), LocalizationHolder.rm.GetString("XmlExchange.Client_4"));
        string errorMsg;
        if (!exportTask.ExportData(this._exportData, this._exportParams, out errorMsg))
        {
          service1?.WriteString(LocalizationHolder.rm.GetString("XmlExchange.Client_8"), string.Format(LocalizationHolder.rm.GetString("XmlExchange.Client_29"), (object) errorMsg));
        }
        else
        {
          service1?.WriteString(LocalizationHolder.rm.GetString("XmlExchange.Client_8"), LocalizationHolder.rm.GetString("XmlExchange.Client_30"));
          string[] exportDataFiles;
          if (!exportTask.GetExportFiles(out exportDataFiles))
            return;
          foreach (string str1 in exportDataFiles)
          {
            service1?.WriteString(LocalizationHolder.rm.GetString("XmlExchange.Client_8"), string.Format(LocalizationHolder.rm.GetString("XmlExchange.Client_31"), (object) Path.GetFileName(str1)));
            string str2 = string.Empty;
            string[] strArray = str1.Split(Path.DirectorySeparatorChar);
            bool flag = false;
            char directorySeparatorChar;
            for (int index = strArray.Length - 1; index >= 0; --index)
            {
              if (GuidHelper.IsGuid(strArray[index]))
              {
                flag = true;
                break;
              }
              string str3;
              if (!(str2 != string.Empty))
              {
                str3 = strArray[index];
              }
              else
              {
                string str4 = strArray[index];
                directorySeparatorChar = Path.DirectorySeparatorChar;
                string str5 = directorySeparatorChar.ToString();
                string str6 = str2;
                str3 = str4 + str5 + str6;
              }
              str2 = str3;
            }
            string exportDir = this._exportDir;
            directorySeparatorChar = Path.DirectorySeparatorChar;
            string str7 = directorySeparatorChar.ToString();
            string str8 = flag ? str2 : Path.GetFileName(str1);
            string str9 = exportDir + str7 + str8;
            int num = 5;
            for (int index = 0; index < num; ++index)
            {
              try
              {
                if (File.Exists(str9))
                  File.Delete(str9);
                string directoryName = Path.GetDirectoryName(str9);
                if (!Directory.Exists(directoryName))
                  Directory.CreateDirectory(directoryName);
                if (File.Exists(str1))
                {
                  File.Move(str1, str9);
                  break;
                }
                IBlobReader exportData = exportTask.GetExportData(str1);
                if (exportData != null)
                {
                  try
                  {
                    exportData.OpenBlob(4194304 /*0x400000*/);
                    using (FileStream fileStream = new FileStream(str9, FileMode.Create))
                    {
                      try
                      {
                        while (true)
                        {
                          byte[] buffer = exportData.ReadDataBlock();
                          if (buffer.Length != 0)
                            fileStream.Write(buffer, 0, buffer.Length);
                          else
                            goto label_47;
                        }
                      }
                      finally
                      {
                        fileStream.Flush();
                        fileStream.Close();
                      }
                    }
                  }
                  finally
                  {
                    exportData.CloseBlob();
                  }
                }
              }
              catch
              {
                if (index == num - 1)
                  throw;
                Thread.Sleep(200);
              }
            }
            continue;
label_47:;
          }
          for (; (int) this.Value < 99; this.Value = (object) ((int) this.Value + 1))
          {
            Thread.Sleep(50);
            this._event.WaitOne();
          }
          service1?.WriteString(LocalizationHolder.rm.GetString("XmlExchange.Client_8"), LocalizationHolder.rm.GetString("XmlExchange.Client_33"));
        }
      }
      finally
      {
        service2.DisposeExportTask(exportTask.TaskGuid);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ProgressTimer_Tick(object sender, EventArgs e)
  {
    int num = (int) this.Value + 1;
    if (num > 90)
      this._progressTimer.Stop();
    this.Value = (object) num;
  }
}
