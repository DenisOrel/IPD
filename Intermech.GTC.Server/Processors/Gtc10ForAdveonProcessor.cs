// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.Gtc10ForAdveonProcessor
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Server.BackgroundTask;
using Intermech.GTC.Server.Exceptions;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Intermech.GTC.Server.Processors;

public class Gtc10ForAdveonProcessor : GtcProcessor
{
  private const string Draw2D = "Documents\\Draw2D";
  private const string Draw3D = "Documents\\Draw3D";
  private const string P21 = "Documents\\P21";
  private const string ToolImage = "Documents\\ToolImage";
  private const string CatalogHierarchyXml = "CatalogHierarchy.xml";
  private const string CatalogPackageXml = "CatalogPackage.xml";

  private Gtc10ForAdveonProcessor()
  {
    this.FileFolderPathHolder.SetData(new FileFolderItem[6]
    {
      new FileFolderItem(FileFolder.Folder, "Documents\\Draw2D", PackageType.Data, true),
      new FileFolderItem(FileFolder.Folder, "Documents\\Draw3D", PackageType.Data, true),
      new FileFolderItem(FileFolder.Folder, "Documents\\P21", PackageType.Data),
      new FileFolderItem(FileFolder.Folder, "Documents\\ToolImage", PackageType.Data, true),
      new FileFolderItem(FileFolder.File, "CatalogHierarchy.xml", PackageType.Hierarchy),
      new FileFolderItem(FileFolder.File, "CatalogPackage.xml", PackageType.Data)
    });
  }

  public Gtc10ForAdveonProcessor(
    Guid sessionGuid,
    BaseTaskForBackgroundTaskService task,
    IImportConfig importConfig)
    : this()
  {
    this.Session = UserSession.GetSessionByID(sessionGuid).Clone("GtcProcessor.Import") ?? throw new Exception(ServiceHolder.Rm.GetString("GTC_1"));
    this.Task = task;
    this.ImportConfig = importConfig;
  }

  protected override void PopulateRootHierarchyDescriptor()
  {
    base.PopulateRootHierarchyDescriptor();
    string itemByName = this.FileFolderPathHolder.GetItemByName("CatalogHierarchy.xml");
    XElement catalogClasses = File.Exists(itemByName) ? XDocument.Load(itemByName).Descendants((XName) "CatalogClasses").FirstOrDefault<XElement>() : throw new Exception($"File not found {itemByName}");
    if (catalogClasses == null)
      return;
    HierarchyItemAdveon[] hierarchyItems = this.GetHierarchyItems(catalogClasses);
    this.Task.CountElements = hierarchyItems.Length;
    Dictionary<string, HierarchyDescriptor> dictionary = new Dictionary<string, HierarchyDescriptor>();
    foreach (HierarchyItemAdveon hierarchyItemAdveon in hierarchyItems)
    {
      HierarchyDescriptor hierarchyDescriptor = new HierarchyDescriptor(hierarchyItemAdveon.Name, hierarchyItemAdveon.Id);
      dictionary.Add(hierarchyItemAdveon.Id, hierarchyDescriptor);
    }
    foreach (HierarchyItemAdveon hierarchyItemAdveon in hierarchyItems)
    {
      HierarchyDescriptor hierarchyDescriptor1;
      if (dictionary.TryGetValue(hierarchyItemAdveon.Id, out hierarchyDescriptor1))
      {
        HierarchyDescriptor hierarchyDescriptor2;
        if (dictionary.TryGetValue(hierarchyItemAdveon.ParentId, out hierarchyDescriptor2))
          hierarchyDescriptor2.Children.Add(hierarchyDescriptor1);
        else
          this.RootHierarchyDescriptor.Children.Add(hierarchyDescriptor1);
      }
    }
  }

  protected override void PopulateListDescriptorItems()
  {
    base.PopulateListDescriptorItems();
    string itemByName = this.FileFolderPathHolder.GetItemByName("CatalogPackage.xml");
    ItemAdveon[] itemAdveonArray = File.Exists(itemByName) ? XDocument.Load(itemByName).Descendants((XName) "CatalogTool").Select<XElement, ItemAdveon>((Func<XElement, ItemAdveon>) (x => new ItemAdveon(x))).ToArray<ItemAdveon>() : throw new Exception($"File not found {itemByName}");
    this.Task.CountElements = itemAdveonArray.Length;
    foreach (ItemAdveon itemAdveon in itemAdveonArray)
    {
      if (this.IsProcessStopping)
        throw new StopTaskException();
      try
      {
        string file = ZipExtractor.ExtractFile(Path.Combine(this.RootPath, itemAdveon.P21Path));
        Tuple<string, string>[] array = ((IEnumerable<Tuple<string, string>>) itemAdveon.Files).Select<Tuple<string, string>, Tuple<string, string>>((Func<Tuple<string, string>, Tuple<string, string>>) (x => new Tuple<string, string>(ZipExtractor.ExtractFile(Path.Combine(this.RootPath, x.Item1)), x.Item2))).Where<Tuple<string, string>>((Func<Tuple<string, string>, bool>) (x => File.Exists(x.Item1))).ToArray<Tuple<string, string>>();
        if (File.Exists(file))
          this.ListDescriptorItems.Add(array.Length != 0 ? new ListItemDescriptor(file, itemAdveon.GtcGeneric, array) : new ListItemDescriptor(file, itemAdveon.GtcGeneric));
      }
      catch (Exception ex)
      {
        this.Task.Result.Messages.Add(new BackgroundTaskMessage(ex.Message));
      }
      finally
      {
        this.Task.Next();
      }
    }
  }

  private HierarchyItemAdveon[] GetHierarchyItems(XElement catalogClasses)
  {
    Dictionary<string, HierarchyItemAdveon> dictionary = new Dictionary<string, HierarchyItemAdveon>();
    foreach (XElement descendant in catalogClasses.Descendants((XName) "CatalogClass"))
    {
      HierarchyItemAdveon hierarchyItemAdveon = new HierarchyItemAdveon(descendant);
      if (!dictionary.ContainsKey(hierarchyItemAdveon.Id))
        dictionary.Add(hierarchyItemAdveon.Id, hierarchyItemAdveon);
      else
        dictionary.TryGetValue(hierarchyItemAdveon.Id, out hierarchyItemAdveon);
      XElement parent1 = descendant.Parent == null || !(descendant.Parent.Name == (XName) "CatalogClassChildren") ? (XElement) null : descendant.Parent;
      if (parent1 != null)
      {
        XElement parent2 = parent1.Parent == null || !(parent1.Parent.Name == (XName) "CatalogClass") ? (XElement) null : parent1.Parent;
        if (parent2 != null)
        {
          XElement xelement = parent2.Element((XName) "NodeID");
          if (xelement != null && hierarchyItemAdveon != null && xelement.Value != string.Empty && hierarchyItemAdveon.ParentId == string.Empty)
            hierarchyItemAdveon.ParentId = xelement.Value;
        }
      }
    }
    return dictionary.Values.ToArray<HierarchyItemAdveon>();
  }
}
