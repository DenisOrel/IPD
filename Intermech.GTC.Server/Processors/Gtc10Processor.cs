// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.Gtc10Processor
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

public class Gtc10Processor : GtcProcessor
{
  private const string ClassHierarchy = "class_hierarchy";
  private const string Step_3DModels = "step_3d_models";
  private const string StepP21Products = "step_p21_products";
  private const string AssortmentTxt = "assortment.txt";
  private const string CatalogInfoTxt = "catalog_info.txt";
  private const string ClassHierarchyPlmxml = "class_hierarchy.plmxml";
  private const string StepP21MappingTxt = "step_p21_mapping.txt";

  private Gtc10Processor()
  {
    this.FileFolderPathHolder.SetData(new FileFolderItem[7]
    {
      new FileFolderItem(FileFolder.Folder, "class_hierarchy", PackageType.Hierarchy),
      new FileFolderItem(FileFolder.Folder, "step_3d_models", PackageType.Data, true),
      new FileFolderItem(FileFolder.Folder, "step_p21_products", PackageType.Data),
      new FileFolderItem(FileFolder.File, "assortment.txt", PackageType.Data),
      new FileFolderItem(FileFolder.File, "catalog_info.txt", PackageType.Hierarchy),
      new FileFolderItem(FileFolder.File, "class_hierarchy.plmxml", PackageType.Hierarchy),
      new FileFolderItem(FileFolder.File, "step_p21_mapping.txt", PackageType.Data)
    });
  }

  public Gtc10Processor(
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
    string itemByName = this.FileFolderPathHolder.GetItemByName("class_hierarchy.plmxml");
    XDocument xdocument = File.Exists(itemByName) ? XDocument.Load(itemByName) : throw new Exception($"File not found {itemByName}");
    XNamespace ns = (XNamespace) "http://www.plmxml.org/Schemas/PLMXMLClassificationSchema";
    HierarchyItem[] array1 = xdocument.Descendants().Where<XElement>((Func<XElement, bool>) (x => x.Name == ns + "AdminClass")).Select<XElement, HierarchyItem>((Func<XElement, HierarchyItem>) (x => new HierarchyItem(x))).ToArray<HierarchyItem>();
    this.Task.CountElements = array1.Length;
    Dictionary<string, HierarchyDescriptor> dictionary = new Dictionary<string, HierarchyDescriptor>();
    foreach (HierarchyItem hierarchyItem in array1)
    {
      Tuple<string, string>[] array2 = ((IEnumerable<Tuple<string, string>>) hierarchyItem.Files).Select<Tuple<string, string>, Tuple<string, string>>((Func<Tuple<string, string>, Tuple<string, string>>) (x => new Tuple<string, string>(ZipExtractor.ExtractFile(Path.Combine(this.RootPath, x.Item1)), x.Item2))).Where<Tuple<string, string>>((Func<Tuple<string, string>, bool>) (x => File.Exists(x.Item1))).Distinct<Tuple<string, string>>((IEqualityComparer<Tuple<string, string>>) new FileInfoComparer()).ToArray<Tuple<string, string>>();
      HierarchyDescriptor hierarchyDescriptor = array2.Length != 0 ? new HierarchyDescriptor(hierarchyItem.Name, hierarchyItem.ClassId, array2) : new HierarchyDescriptor(hierarchyItem.Name, hierarchyItem.ClassId);
      dictionary.Add(hierarchyItem.ClassId, hierarchyDescriptor);
    }
    foreach (HierarchyItem hierarchyItem in array1)
    {
      HierarchyDescriptor hierarchyDescriptor1;
      if (dictionary.TryGetValue(hierarchyItem.ClassId, out hierarchyDescriptor1))
      {
        HierarchyDescriptor hierarchyDescriptor2;
        if (dictionary.TryGetValue(hierarchyItem.Parent, out hierarchyDescriptor2))
          hierarchyDescriptor2.Children.Add(hierarchyDescriptor1);
        else
          this.RootHierarchyDescriptor.Children.Add(hierarchyDescriptor1);
      }
    }
  }

  protected override void PopulateListDescriptorItems()
  {
    base.PopulateListDescriptorItems();
    string itemByName = this.FileFolderPathHolder.GetItemByName("assortment.txt");
    List<string> source = File.Exists(itemByName) ? ((IEnumerable<string>) File.ReadAllLines(itemByName)).ToList<string>() : throw new Exception($"File not found {itemByName}");
    source.RemoveAt(0);
    DataItem[] array = source.Select<string, DataItem>((Func<string, DataItem>) (x => new DataItem(x.Split('\t')[0], x.Split('\t')[1], x.Split('\t')[5]))).ToArray<DataItem>();
    this.Task.CountElements = array.Length;
    foreach (DataItem dataItem in array)
    {
      if (this.IsProcessStopping)
        throw new StopTaskException();
      try
      {
        string file = ZipExtractor.ExtractFile(Path.Combine(this.FileFolderPathHolder.GetItemByName("step_p21_products"), dataItem.P21FileName));
        if (File.Exists(file))
          this.ListDescriptorItems.Add(new ListItemDescriptor(file, dataItem.GtcClassId));
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
}
