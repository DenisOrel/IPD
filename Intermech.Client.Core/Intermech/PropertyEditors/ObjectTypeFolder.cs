
// Type: Intermech.PropertyEditors.ObjectTypeFolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DatabaseConfigurator;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class ObjectTypeFolder : CustomFolder
{
  protected bool docExtensionsEnabled;
  protected int objtypeDocumentId = -1;
  /// <summary>
  /// Является ли данный объект вновь созданным типом объекта, либо он создан на основании существующего типа
  /// </summary>
  internal bool IsNewType;
  private int iconIndex4Category;
  private Icon iconValue;
  private ObjectTypeProperties oldOTP;
  private PropDescriptor guidPropDescriptor;
  private PropDescriptor iconPropDescriptor;
  private PropDescriptor captionAttributePropDescriptor;
  private PropDescriptor anyAttributePropDescriptor;
  private PropDescriptor identPropDescriptor;
  private PropDescriptor lifetimePropDescriptor;
  private PropDescriptor optionCurrentProjectEnabledDescriptor;
  private PropDescriptor optionCheckParentAccessDescriptor;
  private PropDescriptor optionLocalObjectTypeDescriptor;
  private PropDescriptor optionDisableManualCreateDescriptor;
  private PropDescriptor optionCreateSnapshotsDescriptor;
  private PropDescriptor optionAutoContentEnabledDescriptor;
  private PropDescriptor optionMandateAccessDescriptor;
  private PropDescriptor optionAttributesIndexDescriptor;
  private PropDescriptor optionAutoCreateSnapshotsDescriptor;
  private PropDescriptor optionDisablePrototypingDescriptor;
  private PropDescriptor optionNotificationsEnabledDescriptor;
  private PropDescriptor optionForumEnabledDescriptor;
  private PropDescriptor optionExtendedAuditDescriptor;
  private PropDescriptor optionEnableWebEditDescriptor;
  private ObjectTypeProperties objectTypeProperties;
  private MenuButtonItem miRecreateView;
  private new MenuButtonItem miCut;
  private new MenuButtonItem miPaste;
  private bool Static;
  private int schemaId;
  private bool changeObjectsSchema;

  internal void SetIconForVirtual(Icon aIconValue)
  {
    if (!this.IsVirtualFolder)
      return;
    this.iconValue = aIconValue;
  }

  public int SchemaId
  {
    get => this.schemaId;
    set
    {
      if (this.schemaId == value)
        return;
      this.schemaId = value;
      StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage, true);
      EventsHolder.FireWasChanged((object) this, this.instGuid, new EventArgs());
    }
  }

  public bool ChangeObjectsSchema
  {
    set => this.changeObjectsSchema = value;
  }

  public override bool DelEnabled => true;

  public override bool AddChildEnabled => true;

  public override bool NeedApply => true;

  public override bool NeedSave => true;

  public override bool NeedPageSave => true;

  public ObjectTypeFolder(
    Guid aInstGuid,
    string aText,
    object aNodeParent,
    int aId,
    bool isNew,
    string aObjName,
    ObjectVersionModes aVersionMode,
    string aNote,
    int aDefaultRelation,
    Guid aGuid,
    string aArea,
    int aCaptionAttribute,
    bool aAnyAttributes,
    string aShortName,
    int aLifetimeReserve,
    ObjectTypeOptions aOptions,
    int aSchemaId,
    bool aStatic)
    : this(aInstGuid, aText, aNodeParent, aId, isNew, aObjName, aVersionMode, aNote, aDefaultRelation, aGuid, aArea, aCaptionAttribute, aAnyAttributes, aShortName, aLifetimeReserve, aOptions, aSchemaId)
  {
    this.Static = aStatic;
    this.node.TreeView.BeginUpdate();
    try
    {
      this.node.Nodes.Clear();
    }
    finally
    {
      this.node.TreeView.EndUpdate();
    }
  }

  public ObjectTypeFolder(
    Guid aInstGuid,
    string aText,
    object aNodeParent,
    int aId,
    bool isNew,
    string aObjName,
    ObjectVersionModes aVersionMode,
    string aNote,
    int aDefaultRelation,
    Guid aGuid,
    string aArea,
    int aCaptionAttribute,
    bool aAnyAttributes,
    string aShortName,
    int aLifetimeReserve,
    ObjectTypeOptions aOptions,
    int aSchemaId)
    : base(aInstGuid, aText, aNodeParent, (object) aId, isNew)
  {
    this.InitDocExtensionsFlag();
    this.changeObjectsSchema = false;
    this.schemaId = aSchemaId;
    this.objectTypeProperties = new ObjectTypeProperties(aId, aText, aObjName, aNote, aVersionMode, aDefaultRelation, aArea, aGuid, aCaptionAttribute, aAnyAttributes, InheritModes.Private, aShortName, aLifetimeReserve, aOptions, aSchemaId);
    if (Statics.IconSrv != null)
      this.iconIndex4Category = 0;
    this.iconValue = (Icon) null;
    int num = 0;
    if (!isNew)
    {
      if (Statics.IconSrv != null)
      {
        num = Statics.IconSrv.IndexOf(4, aId);
        if (this.iconIndex4Category != num)
          this.iconValue = Statics.IconSrv.GetIconEx(4, aId);
      }
    }
    else
      num = this.iconIndex4Category;
    this.node.ImageIndex = num;
    this.node.SelectedImageIndex = num;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetObjectType((int) this.Id);
  }

  public override IFolder AddChildCallback()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.GetLCSchemaCollection();
      ObjectTypeFolder objectTypeFolder = new ObjectTypeFolder(this.instGuid, LocalizationHolder.rm.GetString("Client.Core_1171"), (object) this.Node, CoreConsts.IDGeneratorNextValue, true, string.Empty, ObjectVersionModes.SingleVersion, string.Empty, this.objectTypeProperties.DefaultRelation, Guid.NewGuid(), this.objectTypeProperties.AreaID, 0, this.objectTypeProperties.AnyAttributes, string.Empty, this.objectTypeProperties.LifetimeReserve, ObjectTypeOptions.None, this.objectTypeProperties.SchemaID);
      objectTypeFolder.IsNewType = true;
      if (Statics.IconSrv != null)
      {
        int num = Statics.IconSrv.IndexOf(4, this.objectTypeProperties.ObjectType);
        Icon aIconValue = (Icon) null;
        int iconIndex4Category = this.iconIndex4Category;
        if (num != iconIndex4Category)
          aIconValue = Statics.IconSrv.GetIconEx(4, this.objectTypeProperties.ObjectType);
        objectTypeFolder.SetIconForVirtual(aIconValue);
      }
      return (IFolder) objectTypeFolder;
    }
  }

  public override void LoadDataTable(bool reload)
  {
    this.dataTable = DataHolders.ObjectTypesHolder.LoadData((reload ? 1 : 0) != 0, this.Id);
  }

  public override void PopulateCallback(bool reload)
  {
    if (this.Static)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) this.dataTable.Rows)
    {
      if (!(this.Node.TreeView is ISelectorFilter treeView) || treeView != null && treeView.IsInFilter(this.ListCategoryValue, (object) Convert.ToInt32(row["F_OBJECT_TYPE"])))
      {
        ObjectTypeFolder objectTypeFolder = new ObjectTypeFolder(this.instGuid, row["F_OBJ_TYPE_NAME"].ToString(), (object) this.Node, Convert.ToInt32(row["F_OBJECT_TYPE"]), false, row["F_OBJ_NAME"].ToString(), (ObjectVersionModes) Convert.ToInt32(row["F_VERSIONABLE"]), row["F_NOTE"].ToString(), Convert.ToInt32(row["F_DEFAULT_RELATION"]), new Guid(row["F_GUID"].ToString()), row["F_AREA_ID"].ToString(), Convert.ToInt32(row["F_CAPTION_ATTRIBUTE"]), Convert.ToInt16(row["F_ANY_ATTRIBUTES"]) == (short) 1, row["F_SHORT_NAME"].ToString(), Convert.ToInt32(row["F_DEL_TIME"]), (ObjectTypeOptions) Convert.ToInt32(row["F_OPTIONS"]), Convert.ToInt32(row["F_SCHEMA_ID"]));
      }
    }
  }

  public override bool LoadDataCallback(bool reload)
  {
    PropertyGrid propertyGrid = (this.GetPropertyForm() as IConfigPage).PropertyGrid;
    if (propertyGrid != null)
    {
      EventsHolder.BlockOnChange = true;
      try
      {
        propertyGrid.SelectedObject = (object) this;
        this.guidPropDescriptor.SetReadOnly(!this.IsVirtualFolder && !ClientConsts.InDeveloperMode);
        if (this.IsVirtualFolder)
        {
          this.textValue = this.node.Text;
        }
        else
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObjectType serverObject = this.GetServerObject(sessionKeeper.Session) as IDBObjectType;
            this.idValue = (object) serverObject.ObjectType;
            this.textValue = serverObject.ObjectTypeName;
            this.objectTypeProperties = serverObject.PropertiesStructure;
            int num = this.iconIndex4Category;
            this.iconValue = (Icon) null;
            if (Statics.IconSrv != null)
            {
              num = Statics.IconSrv.IndexOf(4, (int) this.idValue);
              if (num != this.iconIndex4Category)
                this.iconValue = Statics.IconSrv.GetIconEx(4, (int) this.idValue);
            }
            this.node.ImageIndex = num;
            this.node.SelectedImageIndex = num;
          }
        }
        this.PropDescriptorCollection[0].SetValue((object) this, (object) this.textValue);
        this.PropDescriptorCollection[1].SetValue((object) this, (object) this.objectTypeProperties.ObjectInstanceName);
        this.PropDescriptorCollection[2].SetValue((object) this, (object) this.objectTypeProperties.Note);
        this.PropDescriptorCollection[3].SetValue((object) this, (object) this.iconValue);
        this.PropDescriptorCollection[4].SetValue((object) this, (object) new ObjectVersionModePropertyClass(this.objectTypeProperties.Versionable));
        this.PropDescriptorCollection[5].SetValue((object) this, (object) new RelationTypePropertyClass(this.objectTypeProperties.DefaultRelation));
        this.PropDescriptorCollection[6].SetValue((object) this, (object) new SubjectAreaPropertyClass(this.objectTypeProperties.AreaID));
        this.PropDescriptorCollection[7].SetValue((object) this, (object) this.objectTypeProperties.ObjectTypeGuid);
        this.PropDescriptorCollection[8].SetValue((object) this, (object) new AttributePropertyClass(this.objectTypeProperties.CaptionAttribute));
        this.PropDescriptorCollection[9].SetValue((object) this, (object) new BoolPropertyClass(this.objectTypeProperties.AnyAttributes));
        this.PropDescriptorCollection[10].SetValue((object) this, (object) this.objectTypeProperties.ObjectTypeShortName);
        this.PropDescriptorCollection[11].SetValue((object) this, this.idValue);
        this.lifetimePropDescriptor.SetValue((object) this, this.lifetimePropDescriptor.Converter.ConvertTo((object) this.objectTypeProperties.LifetimeReserve, typeof (string)));
        this.PropDescriptorCollection[13].SetValue((object) this, (object) new BoolPropertyClass((this.objectTypeProperties.Options & ObjectTypeOptions.CurrentProjectEnabled) == ObjectTypeOptions.CurrentProjectEnabled));
        this.PropDescriptorCollection[14].SetValue((object) this, (object) new BoolPropertyClass((this.objectTypeProperties.Options & ObjectTypeOptions.CheckParentAccess) == ObjectTypeOptions.CheckParentAccess));
        this.PropDescriptorCollection[15].SetValue((object) this, (object) new BoolPropertyClass((this.objectTypeProperties.Options & ObjectTypeOptions.LocalObjectType) == ObjectTypeOptions.LocalObjectType));
        this.PropDescriptorCollection[16 /*0x10*/].SetValue((object) this, (object) new BoolPropertyClass((this.objectTypeProperties.Options & ObjectTypeOptions.DisableManualCreate) == ObjectTypeOptions.DisableManualCreate));
        this.PropDescriptorCollection[17].SetValue((object) this, (object) new BoolPropertyClass((this.objectTypeProperties.Options & ObjectTypeOptions.CreateSnapshots) == ObjectTypeOptions.CreateSnapshots));
        this.PropDescriptorCollection[18].SetValue((object) this, (object) new BoolPropertyClass((this.objectTypeProperties.Options & ObjectTypeOptions.AutoContextEnabled) == ObjectTypeOptions.AutoContextEnabled));
        this.PropDescriptorCollection[19].SetValue((object) this, (object) new BoolPropertyClass((this.objectTypeProperties.Options & ObjectTypeOptions.MandateAccess) == ObjectTypeOptions.MandateAccess));
        this.PropDescriptorCollection[20].SetValue((object) this, (object) new BoolPropertyClass((this.objectTypeProperties.Options & ObjectTypeOptions.AttributesIndex) == ObjectTypeOptions.AttributesIndex));
        this.PropDescriptorCollection[21].SetValue((object) this, (object) new BoolPropertyClass((this.objectTypeProperties.Options & ObjectTypeOptions.AutoCreateSnapshots) == ObjectTypeOptions.AutoCreateSnapshots));
        this.PropDescriptorCollection[22].SetValue((object) this, (object) new BoolPropertyClass((this.objectTypeProperties.Options & ObjectTypeOptions.DisablePrototyping) == ObjectTypeOptions.DisablePrototyping));
        this.PropDescriptorCollection[23].SetValue((object) this, (object) new BoolPropertyClass((this.objectTypeProperties.Options & ObjectTypeOptions.NotificationsEnabled) == ObjectTypeOptions.NotificationsEnabled));
        this.PropDescriptorCollection[24].SetValue((object) this, (object) new BoolPropertyClass((this.objectTypeProperties.Options & ObjectTypeOptions.ForumEnabled) == ObjectTypeOptions.ForumEnabled));
        this.PropDescriptorCollection[25].SetValue((object) this, (object) new BoolPropertyClass((this.objectTypeProperties.Options & ObjectTypeOptions.ExtendedAudit) == ObjectTypeOptions.ExtendedAudit));
        this.PropDescriptorCollection[26].SetValue((object) this, (object) new BoolPropertyClass((this.objectTypeProperties.Options & ObjectTypeOptions.EnableWebEdit) == ObjectTypeOptions.EnableWebEdit));
        this.schemaId = this.objectTypeProperties.SchemaID;
      }
      finally
      {
        EventsHolder.BlockOnChange = false;
      }
    }
    return true;
  }

  /// <summary>назначение системного GUID</summary>
  public override void SetSystemGuid()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IGuidService customService = (IGuidService) sessionKeeper.Session.GetCustomService(typeof (IGuidService));
      if (customService != null)
      {
        this.textValue = (string) this.PropDescriptorCollection[0].GetValue((object) this);
        string note = (string) this.PropDescriptorCollection[2].GetValue((object) this);
        Guid nextSystemGuid = customService.GenerateNextSystemGuid(4, this.textValue, note);
        IDBObjectType serverObject = this.GetServerObject(sessionKeeper.Session) as IDBObjectType;
        this.objectTypeProperties.ObjectTypeGuid = nextSystemGuid;
        ObjectTypeProperties objectTypeProperties = this.objectTypeProperties;
        serverObject.PropertiesStructure = objectTypeProperties;
      }
      base.SetSystemGuid();
    }
  }

  public override bool SaveCallback()
  {
    this.textValue = (string) this.PropDescriptorCollection[0].GetValue((object) this);
    this.objectTypeProperties.ObjectTypeName = this.textValue;
    this.objectTypeProperties.ObjectInstanceName = (string) this.PropDescriptorCollection[1].GetValue((object) this);
    this.objectTypeProperties.Note = (string) this.PropDescriptorCollection[2].GetValue((object) this);
    this.iconValue = (Icon) this.PropDescriptorCollection[3].GetValue((object) this);
    this.objectTypeProperties.Versionable = ((ObjectVersionModePropertyClass) this.PropDescriptorCollection[4].GetValue((object) this)).ObjectVersionMode;
    this.objectTypeProperties.DefaultRelation = ((RelationTypePropertyClass) this.PropDescriptorCollection[5].GetValue((object) this)).RelationType;
    this.objectTypeProperties.AreaID = ((SubjectAreaPropertyClass) this.PropDescriptorCollection[6].GetValue((object) this)).Areas;
    this.objectTypeProperties.ObjectTypeGuid = (Guid) this.PropDescriptorCollection[7].GetValue((object) this);
    this.objectTypeProperties.CaptionAttribute = ((AttributePropertyClass) this.PropDescriptorCollection[8].GetValue((object) this)).Attribute;
    this.objectTypeProperties.AnyAttributes = ((BoolPropertyClass) this.PropDescriptorCollection[9].GetValue((object) this)).Boolean;
    this.objectTypeProperties.ObjectTypeShortName = (string) this.PropDescriptorCollection[10].GetValue((object) this);
    this.objectTypeProperties.LifetimeReserve = (int) this.lifetimePropDescriptor.Converter.ConvertTo(this.lifetimePropDescriptor.GetValue((object) this), typeof (int));
    if (((BoolPropertyClass) this.PropDescriptorCollection[13].GetValue((object) this)).Boolean)
      this.objectTypeProperties.Options |= ObjectTypeOptions.CurrentProjectEnabled;
    else
      this.objectTypeProperties.Options &= ~ObjectTypeOptions.CurrentProjectEnabled;
    if (((BoolPropertyClass) this.PropDescriptorCollection[14].GetValue((object) this)).Boolean)
      this.objectTypeProperties.Options |= ObjectTypeOptions.CheckParentAccess;
    else
      this.objectTypeProperties.Options &= ~ObjectTypeOptions.CheckParentAccess;
    if (((BoolPropertyClass) this.PropDescriptorCollection[15].GetValue((object) this)).Boolean)
      this.objectTypeProperties.Options |= ObjectTypeOptions.LocalObjectType;
    else
      this.objectTypeProperties.Options &= ~ObjectTypeOptions.LocalObjectType;
    if (((BoolPropertyClass) this.PropDescriptorCollection[16 /*0x10*/].GetValue((object) this)).Boolean)
      this.objectTypeProperties.Options |= ObjectTypeOptions.DisableManualCreate;
    else
      this.objectTypeProperties.Options &= ~ObjectTypeOptions.DisableManualCreate;
    if (((BoolPropertyClass) this.PropDescriptorCollection[17].GetValue((object) this)).Boolean)
      this.objectTypeProperties.Options |= ObjectTypeOptions.CreateSnapshots;
    else
      this.objectTypeProperties.Options &= ~ObjectTypeOptions.CreateSnapshots;
    if (((BoolPropertyClass) this.PropDescriptorCollection[18].GetValue((object) this)).Boolean)
      this.objectTypeProperties.Options |= ObjectTypeOptions.AutoContextEnabled;
    else
      this.objectTypeProperties.Options &= ~ObjectTypeOptions.AutoContextEnabled;
    if (((BoolPropertyClass) this.PropDescriptorCollection[19].GetValue((object) this)).Boolean)
      this.objectTypeProperties.Options |= ObjectTypeOptions.MandateAccess;
    else
      this.objectTypeProperties.Options &= ~ObjectTypeOptions.MandateAccess;
    if (((BoolPropertyClass) this.PropDescriptorCollection[20].GetValue((object) this)).Boolean)
      this.objectTypeProperties.Options |= ObjectTypeOptions.AttributesIndex;
    else
      this.objectTypeProperties.Options &= ~ObjectTypeOptions.AttributesIndex;
    if (((BoolPropertyClass) this.PropDescriptorCollection[21].GetValue((object) this)).Boolean)
      this.objectTypeProperties.Options |= ObjectTypeOptions.AutoCreateSnapshots;
    else
      this.objectTypeProperties.Options &= ~ObjectTypeOptions.AutoCreateSnapshots;
    if (((BoolPropertyClass) this.PropDescriptorCollection[22].GetValue((object) this)).Boolean)
      this.objectTypeProperties.Options |= ObjectTypeOptions.DisablePrototyping;
    else
      this.objectTypeProperties.Options &= ~ObjectTypeOptions.DisablePrototyping;
    if (((BoolPropertyClass) this.PropDescriptorCollection[23].GetValue((object) this)).Boolean)
      this.objectTypeProperties.Options |= ObjectTypeOptions.NotificationsEnabled;
    else
      this.objectTypeProperties.Options &= ~ObjectTypeOptions.NotificationsEnabled;
    if (((BoolPropertyClass) this.PropDescriptorCollection[24].GetValue((object) this)).Boolean)
      this.objectTypeProperties.Options |= ObjectTypeOptions.ForumEnabled;
    else
      this.objectTypeProperties.Options &= ~ObjectTypeOptions.ForumEnabled;
    if (((BoolPropertyClass) this.PropDescriptorCollection[25].GetValue((object) this)).Boolean)
      this.objectTypeProperties.Options |= ObjectTypeOptions.ExtendedAudit;
    else
      this.objectTypeProperties.Options &= ~ObjectTypeOptions.ExtendedAudit;
    if (((BoolPropertyClass) this.PropDescriptorCollection[26].GetValue((object) this)).Boolean)
      this.objectTypeProperties.Options |= ObjectTypeOptions.EnableWebEdit;
    else
      this.objectTypeProperties.Options &= ~ObjectTypeOptions.EnableWebEdit;
    this.objectTypeProperties.SchemaID = this.schemaId;
    this.objectTypeProperties.ChangeObjectsSchema = this.changeObjectsSchema;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        if (this.IsVirtualFolder)
        {
          this.idValue = (object) (!(this.nodeParent.Tag.GetType() == this.GetType()) ? (this.nodeParent.Tag as CustomFolder).GetServerObject(sessionKeeper.Session) as IDBObjectTypeCollection : sessionKeeper.Session.GetObjectTypeCollection(Convert.ToInt32((this.nodeParent.Tag as CustomFolder).Id), CoreConsts.FilterRecords)).Create(this.objectTypeProperties);
          (this.GetServerObject(sessionKeeper.Session) as IDBObjectType).Icon = ArraySrv.IconToArray(this.iconValue);
          int num = this.iconIndex4Category;
          if (Statics.IconSrv != null)
          {
            num = Statics.IconSrv.AddIcon(this.iconValue, 4, (int) this.idValue);
            if (num == 0)
              num = this.iconIndex4Category;
          }
          this.node.ImageIndex = num;
          this.node.SelectedImageIndex = num;
          this.objectTypeProperties.ObjectType = (int) this.idValue;
          this.identPropDescriptor.SetValue((object) this, this.idValue);
          DataHolders.ObjectTypesHolder.ClearHierarchy();
        }
        else
        {
          IDBObjectType serverObject = this.GetServerObject(sessionKeeper.Session) as IDBObjectType;
          this.oldOTP = serverObject.PropertiesStructure;
          serverObject.PropertiesStructure = this.objectTypeProperties;
          byte[] array = ArraySrv.IconToArray(this.iconValue);
          if (!ArraySrv.Compare(serverObject.Icon, array))
          {
            serverObject.Icon = array;
            int num = this.iconIndex4Category;
            if (Statics.IconSrv != null)
            {
              num = Statics.IconSrv.AddIcon(this.iconValue, 4, (int) this.idValue);
              if (num == 0)
                num = this.iconIndex4Category;
            }
            this.node.ImageIndex = num;
            this.node.SelectedImageIndex = num;
          }
        }
        this.guidPropDescriptor.SetReadOnly(true);
        DataHolders.ObjectTypesHolder.ClearAllObjectTypes();
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
        return false;
      }
      finally
      {
        DataHolders.ObjectTypesHolder.ClearInfo();
      }
    }
    INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    DBObjectTypesEventArgs e;
    if (this.IsNewType)
    {
      this.IsNewType = false;
      e = new DBObjectTypesEventArgs("ObjectTypesCreated", this.objectTypeProperties.ObjectType);
    }
    else
      e = new DBObjectTypesEventArgs("ObjectTypesChanged", this.objectTypeProperties.ObjectType);
    if (service != null && e != null)
      service.FireEvent((object) null, (NotificationEventArgs) e);
    return true;
  }

  private void AddToCBList(bool condition, List<object> list, bool b)
  {
    if (condition)
      list.Add((object) b);
    else
      list.Add((object) null);
  }

  public override bool SaveCallbackEnd(bool aVirtualFolder)
  {
    if (aVirtualFolder)
      return true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetObjectTypeCollection((int) this.idValue, false).Select("").Rows.Count == 0)
        return true;
      ObjectTypeProperties propertiesStructure1 = (this.GetServerObject(sessionKeeper.Session) as IDBObjectType).PropertiesStructure;
      Params4SubFoldersApplyForm foldersApplyForm = new Params4SubFoldersApplyForm();
      List<object> objectList = new List<object>();
      bool b = true;
      this.AddToCBList(this.oldOTP.DefaultRelation != propertiesStructure1.DefaultRelation, objectList, b);
      this.AddToCBList(this.oldOTP.AreaID != propertiesStructure1.AreaID, objectList, b);
      this.AddToCBList(this.oldOTP.CaptionAttribute != propertiesStructure1.CaptionAttribute, objectList, b);
      this.AddToCBList(this.oldOTP.AnyAttributes != propertiesStructure1.AnyAttributes, objectList, b);
      this.AddToCBList(this.oldOTP.LifetimeReserve != propertiesStructure1.LifetimeReserve, objectList, b);
      this.AddToCBList(this.oldOTP.SchemaID != propertiesStructure1.SchemaID, objectList, b);
      this.AddToCBList(this.oldOTP.Options != propertiesStructure1.Options, objectList, b);
      List<PropDescriptor> pdList = new List<PropDescriptor>((IEnumerable<PropDescriptor>) CategoryPropsHolder.GetPropDescriptors((PropDescriptorHolder) this, this.Category, this.idValue));
      foreach (ObjectTypeOptions option in Enum.GetValues(typeof (ObjectTypeOptions)))
      {
        if (option != ObjectTypeOptions.None)
        {
          int index = 0;
          while (index < pdList.Count)
          {
            if (ObjectTypeOptionsHelper.GetCaption(option) == pdList[index].DisplayName)
              pdList.RemoveAt(index);
            else
              ++index;
          }
        }
      }
      int index1 = 0;
      while (index1 < pdList.Count)
      {
        if (!pdList[index1].ChangedValueApplied)
          pdList.RemoveAt(index1);
        else
          ++index1;
      }
      bool flag1 = true;
      for (int index2 = 0; index2 < objectList.Count; ++index2)
      {
        if (objectList[index2] != null)
        {
          flag1 = false;
          break;
        }
      }
      if (flag1 && pdList.Count == 0)
        return true;
      bool objSchemaChangeFlag = false;
      if (foldersApplyForm.Execute(objectList, this.oldOTP.Options, propertiesStructure1.Options, out objSchemaChangeFlag, pdList) != DialogResult.Yes)
        return true;
      bool flag2 = false;
      for (int index3 = 0; index3 < objectList.Count; ++index3)
      {
        if (objectList[index3] != null)
          flag2 = flag2 || (bool) objectList[index3];
      }
      if (flag2)
      {
        ObjectTypeOptions objectTypeOptions1 = this.oldOTP.Options ^ propertiesStructure1.Options;
        ObjectTypeOptions objectTypeOptions2 = propertiesStructure1.Options & objectTypeOptions1;
        ObjectTypeOptions objectTypeOptions3 = this.oldOTP.Options & objectTypeOptions1;
        (sessionKeeper.Session as IClientSession).ClientCache.LockReload = true;
        try
        {
          IDBObjectTypeCollection objectTypeCollection = sessionKeeper.Session.GetObjectTypeCollection(Convert.ToInt32(this.Id), false);
          if (objectTypeCollection != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) objectTypeCollection.SelectRecursive("").Rows)
            {
              int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
              string str = string.Empty;
              try
              {
                IDBObjectType objectType = sessionKeeper.Session.GetObjectType(int32);
                if (objectType != null)
                {
                  ObjectTypeProperties propertiesStructure2 = objectType.PropertiesStructure;
                  str = propertiesStructure2.ObjectTypeName;
                  for (int index4 = 0; index4 < objectList.Count; ++index4)
                  {
                    if (objectList[index4] != null && (bool) objectList[index4])
                    {
                      switch (index4)
                      {
                        case 0:
                          propertiesStructure2.DefaultRelation = propertiesStructure1.DefaultRelation;
                          continue;
                        case 1:
                          propertiesStructure2.AreaID = propertiesStructure1.AreaID;
                          continue;
                        case 2:
                          propertiesStructure2.CaptionAttribute = propertiesStructure1.CaptionAttribute;
                          continue;
                        case 3:
                          propertiesStructure2.AnyAttributes = propertiesStructure1.AnyAttributes;
                          continue;
                        case 4:
                          propertiesStructure2.LifetimeReserve = propertiesStructure1.LifetimeReserve;
                          continue;
                        case 5:
                          propertiesStructure2.SchemaID = propertiesStructure1.SchemaID;
                          propertiesStructure2.ChangeObjectsSchema = objSchemaChangeFlag;
                          continue;
                        case 6:
                          propertiesStructure2.Options = (propertiesStructure2.Options | objectTypeOptions2) & ~objectTypeOptions3;
                          continue;
                        default:
                          continue;
                      }
                    }
                  }
                  objectType.PropertiesStructure = propertiesStructure2;
                }
              }
              catch (Exception ex)
              {
                string Message = string.Format(LocalizationHolder.rm.GetString("Client.Core_119"), str != string.Empty ? (object) $"\"{str}\"" : (object) ("id=" + int32.ToString()), (object) ex.Message);
                if (IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_82"), Message, MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.No)
                  break;
              }
            }
          }
        }
        finally
        {
          (sessionKeeper.Session as IClientSession).ClientCache.LockReload = false;
          (sessionKeeper.Session as IClientSession).ClientCache.ReloadCacheCategory(4, sessionKeeper.Session);
        }
      }
      if (pdList != null)
      {
        if (pdList.Count > 0)
        {
          foreach (ICategoryProps registeredCategoryProp in CategoryPropsHolder.GetRegisteredCategoryProps(this.Category))
          {
            if (registeredCategoryProp is ICategoryProps4ObjectType)
              ((ICategoryProps4ObjectType) registeredCategoryProp).ApplyValuesOnSubfolders((PropDescriptorHolder) this, this.Category, this.idValue, (PropertyDescriptor[]) pdList.ToArray());
          }
        }
      }
    }
    return true;
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    pdc.Add((PropertyDescriptor) new PropDescriptor(0, (object) this, LocalizationHolder.rm.GetString("Client.Core_33"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.ObjectType_Name, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(1, (object) this, LocalizationHolder.rm.GetString("Client.Core_123"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.ObjectType_ObjName, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(2, (object) this, LocalizationHolder.rm.GetString("Client.Core_35"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.ObjectType_Note, false, true, false));
    this.iconPropDescriptor = new PropDescriptor(3, (object) this, LocalizationHolder.rm.GetString("Client.Core_17"), (object) null, typeof (Icon), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.ObjectType_Icon, false, true, true);
    pdc.Add((PropertyDescriptor) this.iconPropDescriptor);
    pdc.Add((PropertyDescriptor) new PropDescriptor(4, (object) this, EnumTypeHelper.GetDescription(typeof (ObjectVersionModes)), (object) null, typeof (ObjectVersionModePropertyClass), (TypeConverter) new ObjectVersionModesConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_Versionable, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(5, (object) this, LocalizationHolder.rm.GetString("Client.Core_124"), (object) null, typeof (RelationTypePropertyClass), (TypeConverter) new RelationTypeConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_DefaultRel, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(6, (object) this, LocalizationHolder.rm.GetString("Client.Core_70"), (object) null, typeof (SubjectAreaPropertyClass), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.ObjectType_Area, false, true, false));
    this.guidPropDescriptor = new PropDescriptor(7, (object) this, LocalizationHolder.rm.GetString("Client.Core_39"), (object) null, typeof (Guid), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.ObjectType_GUID, false, true, false);
    pdc.Add((PropertyDescriptor) this.guidPropDescriptor);
    this.captionAttributePropDescriptor = new PropDescriptor(8, (object) this, LocalizationHolder.rm.GetString("Client.Core_125"), (object) null, typeof (AttributePropertyClass), (TypeConverter) new AttributeTypeConverter(true, new EventsHolder.GetListDelegate(this.GetList)), (object) null, string.Empty, PropDescriptions.ObjectType_Caption, false, true, false);
    pdc.Add((PropertyDescriptor) this.captionAttributePropDescriptor);
    this.anyAttributePropDescriptor = new PropDescriptor(9, (object) this, LocalizationHolder.rm.GetString("Client.Core_127"), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_AnyAttribute, false, true, false);
    pdc.Add((PropertyDescriptor) this.anyAttributePropDescriptor);
    pdc.Add((PropertyDescriptor) new PropDescriptor(10, (object) this, LocalizationHolder.rm.GetString("Client.Core_74"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.ObjectType_ShortName, false, true, false));
    this.identPropDescriptor = new PropDescriptor(11, (object) this, LocalizationHolder.rm.GetString("Client.Core_37"), (object) null, typeof (long), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.ObjectType_Ident, true, true, false);
    pdc.Add((PropertyDescriptor) this.identPropDescriptor);
    this.lifetimePropDescriptor = new PropDescriptor(12, (object) this, LocalizationHolder.rm.GetString("Client.Core_1172"), (object) null, typeof (int), (TypeConverter) new UnlimitedConverter(), (object) new UnlimitedEditor(), string.Empty, PropDescriptions.ObjectType_Lifetime, false, true, false);
    pdc.Add((PropertyDescriptor) this.lifetimePropDescriptor);
    this.optionCurrentProjectEnabledDescriptor = new PropDescriptor(13, (object) this, ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.CurrentProjectEnabled), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_CurrentProjectEnabled, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionCurrentProjectEnabledDescriptor);
    this.optionCheckParentAccessDescriptor = new PropDescriptor(14, (object) this, ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.CheckParentAccess), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_CheckParentAccess, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionCheckParentAccessDescriptor);
    this.optionLocalObjectTypeDescriptor = new PropDescriptor(15, (object) this, ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.LocalObjectType), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_LocalObjectType, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionLocalObjectTypeDescriptor);
    this.optionDisableManualCreateDescriptor = new PropDescriptor(16 /*0x10*/, (object) this, ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.DisableManualCreate), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_DisableManualCreate, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDisableManualCreateDescriptor);
    this.optionCreateSnapshotsDescriptor = new PropDescriptor(17, (object) this, ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.CreateSnapshots), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_CreateSnapShots, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionCreateSnapshotsDescriptor);
    this.optionAutoContentEnabledDescriptor = new PropDescriptor(18, (object) this, ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.AutoContextEnabled), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_AutoContentEnabled, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionAutoContentEnabledDescriptor);
    this.optionMandateAccessDescriptor = new PropDescriptor(19, (object) this, ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.MandateAccess), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_MandateAccess, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionMandateAccessDescriptor);
    this.optionAttributesIndexDescriptor = new PropDescriptor(20, (object) this, ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.AttributesIndex), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_AttributesIndex, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionAttributesIndexDescriptor);
    this.optionAutoCreateSnapshotsDescriptor = new PropDescriptor(21, (object) this, ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.AutoCreateSnapshots), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_AutoCreateSnaphots, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionAutoCreateSnapshotsDescriptor);
    this.optionDisablePrototypingDescriptor = new PropDescriptor(22, (object) this, ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.DisablePrototyping), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_DisablePrototyping, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDisablePrototypingDescriptor);
    this.optionNotificationsEnabledDescriptor = new PropDescriptor(23, (object) this, ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.NotificationsEnabled), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_NotificationsEnabled, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionNotificationsEnabledDescriptor);
    this.optionForumEnabledDescriptor = new PropDescriptor(24, (object) this, ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.ForumEnabled), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_ForumEnabled, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionForumEnabledDescriptor);
    this.optionExtendedAuditDescriptor = new PropDescriptor(25, (object) this, ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.ExtendedAudit), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_ExtendedAudit, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionExtendedAuditDescriptor);
    this.optionEnableWebEditDescriptor = new PropDescriptor(26, (object) this, ObjectTypeOptionsHelper.GetCaption(ObjectTypeOptions.EnableWebEdit), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.ObjectType_EnableWebEdit, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionEnableWebEditDescriptor);
  }

  public override void GetContextMenu(
    ContextMenuBarItem contextMenu,
    IEventsDispatcher iEventsDispatcher)
  {
    base.GetContextMenu(contextMenu, iEventsDispatcher);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if ((this.GetServerObject(sessionKeeper.Session) as IDBObjectType as IDBGuid).IsSystemGUID)
      {
        if (this.miSetSystemGuid != null)
          contextMenu.Items.Remove((ToolbarItemBase) this.miSetSystemGuid);
      }
    }
    if (this.Static)
    {
      contextMenu.Items.Remove((ToolbarItemBase) this.miAdd);
      contextMenu.Items.Remove((ToolbarItemBase) this.miDelete);
    }
    else
    {
      this.miCut = new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_129"), new EventHandler(this.CutObjectType));
      this.miCut.BeginGroup = true;
      this.miPaste = new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_99"), new EventHandler(this.PasteObjectType));
      INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
      if (service != null)
      {
        this.miCut.ImageIndex = service.ImageIndex("imgCut");
        this.miPaste.ImageIndex = service.ImageIndex("imgPaste");
      }
      this.miRecreateView = new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_130"), new EventHandler(this.RecreateView));
      this.miRecreateView.BeginGroup = true;
      contextMenu.Items.AddRange((ToolbarItemBase[]) new MenuButtonItem[3]
      {
        this.miCut,
        this.miPaste,
        this.miRecreateView
      });
    }
  }

  public override void SetContextMenuItemStatus(ContextMenuBarItem contextMenu)
  {
    base.SetContextMenuItemStatus(contextMenu);
    this.miOpenInNewWindow.Visible = true;
    if (this.Static)
      return;
    this.miRecreateView.Enabled = !this.InChange;
    this.miCut.Enabled = !this.InChange;
    this.miPaste.Enabled = !this.InChange && CoreConsts.ObjectTypeToPaste != -1;
  }

  private void CutObjectType(object sender, EventArgs e)
  {
    CoreConsts.ObjectTypeToPaste = (int) this.idValue;
  }

  private void PasteObjectType(object sender, EventArgs e)
  {
    ObjectTypesFolder.PasteObjectTypeToCustomFolder((CustomFolder) this);
  }

  private void RecreateView(object sender, EventArgs e)
  {
    DialogResult dialogResult = DialogResult.No;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((int) this.Id);
    if (childrenIdRecursive.Count > 1)
    {
      dialogResult = IMMessageBox.Show("Внимание", "Пересоздать представление данных также для вложенных типов объектов?", MessageBoxButtons.YesNoCancel, IMMessageBoxImage.Warning);
      if (dialogResult == DialogResult.Cancel)
        return;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int num = dialogResult == DialogResult.Yes ? childrenIdRecursive.Count : 1;
      for (int index = 0; index < num; ++index)
        sessionKeeper.Session.GetObjectType(childrenIdRecursive[index])?.RebuildView();
    }
  }

  public override bool DeleteCallbackBefore(ref long deleteMode)
  {
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((int) this.idValue);
    bool flag1 = childrenIdRecursive.Count > 1;
    if (!flag1)
    {
      if (IMMessageBox.Show(MessageDialogs.msgConfirmDelete, string.Format(MessageDialogs.msgReallyDelete0, (object) this.textValue), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
        return false;
    }
    else
    {
      if (IMMessageBox.Show(MessageDialogs.msgConfirmDelete, string.Format(MessageDialogs.msgReallyDeleteObjTypeWithChildren, (object) this.textValue), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
        return false;
      deleteMode = (long) Intermech.Consts.DeleteChildren;
    }
    bool flag2 = false;
    bool flag3 = false;
    int num1 = 0;
    bool flag4 = false;
    bool flag5 = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType1 = sessionKeeper.Session.GetObjectType((int) this.idValue);
      int objectsCount;
      int snapshotsCount;
      if (objectType1 != null)
      {
        objectType1.GetObjectsInfo(out objectsCount, out snapshotsCount);
        if (objectsCount > 0)
          flag3 = true;
        if (snapshotsCount > 0)
        {
          flag2 = true;
          num1 = snapshotsCount;
        }
      }
      if (flag1)
      {
        for (int index = 1; index < childrenIdRecursive.Count; ++index)
        {
          IDBObjectType objectType2 = sessionKeeper.Session.GetObjectType(childrenIdRecursive[index]);
          if (objectType2 != null)
          {
            objectType2.GetObjectsInfo(out objectsCount, out snapshotsCount);
            if (objectsCount > 0)
              flag5 = true;
            if (snapshotsCount > 0)
              flag4 = true;
          }
          if (flag5 & flag4)
            break;
        }
      }
    }
    if (flag3)
    {
      int num2 = (int) IMMessageBox.Show(MessageDialogs.msgWarning, LocalizationHolder.rm.GetString("Client.Core_ObjTypeHasObjects"), MessageBoxButtons.OK, IMMessageBoxImage.Warning);
      return false;
    }
    if (flag5)
    {
      int num3 = (int) IMMessageBox.Show(MessageDialogs.msgWarning, LocalizationHolder.rm.GetString("Client.Core_ChildObjTypesHasObjects"), MessageBoxButtons.OK, IMMessageBoxImage.Warning);
      return false;
    }
    string str = string.Empty;
    if (flag2)
      str = string.Format(LocalizationHolder.rm.GetString("Client.Core_ObjTypesHasSnapshots"), (object) num1.ToString()) + ".\n";
    if (flag4)
      str = $"{str}{LocalizationHolder.rm.GetString("Client.Core_ChildObjTypesHasSnapshots")}.\n";
    string Message = $"{str}{LocalizationHolder.rm.GetString("Client.Core_ConfirmSnapshotsDelete")}.";
    return !(flag2 | flag4) || IMMessageBox.Show(MessageDialogs.msgWarning, Message, MessageBoxButtons.OKCancel, IMMessageBoxImage.Question) == DialogResult.OK;
  }

  public override void ConstructPages(TabControl tabControl)
  {
    if (tabControl == null)
      return;
    List<TabPage> tabPageList = new List<TabPage>((IEnumerable<TabPage>) new TabPage[8]
    {
      (TabPage) TabPagesHolder.TabPages(this.instGuid).ListTabPage,
      (TabPage) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage,
      (TabPage) TabPagesHolder.TabPages(this.instGuid).Attr4ObjTypeTabPage,
      (TabPage) TabPagesHolder.TabPages(this.instGuid).ObjTypeApplTabPage,
      (TabPage) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage,
      (TabPage) TabPagesHolder.TabPages(this.instGuid).LCSchema4ObjTypeTabPage,
      (TabPage) TabPagesHolder.TabPages(this.instGuid).Forms4ObjectTypePage,
      (TabPage) TabPagesHolder.TabPages(this.instGuid).ActionsTabPage
    });
    if (this.Static)
      tabPageList.Insert(1, (TabPage) TabPagesHolder.TabPages(this.instGuid).ParentTypeTabPage);
    if (this.docExtensionsEnabled)
    {
      tabPageList.Insert(this.Static ? 3 : 2, (TabPage) TabPagesHolder.TabPages(this.instGuid).DocObjTypeTabPage);
      IDatabaseConfiguratorService service = ServicesManager.GetService(typeof (IDatabaseConfiguratorService)) as IDatabaseConfiguratorService;
      if (service.DocumentAdditionalViews != null)
      {
        for (int index = 0; index < service.DocumentAdditionalViews.Length; ++index)
        {
          IAdditionalTabPage page = service.DocumentAdditionalViews[index].GetPage(this.instGuid);
          if (page.Index >= 0)
            tabPageList.Insert(page.Index, (TabPage) page.TabPage);
          else
            tabPageList.Add((TabPage) page.TabPage);
        }
      }
    }
    TabControlProcessor.AssignTabPages(tabControl, (object[]) tabPageList.ToArray());
  }

  public override int ExportCategoryValue => 4;

  public override int ListCategoryValue => 4;

  private ArrayList GetList(object s, params object[] args)
  {
    ArrayList list = new ArrayList();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!this.IsVirtualFolder)
      {
        DataTable dataTable = (this.GetServerObject(sessionKeeper.Session) as IDBObjectType).Attributes.Select("");
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            list.Add((object) new AttributePropertyClass(Convert.ToInt32(row["F_ATTRIBUTE_ID"])));
        }
      }
      else if (this.nodeParent != null)
      {
        if (this.nodeParent.Tag != null)
        {
          if (this.nodeParent.Tag is ObjectTypeFolder)
          {
            DataTable dataTable = (((CustomFolder) this.nodeParent.Tag).GetServerObject(sessionKeeper.Session) as IDBObjectType).Attributes.Select("");
            if (dataTable != null)
            {
              foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
              {
                if (Convert.ToInt32(row["F_PUBLIC"]) == 2 || Convert.ToInt32(row["F_PUBLIC"]) == 1)
                  list.Add((object) new AttributePropertyClass(Convert.ToInt32(row["F_ATTRIBUTE_ID"])));
              }
            }
          }
        }
      }
    }
    list.Sort((IComparer) new AttributePropertyClassComparator());
    list.Insert(0, (object) new AttributePropertyClass(0));
    return list;
  }

  private ArrayList GetListInherit(object s, params object[] args)
  {
    return this.nodeParent == null || this.nodeParent.Tag.GetType() == typeof (ObjectTypesFolder) ? new ArrayList((ICollection) new object[1]
    {
      (object) new InheritModePropertyClass(InheritModes.Private)
    }) : new ArrayList((ICollection) new object[2]
    {
      (object) new InheritModePropertyClass(InheritModes.Private),
      (object) new InheritModePropertyClass(InheritModes.Inherited)
    });
  }

  protected int GetObjtypeDocumentId()
  {
    return MetaDataHelper.GetObjectType(new Guid("cad00070-306c-11d8-b4e9-00304f19f545")).ObjectTypeID;
  }

  /// <summary>
  /// инициализация флага редактирования дополнительных полей для типа объекта Документы
  /// </summary>
  protected void InitDocExtensionsFlag()
  {
    this.docExtensionsEnabled = false;
    if (this.objtypeDocumentId == -1)
      this.objtypeDocumentId = this.GetObjtypeDocumentId();
    for (TreeNode treeNode = this.node; treeNode != null && treeNode.Tag is ObjectTypeFolder; treeNode = ((CustomFolder) treeNode.Tag).NodeParent)
    {
      if ((int) ((DBPropDescriptorHolder) treeNode.Tag).Id == this.objtypeDocumentId)
      {
        this.docExtensionsEnabled = true;
        break;
      }
    }
  }

  public override int Category => 4;
}
