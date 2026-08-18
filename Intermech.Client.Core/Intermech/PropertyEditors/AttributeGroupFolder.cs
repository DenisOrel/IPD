
// Type: Intermech.PropertyEditors.AttributeGroupFolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class AttributeGroupFolder : CustomFolder
{
  protected string noteValue = string.Empty;
  protected string areaValue = string.Empty;
  protected string languageValue = string.Empty;
  protected Guid guidValue = Guid.Empty;
  private bool _useFilter = true;
  protected DataTable dataTableGroups;
  protected MenuButtonItem miAddGroup;
  private PropDescriptor guidPropDescriptor;

  public override bool DelEnabled => true;

  public override bool AddChildEnabled => true;

  public override bool NeedApply => true;

  public override bool NeedSave => true;

  public override bool CutEnabled => true;

  public override bool PasteEnabled => true;

  public override bool NeedPageSave => true;

  public AttributeGroupFolder(
    Guid aInstGuid,
    string aText,
    object aNodeParent,
    int aId,
    bool isNew,
    string aNote,
    string aArea,
    string aLanguage,
    Guid aGuid)
    : base(aInstGuid, aText, aNodeParent, (object) aId, isNew)
  {
    this.noteValue = aNote;
    this.areaValue = aArea;
    this.languageValue = aLanguage;
    this.guidValue = aGuid;
    if (Statics.IconSrv == null)
      return;
    this.node.ImageIndex = Statics.IconSrv.IndexOf(12, aId == -1 ? -1 : 0);
    this.node.SelectedImageIndex = this.node.ImageIndex;
  }

  public AttributeGroupFolder(
    Guid aInstGuid,
    string aText,
    object aNodeParent,
    int aId,
    bool isNew,
    string aNote,
    string aArea,
    string aLanguage,
    Guid aGuid,
    bool useFilter)
    : this(aInstGuid, aText, aNodeParent, aId, isNew, aNote, aArea, aLanguage, aGuid)
  {
    this._useFilter = useFilter;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetAttributesGroup(Convert.ToInt32(this.Id));
  }

  public override IDBSecurity GetSecurity(IUserSession session, object id)
  {
    return (int) id == -1 ? session.GetAttributeTypeCollection((int) id) as IDBSecurity : base.GetSecurity(session, id);
  }

  public override IFolder AddChildCallback()
  {
    if (this.activeMenuItem == null || this.activeMenuItem == this.miAdd)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        AttributeTypePropertiesValidator validator = (this.GetServerObject(sessionKeeper.Session) as IDBAttributesGroup).Attributes.GetValidator(FieldTypes.ftString);
        return (IFolder) new AttributeFolder(this.instGuid, validator.Name, (object) this.Node, 0, true, validator.ShortName, validator.Alias, validator.Note, validator.FieldType, validator.DefaultValue, validator.MultiValueMode[0], validator.Computed[0], validator.SizeType[0], validator.Formula.ToString(), validator.Unique[0], validator.LevelID, validator.LanguageID, validator.AttributeGuid, validator.AreaID, this.StoreClientCacheTimestamp(validator.PossibleValuesTable), validator.OptimizationMode[0], validator.IsContent, validator.Options, validator.Mask);
      }
    }
    return this.activeMenuItem == this.miAddGroup ? (IFolder) new AttributeGroupFolder(this.instGuid, LocalizationHolder.rm.GetString("Client.Core_66"), (object) this.Node, CoreConsts.IDGeneratorNextValue, true, string.Empty, string.Empty, string.Empty, Guid.NewGuid()) : (IFolder) null;
  }

  public override IFolder AddChildDubbedCallback(IFolder ifolder)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int id = (int) (ifolder.Node.Tag as IFolder).Id;
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(id);
      return attributeType == null ? (IFolder) null : (IFolder) new AttributeFolder(this.instGuid, attributeType.Name, (object) this.Node, attributeType.AttributeID, false, attributeType.ShortName, attributeType.Alias, attributeType.Note, attributeType.AttributeType, attributeType.DefaultValue, attributeType.MultipleValued, attributeType.Computed, attributeType.SizeType, attributeType.Formula, attributeType.UniqueMode, attributeType.LevelID, (attributeType as IDBLanguage).LanguageID, (attributeType as IDBGuid).GUID, (attributeType as IDBSubjectArea).SubjectAreas, this.StoreClientCacheTimestamp(attributeType.GetPossibleValues()), attributeType.OptimizationMode, attributeType.IsContent, attributeType.Options, attributeType.Mask);
    }
  }

  public override void LoadDataTable(bool reload)
  {
    this.dataTable = DataHolders.AttributesHolder.LoadData((reload ? 1 : 0) != 0, this.Id);
    this.dataTableGroups = DataHolders.AttributeGroupsHolder.LoadData(reload);
  }

  public override void PopulateCallback(bool reload)
  {
    ISelectorFilter treeView = this.Node.TreeView as ISelectorFilter;
    if (this.dataTableGroups != null)
    {
      foreach (DataRow dataRow in this.dataTableGroups.Select("F_PARENT_ID = " + this.Id.ToString()))
      {
        if (treeView == null || !this._useFilter || treeView != null && treeView.IsInFilter(12, (object) Convert.ToInt32(dataRow["F_GROUP_ID"])))
        {
          AttributeGroupFolder attributeGroupFolder = new AttributeGroupFolder(this.instGuid, dataRow["F_GROUP_NAME"].ToString(), (object) this.Node, Convert.ToInt32(dataRow["F_GROUP_ID"]), false, dataRow["F_NOTE"].ToString(), dataRow["F_AREA_ID"].ToString(), Convert.ToString(dataRow["F_LANGUAGE_ID"]), new Guid(dataRow["F_GUID"].ToString()), this._useFilter);
        }
      }
    }
    foreach (DataRow row in (InternalDataCollectionBase) this.dataTable.Rows)
    {
      if (treeView == null || !this._useFilter || treeView != null && treeView.IsInFilter(this.ListCategoryValue, (object) Convert.ToInt32(row["F_ATTRIBUTE_ID"])))
      {
        object currentDateTime = row["F_DEFAULT_VALUE"];
        if (Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]) == 4 && currentDateTime != null && currentDateTime is string)
          currentDateTime = DateTimeCultureConverter.ConvertUniversalDateTimeStringToCurrentDateTime(currentDateTime.ToString());
        AttributeFolder attributeFolder = new AttributeFolder(this.instGuid, row["F_NAME"].ToString(), (object) this.Node, Convert.ToInt32(row["F_ATTRIBUTE_ID"]), false, row["F_SHORT_NAME"].ToString(), row["F_ALIAS"].ToString(), row["F_NOTE"].ToString(), (FieldTypes) Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]), currentDateTime, (MultiValueModes) Convert.ToInt32(row["F_MULTIPLE_VALUED"]), (ComputeValueModes) Convert.ToInt32(row["F_COMPUTED"]), (long) Convert.ToInt32(row["F_SIZE_TYPE"]), row["F_FORMULA"].ToString(), (UniqueValueModes) Convert.ToInt32(row["F_UNIQUE"]), Convert.ToInt32(row["F_LEVEL_ID"]), row["F_LANGUAGE_ID"].ToString(), new Guid(row["F_GUID"].ToString()), row["F_AREA_ID"].ToString(), (DataTable) null, (OptimizationModes) Convert.ToInt32(row["F_INVIEW"]), Convert.ToInt32(row["F_CONTENT"]) == 1, (AttributeOptions) Convert.ToInt32(row["F_OPTIONS"]), row["F_MASK"].ToString());
      }
    }
  }

  public override bool LoadDataCallback(bool reload)
  {
    PropertyGrid propertyGrid = (this.GetPropertyForm() as IConfigPage).PropertyGrid;
    if (propertyGrid == null)
      return true;
    EventsHolder.BlockOnChange = true;
    try
    {
      propertyGrid.SelectedObject = (object) this;
      this.guidPropDescriptor.SetReadOnly(!this.IsVirtualFolder && !ClientConsts.InDeveloperMode);
      if (this.IsVirtualFolder)
      {
        this.idValue = (object) 0;
        this.textValue = this.node.Text;
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttributesGroup serverObject = this.GetServerObject(sessionKeeper.Session) as IDBAttributesGroup;
          this.idValue = (object) serverObject.GroupID;
          this.textValue = serverObject.GroupName;
          this.noteValue = serverObject.Note;
          this.areaValue = (serverObject as IDBSubjectArea).SubjectAreas;
          this.languageValue = (serverObject as IDBLanguage).LanguageID;
          this.guidValue = (serverObject as IDBGuid).GUID;
        }
      }
      this.PropDescriptorCollection[0].SetValue((object) this, (object) this.textValue);
      this.PropDescriptorCollection[1].SetValue((object) this, (object) this.noteValue);
      this.PropDescriptorCollection[2].SetValue((object) this, (object) new LanguagePropertyClass(this.languageValue));
      this.PropDescriptorCollection[3].SetValue((object) this, (object) new SubjectAreaPropertyClass(this.areaValue));
      this.PropDescriptorCollection[4].SetValue((object) this, this.idValue);
      this.PropDescriptorCollection[5].SetValue((object) this, (object) this.guidValue);
    }
    finally
    {
      EventsHolder.BlockOnChange = false;
    }
    return true;
  }

  /// <summary>Назначение системного Guid</summary>
  public override void SetSystemGuid()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IGuidService customService = (IGuidService) sessionKeeper.Session.GetCustomService(typeof (IGuidService));
      if (customService != null)
      {
        this.textValue = (string) this.PropDescriptorCollection[0].GetValue((object) this);
        this.noteValue = (string) this.PropDescriptorCollection[1].GetValue((object) this);
        Guid nextSystemGuid = customService.GenerateNextSystemGuid(12, this.textValue, this.noteValue);
        (this.GetServerObject(sessionKeeper.Session) as IDBAttributesGroup).SetGUID(nextSystemGuid);
      }
      base.SetSystemGuid();
    }
  }

  public override void GetContextMenu(
    ContextMenuBarItem contextMenu,
    IEventsDispatcher iEventsDispatcher)
  {
    base.GetContextMenu(contextMenu, iEventsDispatcher);
    this.miAdd.Text = LocalizationHolder.rm.GetString("Client.Core_CreateAttr");
    this.miAddGroup = new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_CreateAttrGroup"), (EventHandler) iEventsDispatcher.EventsList[(object) ContextMenuID.cmiAddGroup]);
    INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    if (service != null)
      this.miAddGroup.ImageIndex = service.ImageIndex("imgInsertItem");
    if ((int) this.idValue != -1)
    {
      int num = contextMenu.Items.IndexOf((ToolbarItemBase) this.miAdd);
      if (num != -1 && num < contextMenu.Items.Count - 1)
        contextMenu.Items.Insert(num + 1, (ToolbarItemBase) this.miAddGroup);
      else
        contextMenu.Items.AddRange((ToolbarItemBase[]) new MenuButtonItem[1]
        {
          this.miAddGroup
        });
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(this.GetServerObject(sessionKeeper.Session) as IDBAttributesGroup as IDBGuid).IsSystemGUID || this.miSetSystemGuid == null)
        return;
      contextMenu.Items.Remove((ToolbarItemBase) this.miSetSystemGuid);
    }
  }

  public override bool SaveCallback()
  {
    this.textValue = (string) this.PropDescriptorCollection[0].GetValue((object) this);
    this.noteValue = (string) this.PropDescriptorCollection[1].GetValue((object) this);
    this.languageValue = ((LanguagePropertyClass) this.PropDescriptorCollection[2].GetValue((object) this)).Language;
    this.areaValue = ((SubjectAreaPropertyClass) this.PropDescriptorCollection[3].GetValue((object) this)).Areas;
    this.guidValue = (Guid) this.PropDescriptorCollection[5].GetValue((object) this);
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this.IsVirtualFolder)
        {
          TreeNode treeNode = this.Node;
          while (treeNode != null && !(treeNode.Tag is AttributesFolder))
            treeNode = treeNode.Parent;
          this.idValue = (object) ((treeNode.Tag as CustomFolder).GetServerObject(sessionKeeper.Session) as IDBAttributesGroupCollection).Create(this.textValue, this.noteValue, this.languageValue, this.areaValue, this.guidValue);
          if (this.GetServerObject(sessionKeeper.Session) is IDBAttributesGroup serverObject)
          {
            if (this.node.Parent != null)
            {
              if (this.node.Parent.Tag != null)
              {
                if (this.node.Parent.Tag is AttributeGroupFolder)
                  serverObject.ParentID = (int) ((DBPropDescriptorHolder) this.node.Parent.Tag).Id;
              }
            }
          }
        }
        else
        {
          IDBAttributesGroup serverObject = this.GetServerObject(sessionKeeper.Session) as IDBAttributesGroup;
          if (serverObject.GroupName != this.textValue)
            serverObject.GroupName = this.textValue;
          if (serverObject.Note != this.noteValue)
            serverObject.Note = this.noteValue;
          IDBSubjectArea dbSubjectArea = serverObject as IDBSubjectArea;
          if (dbSubjectArea.SubjectAreas != this.areaValue)
            dbSubjectArea.SubjectAreas = this.areaValue;
          IDBLanguage dbLanguage = serverObject as IDBLanguage;
          if (dbLanguage.LanguageID != this.languageValue)
            dbLanguage.LanguageID = this.languageValue;
          if ((serverObject as IDBGuid).GUID != this.guidValue)
            serverObject.SetGUID(this.guidValue);
        }
      }
      this.guidPropDescriptor.SetReadOnly(true);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      return false;
    }
    finally
    {
      DataHolders.AttributeGroupsHolder.ClearInfo();
    }
    return true;
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    pdc.Add((PropertyDescriptor) new PropDescriptor(0, (object) this, LocalizationHolder.rm.GetString("Client.Core_33"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.AttributeGroup_Name, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(1, (object) this, LocalizationHolder.rm.GetString("Client.Core_35"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.AttributeGroup_Note, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(2, (object) this, LocalizationHolder.rm.GetString("Client.Core_1168"), (object) null, typeof (LanguagePropertyClass), (TypeConverter) new LanguageConverter(), (object) null, string.Empty, PropDescriptions.AttributeGroup_Language, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(3, (object) this, LocalizationHolder.rm.GetString("Client.Core_70"), (object) null, typeof (SubjectAreaPropertyClass), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.AttributeGroup_Area, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(4, (object) this, LocalizationHolder.rm.GetString("Client.Core_37"), (object) null, typeof (int), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.AttributeGroup_Ident, true, true, false));
    this.guidPropDescriptor = new PropDescriptor(5, (object) this, LocalizationHolder.rm.GetString("Client.Core_39"), (object) null, typeof (Guid), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.AttributeGroup_GUID, false, true, false);
    pdc.Add((PropertyDescriptor) this.guidPropDescriptor);
  }

  public override void SetContextMenuItemStatus(ContextMenuBarItem contextMenu)
  {
    base.SetContextMenuItemStatus(contextMenu);
    if ((int) this.idValue == -1 && this.CutEnabled)
      this.miCut.Enabled = false;
    if ((int) this.idValue == -1 || !this.PasteEnabled || !(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
      return;
    object dataObject = service.GetDataObject();
    IDBAttributeIDCollection attributeIdCollection = dataObject as IDBAttributeIDCollection;
    IDBAttributeGroupIDCollection groupIdCollection = dataObject as IDBAttributeGroupIDCollection;
    this.miPaste.Enabled = (attributeIdCollection != null || groupIdCollection != null) && !this.InChange;
  }

  public override void Cut()
  {
    if (!(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
      return;
    DBAttributeGroupIDCollection clipboardObject = new DBAttributeGroupIDCollection(new ArrayList((ICollection) new DBAttributeGroupID[1]
    {
      new DBAttributeGroupID((int) this.idValue)
    }));
    service.SetDataObject((object) clipboardObject);
  }

  public override void Paste()
  {
    if (!this.CanPaste)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
        return;
      object dataObject = service.GetDataObject();
      if (dataObject is IDBAttributeGroupIDCollection groupIdCollection)
      {
        if (!(this.GetServerObject(sessionKeeper.Session) is IDBAttributesGroup serverObject))
          return;
        int groupId = serverObject.GroupID;
        if (groupIdCollection.Count > 0)
        {
          List<int> intList = new List<int>();
          intList.Add((int) this.Id);
          TreeNode tn = this.node;
          while (tn.Parent != null)
          {
            tn = tn.Parent;
            if (!(tn.Tag is AttributesFolder))
              intList.Add((int) (tn.Tag as CustomFolder).Id);
            else
              break;
          }
          List<int> processedGroups = new List<int>();
          try
          {
            for (int index = 0; index < groupIdCollection.Count; ++index)
            {
              int attributeGroupId = groupIdCollection.GetAttributeGroupID(index).AttributeGroupID;
              if (intList.IndexOf(attributeGroupId) == -1)
              {
                processedGroups.Add(attributeGroupId);
                IDBAttributesGroup attributesGroup = sessionKeeper.Session.GetAttributesGroup(attributeGroupId);
                if (attributesGroup != null)
                  attributesGroup.ParentID = groupId;
              }
            }
            DataHolders.AttributeGroupsHolder.ClearInfo();
          }
          catch
          {
            DataHolders.AttributeGroupsHolder.ClearInfo();
            if (!ClientConsts.IsFakeNode(tn))
              (tn.Tag as IFolder).Populate(false);
            throw;
          }
          this.ProcessPaste((CustomFolder) (tn.Tag as AttributesFolder), processedGroups);
          if (!ClientConsts.IsFakeNode(this.node))
            this.Populate(false);
        }
      }
      if (!(dataObject is IDBAttributeIDCollection attributeIdCollection) || !(this.GetServerObject(sessionKeeper.Session) is IDBAttributesGroup serverObject1) || attributeIdCollection.Count <= 0)
        return;
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < attributeIdCollection.Count; ++index)
        arrayList.Add((object) attributeIdCollection.GetAttributeID(index).AttribyteID);
      serverObject1.IncludeAttribute((int[]) arrayList.ToArray(typeof (int)));
      DataHolders.AttributesHolder.ClearInfo((object) (int) this.idValue);
      if (ClientConsts.IsFakeNode(this.node))
        return;
      this.Populate(false);
    }
  }

  private void ProcessPaste(CustomFolder folder, List<int> processedGroups)
  {
    TreeNode node = folder.Node;
    if (processedGroups.Count == 0 || ClientConsts.IsFakeNode(node))
      return;
    int index = 0;
    while (index < node.Nodes.Count && processedGroups.Count > 0)
    {
      if (node.Nodes[index].Tag is AttributeGroupFolder tag && (int) tag.Id != -1)
      {
        int id = (int) tag.Id;
        if (processedGroups.IndexOf(id) == -1)
        {
          this.ProcessPaste((CustomFolder) tag, processedGroups);
        }
        else
        {
          bool flag = false;
          if (this.node.TreeView.SelectedNode == node.Nodes[index])
            flag = true;
          node.Nodes[index].Remove();
          if (flag)
          {
            this.node.TreeView.SelectedNode = this.node;
            continue;
          }
          continue;
        }
      }
      ++index;
    }
  }

  public override void ConstructPages(TabControl tabControl)
  {
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage, (object) TabPagesHolder.TabPages(this.instGuid).ListTabPage, (object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage, (object) TabPagesHolder.TabPages(this.instGuid).ActionsTabPage);
  }

  public override int ExportCategoryValue => 3;

  public override int ListCategoryValue => 3;

  public override int Category => 12;
}
