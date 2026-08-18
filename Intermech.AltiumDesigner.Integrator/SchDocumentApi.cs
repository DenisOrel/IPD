// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.SchDocumentApi
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class SchDocumentApi(
  ADMechanicalDriver driver,
  CaptureChangesDriverContext driverContext) : ADDocumentApi(driver, driverContext)
{
  public override List<LocalId<int>> DetectNewDocumentType(SectionEntity docItem)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    List<LocalId<int>> localIdList = new List<LocalId<int>>();
    List<ValueRecord> itemsList = this.documentAttributeCodec.ReadFileProperties(this.GetBagContainer(docItem), (ICollection<StringKey>) new StringKey[1]
    {
      new StringKey(IDCache.Default.Designation.Text)
    }).Bag.GetItemsList();
    if (itemsList != null && itemsList.Count > 0 && itemsList[0].Value != null)
    {
      Guid electricalSchemaType = DocumentHelper.GetElectricalSchemaType((string) itemsList[0].Value);
      if (electricalSchemaType != Guid.Empty)
      {
        IMSObjectType type = MetaDataHelper.GetObjectType(electricalSchemaType);
        bool flag = true;
        if (this.driver.IntegratorSettings.SchemaDocumentTypes.Count > 0 && !this.driver.IntegratorSettings.SchemaDocumentTypes.Exists((Predicate<GlobalId<int>>) (x => x.Id == type.ObjectTypeID)))
          flag = false;
        if (flag)
        {
          localIdList.Add(new LocalId<int>(type.ObjectTypeID, type.ObjectTypeName));
          return localIdList;
        }
      }
    }
    localIdList.AddRange((IEnumerable<LocalId<int>>) this.driver.IntegratorSettings.SchemaDocumentTypes);
    return localIdList;
  }

  protected override IValueBagContainer GetBagContainer(SectionEntity docItem)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    return (IValueBagContainer) new ParametersContainer((IParametrable) ApiHelper.GetSchDocument(docItem.Sections.Get<AddInProxy>().AddIn, docItem.Sections.Get<FilesSection>().MasterFile, true));
  }

  protected override List<string> OnGetSatelliteFiles(
    SectionEntity docItem,
    AddInProxy proxy,
    FilesSection file)
  {
    List<string> satelliteFiles = docItem.Sections.Get<List<string>>((List<string>) null);
    if (satelliteFiles != null)
      return satelliteFiles;
    SchDocumentProxy schDocumentProxy = new SchDocumentProxy(ApiHelper.GetSchDocument(proxy.AddIn, file.MasterFile, true), file.MasterFile, this.settingsSvc.GetSettings());
    return schDocumentProxy.SheetNumber != 1 ? new List<string>(0) : ApiHelper.GetAdditionalSheets(schDocumentProxy.Project, schDocumentProxy.Name);
  }

  protected override IFileDependenciesHandler OnTryGetFileDependenciesHandler(
    SectionEntity docItem,
    AddInProxy proxy,
    FilesSection file)
  {
    return (IFileDependenciesHandler) new MechanicalFileDependenciesHandler((MechanicalDriver) this.driver, this.driverContext, ClientContext.FileVault);
  }

  protected override IAttributeCodec documentAttributeCodec => this.apiSvc.SchemaDocumentCodec;

  protected override ISynchronizedObjectAttributes documentAttributes
  {
    get => this.settingsSvc.SynchronizedDocumentAttributes;
  }
}
