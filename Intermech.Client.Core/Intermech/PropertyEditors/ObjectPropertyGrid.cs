
// Type: Intermech.PropertyEditors.ObjectPropertyGrid
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.PropertyEditors;

/// <summary>
/// Класс для редактирования атрибутов объектов и связей.
/// Унаследован от класса PropertyGrid.
/// </summary>
public class ObjectPropertyGrid : PropertyGrid
{
  private bool blockOnValueChange;
  private bool blockOnPropertyTabChange;
  private bool blockOnMasterAssign;
  private string safeGridItemLabel = string.Empty;
  private GridItemType safeGridItemType;
  internal ObjectPropDescriptorHolder objPDH = new ObjectPropDescriptorHolder();
  private string objVerIdString = "verid";
  private string objIdString = "id";
  private bool isChanged;
  /// <summary>
  /// При установке значения в true не дает изменять тип объекта. По умолчанию значение свойства false.
  /// </summary>
  private bool lockTypeChange;
  private System.Type[] tabTypes;
  private bool internalMenuEnabled = true;
  private bool needBaseCallback;
  public static readonly Guid AddMenuItemId = Guid.NewGuid();
  public static readonly Guid DeleteMenuItemId = Guid.NewGuid();
  public static readonly Guid OpenObjMenuItemId = Guid.NewGuid();
  private ContextMenu contextMenu;
  private ContextMenu contextMenuSafe;
  private MenuItemExt addMenuItem;
  private MenuItemExt deleteMenuItem;
  private MenuItemExt showObjMenuItem;

  [Browsable(false)]
  public long Id => this.objPDH.Id;

  [Browsable(false)]
  public AttributableElements AttributableElement => this.objPDH.AttributableElement;

  [Browsable(false)]
  public GetAttributeValuesModes AttributeValuesModes => this.objPDH.AttributeValuesModes;

  [Browsable(false)]
  public bool IsChanged => this.isChanged;

  /// <summary>
  /// При установке значения в true не дает изменять тип объекта. По умолчанию значение свойства false.
  /// </summary>
  public bool LockTypeChange
  {
    get => this.lockTypeChange;
    set => this.lockTypeChange = value;
  }

  public event ObjectPropertyGrid.GridChangedDelegate GridChanged;

  public bool InternalMenuEnabled
  {
    get => this.internalMenuEnabled;
    set
    {
      this.internalMenuEnabled = value;
      if (this.internalMenuEnabled)
        this.ContextMenu = this.contextMenu;
      else
        this.ContextMenu = (ContextMenu) null;
    }
  }

  public override ContextMenu ContextMenu
  {
    get => base.ContextMenu == this.contextMenu ? (ContextMenu) null : base.ContextMenu;
    set => base.ContextMenu = value;
  }

  private void CreateContextMenu()
  {
    this.addMenuItem = new MenuItemExt(LocalizationHolder.rm.GetString("Client.Core_94a"), new EventHandler(this.OnAddMenuItem), (object) ObjectPropertyGrid.AddMenuItemId);
    this.deleteMenuItem = new MenuItemExt(LocalizationHolder.rm.GetString("Client.Core_96"), new EventHandler(this.OnDeleteMenuItem), (object) ObjectPropertyGrid.DeleteMenuItemId);
    this.showObjMenuItem = new MenuItemExt(LocalizationHolder.rm.GetString("Client.Core_ShowObj"), new EventHandler(this.OnShowObjMenuItem), (object) ObjectPropertyGrid.OpenObjMenuItemId);
    this.contextMenu = new ContextMenu();
  }

  private MenuItemExt GetMenuItemByTag(object tag, ContextMenu aContextMenu)
  {
    for (int index = 0; index < aContextMenu.MenuItems.Count; ++index)
    {
      if (aContextMenu.MenuItems[index] is MenuItemExt && ((MenuItemExt) aContextMenu.MenuItems[index]).Tag.Equals(tag))
        return (MenuItemExt) aContextMenu.MenuItems[index];
    }
    return (MenuItemExt) null;
  }

