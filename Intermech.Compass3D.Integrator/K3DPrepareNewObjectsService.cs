// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DPrepareNewObjectsService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.CADInterface.Proxies;
using Intermech.Client.Core;
using Intermech.Data;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DPrepareNewObjectsService(IIntegrator owner) : CADPrepareNewObjectsService(owner)
{
  public override void PrepareNewObject(long objectId)
  {
    base.PrepareNewObject(objectId);
    this.InitializeDocumentCode(objectId);
  }

  private void InitializeDocumentCode(long objectId)
  {
    int objectType = DBHelper.GetObjectType(objectId);
    string docCode = DocumentDesignationHelper.GetDocCode(objectType);
    if (string.IsNullOrEmpty(docCode))
      return;
    string masterFileName = this.FileVault.DBFilesInfo.GetMasterFileName(objectId, false);
    if (string.IsNullOrEmpty(masterFileName))
      return;
    string fullName = this.FileVault.PublishTree(objectId, masterFileName, VersionsRuleSources.GetEditorRule(), (IFileArea) this.FileVault.WorkArea);
    DocumentAttributesOptions.GetDecodeOptions(objectType);
    EncodeAttributesOptions encodeOptions = DocumentAttributesOptions.GetEncodeOptions(objectType);
    StringKey[] attributeKeys = new StringKey[1]
    {
      (StringKey) CADVirtualAttributes.DocumentCode
    };
    ICADInterfaceService service = ServiceUtils.GetService<ICADInterfaceService>((object) this.Integrator, true);
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) service))
    {
      CADDocumentProxy document = cadApiSession.Application.OpenDocument(fullName, false);
      IAttributeCodec documentCodec = service.GetDocumentCodec(document);
      IValueBagContainer attributeContainer = service.GetDocumentAttributeContainer(document);
      ContainerValues containerValues = documentCodec.ReadFileProperties(attributeContainer, (ICollection<StringKey>) attributeKeys);
      ValueBag attributes = new ValueBag();
      attributes.TryUpdate(attributeKeys[0], (object) docCode, containerValues.IsOpenMetadata);
      if (!attributes.HasChanges)
        return;
      documentCodec.Encode(new EncodeAttributesParams(attributeContainer, (ICollection<StringKey>) attributes.GetChangedItemsKeys(), attributes, containerValues, encodeOptions)
      {
        ContainerDisplayName = Path.GetFileName(document.FullName)
      });
      documentCodec.Formatter.Write(attributeContainer, containerValues);
      if (!document.Modified || document.ReadOnly)
        return;
      document.Save();
    }
  }
}
