
// Type: Intermech.PropertyEditors.AttributeFolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DatabaseConfigurator;
using Intermech.DataFormats;
using Intermech.Expressions;
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

public class AttributeFolder : CustomFolder
{
  /// <summary>Является ли данный атрибут вновь созданным</summary>
  internal bool IsNewAttr;
  private PropDescriptor typePropDescriptor;
  private PropDescriptor defaultPropDescriptor;
  private PropDescriptor defaultAsIntPropDescriptor;
  private PropDescriptor defaultAsIntListPropDescriptor;
  private PropDescriptor defaultAsDoublePropDescriptor;
  private PropDescriptor defaultAsDoubleListPropDescriptor;
  private PropDescriptor defaultAsStringPropDescriptor;
  private PropDescriptor defaultAsStringListPropDescriptor;
  private PropDescriptor defaultAsBooleanPropDescriptor;
  private PropDescriptor defaultAsDateTimePropDescriptor;
  private PropDescriptor defaultAsDateTimeListPropDescriptor;
  private PropDescriptor defaultAsGuidPropDescriptor;
  private PropDescriptor defaultAsGuidListPropDescriptor;
  private PropDescriptor defaultAsObjectPropDescriptor;
  private PropDescriptor defaultAsObjectListPropDescriptor;
  private PropDescriptor defaultAsObjectIDPropDescriptor;
  private PropDescriptor defaultAsObjectIDListPropDescriptor;
  private PropDescriptor defaultAsMeasuredPropDescriptor;
  private PropDescriptor formulaPropDescriptor;
  private PropDescriptor guidPropDescriptor;
  private PropDescriptor possiblePropDescriptor;
  private PropDescriptor listPropDescriptor;
  private PropDescriptor optimizationPropDescriptor;
  private PropDescriptor computePropDescriptor;
  private PropDescriptor uniquePropDescriptor;
  private PropDescriptor sizePropDescriptor;
  private PropDescriptor sizeAsIntPropDescriptor;
  private PropDescriptor sizeAsObjTypePropDescriptor;
  private PropDescriptor sizeAsPhysValObjectPropDescriptor;
  private PropDescriptor identPropDescriptor;
  private PropDescriptor maskPropDescriptor;
  private PropDescriptor optionSaveInLogPropDescriptor;
  private PropDescriptor optionSavePrivateHistory;
  private PropDescriptor optionSaveCommonHistory;
  private PropDescriptor optionDisableNulls;
  private PropDescriptor optionGetDescriptionEvent;
  private PropDescriptor optionInternal;
  private PropDescriptor optionModifyInBase;
  private PropDescriptor optionDisableManualEdit;
  private PropDescriptor optionDontCopyPrototypeValue;
  private PropDescriptor optionDontCopyPrototypeValue4Article;
  private PropDescriptor optionEnableOwnerAccessCheck;
  private PropDescriptor optionAddToGlobalIndex;
  private PropDescriptor optionDisableSplitIndexValue;
  private PropDescriptor optionLocalImbaseAttribute;
  private PropDescriptor optionLocalImbaseFlagTableRecordRef;
  private PropDescriptor optionDontCopyVersionValue;
  private PropDescriptor optionCopyValues2ChildObject;
  private PropDescriptor masterPropDescriptor;
  private PropDescriptor sourcePropDescriptor;
  protected bool _BlockOnChange;
  protected bool warning4OptimizationNeeded;
  private AttributeTypeProperties attributeTypeProperties;
  private bool possibleValuesDataTableChanged;
  private DataTable possibleValuesDataTable;
  private DataTable possibleValuesDataTableOrig;
  private bool isContentOrig;
  /// <summary>id типа атрибута Количество</summary>
  private Lazy<int> сountAttributeTypeID = new Lazy<int>((Func<int>) (() => MetaDataHelper.GetAttributeTypeID(new Guid("cad00267-306c-11d8-b4e9-00304f19f545"))));

  public override bool DelEnabled => true;

  public override bool AddChildEnabled => false;

  public override bool NeedApply => true;

  public override bool NeedSave => true;

  public override bool NeedPageSave => true;

  public override bool CopyEnabled => true;

  public override bool ExcludeEnabled => true;

  public override bool CloneEnabled => true;

  public AttributeFolder(
    Guid aInstGuid,
    string aText,
    object aNodeParent,
    int aId,
    bool isNew,
    string aShort,
    string aAlias,
    string aNote,
    FieldTypes aType,
    object aDefault,
    MultiValueModes aMultiple,
    ComputeValueModes aComputed,
    long aSize,
    string aFormula,
    UniqueValueModes aUnique,
    int aLevel,
    string aLanguage,
    Guid aGuid,
    string aArea,
    DataTable aPossibleValues,
    OptimizationModes anOptimizationMode,
    bool aIsContent,
    AttributeOptions aAttributeOptions,
    string aMask)
    : base(aInstGuid, aText, aNodeParent, (object) aId, isNew)
  {
    this.IsNewAttr = isNew;
    this.attributeTypeProperties = new AttributeTypeProperties(aId, aText, aShort, aAlias, aNote, aType, aDefault, aMultiple, aComputed, aSize, aFormula, aUnique, aLevel, aLanguage, aArea, aGuid, anOptimizationMode, aIsContent, aAttributeOptions, aMask, 0, 0);
    this.possibleValuesDataTable = aPossibleValues;
    this.possibleValuesDataTableOrig = aPossibleValues?.Copy();
    this.isContentOrig = aIsContent;
    this.possibleValuesDataTableChanged = isNew && this.possibleValuesDataTable != null;
    if (Statics.IconSrv == null)
      return;
    int num = Statics.IconSrv.IndexOf(3, -1, (object) aType);
    this.node.ImageIndex = num;
    this.node.SelectedImageIndex = num;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetAttributeType(Convert.ToInt32(this.Id));
  }

