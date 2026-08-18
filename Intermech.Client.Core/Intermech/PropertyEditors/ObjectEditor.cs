
// Type: Intermech.PropertyEditors.ObjectEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Extensions;
using Intermech.Holders;
using Intermech.Imbase;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ObjectEditor.</summary>
public class ObjectEditor : UITypeEditor
{
  /// <summary>
  /// обработка значения "Текущий пользователь" - только для конфигуратора БД
  /// </summary>
  public bool CurrentUserCustomProcessing;
  /// <summary>
  /// Множественный выбор.
  /// до 19.02.12 MultiSelect отсутствовал и выбор по умолчанию был множественный, но, похоже, более правильным будет false
  /// </summary>
  public bool MultiSelect;
  /// <summary>
  /// может быть применен множественный выбор (то есть редактирование вне ObjectPropertyGrid).
  /// 
  /// protected, чтобы callback-и, вызываемые из Edit(), могли понять, что даже при имеющемся MultiValued у атрибута редактироваться будет только одно значение.
  /// </summary>
  protected bool MultiSelectCanApplied;
  protected EventsHolder.GetListDelegate getObjTypeList;
  protected ArrayList objTypeList;
  protected int attributeId;
  protected ArrayList mainObjTypeList;
  /// <summary>
  /// флаг обработки версии объектов по VersionID или объектов по ID
  /// </summary>
  protected bool objectVersionProcessed = true;

  public ObjectEditor(bool _objectVersionProcessed = true)
  {
    this.objectVersionProcessed = _objectVersionProcessed;
  }

  /// <summary>Инициализация списка типов объектов через событие.</summary>
  /// <param name="aGetObjTypeList"></param>
  public ObjectEditor(EventsHolder.GetListDelegate aGetObjTypeList, bool _objectVersionProcessed = true)
  {
    this.getObjTypeList = aGetObjTypeList;
    this.objectVersionProcessed = _objectVersionProcessed;
  }

  /// <summary>Инициализация списка типов объектов напрямую.</summary>
  /// <param name="aObjTypeList"></param>
  public ObjectEditor(ArrayList aObjTypeList, bool _objectVersionProcessed = true)
  {
    this.objTypeList = aObjTypeList;
    this.objectVersionProcessed = _objectVersionProcessed;
  }

  /// <summary>Инициализация списка типов объектов id атрибута.</summary>
  /// <param name="aAttributeId"></param>
  public ObjectEditor(int aAttributeId)
  {
    this.attributeId = aAttributeId;
    this.InitAttrData();
  }

  public ObjectEditor(int aAttributeId, FieldTypes aAttributeType)
  {
    this.attributeId = aAttributeId;
    this.objectVersionProcessed = aAttributeType == FieldTypes.ftObjectLink;
  }

