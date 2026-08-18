
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrListBoxBtn
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Imbase;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Редактор списка ссылок на объекты.</summary>
public class AttrListBoxBtn : AttrListBoxBase
{
  private ControlButton _btnForm;
  private MenuBar _bar;
  private MenuBarItem _controlMenu;
  private MenuButtonItem _navigatorItems;
  private MenuButtonItem _mbiAdd;
  private MenuButtonItem _mbiDel;
  private MenuButtonItem _mbiEdit;
  private MenuButtonItem _mbiClear;
  private MenuButtonItem _mbiForm;
  private MenuButtonItem _mbiPaste;
  private bool skip;
  /// <summary>Контейнер сервисов</summary>
  private ServiceContainer _services;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Отступы от краев в элементе управления.</summary>
  /// <remarks>Здесь нужно только для того, чтобы запреить сериализацию</remarks>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new Padding Padding
  {
    get => base.Padding;
    private set => base.Padding = value;
  }

  /// <summary>Доступость пункта меню "Вставить".</summary>
  private bool IsPasteEnabled
  {
    get
    {
      IDBObjectTypedIDCollection typedIdCollection = (IDBObjectTypedIDCollection) null;
      if (this.EnabledCtrl && this.AttributeInfo != null && this._attrValues != null)
        typedIdCollection = (ApplicationServices.Container.GetService(typeof (IClipboard)) as IClipboard).GetDataObject() as IDBObjectTypedIDCollection;
      return typedIdCollection != null;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DefaultValue(false)]
  public bool SelectFromImbase { get; set; }

  /// <summary>Показ контекстного меню</summary>
  [DefaultValue(true)]
  public bool ShowContextMenu { get; set; }

  /// <summary>Конструктор.</summary>
  public AttrListBoxBtn()
  {
    MenuButtonItem menuButtonItem1 = new MenuButtonItem();
    menuButtonItem1.Enabled = true;
    menuButtonItem1.CommandName = "miAdd";
    menuButtonItem1.Tag = (object) 0;
    this._mbiAdd = menuButtonItem1;
    MenuButtonItem menuButtonItem2 = new MenuButtonItem();
    menuButtonItem2.Enabled = false;
    menuButtonItem2.CommandName = "miDel";
    this._mbiDel = menuButtonItem2;
    MenuButtonItem menuButtonItem3 = new MenuButtonItem();
    menuButtonItem3.Enabled = false;
    menuButtonItem3.CommandName = "miEdit";
    menuButtonItem3.Tag = (object) 1;
    this._mbiEdit = menuButtonItem3;
    MenuButtonItem menuButtonItem4 = new MenuButtonItem();
    menuButtonItem4.Enabled = false;
    menuButtonItem4.CommandName = "miClear";
    this._mbiClear = menuButtonItem4;
    MenuButtonItem menuButtonItem5 = new MenuButtonItem();
    menuButtonItem5.Enabled = false;
    menuButtonItem5.CommandName = "miForm";
    this._mbiForm = menuButtonItem5;
    MenuButtonItem menuButtonItem6 = new MenuButtonItem();
    menuButtonItem6.Enabled = false;
    menuButtonItem6.CommandName = "miPaste";
    menuButtonItem6.BeginGroup = true;
    this._mbiPaste = menuButtonItem6;
    this._services = new ServiceContainer();
    // ISSUE: explicit constructor call
    base.\u002Ector();
    this.InitializeComponent();
    this.Name = string.Empty;
    this.ShowContextMenu = true;
    this._btnForm = new ControlButton("Form", 7)
    {
      Enabled = false
    };
    this._btnForm.Click += new EventHandler(this.On_btnForm_Click);
    this.AddTopButton(this._btnForm);
    this.MenuItemClick += new EventHandler(this.On_btnAddEdit_Click);
    this._lst.ContextMenuStrip = (ContextMenuStrip) null;
    this.CreateContextMenu();
    this.InitServices();
  }

  /// <summary>Добавить элемент.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnAddEdit_Click(object sender, EventArgs e)
  {
    if (this.AttributeInfo == null || this._attrValues == null)
      return;
    bool flag = this._attrValues.AttributeType != FieldTypes.ftObjectLinkByID;
    int int32 = Convert.ToInt32(sender is MenuButtonItem menuButtonItem ? menuButtonItem.Tag : (sender as ControlButton).Tag);
    List<long> existItems = this.FillIDsList((IList) this._lst.Items);
    if (this._describer != null)
    {
      Dictionary<long, object> valueFromDescriber = this.GetValueFromDescriber(int32 == 0 ? (object) null : this._lst.SelectedItem);
      if (valueFromDescriber == null)
        return;
      foreach (KeyValuePair<long, object> keyValuePair in valueFromDescriber)
      {
        if (existItems.Contains(keyValuePair.Key) && (int32 == 0 || existItems.IndexOf(keyValuePair.Key) != this._lst.SelectedIndex))
        {
          int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("FormDesigner_ListControls_ValueExist"), (object) Convert.ToString(keyValuePair.Value)), LocalizationHolder.rm.GetString("FormDesigner_ListControls_DublicationValue"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else
        {
          this._lst.BeginUpdate();
          try
          {
            if (int32 == 0)
              this._lst.SelectedIndex = this._lst.Items.Add(keyValuePair.Value);
            else
              this._lst.Items[this._lst.SelectedIndex] = keyValuePair.Value;
          }
          finally
          {
            this._lst.EndUpdate();
          }
          this.Modified = true;
        }
      }
    }
    else if (this.SelectFromImbase)
    {
      if (!(ApplicationServices.Container.GetService(typeof (IImbaseFilterSelector)) is IImbaseFilterSelector service))
      {
        this.Error = "Не удалось получить сервис выбора объектов из IMBASE.";
      }
      else
      {
        List<long> newItems = new List<long>(0);
        List<long> catalogIDs = (List<long>) null;
        ImbaseCatalogSelectMode mode = ImbaseCatalogSelectMode.imcmNone;
        try
        {
          int typeID = -1;
          if (this.ParentInfo.ElementKind == AttributableElements.Object)
          {
            if (this.ParentTypeID == -1)
            {
              QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(this.ParentInfo.ElementIdentifier);
              if (!objectInfo.Empty)
                this.ParentTypeID = objectInfo.ObjectTypeID;
            }
            typeID = this.ParentTypeID;
          }
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            ImbaseExtendedItem imbaseExtendedItem = ExtendedServiceHelper.GetImbaseExtendedItem(sessionKeeper.Session, typeID, this._attrValues.AttributeID);
            if (imbaseExtendedItem != null)
            {
              catalogIDs = imbaseExtendedItem.CatalogIDs;
              mode = imbaseExtendedItem.SelectMode;
            }
          }
        }
        catch
        {
        }
        finally
        {
          if (catalogIDs == null || catalogIDs.Count == 0)
          {
            catalogIDs = (List<long>) null;
            this.Error = LocalizationHolder.rm.GetString("AttrTextBtnComp.ImbaseCatalog.NotRef");
          }
        }
        if (catalogIDs == null)
          return;
        Dictionary<TypedInfoItem, IEnumerable<AttributeValues>> dict = new Dictionary<TypedInfoItem, IEnumerable<AttributeValues>>(2);
        IElementInfo elementInfo = (IElementInfo) null;
        if (this.DesForm != null)
        {
          elementInfo = this.DesForm.Info;
          List<AttributeValues> changedAttributes1 = this.DesForm.GetBaseElementChangedAttributes;
          if (changedAttributes1.Count > 0)
          {
            if (elementInfo.ElementKind == AttributableElements.Object)
              dict.Add((TypedInfoItem) new ObjInfoItem(elementInfo.ElementIdentifier, this.DesForm.ElementTypeID), (IEnumerable<AttributeValues>) changedAttributes1);
            else
              dict.Add((TypedInfoItem) new RelInfoItem(elementInfo.ElementIdentifier, this.DesForm.ElementTypeID), (IEnumerable<AttributeValues>) changedAttributes1);
          }
          List<AttributeValues> changedAttributes2 = this.DesForm.GetAdditionalElementChangedAttributes;
          if (changedAttributes2.Count > 0)
            dict.Add((TypedInfoItem) new RelInfoItem(this.DesForm.RelationInfo.ElementIdentifier), (IEnumerable<AttributeValues>) changedAttributes2);
        }
        long objID = this.ParentInfo.ElementKind == AttributableElements.Object ? this.ParentInfo.ElementIdentifier : (elementInfo != null ? elementInfo.ElementIdentifier : 0L);
        int[] needObjTypes = (int[]) null;
        if (mode == ImbaseCatalogSelectMode.imcmCreateObject)
          needObjTypes = MetaDataHelper.GetLinkedObjectTypes(this._attrValues.AttributeID)?.ToArray();
        long objectID = service.SelectImbaseObject(catalogIDs, needObjTypes, objID, 0L, mode, dict, this._attrValues.AttributeID);
        if (objectID == 0L)
          return;
        long num = objectID;
        if (!flag)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
            if (dbObject != null)
              num = dbObject.ID;
          }
        }
        newItems.Add(num);
        this.AddItems(existItems, newItems, int32 == 0);
        this.Modified = true;
      }
    }
    else
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.AttributeInfo.AttributeGuid);
      if (attributeType == null)
        return;
      int usersTypeId = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).UsersTypeID;
      List<long> newItems = new List<long>(0);
      if (Convert.ToInt32(attributeType.SizeType) == usersTypeId)
      {
        IDescriptor rootDescriptor = (IDescriptor) new UsersGroupsDescriptor();
        if (!(Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_1129"), rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.SelectObjects) is IDBTypedObjectID[] dbTypedObjectIdArray) || dbTypedObjectIdArray.Length == 0 || dbTypedObjectIdArray[0].ObjectType != usersTypeId)
          return;
        foreach (IDBTypedObjectID dbTypedObjectId in dbTypedObjectIdArray)
          newItems.Add(flag ? dbTypedObjectId.ObjectID : dbTypedObjectId.ID);
      }
      else
      {
        IDescriptor rootDescriptor;
        if (attributeType.SizeType == -1L)
          rootDescriptor = (IDescriptor) new ObjectTypesNodeDescriptor();
        else if (attributeType.SizeType == 0L)
        {
          ArrayList typeListByAttrId = ObjectEditor.GetObjTypeListByAttrId(attributeType.AttributeID);
          DescriptorCollection descriptors = new DescriptorCollection();
          if (typeListByAttrId != null)
          {
            int result = 0;
            foreach (object obj in typeListByAttrId)
            {
              if (int.TryParse(Convert.ToString(obj), out result))
              {
                if (result == usersTypeId)
                  descriptors.Add((IDescriptor) new UsersGroupsDescriptor());
                else
                  descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(result));
              }
            }
          }
          rootDescriptor = (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Client.Core_1266"), descriptors);
        }
        else
        {
          rootDescriptor = (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(Convert.ToInt32(attributeType.SizeType));
          Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new TypedObjectsSelectedItemsAnalyzer(Convert.ToInt32(attributeType.SizeType), true), true);
        }
        if (!(Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_1130"), rootDescriptor, typeof (IDBObjectID), SelectionOptions.Default) is IDBObjectID[] dbObjectIdArray) || dbObjectIdArray.Length == 0)
          return;
        foreach (IDBObjectID dbObjectId in dbObjectIdArray)
          newItems.Add(flag ? dbObjectId.Value : dbObjectId.ID);
      }
      this.AddItems(existItems, newItems, int32 == 0);
      this.Modified = true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_miDel_Click(object sender, EventArgs e) => this.DeleteItem();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_miClear_Click(object sender, EventArgs e) => this.ClearItems();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnForm_Click(object sender, EventArgs e)
  {
    if (this._lst.SelectedIndex <= -1)
      return;
    long result = 0;
    if (this._describer != null)
    {
      if (!long.TryParse(Convert.ToString(this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, this._lst.SelectedItem)), out result))
        result = 0L;
    }
    else
      result = this._lst.SelectedItem is ObjectIDToCaption selectedItem ? selectedItem.ObjectID : 0L;
    if (result == 0L)
      return;
    long ObjectID = result;
    if (this.AttributeInfo != null && this._attrValues != null && this._attrValues.AttributeType == FieldTypes.ftObjectLinkByID)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectBaseVersionById = sessionKeeper.Session.GetObjectBaseVersionByID(result, false);
        if (objectBaseVersionById != null)
          ObjectID = objectBaseVersionById.ObjectID;
      }
    }
    int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, ObjectID, false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_miPaste_Click(object sender, EventArgs e)
  {
    if (this._describer != null)
      throw new ApplicationException(LocalizationHolder.rm.GetString("FormDesigner_AttrsWithDescriber_Paste_Error"));
    bool flag1 = true;
    if (this.AttributeInfo != null && this._attrValues != null)
      flag1 = this._attrValues.AttributeType != FieldTypes.ftObjectLinkByID;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.AttributeInfo.AttributeGuid);
    if (attributeType == null)
      return;
    int attributeId = attributeType.AttributeID;
    IDBObjectTypedIDCollection dataObject = (ApplicationServices.Container.GetService(typeof (IClipboard)) as IClipboard).GetDataObject() as IDBObjectTypedIDCollection;
    List<long> newItems = new List<long>(0);
    if (attributeType.SizeType == -1L)
    {
      for (int index = 0; index < dataObject.Count; ++index)
        newItems.Add(flag1 ? dataObject.GetTypedObjectID(index).ObjectID : dataObject.GetTypedObjectID(index).ID);
    }
    else if (attributeType.SizeType == 0L)
    {
      List<int> possibleTypes = this.GetPossibleTypes(attributeId);
      if (possibleTypes != null)
      {
        for (int index = 0; index < dataObject.Count; ++index)
        {
          IDBTypedObjectID typedObjectId = dataObject.GetTypedObjectID(index);
          bool flag2 = true;
          foreach (int parentType in possibleTypes)
          {
            if (typedObjectId.ObjectType == parentType || MetaDataHelper.IsObjectTypeChildOf(typedObjectId.ObjectType, parentType))
            {
              flag2 = false;
              break;
            }
          }
          if (!flag2)
            newItems.Add(flag1 ? typedObjectId.ObjectID : typedObjectId.ID);
        }
      }
    }
    else
    {
      int int32 = Convert.ToInt32(attributeType.SizeType);
      for (int index = 0; index < dataObject.Count; ++index)
      {
        IDBTypedObjectID typedObjectId = dataObject.GetTypedObjectID(index);
        if (typedObjectId.ObjectType == int32)
          newItems.Add(flag1 ? typedObjectId.ObjectID : typedObjectId.ID);
      }
    }
    this.AddItems(this.FillIDsList((IList) this._lst.Items), newItems);
    this.Modified = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lst_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._btnForm.Enabled = this._mbiForm.Enabled = this._lst.Items.Count > 0 && this._lst.SelectedIndex > -1 && this._lst.SelectedIndices.Count == 1;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lst_MouseUp(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    this.AddNavigatorContextMenu();
  }

  /// <summary>Получение элемент.</summary>
  /// <param name="item">Элемент в списке элементов</param>
  /// <returns>Значение </returns>
  private object OnGetItemForAttributeValuesFromDescriber(object item)
  {
    object attributeValue = this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, item);
    return attributeValue == null || attributeValue == DBNull.Value || !(attributeValue is long) ? (object) DBNull.Value : attributeValue;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="existItems"></param>
  /// <param name="newItems"></param>
  /// <param name="isAdd"></param>
  private void AddItems(List<long> existItems, List<long> newItems, bool isAdd = true)
  {
    bool _objectVersionProcessed = true;
    if (this.AttributeInfo != null && this._attrValues != null)
      _objectVersionProcessed = this._attrValues.AttributeType != FieldTypes.ftObjectLinkByID;
    int num = 0;
    string format = LocalizationHolder.rm.GetString("FormDesigner_ListControls_ValueExist");
    string str = LocalizationHolder.rm.GetString("FormDesigner_IdenticalMessage_Skip");
    string caption = LocalizationHolder.rm.GetString("FormDesigner_ListControls_DublicationValue");
    this._lst.BeginUpdate();
    try
    {
      string empty = string.Empty;
      if (!isAdd)
      {
        ObjectIDToCaption objectIdToCaption = new ObjectIDToCaption(newItems[0], _objectVersionProcessed);
        if (!existItems.Contains(objectIdToCaption.ObjectID))
        {
          existItems.Remove((this._lst.Items[this._lst.SelectedIndex] as ObjectIDToCaption).ObjectID);
          existItems.Add(objectIdToCaption.ObjectID);
          this._lst.Items[this._lst.SelectedIndex] = (object) objectIdToCaption;
          ++num;
        }
        else if (existItems.IndexOf(objectIdToCaption.ObjectID) != this._lst.SelectedIndex)
          this.skip = MessageBox.Show($"{string.Format(format, (object) objectIdToCaption.ToString())}\n{str}", caption, MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes;
      }
      for (int index = num; index < newItems.Count; ++index)
      {
        ObjectIDToCaption objectIdToCaption = new ObjectIDToCaption(newItems[index], _objectVersionProcessed);
        if (existItems.Contains(objectIdToCaption.ObjectID))
        {
          if (!this.skip)
            this.skip = MessageBox.Show($"{string.Format(format, (object) objectIdToCaption.ToString())}\n{str}", caption, MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes;
        }
        else
          this._lst.SelectedIndex = this._lst.Items.Add((object) objectIdToCaption);
      }
    }
    finally
    {
      this._lst.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void AddNavigatorContextMenu()
  {
    if (this._navigatorItems != null)
    {
      if (this._controlMenu.Items.Contains((ToolbarItemBase) this._navigatorItems))
        this._controlMenu.Items.Remove((ToolbarItemBase) this._navigatorItems);
      this._navigatorItems.Dispose();
    }
    if (!this.ShowContextMenu)
      return;
    MenuButtonItem menuButtonItem = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_NavigatorCommands"));
    menuButtonItem.BeginGroup = true;
    this._navigatorItems = menuButtonItem;
    bool flag = false;
    if (this._lst.SelectedItems.Count > 0)
    {
      MenuBarItem menu = Intermech.Navigator.ContextMenu.Services.GetMenu(Intermech.Navigator.ContextMenu.Services.GetItems(this.FillIDsList((IList) this._lst.SelectedItems).ToArray()), (System.IServiceProvider) this._services);
      if (menu != null && menu.Items.Count > 0)
      {
        foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) menu.Items)
          this._navigatorItems.Items.Add(toolbarItemBase.CloneItem());
        flag = true;
      }
    }
    this._navigatorItems.Enabled = flag;
    this._controlMenu.Items.Add((ToolbarItemBase) this._navigatorItems);
  }

  /// <summary>
  /// 
  /// </summary>
  private void CreateContextMenu()
  {
    if (Holder.BarManager != null && Holder.BarManager.MenuBar != null)
      this._bar.ImageList = Holder.BarManager.MenuBar.ImageList;
    this._mbiAdd.Text = LocalizationHolder.rm.GetString("Client.Core_94a");
    this._mbiAdd.Image = FormDesignerUtils.ButtonImages.ContainsKey("Add") ? FormDesignerUtils.ButtonImages["Add"] : (Image) null;
    this._mbiAdd.Click += new EventHandler(this.On_btnAddEdit_Click);
    this._mbiDel.Text = LocalizationHolder.rm.GetString("Client.Core_96");
    this._mbiDel.Image = FormDesignerUtils.ButtonImages.ContainsKey("Del") ? FormDesignerUtils.ButtonImages["Del"] : (Image) null;
    this._mbiDel.Click += new EventHandler(this.On_miDel_Click);
    this._mbiEdit.Text = LocalizationHolder.rm.GetString("Client.Core_470");
    this._mbiEdit.Image = FormDesignerUtils.ButtonImages.ContainsKey("Edit") ? FormDesignerUtils.ButtonImages["Edit"] : (Image) null;
    this._mbiEdit.Click += new EventHandler(this.On_btnAddEdit_Click);
    this._mbiClear.Text = LocalizationHolder.rm.GetString("Client.Core_1128");
    this._mbiClear.Image = FormDesignerUtils.ButtonImages.ContainsKey("Clean") ? FormDesignerUtils.ButtonImages["Clean"] : (Image) null;
    this._mbiClear.Click += new EventHandler(this.On_miClear_Click);
    this._mbiForm.Text = LocalizationHolder.rm.GetString("AttrTextBtn.Button.ObjectCard.ToolTip");
    this._mbiForm.Image = FormDesignerUtils.ButtonImages.ContainsKey("Form") ? FormDesignerUtils.ButtonImages["Form"] : (Image) null;
    this._mbiForm.Click += new EventHandler(this.On_btnForm_Click);
    INamedImageList service = ApplicationServices.Container.GetService(typeof (INamedImageList)) as INamedImageList;
    this._mbiPaste.Text = LocalizationHolder.rm.GetString("Client.Core_99");
    this._mbiPaste.Click += new EventHandler(this.On_miPaste_Click);
    this._mbiPaste.ImageIndex = service != null ? service.ImageIndex("imgPaste") : -1;
    this._controlMenu.Items.AddRange((ToolbarItemBase[]) new MenuButtonItem[6]
    {
      this._mbiAdd,
      this._mbiDel,
      this._mbiEdit,
      this._mbiClear,
      this._mbiForm,
      this._mbiPaste
    });
    this._bar.SetPopupMenu((Control) this._lst, this._controlMenu);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attrID"></param>
  /// <returns></returns>
  private List<int> GetPossibleTypes(int attrID)
  {
    List<int> intList = (List<int>) null;
    ArrayList typeListByAttrId = ObjectEditor.GetObjTypeListByAttrId(attrID);
    if (typeListByAttrId != null)
    {
      intList = new List<int>(typeListByAttrId.Count);
      int result = 0;
      foreach (object obj in typeListByAttrId)
      {
        if (int.TryParse(Convert.ToString(obj), out result))
          intList.Add(result);
      }
    }
    return intList == null || intList.Count <= 0 ? (List<int>) null : intList;
  }

  /// <summary>
  /// Заполнить список идентификаторов добавленных элементов.
  /// Список необходим для того, чтобы повторно не добавлять существующие элементы.
  /// </summary>
  private List<long> FillIDsList(IList items)
  {
    List<long> longList = new List<long>(items.Count);
    Func<object, object> func = this._describer == null ? new Func<object, object>(((AttrListBoxBase) this).GetItemForAttributeValues) : new Func<object, object>(this.OnGetItemForAttributeValuesFromDescriber);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool flag = true;
      if (this.AttributeInfo != null && this._attrValues != null)
        flag = this._attrValues.AttributeType != FieldTypes.ftObjectLinkByID;
      foreach (object obj1 in (IEnumerable) items)
      {
        object obj2 = func(obj1);
        if (obj2 == DBNull.Value)
          obj2 = (object) -1L;
        long int64 = Convert.ToInt64(obj2);
        long num = int64;
        if (int64 != -1L && flag)
        {
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(int64, false);
          if (objectActualCopy != null)
            num = objectActualCopy.ObjectID;
          else
            continue;
        }
        if (!longList.Contains(num))
          longList.Add(num);
      }
    }
    return longList;
  }

  /// <summary>Выбор значения через дескриптор.</summary>
  /// <param name="selectedItem">Выбранный в списке элемент</param>
  /// <returns>Список новых элементов</returns>
  private Dictionary<long, object> GetValueFromDescriber(object selectedItem)
  {
    Dictionary<long, object> dictionary = (Dictionary<long, object>) null;
    if (this._describer.GetPropDescriptorEditor(this._attrValues.AttributeID) is UITypeEditor descriptorEditor)
    {
      using (ServiceContainer provider = new ServiceContainer())
      {
        using (DropDownEditorForm serviceInstance = new DropDownEditorForm())
        {
          provider.AddService(typeof (IWindowsFormsEditorService), (object) serviceInstance);
          ITypeDescriptorContext context = (ITypeDescriptorContext) new ControlsContext(this.Values, this._describer, this.ParentInfo);
          switch (descriptorEditor.GetEditStyle(context))
          {
            case UITypeEditorEditStyle.Modal:
            case UITypeEditorEditStyle.DropDown:
              object obj = descriptorEditor.EditValue(context, (System.IServiceProvider) provider, selectedItem);
              if (obj != null)
              {
                if (!(obj is object[] objArray))
                  objArray = new object[1]{ obj };
                dictionary = new Dictionary<long, object>(objArray.Length);
                foreach (object propertyValue in objArray)
                {
                  object attributeValue = this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, propertyValue);
                  if (attributeValue != null && attributeValue != DBNull.Value && attributeValue is long)
                  {
                    long int64 = Convert.ToInt64(attributeValue);
                    if (!dictionary.ContainsKey(int64))
                      dictionary.Add(int64, propertyValue);
                  }
                }
                break;
              }
              break;
          }
        }
      }
    }
    return dictionary == null || dictionary.Count <= 0 ? (Dictionary<long, object>) null : dictionary;
  }

  /// <summary>Инициализируем сервисы.</summary>
  private void InitServices()
  {
    this._services.AddService(typeof (ICurrentUserAndRole), ApplicationServices.Container.GetService(typeof (ICurrentUserAndRole)));
    this._services.AddService(typeof (IDefaultCommands4ObjTypes), (object) (ApplicationServices.Container.GetService(typeof (IDefaultCommands4ObjTypes)) as IDefaultCommands4ObjTypes));
    this._services.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InParametersCard));
    this._services.AddService(typeof (IIODispatcher), (object) new IODispatcher());
    INotificationService service = ApplicationServices.Container.GetService(typeof (INotificationService)) as INotificationService;
    this._services.AddService(typeof (INotificationService), (object) service);
    if (service is SwitchedNotificationService notificationService)
      notificationService.Enabled = true;
    this._services.AddService(typeof (IFiltrationService), (object) ServiceUtils.GetService<IFiltrationService>((object) ApplicationServices.Container, true));
  }

  /// <summary>Освобождаем сервисы.</summary>
  private void ReleaseServices()
  {
    this._services.RemoveService(typeof (ICurrentUserAndRole));
    this._services.RemoveService(typeof (IDefaultCommands4ObjTypes));
    this._services.RemoveService(typeof (INotificationService));
    this._services.RemoveService(typeof (IFiltrationService));
  }

  /// <summary>Проверка доступности кнопок и пунктов меню.</summary>
  protected override void CheckAccessibilityButtons()
  {
    if (this._btnForm != null)
      this._btnForm.Enabled = this._mbiForm.Enabled = this._lst.Items.Count != 0 && this._lst.SelectedIndex != -1 && this._lst.SelectedIndices.Count == 1;
    this._mbiPaste.Enabled = this.IsPasteEnabled;
    if (this._enabled)
    {
      this._mbiAdd.Enabled = true;
      if (this._lst.Items.Count == 0)
        this._mbiEdit.Enabled = this._mbiDel.Enabled = this._mbiClear.Enabled = false;
      else if (this._lst.SelectedIndex == -1)
      {
        this._mbiEdit.Enabled = this._mbiDel.Enabled = false;
        this._mbiClear.Enabled = true;
      }
      else
      {
        this._mbiDel.Enabled = this._mbiClear.Enabled = true;
        this._mbiEdit.Enabled = this._lst.SelectedIndices.Count == 1;
      }
    }
    else
      this._mbiAdd.Enabled = this._mbiEdit.Enabled = this._mbiDel.Enabled = this._mbiClear.Enabled = false;
    base.CheckAccessibilityButtons();
  }

  /// <summary>Создание нового элемент.</summary>
  /// <param name="item">Значение из списка значений атрибута</param>
  /// <returns>Созданный элемент</returns>
  protected override object CreateItemForListBox(object item)
  {
    bool _objectVersionProcessed = true;
    if (this.AttributeInfo != null && this._attrValues != null)
      _objectVersionProcessed = this._attrValues.AttributeType != FieldTypes.ftObjectLinkByID;
    return (object) new ObjectIDToCaption(item.GetType() != typeof (long) ? -1L : Convert.ToInt64(item), _objectVersionProcessed);
  }

  /// <summary>Получение элемента.</summary>
  /// <param name="value">Элемент в списке элементов</param>
  /// <returns>Значение</returns>
  protected override object GetItemForAttributeValues(object value)
  {
    object obj = (object) DBNull.Value;
    return !(value is ObjectIDToCaption objectIdToCaption) ? obj : (object) objectIdToCaption.ObjectID;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.MenuItemClick -= new EventHandler(this.On_btnAddEdit_Click);
      this._lst.SelectedIndexChanged -= new EventHandler(this.On_lst_SelectedIndexChanged);
      this._lst.MouseUp -= new MouseEventHandler(this.On_lst_MouseUp);
      this._bar.Dispose();
      this.ReleaseServices();
      this._services.Dispose();
      if (this._btnForm != null)
        this._btnForm.Click -= new EventHandler(this.On_btnForm_Click);
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttrListBoxBtn));
    ((ISupportInitialize) this._err).BeginInit();
    this.SuspendLayout();
    this._err.SetIconAlignment((Control) this._lst, (ErrorIconAlignment) componentResourceManager.GetObject("_lst.IconAlignment"));
    this._err.SetIconPadding((Control) this._lst, (int) componentResourceManager.GetObject("_lst.IconPadding"));
    this._lst.SelectionMode = SelectionMode.MultiExtended;
    this._lst.SelectedIndexChanged += new EventHandler(this.On_lst_SelectedIndexChanged);
    this._lst.MouseUp += new MouseEventHandler(this.On_lst_MouseUp);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.Control;
    this._err.SetIconAlignment((Control) this, (ErrorIconAlignment) componentResourceManager.GetObject("$this.IconAlignment"));
    this._err.SetIconPadding((Control) this, (int) componentResourceManager.GetObject("$this.IconPadding"));
    this.Name = nameof (AttrListBoxBtn);
    this.Controls.SetChildIndex((Control) this._lst, 0);
    ((ISupportInitialize) this._err).EndInit();
    this.ResumeLayout(false);
  }
}
