// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.CategoryProp.ImObj4AttrCatProps
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.ComponentModel;
using Intermech.DatabaseConfigurator;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.CategoryProp;

public class ImObj4AttrCatProps : ICategoryProps
{
  private PropDescriptor _imCatalogPropDescriptor;
  private PropDescriptor _imCatalogModePropDescriptor;
  private string _imCatalogPropDescriptorName = string.Empty;
  private string _imCatalogModePropDescriptorName = string.Empty;
  private ExtendedServiceHelper.ObjTypeInfo _objTypeInfo;
  private ImbaseExtendedItem _imExtItem;

  private void InitializeData()
  {
    this._imCatalogPropDescriptorName = LocalizationHolder.rm.GetString("Imbase.Client_144");
    this._imCatalogModePropDescriptorName = LocalizationHolder.rm.GetString("Imbase.Client_145");
  }

  private Attr4ObjInfo GetAttr4ObjInfo(
    PropDescriptorHolder propDescriptorHolder,
    int category,
    object id)
  {
    if (propDescriptorHolder == null)
      return (Attr4ObjInfo) null;
    List<int> intList = (List<int>) null;
    switch (category)
    {
      case 3:
        if (!(id is int))
          return (Attr4ObjInfo) null;
        if (propDescriptorHolder.PropDescriptorCollection[8].GetValue((object) propDescriptorHolder) is ObjectTypeMultiPropertyClass multiPropertyClass)
          intList = multiPropertyClass.ObjectTypeList;
        return new Attr4ObjInfo((int) id, -1, InheritModes.Public, ((PossibleValuesPropertyClass) propDescriptorHolder.PropDescriptorCollection[15].GetValue((object) propDescriptorHolder)).FieldType, intList?.ToArray());
      case 22:
        if (!(id is Attribute4ObjectTypeProperties objectTypeProperties))
          return (Attr4ObjInfo) null;
        ArrayList typeListByAttrId = ObjectEditor.GetObjTypeListByAttrId(objectTypeProperties.AttributeID);
        if (typeListByAttrId != null)
        {
          intList = new List<int>(typeListByAttrId.Count);
          foreach (object obj in typeListByAttrId)
          {
            int result;
            if (obj != null && int.TryParse(obj.ToString(), out result))
              intList.Add(result);
          }
        }
        FieldTypes fieldType = FieldTypes.ftUnknown;
        object obj1 = propDescriptorHolder.PropDescriptorCollection[8].GetValue((object) propDescriptorHolder);
        if (obj1 != null && obj1 is FieldTypePropertyClass)
          fieldType = ((FieldTypePropertyClass) obj1).FieldType;
        return new Attr4ObjInfo(objectTypeProperties.AttributeID, objectTypeProperties.ObjectType, ((InheritModePropertyClass) propDescriptorHolder.PropDescriptorCollection[0].GetValue((object) propDescriptorHolder)).InheritMode, fieldType, intList?.ToArray());
      default:
        return (Attr4ObjInfo) null;
    }
  }