  private void InitAttrData()
  {
    if (this.attributeId == 0)
      return;
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(this.attributeId);
    this.objectVersionProcessed = attributeType.AttributeType == FieldTypes.ftObjectLink;
    this.MultiSelect = attributeType.MultipleValued == MultiValueModes.MultiValues || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return context != null && context.PropertyDescriptor != null && context.PropertyDescriptor.IsReadOnly ? UITypeEditorEditStyle.None : UITypeEditorEditStyle.Modal;
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
    this.MultiSelectCanApplied = this.MultiSelect && (context == null || !(context.PropertyDescriptor is SimplePropDescriptor));
    long num1 = 0;
    if (value != null && value != DBNull.Value && value is ObjectPropertyClass)
      num1 = (value as ObjectPropertyClass).ObjectID;
    if (ServicesManager.GetService(typeof (IImbaseFilterSelector)) is IImbaseFilterSelector service)
    {
      int result = this.attributeId;
      int objTypeID = -1;
      if (context != null)
      {
        if (context.Instance is AttributeFolder)
        {
          if (result == 0)
          {
            AttributeFolder instance = context.Instance as AttributeFolder;
            if (instance.Id != null)
              int.TryParse(instance.Id.ToString(), out result);
          }
        }
        else if (context.Instance is Attr4ObjTypeClass)
        {
          Attr4ObjTypeClass instance = context.Instance as Attr4ObjTypeClass;
          if (result == 0)
            result = instance.AttributeID;
          objTypeID = instance.Attribute4ObjectTypeProperties.ObjectType;
        }
        else if (context.Instance is Attr4RelTypeClass)
        {
          Attr4RelTypeClass instance = context.Instance as Attr4RelTypeClass;
          if (result == 0)
            result = instance.AttributeID;
          objTypeID = instance.Attribute4RelationTypeProperties.RelationType;
        }
        else if (context.Instance is ObjectPropDescriptorHolder)
        {
          ObjectPropDescriptorHolder instance = (ObjectPropDescriptorHolder) context.Instance;
          if (instance.ElementKind == AttributableElements.Object)
            objTypeID = instance.ElementType;
        }
      }
      if (result != 0)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          ExtendedServiceHelper.ObjTypeInfo objTypeData = ExtendedServiceHelper.GetObjTypeData(objTypeID, sessionKeeper.Session);
          if (objTypeData != null)
          {
            ImbaseExtendedItem imbaseExtendedItem = objTypeData.GetValue(result, sessionKeeper.Session);
            if (imbaseExtendedItem != null)
            {
              if (imbaseExtendedItem.SelectMode != ImbaseCatalogSelectMode.imcmNone)
              {
                List<long> catalogIds = imbaseExtendedItem.CatalogIDs;
                ImbaseCatalogSelectMode selectMode = imbaseExtendedItem.SelectMode;
                if (catalogIds != null)
                {
                  if (catalogIds.Count != 0)
                  {
                    long aObjectID = service.SelectImbaseObject(catalogIds, (int[]) null, 0L, num1, selectMode, _objectVersionProcessed: this.objectVersionProcessed);
                    return aObjectID == 0L || num1 == aObjectID ? value : (object) new ObjectPropertyClass(aObjectID, this.objectVersionProcessed);
                  }
                }
              }
            }
          }
        }
      }
    }
    this.mainObjTypeList = (ArrayList) null;
    if (this.objTypeList != null)
      this.mainObjTypeList = this.objTypeList;
    if (this.mainObjTypeList == null && this.getObjTypeList != null)
      this.mainObjTypeList = this.getObjTypeList((object) this);
    if (this.mainObjTypeList == null && this.attributeId != 0)
      this.mainObjTypeList = ObjectEditor.GetObjTypeListByAttrId(this.attributeId);
    if (this.mainObjTypeList == null)
      this.mainObjTypeList = new ArrayList();
    if (this.mainObjTypeList.Count == 0)
      this.mainObjTypeList.Add((object) -1);
    if (this.CurrentUserCustomProcessing && this.mainObjTypeList.IndexOf((object) -1) == -1)
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00002-306c-11d8-b4e9-00304f19f545"));
      bool flag = false;
      for (int index = 0; index < this.mainObjTypeList.Count; ++index)
      {
        if (childrenIdRecursive.IndexOf(Convert.ToInt32(this.mainObjTypeList[index])) != -1)
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        switch (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_CurrentUserQuery"), LocalizationHolder.rm.GetString("Client.Core_CurrentUserQueryCaption"), MessageBoxButtons.YesNoCancel))
        {
          case DialogResult.Cancel:
            return value;
          case DialogResult.Yes:
            return (object) new ObjectPropertyClass(ObjectPropertyClassVariant.opcvCurrentUser, this.objectVersionProcessed);
        }
      }
    }
    IDBObjectID[] dbObjectIdArray = (IDBObjectID[]) null;
    if (this.attributeId == MetaDataHelper.GetAttributeTypeID(new Guid("cad00815-306c-11d8-b4e9-00304f19f545")))
    {
      if (context.Instance is ObjectPropDescriptorHolder)
      {
        ObjectPropDescriptorHolder instance = (ObjectPropDescriptorHolder) context.Instance;
        if (instance.ElementKind == AttributableElements.Object)
        {
          int elementType = instance.ElementType;
          List<long> forTypeTemplates = ObjectEditor.GetAssignedForTypeTemplates(elementType);
          string objectTypeName = MetaDataHelper.GetObjectTypeName(elementType);
          if (forTypeTemplates.Count == 0)
          {
            int num2 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("TemplateIsAbsenceError"), (object) objectTypeName), LocalizationHolder.rm.GetString("Client.Core_281"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
          else
          {
            IDescriptor rootDescriptor = (IDescriptor) new ListDescriptor(Intermech.Navigator.Consts.CategoryAllObjectTypes, -1, string.Format(LocalizationHolder.rm.GetString("TemplatesForType"), (object) objectTypeName), (IList) forTypeTemplates);
            object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("ChooseTemplate"), rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.HideTree | SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
            if (objArray != null && objArray.Length == 1)
              dbObjectIdArray = new IDBObjectID[1]
              {
                objArray[0] as IDBObjectID
              };
          }
        }
      }
    }
    else
      dbObjectIdArray = this.GetObjectsIDs(num1, this.objectVersionProcessed);
    if (dbObjectIdArray != null)
    {
      if (!this.MultiSelect || !this.MultiSelectCanApplied)
      {
        value = (object) new ObjectPropertyClass(this.objectVersionProcessed ? dbObjectIdArray[0].Value : dbObjectIdArray[0].ID, this.objectVersionProcessed);
      }
      else
      {
        List<ObjectPropertyClass> objectPropertyClassList = new List<ObjectPropertyClass>();
        foreach (IDBObjectID dbObjectId in dbObjectIdArray)
          objectPropertyClassList.Add(new ObjectPropertyClass(this.objectVersionProcessed ? dbObjectId.Value : dbObjectId.ID, this.objectVersionProcessed));
        value = (object) objectPropertyClassList.ToArray();
      }
    }
    return value;
  }

  /// <summary>
  /// Получить шаблоны, назначенные на тип объекта. Учитывает родительские типы.
  /// </summary>
  /// <param name="attrOwnerTypeID">Тип объекта, которому назначаем атрибут</param>
  /// <returns>Шаблоны, назначенные на тип объекта. Учитывает родительские типы.</returns>
  private static List<long> GetAssignedForTypeTemplates(int attrOwnerTypeID)
  {
    List<long> forTypeTemplates;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<Guid> objectTypeParentsGuid = MetaDataHelper.GetObjectTypeParentsGuid(attrOwnerTypeID);
      List<int> childrenIdRecursive1 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(attrOwnerTypeID);
      objectTypeParentsGuid.SafeAdd<Guid>(MetaDataHelper.GetObjectTypeGuid(attrOwnerTypeID));
      int num = attrOwnerTypeID;
      childrenIdRecursive1.SafeAdd<int>(num);
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(MetaDataHelper.GetObjectTypeID(new Guid("cad00813-306c-11d8-b4e9-00304f19f545")));
      ColumnDescriptor[] columns = new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
      };
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      if (attrOwnerTypeID == MetaDataHelper.GetObjectTypeID(new Guid("cad00812-306c-11d8-b4e9-00304f19f545")) || MetaDataHelper.IsObjectTypeChildOf(attrOwnerTypeID, new Guid("cad00812-306c-11d8-b4e9-00304f19f545")))
      {
        int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid("cad00813-306c-11d8-b4e9-00304f19f545"));
        List<int> childrenIdRecursive2 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId);
        childrenIdRecursive2.SafeAdd<int>(objectTypeId);
        conditionStructureList.Add(new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive2.ToArray(), LogicalOperators.OR, 0, false));
      }
      else
        conditionStructureList.Add(new ConditionStructure(new Guid("cad001a0-306c-11d8-b4e9-00304f19f545"), RelationalOperators.In, (object) objectTypeParentsGuid.Cast<object>().ToArray<object>(), LogicalOperators.NONE, 0));
      DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureList.ToArray(), columns);
      DataTable dataTable = objectCollection.Select(paramSet);
      forTypeTemplates = new List<long>();
      if (dataTable == null)
      {
        if (dataTable.Rows.Count <= 0)
          goto label_16;
      }
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        forTypeTemplates.Add(int64);
      }
    }
