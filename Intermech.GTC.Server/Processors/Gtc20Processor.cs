// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.Gtc20Processor
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

public class Gtc20Processor : GtcProcessor
{
  private const string ProductDataFiles = "product_data_files";
  private const string Product_3DModelsDetailed = "product_3d_models_detailed";
  private const string Product_3DModelsBasic = "product_3d_models_basic";
  private const string ProductPictures = "product_pictures";
  private const string Product_2DDrawings = "product_2d_drawings";
  private const string ProductFamilyDrawings = "product_family_drawings";
  private const string ClassDrawings = "class_drawings";
  private const string ClassIcons = "class_icons";
  private const string ProductBrandLogos = "product_brand_logos";
  private const string Disclaimer = "disclaimer";
  private const string PackageAssortmentXml = "package_assortment.xml";
  private const string PackageSubsetAssortmentXml = "package_subset_assortment.xml";
  private const string GtcClassHierarchyVendorXml = "gtc_class_hierarchy_vendor.xml";
  private const string PackageMetaDataXml = "package_meta_data.xml";
  private const string PackageLogoJpg = "package_logo.jpg";
  private const string ReadmeTxt = "readme.txt";

  private Gtc20Processor()
  {
    this.FileFolderPathHolder.SetData(new FileFolderItem[13]
    {
      new FileFolderItem(FileFolder.Folder, "product_data_files", PackageType.Data),
      new FileFolderItem(FileFolder.Folder, "product_3d_models_detailed", PackageType.Data, true),
      new FileFolderItem(FileFolder.Folder, "product_3d_models_basic", PackageType.Data, true),
      new FileFolderItem(FileFolder.Folder, "product_pictures", PackageType.Data, true),
      new FileFolderItem(FileFolder.Folder, "product_2d_drawings", PackageType.Data, true),
      new FileFolderItem(FileFolder.Folder, "product_family_drawings", PackageType.Data, true),
      new FileFolderItem(FileFolder.Folder, "class_drawings", PackageType.Hierarchy),
      new FileFolderItem(FileFolder.Folder, "class_icons", PackageType.Hierarchy),
      new FileFolderItem(FileFolder.Folder, "product_brand_logos", PackageType.Data, true),
      new FileFolderItem(FileFolder.File, "package_assortment.xml", PackageType.Data),
      new FileFolderItem(FileFolder.File, "package_subset_assortment.xml", PackageType.Data),
      new FileFolderItem(FileFolder.File, "gtc_class_hierarchy_vendor.xml", PackageType.Hierarchy),
      new FileFolderItem(FileFolder.File, "package_meta_data.xml", PackageType.Hierarchy)
    });
  }

  public Gtc20Processor(
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
    string itemByName = this.FileFolderPathHolder.GetItemByName("gtc_class_hierarchy_vendor.xml");
    GtcClassHierarchy[] gtcClassHierarchyArray = File.Exists(itemByName) ? XDocument.Load(itemByName).Descendants((XName) "gtc_class").Select<XElement, GtcClassHierarchy>((Func<XElement, GtcClassHierarchy>) (x => new GtcClassHierarchy(x))).ToArray<GtcClassHierarchy>() : throw new Exception($"File not found {itemByName}");
    this.Task.CountElements = gtcClassHierarchyArray.Length;
    Dictionary<string, HierarchyDescriptor> dictionary = new Dictionary<string, HierarchyDescriptor>();
    foreach (GtcClassHierarchy gtcClassHierarchy in gtcClassHierarchyArray)
    {
      HierarchyDescriptor hierarchyDescriptor = new HierarchyDescriptor(gtcClassHierarchy.NodeName, gtcClassHierarchy.Id);
      dictionary.Add(gtcClassHierarchy.Id, hierarchyDescriptor);
    }
    foreach (GtcClassHierarchy gtcClassHierarchy in gtcClassHierarchyArray)
    {
      HierarchyDescriptor hierarchyDescriptor1;
      if (dictionary.TryGetValue(gtcClassHierarchy.Id, out hierarchyDescriptor1))
      {
        HierarchyDescriptor hierarchyDescriptor2;
        if (dictionary.TryGetValue(gtcClassHierarchy.ParentId, out hierarchyDescriptor2))
          hierarchyDescriptor2.Children.Add(hierarchyDescriptor1);
        else
          this.RootHierarchyDescriptor.Children.Add(hierarchyDescriptor1);
      }
    }
  }

  protected override void PopulateListDescriptorItems()
  {
    base.PopulateListDescriptorItems();
    string itemByName = this.FileFolderPathHolder.GetItemByName("package_assortment.xml");
    GtcItem[] gtcItemArray = File.Exists(itemByName) ? XDocument.Load(itemByName).Descendants((XName) "item").Select<XElement, GtcItem>((Func<XElement, GtcItem>) (x => new GtcItem(x))).ToArray<GtcItem>() : throw new Exception($"File not found {itemByName}");
    this.Task.CountElements = gtcItemArray.Length;
    foreach (GtcItem gtcItem in gtcItemArray)
    {
      if (this.IsProcessStopping)
        throw new StopTaskException();
      try
      {
        string aFilePath = string.Empty;
        string file1 = ZipExtractor.ExtractFile(Path.Combine(this.RootPath, gtcItem.P21FileUrl));
        if (gtcItem.P21FileUrl != string.Empty && File.Exists(file1))
        {
          aFilePath = file1;
        }
        else
        {
          string file2 = ZipExtractor.ExtractFile(Path.Combine(this.FileFolderPathHolder.GetItemByName("product_data_files"), gtcItem.P21FileName));
          if (gtcItem.P21FileName != string.Empty && File.Exists(file2))
            aFilePath = file2;
        }
        if (aFilePath != string.Empty)
          this.ListDescriptorItems.Add(new ListItemDescriptor(aFilePath, gtcItem.GtcGenericClassId));
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
