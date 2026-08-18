// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.IMCatalogs
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.ECO.Client;

public class IMCatalogs : List<long>
{
  private static IMCatalogs _all = (IMCatalogs) null;
  public static string SectionName = "ECO.IMBaseCatalogs";
  private bool _loaded;

  public static IMCatalogs All
  {
    get
    {
      if (IMCatalogs._all == null)
        IMCatalogs._all = new IMCatalogs();
      return IMCatalogs._all;
    }
  }

  public void Load(XmlIni ini)
  {
    this.Clear();
    long num = ini.ReadInteger("", "Count");
    for (int index = 1; (long) index <= num; ++index)
    {
      string Section = "i" + index.ToString();
      this.Add(ini.ReadInteger(Section, "CatID"));
    }
  }

  public void Load(IUserSession session)
  {
    IDBConfigurations configurations = session.Configurations;
    byte[] config_file = new byte[0];
    try
    {
      configurations.LoadConfigData(IMCatalogs.SectionName, out BlobInformation _, out config_file, session.UserID);
    }
    catch
    {
    }
    if (config_file.Length != 0)
    {
      using (MemoryStream memoryStream = new MemoryStream(config_file))
      {
        memoryStream.Position = 0L;
        XmlIni ini = new XmlIni();
        ini.Load((Stream) memoryStream);
        this.Load(ini);
      }
    }
    this._loaded = true;
  }

  public bool Loaded => this._loaded;

  public void Save(XmlIni ini)
  {
    int num1 = 1;
    foreach (long num2 in (List<long>) this)
    {
      string Section = "i" + num1.ToString();
      ini.WriteInteger(Section, "CatID", num2);
      ++num1;
    }
    ini.WriteInteger("", "Count", (long) (num1 - 1));
  }

  public void Save(IUserSession session)
  {
    if (IMCatalogs._all == null)
      return;
    IDBConfigurations configurations = session.Configurations;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      XmlIni ini = new XmlIni();
      this.Save(ini);
      ini.Save((Stream) memoryStream);
      BlobInformation config_info = new BlobInformation(memoryStream.Length, memoryStream.Length, DateTime.Now, IMCatalogs.SectionName, ArcMethods.NotPacked, "");
      configurations.WriteConfigData(config_info, memoryStream.ToArray(), session.UserID);
      memoryStream.Close();
    }
  }
}