label_16:
    return forTypeTemplates;
  }

  /// <summary>
  /// Возвращает массив идентификаторов версий объектов, выбранных в SelectorForm.
  /// </summary>
  /// <param name="newValue">Выбранный ранее идентификатор версии объекта</param>
  /// <returns></returns>
  protected virtual IDBObjectID[] GetObjectsIDs(long newValue, bool _objectVersionProcessed = true)
  {
    return SelectorForm.SelectObjects((int[]) this.mainObjTypeList.ToArray(typeof (int)), new long[1]
    {
      newValue
    }, (this.MultiSelect ? 1 : 0) != 0, true, true, (_objectVersionProcessed ? 1 : 0) != 0);
  }

  /// <summary>
  /// По id атрибута типа Ссылка на объект возвращаем список типов объектов для выбора; -1 - все типы.
  /// </summary>
  /// <param name="attrId"></param>
  /// <returns></returns>
  public static ArrayList GetObjTypeListByAttrId(int attrId)
  {
    ArrayList typeListByAttrId = (ArrayList) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attrId);
      if (attributeType != null)
      {
        if (attributeType.AttributeType != FieldTypes.ftObjectLink && attributeType.AttributeType != FieldTypes.ftObjectLinkByID)
        {
          if (attributeType.AttributeType == FieldTypes.ftSystem)
          {
            if (!MetaDataHelper.IsSystemAttributeSupportsObjectLinks(attributeType.GUID))
              goto label_15;
          }
          else
            goto label_15;
        }
        int int32 = Convert.ToInt32(attributeType.SizeType);
        int[] c = (int[]) null;
        if (int32 == 0)
        {
          AttributeTypeProperties propertiesStructure = attributeType.PropertiesStructure;
          if (propertiesStructure.MetadataExtensions[(object) "OBJ_LINKS_ID"] != null)
          {
            propertiesStructure = attributeType.PropertiesStructure;
            c = (int[]) propertiesStructure.MetadataExtensions[(object) "OBJ_LINKS_ID"];
          }
          if (c == null || c.Length == 0)
            c = new int[1]{ -1 };
        }
        else
          c = new int[1]{ int32 };
        typeListByAttrId = new ArrayList((ICollection) c);
      }
    }
label_15:
    return typeListByAttrId;
  }
}