  private void PlugContextMenuItems(ContextMenu aContextMenu)
  {
    if (aContextMenu == null)
      return;
    aContextMenu.Popup += new EventHandler(this.contextMenu_Popup);
    if (this.GetMenuItemByTag((object) ObjectPropertyGrid.AddMenuItemId, aContextMenu) == null)
      aContextMenu.MenuItems.Add(0, (MenuItem) this.addMenuItem);
    if (this.GetMenuItemByTag((object) ObjectPropertyGrid.DeleteMenuItemId, aContextMenu) == null)
      aContextMenu.MenuItems.Add(1, (MenuItem) this.deleteMenuItem);
    if (this.GetMenuItemByTag((object) ObjectPropertyGrid.OpenObjMenuItemId, aContextMenu) != null)
      return;
    aContextMenu.MenuItems.Add(2, (MenuItem) this.showObjMenuItem);
  }

  private void UnplugContextMenuItems(ContextMenu aContextMenu)
  {
    if (aContextMenu == null)
      return;
    aContextMenu.Popup -= new EventHandler(this.contextMenu_Popup);
    MenuItemExt menuItemByTag1 = this.GetMenuItemByTag((object) ObjectPropertyGrid.AddMenuItemId, aContextMenu);
    MenuItemExt menuItemByTag2 = this.GetMenuItemByTag((object) ObjectPropertyGrid.DeleteMenuItemId, aContextMenu);
    MenuItemExt menuItemByTag3 = this.GetMenuItemByTag((object) ObjectPropertyGrid.OpenObjMenuItemId, aContextMenu);
    if (menuItemByTag1 != null)
      aContextMenu.MenuItems.Remove((MenuItem) menuItemByTag1);
    if (menuItemByTag2 != null)
      aContextMenu.MenuItems.Remove((MenuItem) menuItemByTag2);
    if (menuItemByTag3 == null)
      return;
    aContextMenu.MenuItems.Remove((MenuItem) menuItemByTag3);
  }

  public PropertyTab PropertyTabByGuid(Guid guid)
  {
    PropertyTab propertyTab = (PropertyTab) null;
    for (int index = 0; index < this.PropertyTabs.Count; ++index)
    {
      if (this.PropertyTabs[index] is IObjectPropertyGridTab && ((IObjectPropertyGridTab) this.PropertyTabs[index]).TabGuid.Equals(guid))
      {
        propertyTab = this.PropertyTabs[index];
        break;
      }
    }
    return propertyTab;
  }

