// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.InformationRequest.InformationRequest
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.InformationCollector;
using Intermech.Interfaces.Plugins;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Client.InformationRequest;

/// <summary>
///  сформировать и отправить в техподдержку ошибку или вопрос
/// </summary>
public class InformationRequest
{
  /// <summary>
  /// Собрать информацию об ошибки и сохранить её в файл, указанный пользователем
  /// </summary>
  /// <param name="ex">ошибка</param>
  /// <param name="reportZipName">имя архива, в котором будет сохранён отчёт</param>
  public void SaveReportToXml(Exception ex, string reportZipName)
  {
    string str = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(reportZipName));
    this.FormReport(IPSInformation.ExceptionInformation(ex), str);
    IPSInformation.PackReport(str, reportZipName);
  }

  /// <summary>Сделать скриншот экрана</summary>
  /// <param name="filePath">имя файла, в котором будет храниться скриншот</param>
  /// <returns></returns>
  private void CreateScreenshot(string filePath)
  {
    if (Screen.AllScreens.Length > 1)
    {
      List<byte[]> screensDataList = new List<byte[]>();
      ScreenshotCapture screenshotCapture = new ScreenshotCapture();
      using (Bitmap bitmap = screenshotCapture.CaptureDesktop())
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          bitmap.Save((Stream) memoryStream, ImageFormat.Png);
          screensDataList.Add(memoryStream.ToArray());
        }
      }
      for (int index = 0; index < Screen.AllScreens.Length; ++index)
      {
        using (Bitmap bitmap = screenshotCapture.CaptureMonitor(index))
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            bitmap.Save((Stream) memoryStream, ImageFormat.Png);
            screensDataList.Add(memoryStream.ToArray());
          }
        }
      }
      ScreenshotSelector screenshotSelector = new ScreenshotSelector();
      screenshotSelector.SetScreenShot(screensDataList);
      if (screenshotSelector.ShowDialog() == DialogResult.OK)
      {
        using (MemoryStream memoryStream = new MemoryStream(screensDataList[screenshotSelector.SelectedScreenshotIndex]))
        {
          using (Image image = Image.FromStream((Stream) memoryStream))
            image.Save(filePath, ImageFormat.Png);
        }
      }
      else
      {
        using (MemoryStream memoryStream = new MemoryStream(screensDataList[0]))
        {
          using (Image image = Image.FromStream((Stream) memoryStream))
            image.Save(filePath, ImageFormat.Png);
        }
      }
    }
    else
    {
      using (Bitmap bitmap = new ScreenshotCapture().CaptureDesktop())
        bitmap.Save(filePath, ImageFormat.Png);
    }
  }

  /// <summary>сохранить отчёт в поток</summary>
  /// <param name="report"></param>
  /// <param name="fileName"></param>
  private void SaveToFile(InformationNode report, string fileName)
  {
    XMLSettingsStorage sets = new XMLSettingsStorage();
    sets.document.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><IPS />");
    foreach (InformationNode currentInfoNode in (List<InformationNode>) report)
      this.WriteIntoXml(sets, (XmlNode) sets.document.DocumentElement, currentInfoNode);
    sets.Save(fileName);
  }

  /// <summary>добавляем узлы с информацией в xml</summary>
  /// <param name="sets"></param>
  /// <param name="parentNode"></param>
  /// <param name="currentInfoNode"></param>
  private void WriteIntoXml(
    XMLSettingsStorage sets,
    XmlNode parentNode,
    InformationNode currentInfoNode)
  {
    if (currentInfoNode.Type == NodeType.Element)
    {
      XmlNode parentNode1 = sets.AddNode(parentNode, currentInfoNode.NodeName);
      parentNode1.InnerText = currentInfoNode.NodeValue;
      foreach (InformationNode currentInfoNode1 in (List<InformationNode>) currentInfoNode)
        this.WriteIntoXml(sets, parentNode1, currentInfoNode1);
    }
    else
      sets.SetAttributeValue(parentNode, currentInfoNode.NodeName, currentInfoNode.NodeValue);
  }

  /// <summary>
  /// сформировать отчёт об ошибке и отправить его в техподдержку
  /// </summary>
  /// <param name="ex">ошибка</param>
  /// <param name="reportTopic"> тема письма</param>
  /// <param name="reportText"> текст письма </param>
  public void SendReport(Exception ex, string reportTopic, string reportText)
  {
    InformationNode additionalInformation = IPSInformation.ExceptionInformation(ex);
    additionalInformation.Add(new InformationNode("Topic", reportTopic));
    additionalInformation.Add(new InformationNode("Request", reportText));
    this.SendRequest(additionalInformation);
  }

  /// <summary>отправить запрос  в техподдержку</summary>
  /// <param name="additionalInformation">Может быть информация оо исключении или информация от пользователя</param>
  /// <param name="attach">файлы, которые хочет отправить пользователь </param>
  public void SendRequest(InformationNode additionalInformation, params string[] attach)
  {
    string str1 = Path.Combine(Path.GetTempPath(), "IPS_Report" + (object) DateTime.Now.Ticks);
    this.FormReport(additionalInformation, str1);
    if (attach != null && attach.Length != 0)
    {
      string str2 = Path.Combine(str1, IPSInformation.ATTACH_PATH);
      if (!Directory.Exists(str2))
        Directory.CreateDirectory(str2);
      for (int index = 0; index < attach.Length; ++index)
      {
        string str3 = attach[index];
        string str4 = Path.Combine(str2, Path.GetFileName(str3));
        if (File.Exists(str4))
          str4 = Path.Combine(str2, $"{Path.GetFileNameWithoutExtension(str3)}_{index}.{Path.GetExtension(str3)}");
        File.Copy(str3, str4);
      }
    }
    string str5 = str1 + ".zip";
    IPSInformation.PackReport(str1, str5);
    Mapi mapi = new Mapi();
    if (!mapi.Logon(IntPtr.Zero))
      throw new Exception($"Во время отправки письма произошла ошибка {mapi.Error()}");
    string reportNodeValue1 = IPSInformation.GetReportNodeValue(additionalInformation, "Topic");
    string reportNodeValue2 = IPSInformation.GetReportNodeValue(additionalInformation, "Request");
    if (!mapi.SendReport(str5, reportNodeValue1, reportNodeValue2))
      throw new Exception("Во время отправки письма произошла ошибка " + mapi.Error());
    mapi.Reset();
    mapi.Logoff();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="information"></param>
  /// <param name="reportFolderName"></param>
  private void FormReport(InformationNode information, string reportFolderName)
  {
    if (ApplicationServices.Container.GetService(typeof (IMainFormUpdate)) is IMainFormUpdate service && service.MainForm.InvokeRequired)
      service.MainForm.Invoke((Delegate) new Intermech.Interfaces.Client.InformationRequest.InformationRequest.FormReportDelegate(this.InternalFormReport), (object) information, (object) reportFolderName);
    else
      this.InternalFormReport(information, reportFolderName);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="information"></param>
  /// <param name="reportFolderName"> папка, в которой будет храниться отчёт </param>
  /// <returns>путь к папке с отчётом</returns>
  private void InternalFormReport(InformationNode information, string reportFolderName)
  {
    if (!Directory.Exists(reportFolderName))
      Directory.CreateDirectory(reportFolderName);
    string fileName = Path.Combine(reportFolderName, Path.GetFileName(reportFolderName)) + ".xml";
    string str1 = Path.Combine(reportFolderName, IPSInformation.CLIENT_FILES_PATH);
    Directory.CreateDirectory(str1);
    string str2 = Path.Combine(reportFolderName, IPSInformation.SERVER_FILES_PATH);
    Directory.CreateDirectory(str2);
    string str3 = Path.Combine(reportFolderName, IPSInformation.ATTACH_PATH);
    Directory.CreateDirectory(str3);
    this.CreateScreenshot(Path.Combine(str3, DateTime.Now.Ticks.ToString()) + ".jpg");
    InformationNode report = new InformationNode("IPS");
    report.Add(information);
    report.Add(this.ClientInformation());
    report.Add(this.ServerInformation());
    List<string> clientLogFiles = this.GetClientLogFiles();
    if (clientLogFiles != null)
    {
      for (int index = 0; index < clientLogFiles.Count; ++index)
      {
        string str4 = clientLogFiles[index];
        string destFileName = Path.Combine(str1, Path.GetFileName(str4));
        File.Copy(str4, destFileName, true);
      }
    }
    List<string> serverLogFiles = this.GetServerLogFiles(str2);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        if (this.GetServerInformationCollector(sessionKeeper.Session) != null)
        {
          if (serverLogFiles.Count > 0)
          {
            string str5 = serverLogFiles[0];
            System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap()
            {
              ExeConfigFilename = str5
            }, ConfigurationUserLevel.None);
            if (configuration.AppSettings.Settings["Password"] != null)
              configuration.AppSettings.Settings["Password"].Value = "EMPTY_VALUE";
            if (configuration.AppSettings.Settings["Portal Password"] != null)
              configuration.AppSettings.Settings["Portal Password"].Value = "EMPTY_VALUE";
            configuration.Save();
          }
        }
      }
      catch (Exception ex)
      {
      }
    }
    string str6 = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", string.Empty);
    if (File.Exists(str6))
    {
      string destFileName = Path.Combine(str1, Path.GetFileName(str6));
      File.Copy(str6, destFileName, true);
    }
    this.SaveToFile(report, fileName);
  }

  protected virtual IServerInformationCollector GetServerInformationCollector(IUserSession session)
  {
    return session.GetCustomService(typeof (IServerInformationCollector)) as IServerInformationCollector;
  }

  /// <summary>Список файлов с логами клиента</summary>
  /// <param name="logSize"></param>
  /// <returns>Список файлов логов удовлетворяющий условиям что файл с логами не должен превышеть 1 мб</returns>
  protected virtual List<string> GetClientLogFiles()
  {
    List<string> clientLogFiles = (List<string>) null;
    string str = ConfigurationManager.AppSettings.Get("LogPath");
    if (!string.IsNullOrEmpty(str))
      str = Environment.ExpandEnvironmentVariables(str);
    if (!string.IsNullOrEmpty(str))
    {
      clientLogFiles = ((IEnumerable<string>) Directory.GetFiles(str, "*.log", SearchOption.AllDirectories)).ToList<string>();
      for (int index = 0; index < clientLogFiles.Count; ++index)
      {
        FileInfo fileInfo = new FileInfo(clientLogFiles[index]);
        if (fileInfo.Length > (long) IPSInformation.MAX_SINGLE_LOG_FILE_SIZE)
        {
          string path2 = $"{fileInfo.Name}_truncate_{DateTime.Now.ToString("dd.MM.yyyy_HH.mm")}{fileInfo.Extension}";
          string tempFilePath = Path.Combine(Path.GetTempPath(), path2);
          if (this.TruncateFile(clientLogFiles[index], tempFilePath))
          {
            clientLogFiles[index] = tempFilePath;
          }
          else
          {
            clientLogFiles.Remove(clientLogFiles[index]);
            --index;
          }
        }
      }
    }
    return clientLogFiles;
  }

  protected List<string> GetServerLogFiles(string serverFilesPath)
  {
    List<string> serverLogFiles = new List<string>();
    try
    {
      IServerInformationCollector customService = (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IServerInformationCollector)) as IServerInformationCollector;
      List<FileInfo> fileInfoList = customService.LogFiles();
      FileInfo fileInfo1 = fileInfoList[fileInfoList.Count - 1];
      string path1 = Path.Combine(serverFilesPath, fileInfo1.Name);
      using (FileStream fileStream = new FileStream(path1, FileMode.Create, FileAccess.Write))
      {
        byte[] buffer = customService.ReadLogFile(fileInfo1.FullName);
        fileStream.Write(buffer, 0, buffer.Length);
      }
      serverLogFiles.Add(path1);
      fileInfoList.Remove(fileInfo1);
      for (int index = 0; index < fileInfoList.Count; ++index)
      {
        FileInfo fileInfo2 = fileInfoList[index];
        if (fileInfo2.Length > (long) IPSInformation.MAX_SINGLE_LOG_FILE_SIZE)
        {
          string path2 = $"{fileInfo2.Name}_truncate_{DateTime.Now.ToString("dd.MM.yyyy_HH.mm")}{fileInfo2.Extension}";
          string path3 = Path.Combine(serverFilesPath, path2);
          using (FileStream fileStream = new FileStream(path3, FileMode.Create, FileAccess.Write))
          {
            byte[] buffer = customService.TruncateLogFile(fileInfo2.FullName);
            fileStream.Write(buffer, 0, buffer.Length);
          }
          serverLogFiles.Add(path3);
        }
        else
        {
          string path4 = Path.Combine(serverFilesPath, fileInfo2.Name);
          using (FileStream fileStream = new FileStream(path4, FileMode.Create, FileAccess.Write))
          {
            byte[] buffer = customService.ReadLogFile(fileInfo2.FullName);
            fileStream.Write(buffer, 0, buffer.Length);
          }
          serverLogFiles.Add(path4);
        }
      }
    }
    catch (Exception ex)
    {
    }
    return serverLogFiles;
  }

  private bool TruncateFile(string logFile, string tempFilePath)
  {
    try
    {
      using (FileStream fileStream1 = new FileStream(logFile, FileMode.Open))
      {
        long num = fileStream1.Seek((long) -IPSInformation.MAX_SINGLE_LOG_FILE_SIZE, SeekOrigin.End);
        using (FileStream fileStream2 = new FileStream(tempFilePath, FileMode.Create))
        {
          byte[] buffer = new byte[fileStream1.Length - num];
          fileStream1.Read(buffer, 0, buffer.Length);
          fileStream2.Write(buffer, 0, buffer.Length);
        }
      }
      return true;
    }
    catch
    {
      return false;
    }
  }

  protected virtual List<FileInfo> GetServerLogFiles(ref long logSize)
  {
    List<FileInfo> serverLogFiles = new List<FileInfo>();
    try
    {
      serverLogFiles = ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IServerInformationCollector)) as IServerInformationCollector).LogFiles();
      for (int index = 0; index < serverLogFiles.Count - 1; ++index)
      {
        FileInfo fileInfo = serverLogFiles[index];
        logSize += fileInfo.Length;
      }
    }
    catch (Exception ex)
    {
    }
    return serverLogFiles;
  }

  protected virtual void GetUserInformation(InformationNode clientNode)
  {
    if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service))
      return;
    clientNode.Add(new InformationNode("UserName", service.UserName));
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(service.RoleGuid);
        clientNode.Add(new InformationNode("UserRole", objectInfo.Caption));
      }
    }
    catch
    {
      if (service == null)
        return;
      clientNode.Add(new InformationNode("UserRole", service.RoleGuid.ToString()));
    }
  }

  /// <summary>собираем всё что можно о клиенте</summary>
  /// <returns></returns>
  private InformationNode ClientInformation()
  {
    InformationNode clientNode = new InformationNode(nameof (ClientInformation));
    clientNode.Add(IPSInformation.VersionInformation());
    InformationNode informationNode = new InformationNode("Framework");
    foreach (string nodeValue in FrameworkVersionDetection.SearchFrameworkVersionsInstalled())
      informationNode.Add(new InformationNode("FrameworkVersion", nodeValue));
    clientNode.Add(informationNode);
    clientNode.Add(new InformationNode("WindowsVersion", OSInformation.GetOSInfo()));
    this.GetUserInformation(clientNode);
    clientNode.Add(this.PluginsInformation());
    clientNode.Add(IPSInformation.ClientHomeConfig());
    clientNode.Add(this.ClientOutput());
    return clientNode;
  }

  protected virtual InformationNode PluginsInformation()
  {
    return IPSInformation.PluginsInformation(ServicesManager.GetService(typeof (IPluginManager)) as IPluginManager);
  }

  /// <summary>собираем информацию о сервере</summary>
  /// <returns></returns>
  protected virtual InformationNode ServerInformation()
  {
    try
    {
      return ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IServerInformationCollector)) as IServerInformationCollector).CollectServerInformation();
    }
    catch (Exception ex)
    {
      return new InformationNode("ServerException")
      {
        IPSInformation.ExceptionInformation(ex)
      };
    }
  }

  /// <summary>Получить информацию из окна Вывод</summary>
  /// <returns></returns>
  private InformationNode ClientOutput()
  {
    InformationNode informationNode = new InformationNode(nameof (ClientOutput));
    if (ServicesManager.GetService(typeof (IOutputView)) is IOutputView service && service is IOutputViewHistory)
    {
      foreach (Tuple<string, string> tuple in (service as IOutputViewHistory).GetOutputHistory())
      {
        string nodeValue = tuple.Item1;
        informationNode.Add(new InformationNode("OutputMessage", tuple.Item2)
        {
          new InformationNode("OutputCategory", nodeValue, NodeType.Attribute)
        });
      }
    }
    return informationNode;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="information"></param>
  /// <param name="reportFolderName"></param>
  private delegate void FormReportDelegate(InformationNode information, string reportFolderName);
}
