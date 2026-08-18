// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.AuxiliaryDocumentTypeSelectorBase
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CacheServices;
using Intermech.Client.Core;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators;

public abstract class AuxiliaryDocumentTypeSelectorBase : IDocumentTypeSelector
{
  protected readonly bool allowOtherDocumentsType;

  protected AuxiliaryDocumentTypeSelectorBase(bool allowOtherDocumentsType)
  {
    this.allowOtherDocumentsType = allowOtherDocumentsType;
  }

  public SelectedObjectType SelectDocumentType(SectionEntity docItem)
  {
    int[] possibleObjectTypes = docItem != null ? this.GetPossibleDocumentTypes(docItem) : throw new ArgumentNullException(nameof (docItem));
    bool flag = !FileVars.SoftMode.Value;
    if (possibleObjectTypes.Length != 0)
    {
      if (possibleObjectTypes.Length == 1)
        return new SelectedObjectType(possibleObjectTypes[0], false);
      if (!flag)
        return new SelectedObjectType(possibleObjectTypes[0], true);
      FileNameBasedTypeSelector basedTypeSelector = new FileNameBasedTypeSelector();
      basedTypeSelector.OnVisualSelect += (EventHandler<SelectObjectTypeArgs>) ((sender, e) =>
      {
        SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Tools.Components_460"), typeof (ObjectTypeFolder), false);
        selectorForm.SelectorFilter = (ISelectorFilter) new AuxiliaryDocumentTypeSelectorBase.SelectorFilter(possibleObjectTypes);
        if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count != 1)
          return;
        e.ObjectType = (LocalId<int>) DBHelper.CreateObjectTypeGID((int) selectorForm.IDList[0]);
      });
      return new SelectedObjectType(basedTypeSelector.Select(FilesSection.GetMasterFile(docItem)).Id, false);
    }
    if (flag)
    {
      FileNameBasedTypeSelector basedTypeSelector = new FileNameBasedTypeSelector();
      basedTypeSelector.OnVisualSelect += (EventHandler<SelectObjectTypeArgs>) ((sender, e) =>
      {
        SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Tools.Components_460"), typeof (ObjectTypeFolder), false);
        selectorForm.SelectorFilter = (ISelectorFilter) new AuxiliaryDocumentTypeSelectorBase.ObjectTypesWithFiles();
        if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count != 1)
          return;
        e.ObjectType = (LocalId<int>) DBHelper.CreateObjectTypeGID((int) selectorForm.IDList[0]);
      });
      return new SelectedObjectType(basedTypeSelector.Select(FilesSection.GetMasterFile(docItem)).Id, false);
    }
    if (this.allowOtherDocumentsType)
      return new SelectedObjectType(IDCache.Default.OtherDocuments.Id, true);
    throw new NotSupportedException();
  }

  protected abstract int[] GetPossibleDocumentTypes(SectionEntity docItem);

  private sealed class ObjectTypesWithFiles : ISelectorFilter
  {
    private int fileAttrId;

    public ObjectTypesWithFiles()
    {
      this.fileAttrId = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).FileAttributeID;
    }

    public bool IsInFilter(int category, object id)
    {
      return MetaDataHelper.GetAttribute4ObjectType((int) id, this.fileAttrId) != null;
    }
  }

  private sealed class SelectorFilter : ISelectorFilter
  {
    private List<int> validIds;

    public SelectorFilter(int[] objTypeIds)
    {
      IObjectTypeHierarchy service = (IObjectTypeHierarchy) ServiceUtils.GetService<ICacheServices>((object) ServicesManager.ServiceContainer, true).GetService("ObjectTypeHierarchy");
      this.validIds = new List<int>((IEnumerable<int>) objTypeIds);
      for (int index1 = 0; index1 < objTypeIds.Length; ++index1)
      {
        int[] parentTypes = service.GetParentTypes(objTypeIds[index1]);
        for (int index2 = 0; index2 < parentTypes.Length; ++index2)
        {
          if (!this.validIds.Contains(parentTypes[index2]))
            this.validIds.Add(parentTypes[index2]);
        }
      }
    }

    public bool IsInFilter(int category, object id) => this.validIds.Contains((int) id);
  }
}