  private FieldTypes GetFieldType(Attribute4ObjectTypeProperties attr4ObjType)
  {
    FieldTypes fieldType = FieldTypes.ftUnknown;
    if (attr4ObjType.FieldType == FieldTypes.ftUnknown)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attr4ObjType.AttributeID);
      if (attributeType != null)
        fieldType = attributeType.FieldType;
    }
    else
      fieldType = attr4ObjType.FieldType;
    return fieldType;
  }

  private int GetCommonObjTypeID(int[] objTypeIDs, out bool isValid)
  {
    isValid = true;
    List<int> enabledObjectTypes = MetaDataHelper.GetTopParentEnabledObjectTypes((IEnumerable<int>) new List<int>((IEnumerable<int>) objTypeIDs));
    foreach (int num in enabledObjectTypes)
    {
      if (num != Intermech.Imbase.Consts.ImbaseRootObjectTypeID && num != -1)
      {
        isValid = false;
        break;
      }
    }
    return enabledObjectTypes.Count != 1 ? -1 : enabledObjectTypes[0];
  }

  private PropDescriptor FindDescriptor(PropDescriptorHolder pdh, string descriptorName)
  {
    PropDescriptor descriptor = (PropDescriptor) null;
    if (pdh?.PropDescriptorCollection == null)
      return (PropDescriptor) null;
    foreach (PropDescriptor propDescriptor in pdh.PropDescriptorCollection)
    {
      if (propDescriptor != null && !(propDescriptor.DisplayName != descriptorName))
      {
        descriptor = propDescriptor;
        break;
      }
    }
    return descriptor;
  }

  private void FillDescriptors(Attr4ObjInfo attr4ObjInfo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._objTypeInfo = ExtendedServiceHelper.GetObjTypeData(attr4ObjInfo.ObjTypeID, sessionKeeper.Session);
      this._imExtItem = this._objTypeInfo != null ? this._objTypeInfo.GetValue(attr4ObjInfo.AttrTypeID, sessionKeeper.Session) : (ImbaseExtendedItem) null;
    }
    if (this._imExtItem == null)
      this._imExtItem = new ImbaseExtendedItem();
    if (this._imCatalogModePropDescriptor != null)
    {
      this._imCatalogModePropDescriptor.SetReadOnly(attr4ObjInfo.InheritMode == InheritModes.Inherited);
      this._imCatalogModePropDescriptor.SetValue(this._imCatalogModePropDescriptor.Component, (object) this._imExtItem.SelectMode);
    }
    if (this._imCatalogPropDescriptor == null)
      return;
    this.InvalidateCatalogDescriptor(attr4ObjInfo, this._imExtItem.SelectMode);
    this._imCatalogPropDescriptor.SetValue(this._imCatalogPropDescriptor.Component, (object) this._imExtItem.CatalogIDs);
  }

  private void FillVisibility(Attr4ObjInfo attr4ObjInfo)
  {
    if (attr4ObjInfo == null)
      return;
    FieldTypes fieldType = attr4ObjInfo.FieldType;
    this._imCatalogPropDescriptor.SetBrowsable(fieldType == FieldTypes.ftObjectLink);
    this._imCatalogModePropDescriptor.SetBrowsable(fieldType == FieldTypes.ftObjectLink);
  }

  private void InvalidateCatalogDescriptor(
    Attr4ObjInfo attr4ObjInfo,
    ImbaseCatalogSelectMode selectMode)
  {
    if (this._imCatalogPropDescriptor == null)
      return;
    if (this._imCatalogPropDescriptor.GetEditor(typeof (ImbaseExtendedCatalogEditor)) is ImbaseExtendedCatalogEditor editor)
      editor.SelectMode = selectMode;
    string empty = string.Empty;
    switch (selectMode)
    {
      case ImbaseCatalogSelectMode.imcmSelectFolder:
        bool isValid;
        this.GetCommonObjTypeID(attr4ObjInfo.ValueType, out isValid);
        this._imCatalogPropDescriptor.SetReadOnly(attr4ObjInfo.InheritMode == InheritModes.Inherited || !isValid);
        if (!isValid)
        {
          this._imCatalogPropDescriptor.SetValue(this._imCatalogPropDescriptor.Component, (object) new List<long>());
          this._imCatalogPropDescriptor.ValueChanged = true;
          empty = LocalizationHolder.rm.GetString("Imbase.Client_148");
          break;
        }
        break;
      case ImbaseCatalogSelectMode.imcmCreateObject:
        this._imCatalogPropDescriptor.SetReadOnly(attr4ObjInfo.InheritMode == InheritModes.Inherited);
        if (editor != null)
        {
          object obj = this._imCatalogPropDescriptor.GetValue(this._imCatalogPropDescriptor.Component);
          List<long> longList = new List<long>();
          switch (obj)
          {
            case long num1:
              longList.Add(num1);
              break;
            case List<long> collection:
              longList.AddRange((IEnumerable<long>) collection);
              break;
          }
          bool flag = false;
          foreach (long num2 in longList)
          {
            if (!editor.GetImCatalogs(attr4ObjInfo.ValueType).Contains(num2))
            {
              flag = true;
              break;
            }
          }
          if (flag)
          {
            this._imCatalogPropDescriptor.SetValue(this._imCatalogPropDescriptor.Component, (object) new List<long>());
            this._imCatalogPropDescriptor.ValueChanged = true;
            break;
          }
          break;
        }
        break;
      default:
        this._imCatalogPropDescriptor.SetReadOnly(true);
        break;
    }
    this._imCatalogPropDescriptor.SetDescription(empty);
  }

  public ImObj4AttrCatProps() => this.InitializeData();

  string ICategoryProps.SubscriberID => nameof (ImObj4AttrCatProps);

  PropDescriptor[] ICategoryProps.GetPropDescriptors(
    PropDescriptorHolder propDescriptorHolder,
    int category,
    object id)
  {
    if (propDescriptorHolder == null)
      return (PropDescriptor[]) null;
    Attr4ObjInfo attr4ObjInfo;
    if ((attr4ObjInfo = this.GetAttr4ObjInfo(propDescriptorHolder, category, id)) == null)
      return (PropDescriptor[]) null;
    List<PropDescriptor> propDescriptorList = new List<PropDescriptor>(2);
    this._imCatalogPropDescriptor = this.FindDescriptor(propDescriptorHolder, this._imCatalogPropDescriptorName);
    if (this._imCatalogPropDescriptor == null)
    {
      TypeConverter converter = (TypeConverter) new ObjID2ObjCaptionConverter();
      object editor = (object) new ImbaseExtendedCatalogEditor();
      string description = LocalizationHolder.rm.GetString("Imbase.Client_146");
      this._imCatalogPropDescriptor = new PropDescriptor(0, (object) null, this._imCatalogPropDescriptorName, (object) null, typeof (long), converter, editor, string.Empty, description, false, true, true);
    }
    propDescriptorList.Add(this._imCatalogPropDescriptor);
    this._imCatalogModePropDescriptor = this.FindDescriptor(propDescriptorHolder, this._imCatalogModePropDescriptorName);
    if (this._imCatalogModePropDescriptor == null)
    {
      EnumCustomConverter enumCustomConverter;
      if (attr4ObjInfo.AttrTypeID != Intermech.Imbase.Consts.ImbaseObjectRefAttID)
        enumCustomConverter = new EnumCustomConverter(typeof (ImbaseCatalogSelectMode));
      else
        enumCustomConverter = new EnumCustomConverter(typeof (ImbaseCatalogSelectMode), new TypeConverter.StandardValuesCollection((ICollection) new object[2]
        {
          (object) ImbaseCatalogSelectMode.imcmSelectFolder,
          (object) ImbaseCatalogSelectMode.imcmNone
        }));
      TypeConverter converter = (TypeConverter) enumCustomConverter;
      object editor = (object) null;
      string description = LocalizationHolder.rm.GetString("Imbase.Client_147");
      this._imCatalogModePropDescriptor = new PropDescriptor(1, (object) null, this._imCatalogModePropDescriptorName, (object) null, typeof (ImbaseCatalogSelectMode), converter, editor, string.Empty, description, false, true, true);
    }
    propDescriptorList.Add(this._imCatalogModePropDescriptor);
    this.FillDescriptors(attr4ObjInfo);
    this.FillVisibility(attr4ObjInfo);
    return propDescriptorList.ToArray();
  }

  bool ICategoryProps.Apply(PropDescriptorHolder pdh, int category, object id, object idOld)
  {
    if (pdh == null || pdh.PropDescriptorCollection == null)
      return true;
    id = (object) this.GetAttr4ObjInfo(pdh, category, id);
    if (this._objTypeInfo == null || !(id is Attr4ObjInfo))
      return true;
    Attr4ObjInfo attr4ObjInfo = (Attr4ObjInfo) id;
    if (attr4ObjInfo.FieldType != FieldTypes.ftObjectLink)
      return true;
    bool flag = false;
    if (this._imCatalogPropDescriptor != null && this._imCatalogPropDescriptor.ValueChanged && this._imCatalogPropDescriptor.GetValue(this._imCatalogPropDescriptor.Component) is List<long> longList)
    {
      this._imExtItem.CatalogIDs = longList;
      flag = true;
    }
    object obj;
    if (this._imCatalogModePropDescriptor != null && this._imCatalogModePropDescriptor.ValueChanged && (obj = this._imCatalogModePropDescriptor.GetValue(this._imCatalogModePropDescriptor.Component)) is ImbaseCatalogSelectMode)
    {
      this._imExtItem.SelectMode = (ImbaseCatalogSelectMode) obj;
      flag = true;
    }
    if (flag)
    {
      this._objTypeInfo.SetValue(attr4ObjInfo.AttrTypeID, this._imExtItem);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this._objTypeInfo.SaveData(sessionKeeper.Session);
    }
    return true;
  }

  void ICategoryProps.Cancel(PropDescriptorHolder pdh, int category, object id)
  {
    if (pdh?.PropDescriptorCollection == null)
      return;
    Attr4ObjInfo attr4ObjInfo = this.GetAttr4ObjInfo(pdh, category, id);
    if (attr4ObjInfo == null || attr4ObjInfo.FieldType != FieldTypes.ftObjectLink)
      return;
    if (this._objTypeInfo != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this._objTypeInfo.LoadData(sessionKeeper.Session);
    }
    this.FillDescriptors(attr4ObjInfo);
  }

  void ICategoryProps.ChangeEventData(
    PropDescriptorHolder pdh,
    int category,
    object id,
    EventArgs e)
  {
    if (pdh == null || pdh.PropDescriptorCollection == null)
      return;
    Attr4ObjInfo attr4ObjInfo = this.GetAttr4ObjInfo(pdh, category, id);
    if (attr4ObjInfo == null)
      return;
    FieldTypes fieldType = attr4ObjInfo.FieldType;
    if (fieldType != FieldTypes.ftObjectLink && this._imExtItem != null)
      this._imExtItem.SelectMode = ImbaseCatalogSelectMode.imcmNone;
    switch (e)
    {
      case DeleteIDEvenArgs _:
        if (fieldType != FieldTypes.ftObjectLink || this._objTypeInfo == null)
          break;
        this._objTypeInfo.SetValue(attr4ObjInfo.AttrTypeID, (ImbaseExtendedItem) null);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          this._objTypeInfo.SaveData(sessionKeeper.Session);
          break;
        }
      case PropertyValueChangedEventArgs changedEventArgs:
        if (changedEventArgs.ChangedItem?.PropertyDescriptor != null && ((PropDescriptor) changedEventArgs.ChangedItem.PropertyDescriptor).PropID == 4)
        {
          FieldTypes fieldTypes = FieldTypes.ftUnknown;
          if (changedEventArgs.OldValue is FieldTypePropertyClass oldValue)
            fieldTypes = oldValue.FieldType;
          if (fieldTypes != FieldTypes.ftObjectLink && fieldType != FieldTypes.ftObjectLink)
            break;
          this.FillVisibility(attr4ObjInfo);
          if (pdh is IFolder folder && folder.PropertiesForm is PropertyTabPageForm propertiesForm && propertiesForm.PropertyGrid != null)
            propertiesForm.PropertyGrid.Refresh();
        }
        if (fieldType != FieldTypes.ftObjectLink)
          break;
        PropDescriptor catalogPropDescriptor = this._imCatalogPropDescriptor;
        if ((catalogPropDescriptor != null ? (catalogPropDescriptor.Equals((object) changedEventArgs.ChangedItem?.PropertyDescriptor) ? 1 : 0) : 0) != 0)
          this._imCatalogPropDescriptor.ValueChanged = true;
        if (this._imCatalogModePropDescriptor == null)
          break;
        if (this._imCatalogModePropDescriptor.Equals((object) changedEventArgs.ChangedItem?.PropertyDescriptor))
        {
          this._imCatalogModePropDescriptor.ValueChanged = true;
          this.InvalidateCatalogDescriptor(attr4ObjInfo, (ImbaseCatalogSelectMode) this._imCatalogModePropDescriptor.GetValue(this._imCatalogModePropDescriptor.Component));
        }
        if (changedEventArgs.ChangedItem?.PropertyDescriptor != pdh.PropDescriptorCollection[8] && changedEventArgs.ChangedItem?.PropertyDescriptor != pdh.PropDescriptorCollection[0])
          break;
        this.InvalidateCatalogDescriptor(attr4ObjInfo, (ImbaseCatalogSelectMode) this._imCatalogModePropDescriptor.GetValue(this._imCatalogModePropDescriptor.Component));
        break;
    }
  }
}
