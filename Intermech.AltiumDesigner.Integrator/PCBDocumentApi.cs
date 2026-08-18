// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.PCBDocumentApi
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class PCBDocumentApi : ADDocumentApi
{
  private readonly ADMechanicalDriver _driver;
  private readonly CaptureChangesDriverContext _driverContext;

  public PCBDocumentApi(ADMechanicalDriver driver, CaptureChangesDriverContext driverContext)
    : base(driver, driverContext)
  {
    this._driver = driver ?? throw new ArgumentNullException(nameof (driver));
    this._driverContext = driverContext ?? throw new ArgumentNullException(nameof (driverContext));
  }

  protected override IAttributeCodec documentAttributeCodec => this.apiSvc.PCBDocCodec;

  protected override ISynchronizedObjectAttributes documentAttributes
  {
    get => this.settingsSvc.SynchronizedDocumentAttributes;
  }

  public override List<LocalId<int>> DetectNewDocumentType(SectionEntity docItem)
  {
    List<LocalId<int>> localIdList = new List<LocalId<int>>();
    if (this.driver.IntegratorSettings.PCBDocumentTypes != null && this.driver.IntegratorSettings.PCBDocumentTypes.Count > 0)
      localIdList.AddRange((IEnumerable<LocalId<int>>) this.driver.IntegratorSettings.PCBDocumentTypes);
    return localIdList;
  }

  public override void EncodeDocumentAttributes(
    SectionEntity docItem,
    ICollection<StringKey> attributeKeys,
    ValueBag attributes,
    ContainerValues fileProperties)
  {
  }

  public override ValueBag DecodeDocumentAttributes(
    SectionEntity docItem,
    ContainerValues fileProperties)
  {
    return ObjectSection.IsNewObject(docItem) ? new ValueBag() : this.driver.Operations.Db.ReadObjectAttributes(docItem, (IDBAttributableTypeRef) new DirectObjectAttributesRef(ObjectSection.GetObjectType(docItem)));
  }

  public override ContainerValues ReadDocumentProperties(SectionEntity docItem)
  {
    return new ContainerValues(new ValueBag(), false);
  }

  public override bool WriteDocumentProperties(
    SectionEntity docItem,
    ContainerValues fileProperties)
  {
    return false;
  }

  protected override IValueBagContainer GetBagContainer(SectionEntity docItem)
  {
    return (IValueBagContainer) new EmptyBagContainer();
  }

  protected override List<string> OnGetSatelliteFiles(
    SectionEntity docItem,
    AddInProxy proxy,
    FilesSection file)
  {
    ADIntegratorSettings settings = this.settingsSvc.GetSettings();
    if (string.IsNullOrEmpty(settings.GerberFiles))
      return new List<string>(0);
    string projectFile = this.GetProjectFile(Path.GetDirectoryName(file.MasterFile));
    if (string.IsNullOrEmpty(projectFile))
      return new List<string>(0);
    FileTypeService service = ServiceUtils.GetService<FileTypeService>((object) this.driver.Integrator, true);
    IADProject project = ApiHelper.GetProject(proxy.AddIn, projectFile);
    using (ADClientSponsor adClientSponsor = new ADClientSponsor())
    {
      adClientSponsor.Register((object) project);
      return AdditionalFiles.GetGerberFiles(service, project, settings.GerberFiles.Split(','), true);
    }
  }

  private string GetProjectFile(string levelFolder)
  {
    string[] files = Directory.GetFiles(levelFolder, $"*{".PrjPcb"}", SearchOption.TopDirectoryOnly);
    if (files.Length == 1)
      return files[0];
    return Directory.GetParent(levelFolder) == null ? (string) null : this.GetProjectFile(Directory.GetParent(levelFolder).FullName);
  }

  protected override IFileDependenciesHandler OnTryGetFileDependenciesHandler(
    SectionEntity docItem,
    AddInProxy proxy,
    FilesSection file)
  {
    return (IFileDependenciesHandler) new MechanicalFileDependenciesHandler((MechanicalDriver) this.driver, this.driverContext, ClientContext.FileVault);
  }
}
