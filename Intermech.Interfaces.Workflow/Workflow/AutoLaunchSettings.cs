// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.AutoLaunchSettings
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Workflow;

public class AutoLaunchSettings : List<AutoLaunchInfo>
{
  private static AutoLaunchSettings _all = (AutoLaunchSettings) null;
  public static string SectionName = "Workflow.AutoLaunch";
  private bool _loaded;
  private static HashSet<int> _allTypeIDs = (HashSet<int>) null;

  public static AutoLaunchSettings All
  {
    get
    {
      if (AutoLaunchSettings._all == null)
        AutoLaunchSettings._all = new AutoLaunchSettings();
      return AutoLaunchSettings._all;
    }
  }

  public void Load(XmlIni ini, IUserSession session)
  {
    this.Clear();
    long num1 = ini.ReadInteger("", "Count");
    for (int index = 1; (long) index <= num1; ++index)
    {
      string Section = "i" + index.ToString();
      int num2 = (int) ini.ReadInteger(Section, "TypeID");
      long num3 = ini.ReadInteger(Section, "SchemeID");
      ProcessPriority processPriority = (ProcessPriority) ini.ReadInteger(Section, "ProcessPriority", 0L);
      AutoLaunchInfo autoLaunchInfo = new AutoLaunchInfo(num2, num3);
      autoLaunchInfo.ProcessPriority = processPriority;
      if (session != null)
      {
        IDBObjectType objectType = session.GetObjectType(num2);
        autoLaunchInfo.TypeName = objectType != null ? objectType.ObjectTypeName : "??";
        QuickObjectInfo objectInfo = session.GetObjectInfo(num3);
        autoLaunchInfo.SchemeName = !objectInfo.Empty ? objectInfo.Caption : "??";
      }
      this.Add(autoLaunchInfo);
    }
  }

  public void Load(IUserSession session, bool loadNames = false)
  {
    if (!(ApplicationServices.Container.GetService(typeof (IDBConfigurations)) is IDBConfigurations dbConfigurations1))
      dbConfigurations1 = session.Configurations;
    IDBConfigurations dbConfigurations2 = dbConfigurations1;
    byte[] config_file = new byte[0];
    try
    {
      dbConfigurations2.LoadConfigData(AutoLaunchSettings.SectionName, out BlobInformation _, out config_file, 0L);
    }
    catch (Exception ex)
    {
      if (ApplicationServices.Container.GetService(typeof (IOutputView)) is IOutputView service)
        service.WriteString("Ошибки", "При загрузке настроек автозапуска процессов произошла ошибка: " + ex.Message);
    }
    if (config_file.Length != 0)
    {
      using (MemoryStream memoryStream = new MemoryStream(config_file))
      {
        memoryStream.Position = 0L;
        XmlIni ini = new XmlIni();
        ini.Load((Stream) memoryStream);
        this.Load(ini, loadNames ? session : (IUserSession) null);
      }
    }
    AutoLaunchSettings._allTypeIDs = (HashSet<int>) null;
    this._loaded = true;
  }

  public bool Loaded => this._loaded;

  public void Save(XmlIni ini)
  {
    int num = 1;
    foreach (AutoLaunchInfo autoLaunchInfo in (List<AutoLaunchInfo>) this)
    {
      string Section = "i" + num.ToString();
      ini.WriteInteger(Section, "TypeID", (long) autoLaunchInfo.TypeID);
      ini.WriteInteger(Section, "SchemeID", autoLaunchInfo.SchemeID);
      ini.WriteInteger(Section, "ProcessPriority", (long) autoLaunchInfo.ProcessPriority);
      ++num;
    }
    ini.WriteInteger("", "Count", (long) (num - 1));
  }

  public void Save(IUserSession session)
  {
    if (AutoLaunchSettings._all == null)
      return;
    IDBConfigurations configurations = session.Configurations;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      XmlIni ini = new XmlIni();
      this.Save(ini);
      ini.Save((Stream) memoryStream);
      BlobInformation config_info = new BlobInformation(memoryStream.Length, memoryStream.Length, DateTime.Now, AutoLaunchSettings.SectionName, ArcMethods.NotPacked, "");
      configurations.WriteConfigData(config_info, memoryStream.ToArray(), 0L);
    }
  }

  /// <summary>
  /// Список всех типов объектов (включая вложенные типы), для которых настроен автозапуск процессов
  /// </summary>
  public static HashSet<int> AllTypeIDs
  {
    get
    {
      if (AutoLaunchSettings._allTypeIDs == null)
      {
        AutoLaunchSettings._allTypeIDs = new HashSet<int>();
        foreach (AutoLaunchInfo autoLaunchInfo in (List<AutoLaunchInfo>) AutoLaunchSettings.All)
        {
          if (!AutoLaunchSettings._allTypeIDs.Contains(autoLaunchInfo.TypeID))
            AutoLaunchSettings._allTypeIDs.Add(autoLaunchInfo.TypeID);
          foreach (int num in MetaDataHelper.GetObjectTypeChildrenID(autoLaunchInfo.TypeID))
          {
            if (!AutoLaunchSettings._allTypeIDs.Contains(num))
              AutoLaunchSettings._allTypeIDs.Add(num);
          }
        }
      }
      return AutoLaunchSettings._allTypeIDs;
    }
  }
}
