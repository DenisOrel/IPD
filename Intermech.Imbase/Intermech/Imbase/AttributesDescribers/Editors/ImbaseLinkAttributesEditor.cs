// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.Editors.ImbaseLinkAttributesEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core.PropertyEditors;
using Intermech.DataFormats;
using Intermech.Imbase.Commands;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers.Editors;

internal class ImbaseLinkAttributesEditor : UITypeEditor
{
  private readonly int _attributeId;

  private static List<long> GetImbaseCatalogs(
    int objTypeId,
    int attrTypeId,
    out ImbaseCatalogSelectMode selectMode)
  {
    ImbaseExtendedItem imbaseExtendedItem = ExtendedServiceHelper.GetObjTypeData(objTypeId, (IUserSession) null)?.GetValue(attrTypeId, (IUserSession) null);
    selectMode = imbaseExtendedItem != null ? imbaseExtendedItem.SelectMode : ImbaseCatalogSelectMode.imcmNone;
    return imbaseExtendedItem == null ? new List<long>() : imbaseExtendedItem.CatalogIDs;
  }

  public ImbaseLinkAttributesEditor(int attributeId) => this._attributeId = attributeId;

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    AttributablePropertyClass attributablePropertyClass = value as AttributablePropertyClass;
    List<long> catalogIDs = (List<long>) null;
    IElementInfo elementInfo = attributablePropertyClass?.ElementInfo;
    IMSAttribute4 imsAttribute4 = (IMSAttribute4) null;
    if (elementInfo != null && elementInfo.ElementKind == AttributableElements.Object)
    {
      int objectTypeId;
      if (elementInfo is IDBObjectTypeID dbObjectTypeId)
      {
        objectTypeId = dbObjectTypeId.Value;
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          objectTypeId = sessionKeeper.Session.GetObjectInfo(elementInfo.ElementIdentifier).ObjectTypeID;
      }
      ImbaseCatalogSelectMode selectMode;
      catalogIDs = ImbaseLinkAttributesEditor.GetImbaseCatalogs(objectTypeId, attributablePropertyClass.AttributeId, out selectMode);
      if (selectMode == ImbaseCatalogSelectMode.imcmNone)
        catalogIDs = (List<long>) null;
      imsAttribute4 = (IMSAttribute4) MetaDataHelper.GetAttribute4ObjectType(objectTypeId, Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID);
    }
    ObjectPropDescriptorHolder instance = context != null ? context.Instance as ObjectPropDescriptorHolder : (ObjectPropDescriptorHolder) null;
    SimplePropDescriptor aPD = (SimplePropDescriptor) null;
    if (instance != null)
      aPD = instance.GetPropDescriptorByID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID) as SimplePropDescriptor;
    long num = 0;
    if (aPD?.AttributeValuePropertyClass != null)
    {
      object avValue = AttributeValuesEditor.GetAVValue((PropDescriptor) aPD, aPD.AttributeValuePropertyClass.AttributeValue, (object) instance);
      if (avValue != null)
      {
        try
        {
          num = Convert.ToInt64(avValue);
        }
        catch (Exception ex)
        {
        }
      }
    }
    // ISSUE: explicit non-virtual call
    if (catalogIDs != null && __nonvirtual (catalogIDs.Count) > 0)
    {
      IImbaseFilterSelector service = ServiceUtils.GetService<IImbaseFilterSelector>((object) ApplicationServices.Container, false);
      if (service != null)
      {
        service.RecordID = num;
        ImbaseCatalogSelectMode mode = imsAttribute4 != null ? ImbaseCatalogSelectMode.imcmAllowSelectRow : ImbaseCatalogSelectMode.imcmSelectFolder;
        long objectId = service.SelectImbaseObject(catalogIDs, (int[]) null, 0L, attributablePropertyClass.ObjectID, mode);
        if (objectId == 0L)
          return value;
        if (instance != null && imsAttribute4 != null && num != service.RecordID)
        {
          object recordId = (object) service.RecordID;
          if (service.RecordID == 0L && (imsAttribute4.Options & AttributeOptions.DisableNulls) == AttributeOptions.None)
            recordId = (object) DBNull.Value;
          if (aPD == null && elementInfo != null)
          {
            AttributeValues attributeValues = AttributeProcessor.CreateAttributeValues(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID, elementInfo.ElementIdentifier, elementInfo.ElementKind);
            instance.AddProperty(new AttributeValues[1]
            {
              attributeValues
            }, out bool _);
            aPD = instance.GetPropDescriptorByID(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID) as SimplePropDescriptor;
          }
          if (aPD?.AttributeValuePropertyClass != null)
          {
            AttributeValues attributeValue = aPD.AttributeValuePropertyClass.AttributeValue;
            attributeValue.Values = new object[1]
            {
              recordId
            };
            object pdValue = AttributeValuesEditor.GetPDValue(attributeValue, 0, -1L, AttributableElements.None, string.Empty, (DataTable) null);
            aPD.SetValue(aPD.Component, pdValue);
            aPD.ValueChanged = true;
          }
        }
        return (object) new AttributablePropertyClass(attributablePropertyClass.ElementInfo, attributablePropertyClass.AttributeId, objectId);
      }
    }
    ImbaseSelectFromTreeAnalyzer analyzer = new ImbaseSelectFromTreeAnalyzer(new List<int>((IEnumerable<int>) Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS));
    Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) analyzer, true);
    Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Imbase.Client_87"), string.Empty, (IDescriptor) new ImbaseRootNodeDescriptor(), SelectionOptions.Default);
    return analyzer.TreeSelectedItems == null || analyzer.TreeSelectedItems.Count == 0 || !(analyzer.TreeSelectedItems.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData) ? value : (object) new AttributablePropertyClass(attributablePropertyClass?.ElementInfo, attributablePropertyClass != null ? attributablePropertyClass.AttributeId : this._attributeId, itemData.Value, itemData.Caption);
  }
}