  /// <summary>Патч редакторов, если они изменяются динамически</summary>
  private void PatchEditors()
  {
    AttributeEditor editor1 = (AttributeEditor) this.masterPropDescriptor.GetEditor(typeof (AttributeEditor));
    if (editor1 != null)
    {
      AttributeEditor attributeEditor = editor1;
      int[] numArray;
      if (!this.IsVirtualFolder)
        numArray = new int[1]{ (int) this.idValue };
      else
        numArray = (int[]) null;
      attributeEditor.ExcludeAttributeId = numArray;
    }
    AttributeEditor editor2 = (AttributeEditor) this.sourcePropDescriptor.GetEditor(typeof (AttributeEditor));
    if (editor2 != null)
    {
      editor2.FilterByTypes = new FieldTypes[1]
      {
        ((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType
      };
      AttributeEditor attributeEditor = editor2;
      int[] numArray;
      if (!this.IsVirtualFolder)
        numArray = new int[1]{ (int) this.idValue };
      else
        numArray = (int[]) null;
      attributeEditor.ExcludeAttributeId = numArray;
    }
    if (!this.maskPropDescriptor.IsReadOnly && ((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType == FieldTypes.ftDateTime)
      this.maskPropDescriptor.SetEditor((object) new DateTimeMaskEditor());
    else
      this.maskPropDescriptor.SetEditor((object) null);
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
      if (this.IsVirtualFolder)
      {
        this.idValue = (object) 0;
        this.textValue = this.node.Text;
        this.possibleValuesDataTableChanged = this.possibleValuesDataTable != null && this.possibleValuesDataTable.Rows.Count > 0;
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          this.possibleValuesDataTableChanged = false;
          IDBAttributeType serverObject = this.GetServerObject(sessionKeeper.Session) as IDBAttributeType;
          this.attributeTypeProperties = serverObject.PropertiesStructure;
          this.idValue = (object) this.attributeTypeProperties.AttributeID;
          this.textValue = this.attributeTypeProperties.Name;
          if (this.attributeTypeProperties.FieldType == FieldTypes.ftDateTime && this.attributeTypeProperties.DefaultValue != null && this.attributeTypeProperties.DefaultValue is string)
            this.attributeTypeProperties.DefaultValue = DateTimeCultureConverter.ConvertUniversalDateTimeStringToCurrentDateTime(this.attributeTypeProperties.DefaultValue.ToString());
          this.possibleValuesDataTable = this.StoreClientCacheTimestamp(serverObject.GetPossibleValues());
          this.possibleValuesDataTableOrig = this.possibleValuesDataTable != null ? this.possibleValuesDataTable.Copy() : (DataTable) null;
          this.isContentOrig = this.attributeTypeProperties.IsContent;
        }
      }
      this.PropDescriptorCollection[4].SetValue((object) this, (object) new FieldTypePropertyClass(this.attributeTypeProperties.FieldType));
      MultiValueModes multiValueMode = this.attributeTypeProperties.MultiValueMode;
      bool possibleValuesReadonly = this.GetValidator().PossibleValuesTable == null || multiValueMode == MultiValueModes.SingleValue || multiValueMode == MultiValueModes.MultiValues;
      this.AssignDefaultPropDescriptor(false, possibleValuesReadonly);
      this.AssignSizePropDescriptor();
      this.PropDescriptorCollection[0].SetValue((object) this, (object) this.attributeTypeProperties.Name);
      this.PropDescriptorCollection[1].SetValue((object) this, (object) this.attributeTypeProperties.ShortName);
      this.PropDescriptorCollection[2].SetValue((object) this, (object) this.attributeTypeProperties.Alias);
      this.PropDescriptorCollection[3].SetValue((object) this, (object) this.attributeTypeProperties.Note);
      this.SetDefaultPropDescriptorValue(this.attributeTypeProperties.DefaultValue, possibleValuesReadonly);
      this.PropDescriptorCollection[6].SetValue((object) this, (object) new MultiValueModePropertyClass(this.attributeTypeProperties.MultiValueMode));
      this.PropDescriptorCollection[7].SetValue((object) this, (object) new ComputeValueModePropertyClass(this.attributeTypeProperties.Computed));
      if (this.attributeTypeProperties.FieldType == FieldTypes.ftObjectLink || this.attributeTypeProperties.FieldType == FieldTypes.ftObjectLinkByID)
      {
        List<int> objTypeList = (List<int>) null;
        if (this.attributeTypeProperties.SizeType == 0L && this.attributeTypeProperties.MetadataExtensions[(object) "OBJ_LINKS_ID"] != null)
          objTypeList = new List<int>((IEnumerable<int>) (int[]) this.attributeTypeProperties.MetadataExtensions[(object) "OBJ_LINKS_ID"]);
        this.SetSizePropDescriptor(this.attributeTypeProperties.SizeType, objTypeList, (List<long>) null);
      }
      else if (this.attributeTypeProperties.FieldType == FieldTypes.ftMeasured)
      {
        List<long> objList = (List<long>) null;
        if (this.attributeTypeProperties.SizeType == 0L && this.attributeTypeProperties.MetadataExtensions[(object) "MU_PHYSICAL_ID"] != null)
          objList = new List<long>((IEnumerable<long>) (long[]) this.attributeTypeProperties.MetadataExtensions[(object) "MU_PHYSICAL_ID"]);
        this.SetSizePropDescriptor(this.attributeTypeProperties.SizeType, (List<int>) null, objList);
      }
      else
        this.SetSizePropDescriptor(this.attributeTypeProperties.SizeType);
      this.PropDescriptorCollection[9].SetValue((object) this, (object) new LevelPropertyClass(this.attributeTypeProperties.LevelID));
      this.PropDescriptorCollection[10].SetValue((object) this, (object) new UniqueValueModePropertyClass(this.attributeTypeProperties.Unique));
      this.PropDescriptorCollection[11].SetValue((object) this, (object) this.attributeTypeProperties.Formula);
      this.PropDescriptorCollection[12].SetValue((object) this, (object) new LanguagePropertyClass(this.attributeTypeProperties.LanguageID));
      this.PropDescriptorCollection[13].SetValue((object) this, (object) new SubjectAreaPropertyClass(this.attributeTypeProperties.AreaID));
      this.PropDescriptorCollection[14].SetValue((object) this, (object) this.attributeTypeProperties.AttributeGuid);
      this.PropDescriptorCollection[15].SetValue((object) this, (object) new PossibleValuesPropertyClass(this.possibleValuesDataTable, this.attributeTypeProperties.FieldType));
      this.PropDescriptorCollection[16 /*0x10*/].SetValue((object) this, this.idValue);
      this.PropDescriptorCollection[17].SetValue((object) this, (object) new OptimizationModePropertyClass(this.attributeTypeProperties.OptimizationMode));
      this.PropDescriptorCollection[18].SetValue((object) this, (object) new BoolPropertyClass(this.attributeTypeProperties.IsContent));
      this.PropDescriptorCollection[19].SetValue((object) this, (object) this.attributeTypeProperties.Mask);
      this.PropDescriptorCollection[20].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.SaveInLog) == AttributeOptions.SaveInLog));
      this.PropDescriptorCollection[21].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.SavePrivateHistory) == AttributeOptions.SavePrivateHistory));
      this.PropDescriptorCollection[22].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.SaveCommonHistory) == AttributeOptions.SaveCommonHistory));
      this.PropDescriptorCollection[23].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls));
      this.PropDescriptorCollection[24].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.GetDescriptionEvent) == AttributeOptions.GetDescriptionEvent));
      this.PropDescriptorCollection[25].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.Internal) == AttributeOptions.Internal));
      this.PropDescriptorCollection[26].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.ModifyInBase) == AttributeOptions.ModifyInBase));
      this.PropDescriptorCollection[27].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit));
      this.PropDescriptorCollection[28].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.DontCopyPrototypeValue) == AttributeOptions.DontCopyPrototypeValue));
      this.PropDescriptorCollection[29].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.DontCopyPrototypeAttributeValueForArticle) == AttributeOptions.DontCopyPrototypeAttributeValueForArticle));
      this.PropDescriptorCollection[30].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.EnableOwnerAccessCheck) == AttributeOptions.EnableOwnerAccessCheck));
      this.PropDescriptorCollection[31 /*0x1F*/].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.AddToGlobalIndex) == AttributeOptions.AddToGlobalIndex));
      this.PropDescriptorCollection[32 /*0x20*/].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.DisableSplitIndexValue) == AttributeOptions.DisableSplitIndexValue));
      this.PropDescriptorCollection[33].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.LocalImbaseAttribute) == AttributeOptions.LocalImbaseAttribute));
      if (this.attributeTypeProperties.FieldType == FieldTypes.ftString)
      {
        ((PropDescriptor) this.PropDescriptorCollection[34]).SetReadOnly(false);
        this.PropDescriptorCollection[34].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.ImbaseFlag_TableRecordRef));
      }
      else
      {
        ((PropDescriptor) this.PropDescriptorCollection[34]).SetReadOnly(true);
        this.PropDescriptorCollection[34].SetValue((object) this, (object) null);
      }
      this.PropDescriptorCollection[35].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.DontCopyVersionValue) == AttributeOptions.DontCopyVersionValue));
      this.PropDescriptorCollection[36].SetValue((object) this, (object) new BoolPropertyClass((this.attributeTypeProperties.Options & AttributeOptions.CopyValues2ChildObject) == AttributeOptions.CopyValues2ChildObject));
      this.masterPropDescriptor.SetValue((object) this, this.attributeTypeProperties.MasterAttributeID == 0 ? (object) (AttributePropertyClass) null : (object) new AttributePropertyClass(this.attributeTypeProperties.MasterAttributeID));
      this.sourcePropDescriptor.SetValue((object) this, this.attributeTypeProperties.SourceAttributeID == 0 ? (object) (AttributePropertyClass) null : (object) new AttributePropertyClass(this.attributeTypeProperties.SourceAttributeID));
      if (this.attributeTypeProperties.FieldType == FieldTypes.ftSystem)
      {
        this.masterPropDescriptor.SetEditor((object) null);
        this.sourcePropDescriptor.SetEditor((object) null);
      }
      this.PatchEditors();
    }
    finally
    {
      EventsHolder.BlockOnChange = false;
    }
    this.CheckPropertyStates(false);
    this.warning4OptimizationNeeded = false;
    return true;
  }

  public override void SetContextMenuItemStatus(ContextMenuBarItem contextMenu)
  {
    base.SetContextMenuItemStatus(contextMenu);
    if ((this.nodeParent == null ? 0 : (this.nodeParent.Tag is AttributeTypeAssignedGroupFolder ? 1 : 0)) == 0)
      return;
    for (int index = 0; index < contextMenu.Items.Count; ++index)
    {
      if (contextMenu.Items[index].Visible && contextMenu.Items[index].Enabled && contextMenu.Items[index] != this.miFind)
        contextMenu.Items[index].Enabled = false;
    }
  }

  public override IFolder Clone()
  {
    IFolder folder = (IFolder) null;
    if (IMMessageBox.Show(MessageDialogs.msgConfirmAction, LocalizationHolder.rm.GetString("Client.Core_CloneAttr"), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes)
    {
      IFolder attributesGroupFolder = this.IDatabaseConfiguratorControl.GetAllAttributesGroupFolder();
      int num;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        AttributeTypePropertiesValidator validator = ((attributesGroupFolder as CustomFolder).GetServerObject(sessionKeeper.Session) as IDBAttributesGroup).Attributes.GetValidator(this.attributeTypeProperties.FieldType);
        num = ((attributesGroupFolder as CustomFolder).GetServerObject(sessionKeeper.Session) as IDBAttributesGroup).Attributes.Create(new AttributeTypeProperties(this.attributeTypeProperties)
        {
          AttributeID = 0,
          Name = validator.Name,
          AttributeGuid = Guid.Empty,
          PossibleValues = this.possibleValuesDataTable,
          Alias = string.Empty
        });
      }
      attributesGroupFolder.Update();
      TreeNode node = attributesGroupFolder.Node;
      for (int index = 0; index < node.Nodes.Count; ++index)
      {
        if (node.Nodes[index].Tag is CustomFolder && Convert.ToInt32(((DBPropDescriptorHolder) node.Nodes[index].Tag).Id) == num)
        {
          folder = node.Nodes[index].Tag as IFolder;
          break;
        }
      }
    }
    return folder;
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
        string note = (string) this.PropDescriptorCollection[3].GetValue((object) this);
        Guid nextSystemGuid = customService.GenerateNextSystemGuid(3, this.textValue, note);
        IDBAttributeType serverObject = this.GetServerObject(sessionKeeper.Session) as IDBAttributeType;
        this.attributeTypeProperties.AttributeGuid = nextSystemGuid;
        AttributeTypeProperties attributeTypeProperties = this.attributeTypeProperties;
        serverObject.PropertiesStructure = attributeTypeProperties;
      }
      base.SetSystemGuid();
    }
  }

  public override void GetContextMenu(
    ContextMenuBarItem contextMenu,
    IEventsDispatcher iEventsDispatcher)
  {
    base.GetContextMenu(contextMenu, iEventsDispatcher);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(this.GetServerObject(sessionKeeper.Session) as IDBAttributeType as IDBGuid).IsSystemGUID || this.miSetSystemGuid == null)
        return;
      contextMenu.Items.Remove((ToolbarItemBase) this.miSetSystemGuid);
    }
  }

  public override bool SaveCallback()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!this.IsVirtualFolder && this.GetServerObject(sessionKeeper.Session) is IDBAttributeType serverObject1)
      {
        List<int> objTypeList = (List<int>) null;
        List<long> objList = (List<long>) null;
        FieldTypes fieldType = ((FieldTypePropertyClass) this.PropDescriptorCollection[4].GetValue((object) this)).FieldType;
        long sizePropDescriptor = this.GetSizePropDescriptor(out objTypeList, out objList);
        bool flag1 = false;
        bool flag2 = true;
        AttributeTypeProperties propertiesStructure;
        if ((serverObject1.AttributeType == FieldTypes.ftObjectLink || serverObject1.AttributeType == FieldTypes.ftObjectLinkByID) && serverObject1.AttributeType == fieldType)
        {
          flag1 = true;
          List<int> intList1 = new List<int>();
          if (serverObject1.SizeType <= 0L && serverObject1.PropertiesStructure.MetadataExtensions[(object) "OBJ_LINKS_ID"] != null)
          {
            List<int> intList2 = intList1;
            propertiesStructure = serverObject1.PropertiesStructure;
            int[] metadataExtension = (int[]) propertiesStructure.MetadataExtensions[(object) "OBJ_LINKS_ID"];
            intList2.AddRange((IEnumerable<int>) metadataExtension);
          }
          flag2 = intList1.Count == objTypeList.Count;
          if (flag2)
          {
            intList1.Sort();
            objTypeList.Sort();
            for (int index = 0; index < intList1.Count; ++index)
            {
              if (intList1[index] != objTypeList[index])
              {
                flag2 = false;
                break;
              }
            }
          }
        }
        bool flag3 = false;
        bool flag4 = true;
        if (serverObject1.AttributeType == FieldTypes.ftMeasured && serverObject1.AttributeType == fieldType)
        {
          flag3 = true;
          List<long> longList1 = new List<long>();
          if (serverObject1.SizeType <= 0L)
          {
            propertiesStructure = serverObject1.PropertiesStructure;
            if (propertiesStructure.MetadataExtensions[(object) "MU_PHYSICAL_ID"] != null)
            {
              List<long> longList2 = longList1;
              propertiesStructure = serverObject1.PropertiesStructure;
              long[] metadataExtension = (long[]) propertiesStructure.MetadataExtensions[(object) "MU_PHYSICAL_ID"];
              longList2.AddRange((IEnumerable<long>) metadataExtension);
            }
          }
          flag4 = longList1.Count == objList.Count;
          if (flag4)
          {
            longList1.Sort();
            objList.Sort();
            for (int index = 0; index < longList1.Count; ++index)
            {
              if (longList1[index] != objList[index])
              {
                flag4 = false;
                break;
              }
            }
          }
        }
        if ((serverObject1.AttributeType != fieldType || serverObject1.SizeType != sizePropDescriptor || flag1 && !flag2 || flag3 && !flag4) && !AttributeCacheHelper.IsSafeConvert(serverObject1, fieldType, sizePropDescriptor) && IMMessageBox.Show(MessageDialogs.msgConfirmSave, LocalizationHolder.rm.GetString("Client.Core_71"), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes || serverObject1.AttributeType != fieldType && IMMessageBox.Show(MessageDialogs.msgWarning, "Изменение типа данных атрибута может быть длительным процессом, поэтому его рекомендуется выполнять в нерабочее время.\n\nПродолжить сейчас?", MessageBoxButtons.YesNo, IMMessageBoxImage.Warning) != DialogResult.Yes)
          return false;
      }
      MultiValueModes multiValueMode = ((MultiValueModePropertyClass) this.PropDescriptorCollection[6].GetValue((object) this)).MultiValueMode;
      bool possibleValuesReadonly = this.GetValidator().PossibleValuesTable == null || multiValueMode == MultiValueModes.SingleValue || multiValueMode == MultiValueModes.MultiValues;
      bool flag5 = false;
      if (possibleValuesReadonly && !this.IsVirtualFolder && this.GetServerObject(sessionKeeper.Session) is IDBAttributeType serverObject2)
      {
        DataTable possibleValues = serverObject2.GetPossibleValues();
        if (possibleValues != null && possibleValues.Rows.Count > 0)
        {
          if (IMMessageBox.Show(MessageDialogs.msgWarning, "Указанное изменение режима работы со списковыми параметрами приведет к очистке списка допустимых значений.\n\nПродолжить сейчас?", MessageBoxButtons.YesNo, IMMessageBoxImage.Warning) != DialogResult.Yes)
            return false;
          flag5 = true;
        }
      }
      this.attributeTypeProperties.FieldType = ((FieldTypePropertyClass) this.PropDescriptorCollection[4].GetValue((object) this)).FieldType;
      this.textValue = (string) this.PropDescriptorCollection[0].GetValue((object) this);
      this.attributeTypeProperties.Name = this.textValue;
      this.attributeTypeProperties.ShortName = (string) this.PropDescriptorCollection[1].GetValue((object) this);
      this.attributeTypeProperties.Alias = (string) this.PropDescriptorCollection[2].GetValue((object) this);
      this.attributeTypeProperties.Note = (string) this.PropDescriptorCollection[3].GetValue((object) this);
      this.attributeTypeProperties.DefaultValue = this.GetDefaultPropDescriptorValue(possibleValuesReadonly);
      this.attributeTypeProperties.MultiValueMode = ((MultiValueModePropertyClass) this.PropDescriptorCollection[6].GetValue((object) this)).MultiValueMode;
      this.attributeTypeProperties.Computed = ((ComputeValueModePropertyClass) this.PropDescriptorCollection[7].GetValue((object) this)).ComputeValueMode;
      List<int> objTypeList1 = (List<int>) null;
      List<long> objList1 = (List<long>) null;
      this.attributeTypeProperties.SizeType = this.GetSizePropDescriptor(out objTypeList1, out objList1);
      if ((this.attributeTypeProperties.FieldType == FieldTypes.ftObjectLink || this.attributeTypeProperties.FieldType == FieldTypes.ftObjectLinkByID) && objTypeList1 != null)
        this.attributeTypeProperties.MetadataExtensions[(object) "OBJ_LINKS_ID"] = (object) objTypeList1.ToArray();
      if (this.attributeTypeProperties.FieldType == FieldTypes.ftMeasured && objList1 != null)
        this.attributeTypeProperties.MetadataExtensions[(object) "MU_PHYSICAL_ID"] = (object) objList1.ToArray();
      this.attributeTypeProperties.Formula = (string) this.PropDescriptorCollection[11].GetValue((object) this);
      this.attributeTypeProperties.Unique = ((UniqueValueModePropertyClass) this.PropDescriptorCollection[10].GetValue((object) this)).UniqueValueMode;
      this.attributeTypeProperties.LevelID = ((LevelPropertyClass) this.PropDescriptorCollection[9].GetValue((object) this)).Level;
      this.attributeTypeProperties.LanguageID = ((LanguagePropertyClass) this.PropDescriptorCollection[12].GetValue((object) this)).Language;
      this.attributeTypeProperties.AreaID = ((SubjectAreaPropertyClass) this.PropDescriptorCollection[13].GetValue((object) this)).Areas;
      this.attributeTypeProperties.AttributeGuid = (Guid) this.PropDescriptorCollection[14].GetValue((object) this);
      if (this.warning4OptimizationNeeded && IMMessageBox.Show(MessageDialogs.msgWarning, LocalizationHolder.rm.GetString("Client.Core_72"), MessageBoxButtons.YesNo, IMMessageBoxImage.Warning) == DialogResult.Yes)
        this.PropDescriptorCollection[17].SetValue((object) this, (object) new OptimizationModePropertyClass(OptimizationModes.Write));
      this.warning4OptimizationNeeded = false;
      this.attributeTypeProperties.OptimizationMode = ((OptimizationModePropertyClass) this.PropDescriptorCollection[17].GetValue((object) this)).OptimizationMode;
      this.attributeTypeProperties.IsContent = ((BoolPropertyClass) this.PropDescriptorCollection[18].GetValue((object) this)).Boolean;
      this.attributeTypeProperties.Mask = (string) this.PropDescriptorCollection[19].GetValue((object) this);
      int num1 = ((BoolPropertyClass) this.PropDescriptorCollection[20].GetValue((object) this)).Boolean ? 1 : 0;
      bool boolean1 = ((BoolPropertyClass) this.PropDescriptorCollection[21].GetValue((object) this)).Boolean;
      bool boolean2 = ((BoolPropertyClass) this.PropDescriptorCollection[22].GetValue((object) this)).Boolean;
      bool boolean3 = ((BoolPropertyClass) this.PropDescriptorCollection[23].GetValue((object) this)).Boolean;
      bool boolean4 = ((BoolPropertyClass) this.PropDescriptorCollection[24].GetValue((object) this)).Boolean;
      bool boolean5 = ((BoolPropertyClass) this.PropDescriptorCollection[25].GetValue((object) this)).Boolean;
      bool boolean6 = ((BoolPropertyClass) this.PropDescriptorCollection[26].GetValue((object) this)).Boolean;
      bool boolean7 = ((BoolPropertyClass) this.PropDescriptorCollection[27].GetValue((object) this)).Boolean;
      bool boolean8 = ((BoolPropertyClass) this.PropDescriptorCollection[28].GetValue((object) this)).Boolean;
      bool boolean9 = ((BoolPropertyClass) this.PropDescriptorCollection[30].GetValue((object) this)).Boolean;
      bool boolean10 = ((BoolPropertyClass) this.PropDescriptorCollection[31 /*0x1F*/].GetValue((object) this)).Boolean;
      bool boolean11 = ((BoolPropertyClass) this.PropDescriptorCollection[32 /*0x20*/].GetValue((object) this)).Boolean;
      bool boolean12 = ((BoolPropertyClass) this.PropDescriptorCollection[33].GetValue((object) this)).Boolean;
      bool flag6 = false;
      if (this.attributeTypeProperties.FieldType == FieldTypes.ftString)
        flag6 = ((BoolPropertyClass) this.PropDescriptorCollection[34].GetValue((object) this)).Boolean;
      bool boolean13 = ((BoolPropertyClass) this.PropDescriptorCollection[35].GetValue((object) this)).Boolean;
      bool boolean14 = ((BoolPropertyClass) this.PropDescriptorCollection[29].GetValue((object) this)).Boolean;
      bool boolean15 = ((BoolPropertyClass) this.PropDescriptorCollection[36].GetValue((object) this)).Boolean;
      this.attributeTypeProperties.Options = AttributeOptions.None;
      if (num1 != 0)
        this.attributeTypeProperties.Options |= AttributeOptions.SaveInLog;
      if (boolean1)
        this.attributeTypeProperties.Options |= AttributeOptions.SavePrivateHistory;
      if (boolean2)
        this.attributeTypeProperties.Options |= AttributeOptions.SaveCommonHistory;
      if (boolean3)
        this.attributeTypeProperties.Options |= AttributeOptions.DisableNulls;
      if (boolean4)
        this.attributeTypeProperties.Options |= AttributeOptions.GetDescriptionEvent;
      if (boolean5)
        this.attributeTypeProperties.Options |= AttributeOptions.Internal;
      if (boolean6)
        this.attributeTypeProperties.Options |= AttributeOptions.ModifyInBase;
      if (boolean7)
        this.attributeTypeProperties.Options |= AttributeOptions.DisableManualEdit;
      if (boolean8)
        this.attributeTypeProperties.Options |= AttributeOptions.DontCopyPrototypeValue;
      if (boolean9)
        this.attributeTypeProperties.Options |= AttributeOptions.EnableOwnerAccessCheck;
      if (boolean10)
        this.attributeTypeProperties.Options |= AttributeOptions.AddToGlobalIndex;
      if (boolean11)
        this.attributeTypeProperties.Options |= AttributeOptions.DisableSplitIndexValue;
      if (boolean12)
        this.attributeTypeProperties.Options |= AttributeOptions.LocalImbaseAttribute;
      if (flag6)
        this.attributeTypeProperties.Options |= AttributeOptions.ImbaseFlag_TableRecordRef;
      if (boolean13)
        this.attributeTypeProperties.Options |= AttributeOptions.DontCopyVersionValue;
      if (boolean14)
        this.attributeTypeProperties.Options |= AttributeOptions.DontCopyPrototypeAttributeValueForArticle;
      if (boolean15)
        this.attributeTypeProperties.Options |= AttributeOptions.CopyValues2ChildObject;
      this.attributeTypeProperties.MasterAttributeID = this.masterPropDescriptor.GetValue((object) this) == null ? 0 : ((AttributePropertyClass) this.masterPropDescriptor.GetValue((object) this)).Attribute;
      this.attributeTypeProperties.SourceAttributeID = this.sourcePropDescriptor.GetValue((object) this) == null ? 0 : ((AttributePropertyClass) this.sourcePropDescriptor.GetValue((object) this)).Attribute;
      DataTable dataTable = (DataTable) null;
      if (flag5)
      {
        dataTable = this.possibleValuesDataTable;
        this.possibleValuesDataTable = (DataTable) null;
      }
      else
        this.possibleValuesDataTable = ((PossibleValuesPropertyClass) this.PropDescriptorCollection[15].GetValue((object) this)).PossibleValues;
      this.attributeTypeProperties.PossibleValues = this.possibleValuesDataTable;
      bool flag7 = false;
      if (!this.IsVirtualFolder && PossibleValuesPropertyClass.ValuesModifiedOrDeleted(this.possibleValuesDataTableOrig, this.possibleValuesDataTable))
      {
        flag7 = this.isContentOrig || this.attributeTypeProperties.IsContent;
        if (!flag7)
          flag7 = this.IsContentForAnyObjectType();
      }
      if (flag7 && IMMessageBox.Show(MessageDialogs.msgConfirmSave, "Выполняемые изменения атрибута могут привести к устареванию подписей на некоторых документах, у которых присутствует данный атрибут", MessageBoxButtons.OKCancel, IMMessageBoxImage.Warning) != DialogResult.OK)
      {
        if (flag5)
          this.possibleValuesDataTable = dataTable;
        return false;
      }
      if (flag5)
        this.possibleValuesDataTableChanged = true;
      try
      {
        IDBAttributeType serverObject3;
        if (this.IsVirtualFolder)
        {
          this.idValue = (object) ((this.nodeParent.Tag as CustomFolder).GetServerObject(sessionKeeper.Session) as IDBAttributesGroup).Attributes.Create(this.attributeTypeProperties);
          this.attributeTypeProperties.AttributeID = (int) this.idValue;
          this.identPropDescriptor.SetValue((object) this, this.idValue);
          serverObject3 = this.GetServerObject(sessionKeeper.Session) as IDBAttributeType;
          this.PatchEditors();
        }
        else
          serverObject3 = this.GetServerObject(sessionKeeper.Session) as IDBAttributeType;
        this.guidPropDescriptor.SetReadOnly(!ClientConsts.InDeveloperMode);
        try
        {
          if (!this.IsVirtualFolder)
          {
            serverObject3.PropertiesStructure = this.attributeTypeProperties;
            serverObject3 = this.GetServerObject(sessionKeeper.Session) as IDBAttributeType;
          }
          if (Statics.IconSrv != null)
          {
            int num2 = Statics.IconSrv.IndexOf(3, -1, (object) this.attributeTypeProperties.FieldType);
            this.node.ImageIndex = num2;
            this.node.SelectedImageIndex = num2;
          }
          if (this.possibleValuesDataTableChanged)
          {
            this.possibleValuesDataTableChanged = false;
            this.possibleValuesDataTable = this.StoreClientCacheTimestamp(serverObject3.GetPossibleValues());
            this.possibleValuesDataTableOrig = this.possibleValuesDataTable != null ? this.possibleValuesDataTable.Copy() : (DataTable) null;
            this.isContentOrig = this.attributeTypeProperties.IsContent;
            EventsHolder.BlockOnChange = true;
            try
            {
              this.PropDescriptorCollection[15].SetValue((object) this, (object) new PossibleValuesPropertyClass(this.possibleValuesDataTable, this.attributeTypeProperties.FieldType));
            }
            finally
            {
              EventsHolder.BlockOnChange = false;
            }
          }
        }
        catch
        {
          if (this.IsVirtualFolder)
            this.isVirtualFolder = false;
          throw;
        }
      }
      catch
      {
        throw;
      }
      finally
      {
        DataHolders.AttributesHolder.ClearInfo();
        INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
        DBAttributesEventArgs e;
        if (this.IsNewAttr)
        {
          this.IsNewAttr = false;
          e = new DBAttributesEventArgs("AttributeCreated", this.attributeTypeProperties.AttributeID);
        }
        else
          e = new DBAttributesEventArgs("AttributeChanged", this.attributeTypeProperties.AttributeID);
        if (service != null && e != null)
          service.FireEvent((object) null, (NotificationEventArgs) e);
      }
    }
    return true;
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    pdc.Add((PropertyDescriptor) new PropDescriptor(0, (object) this, LocalizationHolder.rm.GetString("Client.Core_33"), (object) null, typeof (string), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Identification), PropDescriptions.Attribute_Name, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(1, (object) this, LocalizationHolder.rm.GetString("Client.Core_74"), (object) null, typeof (string), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Identification), PropDescriptions.Attribute_Short, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(2, (object) this, LocalizationHolder.rm.GetString("Client.Core_76"), (object) null, typeof (string), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Identification), PropDescriptions.Attribute_Alias, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(3, (object) this, LocalizationHolder.rm.GetString("Client.Core_35"), (object) null, typeof (string), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Identification), PropDescriptions.Attribute_Note, false, true, false));
    this.typePropDescriptor = (PropDescriptor) new FieldTypePropDescriptor(4, (object) this, EnumTypeHelper.GetDescription(typeof (FieldTypes)), (object) null, typeof (FieldTypePropertyClass), (TypeConverter) new FieldTypesConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_Type, false, true, false);
    pdc.Add((PropertyDescriptor) this.typePropDescriptor);
    string name1 = LocalizationHolder.rm.GetString("Client.Core_43");
    string attributeDefault = PropDescriptions.Attribute_Default;
    string caption1 = VisualCategoriesHelper.GetCaption(VisualCategories.InputControl);
    this.defaultAsIntPropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (long), (TypeConverter) new Int64CustomConverter(), (object) null, caption1, attributeDefault, false, true, false);
    this.defaultAsIntListPropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (Int64PropertyClass), (TypeConverter) new IntTypeConverter(new EventsHolder.GetListDelegate(this.GetListByType)), (object) new IntDropDownListEditor(new EventsHolder.GetListDelegate(this.GetListByType)), caption1, attributeDefault, false, true, true);
    this.defaultAsDoublePropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (double), (TypeConverter) new DoubleCustomConverter(), (object) null, caption1, attributeDefault, false, true, false);
    this.defaultAsDoubleListPropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (DoublePropertyClass), (TypeConverter) new DoubleTypeConverter(new EventsHolder.GetListDelegate(this.GetListByType)), (object) new DoubleDropDownListEditor(new EventsHolder.GetListDelegate(this.GetListByType)), caption1, attributeDefault, false, true, true);
    this.defaultAsStringPropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (string), (TypeConverter) null, (object) null, caption1, attributeDefault, false, true, false);
    this.defaultAsStringListPropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (StringPropertyClass), (TypeConverter) new StringTypeConverter(new EventsHolder.GetListDelegate(this.GetListByType)), (object) new StringDropDownListEditor(new EventsHolder.GetListDelegate(this.GetListByType)), caption1, attributeDefault, false, true, true);
    this.defaultAsBooleanPropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (bool), (TypeConverter) new YesNoConverter(), (object) null, caption1, attributeDefault, false, true, false);
    this.defaultAsDateTimePropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (DateTime), (TypeConverter) new DateTimeNowConverter(), (object) new DateTimeNowEditor(), caption1, attributeDefault, false, true, false);
    this.defaultAsDateTimeListPropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (DateTimePropertyClass), (TypeConverter) new DateTimeTypeConverter(new EventsHolder.GetListDelegate(this.GetListByType)), (object) new DateTimeDropDownListEditor(new EventsHolder.GetListDelegate(this.GetListByType)), caption1, attributeDefault, false, true, true);
    this.defaultAsGuidPropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (Guid), (TypeConverter) new GuidCustomConverter(), (object) null, caption1, attributeDefault, false, true, false);
    this.defaultAsGuidListPropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (GuidPropertyClass), (TypeConverter) new GuidTypeConverter(new EventsHolder.GetListDelegate(this.GetListByType)), (object) new GuidDropDownListEditor(new EventsHolder.GetListDelegate(this.GetListByType)), caption1, attributeDefault, false, true, true);
    this.defaultAsObjectPropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (ObjectPropertyClass), (TypeConverter) null, (object) new ObjectEditor(new EventsHolder.GetListDelegate(this.GetObjTypeList))
    {
      CurrentUserCustomProcessing = true
    }, caption1, attributeDefault, false, true, true);
    this.defaultAsObjectListPropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (ObjectPropertyClass), (TypeConverter) null, (object) new ObjectDropDownSelector(new EventsHolder.GetListDelegate(this.GetListByType)), caption1, attributeDefault, false, true, true);
    this.defaultAsObjectIDPropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (ObjectPropertyClass), (TypeConverter) null, (object) new ObjectEditor(new EventsHolder.GetListDelegate(this.GetObjTypeList), false)
    {
      CurrentUserCustomProcessing = true
    }, caption1, attributeDefault, false, true, true);
    this.defaultAsObjectIDListPropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (ObjectPropertyClass), (TypeConverter) null, (object) new ObjectDropDownSelector(new EventsHolder.GetListDelegate(this.GetListByType), false), caption1, attributeDefault, false, true, true);
    this.defaultAsMeasuredPropDescriptor = new PropDescriptor(5, (object) this, name1, (object) null, typeof (string), (TypeConverter) null, (object) new MeasureEditor(new EventsHolder.GetListDelegate(this.GetMeasureDescriptorList), new GetDefaultMeasureIDDelegate(this.GetDefaultMeasureID)), caption1, attributeDefault, false, true, false);
    this.defaultPropDescriptor = this.defaultAsStringPropDescriptor;
    pdc.Add((PropertyDescriptor) this.defaultPropDescriptor);
    this.listPropDescriptor = new PropDescriptor(6, (object) this, EnumTypeHelper.GetDescription(typeof (MultiValueModes)), (object) null, typeof (MultiValueModePropertyClass), (TypeConverter) new MultiValueModesConverter(new EventsHolder.GetListDelegate(this.GetList)), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_List, false, true, false);
    pdc.Add((PropertyDescriptor) this.listPropDescriptor);
    this.computePropDescriptor = new PropDescriptor(7, (object) this, EnumTypeHelper.GetDescription(typeof (ComputeValueModes)), (object) null, typeof (ComputeValueModePropertyClass), (TypeConverter) new ComputeValueModesConverter(new EventsHolder.GetListDelegate(this.GetList)), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_ComputeValueModes, false, true, false);
    pdc.Add((PropertyDescriptor) this.computePropDescriptor);
    this.sizeAsIntPropDescriptor = new PropDescriptor(8, (object) this, LocalizationHolder.rm.GetString("Client.Core_79"), (object) null, typeof (long), (TypeConverter) new IntMaxTypeConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_Size, false, true, false);
    this.sizeAsObjTypePropDescriptor = new PropDescriptor(8, (object) this, LocalizationHolder.rm.GetString("Client.Core_80"), (object) null, typeof (ObjectTypeMultiPropertyClass), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_ObjType, false, true, false);
    this.sizeAsPhysValObjectPropDescriptor = new PropDescriptor(8, (object) this, LocalizationHolder.rm.GetString("Client.Core_1169"), (object) null, typeof (ObjectListPropertyClass), (TypeConverter) null, (object) new ObjectListEditor(new EventsHolder.GetListDelegate(this.GetPhysicalValuesList)), VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_PhysValue + (this.idValue.Equals((object) this.сountAttributeTypeID.Value) ? "\n" + PropDescriptions.Attribute_PhysValue4CountAttr : ""), false, true, false);
    this.sizePropDescriptor = this.sizeAsIntPropDescriptor;
    pdc.Add((PropertyDescriptor) this.sizePropDescriptor);
    pdc.Add((PropertyDescriptor) new PropDescriptor(9, (object) this, LocalizationHolder.rm.GetString("Client.Core_42"), (object) null, typeof (LevelPropertyClass), (TypeConverter) new LevelConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Filtration), PropDescriptions.Attribute_Level, false, true, false));
    this.uniquePropDescriptor = new PropDescriptor(10, (object) this, EnumTypeHelper.GetDescription(typeof (UniqueValueModes)), (object) null, typeof (UniqueValueModePropertyClass), (TypeConverter) new UniqueValueModesConverter(new EventsHolder.GetListDelegate(this.GetList)), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_Unique, false, true, false);
    pdc.Add((PropertyDescriptor) this.uniquePropDescriptor);
    this.formulaPropDescriptor = new PropDescriptor(11, (object) this, LocalizationHolder.rm.GetString("Client.Core_41"), (object) null, typeof (string), TypeDescriptor.GetConverter(typeof (string)), (object) new AttributeFormulaUITypeEditor(new EventsHolder.GetAttributeTypeDelegate(this.GetAttributeTypeCallback)), VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_Formula, false, true, false);
    pdc.Add((PropertyDescriptor) this.formulaPropDescriptor);
    pdc.Add((PropertyDescriptor) new PropDescriptor(12, (object) this, LocalizationHolder.rm.GetString("Client.Core_69"), (object) null, typeof (LanguagePropertyClass), (TypeConverter) new LanguageConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Filtration), PropDescriptions.Attribute_Language, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(13, (object) this, LocalizationHolder.rm.GetString("Client.Core_70"), (object) null, typeof (SubjectAreaPropertyClass), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Filtration), PropDescriptions.Attribute_Area, false, true, false));
    this.guidPropDescriptor = new PropDescriptor(14, (object) this, LocalizationHolder.rm.GetString("Client.Core_39"), (object) null, typeof (Guid), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Identification), PropDescriptions.Attribute_GUID, false, true, false);
    pdc.Add((PropertyDescriptor) this.guidPropDescriptor);
    this.possiblePropDescriptor = new PropDescriptor(15, (object) this, LocalizationHolder.rm.GetString("Client.Core_81"), (object) null, typeof (PossibleValuesPropertyClass), (TypeConverter) null, (object) new PossibleValuesEditor(new EventsHolder.GetListDelegate(this.GetAcceptedObjType)), VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_Possible, false, true, false);
    pdc.Add((PropertyDescriptor) this.possiblePropDescriptor);
    this.possiblePropDescriptor.SetResetValue(true);
    this.identPropDescriptor = new PropDescriptor(16 /*0x10*/, (object) this, LocalizationHolder.rm.GetString("Client.Core_37"), (object) null, typeof (long), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Identification), PropDescriptions.Attribute_Ident, true, true, false);
    pdc.Add((PropertyDescriptor) this.identPropDescriptor);
    this.optimizationPropDescriptor = new PropDescriptor(17, (object) this, EnumTypeHelper.GetDescription(typeof (OptimizationModes)), (object) null, typeof (OptimizationModePropertyClass), (TypeConverter) new OptimizationModesConverter(new EventsHolder.GetListDelegate(this.GetList)), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_Optimization, false, true, false);
    pdc.Add((PropertyDescriptor) this.optimizationPropDescriptor);
    pdc.Add((PropertyDescriptor) new PropDescriptor(18, (object) this, LocalizationHolder.rm.GetString("Client.Core_44"), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_IsContent, false, true, false));
    this.maskPropDescriptor = new PropDescriptor(19, (object) this, LocalizationHolder.rm.GetString("Client.Core_45"), (object) null, typeof (string), (TypeConverter) null, (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_Mask, false, true, false);
    pdc.Add((PropertyDescriptor) this.maskPropDescriptor);
    this.optionSaveInLogPropDescriptor = new PropDescriptor(20, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.SaveInLog), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.History), PropDescriptions.Attribute_SaveInLogPropDescriptor, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionSaveInLogPropDescriptor);
    this.optionSavePrivateHistory = new PropDescriptor(21, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.SavePrivateHistory), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.History), PropDescriptions.Attribute_SavePrivateHistory, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionSavePrivateHistory);
    this.optionSaveCommonHistory = new PropDescriptor(22, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.SaveCommonHistory), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.History), PropDescriptions.Attribute_SaveCommonHistory, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionSaveCommonHistory);
    this.optionDisableNulls = new PropDescriptor(23, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DisableNulls), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), AttributeOptionsHelper.GetCaption(AttributeOptions.DisableNulls), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDisableNulls);
    this.optionGetDescriptionEvent = new PropDescriptor(24, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.GetDescriptionEvent), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_GetDescriptionEvent, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionGetDescriptionEvent);
    this.optionInternal = new PropDescriptor(25, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.Internal), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.Filtration), PropDescriptions.Attribute_Internal, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionInternal);
    this.optionModifyInBase = new PropDescriptor(26, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.ModifyInBase), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_ModifyInBase, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionModifyInBase);
    this.optionDisableManualEdit = new PropDescriptor(27, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DisableManualEdit), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_DisableManualEdit, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDisableManualEdit);
    this.optionDontCopyPrototypeValue = new PropDescriptor(28, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DontCopyPrototypeValue), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_DontCopyPrototypeValue, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDontCopyPrototypeValue);
    bool browsable = Attr4ObjTypeClass.IsAttributeDefinedForType(this.attributeTypeProperties.AttributeID, new Guid("cad00132-306c-11d8-b4e9-00304f19f545"));
    this.optionDontCopyPrototypeValue4Article = new PropDescriptor(29, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DontCopyPrototypeAttributeValueForArticle), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_DontCopyPrototypeValue4Article, false, browsable, false);
    pdc.Add((PropertyDescriptor) this.optionDontCopyPrototypeValue4Article);
    this.optionEnableOwnerAccessCheck = new PropDescriptor(30, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.EnableOwnerAccessCheck), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InputControl), PropDescriptions.Attribute_EnableOwnerAccessCheck, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionEnableOwnerAccessCheck);
    this.optionAddToGlobalIndex = new PropDescriptor(31 /*0x1F*/, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.AddToGlobalIndex), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_AddToGlobalIndex, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionAddToGlobalIndex);
    this.optionDisableSplitIndexValue = new PropDescriptor(32 /*0x20*/, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DisableSplitIndexValue), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_DisableSplitIndexValue, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDisableSplitIndexValue);
    this.optionLocalImbaseAttribute = new PropDescriptor(33, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.LocalImbaseAttribute), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_LocalImbaseAttribute, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionLocalImbaseAttribute);
    this.optionLocalImbaseFlagTableRecordRef = new PropDescriptor(34, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.ImbaseFlag_TableRecordRef), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_LocalImbaseFlagTableRecordRef, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionLocalImbaseFlagTableRecordRef);
    this.optionDontCopyVersionValue = new PropDescriptor(35, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.DontCopyVersionValue), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_DontCopyVersionValue, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDontCopyVersionValue);
    this.optionCopyValues2ChildObject = new PropDescriptor(36, (object) this, AttributeOptionsHelper.GetCaption(AttributeOptions.CopyValues2ChildObject), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.InformationType), PropDescriptions.Attribute_CopyValues2ChildObject, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionCopyValues2ChildObject);
    string name2 = LocalizationHolder.rm.GetString("Client.Core_1163");
    System.Type type1 = typeof (AttributePropertyClass);
    FieldTypes[] aFilterByTypes = new FieldTypes[1]
    {
      FieldTypes.ftObjectLink
    };
    int[] aExcludeAttrId1;
    if (!this.IsVirtualFolder)
      aExcludeAttrId1 = new int[1]{ (int) this.idValue };
    else
      aExcludeAttrId1 = (int[]) null;
    AttributeEditor editor1 = new AttributeEditor(false, aFilterByTypes, aExcludeAttrId1);
    string caption2 = VisualCategoriesHelper.GetCaption(VisualCategories.DataSources);
    string attributeMaster = PropDescriptions.Attribute_Master;
    this.masterPropDescriptor = new PropDescriptor(37, (object) this, name2, (object) null, type1, (TypeConverter) null, (object) editor1, caption2, attributeMaster, true, true, true);
    pdc.Add((PropertyDescriptor) this.masterPropDescriptor);
    string name3 = LocalizationHolder.rm.GetString("Client.Core_1164");
    System.Type type2 = typeof (AttributePropertyClass);
    int[] aExcludeAttrId2;
    if (!this.IsVirtualFolder)
      aExcludeAttrId2 = new int[1]{ (int) this.idValue };
    else
      aExcludeAttrId2 = (int[]) null;
    AttributeEditor editor2 = new AttributeEditor(false, (FieldTypes[]) null, aExcludeAttrId2);
    string caption3 = VisualCategoriesHelper.GetCaption(VisualCategories.DataSources);
    string attributeSource = PropDescriptions.Attribute_Source;
    this.sourcePropDescriptor = new PropDescriptor(38, (object) this, name3, (object) null, type2, (TypeConverter) null, (object) editor2, caption3, attributeSource, true, true, true);
    pdc.Add((PropertyDescriptor) this.sourcePropDescriptor);
  }

  private long GetDefaultMeasureID(object sender, params object[] args)
  {
    long defaultMeasureId = -1;
    long sizePropDescriptor = this.GetSizePropDescriptor();
    if (sizePropDescriptor != -1L)
      defaultMeasureId = MeasureHelper.GetBaseMeasureID(sizePropDescriptor);
    return defaultMeasureId;
  }

  private AttributeTypePropertiesValidator GetValidator()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      FieldTypes fieldType = ((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType;
      return ((this.nodeParent.Tag as CustomFolder).GetServerObject(sessionKeeper.Session) as IDBAttributesGroup).Attributes.GetValidator(fieldType);
    }
  }

  public override void ChangeEventProcessing(object s, EventArgs e)
  {
    if (this._BlockOnChange)
      return;
    this._BlockOnChange = true;
    try
    {
      if (!(e is PropertyValueChangedEventArgs))
        return;
      PropertyValueChangedEventArgs changedEventArgs = (PropertyValueChangedEventArgs) e;
      if (((PropDescriptor) changedEventArgs.ChangedItem.PropertyDescriptor).PropID == 15)
      {
        this.possibleValuesDataTableChanged = true;
        PossibleValuesPropertyClass valuesPropertyClass = (PossibleValuesPropertyClass) this.PropDescriptorCollection[15].GetValue((object) this);
        if (valuesPropertyClass != null)
        {
          this.possibleValuesDataTable = valuesPropertyClass.PossibleValues;
        }
        else
        {
          if (this.possibleValuesDataTable.Rows.Count > 0 && IMMessageBox.Show(MessageDialogs.msgQuery, MessageDialogs.msgReallyDeleteValue, MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes)
            this.possibleValuesDataTable.Clear();
          this.PropDescriptorCollection[15].SetValue((object) this, (object) new PossibleValuesPropertyClass(this.possibleValuesDataTable, ((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType));
          (this.GetPropertyForm() as IConfigPage).PropertyGrid?.Refresh();
        }
        if (this.PropDescriptorCollection.IndexOf((PropertyDescriptor) this.sizeAsIntPropDescriptor) != -1 && !this.sizeAsIntPropDescriptor.IsReadOnly && this.possibleValuesDataTable != null)
          this.SetSizePropDescriptor(this.GetMaxPossibleValueLength(this.possibleValuesDataTable, this.GetSizePropDescriptor()));
      }
      if (((PropDescriptor) changedEventArgs.ChangedItem.PropertyDescriptor).PropID == 4)
      {
        bool flag = false;
        if (!this.isVirtualFolder)
        {
          if (!this.possiblePropDescriptor.IsReadOnly)
          {
            PossibleValuesPropertyClass valuesPropertyClass = (PossibleValuesPropertyClass) this.possiblePropDescriptor.GetValue((object) this);
            if (valuesPropertyClass != null && valuesPropertyClass.PossibleValues.Rows.Count > 0)
              flag = true;
          }
          if (!flag)
          {
            object propDescriptorValue = this.GetDefaultPropDescriptorValue(((FieldTypePropertyClass) changedEventArgs.OldValue).FieldType, this.possiblePropDescriptor.IsReadOnly);
            if (propDescriptorValue != null && propDescriptorValue.ToString() != string.Empty)
              flag = true;
          }
          if (flag)
          {
            int num = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1261"), LocalizationHolder.rm.GetString("Client.Core_1260"), MessageBoxButtons.YesNoCancel, IMMessageBoxImage.Warning);
            if (num != 6)
              flag = false;
            if (num == 2)
            {
              changedEventArgs.ChangedItem.PropertyDescriptor.SetValue((object) this, changedEventArgs.OldValue);
              return;
            }
          }
        }
        if (!this.isVirtualFolder & flag)
        {
          FieldTypePropertyClass typePropertyClass = (FieldTypePropertyClass) changedEventArgs.ChangedItem.PropertyDescriptor.GetValue((object) this);
          FieldTypePropertyClass oldValue = (FieldTypePropertyClass) changedEventArgs.OldValue;
          this.CheckPropertyStates(true);
          this.possiblePropDescriptor.SetValue((object) this, (object) new PossibleValuesPropertyClass((DataTable) null, typePropertyClass.FieldType));
          this.SetDefaultPropDescriptorValue((object) null, this.possiblePropDescriptor.IsReadOnly);
          IDatabaseConfiguratorControl configuratorControl = this.IDatabaseConfiguratorControl;
          try
          {
            if (configuratorControl == null)
              throw new Exception(LocalizationHolder.rm.GetString("Client.Core_1262"));
            configuratorControl.Apply((object) this);
          }
          catch (Exception ex)
          {
            configuratorControl?.Cancel((object) this);
            ExceptionHelper.ExceptionService.ShowException(ex);
            return;
          }
        }
        else
        {
          this.CheckPropertyStates(true);
          (this.GetPropertyForm() as IConfigPage).PropertyGrid?.Refresh();
        }
      }
      if (((PropDescriptor) changedEventArgs.ChangedItem.PropertyDescriptor).PropID == 37 && this.masterPropDescriptor.GetValue((object) this) == null)
      {
        this.sourcePropDescriptor.ResetValue((object) this);
        (this.GetPropertyForm() as IConfigPage).PropertyGrid?.Refresh();
      }
      if (((PropDescriptor) changedEventArgs.ChangedItem.PropertyDescriptor).PropID == 17)
      {
        if (changedEventArgs.OldValue != null && ((OptimizationModePropertyClass) changedEventArgs.OldValue).OptimizationMode == OptimizationModes.Write)
          this.warning4OptimizationNeeded = true;
        if (((OptimizationModePropertyClass) changedEventArgs.ChangedItem.PropertyDescriptor.GetValue((object) this)).OptimizationMode == OptimizationModes.Write)
          this.warning4OptimizationNeeded = false;
      }
      if (((PropDescriptor) changedEventArgs.ChangedItem.PropertyDescriptor).PropID == 8)
      {
        if ((((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType == FieldTypes.ftObjectLink || ((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType == FieldTypes.ftObjectLinkByID) && ((ObjectTypeMultiPropertyClass) changedEventArgs.ChangedItem.PropertyDescriptor.GetValue((object) this)).ObjectTypeList[0] != -1)
          this.defaultPropDescriptor.ResetValue((object) this);
        if (((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType == FieldTypes.ftMeasured && ((ObjectListPropertyClass) changedEventArgs.ChangedItem.PropertyDescriptor.GetValue((object) this)).ObjectIDList[0] != -1L)
        {
          if (((ObjectListPropertyClass) changedEventArgs.OldValue).ObjectIDList[0] == -1L)
          {
            object obj = this.defaultPropDescriptor.GetValue((object) this);
            if (obj != null)
            {
              MeasuredValue measuredValue = (MeasuredValue) null;
              try
              {
                measuredValue = MeasureHelper.ConvertToMeasuredValue(obj.ToString());
              }
              catch
              {
              }
              if (measuredValue != null)
              {
                MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(measuredValue.MeasureID);
                if (descriptor != null)
                {
                  bool flag = false;
                  List<long> objectIdList = ((ObjectListPropertyClass) changedEventArgs.ChangedItem.PropertyDescriptor.GetValue((object) this)).ObjectIDList;
                  for (int index = 0; index < objectIdList.Count; ++index)
                  {
                    long num = objectIdList[index];
                    if (descriptor.PhysicalQuantityID == num)
                    {
                      flag = true;
                      break;
                    }
                  }
                  if (!flag)
                    this.defaultPropDescriptor.ResetValue((object) this);
                }
                else
                  this.defaultPropDescriptor.ResetValue((object) this);
              }
              else
                this.defaultPropDescriptor.ResetValue((object) this);
            }
          }
          else
            this.defaultPropDescriptor.ResetValue((object) this);
        }
        (this.GetPropertyForm() as IConfigPage).PropertyGrid?.Refresh();
      }
      if (((PropDescriptor) changedEventArgs.ChangedItem.PropertyDescriptor).PropID == 6)
      {
        MultiValueModes multiValueMode = ((MultiValueModePropertyClass) this.listPropDescriptor.GetValue((object) this)).MultiValueMode;
        this.possiblePropDescriptor.SetReadOnly(this.GetValidator().PossibleValuesTable == null || multiValueMode == MultiValueModes.SingleValue || multiValueMode == MultiValueModes.MultiValues);
        this.possiblePropDescriptor.SetResetValue(!this.possiblePropDescriptor.IsReadOnly);
        this.AssignDefaultPropDescriptor(true, this.possiblePropDescriptor.IsReadOnly);
        (this.GetPropertyForm() as IConfigPage).PropertyGrid?.Refresh();
      }
      if (((PropDescriptor) changedEventArgs.ChangedItem.PropertyDescriptor).PropID == 7)
      {
        this.CheckFormulaReadonly();
        (this.GetPropertyForm() as IConfigPage).PropertyGrid?.Refresh();
      }
      if (((PropDescriptor) changedEventArgs.ChangedItem.PropertyDescriptor).PropID == 31 /*0x1F*/)
      {
        BoolPropertyClass boolPropertyClass = (BoolPropertyClass) this.optionAddToGlobalIndex.GetValue((object) this);
        if (boolPropertyClass != null && !boolPropertyClass.Boolean)
        {
          this.optionDisableSplitIndexValue.SetValue((object) this, (object) new BoolPropertyClass(false));
          this.optionDisableSplitIndexValue.SetReadOnly(true);
        }
        else
          this.optionDisableSplitIndexValue.SetReadOnly(false);
      }
      base.ChangeEventProcessing(s, e);
    }
    finally
    {
      this._BlockOnChange = false;
    }
  }

  private void CheckPropertyStates(bool fillByDefault)
  {
    AttributeTypePropertiesValidator validator = this.GetValidator();
    for (int index = 0; index < this.PropDescriptorCollection.Count; ++index)
      ((PropDescriptor) this.PropDescriptorCollection[index]).SetReadOnly(false);
    this.formulaPropDescriptor.SetReadOnly(validator.Formula == null);
    if (fillByDefault)
      this.formulaPropDescriptor.SetValue((object) this, validator.Formula);
    if (fillByDefault)
      this.AssignSizePropDescriptor();
    this.sizePropDescriptor.SetReadOnly(validator.SizeType == null || validator.SizeType.Length == 0);
    if (fillByDefault)
    {
      if (this.sizePropDescriptor.IsReadOnly)
        this.sizePropDescriptor.SetValue((object) this, (object) null);
      else
        this.SetSizePropDescriptor(validator.SizeType[0]);
    }
    TypeConverter.StandardValuesCollection standardValues1 = this.listPropDescriptor.Converter.GetStandardValues((ITypeDescriptorContext) null);
    bool flag1 = false;
    if (standardValues1.Count == 0)
    {
      this.listPropDescriptor.SetValue((object) this, (object) null);
    }
    else
    {
      MultiValueModes multiValueMode = ((MultiValueModePropertyClass) this.listPropDescriptor.GetValue((object) this)).MultiValueMode;
      for (int index = 0; index < standardValues1.Count; ++index)
      {
        if (((MultiValueModePropertyClass) standardValues1[index]).MultiValueMode == multiValueMode)
        {
          flag1 = true;
          break;
        }
      }
      if (!flag1)
        this.listPropDescriptor.SetValue((object) this, (object) new MultiValueModePropertyClass(((MultiValueModePropertyClass) standardValues1[0]).MultiValueMode));
    }
    MultiValueModes multiValueMode1 = ((MultiValueModePropertyClass) this.listPropDescriptor.GetValue((object) this)).MultiValueMode;
    this.possiblePropDescriptor.SetReadOnly(validator.PossibleValuesTable == null || multiValueMode1 == MultiValueModes.SingleValue || multiValueMode1 == MultiValueModes.MultiValues);
    this.possiblePropDescriptor.SetResetValue(!this.possiblePropDescriptor.IsReadOnly);
    if (fillByDefault)
    {
      this.possiblePropDescriptor.SetValue((object) this, (object) new PossibleValuesPropertyClass(validator.PossibleValuesTable, ((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType));
      this.possibleValuesDataTable = this.StoreClientCacheTimestamp(validator.PossibleValuesTable);
      this.possibleValuesDataTableChanged = true;
    }
    if (fillByDefault)
      this.AssignDefaultPropDescriptor(false, this.possiblePropDescriptor.IsReadOnly);
    this.defaultPropDescriptor.SetReadOnly(validator.DefaultValue == null);
    if (fillByDefault)
    {
      if (this.defaultPropDescriptor.IsReadOnly || validator.DefaultValue == null)
        this.SetDefaultPropDescriptorValue((object) null, this.possiblePropDescriptor.IsReadOnly);
      else
        this.SetDefaultPropDescriptorValue(validator.DefaultValue, this.possiblePropDescriptor.IsReadOnly);
    }
    TypeConverter.StandardValuesCollection standardValues2 = this.computePropDescriptor.Converter.GetStandardValues((ITypeDescriptorContext) null);
    bool flag2 = false;
    if (standardValues2.Count == 0)
    {
      this.computePropDescriptor.SetValue((object) this, (object) null);
    }
    else
    {
      ComputeValueModes computeValueMode = ((ComputeValueModePropertyClass) this.computePropDescriptor.GetValue((object) this)).ComputeValueMode;
      for (int index = 0; index < standardValues2.Count; ++index)
      {
        if (((ComputeValueModePropertyClass) standardValues2[index]).ComputeValueMode == computeValueMode)
        {
          flag2 = true;
          break;
        }
      }
      if (!flag2)
        this.computePropDescriptor.SetValue((object) this, (object) new ComputeValueModePropertyClass(((ComputeValueModePropertyClass) standardValues2[0]).ComputeValueMode));
    }
    TypeConverter.StandardValuesCollection standardValues3 = this.uniquePropDescriptor.Converter.GetStandardValues((ITypeDescriptorContext) null);
    bool flag3 = false;
    if (standardValues3.Count == 0)
    {
      this.uniquePropDescriptor.SetValue((object) this, (object) null);
    }
    else
    {
      UniqueValueModes uniqueValueMode = ((UniqueValueModePropertyClass) this.uniquePropDescriptor.GetValue((object) this)).UniqueValueMode;
      for (int index = 0; index < standardValues3.Count; ++index)
      {
        if (((UniqueValueModePropertyClass) standardValues3[index]).UniqueValueMode == uniqueValueMode)
        {
          flag3 = true;
          break;
        }
      }
      if (!flag3)
        this.uniquePropDescriptor.SetValue((object) this, (object) new UniqueValueModePropertyClass(((UniqueValueModePropertyClass) standardValues3[0]).UniqueValueMode));
    }
    this.maskPropDescriptor.SetReadOnly(validator.Mask == null);
    if (fillByDefault)
      this.maskPropDescriptor.SetValue((object) this, (object) validator.Mask);
    if (fillByDefault)
    {
      this.optionSaveInLogPropDescriptor.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.SaveInLog) == AttributeOptions.SaveInLog));
      this.optionSavePrivateHistory.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.SavePrivateHistory) == AttributeOptions.SavePrivateHistory));
      this.optionSaveCommonHistory.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.SaveCommonHistory) == AttributeOptions.SaveCommonHistory));
      this.optionDisableNulls.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls));
      this.optionGetDescriptionEvent.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.GetDescriptionEvent) == AttributeOptions.GetDescriptionEvent));
      this.optionInternal.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.Internal) == AttributeOptions.Internal));
      this.optionModifyInBase.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.ModifyInBase) == AttributeOptions.ModifyInBase));
      this.optionDisableManualEdit.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit));
      this.optionDontCopyPrototypeValue.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.DontCopyPrototypeValue) == AttributeOptions.DontCopyPrototypeValue));
      this.optionDontCopyPrototypeValue4Article.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.DontCopyPrototypeAttributeValueForArticle) == AttributeOptions.DontCopyPrototypeAttributeValueForArticle));
      this.optionEnableOwnerAccessCheck.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.EnableOwnerAccessCheck) == AttributeOptions.EnableOwnerAccessCheck));
      this.optionAddToGlobalIndex.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.AddToGlobalIndex) == AttributeOptions.AddToGlobalIndex));
      this.optionDisableSplitIndexValue.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.DisableSplitIndexValue) == AttributeOptions.DisableSplitIndexValue));
      this.optionLocalImbaseAttribute.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.LocalImbaseAttribute) == AttributeOptions.LocalImbaseAttribute));
      if (((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType == FieldTypes.ftString)
        this.optionLocalImbaseFlagTableRecordRef.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.ImbaseFlag_TableRecordRef) == AttributeOptions.ImbaseFlag_TableRecordRef));
      else
        this.optionLocalImbaseFlagTableRecordRef.SetValue((object) this, (object) null);
      this.optionDontCopyVersionValue.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.DontCopyVersionValue) == AttributeOptions.DontCopyVersionValue));
      this.optionCopyValues2ChildObject.SetValue((object) this, (object) new BoolPropertyClass((validator.Options & AttributeOptions.CopyValues2ChildObject) == AttributeOptions.CopyValues2ChildObject));
    }
    this.optionLocalImbaseFlagTableRecordRef.SetReadOnly(((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType != FieldTypes.ftString);
    if (!((BoolPropertyClass) this.optionAddToGlobalIndex.GetValue((object) this)).Boolean)
      this.optionDisableSplitIndexValue.SetReadOnly(true);
    else
      this.optionDisableSplitIndexValue.SetReadOnly(false);
    if (fillByDefault)
      this.PatchEditors();
    this.guidPropDescriptor.SetReadOnly(!this.IsVirtualFolder && !ClientConsts.InDeveloperMode);
    this.identPropDescriptor.SetReadOnly(true);
    this.CheckFormulaReadonly();
    if ((int) this.Id >= 0)
      return;
    for (int index = 0; index < this.PropDescriptorCollection.Count; ++index)
      ((PropDescriptor) this.PropDescriptorCollection[index]).SetReadOnly(true);
  }

  private void CheckFormulaReadonly()
  {
    ComputeValueModePropertyClass modePropertyClass = (ComputeValueModePropertyClass) this.computePropDescriptor.GetValue((object) this);
    if (modePropertyClass == null)
      return;
    bool aReadOnly = modePropertyClass.ComputeValueMode == ComputeValueModes.NotComputableValue;
    this.formulaPropDescriptor.SetReadOnly(aReadOnly);
    if (aReadOnly)
      this.formulaPropDescriptor.SetEditor((object) null);
    else
      this.formulaPropDescriptor.SetEditor((object) new AttributeFormulaUITypeEditor(new EventsHolder.GetAttributeTypeDelegate(this.GetAttributeTypeCallback)));
  }

  private FieldTypes GetAttributeTypeCallback(object s, params object[] args)
  {
    return ((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType;
  }

  private long GetMaxPossibleValueLength(DataTable pValues, long defaultLength)
  {
    if (pValues == null)
      return defaultLength;
    long possibleValueLength = defaultLength;
    string columnName = string.Empty;
    for (int index = 0; index < pValues.Columns.Count; ++index)
    {
      if (pValues.Columns[index].ColumnName != "F_INLIST_ID")
      {
        columnName = pValues.Columns[index].ColumnName;
        break;
      }
    }
    foreach (DataRow row in (InternalDataCollectionBase) pValues.Rows)
    {
      int length = row[columnName].ToString().Length;
      if ((long) length > possibleValueLength)
        possibleValueLength = (long) length;
    }
    return possibleValueLength;
  }

  private void AssignSizePropDescriptor()
  {
    switch (((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType)
    {
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftObjectLinkByID:
        if (this.sizePropDescriptor == this.sizeAsObjTypePropDescriptor)
          break;
        this._pdc = PropDescriptorHolder.RemovePDCItem(this.PropDescriptorCollection, 8);
        this.sizePropDescriptor = this.sizeAsObjTypePropDescriptor;
        this.PropDescriptorCollection.Insert(8, (PropertyDescriptor) this.sizePropDescriptor);
        break;
      case FieldTypes.ftMeasured:
        if (this.sizePropDescriptor == this.sizeAsPhysValObjectPropDescriptor)
          break;
        this._pdc = PropDescriptorHolder.RemovePDCItem(this.PropDescriptorCollection, 8);
        this.sizePropDescriptor = this.sizeAsPhysValObjectPropDescriptor;
        this.PropDescriptorCollection.Insert(8, (PropertyDescriptor) this.sizePropDescriptor);
        break;
      default:
        if (this.sizePropDescriptor == this.sizeAsIntPropDescriptor)
          break;
        this._pdc = PropDescriptorHolder.RemovePDCItem(this.PropDescriptorCollection, 8);
        this.sizePropDescriptor = this.sizeAsIntPropDescriptor;
        this.PropDescriptorCollection.Insert(8, (PropertyDescriptor) this.sizePropDescriptor);
        break;
    }
  }

  private long GetSizePropDescriptor()
  {
    return this.GetSizePropDescriptor(out List<int> _, out List<long> _);
  }

  private long GetSizePropDescriptor(out List<int> objTypeList, out List<long> objList)
  {
    objTypeList = (List<int>) null;
    objList = (List<long>) null;
    if (this.sizePropDescriptor.GetValue((object) this) == null)
      return 0;
    long sizePropDescriptor = 0;
    if (this.sizePropDescriptor == this.sizeAsIntPropDescriptor)
      sizePropDescriptor = Convert.ToInt64(this.sizePropDescriptor.GetValue((object) this));
    else if (this.sizePropDescriptor == this.sizeAsObjTypePropDescriptor)
    {
      ObjectTypeMultiPropertyClass multiPropertyClass = (ObjectTypeMultiPropertyClass) this.sizePropDescriptor.GetValue((object) this);
      if (multiPropertyClass.ObjectTypePropertyClassList.Count == 1)
      {
        sizePropDescriptor = (long) multiPropertyClass.ObjectTypePropertyClassList[0].ObjectType;
        objTypeList = new List<int>();
      }
      else
      {
        sizePropDescriptor = 0L;
        objTypeList = new List<int>((IEnumerable<int>) multiPropertyClass.ObjectTypeList.ToArray());
      }
    }
    else if (this.sizePropDescriptor == this.sizeAsPhysValObjectPropDescriptor)
    {
      ObjectListPropertyClass listPropertyClass = (ObjectListPropertyClass) this.sizePropDescriptor.GetValue((object) this);
      if (listPropertyClass.ObjectPropertyClassList.Count == 1)
      {
        sizePropDescriptor = listPropertyClass.ObjectPropertyClassList[0].ObjectID;
        objList = new List<long>();
      }
      else
      {
        sizePropDescriptor = 0L;
        objList = new List<long>((IEnumerable<long>) listPropertyClass.ObjectIDList.ToArray());
      }
    }
    return sizePropDescriptor;
  }

  private void SetSizePropDescriptor(long spdValue)
  {
    this.SetSizePropDescriptor(spdValue, (List<int>) null, (List<long>) null);
  }

  private void SetSizePropDescriptor(long spdValue, List<int> objTypeList, List<long> objList)
  {
    if (this.sizePropDescriptor == this.sizeAsIntPropDescriptor)
      this.sizePropDescriptor.SetValue((object) this, (object) spdValue);
    else if (this.sizePropDescriptor == this.sizeAsObjTypePropDescriptor)
    {
      int int32 = Convert.ToInt32(spdValue);
      if (objTypeList == null || objTypeList.Count == 0)
        objTypeList = new List<int>((IEnumerable<int>) new int[1]
        {
          int32
        });
      this.sizePropDescriptor.SetValue((object) this, (object) new ObjectTypeMultiPropertyClass(objTypeList));
    }
    else
    {
      if (this.sizePropDescriptor != this.sizeAsPhysValObjectPropDescriptor)
        return;
      if (objList == null || objList.Count == 0)
        objList = new List<long>((IEnumerable<long>) new long[1]
        {
          spdValue
        });
      this.sizePropDescriptor.SetValue((object) this, (object) new ObjectListPropertyClass(objList));
    }
  }

  private void AssignDefaultPropDescriptor(bool withSaveValues, bool possibleValuesReadonly)
  {
    object obj = (object) null;
    if (withSaveValues)
      obj = this.defaultPropDescriptor.GetValue((object) this);
    FieldTypes fieldType = ((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType;
    this._pdc = PropDescriptorHolder.RemovePDCItem(this.PropDescriptorCollection, 5);
    try
    {
      switch (fieldType)
      {
        case FieldTypes.ftInteger:
          if (possibleValuesReadonly)
          {
            this.defaultPropDescriptor = this.defaultAsIntPropDescriptor;
            break;
          }
          this.defaultPropDescriptor = this.defaultAsIntListPropDescriptor;
          obj = (object) null;
          break;
        case FieldTypes.ftDouble:
          if (possibleValuesReadonly)
          {
            this.defaultPropDescriptor = this.defaultAsDoublePropDescriptor;
            break;
          }
          this.defaultPropDescriptor = this.defaultAsDoubleListPropDescriptor;
          obj = (object) null;
          break;
        case FieldTypes.ftDateTime:
          if (possibleValuesReadonly)
          {
            this.defaultPropDescriptor = this.defaultAsDateTimePropDescriptor;
            break;
          }
          this.defaultPropDescriptor = this.defaultAsDateTimeListPropDescriptor;
          obj = (object) null;
          break;
        case FieldTypes.ftObjectLink:
          if (possibleValuesReadonly)
          {
            this.defaultPropDescriptor = this.defaultAsObjectPropDescriptor;
            break;
          }
          this.defaultPropDescriptor = this.defaultAsObjectListPropDescriptor;
          obj = (object) null;
          break;
        case FieldTypes.ftBoolean:
          this.defaultPropDescriptor = this.defaultAsBooleanPropDescriptor;
          break;
        case FieldTypes.ftMeasured:
          this.defaultPropDescriptor = this.defaultAsMeasuredPropDescriptor;
          break;
        case FieldTypes.ftGuid:
          if (possibleValuesReadonly)
          {
            this.defaultPropDescriptor = this.defaultAsGuidPropDescriptor;
            break;
          }
          this.defaultPropDescriptor = this.defaultAsGuidListPropDescriptor;
          obj = (object) null;
          break;
        case FieldTypes.ftObjectLinkByID:
          if (possibleValuesReadonly)
          {
            this.defaultPropDescriptor = this.defaultAsObjectIDPropDescriptor;
            break;
          }
          this.defaultPropDescriptor = this.defaultAsObjectIDListPropDescriptor;
          obj = (object) null;
          break;
        default:
          if (possibleValuesReadonly)
          {
            this.defaultPropDescriptor = this.defaultAsStringPropDescriptor;
            break;
          }
          this.defaultPropDescriptor = this.defaultAsStringListPropDescriptor;
          obj = (object) null;
          break;
      }
    }
    finally
    {
      this.PropDescriptorCollection.Insert(5, (PropertyDescriptor) this.defaultPropDescriptor);
      if (withSaveValues)
        this.defaultPropDescriptor.SetValue((object) this, obj);
    }
  }

  private void SetDefaultPropDescriptorValue(object aDefaultValue, bool possibleValuesReadonly)
  {
    FieldTypes fieldType = ((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType;
    switch (fieldType)
    {
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftObjectLinkByID:
        object obj = (object) null;
        if (aDefaultValue != null && aDefaultValue.ToString() != string.Empty)
          obj = !(aDefaultValue.ToString() == Intermech.Consts.CurrentUserFunction) ? (object) new ObjectPropertyClass(Convert.ToInt64(aDefaultValue), fieldType == FieldTypes.ftObjectLink) : (object) new ObjectPropertyClass(ObjectPropertyClassVariant.opcvCurrentUser, fieldType == FieldTypes.ftObjectLink);
        this.PropDescriptorCollection[5].SetValue((object) this, obj);
        break;
      default:
        if (possibleValuesReadonly)
        {
          this.PropDescriptorCollection[5].SetValue((object) this, aDefaultValue);
          break;
        }
        if (aDefaultValue != null && aDefaultValue.ToString() != string.Empty)
        {
          switch (fieldType)
          {
            case FieldTypes.ftString:
              this.PropDescriptorCollection[5].SetValue((object) this, (object) new StringPropertyClass(Convert.ToString(aDefaultValue), string.Empty, this.possibleValuesDataTable));
              return;
            case FieldTypes.ftInteger:
              this.PropDescriptorCollection[5].SetValue((object) this, (object) new Int64PropertyClass(Convert.ToInt64(aDefaultValue), string.Empty, this.possibleValuesDataTable));
              return;
            case FieldTypes.ftDouble:
              this.PropDescriptorCollection[5].SetValue((object) this, (object) new DoublePropertyClass(Convert.ToDouble(aDefaultValue), string.Empty, this.possibleValuesDataTable));
              return;
            case FieldTypes.ftDateTime:
              this.PropDescriptorCollection[5].SetValue((object) this, (object) new DateTimePropertyClass(Convert.ToDateTime(aDefaultValue), string.Empty, this.possibleValuesDataTable));
              return;
            case FieldTypes.ftGuid:
              this.PropDescriptorCollection[5].SetValue((object) this, (object) new GuidPropertyClass(new Guid(aDefaultValue.ToString()), string.Empty, this.possibleValuesDataTable));
              return;
            default:
              this.PropDescriptorCollection[5].SetValue((object) this, aDefaultValue);
              return;
          }
        }
        else
        {
          this.PropDescriptorCollection[5].SetValue((object) this, aDefaultValue);
          break;
        }
    }
  }

  private object GetDefaultPropDescriptorValue(bool possibleValuesReadonly)
  {
    return this.GetDefaultPropDescriptorValue(((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType, possibleValuesReadonly);
  }

  private object GetDefaultPropDescriptorValue(FieldTypes ft, bool possibleValuesReadonly)
  {
    object propDescriptorValue = this.PropDescriptorCollection[5].GetValue((object) this);
    if (ft == FieldTypes.ftObjectLink || ft == FieldTypes.ftObjectLinkByID)
    {
      if (propDescriptorValue != null)
        propDescriptorValue = ((ObjectPropertyClass) propDescriptorValue).ObjectPropertyClassVariant != ObjectPropertyClassVariant.opcvCurrentUser ? (object) ((ObjectPropertyClass) propDescriptorValue).ObjectID : (object) Intermech.Consts.CurrentUserFunction;
    }
    else if (!possibleValuesReadonly && propDescriptorValue != null && propDescriptorValue.ToString() != string.Empty)
    {
      switch (ft)
      {
        case FieldTypes.ftString:
          propDescriptorValue = ((PropertyClass) propDescriptorValue).Value;
          break;
        case FieldTypes.ftInteger:
          propDescriptorValue = ((PropertyClass) propDescriptorValue).Value;
          break;
        case FieldTypes.ftDouble:
          propDescriptorValue = ((PropertyClass) propDescriptorValue).Value;
          break;
        case FieldTypes.ftDateTime:
          propDescriptorValue = ((PropertyClass) propDescriptorValue).Value;
          break;
        case FieldTypes.ftGuid:
          propDescriptorValue = ((PropertyClass) propDescriptorValue).Value;
          break;
      }
    }
    if (propDescriptorValue is PropertyClass)
      propDescriptorValue = (object) null;
    return propDescriptorValue;
  }

  public override void Copy()
  {
    if (!(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
      return;
    DBAttributeIDCollection clipboardObject = new DBAttributeIDCollection(new ArrayList((ICollection) new DBAttributeID[1]
    {
      new DBAttributeID((int) this.idValue)
    }));
    service.SetDataObject((object) clipboardObject);
  }

  public override bool CanExclude
  {
    get
    {
      return this.nodeParent != null && this.nodeParent.Tag is AttributeGroupFolder && (int) ((DBPropDescriptorHolder) this.nodeParent.Tag).Id != -1;
    }
  }

  public override bool ExcludeCallback(ref bool needNodeRemove)
  {
    needNodeRemove = true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (((CustomFolder) this.nodeParent.Tag).GetServerObject(sessionKeeper.Session) is IDBAttributesGroup serverObject)
      {
        try
        {
          int[] numArray = new int[1]{ (int) this.idValue };
          serverObject.ExcludeAttribute(numArray);
          return true;
        }
        catch (Exception ex)
        {
          ExceptionHelper.ExceptionService.ShowException(ex);
        }
      }
    }
    return false;
  }

  public override bool DeleteCallbackBefore(ref long deleteMode)
  {
    if (IMMessageBox.Show(MessageDialogs.msgConfirmDelete, string.Format(MessageDialogs.msgReallyDelete0, (object) this.textValue), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
      return false;
    if (!this.attributeTypeProperties.IsContent)
    {
      switch (IMMessageBox.Show(MessageDialogs.msgConfirmDelete, string.Format(LocalizationHolder.rm.GetString("Client.Core_51"), (object) this.textValue), MessageBoxButtons.YesNoCancel, IMMessageBoxImage.Warning))
      {
        case DialogResult.Cancel:
          return false;
        case DialogResult.Yes:
          deleteMode |= Convert.ToInt64(Intermech.Consts.DeleteInstances);
          break;
      }
    }
    return true;
  }

  private ArrayList GetList(object s, params object[] args)
  {
    ArrayList list = (ArrayList) null;
    AttributeTypePropertiesValidator validator = this.GetValidator();
    if (s is DropDownTypeConverter)
      list = ((DropDownTypeConverter) s).GetStandardValuesCustomList((ITypeDescriptorContext) null, (object) validator);
    return list;
  }

  private ArrayList GetAcceptedObjType(object s, params object[] args)
  {
    ArrayList acceptedObjType = (ArrayList) null;
    switch (((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType)
    {
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftObjectLinkByID:
        acceptedObjType = new ArrayList();
        acceptedObjType.AddRange((ICollection) ((ObjectTypeMultiPropertyClass) this.sizePropDescriptor.GetValue((object) this)).ObjectTypeList.ToArray());
        break;
    }
    return acceptedObjType;
  }

  private ArrayList GetObjTypeList(object s, params object[] values)
  {
    int num = -1;
    if (this.sizePropDescriptor == this.sizeAsObjTypePropDescriptor)
      return new ArrayList((ICollection) ((ObjectTypeMultiPropertyClass) this.sizePropDescriptor.GetValue((object) this)).ObjectTypeList.ToArray());
    return new ArrayList((ICollection) new int[1]{ num });
  }

  private ArrayList GetListByType(object s, params object[] args)
  {
    if (args.Length == 0)
      return (ArrayList) null;
    System.Type type = args[0] as System.Type;
    if (type == (System.Type) null || this.possibleValuesDataTable == null)
      return (ArrayList) null;
    ArrayList listByType = new ArrayList();
    foreach (DataRow row in (InternalDataCollectionBase) this.possibleValuesDataTable.Rows)
    {
      object obj = (object) null;
      try
      {
        if (type == typeof (long))
          obj = (object) new Int64PropertyClass(Convert.ToInt64(row["F_INTEGER_VALUE"]), string.Empty, this.possibleValuesDataTable);
        if (type == typeof (double))
          obj = (object) new DoublePropertyClass(Convert.ToDouble(row["F_DOUBLE_VALUE"]), string.Empty, this.possibleValuesDataTable);
        if (type == typeof (string))
          obj = (object) new StringPropertyClass(row["F_STRING_VALUE"].ToString(), string.Empty, this.possibleValuesDataTable);
        if (type == typeof (DateTime))
          obj = (object) new DateTimePropertyClass(Convert.ToDateTime(row["F_DATE_VALUE"]), string.Empty, this.possibleValuesDataTable);
        if (type == typeof (Guid))
          obj = (object) new GuidPropertyClass(new Guid(Convert.ToString(row["F_STRING_VALUE"])), string.Empty, this.possibleValuesDataTable);
        if (type == typeof (ObjectPropertyClass))
          obj = (object) Convert.ToInt64(row["F_INTEGER_VALUE"]);
      }
      catch
      {
      }
      if (obj != null)
        listByType.Add(obj);
    }
    return listByType;
  }

  private ArrayList GetPhysicalValuesList(object s, params object[] args)
  {
    ArrayList physicalValuesList = new ArrayList();
    physicalValuesList.Add((object) new object[3]
    {
      (object) Convert.ToInt64(-1),
      null,
      (object) CoreConsts.NegativeIdDefaultFCaption
    });
    DataTable dataTable = DataHolders.PhysicalValuesHolder.LoadData(false);
    foreach (DataRow dataRow in dataTable.Select("", dataTable.Columns[1].ColumnName))
      physicalValuesList.Add((object) new object[3]
      {
        (object) Convert.ToInt64(dataRow[0]),
        (object) dataRow[1].ToString(),
        (object) CoreConsts.NegativeIdDefaultFCaption
      });
    return physicalValuesList;
  }

  private ArrayList GetMeasureDescriptorList(object s, params object[] args)
  {
    ArrayList measureDescriptorList = (ArrayList) null;
    if (((FieldTypePropertyClass) this.typePropDescriptor.GetValue((object) this)).FieldType == FieldTypes.ftMeasured)
    {
      List<long> objectIdList = ((ObjectListPropertyClass) this.sizeAsPhysValObjectPropDescriptor.GetValue((object) this)).ObjectIDList;
      if (objectIdList[0] == -1L)
      {
        measureDescriptorList = this.guidPropDescriptor.GetValue((object) this) == null || !(this.guidPropDescriptor.GetValue((object) this) is Guid) || !((Guid) this.guidPropDescriptor.GetValue((object) this)).Equals(new Guid("cad00267-306c-11d8-b4e9-00304f19f545")) ? new ArrayList((ICollection) MeasureHelper.Measures) : MeasureEditor.CollectCountMeasureDescriptors();
      }
      else
      {
        measureDescriptorList = new ArrayList();
        for (int index = 0; index < MeasureHelper.Measures.Length; ++index)
        {
          if (objectIdList.IndexOf(MeasureHelper.Measures[index].PhysicalQuantityID) != -1)
            measureDescriptorList.Add((object) MeasureHelper.Measures[index]);
        }
      }
    }
    return measureDescriptorList;
  }

  private DataTable ReturnPossibleValuesDataTable(object s, params object[] args)
  {
    MultiValueModes multiValueMode = ((MultiValueModePropertyClass) this.PropDescriptorCollection[6].GetValue((object) this)).MultiValueMode;
    return (this.GetValidator().PossibleValuesTable == null || multiValueMode == MultiValueModes.SingleValue ? 1 : (multiValueMode == MultiValueModes.MultiValues ? 1 : 0)) == 0 ? this.possibleValuesDataTable : (DataTable) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns>true = влияет на дату модификации содержимого объектов хотя бы у одного из типов объектов, к которым привязан</returns>
  private bool IsContentForAnyObjectType()
  {
    int id = (int) this.Id;
    DataTable dataTable = (DataTable) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectTypeCollection objectTypeCollection = sessionKeeper.Session.GetObjectTypeCollection(-2, CoreConsts.FilterRecords);
      if (objectTypeCollection != null)
        dataTable = objectTypeCollection.GetUsedByAttribute(id);
    }
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
      IDBObjectTypeInfo objectType = service.GetObjectType(int32, false);
      if (objectType != null)
      {
        IDBAttributeTypeInfo4 attributeById = objectType.Attributes.GetAttributeByID(id, false);
        if (attributeById != null && attributeById.IsContent)
          return true;
      }
    }
    return false;
  }

  public override void ConstructPages(TabControl tabControl)
  {
    if (tabControl == null)
      return;
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage, (object) TabPagesHolder.TabPages(this.instGuid).ObjTypes4AttrTabPage, (object) TabPagesHolder.TabPages(this.instGuid).RelTypes4AttrTabPage, (object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage, (object) TabPagesHolder.TabPages(this.instGuid).ActionsTabPage, (object) TabPagesHolder.TabPages(this.instGuid).AttrGroupsListTabPage, (object) TabPagesHolder.TabPages(this.instGuid).AttrImbaseTablesListTabPage);
  }

  public override int Category => 3;
}
