// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.FiltersManager
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using Intermech.Kernel.Search;
using System;
using System.IO;

#nullable disable
namespace Intermech.Security.EventLog;

internal sealed class FiltersManager
{
  private static FilterCollection _filters;
  private static bool _precached;
  private const string _filterPrefix = "FilterGuid_";
  private const string _attributePrefix = "AttributeID_";
  private const string _configID = "EventLogFilters";
  private const string _configFile = "EventLog.Filters";
  private const string _filterName = "FilterName";
  private const string _filterItemEnabled = "Enabled";
  private const string _filterItemAttributeID = "AttributeID";
  private const string _filterItemOperator = "Operator";
  private const string _filterItemValue = "Value";

  public static FilterCollection Filters
  {
    get
    {
      if (!FiltersManager._precached)
      {
        FiltersManager._filters = FiltersManager.PrecacheFilters();
        FiltersManager._precached = true;
      }
      return FiltersManager._filters;
    }
  }

  public static void Flush()
  {
    if (!FiltersManager._precached || !FiltersManager.Filters.Modified)
      return;
    ConfigurationManager configurationManager = new ConfigurationManager("EventLogFilters");
    for (int index1 = 0; index1 < FiltersManager.Filters.Count; ++index1)
    {
      IConfiguration configuration1 = configurationManager.Create("FilterGuid_" + FiltersManager.Filters[index1].Guid.ToString());
      configuration1.SetProperty("FilterName", FiltersManager.Filters[index1].Name);
      for (int index2 = 0; index2 < FiltersManager.Filters[index1].Items.Length; ++index2)
      {
        IConfiguration configuration2 = configuration1;
        int attributeId = (int) FiltersManager.Filters[index1].Items[index2].AttributeID;
        string name = "AttributeID_" + attributeId.ToString();
        IConfiguration configuration3 = configuration2.Add(name);
        configuration3.SetProperty("Enabled", FiltersManager.Filters[index1].Items[index2].Enabled ? "+" : "-");
        attributeId = (int) FiltersManager.Filters[index1].Items[index2].Operator;
        configuration3.SetProperty("Operator", attributeId.ToString());
        configuration3.SetProperty("Value", FiltersManager.Filters[index1].Items[index2].AsString);
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        configurationManager.Save((Stream) memoryStream);
        IDBAttribute configAttribute = sessionKeeper.Session.Configurations.GetConfigAttribute("EventLog.Filters");
        long dbObjectId = configAttribute.DBObjectID;
        BlobInformation blobInformation = new BlobInformation(0L, 0L, DateTime.Now, "EventLog.Filters", ArcMethods.ZLibPacked, string.Empty);
        int attributeId = configAttribute.AttributeID;
        int index = configAttribute.Index;
        BlobInformation aBlobInformation = blobInformation;
        MemoryStream aSourceStream = memoryStream;
        new BlobProcWriter(dbObjectId, AttributableElements.Object, attributeId, index, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      }
    }
    FiltersManager.Filters.Modified = false;
  }

  private static FilterCollection PrecacheFilters()
  {
    FilterCollection filterCollection = new FilterCollection();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (MemoryStream aDestStream = new MemoryStream())
      {
        IDBAttribute configAttribute = sessionKeeper.Session.Configurations.GetConfigAttribute("EventLog.Filters");
        new BlobProcReader(configAttribute.DBObjectID, AttributableElements.Object, configAttribute.AttributeID, configAttribute.Index, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
        if (aDestStream.Length != 0L)
        {
          aDestStream.Position = 0L;
          ConfigurationManager configurationManager = new ConfigurationManager("EventLogFilters");
          configurationManager.Load((Stream) aDestStream);
          for (int index1 = 0; index1 < configurationManager.Configurations.Count; ++index1)
          {
            IConfiguration configuration1 = configurationManager.Configurations[index1];
            try
            {
              Filter filter = new Filter(new Guid(configuration1.Name.Remove(0, "FilterGuid_".Length)));
              filter.Name = configuration1.GetProperty("FilterName");
              for (int index2 = 0; index2 < configuration1.Configurations.Count; ++index2)
              {
                IConfiguration configuration2 = configuration1.Configurations[index2];
                ObligatoryObjectAttributes attributeID = (ObligatoryObjectAttributes) int.Parse(configuration2.Name.Remove(0, "AttributeID_".Length));
                FilterItem filterItem = filter.FindItem(attributeID);
                if (filterItem != null)
                {
                  filterItem.Enabled = configuration2.GetProperty("Enabled") == "+";
                  filterItem.Operator = (FlagsConditions) int.Parse(configuration2.GetProperty("Operator"));
                  filterItem.AsString = configuration2.GetProperty("Value");
                }
              }
              filterCollection.Add(filter);
            }
            catch
            {
            }
          }
        }
      }
    }
    filterCollection.Modified = false;
    return filterCollection;
  }
}
