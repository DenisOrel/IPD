// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.PumpSettings
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Sync.DataBase;
using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

internal static class PumpSettings
{
  private static Dictionary<string, Guid> _attributes = new Dictionary<string, Guid>();
  private static Dictionary<string, string> _shortMeasureNames = new Dictionary<string, string>();
  private static List<Guid> _artTypes = new List<Guid>();
  private static List<Guid> _docTypes = new List<Guid>();

  public static void Init(string path, IDataBase sourceDB)
  {
    FileInfo fileInfo = new FileInfo(Path.Combine(path, "PumpSettings.xml"));
    if (!fileInfo.Exists)
      throw new FileNotFoundException($"В директории '{path}' не найден файл 'PumpSettings.xml'");
    PumpSettings._attributes.Clear();
    PumpSettings._shortMeasureNames.Clear();
    PumpSettings._artTypes.Clear();
    PumpSettings._docTypes.Clear();
    ConfigurationManager configurationManager = new ConfigurationManager("Settings");
    using (Stream stream = (Stream) File.OpenRead(fileInfo.FullName))
      configurationManager.Load(stream);
    DataTable dataTable = sourceDB.ExecuteDataTable("SELECT F.F_KEY,T.F_TABLE,F.F_FIELD FROM IM_FIELDS F, IM_TABLES T WHERE F.F_TABLE_ID = T.F_KEY");
    dataTable.PrimaryKey = new DataColumn[1]
    {
      dataTable.Columns[0]
    };
    IConfiguration configuration1 = configurationManager.Open("IMBASEFIELD");
    if (configuration1 != null)
    {
      foreach (IConfiguration configuration2 in (IEnumerable) configuration1.Configurations)
      {
        if (configuration2.HasProperty("NAME") && configuration2.HasProperty("GUID"))
        {
          string str = configuration2.GetProperty("NAME");
          string property = configuration2.GetProperty("GUID");
          long result;
          if (str.IndexOf('.') == -1 && long.TryParse(str, out result))
          {
            DataRow dataRow = dataTable.Rows.Find((object) result);
            if (dataRow != null)
              str = $"{dataRow[1].ToString()}.{dataRow[2].ToString()}";
            else
              continue;
          }
          if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(property))
            PumpSettings._attributes.Add(str, new Guid(property));
        }
      }
    }
    IConfiguration configuration3 = configurationManager.Open("MEASURES");
    if (configuration3 != null)
    {
      foreach (IConfiguration configuration4 in (IEnumerable) configuration3.Configurations)
      {
        if (configuration4.HasProperty("NAME") && configuration4.HasProperty("NEW_NAME"))
        {
          string property1 = configuration4.GetProperty("NAME");
          string property2 = configuration4.GetProperty("NEW_NAME");
          if (!string.IsNullOrEmpty(property1) && !string.IsNullOrEmpty(property2))
            PumpSettings._shortMeasureNames[property1] = property2;
        }
      }
    }
    IConfiguration configuration5 = configurationManager.Open("ART_TYPES");
    if (configuration5 != null)
    {
      foreach (IConfiguration configuration6 in (IEnumerable) configuration5.Configurations)
      {
        if (configuration6.HasProperty("GUID"))
        {
          Guid guid = new Guid(configuration6.GetProperty("GUID"));
          if (!PumpSettings._artTypes.Contains(guid))
            PumpSettings._artTypes.Add(guid);
        }
      }
    }
    IConfiguration configuration7 = configurationManager.Open("DOC_TYPES");
    if (configuration7 == null)
      return;
    foreach (IConfiguration configuration8 in (IEnumerable) configuration7.Configurations)
    {
      if (configuration8.HasProperty("GUID"))
      {
        Guid guid = new Guid(configuration8.GetProperty("GUID"));
        if (!PumpSettings._docTypes.Contains(guid))
          PumpSettings._docTypes.Add(guid);
      }
    }
  }

  public static Guid GetAttributeGuid(string table, string field)
  {
    Guid guid;
    return PumpSettings._attributes == null || PumpSettings._attributes.Count == 0 || !PumpSettings._attributes.TryGetValue($"{table}.{field}", out guid) ? Guid.Empty : guid;
  }

  public static bool TryFoundMeasure(string shortMeasureName, out string newShortMeasureName)
  {
    newShortMeasureName = string.Empty;
    return PumpSettings._shortMeasureNames != null && PumpSettings._shortMeasureNames.TryGetValue(shortMeasureName, out newShortMeasureName);
  }

  public static int[] GetArtTypes()
  {
    return PumpSettings._artTypes == null || PumpSettings._artTypes.Count == 0 ? (int[]) null : PumpSettings._artTypes.Select<Guid, int>(new System.Func<Guid, int>(MetaDataHelper.GetObjectTypeID)).Where<int>((System.Func<int, bool>) (x => x != -1)).ToArray<int>();
  }

  public static int[] GetDocTypes()
  {
    return PumpSettings._docTypes == null || PumpSettings._docTypes.Count == 0 ? (int[]) null : PumpSettings._docTypes.Select<Guid, int>(new System.Func<Guid, int>(MetaDataHelper.GetObjectTypeID)).Where<int>((System.Func<int, bool>) (x => x != -1)).ToArray<int>();
  }
}
