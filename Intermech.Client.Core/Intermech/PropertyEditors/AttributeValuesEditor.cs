
// Type: Intermech.PropertyEditors.AttributeValuesEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Imbase;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for AttributeValueEditor.</summary>
public class AttributeValuesEditor : UITypeEditor
{
  private AttributeValuesForm attributeValuesForm;
  private static IAttributePropertyDescriberService attributePropertyDescriberService = ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return !context.PropertyDescriptor.IsReadOnly ? UITypeEditorEditStyle.Modal : UITypeEditorEditStyle.None;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="sp"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    IWindowsFormsEditorService service = (IWindowsFormsEditorService) sp.GetService(typeof (IWindowsFormsEditorService));
    if (this.attributeValuesForm == null)
      this.attributeValuesForm = new AttributeValuesForm();
    AttributeValuesPropertyClass valuesPropertyClass = (AttributeValuesPropertyClass) value;
    this.attributeValuesForm.AttributeValue = valuesPropertyClass.AttributeValue;
    int num = (int) this.attributeValuesForm.ShowDialog();
    if (this.attributeValuesForm.DialogResult == DialogResult.OK)
    {
      valuesPropertyClass = new AttributeValuesPropertyClass(this.attributeValuesForm.AttributeValue);
      ((PropDescriptor) context.PropertyDescriptor).ValueChanged = true;
    }
    return (object) valuesPropertyClass;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeID"></param>
  /// <returns></returns>
  private static ObligatoryObjectAttributes GetObligatoryObjectAttribute(int attributeID)
  {
    ObligatoryObjectAttributes obligatoryObjectAttribute = ObligatoryObjectAttributes.None;
    if (attributeID < 0)
    {
      Array values = Enum.GetValues(typeof (ObligatoryObjectAttributes));
      for (int index = 0; index < values.Length; ++index)
      {
        if (attributeID == ((int[]) values)[index])
        {
          obligatoryObjectAttribute = (ObligatoryObjectAttributes) ((int[]) values)[index];
          break;
        }
      }
    }
    return obligatoryObjectAttribute;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aMultiValueMode"></param>
  /// <returns></returns>
  private static bool IsValuedFromList(MultiValueModes aMultiValueMode)
  {
    return MultiValueModesHelper.IsValuedFromList(aMultiValueMode);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aMultiValueMode"></param>
  /// <returns></returns>
  private static bool IsMultipleValued(MultiValueModes aMultiValueMode)
  {
    return MultiValueModesHelper.IsMultipleValued(aMultiValueMode);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <returns></returns>
  public static IAttributePropertyDescriber GetAttributePropertyDescriber(int attributeId)
  {
    IAttributePropertyDescriber propertyDescriber = (IAttributePropertyDescriber) null;
    if (AttributeValuesEditor.attributePropertyDescriberService != null)
      propertyDescriber = AttributeValuesEditor.attributePropertyDescriberService.GetDescriber(attributeId);
    return propertyDescriber;
  }

  /// <summary>Выясняем, может ли значение атрибута быть пустым.</summary>
  /// <param name="iElementInfo"></param>
  /// <param name="elementType"></param>
  /// <param name="attributeId"></param>
  /// <returns></returns>
  private static bool AttributeValueCanNull(
    IElementInfo iElementInfo,
    int elementType,
    int attributeId)
  {
    bool flag = true;
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    IDBAttributeTypeInfo4 attributeTypeInfo4 = (IDBAttributeTypeInfo4) null;
    if (iElementInfo.ElementKind == AttributableElements.Object)
    {
      IDBObjectTypeInfo objectType = service.GetObjectType(elementType);
      if (objectType != null)
        attributeTypeInfo4 = objectType.Attributes.GetAttributeByID(attributeId);
    }
    if (iElementInfo.ElementKind == AttributableElements.Relation)
    {
      IDBRelationTypeInfo relationType = service.GetRelationType(elementType);
      if (relationType != null)
        attributeTypeInfo4 = relationType.Attributes.GetAttributeByID(attributeId);
    }
    if (attributeTypeInfo4 != null)
    {
      if ((attributeTypeInfo4.Options & AttributeOptions.DisableNulls) != AttributeOptions.None)
        flag = false;
    }
    else
    {
      IDBAttributeTypeInfo attributeType = service.GetAttributeType(attributeId);
      if (attributeType != null && (attributeType.Options & AttributeOptions.DisableNulls) != AttributeOptions.None)
        flag = false;
    }
    return flag;
  }

  /// <summary>Инициализация параметров дескрипторов.</summary>
  /// <param name="aObjectHolder">IElementInfoEx</param>
  /// <param name="aAttributeValue"></param>
  /// <param name="id"></param>
  /// <param name="name"></param>
  /// <param name="descrname"></param>
  /// <param name="category"></param>
  /// <param name="type"></param>
  /// <param name="typeConverter"></param>
  /// <param name="editor"></param>
  /// <param name="ro"></param>
  /// <param name="reset"></param>
  /// <param name="disableManualEdit"></param>
  /// <returns></returns>
  public static bool GetPDAttributes(
    object aObjectHolder,
    AttributeValues aAttributeValue,
    ref int id,
    ref string name,
    ref string descrname,
    ref string category,
    ref System.Type type,
    ref TypeConverter typeConverter,
    ref object editor,
    ref bool ro,
    ref bool reset,
    ref string mask,
    ref bool disableManualEdit)
  {
    IElementInfoEx info = aObjectHolder as IElementInfoEx;
    int elementType = info.ElementType;
    type = (System.Type) null;
    typeConverter = (TypeConverter) null;
    editor = (object) null;
    reset = false;
    disableManualEdit = false;
    mask = string.Empty;
    id = aAttributeValue.AttributeID;
    name = aAttributeValue.AttributeName;
    AttributePropertyClass attributePropertyClass = (AttributePropertyClass) null;
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    IDBAttributableTypeInfo attributableType1 = ClientCommons.GetAttributableType(elementType, info.ElementKind);
    if (attributableType1 is IDBObjectType && ((IDBObjectType) attributableType1).CaptionAttribute != 0)
      attributePropertyClass = new AttributePropertyClass(((IDBObjectType) attributableType1).CaptionAttribute);
    if (attributableType1 != null)
    {
      IDBAttributeTypeInfo4 attributeById = attributableType1.Attributes.GetAttributeByID(id);
      if (attributeById != null)
      {
        descrname = attributeById.Note;
        disableManualEdit = (attributeById.Options & AttributeOptions.DisableManualEdit) != 0;
        mask = attributeById.Mask;
      }
      else
      {
        IDBAttributeTypeInfo attributeType = service.GetAttributeType(id);
        if (attributeType != null)
        {
          descrname = attributeType.Note;
          disableManualEdit = (attributeType.PropertiesStructure.Options & AttributeOptions.DisableManualEdit) != 0;
          mask = attributeType.Mask;
        }
        else
          descrname = name;
      }
    }
    else
      descrname = name;
    category = aAttributeValue.GroupName;
    ro = aAttributeValue.ReadOnly;
    if (disableManualEdit)
      ro = true;
    if (aAttributeValue.AttributeType != FieldTypes.ftSystem)
    {
      IAttributePropertyDescriber propertyDescriber = AttributeValuesEditor.GetAttributePropertyDescriber(aAttributeValue.AttributeID);
      switch (aAttributeValue.AttributeType)
      {
        case FieldTypes.ftString:
          type = typeof (string);
          if (AttributeValuesEditor.IsValuedFromList(aAttributeValue.MultipleValued))
          {
            type = typeof (StringPropertyClass);
            bool valCanNull = AttributeValuesEditor.AttributeValueCanNull((IElementInfo) info, elementType, id);
            typeConverter = (TypeConverter) new StringTypeConverter(aObjectHolder as IPossibleValuesHolder, valCanNull);
            editor = (object) new StringDropDownListEditor(aObjectHolder as IPossibleValuesHolder, valCanNull);
            break;
          }
          if (propertyDescriber != null)
          {
            try
            {
              type = propertyDescriber.GetPropDescriptorType(aAttributeValue.AttributeID, aAttributeValue.AttributeType);
              typeConverter = propertyDescriber.GetPropDescriptorConverter(aAttributeValue.AttributeID);
              editor = propertyDescriber.GetPropDescriptorEditor(aAttributeValue.AttributeID);
              ro = propertyDescriber.GetPropDescriptorReadonly(aAttributeValue.AttributeID, aAttributeValue.ReadOnly);
              reset = propertyDescriber.GetPropDescriptorReset(aAttributeValue.AttributeID, reset);
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            if (mask != null && mask != string.Empty)
            {
              bool valCanNull = AttributeValuesEditor.AttributeValueCanNull((IElementInfo) info, elementType, id);
              type = typeof (StringMaskedPropertyClass);
              if (!ro)
              {
                editor = (object) new MaskedValueEditor(mask, type, valCanNull);
                if (valCanNull)
                {
                  reset = true;
                  break;
                }
                break;
              }
              break;
            }
            if (!ro)
            {
              editor = (object) new HistoryEditor(info.ElementIdentifier, info.ElementKind, id);
              break;
            }
            break;
          }
        case FieldTypes.ftInteger:
          type = typeof (long);
          if (AttributeValuesEditor.IsValuedFromList(aAttributeValue.MultipleValued))
          {
            type = typeof (Int64PropertyClass);
            bool valCanNull = AttributeValuesEditor.AttributeValueCanNull((IElementInfo) info, elementType, id);
            typeConverter = (TypeConverter) new IntTypeConverter(aObjectHolder as IPossibleValuesHolder, valCanNull);
            editor = (object) new IntDropDownListEditor(aObjectHolder as IPossibleValuesHolder, valCanNull);
            break;
          }
          if (propertyDescriber != null)
          {
            try
            {
              type = propertyDescriber.GetPropDescriptorType(aAttributeValue.AttributeID, aAttributeValue.AttributeType);
              typeConverter = propertyDescriber.GetPropDescriptorConverter(aAttributeValue.AttributeID);
              editor = propertyDescriber.GetPropDescriptorEditor(aAttributeValue.AttributeID);
              ro = propertyDescriber.GetPropDescriptorReadonly(aAttributeValue.AttributeID, aAttributeValue.ReadOnly);
              reset = propertyDescriber.GetPropDescriptorReset(aAttributeValue.AttributeID, reset);
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            if (!ro && info != null)
            {
              editor = (object) new HistoryEditor(info.ElementIdentifier, info.ElementKind, id);
              if (AttributeValuesEditor.AttributeValueCanNull((IElementInfo) info, elementType, id))
              {
                type = typeof (long?);
                break;
              }
              break;
            }
            break;
          }
        case FieldTypes.ftDouble:
          type = typeof (double);
          if (AttributeValuesEditor.IsValuedFromList(aAttributeValue.MultipleValued))
          {
            type = typeof (DoublePropertyClass);
            bool valCanNull = AttributeValuesEditor.AttributeValueCanNull((IElementInfo) info, elementType, id);
            typeConverter = (TypeConverter) new DoubleTypeConverter(aObjectHolder as IPossibleValuesHolder, valCanNull);
            editor = (object) new DoubleDropDownListEditor(aObjectHolder as IPossibleValuesHolder, valCanNull);
            break;
          }
          if (propertyDescriber != null)
          {
            try
            {
              type = propertyDescriber.GetPropDescriptorType(aAttributeValue.AttributeID, aAttributeValue.AttributeType);
              typeConverter = propertyDescriber.GetPropDescriptorConverter(aAttributeValue.AttributeID);
              editor = propertyDescriber.GetPropDescriptorEditor(aAttributeValue.AttributeID);
              ro = propertyDescriber.GetPropDescriptorReadonly(aAttributeValue.AttributeID, aAttributeValue.ReadOnly);
              reset = propertyDescriber.GetPropDescriptorReset(aAttributeValue.AttributeID, reset);
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            if (!ro && info != null)
            {
              editor = (object) new HistoryEditor(info.ElementIdentifier, info.ElementKind, id);
              if (AttributeValuesEditor.AttributeValueCanNull((IElementInfo) info, elementType, id))
              {
                type = typeof (double?);
                break;
              }
              break;
            }
            break;
          }
        case FieldTypes.ftDateTime:
          type = typeof (DateTime);
          if (AttributeValuesEditor.IsValuedFromList(aAttributeValue.MultipleValued))
          {
            type = typeof (DateTimePropertyClass);
            bool valCanNull = AttributeValuesEditor.AttributeValueCanNull((IElementInfo) info, elementType, id);
            if (!disableManualEdit)
            {
              typeConverter = (TypeConverter) new DateTimeTypeConverter(aObjectHolder as IPossibleValuesHolder, valCanNull);
              editor = (object) new DateTimeDropDownListEditor(aObjectHolder as IPossibleValuesHolder, valCanNull);
              break;
            }
            break;
          }
          if (!ro && info != null && AttributeValuesEditor.AttributeValueCanNull((IElementInfo) info, elementType, id))
          {
            type = typeof (DateTime?);
            if (!disableManualEdit)
            {
              editor = (object) new DateTimeFixedEditor();
              break;
            }
            break;
          }
          break;
        case FieldTypes.ftShortBlob:
          type = typeof (BlobPropertyClass);
          ro = true;
          break;
        case FieldTypes.ftFile:
          type = typeof (FilePropertyClass);
          break;
        case FieldTypes.ftExternalLink:
          type = typeof (string);
          ro = true;
          break;
        case FieldTypes.ftObjectLink:
        case FieldTypes.ftObjectLinkByID:
          if (AttributeValuesEditor.IsValuedFromList(aAttributeValue.MultipleValued))
          {
            type = typeof (ObjectPropertyClass);
            bool valCanNull = AttributeValuesEditor.AttributeValueCanNull((IElementInfo) info, elementType, id);
            if (!disableManualEdit)
            {
              typeConverter = (TypeConverter) new ObjectsTypeConverter(aObjectHolder as IPossibleValuesHolder, valCanNull, aAttributeValue.AttributeType == FieldTypes.ftObjectLink);
              editor = (object) new ObjectsDropDownListEditor(aObjectHolder as IPossibleValuesHolder, valCanNull, aAttributeValue.AttributeType == FieldTypes.ftObjectLink);
            }
            if (!ro && !disableManualEdit)
            {
              reset = true;
              break;
            }
            break;
          }
          if (propertyDescriber != null)
          {
            try
            {
              type = propertyDescriber.GetPropDescriptorType(aAttributeValue.AttributeID, aAttributeValue.AttributeType);
              typeConverter = propertyDescriber.GetPropDescriptorConverter(aAttributeValue.AttributeID);
              editor = propertyDescriber.GetPropDescriptorEditor(aAttributeValue.AttributeID);
              ro = propertyDescriber.GetPropDescriptorReadonly(aAttributeValue.AttributeID, aAttributeValue.ReadOnly | disableManualEdit);
              reset = propertyDescriber.GetPropDescriptorReset(aAttributeValue.AttributeID, reset);
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            type = typeof (ObjectPropertyClass);
            if (!(ro | disableManualEdit))
            {
              reset = true;
              editor = (object) ((UITypeEditor) AttributeValuesEditor.ImbaseAttributesHandle((IUserSession) null, id, elementType, info.ElementIdentifier) ?? (UITypeEditor) new ObjectEditor(id, aAttributeValue.AttributeType));
              break;
            }
            break;
          }
        case FieldTypes.ftPassword:
          type = typeof (PasswordPropertyClass);
          break;
        case FieldTypes.ftMemo:
          if (propertyDescriber != null)
          {
            try
            {
              type = propertyDescriber.GetPropDescriptorType(aAttributeValue.AttributeID, aAttributeValue.AttributeType);
              typeConverter = propertyDescriber.GetPropDescriptorConverter(aAttributeValue.AttributeID);
              editor = propertyDescriber.GetPropDescriptorEditor(aAttributeValue.AttributeID);
              ro = propertyDescriber.GetPropDescriptorReadonly(aAttributeValue.AttributeID, aAttributeValue.ReadOnly);
              reset = propertyDescriber.GetPropDescriptorReset(aAttributeValue.AttributeID, reset);
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            type = typeof (MemoPropertyClass);
            break;
          }
        case FieldTypes.ftBlob:
          type = typeof (BlobPropertyClass);
          ro = true;
          break;
        case FieldTypes.ftBoolean:
          type = typeof (BoolPropertyClass);
          if (!disableManualEdit)
          {
            typeConverter = (TypeConverter) new BoolConverter();
            break;
          }
          break;
        case FieldTypes.ftMeasured:
          type = typeof (string);
          if (AttributeValuesEditor.IsValuedFromList(aAttributeValue.MultipleValued))
          {
            type = typeof (MeasuredPropertyClass);
            bool valCanNull = AttributeValuesEditor.AttributeValueCanNull((IElementInfo) info, elementType, id);
            if (!disableManualEdit)
            {
              typeConverter = (TypeConverter) new MeasuredTypeConverter(aObjectHolder as IPossibleValuesHolder, valCanNull);
              editor = (object) new MeasuredDropDownListEditor(aObjectHolder as IPossibleValuesHolder, valCanNull);
              break;
            }
            break;
          }
          if (propertyDescriber != null)
          {
            try
            {
              type = propertyDescriber.GetPropDescriptorType(aAttributeValue.AttributeID, aAttributeValue.AttributeType);
              typeConverter = propertyDescriber.GetPropDescriptorConverter(aAttributeValue.AttributeID);
              editor = propertyDescriber.GetPropDescriptorEditor(aAttributeValue.AttributeID);
              ro = propertyDescriber.GetPropDescriptorReadonly(aAttributeValue.AttributeID, aAttributeValue.ReadOnly);
              reset = propertyDescriber.GetPropDescriptorReset(aAttributeValue.AttributeID, reset);
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            if (!ro && info != null && !disableManualEdit)
            {
              MeasuredIdReceiver measuredIdReceiver = new MeasuredIdReceiver((IElementInfo) info, id);
              editor = (object) new MeasureEditor(id, new GetDefaultMeasureIDDelegate(measuredIdReceiver.GetDefaultMeasureID));
              break;
            }
            break;
          }
        case FieldTypes.ftAutoInc:
          type = typeof (long);
          ro = true;
          break;
        case FieldTypes.ftGuid:
          type = typeof (Guid);
          if (AttributeValuesEditor.IsValuedFromList(aAttributeValue.MultipleValued))
          {
            type = typeof (GuidPropertyClass);
            bool valCanNull = AttributeValuesEditor.AttributeValueCanNull((IElementInfo) info, elementType, id);
            typeConverter = (TypeConverter) new GuidTypeConverter(aObjectHolder as IPossibleValuesHolder, valCanNull);
            editor = (object) new GuidDropDownListEditor(aObjectHolder as IPossibleValuesHolder, valCanNull);
            break;
          }
          if (propertyDescriber != null)
          {
            try
            {
              type = propertyDescriber.GetPropDescriptorType(aAttributeValue.AttributeID, aAttributeValue.AttributeType);
              typeConverter = propertyDescriber.GetPropDescriptorConverter(aAttributeValue.AttributeID);
              editor = propertyDescriber.GetPropDescriptorEditor(aAttributeValue.AttributeID);
              ro = propertyDescriber.GetPropDescriptorReadonly(aAttributeValue.AttributeID, aAttributeValue.ReadOnly);
              reset = propertyDescriber.GetPropDescriptorReset(aAttributeValue.AttributeID, reset);
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            if (!ro && info != null)
            {
              editor = (object) new HistoryEditor(info.ElementIdentifier, info.ElementKind, id);
              if (AttributeValuesEditor.AttributeValueCanNull((IElementInfo) info, elementType, id))
              {
                type = typeof (Guid?);
                break;
              }
              break;
            }
            break;
          }
      }
    }
    else
    {
      type = (System.Type) null;
      switch (AttributeValuesEditor.GetObligatoryObjectAttribute(aAttributeValue.AttributeID))
      {
        case ObligatoryObjectAttributes.F_REL_CREATOR:
        case ObligatoryObjectAttributes.F_CREATOR_ID:
          type = typeof (ObjectPropertyClass);
          break;
        case ObligatoryObjectAttributes.F_ACCESS:
          type = typeof (SecurityLevelPropertyClass);
          typeConverter = (TypeConverter) new SecurityLevelTypeConverter();
          break;
        case ObligatoryObjectAttributes.CAPTION:
          type = typeof (string);
          if (!ro && attributePropertyClass != null)
          {
            IDBAttributableTypeInfo attributableType2 = ClientCommons.GetAttributableType(elementType, info.ElementKind);
            if (attributableType2 != null)
            {
              IDBAttributeTypeInfo4 attributeById = attributableType2.Attributes.GetAttributeByID(attributePropertyClass.Attribute);
              if (attributeById != null && (attributeById.Options & AttributeOptions.DisableManualEdit) != 0)
              {
                ro = true;
                break;
              }
              break;
            }
            break;
          }
          break;
        case ObligatoryObjectAttributes.F_RELATION_TYPE:
          type = typeof (RelationTypePropertyClass);
          typeConverter = (TypeConverter) new RelationTypeConverter();
          break;
        case ObligatoryObjectAttributes.F_SITE_ID:
          type = typeof (SiteIDPropertyClass);
          break;
        case ObligatoryObjectAttributes.F_BASE_VERSION:
          type = typeof (BoolPropertyClass);
          typeConverter = (TypeConverter) new BoolConverter();
          break;
        case ObligatoryObjectAttributes.F_PROJECT_ID:
          type = typeof (ProjectPropertyClass);
          reset = true;
          break;
        case ObligatoryObjectAttributes.F_GUID:
          type = typeof (Guid);
          break;
        case ObligatoryObjectAttributes.F_AREA_ID:
          type = typeof (SubjectAreaPropertyClass);
          break;
        case ObligatoryObjectAttributes.F_LEVEL_ID:
          type = typeof (string);
          ro = true;
          break;
        case ObligatoryObjectAttributes.F_OWNER_ID:
          type = typeof (UserPropertyClass);
          break;
        case ObligatoryObjectAttributes.F_OBJECT_TYPE:
          type = typeof (ObjectTypePropertyClass);
          break;
        case ObligatoryObjectAttributes.F_CHKOUT_BY:
          type = typeof (string);
          break;
        case ObligatoryObjectAttributes.F_LC_STEP:
          type = typeof (string);
          ro = true;
          break;
        default:
          type = typeof (string);
          break;
      }
    }
    if (!ro && Statics.CheckAttributeReadonlyBlacklist(aAttributeValue.AttributeID))
      ro = true;
    if (!ro)
      ro = info.CheckAttributeLock(id);
    if (ro)
      editor = (object) null;
    return type != (System.Type) null;
  }

  /// <summary>
  /// Установлен ли флаг флаг AttributeOptions.ImbaseFlag_TableRecordRef
  /// </summary>
  /// <param name="attributeTypeId"></param>
  /// <param name="objTypeId"></param>
  /// <param name="dbObj"></param>
  /// <param name="sk"></param>
  /// <returns></returns>
  public static bool IsTableRecordRefFlagSet(int attributeTypeId)
  {
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeTypeId);
    return attributeType != null && attributeType.Options.HasFlag((Enum) AttributeOptions.ImbaseFlag_TableRecordRef);
  }

  /// <summary>
  /// Обработаем аттрибуты, у которых есть расширение Imbase
  /// </summary>
  /// <returns></returns>
  public static ImbaseFilterEditor ImbaseAttributesHandle(
    IUserSession session,
    int attrTypeID,
    int objTypeID,
    long objectID)
  {
    ImbaseExtendedItem imbaseExtendedItem = (ImbaseExtendedItem) null;
    ExtendedServiceHelper.ObjTypeInfo objTypeData = ExtendedServiceHelper.GetObjTypeData(objTypeID, session);
    if (objTypeData != null)
      imbaseExtendedItem = objTypeData.GetValue(attrTypeID, session);
    if (imbaseExtendedItem == null || imbaseExtendedItem.SelectMode == ImbaseCatalogSelectMode.imcmNone || imbaseExtendedItem.CatalogIDs == null || imbaseExtendedItem.CatalogIDs.Count == 0)
      return (ImbaseFilterEditor) null;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeID);
    if (attributeType == null || attributeType.SizeType == -1L)
      return new ImbaseFilterEditor(imbaseExtendedItem.CatalogIDs, objectID, imbaseExtendedItem.SelectMode);
    List<int> linkedObjectTypes = MetaDataHelper.GetLinkedObjectTypes(attrTypeID);
    return new ImbaseFilterEditor(imbaseExtendedItem.CatalogIDs, linkedObjectTypes?.ToArray(), objectID, imbaseExtendedItem.SelectMode);
  }

  /// <summary>
  /// Получение значения для дескриптора.
  /// aElementID и aAttributableElement нужны для полей с блобами - для их записи чтения нужно получение IBlobReader/IBlobWriter
  /// possibleValues - список допустимых значений, может быть null, в этом случае при необходимости ищется на месте
  /// </summary>
  /// <param name="attributeValue"></param>
  /// <param name="index"></param>
  /// <param name="elementID"></param>
  /// <param name="attributableElement"></param>
  /// <param name="possibleValues"></param>
  /// <returns></returns>
  public static object GetPDValue(
    AttributeValues attributeValue,
    int index,
    long elementID,
    AttributableElements attributableElement,
    string mask,
    DataTable possibleValues)
  {
    return AttributeValuesEditor.GetPDValue(attributeValue.MultipleValued, attributeValue.AttributeID, attributeValue.AttributeType, attributeValue.Values, attributeValue.Descriptions, index, elementID, attributableElement, mask, possibleValues);
  }

  /// <summary>
  /// Получение значения для дескриптора.
  /// aElementID и aAttributableElement нужны для полей с блобами - для их записи чтения нужно получение IBlobReader/IBlobWriter
  /// possibleValues - список допустимых значений, может быть null, в этом случае при необходимости ищется на месте
  /// </summary>
  public static object GetPDValue(
    MultiValueModes multipleValued,
    int attributeID,
    FieldTypes attributeType,
    object[] values,
    object[] descriptions,
    int index,
    long aElementID,
    AttributableElements aAttributableElement,
    string mask,
    DataTable possibleValues)
  {
    bool flag = AttributeValuesEditor.IsValuedFromList(multipleValued);
    if (flag && possibleValues == null)
      possibleValues = ClientCommons.GetPossibleValues(attributeID);
    object pdValue;
    if (attributeType != FieldTypes.ftSystem)
    {
      IAttributePropertyDescriber propertyDescriber = AttributeValuesEditor.GetAttributePropertyDescriber(attributeID);
      switch (attributeType - 1)
      {
        case FieldTypes.ftUnknown:
          if (propertyDescriber != null)
          {
            try
            {
              pdValue = propertyDescriber.GetPropDescriptorValue((IElementInfo) new AttributeValuesEditor.LocalElementInfo(aElementID, aAttributableElement), attributeID, values[index]);
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            pdValue = values[index] == DBNull.Value || values[index] == null ? (object) null : (!flag ? (mask == null || !(mask != string.Empty) ? values[index] : (object) new StringMaskedPropertyClass(Convert.ToString(values[index]), mask)) : (object) new StringPropertyClass(Convert.ToString(values[index]), string.Empty, possibleValues));
            break;
          }
        case FieldTypes.ftString:
          if (propertyDescriber != null)
          {
            try
            {
              pdValue = propertyDescriber.GetPropDescriptorValue((IElementInfo) new AttributeValuesEditor.LocalElementInfo(aElementID, aAttributableElement), attributeID, values[index]);
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            pdValue = values[index] == DBNull.Value || values[index] == null ? (object) null : (flag ? (object) new Int64PropertyClass(Convert.ToInt64(values[index]), string.Empty, possibleValues) : values[index]);
            break;
          }
        case FieldTypes.ftInteger:
          if (propertyDescriber != null)
          {
            try
            {
              pdValue = propertyDescriber.GetPropDescriptorValue((IElementInfo) new AttributeValuesEditor.LocalElementInfo(aElementID, aAttributableElement), attributeID, values[index]);
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            pdValue = values[index] == DBNull.Value || values[index] == null ? (object) null : (flag ? (object) new DoublePropertyClass(Convert.ToDouble(values[index]), string.Empty, possibleValues) : values[index]);
            break;
          }
        case FieldTypes.ftDouble:
          pdValue = values[index] == DBNull.Value || values[index] == null ? (object) null : (flag ? (object) new DateTimePropertyClass(Convert.ToDateTime(values[index]), string.Empty, possibleValues) : values[index]);
          break;
        case FieldTypes.ftDateTime:
          pdValue = (object) new BlobPropertyClass(values[index]);
          break;
        case FieldTypes.ftShortBlob:
          string empty = string.Empty;
          if (values[index] != DBNull.Value || values[index] == null)
            empty = Convert.ToString(values[index]);
          pdValue = (object) new FilePropertyClass(empty, aElementID, aAttributableElement, attributeID, index);
          break;
        case FieldTypes.ftExternalLink:
        case FieldTypes.ftGuid:
          if (propertyDescriber != null)
          {
            try
            {
              pdValue = propertyDescriber.GetPropDescriptorValue((IElementInfo) new AttributeValuesEditor.LocalElementInfo(aElementID, aAttributableElement), attributeID, values[index]);
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            pdValue = values[index] == DBNull.Value || values[index] == null ? (object) (ObjectPropertyClass) null : (object) new ObjectPropertyClass(Convert.ToInt64(values[index]), descriptions == null || descriptions[index] == null || descriptions[index] == DBNull.Value ? (string) null : descriptions[index].ToString(), attributeType == FieldTypes.ftObjectLink);
            break;
          }
        case FieldTypes.ftObjectLink:
          pdValue = (object) new PasswordPropertyClass(Convert.ToString(values[index]));
          break;
        case FieldTypes.ftPassword:
          IMSAttributeType attributeType1 = MetaDataHelper.GetAttributeType(attributeID);
          int aMaxMemoSize = attributeType1.SizeType < 0L ? Intermech.Consts.MaxShortBlobSize : Convert.ToInt32(attributeType1.SizeType);
          if (propertyDescriber != null)
          {
            try
            {
              pdValue = propertyDescriber.GetPropDescriptorValue((IElementInfo) new AttributeValuesEditor.LocalElementInfo(aElementID, aAttributableElement), attributeID, values[index]);
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            pdValue = values[index] == DBNull.Value || values[index] == null ? (object) new MemoPropertyClass(string.Empty, true, aMaxMemoSize) : (object) new MemoPropertyClass(Convert.ToString(values[index]), aMaxMemoSize);
            break;
          }
        case FieldTypes.ftMemo:
          pdValue = (object) new BlobPropertyClass(values[index]);
          break;
        case FieldTypes.ftBlob:
          pdValue = values[index] == DBNull.Value || values[index] == null ? (object) new BoolPropertyClass(false, true) : (object) new BoolPropertyClass(Convert.ToBoolean(values[index]));
          break;
        case FieldTypes.ftBoolean:
          if (propertyDescriber != null)
          {
            try
            {
              pdValue = propertyDescriber.GetPropDescriptorValue((IElementInfo) new AttributeValuesEditor.LocalElementInfo(aElementID, aAttributableElement), attributeID, values[index]);
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            pdValue = values[index] == DBNull.Value || values[index] == null ? (object) null : (flag ? (object) new MeasuredPropertyClass(Convert.ToString(values[index]), string.Empty, possibleValues) : values[index]);
            break;
          }
        case FieldTypes.ftMeasured:
          pdValue = values[index] == DBNull.Value || values[index] == null ? (object) null : values[index];
          break;
        case FieldTypes.ftSystem:
          if (propertyDescriber != null)
          {
            try
            {
              pdValue = propertyDescriber.GetPropDescriptorValue((IElementInfo) new AttributeValuesEditor.LocalElementInfo(aElementID, aAttributableElement), attributeID, values[index]);
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            pdValue = values[index] == DBNull.Value || values[index] == null ? (object) null : (flag ? (object) new GuidPropertyClass(new Guid(Convert.ToString(values[index])), string.Empty, possibleValues) : values[index]);
            break;
          }
        default:
          pdValue = values[index] == DBNull.Value || values[index] == null ? (object) null : values[index];
          break;
      }
    }
    else
    {
      switch (AttributeValuesEditor.GetObligatoryObjectAttribute(attributeID))
      {
        case ObligatoryObjectAttributes.F_REL_CREATOR:
        case ObligatoryObjectAttributes.F_CREATOR_ID:
          pdValue = (object) new ObjectPropertyClass(Convert.ToInt64(values[index]), descriptions == null || descriptions[index] == null || descriptions[index] == DBNull.Value ? (string) null : Convert.ToString(descriptions[index]));
          break;
        case ObligatoryObjectAttributes.F_ACCESS:
          pdValue = (object) new SecurityLevelPropertyClass(Convert.ToInt32(values[index]));
          break;
        case ObligatoryObjectAttributes.F_RELATION_TYPE:
          pdValue = (object) new RelationTypePropertyClass(Convert.ToInt32(values[index]));
          break;
        case ObligatoryObjectAttributes.F_SITE_ID:
          pdValue = (object) new SiteIDPropertyClass(Convert.ToString(values[index]), descriptions == null || descriptions[index] == null || descriptions[index] == DBNull.Value ? (string) null : Convert.ToString(descriptions[index]));
          break;
        case ObligatoryObjectAttributes.F_BASE_VERSION:
          pdValue = (object) new BoolPropertyClass(Convert.ToBoolean(values[index]));
          break;
        case ObligatoryObjectAttributes.F_PROJECT_ID:
          pdValue = (object) new ProjectPropertyClass(Convert.ToInt64(values[index]), descriptions == null || descriptions[index] == null || descriptions[index] == DBNull.Value ? (string) null : Convert.ToString(descriptions[index]));
          break;
        case ObligatoryObjectAttributes.F_GUID:
          pdValue = (object) (values[index] is Guid ? (Guid) values[index] : new Guid(Convert.ToString(values[index])));
          break;
        case ObligatoryObjectAttributes.F_AREA_ID:
          pdValue = (object) new SubjectAreaPropertyClass(Convert.ToString(values[index]));
          break;
        case ObligatoryObjectAttributes.F_LEVEL_ID:
          pdValue = (object) new LCLevelPropertyClass(Convert.ToInt32(values[index]), descriptions == null || descriptions[index] == null || descriptions[index] == DBNull.Value ? (string) null : Convert.ToString(descriptions[index]));
          break;
        case ObligatoryObjectAttributes.F_OWNER_ID:
          pdValue = (object) new UserPropertyClass(Convert.ToInt64(values[index]), descriptions == null || descriptions[index] == null || descriptions[index] == DBNull.Value ? (string) null : Convert.ToString(descriptions[index]));
          break;
        case ObligatoryObjectAttributes.F_OBJECT_TYPE:
          pdValue = (object) new ObjectTypePropertyClass(Convert.ToInt32(values[index]), descriptions == null || descriptions[index] == null || descriptions[index] == DBNull.Value ? (string) null : Convert.ToString(descriptions[index]));
          break;
        case ObligatoryObjectAttributes.F_CHKOUT_BY:
          pdValue = (object) new UserPropertyClass(Convert.ToInt64(values[index]), descriptions == null || descriptions[index] == null || descriptions[index] == DBNull.Value ? (string) null : Convert.ToString(descriptions[index]));
          break;
        case ObligatoryObjectAttributes.F_LC_STEP:
          pdValue = (object) Convert.ToString(descriptions[index]);
          break;
        default:
          pdValue = (object) Convert.ToString(values[index]);
          break;
      }
    }
    return pdValue;
  }

  /// <summary>
  /// Получение значения для AttributeValue.
  /// aAttributeValue хранит тип и id, aPD содержит значение
  /// component от ICustomTypeDescriptor ( т.е объект к-рый назначается в SelectedObject для PropertyGrid )
  /// </summary>
  /// <param name="aPD"></param>
  /// <param name="aAttributeValue"></param>
  /// <param name="component"></param>
  /// <returns></returns>
  public static object GetAVValue(
    PropDescriptor aPD,
    AttributeValues aAttributeValue,
    object component)
  {
    object avValue = (object) null;
    bool flag = AttributeValuesEditor.IsValuedFromList(aAttributeValue.MultipleValued);
    if (aAttributeValue.AttributeType != FieldTypes.ftSystem)
    {
      IAttributePropertyDescriber propertyDescriber = AttributeValuesEditor.GetAttributePropertyDescriber(aAttributeValue.AttributeID);
      switch (aAttributeValue.AttributeType)
      {
        case FieldTypes.ftString:
          if (propertyDescriber != null)
          {
            try
            {
              avValue = propertyDescriber.GetAttributeValue(component as IElementInfo, aAttributeValue.AttributeID, aPD.GetValue(component));
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            object obj = aPD.GetValue(component);
            avValue = !flag || obj == null || obj == DBNull.Value ? (!(obj is StringMaskedPropertyClass) ? obj : ((PropertyClass) obj).Value) : ((PropertyClass) obj).Value;
            break;
          }
        case FieldTypes.ftInteger:
          if (propertyDescriber != null)
          {
            try
            {
              avValue = propertyDescriber.GetAttributeValue(component as IElementInfo, aAttributeValue.AttributeID, aPD.GetValue(component));
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            object obj = aPD.GetValue(component);
            avValue = !flag || obj == null || obj == DBNull.Value ? obj : ((PropertyClass) obj).Value;
            break;
          }
        case FieldTypes.ftDouble:
          if (propertyDescriber != null)
          {
            try
            {
              avValue = propertyDescriber.GetAttributeValue(component as IElementInfo, aAttributeValue.AttributeID, aPD.GetValue(component));
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            object obj = aPD.GetValue(component);
            avValue = !flag || obj == null || obj == DBNull.Value ? obj : ((PropertyClass) obj).Value;
            break;
          }
        case FieldTypes.ftDateTime:
          object obj1 = aPD.GetValue(component);
          avValue = !flag || obj1 == null || obj1 == DBNull.Value ? obj1 : ((PropertyClass) obj1).Value;
          break;
        case FieldTypes.ftShortBlob:
          BlobPropertyClass blobPropertyClass1 = (BlobPropertyClass) aPD.GetValue(component);
          if (blobPropertyClass1 != null)
          {
            avValue = blobPropertyClass1.Blob;
            break;
          }
          break;
        case FieldTypes.ftObjectLink:
        case FieldTypes.ftObjectLinkByID:
          if (propertyDescriber != null)
          {
            try
            {
              avValue = propertyDescriber.GetAttributeValue(component as IElementInfo, aAttributeValue.AttributeID, aPD.GetValue(component));
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            ObjectPropertyClass objectPropertyClass = (ObjectPropertyClass) aPD.GetValue(component);
            if (objectPropertyClass != null && !objectPropertyClass.NullObject)
            {
              avValue = (object) objectPropertyClass.ObjectID;
              break;
            }
            break;
          }
        case FieldTypes.ftPassword:
          PasswordPropertyClass passwordPropertyClass = (PasswordPropertyClass) aPD.GetValue(component);
          if (passwordPropertyClass != null)
          {
            avValue = (object) passwordPropertyClass.Password;
            break;
          }
          break;
        case FieldTypes.ftMemo:
          if (propertyDescriber != null)
          {
            try
            {
              avValue = propertyDescriber.GetAttributeValue(component as IElementInfo, aAttributeValue.AttributeID, aPD.GetValue(component));
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            MemoPropertyClass memoPropertyClass = (MemoPropertyClass) aPD.GetValue(component);
            if (memoPropertyClass != null && !memoPropertyClass.IsNull)
            {
              avValue = (object) memoPropertyClass.Memo;
              break;
            }
            break;
          }
        case FieldTypes.ftBlob:
          BlobPropertyClass blobPropertyClass2 = (BlobPropertyClass) aPD.GetValue(component);
          if (blobPropertyClass2 != null)
          {
            avValue = blobPropertyClass2.Blob;
            break;
          }
          break;
        case FieldTypes.ftBoolean:
          BoolPropertyClass boolPropertyClass = (BoolPropertyClass) aPD.GetValue(component);
          if (boolPropertyClass != null && !boolPropertyClass.IsNull)
          {
            avValue = (object) boolPropertyClass.Boolean;
            break;
          }
          break;
        case FieldTypes.ftMeasured:
          if (propertyDescriber != null)
          {
            try
            {
              avValue = propertyDescriber.GetAttributeValue(component as IElementInfo, aAttributeValue.AttributeID, aPD.GetValue(component));
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            object obj2 = aPD.GetValue(component);
            avValue = !flag || obj2 == null || obj2 == DBNull.Value ? obj2 : ((PropertyClass) obj2).Value;
            break;
          }
        case FieldTypes.ftAutoInc:
          avValue = aPD.GetValue(component);
          break;
        case FieldTypes.ftGuid:
          if (propertyDescriber != null)
          {
            try
            {
              avValue = propertyDescriber.GetAttributeValue(component as IElementInfo, aAttributeValue.AttributeID, aPD.GetValue(component));
              break;
            }
            catch (Exception ex)
            {
              ExceptionOutput.Write(LocalizationHolder.rm.GetString("PropDescriptorDescriber"), ex);
              throw;
            }
          }
          else
          {
            object obj3 = aPD.GetValue(component);
            avValue = !flag || obj3 == null || obj3 == DBNull.Value ? obj3 : ((PropertyClass) obj3).Value;
            break;
          }
        default:
          avValue = aPD.GetValue(component);
          break;
      }
    }
    else
    {
      switch (AttributeValuesEditor.GetObligatoryObjectAttribute(aAttributeValue.AttributeID))
      {
        case ObligatoryObjectAttributes.F_REL_CREATOR:
        case ObligatoryObjectAttributes.F_CREATOR_ID:
          avValue = (object) ((ObjectPropertyClass) aPD.GetValue(component)).ObjectID;
          break;
        case ObligatoryObjectAttributes.F_ACCESS:
          avValue = (object) ((SecurityLevelPropertyClass) aPD.GetValue(component)).SecurityLevel;
          break;
        case ObligatoryObjectAttributes.F_RELATION_TYPE:
          avValue = (object) ((RelationTypePropertyClass) aPD.GetValue(component)).RelationType;
          break;
        case ObligatoryObjectAttributes.F_BASE_VERSION:
          avValue = (object) ((BoolPropertyClass) aPD.GetValue(component)).Boolean;
          break;
        case ObligatoryObjectAttributes.F_PROJECT_ID:
          ProjectPropertyClass projectPropertyClass = (ProjectPropertyClass) aPD.GetValue(component);
          avValue = projectPropertyClass == null ? (object) 0L : (object) projectPropertyClass.ObjectID;
          break;
        case ObligatoryObjectAttributes.F_GUID:
          avValue = aPD.GetValue(component);
          break;
        case ObligatoryObjectAttributes.F_AREA_ID:
          avValue = (object) ((SubjectAreaPropertyClass) aPD.GetValue(component)).Areas;
          break;
        case ObligatoryObjectAttributes.F_OWNER_ID:
          avValue = (object) ((ObjectPropertyClass) aPD.GetValue(component)).ObjectID;
          break;
        case ObligatoryObjectAttributes.F_OBJECT_TYPE:
          avValue = (object) ((ObjectTypePropertyClass) aPD.GetValue(component)).ObjectType;
          break;
        default:
          avValue = (object) aPD.GetValue(component).ToString();
          break;
      }
    }
    return avValue;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aAttributeValue"></param>
  /// <returns></returns>
  public static bool IsSystemAttributeValue(AttributeValues aAttributeValue)
  {
    return aAttributeValue.AttributeType == FieldTypes.ftSystem;
  }

  /// <summary>
  /// Возвращает маску для атрибута применительно к типу объекта/связи
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="aElementID"></param>
  /// <param name="aAttributableElement"></param>
  /// <returns></returns>
  public static object AttributeValueTransformationByCultureInfo(object value, FieldTypes ft)
  {
    if (value != null && value is string && value.ToString() != string.Empty && ft == FieldTypes.ftDouble)
      value = (object) Convert.ToDouble(value, (IFormatProvider) CultureInfo.InvariantCulture);
    return value;
  }

  /// <summary>
  /// 
  /// </summary>
  internal class LocalElementInfo : IElementInfo
  {
    private AttributableElements attributableElements;
    private long id;

    /// <summary>Конструктор.</summary>
    /// <param name="aId"></param>
    /// <param name="aAttributableElements"></param>
    public LocalElementInfo(long aId, AttributableElements aAttributableElements)
    {
      this.id = aId;
      this.attributableElements = aAttributableElements;
    }

    /// <summary>
    /// 
    /// </summary>
    public AttributableElements ElementKind => this.attributableElements;

    /// <summary>
    /// 
    /// </summary>
    public long ElementIdentifier => this.id;
  }
}
