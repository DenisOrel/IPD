// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.Server.ApplicationEventLog
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Text;

#nullable disable
namespace Intermech.Vault.Interfaces.Server;

public class ApplicationEventLog
{
  private static readonly ILog log = LogManager.GetLogger(typeof (ApplicationEventLog));
  private static Level loggingLevel;
  private static RollingFileAppender appender;

  public static ILog Log => ApplicationEventLog.log;

  public static void LoggingTypeChange(bool accept)
  {
    try
    {
      ApplicationEventLog.appender.ClearFilters();
      ApplicationEventLog.appender.Encoding = Encoding.Unicode;
      ApplicationEventLog.appender.AddFilter((IFilter) new LevelMatchFilter()
      {
        LevelToMatch = Level.Info,
        AcceptOnMatch = accept
      });
      ApplicationEventLog.appender.AddFilter((IFilter) new LevelMatchFilter()
      {
        LevelToMatch = Level.Warn,
        AcceptOnMatch = false
      });
      ApplicationEventLog.appender.AddFilter((IFilter) new LevelMatchFilter()
      {
        LevelToMatch = Level.Debug,
        AcceptOnMatch = false
      });
      ApplicationEventLog.appender.AddFilter((IFilter) new LevelMatchFilter()
      {
        LevelToMatch = ApplicationEventLog.loggingLevel,
        AcceptOnMatch = true
      });
    }
    catch (Exception ex)
    {
    }
  }

  public static void InitLogger()
  {
    ApplicationEventLog.loggingLevel = new Level(41000, "LOGGING");
    ApplicationEventLog.log.Logger.Repository.LevelMap.Add(ApplicationEventLog.loggingLevel);
    PatternLayout patternLayout1 = new PatternLayout("[%d{HH:mm:ss, dd:MM:yyyy}] %5p [Thread %t] (%C.%M) - %m%n");
    string path1 = string.Empty;
    RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Shell Folders");
    if (registryKey != null)
    {
      object obj = registryKey.GetValue("Common Documents");
      if (obj != null)
        path1 = Path.Combine(obj.ToString(), CommonVariables.LOG_PATH);
    }
    RollingFileAppender rollingFileAppender = new RollingFileAppender();
    string str1 = Path.Combine(path1, CommonVariables.DEV_LOG_FOLDER_NAME);
    Directory.CreateDirectory(str1);
    rollingFileAppender.File = str1 + "\\";
    rollingFileAppender.Layout = (ILayout) patternLayout1;
    rollingFileAppender.Encoding = Encoding.Unicode;
    PatternLayout patternLayout2 = new PatternLayout("[%d{HH:mm:ss, dd:MM:yyyy}] %5p %m%n");
    rollingFileAppender.StaticLogFileName = false;
    rollingFileAppender.RollingStyle = RollingFileAppender.RollingMode.Date;
    rollingFileAppender.MaxSizeRollBackups = 5;
    rollingFileAppender.DatePattern = "ddMMyyyy";
    ApplicationEventLog.appender = new RollingFileAppender();
    ApplicationEventLog.appender.Encoding = Encoding.Unicode;
    ApplicationEventLog.appender.StaticLogFileName = true;
    ApplicationEventLog.appender.RollingStyle = RollingFileAppender.RollingMode.Size;
    ApplicationEventLog.appender.MaximumFileSize = "10MB";
    ApplicationEventLog.appender.MaxSizeRollBackups = 0;
    ApplicationEventLog.appender.File = CommonVariables.EventLogPath = Path.Combine(path1, CommonVariables.LOG_FILE_NAME);
    try
    {
      string str2 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DeveloperFolder.lnk");
      if (!File.Exists(str2))
        ShellLinks.CreateShortcut(str2, str1);
    }
    catch
    {
    }
    try
    {
      string str3 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "IntermechVaultLog.lnk");
      if (!File.Exists(str3))
        ShellLinks.CreateShortcut(str3, ApplicationEventLog.appender.File);
    }
    catch
    {
    }
    ApplicationEventLog.appender.Layout = (ILayout) patternLayout2;
    ApplicationEventLog.appender.LockingModel = (FileAppender.LockingModelBase) new FileAppender.MinimalLock();
    BasicConfigurator.Configure((IAppender) ApplicationEventLog.appender);
    BasicConfigurator.Configure((IAppender) rollingFileAppender);
    ApplicationEventLog.appender.ActivateOptions();
    rollingFileAppender.ActivateOptions();
  }

  public static void LogginEventWrite(string massage)
  {
    ApplicationEventLog.log.Logger.Log((Type) null, ApplicationEventLog.loggingLevel, (object) massage, (Exception) null);
  }
}
