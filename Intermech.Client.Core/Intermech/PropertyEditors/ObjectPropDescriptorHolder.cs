
// Type: Intermech.PropertyEditors.ObjectPropDescriptorHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ObjectPropDescriptorHolder.</summary>
public class ObjectPropDescriptorHolder : 
  PropDescriptorHolder,
  IPossibleValuesHolder,
  IElementInfoEx,
  IElementInfo,
  IObjectPropDescriptorHolder
{
  private System.Type[] tabTypes;
  private bool lockTypeChange;
  private List<int> lockedAttributes = new List<int>();
  private ArrayList loadedTabs = new ArrayList();
  private Hashtable visibleAId = new Hashtable();
  private ArrayList deletedAId = new ArrayList();
  private long id;
  private AttributableElements attributableElement;
  private ObjectPropertyGrid objectPropertyGrid;
  private bool anyAttributes;
  internal ArrayList pdcGeneralList = new ArrayList();
  private GetAttributeValuesModes attributeValuesModes = ClientConsts.GetAttributeValuesModes;
  private ArrayList originalAttributeValuesList = new ArrayList();
  private ArrayList attributeValuesList = new ArrayList();
  private int elementType;
  private long cachedId;
  private AttributableElements cachedKind;
  private int cachedType;
  private List<int> cachedLockedAttrsList;

  public List<int> LockedAttributes => this.lockedAttributes;

  public long Id => this.id;

  public AttributableElements AttributableElement => this.attributableElement;

  public ObjectPropertyGrid ObjectPropertyGrid => this.objectPropertyGrid;

  public bool AnyAttributes => this.anyAttributes;

  public GetAttributeValuesModes AttributeValuesModes => this.attributeValuesModes;

  public ArrayList AttributeValuesList => this.attributeValuesList;

  public bool CheckIfDeleted(int attributeType)
  {
    return this.deletedAId.IndexOf((object) attributeType) != -1;
  }

  public DataTable GetPossibleAttributes(bool byType, bool byVisible)
  {
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    IDBCollection dbCollection = (IDBCollection) null;
    if (byType)
    {
      if (this.attributableElement == AttributableElements.Object)
        dbCollection = !byVisible ? service.GetObjectType(this.elementType).Attributes as IDBCollection : service.GetObjectType(this.elementType).VisibleAttributes as IDBCollection;
      if (this.attributableElement == AttributableElements.Relation)
        dbCollection = !byVisible ? service.GetRelationType(this.elementType).Attributes as IDBCollection : service.GetRelationType(this.elementType).VisibleAttributes as IDBCollection;
    }
    else
      dbCollection = (IDBCollection) service.GetAttributeTypeCollection(-1, byVisible);
    DataTable possibleAttributes = dbCollection.Select("F_ATTRIBUTE_ID");
    if (this.lockedAttributes.Count > 0)
    {
      int index1 = this.lockedAttributes.Count - 1;
      for (int index2 = possibleAttributes.Rows.Count - 1; index2 >= 0; --index2)
      {
        int int32 = Convert.ToInt32(possibleAttributes.Rows[index2]["F_ATTRIBUTE_ID"]);
        if (int32 <= this.lockedAttributes[index1])
        {
          if (int32 == this.lockedAttributes[index1])
            possibleAttributes.Rows.RemoveAt(index2);
          --index1;
          if (index1 < 0)
            break;
        }
      }
    }
    return possibleAttributes;
  }

  public IDBAttributable GetAttributable(IUserSession session)
  {
    return ClientCommons.GetAttributable(this.id, this.attributableElement, session);
  }

  public IDBAttributableTypeInfo GetAttributableType()
  {
    return ClientCommons.GetAttributableType(this.elementType, this.attributableElement);
  }

  public virtual DataTable GetPossibleValues(ITypeDescriptorContext context)
  {
    SimplePropDescriptor simplePropDescriptor = (SimplePropDescriptor) null;
    if (this.ObjectPropertyGrid != null)
    {
      GridItem selectedGridItem = this.ObjectPropertyGrid.SelectedGridItem;
      if (selectedGridItem != null)
        simplePropDescriptor = selectedGridItem.PropertyDescriptor as SimplePropDescriptor;
    }
    else if (context != null)
      simplePropDescriptor = context.PropertyDescriptor as SimplePropDescriptor;
    return simplePropDescriptor != null ? ClientCommons.GetPossibleValues(simplePropDescriptor.AttributeValuePropertyClass.AttributeValue.AttributeID) : (DataTable) null;
  }

  public static int GetAttributeValueListIndex(ArrayList list, int aAttributeID)
  {
    int attributeValueListIndex = -1;
    for (int index = 0; index < list.Count; ++index)
    {
      if (((AttributeValues) list[index]).AttributeID == aAttributeID)
      {
        attributeValueListIndex = index;
        break;
      }
    }
    return attributeValueListIndex;
  }

  public static AttributeValues GetAttributeValueListItem(ArrayList list, int aAttributeID)
  {
    int attributeValueListIndex = ObjectPropDescriptorHolder.GetAttributeValueListIndex(list, aAttributeID);
    return attributeValueListIndex >= 0 ? (AttributeValues) list[attributeValueListIndex] : (AttributeValues) null;
  }

  public int GetAttributeValueListIndex(int aAttributeID)
  {
    return ObjectPropDescriptorHolder.GetAttributeValueListIndex(this.attributeValuesList, aAttributeID);
  }

  public AttributeValues GetAttributeValueListItem(int aAttributeID)
  {
    return ObjectPropDescriptorHolder.GetAttributeValueListItem(this.attributeValuesList, aAttributeID);
  }

  private ArrayList CloneAttributeValueList(ArrayList list)
  {
    if (list == null)
      return (ArrayList) null;
    ArrayList arrayList = new ArrayList();
    for (int index = 0; index < list.Count; ++index)
      arrayList.Add(((AttributeValues) list[index]).Clone());
    return arrayList;
  }

  public int GetPdcGeneralListIndex(int aAttributeID)
  {
    int generalListIndex = -1;
    for (int index = 0; index < this.pdcGeneralList.Count; ++index)
    {
      if (((PropDescriptor) this.pdcGeneralList[index]).PropID == aAttributeID)
      {
        generalListIndex = index;
        break;
      }
    }
    return generalListIndex;
  }

  public bool AssignData(
    long aId,
    AttributableElements aAttributableElement,
    GetAttributeValuesModes aAttributeValuesModes,
    ObjectPropertyGrid aOPG,
    bool lockTypeChange,
    System.Type[] tabTypes)
  {
    this.lockTypeChange = lockTypeChange;
    this.tabTypes = tabTypes;
    this.visibleAId.Clear();
    this.deletedAId.Clear();
    this.loadedTabs.Clear();
    this.pdcGeneralList.Clear();
    this.originalAttributeValuesList.Clear();
    this.attributeValuesList.Clear();
    if (aId != -1L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributable attributable = ClientCommons.GetAttributable(aId, aAttributableElement, sessionKeeper.Session);
        if (attributable == null)
          return false;
        this.id = aId;
        this.attributableElement = aAttributableElement;
        this.attributeValuesModes = aAttributeValuesModes | ClientConsts.GetAttributeValuesModesMinimum;
        this.elementType = attributable.TypeID;
        this.anyAttributes = ClientCommons.GetAnyAttributesFlag(this.elementType, this.attributableElement);
        this.lockedAttributes.Clear();
        if (ServicesManager.GetService(typeof (IAttributesLockService)) is IAttributesLockService service)
        {
          this.lockedAttributes.AddRange((IEnumerable<int>) service.GetLockedAttributes(this.attributableElement, this.id, this.elementType));
          this.lockedAttributes.Sort();
        }
      }
      this.DropPropertyDescriptorCollection();
      this.objectPropertyGrid = aOPG;
      if (this.objectPropertyGrid == null || this.objectPropertyGrid.IsDisposed)
        return false;
      this.objectPropertyGrid.SelectedObject = (object) this;
    }
    else
    {
      this.id = 0L;
      this.attributableElement = aAttributableElement;
      this.attributeValuesModes = aAttributeValuesModes;
      this.elementType = 0;
      this.anyAttributes = false;
      this.DropPropertyDescriptorCollection();
      if (this.objectPropertyGrid != null)
        this.objectPropertyGrid.SelectedObject = (object) null;
      this.objectPropertyGrid = (ObjectPropertyGrid) null;
    }
    return true;
  }

  public PropDescriptor AttributeValuesToPropDescriptor(AttributeValues aAttributeValues)
  {
    int id = 0;
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    System.Type type = (System.Type) null;
    TypeConverter typeConverter = (TypeConverter) null;
    object editor = (object) null;
    bool ro = true;
    bool reset = false;
    string empty4 = string.Empty;
    bool disableManualEdit = false;
    if (!AttributeValuesEditor.GetPDAttributes((object) this, aAttributeValues, ref id, ref empty1, ref empty2, ref empty3, ref type, ref typeConverter, ref editor, ref ro, ref reset, ref empty4, ref disableManualEdit))
      return (PropDescriptor) null;
    PropDescriptor propDescriptor = (PropDescriptor) null;
    if (!ro && (this.attributableElement == AttributableElements.Object && id == -7 || this.attributableElement == AttributableElements.Relation && id == -23))
      ro = this.lockTypeChange;
    if (!ro && this.lockedAttributes.IndexOf(id) != -1)
      ro = true;
    if (type != (System.Type) null)
    {
      if (ListPropDescriptor.IsList(aAttributeValues))
      {
        propDescriptor = (PropDescriptor) new ListPropDescriptor(id, (object) this, empty1, (object) new AttributeValuesPropertyClass(aAttributeValues), typeof (AttributeValuesPropertyClass), (TypeConverter) new ObjectGridExpandableObjectConverter(), (object) null, empty3, empty2, ro, true, false, empty4, disableManualEdit);
      }
      else
      {
        DataTable possibleValues = (DataTable) null;
        if (MultiValueModesHelper.IsValuedFromList(aAttributeValues.MultipleValued))
          possibleValues = ClientCommons.GetPossibleValues(aAttributeValues.AttributeID);
        propDescriptor = (PropDescriptor) new SimplePropDescriptor(id, (object) this, empty1, AttributeValuesEditor.GetPDValue(aAttributeValues, 0, this.id, this.attributableElement, empty4, possibleValues), type, typeConverter, editor, empty3, empty2, ro, true, reset, empty4, disableManualEdit, new AttributeValuesPropertyClass(aAttributeValues));
      }
    }
    return propDescriptor;
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    pdc.Clear();
    PropertyDescriptorCollection descriptorCollection = this.ExtendPropDescriptorCollectionbyMode((object) this.objectPropertyGrid.PropertyTabByGuid(PropertiesTabCustom.PropertyTabGuid), this.attributeValuesModes, true);
    for (int index = 0; index < descriptorCollection.Count; ++index)
      pdc.Add(descriptorCollection[index]);
  }

  public PropDescriptor GetPropDescriptorByID(int aPropID)
  {
    for (int index = 0; index < this.pdcGeneralList.Count; ++index)
    {
      if (((PropDescriptor) this.pdcGeneralList[index]).PropID == aPropID)
        return (PropDescriptor) this.pdcGeneralList[index];
    }
    return (PropDescriptor) null;
  }

  private int GetPropDescriptorIndexByID(int aPropID)
  {
    for (int index = 0; index < this.pdcGeneralList.Count; ++index)
    {
      if (((PropDescriptor) this.pdcGeneralList[index]).PropID == aPropID)
        return index;
    }
    return -1;
  }

  public bool SaveData(out ArrayList origList, out ArrayList fireList)
  {
    origList = (ArrayList) null;
    fireList = (ArrayList) null;
    bool flag1 = true;
    if (this.pdcGeneralList.Count == 0 && this.deletedAId.Count == 0)
      return true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int aType = -1;
      IDBAttributable attributable = ClientCommons.GetAttributable(this.id, this.attributableElement, out aType, sessionKeeper.Session);
      if (attributable == null)
        return false;
      ArrayList arrayList = this.CollectAttributeValuesList();
      ArrayList list = (ArrayList) this.attributeValuesList.Clone();
      int index1 = 0;
      while (index1 < list.Count)
      {
        if (!(bool) arrayList[index1])
        {
          list.RemoveAt(index1);
          arrayList.RemoveAt(index1);
        }
        else
          ++index1;
      }
      for (int index2 = 0; index2 < this.deletedAId.Count; ++index2)
      {
        list.Add((object) new AttributeValues((int) this.deletedAId[index2], FieldTypes.ftUnknown, MultiValueModes.SingleValue, ComputeValueModes.NotComputableValue)
        {
          Values = new object[1]
          {
            (object) DeleteModesEnum.None
          }
        });
        arrayList.Add((object) true);
      }
      if (this.attributableElement == AttributableElements.Object)
      {
        IDocumentTypeSettingsService customService = sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService)) as IDocumentTypeSettingsService;
        if (customService.InheritedFromDocuments(sessionKeeper.Session.SessionGUID, aType))
        {
          int attributeId = sessionKeeper.Session.IdentHelper.GetAttributeID("cad0001f-306c-11d8-b4e9-00304f19f545");
          int attributeValueListIndex = ObjectPropDescriptorHolder.GetAttributeValueListIndex(list, attributeId);
          AttributeValues attributeValues = attributeValueListIndex != -1 ? (AttributeValues) list[attributeValueListIndex] : (AttributeValues) null;
          if (attributeValues != null)
          {
            DocumentTypeSettings settings = customService.GetSettings(sessionKeeper.Session.SessionGUID, aType);
            if (settings.DocumentTypeCodeInDesignation && settings.DocumentTypeCode != string.Empty)
            {
              string designation = Convert.ToString(attributeValues.Values[0]);
              attributeValues.Values[0] = (object) DocumentsHelper.AppendDocCode(sessionKeeper.Session, designation, settings.DocumentTypeCode);
            }
          }
        }
      }
      if (list.Count > 0)
      {
        AttributeValues[] array = (AttributeValues[]) list.ToArray(typeof (AttributeValues));
        AttributeProcessor.ReplacePasswordString(array);
        AttributeValues[] attributeValuesArray = attributable.SetAttributesValues(array, false, true, true, this.attributeValuesModes);
        for (int index3 = 0; index3 < this.pdcGeneralList.Count; ++index3)
        {
          if (this.pdcGeneralList[index3] is SimplePropDescriptor)
          {
            if (((PropDescriptor) this.pdcGeneralList[index3]).ValueChanged)
              ((PropDescriptor) this.pdcGeneralList[index3]).ValueChanged = false;
          }
          else if (this.pdcGeneralList[index3] is ListPropDescriptor && ((PropDescriptor) this.pdcGeneralList[index3]).ValueChanged)
            ((PropDescriptor) this.pdcGeneralList[index3]).ResetValueChanged((object) this);
        }
        if (attributeValuesArray != null)
        {
          bool flag2 = false;
          for (int index4 = 0; index4 < attributeValuesArray.Length; ++index4)
          {
            AttributeValues aAttributeValues = attributeValuesArray[index4];
            PropDescriptor propDescriptor = this.AttributeValuesToPropDescriptor(aAttributeValues);
            if (propDescriptor != null)
            {
              int attributeValueListIndex = this.GetAttributeValueListIndex(aAttributeValues.AttributeID);
              if (attributeValueListIndex != -1)
                this.attributeValuesList[attributeValueListIndex] = (object) aAttributeValues;
              PropDescriptor propDescriptorById = this.GetPropDescriptorByID(propDescriptor.PropID);
              if (propDescriptorById != null)
                this.pdcGeneralList.Remove((object) propDescriptorById);
              this.pdcGeneralList.Add((object) propDescriptor);
              flag2 = true;
            }
            int attributeValueListIndex1 = ObjectPropDescriptorHolder.GetAttributeValueListIndex(list, aAttributeValues.AttributeID);
            if (attributeValueListIndex1 == -1)
              list.Add(aAttributeValues.Clone());
            else
              list[attributeValueListIndex1] = aAttributeValues.Clone();
            int attributeValueListIndex2 = ObjectPropDescriptorHolder.GetAttributeValueListIndex(this.attributeValuesList, aAttributeValues.AttributeID);
            if (attributeValueListIndex2 == -1)
              this.attributeValuesList.Add(aAttributeValues.Clone());
            else
              this.attributeValuesList[attributeValueListIndex2] = aAttributeValues.Clone();
          }
          if (flag2)
          {
            this.DropPropertyDescriptorCollection();
            this.objectPropertyGrid.SelectedObject = (object) this;
            flag1 = true;
          }
        }
      }
      origList = this.originalAttributeValuesList;
      fireList = this.CloneAttributeValueList(list);
      this.originalAttributeValuesList = this.CloneAttributeValueList(this.attributeValuesList);
      this.deletedAId.Clear();
    }
    return flag1;
  }

  private bool AttributeExists(ArrayList lAttributeValuesList, int attributeID)
  {
    bool flag = false;
    for (int index = 0; index < lAttributeValuesList.Count; ++index)
    {
      if (((AttributeValues) lAttributeValuesList[index]).AttributeID == attributeID)
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  /// <summary>
  /// добавление property (атрибута).
  /// возвращает true если хоть один атрибут был добавлен виртуально
  /// directWriteOccured = true если хотя бы один атрибут был добавлен напрямую.
  /// </summary>
  /// <param name="aAttributeValues"></param>
  /// <param name="directWriteOccured"></param>
  /// <returns></returns>
  public bool AddProperty(AttributeValues[] aAttributeValues, out bool directWriteOccured)
  {
    return this.AddProperty(aAttributeValues, out directWriteOccured, false, false);
  }

  public bool AddProperty(
    AttributeValues[] aAttributeValues,
    out bool directWriteOccured,
    bool masterProcess,
    bool masterProcessEdit)
  {
    bool flag1 = false;
    bool flag2 = false;
    directWriteOccured = false;
    if (!masterProcess)
    {
      for (int index1 = 0; index1 < aAttributeValues.Length; ++index1)
      {
        if (this.AttributeExists(this.attributeValuesList, aAttributeValues[index1].AttributeID))
        {
          int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_966"), (object) aAttributeValues[index1].AttributeName));
        }
        else
        {
          if (aAttributeValues[index1].AttributeType == FieldTypes.ftAutoInc)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBAttributable attributable = this.GetAttributable(sessionKeeper.Session);
              if (attributable != null)
              {
                IDBAttribute dbAttribute = attributable.Attributes.AddAttribute(aAttributeValues[index1].AttributeID, true);
                if (dbAttribute != null)
                  aAttributeValues[index1].Values = (object[]) dbAttribute.Values.Clone();
                else
                  continue;
              }
              else
                continue;
            }
            directWriteOccured = true;
          }
          PropDescriptor propDescriptor = this.AttributeValuesToPropDescriptor(aAttributeValues[index1]);
          if (propDescriptor != null)
          {
            if (propDescriptor is ListPropDescriptor)
            {
              TypeConverter typeConverter = (TypeConverter) null;
              if (propDescriptor.Converter is ObjectGridExpandableObjectConverter)
                typeConverter = propDescriptor.Converter;
              else if (propDescriptor.Converter is TypeConvertorWrapper && ((TypeConvertorWrapper) propDescriptor.Converter).WrappedTypeConverter is ObjectGridExpandableObjectConverter)
                typeConverter = ((TypeConvertorWrapper) propDescriptor.Converter).WrappedTypeConverter;
              typeConverter?.GetProperties((ITypeDescriptorContext) null, (object) new BugFixObject(new object[2]
              {
                (object) this,
                (object) propDescriptor
              }));
            }
            if (aAttributeValues[index1].AttributeType == FieldTypes.ftAutoInc)
            {
              propDescriptor.ValueChanged = false;
            }
            else
            {
              propDescriptor.ValueChanged = true;
              flag2 = true;
            }
            int index2 = this.deletedAId.IndexOf((object) propDescriptor.PropID);
            if (index2 != -1)
              this.deletedAId.RemoveAt(index2);
            for (int index3 = 0; index3 < this.objectPropertyGrid.PropertyTabs.Count; ++index3)
            {
              IObjectPropertyGridTab propertyTab = (IObjectPropertyGridTab) this.objectPropertyGrid.PropertyTabs[index3];
              if (propertyTab != null)
              {
                ArrayList arrayList = (ArrayList) this.visibleAId[(object) propertyTab.TabGuid] ?? new ArrayList();
                arrayList.Add((object) aAttributeValues[index1].AttributeID);
                this.visibleAId[(object) propertyTab.TabGuid] = (object) arrayList;
              }
            }
            this.DropPropertyDescriptorCollection();
            this.attributeValuesList.Add((object) aAttributeValues[index1]);
            this.pdcGeneralList.Add((object) propDescriptor);
            flag1 = true;
          }
        }
      }
    }
    else
    {
      this.CollectAttributeValuesList();
      AttributeProcessor attributeProcessor = new AttributeProcessor();
      attributeProcessor.MemLoad(this.id, this.attributableElement, this.attributeValuesModes, this.elementType, this.anyAttributes, new Intermech.PropertyEditors.AttrProcessor.AttributeValuesList((IEnumerable<AttributeValues>) this.attributeValuesList.ToArray(typeof (AttributeValues))));
      for (int index = 0; index < aAttributeValues.Length; ++index)
      {
        AttributeValues aAttributeValue = aAttributeValues[index];
        if (aAttributeValue.AttributeType == FieldTypes.ftObjectLink && (aAttributeValue.MultipleValued == MultiValueModes.SingleValue || aAttributeValue.MultipleValued == MultiValueModes.SingleValueFromList))
        {
          AttributeValues byAttributeId = attributeProcessor.ActualAttributeValues.FindByAttributeID(aAttributeValue.AttributeID);
          if (byAttributeId == null || byAttributeId.Values == null || byAttributeId.Values.Length == 0)
          {
            ++index;
          }
          else
          {
            Intermech.PropertyEditors.AttrProcessor.AttributeValuesList deltaList = (Intermech.PropertyEditors.AttrProcessor.AttributeValuesList) null;
            attributeProcessor.AssignMasterAttributePrim(byAttributeId.AttributeID, byAttributeId.Values[0], attributeProcessor.ActualAttributeValues, false, out deltaList);
          }
        }
      }
      Intermech.PropertyEditors.AttrProcessor.AttributeValuesList attributeValuesList = new Intermech.PropertyEditors.AttrProcessor.AttributeValuesList();
      for (int index4 = 0; index4 < attributeProcessor.ActualAttributeValues.Count; ++index4)
      {
        AttributeValues attributeValueListItem = ObjectPropDescriptorHolder.GetAttributeValueListItem(this.attributeValuesList, attributeProcessor.ActualAttributeValues[index4].AttributeID);
        if (attributeValueListItem == null)
        {
          if (attributeProcessor.ActualAttributeValues[index4].AttributeType == FieldTypes.ftAutoInc)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBAttributable attributable = this.GetAttributable(sessionKeeper.Session);
              if (attributable != null)
              {
                IDBAttribute dbAttribute = attributable.Attributes.AddAttribute(attributeProcessor.ActualAttributeValues[index4].AttributeID, true);
                if (dbAttribute != null)
                  attributeProcessor.ActualAttributeValues[index4].Values = (object[]) dbAttribute.Values.Clone();
                else
                  continue;
              }
              else
                continue;
            }
            directWriteOccured = true;
          }
          PropDescriptor propDescriptor = this.AttributeValuesToPropDescriptor(attributeProcessor.ActualAttributeValues[index4]);
          if (propDescriptor != null)
          {
            if (propDescriptor is ListPropDescriptor)
            {
              TypeConverter typeConverter = (TypeConverter) null;
              if (propDescriptor.Converter is ObjectGridExpandableObjectConverter)
                typeConverter = propDescriptor.Converter;
              else if (propDescriptor.Converter is TypeConvertorWrapper && ((TypeConvertorWrapper) propDescriptor.Converter).WrappedTypeConverter is ObjectGridExpandableObjectConverter)
                typeConverter = ((TypeConvertorWrapper) propDescriptor.Converter).WrappedTypeConverter;
              typeConverter?.GetProperties((ITypeDescriptorContext) null, (object) new BugFixObject(new object[2]
              {
                (object) this,
                (object) propDescriptor
              }));
            }
            if (attributeProcessor.ActualAttributeValues[index4].AttributeType == FieldTypes.ftAutoInc)
            {
              propDescriptor.ValueChanged = false;
            }
            else
            {
              propDescriptor.ValueChanged = true;
              flag2 = true;
            }
            int index5 = this.deletedAId.IndexOf((object) propDescriptor.PropID);
            if (index5 != -1)
              this.deletedAId.RemoveAt(index5);
            for (int index6 = 0; index6 < this.objectPropertyGrid.PropertyTabs.Count; ++index6)
            {
              IObjectPropertyGridTab propertyTab = (IObjectPropertyGridTab) this.objectPropertyGrid.PropertyTabs[index6];
              if (propertyTab != null)
              {
                ArrayList arrayList = (ArrayList) this.visibleAId[(object) propertyTab.TabGuid] ?? new ArrayList();
                arrayList.Add((object) attributeProcessor.ActualAttributeValues[index4].AttributeID);
                this.visibleAId[(object) propertyTab.TabGuid] = (object) arrayList;
              }
            }
            this.DropPropertyDescriptorCollection();
            this.attributeValuesList.Add((object) attributeProcessor.ActualAttributeValues[index4]);
            this.pdcGeneralList.Add((object) propDescriptor);
            flag1 = true;
          }
        }
        else if (!attributeProcessor.ActualAttributeValues[index4].Equals(attributeValueListItem))
        {
          if (attributeProcessor.ActualAttributeValues[index4].AttributeType == FieldTypes.ftAutoInc)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBAttributable attributable = this.GetAttributable(sessionKeeper.Session);
              if (attributable != null)
              {
                attributable.SetAttributesValues(new AttributeValues[1]
                {
                  attributeProcessor.ActualAttributeValues[index4]
                });
                IDBAttribute attributeById = attributable.GetAttributeByID(attributeProcessor.ActualAttributeValues[index4].AttributeID);
                if (attributeById != null)
                  attributeProcessor.ActualAttributeValues[index4].Values = (object[]) attributeById.Values.Clone();
                else
                  continue;
              }
              else
                continue;
            }
            directWriteOccured = true;
          }
          AttributeValues actualAttributeValue = attributeProcessor.ActualAttributeValues[index4];
          PropDescriptor propDescriptor = this.AttributeValuesToPropDescriptor(actualAttributeValue);
          if (propDescriptor != null)
          {
            if (attributeProcessor.ActualAttributeValues[index4].AttributeType == FieldTypes.ftAutoInc)
            {
              propDescriptor.ValueChanged = false;
            }
            else
            {
              propDescriptor.ValueChanged = true;
              flag2 = true;
            }
            int attributeValueListIndex = this.GetAttributeValueListIndex(actualAttributeValue.AttributeID);
            if (attributeValueListIndex != -1)
              this.attributeValuesList[attributeValueListIndex] = (object) actualAttributeValue;
            PropDescriptor propDescriptorById = this.GetPropDescriptorByID(propDescriptor.PropID);
            if (propDescriptorById != null)
              this.pdcGeneralList.Remove((object) propDescriptorById);
            this.pdcGeneralList.Add((object) propDescriptor);
            this.DropPropertyDescriptorCollection();
            flag1 = true;
          }
        }
      }
    }
    if (flag1)
      this.objectPropertyGrid.SelectedObject = (object) this;
    return flag2;
  }

  /// <summary>
  /// собирает все измененные значения в attributeValuesList;
  /// возвращает список bool для attributeValuesList, показывающий, что значения назначены
  /// </summary>
  /// <returns></returns>
  private ArrayList CollectAttributeValuesList()
  {
    ArrayList arrayList1 = new ArrayList();
    for (int index = 0; index < this.attributeValuesList.Count; ++index)
      arrayList1.Add((object) false);
    ArrayList arrayList2 = new ArrayList();
    for (int index = 0; index < this.pdcGeneralList.Count; ++index)
    {
      if (this.pdcGeneralList[index] is SimplePropDescriptor && ((PropDescriptor) this.pdcGeneralList[index]).ValueChanged || this.pdcGeneralList[index] is ListPropDescriptor && ((PropDescriptor) this.pdcGeneralList[index]).ValueChanged)
      {
        bool flag1 = this.pdcGeneralList[index].GetType() == typeof (ListPropDescriptor);
        PropDescriptor pdcGeneral = (PropDescriptor) this.pdcGeneralList[index];
        int attributeValueListIndex = this.GetAttributeValueListIndex(pdcGeneral.PropID);
        if (attributeValueListIndex != -1)
        {
          bool flag2 = false;
          arrayList2.Clear();
          if (flag1)
          {
            if (((PropDescriptor) this.pdcGeneralList[index]).ValueChanged)
            {
              for (int lPropID = 0; lPropID < ((ListPropDescriptor) pdcGeneral).PdcList.Count; ++lPropID)
              {
                SimplePropDescriptor listItemByPropId = (SimplePropDescriptor) ((ListPropDescriptor) pdcGeneral).GetPdcListItemByPropID(lPropID);
                if (listItemByPropId != null)
                  arrayList2.Add(AttributeValuesEditor.GetAVValue((PropDescriptor) listItemByPropId, (AttributeValues) this.attributeValuesList[attributeValueListIndex], (object) this));
              }
              flag2 = true;
            }
          }
          else if (((PropDescriptor) this.pdcGeneralList[index]).ValueChanged)
          {
            arrayList2.Add(AttributeValuesEditor.GetAVValue(pdcGeneral, (AttributeValues) this.attributeValuesList[attributeValueListIndex], (object) this));
            flag2 = true;
          }
          if (flag2)
          {
            AttributeValues attributeValues = (AttributeValues) this.attributeValuesList[attributeValueListIndex];
            attributeValues.Values = arrayList2.ToArray();
            this.attributeValuesList[attributeValueListIndex] = (object) attributeValues;
            arrayList1[attributeValueListIndex] = (object) true;
          }
        }
      }
    }
    return arrayList1;
  }

  public bool AddListProperty(ListPropDescriptor aListPropDescriptor)
  {
    bool flag = false;
    AttributeValues attributeValue = ((AttributeValuesPropertyClass) aListPropDescriptor.GetValue((object) this)).AttributeValue;
    if (attributeValue.MultipleValued == MultiValueModes.MultiValues || attributeValue.MultipleValued == MultiValueModes.MultiValuesFromList)
    {
      int id = 0;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      System.Type type = (System.Type) null;
      TypeConverter typeConverter = (TypeConverter) null;
      object editor = (object) null;
      bool ro = true;
      bool reset = false;
      string empty4 = string.Empty;
      bool disableManualEdit = false;
      if (AttributeValuesEditor.GetPDAttributes((object) this, attributeValue, ref id, ref empty1, ref empty2, ref empty3, ref type, ref typeConverter, ref editor, ref ro, ref reset, ref empty4, ref disableManualEdit))
      {
        if (!ro && this.lockedAttributes.IndexOf(id) != -1)
          ro = true;
        SimplePropDescriptor simplePropDescriptor = new SimplePropDescriptor(attributeValue.Values.Length, (object) this, $"[{attributeValue.Values.Length.ToString(ClientConsts.MultiValueEnumerateFormat)}]", (object) null, type, typeConverter, editor, empty3, empty2, ro, true, reset, empty4, disableManualEdit, (AttributeValuesPropertyClass) null);
        simplePropDescriptor.ParentListPropDescriptor = aListPropDescriptor;
        aListPropDescriptor.PdcList.Add((PropertyDescriptor) simplePropDescriptor);
        simplePropDescriptor.ParentListPropDescriptor.ValueChanged = true;
        ArrayList arrayList = new ArrayList();
        arrayList.AddRange((ICollection) attributeValue.Values);
        arrayList.Add((object) null);
        attributeValue.Values = (object[]) arrayList.ToArray(typeof (object));
        for (int index = 0; index < aListPropDescriptor.PdcList.Count; ++index)
        {
          ((PropDescriptor) aListPropDescriptor.PdcList[index]).SetPropID(index);
          ((PropDescriptor) aListPropDescriptor.PdcList[index]).SetName($"[{index.ToString(ClientConsts.MultiValueEnumerateFormat)}]");
        }
        aListPropDescriptor.SetValue((object) this, (object) new AttributeValuesPropertyClass(attributeValue));
        this.objectPropertyGrid.SelectedObject = (object) this;
        flag = true;
      }
    }
    return flag;
  }

  /// <summary>
  /// удаление property (атрибута).
  /// возвращает true если хоть один атрибут был удален виртуально
  /// directWriteOccured = true если хотя бы один атрибут был удален напрямую.
  /// </summary>
  /// <param name="aRemovedDescriptor"></param>
  /// <param name="directWriteOccured"></param>
  /// <returns></returns>
  public bool DeleteProperty(PropDescriptor aRemovedDescriptor, out bool directWriteOccured)
  {
    bool flag1 = false;
    directWriteOccured = false;
    switch (aRemovedDescriptor)
    {
      case ListPropDescriptor _:
      case SimplePropDescriptor _ when ((SimplePropDescriptor) aRemovedDescriptor).ParentListPropDescriptor == null:
        int attributeValueListIndex = this.GetAttributeValueListIndex(aRemovedDescriptor.PropID);
        if (attributeValueListIndex != -1)
        {
          AttributeValues attributeValues = (AttributeValues) this.attributeValuesList[attributeValueListIndex];
          IDBAttributableTypeInfo attributableType = this.GetAttributableType();
          if (attributableType == null)
            return flag1;
          IDBAttributeTypeInfo4 attributeById1 = attributableType.Attributes.GetAttributeByID(attributeValues.AttributeID);
          if (attributeById1 != null && attributeById1.Required == RequiredModes.AutoRequired)
          {
            int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_967"), MessageDialogs.msgError);
            return flag1;
          }
          if (MessageBox.Show(MessageDialogs.msgReallyDelete, MessageDialogs.msgConfirmDelete, MessageBoxButtons.YesNo) != DialogResult.Yes)
            return flag1;
          bool flag2 = attributeValues.AttributeType == FieldTypes.ftAutoInc;
          if (flag2)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBAttributable attributable = this.GetAttributable(sessionKeeper.Session);
              if (attributable == null)
                return flag1;
              IDBAttribute attributeById2 = attributable.GetAttributeByID(attributeValues.AttributeID);
              if (attributeById2 == null)
                return flag1;
              attributeById2.Delete(0L);
            }
            directWriteOccured = true;
          }
          if (this.deletedAId.IndexOf((object) aRemovedDescriptor.PropID) == -1)
            this.deletedAId.Add((object) aRemovedDescriptor.PropID);
          for (int index1 = 0; index1 < this.objectPropertyGrid.PropertyTabs.Count; ++index1)
          {
            IObjectPropertyGridTab propertyTab = (IObjectPropertyGridTab) this.objectPropertyGrid.PropertyTabs[index1];
            if (propertyTab != null)
            {
              ArrayList arrayList1 = (ArrayList) this.visibleAId[(object) propertyTab.TabGuid];
              if (arrayList1 != null)
              {
                int index2 = arrayList1.IndexOf((object) aRemovedDescriptor.PropID);
                if (index2 != -1)
                  arrayList1.RemoveAt(index2);
                if (arrayList1.Count == 0 && this.loadedTabs.IndexOf((object) propertyTab.TabGuid) == -1)
                {
                  ArrayList arrayList2 = (ArrayList) null;
                  this.visibleAId[(object) propertyTab.TabGuid] = (object) arrayList2;
                }
              }
            }
          }
          this.attributeValuesList.RemoveAt(attributeValueListIndex);
          PropDescriptor propDescriptorById = this.GetPropDescriptorByID(aRemovedDescriptor.PropID);
          if (propDescriptorById != null)
          {
            this.pdcGeneralList.Remove((object) propDescriptorById);
            this.DropPropertyDescriptorCollection();
          }
          this.objectPropertyGrid.SelectedObject = (object) this;
          if (!flag2)
          {
            flag1 = true;
            break;
          }
          break;
        }
        break;
      case SimplePropDescriptor _ when ((SimplePropDescriptor) aRemovedDescriptor).ParentListPropDescriptor != null:
        ListPropDescriptor listPropDescriptor = ((SimplePropDescriptor) aRemovedDescriptor).ParentListPropDescriptor;
        AttributeValues attributeValue = ((AttributeValuesPropertyClass) listPropDescriptor.GetValue((object) this)).AttributeValue;
        if (attributeValue.Values.Length > 1)
        {
          int propId = aRemovedDescriptor.PropID;
          ArrayList arrayList = new ArrayList();
          arrayList.AddRange((ICollection) attributeValue.Values);
          arrayList.RemoveAt(propId);
          attributeValue.Values = (object[]) arrayList.ToArray(typeof (object));
          listPropDescriptor.SetValue((object) this, (object) new AttributeValuesPropertyClass(attributeValue));
          listPropDescriptor.PdcList = PropDescriptorHolder.RemovePDCItem(listPropDescriptor.PdcList, propId);
          for (int index = 0; index < listPropDescriptor.PdcList.Count; ++index)
          {
            ((PropDescriptor) listPropDescriptor.PdcList[index]).SetPropID(index);
            ((PropDescriptor) listPropDescriptor.PdcList[index]).SetName($"[{index.ToString(ClientConsts.MultiValueEnumerateFormat)}]");
          }
          listPropDescriptor.ValueChanged = true;
          this.objectPropertyGrid.SelectedObject = (object) this;
          flag1 = true;
          break;
        }
        break;
    }
    return flag1;
  }

  public long ElementIdentifier => this.id;

  public AttributableElements ElementKind => this.attributableElement;

  public int ElementType => this.elementType;

  public bool CheckAttributeLock(int attrId)
  {
    bool flag = false;
    if ((this.cachedLockedAttrsList == null || this.cachedLockedAttrsList != null && (this.cachedId != this.ElementIdentifier || this.cachedKind != this.ElementKind || this.cachedType != this.ElementType)) && ServicesManager.ServiceContainer.GetService(typeof (IAttributesLockService)) is IAttributesLockService service)
    {
      this.cachedLockedAttrsList = new List<int>((IEnumerable<int>) service.GetLockedAttributes(this.ElementKind, this.ElementIdentifier, this.ElementType));
      this.cachedId = this.ElementIdentifier;
      this.cachedKind = this.ElementKind;
      this.cachedType = this.ElementType;
    }
    if (this.cachedLockedAttrsList != null)
      flag = this.cachedLockedAttrsList.IndexOf(attrId) != -1;
    return flag;
  }

  protected override AttributeCollection ExtendAttributes(AttributeCollection attributes)
  {
    ArrayList arrayList = new ArrayList((ICollection) attributes);
    if (this.tabTypes != null)
    {
      Attribute attribute = (Attribute) new PropertyTabAttribute4OPG(this.tabTypes);
      arrayList.Add((object) attribute);
    }
    return new AttributeCollection((Attribute[]) arrayList.ToArray(typeof (Attribute)));
  }

  public PropertyDescriptorCollection ExtendPropDescriptorCollectionbyMode(
    object component,
    GetAttributeValuesModes avm,
    bool hideIfNotInMode)
  {
    if (!(component is IObjectPropertyGridTab objectPropertyGridTab))
      return (PropertyDescriptorCollection) null;
    ArrayList arrayList1 = new ArrayList();
    if (this.loadedTabs.IndexOf((object) objectPropertyGridTab.TabGuid) == -1)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributable attributable = this.GetAttributable(sessionKeeper.Session);
        if (attributable != null)
        {
          ArrayList arrayList2 = (ArrayList) this.visibleAId[(object) objectPropertyGridTab.TabGuid];
          if (arrayList2 != null)
          {
            for (int index = 0; index < arrayList2.Count; ++index)
            {
              int generalListIndex = this.GetPdcGeneralListIndex((int) arrayList2[index]);
              if (generalListIndex != -1)
                arrayList1.Add(this.pdcGeneralList[generalListIndex]);
            }
          }
          else
            arrayList2 = new ArrayList();
          if (this.loadedTabs.IndexOf((object) PropertiesTabCustom.PropertyTabGuid) == -1 && (avm & GetAttributeValuesModes.IncludeOnlyInvisible) != GetAttributeValuesModes.None)
            avm &= ~GetAttributeValuesModes.IncludeOnlyInvisible;
          AttributeValues[] attributesValues = attributable.GetAttributesValues(avm);
          if (!objectPropertyGridTab.TabGuid.Equals(PropertiesTabCustom.PropertyTabGuid) && this.loadedTabs.IndexOf((object) PropertiesTabCustom.PropertyTabGuid) != -1 && (avm & GetAttributeValuesModes.IncludeOnlyInvisible) != GetAttributeValuesModes.None)
          {
            ArrayList arrayList3 = (ArrayList) this.visibleAId[(object) PropertiesTabCustom.PropertyTabGuid];
            for (int index = 0; index < arrayList3.Count; ++index)
            {
              if (this.deletedAId.IndexOf((object) (int) arrayList3[index]) == -1)
              {
                int generalListIndex = this.GetPdcGeneralListIndex((int) arrayList3[index]);
                if (generalListIndex != -1)
                  arrayList1.Add(this.pdcGeneralList[generalListIndex]);
                arrayList2.Add((object) (int) arrayList3[index]);
              }
            }
          }
          for (int index = 0; index < attributesValues.Length; ++index)
          {
            if (arrayList2.IndexOf((object) attributesValues[index].AttributeID) == -1 && this.deletedAId.IndexOf((object) attributesValues[index].AttributeID) == -1)
            {
              if (this.GetAttributeValueListIndex(attributesValues[index].AttributeID) == -1)
              {
                this.attributeValuesList.Add((object) attributesValues[index]);
                this.originalAttributeValuesList.Add(attributesValues[index].Clone());
                PropDescriptor propDescriptor = this.AttributeValuesToPropDescriptor(attributesValues[index]);
                if (propDescriptor != null)
                {
                  this.pdcGeneralList.Add((object) propDescriptor);
                  arrayList1.Add((object) propDescriptor);
                }
              }
              else
              {
                int generalListIndex = this.GetPdcGeneralListIndex(attributesValues[index].AttributeID);
                if (generalListIndex != -1)
                  arrayList1.Add(this.pdcGeneralList[generalListIndex]);
              }
              arrayList2.Add((object) attributesValues[index].AttributeID);
            }
          }
          this.visibleAId[(object) objectPropertyGridTab.TabGuid] = (object) arrayList2;
          this.loadedTabs.Add((object) objectPropertyGridTab.TabGuid);
        }
      }
    }
    else
    {
      ArrayList arrayList4 = (ArrayList) this.visibleAId[(object) objectPropertyGridTab.TabGuid];
      for (int index = 0; index < arrayList4.Count; ++index)
      {
        if (this.deletedAId.IndexOf((object) (int) arrayList4[index]) == -1)
        {
          int generalListIndex = this.GetPdcGeneralListIndex((int) arrayList4[index]);
          if (generalListIndex != -1)
            arrayList1.Add(this.pdcGeneralList[generalListIndex]);
        }
      }
    }
    return new PropertyDescriptorCollection((PropertyDescriptor[]) arrayList1.ToArray(typeof (PropDescriptor)));
  }

  [SpecialName]
  PropertyDescriptorCollection IObjectPropDescriptorHolder.get_PropDescriptorCollection()
  {
    return this.PropDescriptorCollection;
  }
}
