// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CAD.AIDocumentParametersPreparer
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Data;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.CAD;

internal sealed class AIDocumentParametersPreparer
{
  private ICADInterfaceService cadInterfaceService;

  public AIDocumentParametersPreparer(ICADInterfaceService cadInterfaceService)
  {
    this.cadInterfaceService = cadInterfaceService != null ? cadInterfaceService : throw new ArgumentNullException(nameof (cadInterfaceService));
  }

  public void PrepareDocumentParametersToWrite(
    CopyingSession session,
    DBObjectGraphVertex dbObjectVertex,
    CADVirtualParametersContainerSet virtualContainerSet)
  {
    if (dbObjectVertex.Content.Tag != DBObjectContentTag.CADModel)
      return;
    CADModelContent documentContent = dbObjectVertex.Content.AsCADModel();
    CADConfigurationTable configurationTable = documentContent.ConfigurationTable;
    CADConfigurationTableRow row = configurationTable.Rows[0];
    if (configurationTable.Rows.Count == 1 && this.IsSingleNamelessVirtualConfiguration(row))
    {
      this.MergeSingleConfigurationWithDocument(dbObjectVertex, row, virtualContainerSet);
    }
    else
    {
      CADConfigurationTableRow overlayConfigurationRow = configurationTable.Rows.First<CADConfigurationTableRow>((Func<CADConfigurationTableRow, bool>) (x => x.Name == documentContent.DefaultConfigurationName));
      this.MergeOverlayConfigurationWithDocument(dbObjectVertex, overlayConfigurationRow, virtualContainerSet);
    }
  }

  private bool IsSingleNamelessVirtualConfiguration(CADConfigurationTableRow modelConfigurationRow)
  {
    return this.cadInterfaceService.GetArticleRawConfigurationName(modelConfigurationRow.MasterPath, modelConfigurationRow.Name) == string.Empty;
  }

  private void MergeOverlayConfigurationWithDocument(
    DBObjectGraphVertex dbObjectVertex,
    CADConfigurationTableRow overlayConfigurationRow,
    CADVirtualParametersContainerSet virtualContainerSet)
  {
    CADVirtualParametersContainer configurationContainer = virtualContainerSet.GetOrCreateConfigurationContainer(overlayConfigurationRow.Name);
    CADVirtualParametersContainer documentContainer = virtualContainerSet.GetOrCreateDocumentContainer();
    foreach (ValueRecord valueRecord1 in configurationContainer.ValueBag)
    {
      if (this.IsNullOrEmptyString(valueRecord1))
      {
        ValueRecord valueRecord2 = documentContainer.ValueBag.Find(valueRecord1.Key);
        if (valueRecord2 != null && valueRecord2.DataType == valueRecord1.DataType && !this.IsNullOrEmptyString(valueRecord2))
          valueRecord1.Value = valueRecord2.Value;
      }
    }
  }

  private bool IsNullOrEmptyString(ValueRecord valueRecord)
  {
    if (valueRecord.IsNull)
      return true;
    return valueRecord.DataType == typeof (string) && object.Equals(valueRecord.Value, (object) string.Empty);
  }

  private void MergeSingleConfigurationWithDocument(
    DBObjectGraphVertex dbObjectVertex,
    CADConfigurationTableRow singleConfigurationRow,
    CADVirtualParametersContainerSet virtualContainerSet)
  {
    CADVirtualParametersContainer configurationContainer = virtualContainerSet.GetOrCreateConfigurationContainer(singleConfigurationRow.Name);
    CADVirtualParametersContainer documentContainer = virtualContainerSet.GetOrCreateDocumentContainer();
    foreach (ValueRecord valueRecord1 in configurationContainer.ValueBag)
    {
      if (!valueRecord1.IsNull)
      {
        ValueRecord valueRecord2 = documentContainer.ValueBag.Find(valueRecord1.Key);
        if (valueRecord2 == null || valueRecord2.IsNull)
          documentContainer.ValueBag.TryUpdate(valueRecord1.Key, valueRecord1.Value, true);
      }
    }
    configurationContainer.ValueBag.Clear();
  }
}
