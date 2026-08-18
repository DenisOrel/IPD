// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DocumentOperations
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators;

public sealed class DocumentOperations
{
  public ICollection<StringKey> GetIdentityKeys()
  {
    return (ICollection<StringKey>) new StringKey[2]
    {
      (StringKey) IDCache.Default.Designation.Text,
      (StringKey) IDCache.Default.Name.Text
    };
  }

  public DecodeAttributesOptions GetDecodeOptions(SectionEntity docItem)
  {
    return docItem != null ? DocumentAttributesOptions.GetDecodeOptions(ObjectSection.TryGetObjectType(docItem)) : throw new ArgumentNullException(nameof (docItem));
  }

  public EncodeAttributesOptions GetEncodeOptions(SectionEntity docItem)
  {
    return docItem != null ? DocumentAttributesOptions.GetEncodeOptions(ObjectSection.GetObjectType(docItem)) : throw new ArgumentNullException(nameof (docItem));
  }

  public LocalId<int> SelectDocumentType(
    SectionEntity docItem,
    ICollection<LocalId<int>> possibleTypes)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    if (possibleTypes == null)
      throw new ArgumentNullException(nameof (possibleTypes));
    string masterFile = FilesSection.GetMasterFile(docItem);
    FileNameBasedTypeSelector basedTypeSelector = new FileNameBasedTypeSelector();
    basedTypeSelector.TypeFilter = (IEnumerable<LocalId<int>>) possibleTypes;
    basedTypeSelector.OnVisualSelect += (EventHandler<SelectObjectTypeArgs>) ((sender, e) =>
    {
      if (possibleTypes.Count == 0)
        throw new FaultException(LocalizationHolder.rm.GetString("SR_521"));
      SelectItemForm selectItemForm = new SelectItemForm();
      selectItemForm.Text = LocalizationHolder.rm.GetString("Tools.Components_341");
      selectItemForm.Description = string.Format(LocalizationHolder.rm.GetString("Attribute.Tools.Components_9"), (object) masterFile);
      selectItemForm.Items = (IEnumerable) possibleTypes;
      if (selectItemForm.ShowDialog() != DialogResult.OK)
        return;
      e.ObjectType = (LocalId<int>) selectItemForm.SelectedItem;
    });
    return basedTypeSelector.Select(masterFile);
  }

  public bool GetFilesProcessingFlag(SectionEntity documentEntity)
  {
    FilesProcessingOptionsSection processingOptionsSection = documentEntity != null ? documentEntity.Sections.Get<FilesProcessingOptionsSection>((FilesProcessingOptionsSection) null) : throw new ArgumentNullException(nameof (documentEntity));
    return processingOptionsSection == null || processingOptionsSection.EnableFilesProcessing;
  }

  public bool GetDependenciesProcessingFlag(SectionEntity documentEntity)
  {
    FilesProcessingOptionsSection processingOptionsSection = documentEntity != null ? documentEntity.Sections.Get<FilesProcessingOptionsSection>((FilesProcessingOptionsSection) null) : throw new ArgumentNullException(nameof (documentEntity));
    return processingOptionsSection == null || processingOptionsSection.EnableDependenciesProcessing;
  }
}
