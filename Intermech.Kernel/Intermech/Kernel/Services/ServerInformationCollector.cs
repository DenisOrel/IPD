// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ServerInformationCollector
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.InformationCollector;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;


namespace Intermech.Kernel.Services;

public class ServerInformationCollector : LongLifeObject, IServerInformationCollector
{
  public InformationNode CollectServerInformation()
  {
    IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone(nameof (ServerInformationCollector));
    InformationNode informationNode1 = new InformationNode("ServerInformation");
    try
    {
      informationNode1.Add(IPSInformation.VersionInformation());
      string versionInstalled = IISVersionDetection.GetIISVersionInstalled();
      if (!string.IsNullOrEmpty(versionInstalled))
        informationNode1.Add(new InformationNode("IISVersion", versionInstalled));
      InformationNode informationNode2 = new InformationNode("Framework");
      foreach (string nodeValue in FrameworkVersionDetection.SearchFrameworkVersionsInstalled())
        informationNode2.Add(new InformationNode("FrameworkVersion", nodeValue));
      informationNode1.Add(informationNode2);
      informationNode1.Add(new InformationNode("WindowsVersion", OSInformation.GetOSInfo()));
      IPluginManager service = ServerServices.GetService(typeof (IPluginManager)) as IPluginManager;
      informationNode1.Add(IPSInformation.PluginsInformation(service));
      informationNode1.Add(IPSInformation.ServerOutput(sessionTemporaryClone));
    }
    catch (Exception ex)
    {
      informationNode1.Add(IPSInformation.ExceptionInformation(ex));
    }
    finally
    {
      sessionTemporaryClone?.Logout(nameof (ServerInformationCollector));
    }
    return informationNode1;
  }

  public List<FileInfo> LogFiles()
  {
    List<FileInfo> fileInfoList = new List<FileInfo>();
    string currentDirectory = ConfigurationManager.AppSettings.Get("LogPath");
    if (string.IsNullOrEmpty(currentDirectory))
      currentDirectory = Environment.CurrentDirectory;
    foreach (string file in Directory.GetFiles(currentDirectory, "*.log", SearchOption.AllDirectories))
      fileInfoList.Add(new FileInfo(file));
    string str = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", string.Empty);
    if (File.Exists(str))
      fileInfoList.Add(new FileInfo(str));
    return fileInfoList;
  }

  public byte[] ReadLogFile(string logFileName)
  {
    using (FileStream fileStream = new FileStream(logFileName, FileMode.Open, FileAccess.Read))
    {
      byte[] buffer = new byte[fileStream.Length];
      fileStream.Read(buffer, 0, buffer.Length);
      return buffer;
    }
  }

  public byte[] TruncateLogFile(string logFileName)
  {
    using (FileStream fileStream = new FileStream(logFileName, FileMode.Open, FileAccess.Read))
    {
      long num = fileStream.Seek((long) -IPSInformation.MAX_SINGLE_LOG_FILE_SIZE, SeekOrigin.End);
      byte[] buffer = new byte[fileStream.Length - num];
      fileStream.Read(buffer, 0, buffer.Length);
      return buffer;
    }
  }
}
