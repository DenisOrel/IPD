
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrTextBtn
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Projects;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.CustomNode;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.PropertyEditors.AttrProcessor;
using Intermech.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
[Designer(typeof (AttrTextBtnControlDesigner))]
[RefreshProperties(RefreshProperties.All)]
public class AttrTextBtn : AttrsControl
{
  private IAttributePropertyDescriber _describer;
  private object _viewValue;
  private ControlButton _btnDots;
  private ControlButton _btnDel;
  private ControlButton _btnForm;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBox _txt;
  private ContextMenuStrip _cm;
  private ToolStripMenuItem _cmiCopyText;
  private ToolStripSeparator _cmSeparator;
  private ToolStripMenuItem _cmiSelect;
  private ToolStripMenuItem _cmiDel;
  private ToolStripMenuItem _cmiForm;
  private ToolStripMenuItem _cmiNewWindow;
  private ToolStripMenuItem _cmiPaste;
  private ToolStripSeparator _cmSeparator2;

  /// <summary>Цвет фона элемента управления.</summary>
  [DefaultValue(typeof (Color), "Control")]
  public new Color BackColor
  {
    get => this._txt.BackColor;
    set => this._txt.BackColor = value;
  }

  /// <summary>Вид обрамления для элемента управления.</summary>
  [DefaultValue(BorderStyle.Fixed3D)]
  public new BorderStyle BorderStyle
  {
    get => this._txt.BorderStyle;
    set => this._txt.BorderStyle = value;
  }

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  public new Font Font
  {
    get => this._txt.Font;
    set => this._txt.Font = value;
  }

  /// <summary>
  /// Основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  [DefaultValue(typeof (Color), "WindowText")]
  public new Color ForeColor
  {
    get => this._txt.ForeColor;
    set => this._txt.ForeColor = value;
  }

  /// <summary>Текстовая подсказка.</summary>
  [DefaultValue("")]
  public string Hint
  {
    get => this._toolTip.GetToolTip((Control) this._txt);
    set => this._toolTip.SetToolTip((Control) this._txt, value);
  }

  /// <summary>Текст, связанный с элементом управления.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public override string Text
  {
    get => this._txt.Text;
    set
    {
      this._txt.Text = string.IsNullOrEmpty(this._designText) || !string.IsNullOrEmpty(value) ? value : this._designText;
    }
  }

  /// <summary>Выравнивание текста в элементе управления.</summary>
  [DefaultValue(HorizontalAlignment.Left)]
  public HorizontalAlignment TextAlign
  {
    get => this._txt.TextAlign;
    set => this._txt.TextAlign = value;
  }

  /// <summary>
  /// Наименование объекта, который будет являться источником информации.
  /// </summary>
  [Browsable(false)]
  public string DataSourceName { get; set; }

  /// <summary>Отступы от краев в элементе управления.</summary>
  /// <remarks>Здесь нужно только для того, чтобы запреить сериализацию</remarks>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new Padding Padding
  {
    get => base.Padding;
    private set => base.Padding = value;
  }

  /// <summary>Контекстная выборка</summary>
  [Browsable(false)]
  [DefaultValue(typeof (Guid), "00000000-0000-0000-0000-000000000000")]
  public Guid SelectionGuid { get; set; }

  /// <summary>Получение текущего набора значений атрибута.</summary>
  protected override object[] GetValues
  {
    get
    {
      object[] getValues;
      if (this._attrValues.AttributeID == -14)
        getValues = new object[1]
        {
          this._viewValue == null || this._viewValue == DBNull.Value ? (object) 0 : this._viewValue
        };
      else
        getValues = new object[1]
        {
          this._viewValue == null ? (object) DBNull.Value : this._viewValue
        };
      return getValues;
    }
  }

  /// <summary>Доступость пункта меню "Вставить".</summary>
  private bool IsPasteEnabled
  {
    get
    {
      IDBObjectTypedIDCollection typedIdCollection = (IDBObjectTypedIDCollection) null;
      if (this.EnabledCtrl && this.AttributeInfo != null && this._attrValues != null)
        typedIdCollection = (ServicesManager.GetService(typeof (IClipboard)) as IClipboard).GetDataObject() as IDBObjectTypedIDCollection;
      return typedIdCollection != null && typedIdCollection.Count == 1;
    }
  }