  private void OnAddMenuItem(object sender, EventArgs args)
  {
    if (this.SelectedGridItem != null && this.SelectedGridItem.PropertyDescriptor is SimplePropDescriptor && ((SimplePropDescriptor) this.SelectedGridItem.PropertyDescriptor).ParentListPropDescriptor != null)
    {
      if (!this.objPDH.AddListProperty(((SimplePropDescriptor) this.SelectedGridItem.PropertyDescriptor).ParentListPropDescriptor))
        return;
      this.isChanged = true;
      if (this.GridChanged == null)
        return;
      this.GridChanged((object) this, new GridChangedEventArgs(this.isChanged, false));
    }
    else
    {
      DataTable possibleAttributes = this.objPDH.GetPossibleAttributes(!this.objPDH.AnyAttributes, false);
      AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true);
      List<int> intList = new List<int>();
      intList.Add(this.objPDH.ElementType);
      if (this.objPDH.ElementKind == AttributableElements.Object)
        attributesSelectDlg.LoadAttrDialogForObjectsTypes(intList);
      if (this.objPDH.ElementKind == AttributableElements.Relation)
        attributesSelectDlg.LoadAttrDialogForRelationsTypes(intList);
      attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[4]
      {
        FieldTypes.ftBlob,
        FieldTypes.ftShortBlob,
        FieldTypes.ftFile,
        FieldTypes.ftSystem
      });
      if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count <= 0)
        return;
      ArrayList arrayList = new ArrayList();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        for (int index = 0; index < attributesSelectDlg.SelectedAttributesID.Count; ++index)
        {
          DataRow[] dataRowArray = (DataRow[]) null;
          if (possibleAttributes != null)
            dataRowArray = possibleAttributes.Select("F_ATTRIBUTE_ID=" + attributesSelectDlg.SelectedAttributesID[index].ToString());
          if (possibleAttributes == null || dataRowArray.Length != 0)
          {
            IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(Convert.ToInt32(attributesSelectDlg.SelectedAttributesID[index]));
            AttributeValues attributeValues = new AttributeValues(attributeType.AttributeID, attributeType.AttributeType, attributeType.MultipleValued, attributeType.Computed);
            attributeValues.AttributeName = attributeType.Name;
            object[] objArray1 = new object[1];
            object[] objArray2 = new object[1];
            bool flag = true;
            string str = string.Empty;
            ArrayList groupById = DataHolders.AttributesHolder.GetGroupByID(attributeType.AttributeID);
            if (groupById != null && groupById.Count > 0)
              str = DataHolders.AttributeGroupsHolder.GetNamebyID((int) groupById[0]);
            IDBAttributable attributable = this.objPDH.GetAttributable(sessionKeeper.Session);
            if (attributable != null)
            {
              if (attributable.GetAttributeByID(attributeType.AttributeID) != null && !this.objPDH.CheckIfDeleted(attributeType.AttributeID))
              {
                int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_969"), (object) attributeType.Name));
                continue;
              }
              if (attributeType.AttributeType != FieldTypes.ftAutoInc)
              {
                IDBAttribute dbAttribute = attributable.Attributes.AddTemporaryAttribute(attributeType.AttributeID, false);
                if (dbAttribute != null)
                {
                  objArray1 = new object[dbAttribute.Values.Length];
                  dbAttribute.Values.CopyTo((Array) objArray1, 0);
                  objArray2 = new object[dbAttribute.Values.Length];
                  flag = dbAttribute.ReadOnly;
                  if (str == string.Empty)
                    str = dbAttribute.GroupName;
                }
              }
            }
            attributeValues.Values = objArray1;
            attributeValues.ReadOnly = flag;
            attributeValues.GroupName = str;
            attributeValues.Descriptions = objArray2;
            arrayList.Add((object) attributeValues);
          }
          else
          {
            IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(Convert.ToInt32(attributesSelectDlg.SelectedAttributesID[index]));
            string str = attributeType != null ? attributeType.Name : Convert.ToString(attributesSelectDlg.SelectedAttributesID[index]);
            string empty = string.Empty;
            string name = this.objPDH.AttributableElement != AttributableElements.Object ? "Warning_CantAddAttribute4Relation" : "Warning_CantAddAttribute";
            int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString(name), (object) str));
          }
        }
      }
      if (arrayList.Count <= 0)
        return;
      bool directWriteOccured1 = false;
      bool flag1 = this.objPDH.AddProperty((AttributeValues[]) arrayList.ToArray(typeof (AttributeValues)), out directWriteOccured1);
      if (flag1)
      {
        this.isChanged = true;
        List<AttributeValues> attributeValuesList = new List<AttributeValues>();
        for (int index = 0; index < arrayList.Count; ++index)
        {
          int attributeValueListIndex = ObjectPropDescriptorHolder.GetAttributeValueListIndex(this.objPDH.AttributeValuesList, ((AttributeValues) arrayList[index]).AttributeID);
          if (attributeValueListIndex != -1)
          {
            AttributeValues attributeValues = (AttributeValues) this.objPDH.AttributeValuesList[attributeValueListIndex];
            if (attributeValues.AttributeType == FieldTypes.ftObjectLink && (attributeValues.MultipleValued == MultiValueModes.SingleValue || attributeValues.MultipleValued == MultiValueModes.SingleValueFromList))
              attributeValuesList.Add(attributeValues);
          }
        }
        if (attributeValuesList.Count > 0)
        {
          bool directWriteOccured2 = false;
          this.objPDH.AddProperty((AttributeValues[]) arrayList.ToArray(typeof (AttributeValues)), out directWriteOccured2, true, false);
        }
        string attributeName = ((AttributeValues) arrayList[0]).AttributeName;
        GridItemType type = GridItemType.Property;
        if (this.SelectedObject != null && this.SelectedGridItem != null && attributeName != string.Empty)
        {
          GridItem gridItem = this.FindGridItem(attributeName, type, this.SelectedGridItem);
          if (gridItem != null)
            this.SelectedGridItem = gridItem;
        }
      }
      if (!flag1 && !(!flag1 & directWriteOccured1) || this.GridChanged == null)
        return;
      this.GridChanged((object) this, new GridChangedEventArgs(this.isChanged, directWriteOccured1));
    }
  }

  private void OnDeleteMenuItem(object sender, EventArgs args)
  {
    if (this.SelectedGridItem == null)
      return;
    bool directWriteOccured = false;
    bool flag = this.objPDH.DeleteProperty((PropDescriptor) this.SelectedGridItem.PropertyDescriptor, out directWriteOccured);
    if (flag)
      this.isChanged = true;
    if (!flag && !(!flag & directWriteOccured) || this.GridChanged == null)
      return;
    this.GridChanged((object) this, new GridChangedEventArgs(this.isChanged, directWriteOccured));
  }

  private void OnShowObjMenuItem(object sender, EventArgs args)
  {
    if (this.SelectedGridItem == null || !(this.SelectedGridItem.PropertyDescriptor.GetValue((object) this) is ObjectPropertyClass objectPropertyClass))
      return;
    long objectId = objectPropertyClass.ObjectID;
    if (!objectPropertyClass.ObjectVersionProcessed)
    {
      QuickObjectInfo objectInfoById = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfoByID(objectId);
      if (!objectInfoById.Empty)
      {
        objectId = objectInfoById.ObjectID;
      }
      else
      {
        int num = (int) IMMessageBox.Show("Внимание", "Не найдена базовая версия объекта ID=" + (object) objectId, MessageBoxButtons.OK, IMMessageBoxImage.Error);
      }
    }
    Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objectId), (System.IServiceProvider) new AdvancedServiceContainer());
  }

  public static int GetAttributeIDbyGridItem(GridItem gridItem)
  {
    int attributeIdbyGridItem = 0;
    if (gridItem != null)
    {
      if (gridItem.PropertyDescriptor is ListPropDescriptor || gridItem.PropertyDescriptor is SimplePropDescriptor && ((SimplePropDescriptor) gridItem.PropertyDescriptor).ParentListPropDescriptor == null)
        attributeIdbyGridItem = ((PropDescriptor) gridItem.PropertyDescriptor).PropID;
      else if (gridItem.PropertyDescriptor is SimplePropDescriptor && ((SimplePropDescriptor) gridItem.PropertyDescriptor).ParentListPropDescriptor != null)
        attributeIdbyGridItem = ((SimplePropDescriptor) gridItem.PropertyDescriptor).ParentListPropDescriptor.PropID;
    }
    return attributeIdbyGridItem;
  }

  private void contextMenu_Popup(object sender, EventArgs e)
  {
    bool flag1 = true;
    bool flag2 = true;
    bool flag3 = true;
    bool flag4 = false;
    if (this.SelectedGridItem != null)
    {
      int attributeIdbyGridItem = ObjectPropertyGrid.GetAttributeIDbyGridItem(this.SelectedGridItem);
      bool flag5 = this.objPDH.LockedAttributes.IndexOf(attributeIdbyGridItem) != -1;
      if (this.SelectedGridItem.PropertyDescriptor is SimplePropDescriptor && ((SimplePropDescriptor) this.SelectedGridItem.PropertyDescriptor).ParentListPropDescriptor != null && ((SimplePropDescriptor) this.SelectedGridItem.PropertyDescriptor).ParentListPropDescriptor.IsReadOnly | flag5)
      {
        flag2 = false;
        flag3 = false;
      }
      if (this.SelectedGridItem.PropertyDescriptor is SimplePropDescriptor && ((SimplePropDescriptor) this.SelectedGridItem.PropertyDescriptor).ParentListPropDescriptor == null && flag5)
        flag3 = false;
      if (this.SelectedGridItem.PropertyDescriptor is SimplePropDescriptor)
      {
        System.Type propertyType = this.SelectedGridItem.PropertyDescriptor.PropertyType;
        if ((propertyType == typeof (ObjectPropertyClass) || propertyType != (System.Type) null && propertyType.IsSubclassOf(typeof (ObjectPropertyClass))) && this.SelectedGridItem.PropertyDescriptor.GetValue((object) this) is ObjectPropertyClass)
          flag4 = true;
      }
      int attributeValueListIndex = this.objPDH.GetAttributeValueListIndex(attributeIdbyGridItem);
      if (attributeValueListIndex != -1)
        flag1 = AttributeValuesEditor.IsSystemAttributeValue((AttributeValues) this.objPDH.AttributeValuesList[attributeValueListIndex]);
      if (flag3 & flag5)
        flag3 = false;
    }
    this.deleteMenuItem.Visible = this.SelectedGridItem != null && this.SelectedGridItem.PropertyDescriptor is PropDescriptor && !flag1;
    this.showObjMenuItem.Visible = this.SelectedGridItem != null && this.SelectedGridItem.PropertyDescriptor is PropDescriptor;
    this.addMenuItem.Enabled = flag2;
    this.deleteMenuItem.Enabled = flag3;
    this.showObjMenuItem.Enabled = flag4;
  }

  protected override void OnContextMenuChanged(EventArgs e)
  {
    if (!this.internalMenuEnabled)
    {
      base.OnContextMenuChanged(e);
    }
    else
    {
      this.UnplugContextMenuItems(this.contextMenuSafe);
      this.contextMenuSafe = base.ContextMenu;
      this.PlugContextMenuItems(this.contextMenuSafe);
      if (base.ContextMenu == null)
      {
        this.needBaseCallback = true;
        this.ContextMenu = this.contextMenu;
      }
      else
      {
        if (!this.needBaseCallback && base.ContextMenu == this.contextMenu)
          return;
        this.needBaseCallback = false;
        base.OnContextMenuChanged(e);
      }
    }
  }

  public ObjectPropertyGrid()
  {
    this.DrawFlatToolbar = true;
    this.objVerIdString = LocalizationHolder.rm.GetString("Client.Core_272");
    this.objIdString = LocalizationHolder.rm.GetString("Client.Core_272id");
    this.CreateContextMenu();
  }

  private void InitializeComponent()
  {
  }

  /// <summary>Загрузить информацию в компонент</summary>
  /// <param name="aId">Идентификатор элемента (объекта, связи)</param>
  /// <param name="aAttributableElement">Вид элемента (объект, связь)</param>
  /// <param name="aAttributeValuesModes">Флаги отображения информации</param>
  /// <param name="aIsChanged">После загрузки установить состояние изменения</param>
  /// <param name="tabTypes">Type[] в виде списка TabType, производных от ObjectPropertyGridTab</param>
  /// <returns></returns>
  public bool Load(
    long aId,
    AttributableElements aAttributableElement,
    GetAttributeValuesModes aAttributeValuesModes,
    bool aIsChanged,
    params System.Type[] tabTypes)
  {
    string safeGridItemLabel = this.safeGridItemLabel;
    GridItemType safeGridItemType = this.safeGridItemType;
    this.tabTypes = tabTypes;
    if (!this.objPDH.AssignData(aId, aAttributableElement, aAttributeValuesModes, this, this.lockTypeChange, this.tabTypes))
      return false;
    if (this.SelectedObject != null && this.SelectedGridItem != null)
    {
      if (safeGridItemLabel != string.Empty)
      {
        GridItem gridItem = this.FindGridItem(safeGridItemLabel, safeGridItemType, this.SelectedGridItem);
        if (gridItem != null)
          this.SelectedGridItem = gridItem;
      }
      else
      {
        GridItem rootGridItem = this.FindRootGridItem(this.SelectedGridItem);
        if (rootGridItem != null && rootGridItem.GridItems.Count > 0)
          this.SelectedGridItem = rootGridItem.GridItems[0];
      }
    }
    this.isChanged = aIsChanged;
    return true;
  }

  public bool Save() => this.Save(false);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="blankMode">Признак того, что работаем с заготовкой объекта</param>
  /// <returns></returns>
  public bool Save(bool blankMode)
  {
    if (!this.isChanged)
      return true;
    ArrayList origList = (ArrayList) null;
    ArrayList fireList = (ArrayList) null;
    if (this.objPDH.SaveData(out origList, out fireList))
    {
      this.isChanged = false;
      if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
      {
        switch (this.objPDH.AttributableElement)
        {
          case AttributableElements.Object:
            DBObjectsExtendedEventArgs e = new DBObjectsExtendedEventArgs("ObjectsChanged", this.objPDH.Id, this.objPDH.ElementType, (AttributeValues[]) origList.ToArray(typeof (AttributeValues)), (AttributeValues[]) fireList.ToArray(typeof (AttributeValues)));
            if (blankMode)
              e.VerType = ObjectRecordKind.Blank;
            service.FireEvent((object) this, (NotificationEventArgs) e);
            break;
          case AttributableElements.Relation:
            service.FireEvent((object) this, (NotificationEventArgs) new DBRelationsExtendedEventArgs("RelationsChanged", this.objPDH.Id, this.objPDH.ElementType, (AttributeValues[]) origList.ToArray(typeof (AttributeValues)), (AttributeValues[]) fireList.ToArray(typeof (AttributeValues))));
            break;
        }
      }
    }
    return true;
  }

  protected override void OnPropertyValueChanged(PropertyValueChangedEventArgs e)
  {
    if (this.blockOnValueChange)
      return;
    bool flag1 = false;
    PropDescriptor propertyDescriptor1 = (PropDescriptor) e.ChangedItem.PropertyDescriptor;
    this.UpdatePropDescriptorDescription((PropertyDescriptor) propertyDescriptor1);
    if (this.objPDH.AttributableElement == AttributableElements.Object && propertyDescriptor1.PropID == Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_TYPE) || this.objPDH.AttributableElement == AttributableElements.Relation && propertyDescriptor1.PropID == Convert.ToInt32((object) ObligatoryObjectAttributes.F_RELATION_TYPE))
    {
      flag1 = true;
      if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_970"), LocalizationHolder.rm.GetString("Client.Core_971"), MessageBoxButtons.YesNo) != DialogResult.Yes)
      {
        try
        {
          this.blockOnValueChange = true;
          e.ChangedItem.PropertyDescriptor.SetValue((object) this, e.OldValue);
          return;
        }
        finally
        {
          this.blockOnValueChange = false;
        }
      }
    }
    if (flag1)
    {
      int num1 = -1;
      if (this.objPDH.AttributableElement == AttributableElements.Object)
        num1 = ((ObjectTypePropertyClass) propertyDescriptor1.GetValue((object) this)).ObjectType;
      if (this.objPDH.AttributableElement == AttributableElements.Relation)
        num1 = ((RelationTypePropertyClass) propertyDescriptor1.GetValue((object) this)).RelationType;
      if (!this.Save())
      {
        try
        {
          this.blockOnValueChange = true;
          e.ChangedItem.PropertyDescriptor.SetValue((object) this, e.OldValue);
          int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_972"), LocalizationHolder.rm.GetString("Client.Core_82"), MessageBoxButtons.OK);
          return;
        }
        finally
        {
          this.blockOnValueChange = false;
        }
      }
      else
      {
        try
        {
          try
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              if (this.objPDH.AttributableElement == AttributableElements.Object)
              {
                IDBObject dbObject = sessionKeeper.Session.GetObject(this.objPDH.Id);
                if (dbObject != null)
                  dbObject.ObjectType = num1;
              }
              if (this.objPDH.AttributableElement == AttributableElements.Relation)
              {
                IDBRelation relation = sessionKeeper.Session.GetRelation(this.objPDH.Id);
                if (relation != null)
                  relation.RelationType = num1;
              }
            }
          }
          catch (Exception ex)
          {
            this.blockOnValueChange = true;
            e.ChangedItem.PropertyDescriptor.SetValue((object) this, e.OldValue);
            this.blockOnValueChange = false;
            ExceptionHelper.ExceptionService.ShowException(ex);
            return;
          }
          this.blockOnValueChange = true;
          if (!this.Load(this.objPDH.Id, this.objPDH.AttributableElement, this.objPDH.AttributeValuesModes, false, this.tabTypes))
          {
            int num3 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_973"), LocalizationHolder.rm.GetString("Client.Core_82"), MessageBoxButtons.OK);
          }
          else
          {
            DBObjectsEventArgs e1 = new DBObjectsEventArgs("ObjectsChanged", this.objPDH.Id);
            if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
              service.FireEvent((object) this, (NotificationEventArgs) e1);
          }
        }
        finally
        {
          this.blockOnValueChange = false;
        }
      }
    }
    else
    {
      this.isChanged = true;
      ((PropDescriptor) e.ChangedItem.PropertyDescriptor).ValueChanged = true;
    }
    base.OnPropertyValueChanged(e);
    if (flag1)
      return;
    if (this.blockOnMasterAssign)
      return;
    try
    {
      this.blockOnMasterAssign = true;
      PropDescriptor propertyDescriptor2 = (PropDescriptor) e.ChangedItem.PropertyDescriptor;
      if (!(propertyDescriptor2 is SimplePropDescriptor) || ((SimplePropDescriptor) propertyDescriptor2).ParentListPropDescriptor != null)
        return;
      int attributeValueListIndex = ObjectPropDescriptorHolder.GetAttributeValueListIndex(this.objPDH.AttributeValuesList, propertyDescriptor2.PropID);
      if (attributeValueListIndex == -1)
        return;
      AttributeValues attributeValues = (AttributeValues) ((AttributeValues) this.objPDH.AttributeValuesList[attributeValueListIndex]).Clone();
      if (attributeValues.AttributeType != FieldTypes.ftObjectLink || attributeValues.MultipleValued != MultiValueModes.SingleValue && attributeValues.MultipleValued != MultiValueModes.SingleValueFromList)
        return;
      bool directWriteOccured = false;
      bool flag2 = this.objPDH.AddProperty(new AttributeValues[1]
      {
        attributeValues
      }, out directWriteOccured, true, true);
      if (!flag2 && !(!flag2 & directWriteOccured) || this.GridChanged == null)
        return;
      this.GridChanged((object) this, new GridChangedEventArgs(this.isChanged, directWriteOccured));
    }
    finally
    {
      this.blockOnMasterAssign = false;
    }
  }

  protected override void OnSelectedObjectsChanged(EventArgs e) => base.OnSelectedObjectsChanged(e);

  protected override void OnPropertyTabChanged(PropertyTabChangedEventArgs e)
  {
    if (this.blockOnPropertyTabChange)
      return;
    base.OnPropertyTabChanged(e);
  }

  protected override void OnSelectedGridItemChanged(SelectedGridItemChangedEventArgs e)
  {
    base.OnSelectedGridItemChanged(e);
    GridItem gridItem = e.NewSelection;
    if (gridItem == null)
    {
      this.safeGridItemLabel = string.Empty;
    }
    else
    {
      if (gridItem.GridItemType == GridItemType.Property)
      {
        while (gridItem.Parent != null && gridItem.Parent.GridItemType == GridItemType.Property)
          gridItem = gridItem.Parent;
      }
      this.safeGridItemLabel = gridItem.Label;
      this.safeGridItemType = gridItem.GridItemType;
      this.UpdatePropDescriptorDescription(gridItem.PropertyDescriptor);
    }
  }

  private void UpdatePropDescriptorDescription(PropertyDescriptor propertyDescriptor)
  {
    if (propertyDescriptor == null || !(propertyDescriptor is PropDescriptor) || !(propertyDescriptor.PropertyType == typeof (ObjectPropertyClass)))
      return;
    string aDescription = propertyDescriptor.Description;
    int startIndex = aDescription.IndexOf("\n");
    if (startIndex >= 0)
      aDescription = aDescription.Remove(startIndex);
    if (propertyDescriptor.GetValue((object) this) is ObjectPropertyClass objectPropertyClass && objectPropertyClass.ObjectID != 0L)
      aDescription = $"{aDescription}\n{(objectPropertyClass.ObjectVersionProcessed ? this.objVerIdString : this.objIdString)}={objectPropertyClass.ObjectID.ToString()}";
    ((PropDescriptor) propertyDescriptor).SetDescription(aDescription);
    this.Refresh();
  }

  private GridItem FindRootGridItem(GridItem gi)
  {
    while (gi.Parent != null && gi.GridItemType != GridItemType.Root)
      gi = gi.Parent;
    return gi;
  }

  private GridItem FindGridItem(string label, GridItemType type, GridItem gi)
  {
    gi = this.FindRootGridItem(gi);
    return this.FindGridItemCustom(label, type, gi);
  }

  private GridItem FindGridItemCustom(string label, GridItemType type, GridItem gi)
  {
    if (this.GridItemEqual(label, type, gi))
      return gi;
    GridItem gridItemCustom = (GridItem) null;
    foreach (GridItem gridItem in gi.GridItems)
    {
      if (this.GridItemEqual(label, type, gridItem))
      {
        gridItemCustom = gridItem;
        break;
      }
      gridItemCustom = this.FindGridItemCustom(label, type, gridItem);
      if (gridItemCustom != null)
        break;
    }
    return gridItemCustom;
  }

  private bool GridItemEqual(string label, GridItemType type, GridItem gi)
  {
    return gi.GridItemType == type && gi.Label == label;
  }

  protected override System.Type DefaultTabType => typeof (PropertiesTabCustom);

  public delegate void GridChangedDelegate(object sender, GridChangedEventArgs e);
}