  /// <summary>Конструктор.</summary>
  public AttrTextBtn()
  {
    this.InitializeComponent();
    this.Name = string.Empty;
    this._txt.GotFocus += new EventHandler(this.On_txt_GotFocus);
    this._txt.LostFocus += new EventHandler(this.On_txt_LostFocus);
    this._btnDots = new ControlButton("Dots", 0)
    {
      Enabled = false
    };
    this._btnDots.Click += new EventHandler(this.On_btn_Click);
    this._btnDel = new ControlButton("Del", 4)
    {
      Enabled = false
    };
    this._btnDel.Click += new EventHandler(this.On_btnDel_Click);
    this._btnForm = new ControlButton("Form", 7)
    {
      Enabled = false
    };
    this._btnForm.Click += new EventHandler(this.On_btnForm_Click);
    this.AddRightButtons(new List<ControlButton>()
    {
      this._btnDots,
      this._btnDel,
      this._btnForm
    });
  }

  /// <summary>
  /// 
  /// </summary>
  public event KeyEventHandler TxtKeyDown;

  /// <summary>Изменение данных.</summary>
  public event EventHandler ValueChanged;

  /// <summary>
  /// зачитываем условия контекстной выборки SelectionGuid в контексте объекта
  /// </summary>
  /// <param name="objID"></param>
  /// <returns></returns>
  internal static ConditionStructure[] GetSelectionConditions(Guid selectionGuid, long objID)
  {
    ConditionStructure[] selectionConditions = (ConditionStructure[]) null;
    if (!selectionGuid.Equals(Guid.Empty))
    {
      QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(selectionGuid);
      if (!objectInfo.Empty)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          selectionConditions = (ServicesManager.ServiceContainer.GetService(typeof (ISelectionsService)) as ISelectionsService).GetConditionStructures((object) sessionKeeper.Session, objectInfo.ObjectID, objID);
      }
    }
    return selectionConditions;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void On_btn_Click(object sender, EventArgs e)
  {
    if (this.AttributeInfo == null || this._attrValues == null)
      return;
    if (this._describer != null && this._describer.GetPropDescriptorEditor(this._attrValues.AttributeID) is UITypeEditor descriptorEditor)
    {
      ConditionStructure[] conditions = (ConditionStructure[]) null;
      Guid selectionGuid = this.SelectionGuid;
      if (!this.SelectionGuid.Equals(Guid.Empty))
      {
        long objID = 0;
        if (this.FindForm() is DesForm form)
          objID = form.Info.ElementIdentifier;
        conditions = AttrTextBtn.GetSelectionConditions(this.SelectionGuid, objID);
      }
      using (ServiceContainer provider = new ServiceContainer())
      {
        using (DropDownEditorForm serviceInstance = new DropDownEditorForm())
        {
          provider.AddService(typeof (IWindowsFormsEditorService), (object) serviceInstance);
          ITypeDescriptorContext context = (ITypeDescriptorContext) new ControlsContext(this.Values, this._describer, this.ParentInfo, conditions);
          switch (descriptorEditor.GetEditStyle(context))
          {
            case UITypeEditorEditStyle.Modal:
            case UITypeEditorEditStyle.DropDown:
              bool flag = false;
              object propDescriptorValue = this._describer.GetPropDescriptorValue(this.ParentInfo, this._attrValues.AttributeID, this._viewValue);
              object obj = descriptorEditor.EditValue(context, (System.IServiceProvider) provider, propDescriptorValue);
              if (obj != null && obj is object[])
                obj = ((object[]) obj).Length == 0 ? (object) null : ((object[]) obj)[0];
              if (!object.Equals(obj, propDescriptorValue))
              {
                this._viewValue = this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, obj);
                if (obj != null)
                  this._txt.Text = Convert.ToString(obj);
                else
                  this.SetText(this._viewValue, this._attrValues.AttributeType);
                flag = true;
              }
              if (flag)
                this.UpdateSlaveAttribute();
              this._txt.Focus();
              this.OnCompletionOfEditing();
              break;
          }
        }
      }
    }
    else
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.AttributeInfo.AttributeGuid);
      if (attributeType == null)
        return;
      IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
      int usersTypeId = service.UsersTypeID;
      int groupsTypeId = service.GroupsTypeID;
      SelectionOptions options1 = SelectionOptions.Default | SelectionOptions.DisableMultiselect;
      long objID = 0;
      if (this.FindForm() is DesForm form)
        objID = form.Info.ElementIdentifier;
      ConditionStructure[] selectionConditions1 = AttrTextBtn.GetSelectionConditions(this.SelectionGuid, objID);
      bool flag;
      if (Convert.ToInt32(attributeType.SizeType) == usersTypeId)
      {
        List<int> collection = new List<int>()
        {
          usersTypeId
        };
        if (attributeType.AttributeID == -8)
          collection.Add(groupsTypeId);
        Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new TypedObjectsSelectedItemsAnalyzer(new List<int>((IEnumerable<int>) collection), true), true);
        long result = 0;
        if (long.TryParse(Convert.ToString(this._viewValue), out result))
        {
          if (attributeType.FieldType == FieldTypes.ftObjectLinkByID)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject objectBaseVersionById = sessionKeeper.Session.GetObjectBaseVersionByID(result, false);
              if (objectBaseVersionById != null)
                result = objectBaseVersionById.ObjectID;
            }
          }
          Intermech.Navigator.SelectionWindow.RegisterAnalyze((IToSelectItemsAnalyzer) new ObjectsToSelectItemsAnalyzer(result));
        }
        IDescriptor rootDescriptor = (IDescriptor) null;
        if (selectionConditions1 == null)
        {
          rootDescriptor = (IDescriptor) new UsersGroupsDescriptor();
        }
        else
        {
          IMSObjectType objectType = MetaDataHelper.GetObjectType(Convert.ToInt32(attributeType.SizeType));
          if (objectType != null)
            rootDescriptor = (IDescriptor) new ObjectsSelectionDescriptor(Convert.ToInt32(attributeType.SizeType), objectType.ObjectTypeName, (IReadOnlyCollection<ConditionStructure>) selectionConditions1);
        }
        if (!(Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_1129"), rootDescriptor, typeof (IDBTypedObjectID), options1) is IDBTypedObjectID[] dbTypedObjectIdArray) || dbTypedObjectIdArray.Length == 0)
          return;
        if (attributeType.AttributeID == -8)
        {
          if (dbTypedObjectIdArray[0].ObjectType != usersTypeId && dbTypedObjectIdArray[0].ObjectType != groupsTypeId)
            return;
        }
        else if (dbTypedObjectIdArray[0].ObjectType != usersTypeId)
          return;
        this._viewValue = (object) (attributeType.FieldType != FieldTypes.ftObjectLinkByID ? dbTypedObjectIdArray[0].ObjectID : dbTypedObjectIdArray[0].ID);
        flag = true;
      }
      else if (attributeType.AttributeID == -14)
      {
        List<long> longList = (List<long>) null;
        int num = -1;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          ProjectFiltrationModes projectFiltrationMode = sessionKeeper.Session.ProjectFiltrationMode;
          try
          {
            sessionKeeper.Session.ProjectFiltrationMode = ProjectFiltrationModes.UserProjects;
            longList = sessionKeeper.Session.ObjectsSelect(new Guid("cad00812-306c-11d8-b4e9-00304f19f545"), new DBRecordSetParams((ConditionStructure[]) null, new object[1]
            {
              (object) -2
            })).AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => x[0] != null && x[0] != DBNull.Value)).Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))).ToList<long>();
            num = sessionKeeper.Session.IdentHelper.ProjectsTypeID;
          }
          finally
          {
            sessionKeeper.Session.ProjectFiltrationMode = projectFiltrationMode;
          }
        }
        Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new TypedObjectsSelectedItemsAnalyzer(num, true), true);
        long result = 0;
        if (long.TryParse(Convert.ToString(this._viewValue), out result))
        {
          if (attributeType.FieldType == FieldTypes.ftObjectLinkByID)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject objectBaseVersionById = sessionKeeper.Session.GetObjectBaseVersionByID(result, false);
              if (objectBaseVersionById != null)
                result = objectBaseVersionById.ObjectID;
            }
          }
          Intermech.Navigator.SelectionWindow.RegisterAnalyze((IToSelectItemsAnalyzer) new ObjectsToSelectItemsAnalyzer(result));
        }
        SelectionOptions options2 = options1 | SelectionOptions.HideTree;
        IDescriptor rootDescriptor = selectionConditions1 != null ? (IDescriptor) new ObjectsSelectionDescriptor(num, LocalizationHolder.rm.GetString("Client_Core_ObjectsType_Projects"), (IReadOnlyCollection<long>) longList, (IReadOnlyCollection<ConditionStructure>) selectionConditions1) : (IDescriptor) new SelectObjectsDescriptor(LocalizationHolder.rm.GetString("Client_Core_ObjectsType_Projects"), longList);
        if (!(Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_704"), rootDescriptor, typeof (IDBObjectID), options2) is IDBObjectID[] dbObjectIdArray) || dbObjectIdArray.Length == 0)
        {
          this._txt.Focus();
          return;
        }
        this._viewValue = (object) (attributeType.FieldType != FieldTypes.ftObjectLinkByID ? dbObjectIdArray[0].Value : dbObjectIdArray[0].ID);
        flag = true;
      }
      else
      {
        IDescriptor rootDescriptor = (IDescriptor) null;
        if (attributeType.SizeType == -1L)
          rootDescriptor = selectionConditions1 != null ? (IDescriptor) new ObjectsSelectionDescriptor(-1, LocalizationHolder.rm.GetString("Client.Core_1099"), (IReadOnlyCollection<ConditionStructure>) selectionConditions1) : (IDescriptor) new ObjectTypesNodeDescriptor();
        else if (attributeType.SizeType == 0L)
        {
          ArrayList typeListByAttrId = ObjectEditor.GetObjTypeListByAttrId(attributeType.AttributeID);
          DescriptorCollection descriptors = new DescriptorCollection();
          if (typeListByAttrId != null)
          {
            int result = 0;
            foreach (object obj in typeListByAttrId)
            {
              if (obj != null && int.TryParse(Convert.ToString(obj), out result))
              {
                if (result == usersTypeId)
                  descriptors.Add((IDescriptor) new UsersGroupsDescriptor());
                else if (selectionConditions1 == null)
                {
                  descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(result));
                }
                else
                {
                  IMSObjectType objectType = MetaDataHelper.GetObjectType(result);
                  if (objectType != null)
                    descriptors.Add((IDescriptor) new ObjectsSelectionDescriptor(result, objectType.ObjectTypeName, (IReadOnlyCollection<ConditionStructure>) selectionConditions1));
                }
              }
            }
          }
          rootDescriptor = (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Client.Core_283"), descriptors);
        }
        else if (!string.IsNullOrEmpty(this.DataSourceName))
        {
          long sourceId = this.GetSourceID();
          ConditionStructure[] selectionConditions2 = AttrTextBtn.GetSelectionConditions(this.SelectionGuid, sourceId);
          if (selectionConditions2 == null)
          {
            rootDescriptor = (IDescriptor) new AttrTextBtnDescriptor(Convert.ToInt32(attributeType.SizeType), sourceId);
          }
          else
          {
            IMSObjectType objectType = MetaDataHelper.GetObjectType(Convert.ToInt32(attributeType.SizeType));
            if (objectType != null)
              rootDescriptor = (IDescriptor) new ObjectsSelectionDescriptor(Convert.ToInt32(attributeType.SizeType), objectType.ObjectTypeName, (IReadOnlyCollection<ConditionStructure>) selectionConditions2);
          }
        }
        else if (selectionConditions1 == null)
        {
          rootDescriptor = (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(Convert.ToInt32(attributeType.SizeType));
        }
        else
        {
          IMSObjectType objectType = MetaDataHelper.GetObjectType(Convert.ToInt32(attributeType.SizeType));
          if (objectType != null)
            rootDescriptor = (IDescriptor) new ObjectsSelectionDescriptor(Convert.ToInt32(attributeType.SizeType), objectType.ObjectTypeName, (IReadOnlyCollection<ConditionStructure>) selectionConditions1);
        }
        long result1 = 0;
        if (long.TryParse(Convert.ToString(this._viewValue), out result1))
        {
          if (attributeType.FieldType == FieldTypes.ftObjectLinkByID)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject objectBaseVersionById = sessionKeeper.Session.GetObjectBaseVersionByID(result1, false);
              if (objectBaseVersionById != null)
                result1 = objectBaseVersionById.ObjectID;
            }
          }
          Intermech.Navigator.SelectionWindow.RegisterAnalyze((IToSelectItemsAnalyzer) new ObjectsToSelectItemsAnalyzer(result1));
        }
        if (!(Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_1130"), rootDescriptor, typeof (IDBObjectID), options1) is IDBObjectID[] dbObjectIdArray) || dbObjectIdArray.Length == 0)
        {
          this._txt.Focus();
          return;
        }
        this._viewValue = (object) (attributeType.FieldType != FieldTypes.ftObjectLinkByID ? Math.Abs(dbObjectIdArray[0].Value) : dbObjectIdArray[0].ID);
        flag = true;
      }
      if (flag)
        this.UpdateSlaveAttribute();
      this.SetText(this._viewValue, attributeType.FieldType);
      this._txt.Focus();
      this.OnCompletionOfEditing();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnDel_Click(object sender, EventArgs e)
  {
    if (this.AttributeInfo == null || this._attrValues == null)
      return;
    if (this._describer != null)
    {
      object propDescriptorValue = this._describer.GetPropDescriptorValue(this.ParentInfo, this._attrValues.AttributeID, (object) DBNull.Value);
      if (propDescriptorValue != null)
      {
        this._viewValue = this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, propDescriptorValue);
        this._txt.Text = Convert.ToString(propDescriptorValue);
        return;
      }
    }
    this._viewValue = (object) DBNull.Value;
    this.UpdateSlaveAttribute();
    this.SetText(this._viewValue, this._attrValues.AttributeType);
    this.OnCompletionOfEditing();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnForm_Click(object sender, EventArgs e)
  {
    if (this._viewValue == DBNull.Value || this._viewValue == null || !(this._viewValue is long) || (long) this._viewValue == 0L)
      return;
    long num1 = Convert.ToInt64(this._viewValue);
    if (this.AttributeInfo != null && this._attrValues != null && this._attrValues.AttributeType == FieldTypes.ftObjectLinkByID)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectBaseVersionById = sessionKeeper.Session.GetObjectBaseVersionByID(num1, false);
        if (objectBaseVersionById != null)
          num1 = objectBaseVersionById.ObjectID;
      }
    }
    int num2 = (int) PropertiesWindow.Execute(string.Empty, string.Empty, num1, false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cm_Opening(object sender, CancelEventArgs e) => this.CheckAccessibilityButtons();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cmiCopyText_Click(object sender, EventArgs e)
  {
    Clipboard.SetText(this._txt.SelectedText);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cmiPaste_Click(object sender, EventArgs e)
  {
    if (this._describer != null)
      throw new ApplicationException(LocalizationHolder.rm.GetString("FormDesigner_AttrsWithDescriber_Paste_Error"));
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.AttributeInfo.AttributeGuid);
    if (attributeType == null)
      return;
    int attributeId = attributeType.AttributeID;
    IDBTypedObjectID typedObjectId = ((ServicesManager.GetService(typeof (IClipboard)) as IClipboard).GetDataObject() as IDBObjectTypedIDCollection).GetTypedObjectID(0);
    int objectType = typedObjectId.ObjectType;
    string objectTypeName = MetaDataHelper.GetObjectTypeName(objectType);
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    if (Convert.ToInt32(attributeType.SizeType) == service.UsersTypeID)
    {
      if (objectType != service.UsersTypeID && (attributeId != -8 || objectType != service.GroupsTypeID))
        throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("FormDesigner_Paste_WrongObjectType"), (object) objectTypeName, (object) objectType.ToString()));
      this._viewValue = (object) (attributeType.FieldType != FieldTypes.ftObjectLinkByID ? typedObjectId.ObjectID : typedObjectId.ID);
    }
    else if (attributeId == -14)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!MetaDataHelper.GetObjectTypeChildrenIDRecursive(sessionKeeper.Session.IdentHelper.ProjectsTypeID).Contains(objectType))
          throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("FormDesigner_Paste_WrongObjectType"), (object) objectTypeName, (object) objectType.ToString()));
        ProjectFiltrationModes projectFiltrationMode = sessionKeeper.Session.ProjectFiltrationMode;
        try
        {
          sessionKeeper.Session.ProjectFiltrationMode = ProjectFiltrationModes.UserProjects;
          List<long> list = sessionKeeper.Session.ObjectsSelect(new Guid("cad00812-306c-11d8-b4e9-00304f19f545"), new DBRecordSetParams((ConditionStructure[]) null, new object[1]
          {
            (object) -2
          })).AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => x[0] != null && x[0] != DBNull.Value)).Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))).ToList<long>();
          if (list.Count != 0)
          {
            if (list.Contains(typedObjectId.ObjectID))
              goto label_16;
          }
          throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("FormDesigner_Paste_WrongObjectType"), (object) objectTypeName, (object) objectType.ToString()));
        }
        finally
        {
          sessionKeeper.Session.ProjectFiltrationMode = projectFiltrationMode;
        }
label_16:
        this._viewValue = (object) (attributeType.FieldType != FieldTypes.ftObjectLinkByID ? typedObjectId.ObjectID : typedObjectId.ID);
      }
    }
    else
    {
      if (attributeType.SizeType == 0L)
      {
        bool flag = true;
        ArrayList typeListByAttrId = ObjectEditor.GetObjTypeListByAttrId(attributeId);
        if (typeListByAttrId != null)
        {
          int result = 0;
          foreach (object obj in typeListByAttrId)
          {
            if (int.TryParse(Convert.ToString(obj), out result) && (objectType == result || MetaDataHelper.IsObjectTypeChildOf(objectType, result)))
            {
              flag = false;
              break;
            }
          }
        }
        if (flag)
          throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("FormDesigner_Paste_WrongObjectType"), (object) objectTypeName, (object) objectType.ToString()));
      }
      else if (attributeType.SizeType != -1L && Convert.ToInt32(attributeType.SizeType) != objectType)
        throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("FormDesigner_Paste_WrongObjectType"), (object) objectTypeName, (object) objectType.ToString()));
      this._viewValue = (object) (attributeType.FieldType != FieldTypes.ftObjectLinkByID ? typedObjectId.ObjectID : typedObjectId.ID);
      this.UpdateSlaveAttribute();
    }
    this.SetText(this._viewValue, attributeType.FieldType);
    this.OnCompletionOfEditing();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cmiNewWindow_Click(object sender, EventArgs e)
  {
    long result = 0;
    if (!long.TryParse(Convert.ToString(this._viewValue), out result) || result == 0L)
      return;
    Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(result), (System.IServiceProvider) ServicesManager.ServiceContainer);
  }

  /// <summary>Фокусирование текстового контрола.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_GotFocus(object sender, EventArgs e) => this.Error = string.Empty;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_KeyDown(object sender, KeyEventArgs e)
  {
    if (this.AttributeInfo == null || this._attrValues == null)
      return;
    if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Escape)
    {
      if (this.TxtKeyDown == null)
        return;
      this.TxtKeyDown((object) this, e);
    }
    else
    {
      if (!this.EnabledCtrl || e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
        return;
      this.On_btnDel_Click(sender, new EventArgs());
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_LostFocus(object sender, EventArgs e)
  {
    this.Error = !this._disableNulls || !this.EnabledCtrl || this._viewValue != null && this._viewValue != DBNull.Value ? string.Empty : this._errMsg_NullValue;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_SizeChanged(object sender, EventArgs e)
  {
    this.Height = this._txt == null || this._txt.Height < 20 ? 22 : this._txt.Height + 2;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_TextChanged(object sender, EventArgs e)
  {
    if (this.IsDesignMode || this.AttributeInfo == null || this._attrValues == null)
      return;
    bool flag = this._viewValue != null && this._viewValue != DBNull.Value;
    this.Error = !this._disableNulls || !this.EnabledCtrl || flag || this._txt.Focused ? string.Empty : this._errMsg_NullValue;
    this.CheckAccessibilityButtons();
    this.Error = !this._disableNulls || !this.EnabledCtrl || this._viewValue != null && this._viewValue != DBNull.Value || this._txt.Focused ? string.Empty : this._errMsg_NullValue;
    this.Modified = true;
    if (!this.Modified)
      return;
    this.OnValueChanged();
  }

  /// <summary>Значение атрибута.</summary>
  public override AttributeValues Values
  {
    get => base.Values;
    set
    {
      this._describer = (IAttributePropertyDescriber) null;
      this._viewValue = (object) DBNull.Value;
      base.Values = value;
      if (value != null)
      {
        if (ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) is IAttributePropertyDescriberService service && !this.IsDesignMode)
          this._describer = service.GetDescriber(value.AttributeID);
        if (this._describer != null)
        {
          object propDescriptorValue = this._describer.GetPropDescriptorValue(this.ParentInfo, value.AttributeID, value.Values[0]);
          if (propDescriptorValue != null)
          {
            this._viewValue = this._describer.GetAttributeValue(this.ParentInfo, value.AttributeID, propDescriptorValue);
            this._txt.Text = Convert.ToString(propDescriptorValue);
            this.CheckAccessibilityButtons();
            return;
          }
        }
        else
          this._viewValue = value.Values[0];
      }
      this.CheckAccessibilityButtons();
      this.SetText(this._viewValue, value != null ? value.AttributeType : FieldTypes.ftUnknown);
    }
  }

  /// <summary>Доступность контрола.</summary>
  [DefaultValue(true)]
  public override bool EnabledCtrl
  {
    get => this._enabled;
    set
    {
      this._enabled = value;
      this.CheckAccessibilityButtons();
      if (this.IsDesignMode || !(this._txt.BackColor == SystemColors.Window))
        return;
      this._txt.BackColor = SystemColors.Control;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  protected override void SetDesignText(string text)
  {
    base.SetDesignText(text);
    if (this._txt.Text == this._designText)
      this._txt.Text = text;
    this._designText = text;
  }

  /// <summary>Проверка доступности кнопок.</summary>
  private void CheckAccessibilityButtons()
  {
    if (this.IsDesignMode)
    {
      this._buttons.Enabled = this.EnabledCtrl;
    }
    else
    {
      bool flag = this._viewValue != null && this._viewValue != DBNull.Value && this._viewValue is long && (long) this._viewValue != 0L;
      this._btnDots.Enabled = this._cmiSelect.Enabled = this.EnabledCtrl;
      this._btnDel.Enabled = this._cmiDel.Enabled = this.EnabledCtrl & flag;
      this._btnForm.Enabled = this._cmiForm.Enabled = this._cmiNewWindow.Enabled = flag;
      this._cmiPaste.Enabled = this.IsPasteEnabled;
      this._cmiCopyText.Enabled = !string.IsNullOrEmpty(this._txt.SelectedText);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private long GetSourceID()
  {
    long result = 0;
    if (this.FindForm() is DesForm form)
    {
      List<IAttributeEditor> linkedControls = form.GetLinkedControls();
      attrTextBtn = (AttrTextBtn) null;
      foreach (IAttributeEditor attributeEditor in linkedControls)
      {
        if (attributeEditor is AttrTextBtn attrTextBtn)
        {
          if (attrTextBtn.Name == this.DataSourceName)
            break;
        }
        attrTextBtn = (AttrTextBtn) null;
      }
      if (attrTextBtn != null)
      {
        AttributeValues values = attrTextBtn.Values;
        if (values != null && values.Values != null && values.Values.Length != 0 && !long.TryParse(Convert.ToString(values.Values[0]), out result))
          result = 0L;
      }
    }
    return result;
  }

  /// <summary>
  /// 
  /// </summary>
  private void OnValueChanged()
  {
    if (this.ValueChanged == null)
      return;
    this.ValueChanged((object) this, new EventArgs());
  }

  /// <summary>
  /// 
  /// </summary>
  private void SetText(object value, FieldTypes valueAttrType)
  {
    if (value == null || value == DBNull.Value)
    {
      this._txt.Text = string.Empty;
      this.Error = !this._disableNulls || !this.EnabledCtrl || this._txt.Focused ? string.Empty : this._errMsg_NullValue;
    }
    else
    {
      long result = 0;
      if (!long.TryParse(Convert.ToString(value), out result))
        result = 0L;
      this._txt.Text = result != 0L ? Convert.ToString((object) new ObjectIDToCaption(result, valueAttrType != FieldTypes.ftObjectLinkByID)) : string.Empty;
      this.Error = string.Empty;
    }
    this.Invalidate();
  }

  /// <summary>
  /// При выборе значения для мастер атрибута возникает необходимость обновить значение связанного с ним атрибута.
  /// </summary>
  private void UpdateSlaveAttribute()
  {
    if (this._attrValues == null || this.DesForm == null)
      return;
    AttributeProcessor attributeProcessor = this.ParentInfo.ElementKind == AttributableElements.Object ? this.DesForm.Processor : this.DesForm.RelationProcessor;
    if (attributeProcessor == null || !attributeProcessor.IsMasterAttribute(this._attrValues.AttributeID))
      return;
    AttributeValues attributeValues = attributeProcessor.ActualAttributeValues.FindByAttributeID(this._attrValues.AttributeID) ?? this._attrValues;
    attributeValues.Values = new object[1]
    {
      this._viewValue == DBNull.Value || this._viewValue == null ? (object) DBNull.Value : this._viewValue
    };
    AttributeValuesList deltaList = (AttributeValuesList) null;
    attributeProcessor.AssignMasterAttributePrim(attributeValues.AttributeID, attributeValues.Values[0], attributeProcessor.ActualAttributeValues, false, out deltaList);
    this.DesForm.UpdateSlaveAttribute(this.ParentInfo.ElementKind == AttributableElements.Object ? this.DesForm.Info : this.DesForm.RelationInfo, deltaList);
  }

  /// <summary>Необходимость сериализации свойства Font.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFont() => !base.Font.Equals((object) this._txt.Font);

  /// <summary>Необходимость сериализации свойства Text.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeText()
  {
    return !string.IsNullOrEmpty(this._designText) ? this._txt.Text != this._designText : !string.IsNullOrEmpty(this._txt.Text);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._txt.SizeChanged -= new EventHandler(this.On_txt_SizeChanged);
      this._txt.TextChanged -= new EventHandler(this.On_txt_TextChanged);
      this._txt.KeyDown -= new KeyEventHandler(this.On_txt_KeyDown);
      this._txt.GotFocus -= new EventHandler(this.On_txt_GotFocus);
      this._txt.LostFocus -= new EventHandler(this.On_txt_LostFocus);
      this._cm.Opening -= new CancelEventHandler(this.On_cm_Opening);
      this._cmiCopyText.Click -= new EventHandler(this.On_cmiCopyText_Click);
      this._cmiPaste.Click -= new EventHandler(this.On_cmiPaste_Click);
      this._cmiSelect.Click -= new EventHandler(this.On_btn_Click);
      this._cmiDel.Click -= new EventHandler(this.On_btnDel_Click);
      this._cmiForm.Click -= new EventHandler(this.On_btnForm_Click);
      this._cmiNewWindow.Click -= new EventHandler(this.On_cmiNewWindow_Click);
      if (!this.IsDesignMode)
      {
        if (this._btnDots != null)
          this._btnDots.Click -= new EventHandler(this.On_btn_Click);
        if (this._btnDel != null)
          this._btnDel.Click -= new EventHandler(this.On_btnDel_Click);
        if (this._btnForm != null)
          this._btnForm.Click -= new EventHandler(this.On_btnForm_Click);
      }
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttrTextBtn));
    this._txt = new TextBox();
    this._cm = new ContextMenuStrip(this.components);
    this._cmiCopyText = new ToolStripMenuItem();
    this._cmSeparator = new ToolStripSeparator();
    this._cmiPaste = new ToolStripMenuItem();
    this._cmSeparator2 = new ToolStripSeparator();
    this._cmiSelect = new ToolStripMenuItem();
    this._cmiDel = new ToolStripMenuItem();
    this._cmiForm = new ToolStripMenuItem();
    this._cmiNewWindow = new ToolStripMenuItem();
    ((ISupportInitialize) this._err).BeginInit();
    this._cm.SuspendLayout();
    this.SuspendLayout();
    this._txt.ContextMenuStrip = this._cm;
    componentResourceManager.ApplyResources((object) this._txt, "_txt");
    this._txt.Name = "_txt";
    this._txt.ReadOnly = true;
    this._txt.SizeChanged += new EventHandler(this.On_txt_SizeChanged);
    this._txt.TextChanged += new EventHandler(this.On_txt_TextChanged);
    this._txt.KeyDown += new KeyEventHandler(this.On_txt_KeyDown);
    this._cm.Items.AddRange(new ToolStripItem[8]
    {
      (ToolStripItem) this._cmiCopyText,
      (ToolStripItem) this._cmSeparator,
      (ToolStripItem) this._cmiPaste,
      (ToolStripItem) this._cmSeparator2,
      (ToolStripItem) this._cmiSelect,
      (ToolStripItem) this._cmiDel,
      (ToolStripItem) this._cmiForm,
      (ToolStripItem) this._cmiNewWindow
    });
    this._cm.Name = "_cm";
    componentResourceManager.ApplyResources((object) this._cm, "_cm");
    this._cm.Opening += new CancelEventHandler(this.On_cm_Opening);
    this._cmiCopyText.DisplayStyle = ToolStripItemDisplayStyle.Text;
    componentResourceManager.ApplyResources((object) this._cmiCopyText, "_cmiCopyText");
    this._cmiCopyText.Name = "_cmiCopyText";
    this._cmiCopyText.Click += new EventHandler(this.On_cmiCopyText_Click);
    this._cmSeparator.Name = "_cmSeparator";
    componentResourceManager.ApplyResources((object) this._cmSeparator, "_cmSeparator");
    this._cmiPaste.DisplayStyle = ToolStripItemDisplayStyle.Text;
    componentResourceManager.ApplyResources((object) this._cmiPaste, "_cmiPaste");
    this._cmiPaste.Name = "_cmiPaste";
    this._cmiPaste.Click += new EventHandler(this.On_cmiPaste_Click);
    this._cmSeparator2.Name = "_cmSeparator2";
    componentResourceManager.ApplyResources((object) this._cmSeparator2, "_cmSeparator2");
    this._cmiSelect.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this._cmiSelect.Name = "_cmiSelect";
    componentResourceManager.ApplyResources((object) this._cmiSelect, "_cmiSelect");
    this._cmiSelect.Click += new EventHandler(this.On_btn_Click);
    this._cmiDel.DisplayStyle = ToolStripItemDisplayStyle.Text;
    componentResourceManager.ApplyResources((object) this._cmiDel, "_cmiDel");
    this._cmiDel.Name = "_cmiDel";
    this._cmiDel.Click += new EventHandler(this.On_btnDel_Click);
    this._cmiForm.DisplayStyle = ToolStripItemDisplayStyle.Text;
    componentResourceManager.ApplyResources((object) this._cmiForm, "_cmiForm");
    this._cmiForm.Name = "_cmiForm";
    this._cmiForm.Click += new EventHandler(this.On_btnForm_Click);
    this._cmiNewWindow.DisplayStyle = ToolStripItemDisplayStyle.Text;
    componentResourceManager.ApplyResources((object) this._cmiNewWindow, "_cmiNewWindow");
    this._cmiNewWindow.Name = "_cmiNewWindow";
    this._cmiNewWindow.Click += new EventHandler(this.On_cmiNewWindow_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._txt);
    this._err.SetIconAlignment((Control) this, (ErrorIconAlignment) componentResourceManager.GetObject("$this.IconAlignment"));
    this._err.SetIconPadding((Control) this, (int) componentResourceManager.GetObject("$this.IconPadding"));
    this.Name = nameof (AttrTextBtn);
    ((ISupportInitialize) this._err).EndInit();
    this._cm.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
