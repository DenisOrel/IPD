// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.FormDesignerControl
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using DevExpress.IM.Utils;
using Intermech.Bars;
using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.Docking;
using Intermech.Docking.Rendering;
using Intermech.Expert;
using Intermech.FormDesigner.AutoPlace;
using Intermech.FormDesigner.Descriptors;
using Intermech.FormDesigner.Undo;
using Intermech.FormDesigner.Wrappers;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>Сам редактор форм.</summary>
public class FormDesignerControl : 
  UserControl,
  IHostView,
  ICommandTarget,
  IFormDesignerEditorHookable
{
  private IDesignerHost _host;
  private IServiceContainer _serviceContainer;
  private ISelectionService _selectionSrv;
  private UIService _uiSrv;
  private ICommandManager _commandManager;
  internal MenuCommandService _menuCommandService;
  private IFormDesignerEditorService _editorService;
  private MenuItemForm _menuItemForm;
  private FormDesignerToolBar _fdToolBar;
  private IMessageFilter _filter;
  private UndoHandler _undoHandler;
  private Dictionary<System.Type, System.Type> _hash = new Dictionary<System.Type, System.Type>();
  private Dictionary<System.Type, string> _hash4text = new Dictionary<System.Type, string>();
  private Control _lastUsedControl;
  private string _dcPropertiesText = string.Empty;
  private bool _modified;
  private bool _readOnly;
  private bool _inUpdate;
  private bool _inUndo;
  private bool _isWF;
  private bool saveTabPageIndicesLoaded;
  private bool saveTabPageIndices = true;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ComboBox _cmbControls;
  private DockControl _dcProperties;
  private DockControl _dcToolbox;
  private Panel _designerPanel;
  private DockManager _dockManager;
  private PropertyGrid _propertyGrid;
  private DockContainer _bottomDock;
  private DockContainer _leftDock;
  private ToolboxService _lstToolbox;
  private Panel _panel;
  private DockContainer _rightDock;
  private TableLayoutPanel _tableLayoutPanel;
  private ToolTipController _toolTip;
  private Label _label;
  private DockContainer _topDock;
  private Intermech.Bars.ToolBar _tb;
  internal ButtonItem _btnNewForm;
  internal ButtonItem _btnOpen;
  private ButtonItem _btnPreview;
  private ButtonItem _btnUndo;
  private ButtonItem _btnRedo;
  private ButtonItem _btnCancel;
  private ButtonItem _btnOK;
  private LabelItem labelItem1;
  private ImageList _il;
  internal ButtonItem _btnFormLink;
  internal ButtonItem _btnAutoplace;

  /// <summary>
  /// 
  /// </summary>
  public long FormID { get; set; }

  /// <summary>Дизайнер для формы.</summary>
  public DesignSurface Surface { get; private set; }

  /// <summary>Изменились ли данные.</summary>
  public bool Modified
  {
    get => this._modified;
    set
    {
      this._modified = value && !this.ReadOnly;
      this._commandManager.QueryStatus();
      this._btnOK.Enabled = this._btnCancel.Enabled = this._modified;
    }
  }

  /// <summary>Редактор только для просмотра.</summary>
  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      if (this._menuItemForm != null)
        this._menuItemForm.SetMenuReadOnly(value);
      if (this._readOnly)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this.FormID, false);
          if (objectActualCopy.CheckoutBy == 0L)
          {
            this._label.Text = LocalizationHolder.rm.GetString("FormDesigner_110");
          }
          else
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(objectActualCopy.CheckoutBy);
            this._label.Text = string.Format(LocalizationHolder.rm.GetString("FormDesigner_111"), (object) dbObject.Caption);
          }
        }
      }
      this._tableLayoutPanel.Visible = this._readOnly;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public bool IsWorkflowForm
  {
    get => this._isWF;
    set
    {
      this._isWF = value;
      this._btnNewForm.Enabled = this._btnOpen.Enabled = !value;
      this._btnAutoplace.Enabled = this._btnNewForm.Enabled = !value;
      this._menuCommandService._linkTo.Enabled = !value;
      this._menuItemForm.IsWorkFlow = value;
    }
  }

  /// <summary>Конструктор.</summary>
  public FormDesignerControl()
  {
    this.InitializeComponent();
    this._commandManager = ProviderHolder.ServiceProvider.GetService(typeof (ICommandManager)) as ICommandManager;
    this._editorService = ProviderHolder.EditorService;
    ProviderHolder.BarManager.RendererChanged += new EventHandler(this.OnToolBar_RendererChanged);
    this.SetRenderer(ProviderHolder.BarManager.Renderer);
    this._menuItemForm = new MenuItemForm(new Action<object, EventArgs>(this.On_MenuClick));
    this._fdToolBar = new FormDesignerToolBar(new Action<object, EventArgs>(this.On_MenuClick));
    this.PopulateToolbox();
    this._dcPropertiesText = this._dcProperties.Text;
  }

  /// <summary>Событие на обновление списка ToolBoxItems.</summary>
  public event FormDesignerControlToolBoxUpdateEvent ToolBoxUpdateEvent;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnNewForm_Click(object sender, EventArgs e)
  {
    int objectTypeId = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(this.FormID).ObjectTypeID;
    long objectByTypeDialog = ServiceUtils.GetService<IObjectCreatorService>((object) ApplicationServices.Container, false).CreateObjectByTypeDialog(objectTypeId);
    switch (objectByTypeDialog)
    {
      case -1:
        break;
      case 0:
        break;
      default:
        ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectByTypeDialog));
        Intermech.Navigator.Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objectByTypeDialog), (System.IServiceProvider) null);
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnOpen_Click(object sender, EventArgs e)
  {
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad0011b-306c-11d8-b4e9-00304f19f545");
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("EditorForm.SelectionObjectDialog.Caption"), LocalizationHolder.rm.GetString("EditorForm.SelectionObjectDialog.Message"), objectTypeId, SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
    if (numArray == null || numArray.Length == 0)
      return;
    Intermech.Navigator.Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(numArray[0]), (System.IServiceProvider) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnAutoplace_Click(object sender, EventArgs e) => this.AutoPlaceWizardDlg();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnFormLink_Click(object sender, EventArgs e) => this.LinkTo();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnPreview_Click(object sender, EventArgs e)
  {
    using (ImChunkedStream imChunkedStream = new ImChunkedStream())
    {
      ImXmlWriter.Write((Stream) imChunkedStream, this._host);
      try
      {
        using (Form form = ImXmlReader.Read((Stream) imChunkedStream, (IDesignerHost) null) as Form)
        {
          form.FormBorderStyle = FormBorderStyle.Sizable;
          form.ShowIcon = false;
          form.ShowInTaskbar = false;
          form.StartPosition = FormStartPosition.CenterScreen;
          int num = (int) form.ShowDialog();
        }
      }
      catch (Exception ex)
      {
        throw new Exception(LocalizationHolder.rm.GetString("Client.Core_204"), ex);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnUndo_ButtonClick(object sender, EventArgs e) => this.Undo();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnRedo_ButtonClick(object sender, EventArgs e) => this.Redo();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnCancel_Click(object sender, EventArgs e) => this.Rollback();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnOK_Click(object sender, EventArgs e) => this.Commit();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnComponentAdded(object sender, ComponentEventArgs e)
  {
    if (!this._inUndo)
    {
      if (this.ReadOnly)
      {
        this._inUndo = true;
        try
        {
          this._host.DestroyComponent(e.Component);
        }
        finally
        {
          this._inUndo = false;
        }
        this._propertyGrid.Refresh();
      }
      else
      {
        if (e.Component is AttrComboBox component)
          component.DropDownStyle = ComboBoxStyle.DropDownList;
        this._cmbControls.Items.Add((object) e.Component);
      }
    }
    this.Modified = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
  {
    if (this.ReadOnly && !this._inUndo)
    {
      this._inUndo = true;
      try
      {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(e.Component);
        if (e.Member.Name == "Controls")
        {
          Control component = e.Component as Control;
          if (this._lastUsedControl != null)
          {
            if (component != null)
            {
              if (!this._lastUsedControl.Parent.Equals((object) component))
                this._lastUsedControl.Parent = component;
            }
          }
        }
        else if (e.Member.Name == "Size")
        {
          this._lastUsedControl = e.Component as Control;
          if (e.OldValue != null)
            properties[e.Member.Name].SetValue(e.Component, e.OldValue);
        }
        else
        {
          this._lastUsedControl = e.Component as Control;
          properties[e.Member.Name].SetValue(e.Component, e.OldValue);
        }
      }
      catch
      {
      }
      finally
      {
        this._inUndo = false;
      }
      this._propertyGrid.Refresh();
    }
    this.Modified = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnComponentRemoved(object sender, ComponentEventArgs e)
  {
    if (this.ReadOnly && !this._inUndo)
    {
      this._inUndo = true;
      try
      {
        IComponent component1 = this._host.CreateComponent(e.Component.GetType());
        Control component2 = e.Component as Control;
        Control control = component1 as Control;
        if (component2 != null && control != null)
          control.Parent = component2.Parent;
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(e.Component.GetType()))
        {
          if (property.ShouldSerializeValue((object) e.Component) && property.IsBrowsable && !(property.Name == "Visible"))
            property.SetValue((object) component1, property.GetValue((object) e.Component));
        }
      }
      finally
      {
        this._inUndo = false;
      }
      this._propertyGrid.Refresh();
    }
    this._cmbControls.Items.Remove((object) e.Component);
    this.Modified = true;
  }

  /// <summary>Изменение выделенного компонента.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnSelectionChanged(object sender, EventArgs e)
  {
    this._fdToolBar.CheckToolBar(this._selectionSrv);
    this._inUpdate = true;
    if (this._selectionSrv.SelectionCount == 0)
    {
      this._propertyGrid.SelectedObject = (object) null;
      this._cmbControls.SelectedIndex = -1;
    }
    else
    {
      string str = string.Empty;
      ICollection selectedComponents = this._selectionSrv.GetSelectedComponents();
      object[] objArray = new object[selectedComponents.Count];
      int num = 0;
      foreach (object component in (IEnumerable) selectedComponents)
      {
        System.Type type = component.GetType();
        if (this._hash.ContainsKey(type) && this._hash[type] != (System.Type) null)
        {
          object instance = Activator.CreateInstance(this._hash[type], component);
          objArray[num++] = type == typeof (AttrButton) || type == typeof (AttrCheckedListBox) || type == typeof (AttrListBoxBtn) || type == typeof (AttrObjectsList) || type == typeof (AttrMeasuredEdit) || type == typeof (AttrComboBox) || type == typeof (AttrDateEdit) || type == typeof (AttrListBox) || type == typeof (AttrMeasuredListBox) || type == typeof (AttrMemoEdit) || type == typeof (ObjectsList) || instance is ICustomTypeDescriptor ? instance : (object) new ClassWrapperForPropertyGrid(instance);
        }
        else
          objArray[num++] = (object) new lTypeDescriptor(component, true);
        if (this._hash4text.ContainsKey(type) && this._hash4text[type] != null)
          str = " - " + this._hash4text[type];
      }
      if (selectedComponents.Count == 1)
      {
        this._cmbControls.SelectedItem = this._selectionSrv.PrimarySelection;
        this._dcProperties.Text = this._dcPropertiesText + str;
      }
      else
      {
        this._cmbControls.SelectedIndex = -1;
        this._dcProperties.Text = string.Empty;
      }
      this._propertyGrid.SelectedObjects = objArray;
    }
    this._inUpdate = false;
    this._commandManager.QueryStatus();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_MenuClick(object sender, EventArgs e)
  {
    if (!(sender is ButtonItemBase buttonItemBase))
      return;
    switch (buttonItemBase.CommandName)
    {
      case "ShowToolBox":
        this.ShowPanel(this._dcToolbox, true);
        break;
      case "ShowProperties":
        this.ShowPanel(this._dcProperties, true);
        break;
      case "LinkTo":
        this.LinkTo();
        break;
      case "Condition":
        IExpertEditor service = ServiceUtils.GetService<IExpertEditor>((object) ApplicationServices.Container, false);
        object cond1 = (object) null;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          cond1 = (object) CondHelper.LoadObjectCond(sessionKeeper.Session, this.FormID);
        if (!service.EditCondition(ref cond1, string.Format(LocalizationHolder.rm.GetString("FormDesigner_117"), (object) this.FormID)) || !(cond1 is TempFormula cond2))
          break;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          CondHelper.SaveObjectCond(sessionKeeper.Session, this.FormID, cond2);
          break;
        }
      case "Auto":
        this.AutoPlaceWizardDlg();
        break;
      case "Reset":
        this.ResetWorkspace();
        break;
      default:
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("FormDesigner_118"));
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnToolBar_RendererChanged(object sender, EventArgs e)
  {
    this.SetRenderer((sender as BarManager).Renderer);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_cmbControls_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._inUpdate || this._host == null)
      return;
    object[] components;
    if (this._cmbControls.SelectedIndex < 0)
      components = new object[0];
    else
      components = new object[1]
      {
        this._cmbControls.Items[this._cmbControls.SelectedIndex]
      };
    this._selectionSrv.SetSelectedComponents((ICollection) components);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="s"></param>
  /// <param name="e"></param>
  private void On_propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    this.Modified = true;
  }

  /// <summary>Выполнить действия.</summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  public bool Execute(ICommandState commandState)
  {
    bool flag = true;
    if (commandState.CommandName == "Undo")
      flag = this.Undo();
    else if (commandState.CommandName == "Redo")
      flag = this.Redo();
    else if (commandState.CommandName == "Copy")
      this.InvokeStandardCommand(StandardCommands.Copy);
    else if (commandState.CommandName == "Cut")
      this.InvokeStandardCommand(StandardCommands.Cut);
    else if (commandState.CommandName == "Paste")
      this.InvokeStandardCommand(StandardCommands.Paste);
    else if (commandState.CommandName == "Save")
      this.Commit();
    else
      flag = false;
    return flag;
  }

  /// <summary>Проверить возможность выполнения действия.</summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  public bool QueryStatus(ICommandState commandState)
  {
    bool flag = true;
    if (commandState.CommandName == "Undo")
    {
      if (this._undoHandler != null)
        this._btnUndo.Enabled = commandState.Enabled = this._undoHandler.EnableUndo;
      else
        flag = false;
    }
    else if (commandState.CommandName == "Redo")
    {
      if (this._undoHandler != null)
        this._btnRedo.Enabled = commandState.Enabled = this._undoHandler.EnableRedo;
      else
        flag = false;
    }
    else if (commandState.CommandName == "Copy")
      commandState.Enabled = this.GetStatndardCommandsStatus(StandardCommands.Copy);
    else if (commandState.CommandName == "Cut")
      commandState.Enabled = this.GetStatndardCommandsStatus(StandardCommands.Cut);
    else if (commandState.CommandName == "Paste")
      commandState.Enabled = this.GetStatndardCommandsStatus(StandardCommands.Paste);
    else if (commandState.CommandName == "Save")
      commandState.Enabled = this.Modified;
    else
      flag = false;
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="cmd"></param>
  /// <returns></returns>
  private bool GetStatndardCommandsStatus(CommandID cmd)
  {
    bool statndardCommandsStatus = false;
    if (this._menuCommandService != null)
    {
      MenuCommand command = this._menuCommandService.FindCommand(cmd);
      statndardCommandsStatus = command != null && command.Enabled;
    }
    return statndardCommandsStatus;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="cmd"></param>
  private void InvokeStandardCommand(CommandID cmd)
  {
    this._menuCommandService.GlobalInvoke(cmd);
    this._commandManager.QueryStatus();
  }

  /// <summary>Контрол верхнего уровня (Root).</summary>
  public Control View { get; private set; }

  /// <summary>Перекрытие выбора атрибутов.</summary>
  public IFormDesignerEditorHook Hook { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnEnter(EventArgs e)
  {
    this._commandManager.ActiveTarget = (ICommandTarget) this;
    this._menuItemForm.SetMenuVisible(true);
    if (!this.ReadOnly)
      this._fdToolBar.LoadToolBarState();
    base.OnEnter(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLeave(EventArgs e)
  {
    this._menuItemForm.SetMenuVisible(false);
    this._fdToolBar.SaveToolBarState();
    if (!this.ReadOnly)
      this._fdToolBar.RemoveToolBar();
    base.OnLeave(e);
  }

  /// <summary>Активировать редактор форм.</summary>
  /// <param name="readOnly"></param>
  public void Activate(bool readOnly)
  {
    this.ReadOnly = readOnly;
    this.LoadPanels();
    if (!this._dcToolbox.IsOpen)
      this.ShowPanel(this._dcToolbox, true);
    if (!this._dcProperties.IsOpen)
      this.ShowPanel(this._dcProperties, true);
    if (this._editorService != null)
      this._editorService.Add(this.FormID, (Control) this);
    this.LoadForm();
  }

  /// <summary>Деактивировать редактор форм.</summary>
  public void Deactivate()
  {
    ProviderHolder.DockString = this._dockManager.GetLayout();
    if (this._editorService != null)
      this._editorService.Remove(this.FormID);
    this._propertyGrid.SelectedObjects = (object[]) null;
  }

  /// <summary>
  /// 
  /// </summary>
  public void ViewStateReadOnlyChanged(bool readOnly)
  {
    this.ReadOnly = readOnly;
    this._serviceContainer.RemoveService(typeof (IMenuCommandService));
    this._menuCommandService = new MenuCommandService(this._host, this.ReadOnly);
    this._serviceContainer.AddService(typeof (IMenuCommandService), (object) this._menuCommandService);
    this._menuItemForm.MenuCommandSrv = (IMenuCommandService) this._menuCommandService;
    this._fdToolBar.MenuCommandSrv = (IMenuCommandService) this._menuCommandService;
    this._undoHandler = (UndoHandler) null;
    this._lstToolbox.Enabled = !this.ReadOnly;
    this._btnAutoplace.Enabled = !this.ReadOnly && !this.IsWorkflowForm;
  }

  /// <summary>Вызвать диалог автоматической расстановки контролов.</summary>
  public void AutoPlaceWizardDlg()
  {
    if (!(this._host.RootComponent.GetType() == typeof (DesForm)))
      return;
    using (AutoPlaceWizard autoPlaceWizard = new AutoPlaceWizard((object) this._host, this._host.RootComponent as DesForm))
    {
      int num = (int) autoPlaceWizard.ShowDialog();
    }
  }

  /// <summary>Отменить изменения и перечитать форму.</summary>
  /// <param name="loadForm"></param>
  public void Rollback(bool loadForm = true)
  {
    if (this._undoHandler != null)
      this._undoHandler.Detach();
    IFormDesignerEditorHook hook = this.Hook;
    if (loadForm)
    {
      this._dcProperties.Text = this._dcPropertiesText;
      this.LoadForm();
    }
    else
      this.Modified = false;
    this.Hook = hook;
  }

  private bool SaveTabPageIndices
  {
    get
    {
      if (!this.saveTabPageIndicesLoaded)
      {
        this.saveTabPageIndices = (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadBool("CLIENT", "FORMDESIGNER", "SAVETABPAGEINDICES", true, DBConfigMode.GlobalOnly);
        this.saveTabPageIndicesLoaded = true;
      }
      return this.saveTabPageIndices;
    }
  }

  /// <summary>Подтвердить изменения и сохранить в базу.</summary>
  public void Commit()
  {
    using (ImChunkedStream aSourceStream = new ImChunkedStream())
    {
      DesForm rootComponent = this._host.RootComponent as DesForm;
      if (!this.SaveTabPageIndices)
        rootComponent.ResetTabControlIndicesToZero(true);
      try
      {
        ImXmlWriter.Write((Stream) aSourceStream, this._host);
        rootComponent.CheckImageFromLibraryAttribute();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IFormDBObject formDbObject = sessionKeeper.Session.GetObject(this.FormID) as IFormDBObject;
          IDBAttribute attributeByGuid = formDbObject.GetAttributeByGuid(new Guid("cad0011d-306c-11d8-b4e9-00304f19f545"));
          if (rootComponent.Links != null)
            rootComponent.Links.Save();
          formDbObject.AddToCache();
          aSourceStream.Position = 0L;
          BlobInformation aBlobInformation = new BlobInformation(aSourceStream.Length, 0L, DateTime.Now, this.FormID.ToString() + ".xml", ArcMethods.ZLibPacked, LocalizationHolder.rm.GetString("FormDesigner_109"));
          new BlobProcWriter(attributeByGuid, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
        }
        ClientFormsCache.Save(this.FormID, aSourceStream.ToArray());
        this.Modified = false;
      }
      finally
      {
        if (!this.SaveTabPageIndices)
          rootComponent.ResetTabControlIndicesToZero(false);
      }
    }
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this.FormID));
  }

  /// <summary>Сбросить настройки рабочей области.</summary>
  public void ResetWorkspace()
  {
    this._dcToolbox.Parent = (Control) this._leftDock;
    this._leftDock.LayoutSystem = new SplitLayoutSystem(250, 400, Orientation.Horizontal, new LayoutSystemBase[1]
    {
      (LayoutSystemBase) new ControlLayoutSystem(250, 665, new DockControl[1]
      {
        this._dcToolbox
      }, this._dcToolbox)
    });
    this._dcProperties.Parent = (Control) this._rightDock;
    this._rightDock.LayoutSystem = new SplitLayoutSystem(250, 400, Orientation.Horizontal, new LayoutSystemBase[1]
    {
      (LayoutSystemBase) new ControlLayoutSystem(250, 665, new DockControl[1]
      {
        this._dcProperties
      }, this._dcProperties)
    });
    if (this._host == null || !(this._host.RootComponent is DesForm rootComponent) || rootComponent.Location.X >= 0 && rootComponent.Location.Y >= 0)
      return;
    rootComponent.Location = new Point(15, 15);
  }

  /// <summary>События перед сохранением.</summary>
  public void BeforeCheckIn()
  {
    if (this.FormID >= 0L || !this._modified)
      return;
    string caption = LocalizationHolder.rm.GetString("FormDesigner_116");
    if (MessageBox.Show(LocalizationHolder.rm.GetString("FormDesigner_115"), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      this.Commit();
    else
      this.Rollback(false);
  }

  /// <summary>
  /// 
  /// </summary>
  private void LoadPanels()
  {
    if (string.IsNullOrEmpty(ProviderHolder.DockString))
      return;
    try
    {
      XmlDocument xmlDocument1 = new XmlDocument();
      xmlDocument1.InnerXml = ProviderHolder.DockString;
      XmlDocument savedDoc = xmlDocument1;
      XmlNode xmlNode = savedDoc.SelectSingleNode("Layout");
      if (xmlNode == null)
        return;
      XmlDocument xmlDocument2 = new XmlDocument();
      xmlDocument2.InnerXml = this._dockManager.GetLayout();
      XmlDocument defaultDoc = xmlDocument2;
      XmlNode settingsNode1 = this.GetSettingsNode(savedDoc, defaultDoc, this._dcToolbox.Guid);
      if (settingsNode1 != null)
        xmlNode.AppendChild(settingsNode1);
      XmlNode settingsNode2 = this.GetSettingsNode(savedDoc, defaultDoc, this._dcProperties.Guid);
      if (settingsNode2 != null)
        xmlNode.AppendChild(settingsNode2);
      this._dockManager.SetLayout(savedDoc.OuterXml);
    }
    catch
    {
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="savedDoc"></param>
  /// <param name="defaultDoc"></param>
  /// <param name="guid"></param>
  /// <returns></returns>
  private XmlNode GetSettingsNode(XmlDocument savedDoc, XmlDocument defaultDoc, Guid guid)
  {
    XmlNode node = savedDoc.SelectSingleNode($"//Window[@Guid='{guid}']");
    if (node == null)
    {
      node = defaultDoc.SelectSingleNode($"//Window[@Guid='{guid}']");
      if (node != null)
        node = savedDoc.ImportNode(node, true);
    }
    return node;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="panel"></param>
  /// <param name="visible"></param>
  private void ShowPanel(DockControl panel, bool visible) => panel?.Show(this._dockManager);

  /// <summary>
  /// 
  /// </summary>
  private void PopulateToolbox()
  {
    List<IMToolBoxItem> imToolBoxItemList = new List<IMToolBoxItem>();
    string category1 = LocalizationHolder.rm.GetString("FormDesigner.ToolBoxCategory.TextElements");
    string name1 = LocalizationHolder.rm.GetString("FormDesigner_166");
    Bitmap image1 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.Label.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name1, typeof (IMLabel), typeof (LabelWrapper), category1, image1));
    string name2 = LocalizationHolder.rm.GetString("FormDesigner_186");
    Bitmap image2 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrLabel.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name2, typeof (AttrLabel), typeof (AttrLabelWrapper), category1, image2));
    string name3 = LocalizationHolder.rm.GetString("FormDesigner_172");
    Bitmap image3 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrTextEdit.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name3, typeof (AttrTextEdit), typeof (AttrTextEditWrapper), category1, image3));
    string name4 = LocalizationHolder.rm.GetString("Attribute_FormDesigner_Controls_AttrMaskedTextEdit");
    Bitmap image4 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrTextEdit.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name4, typeof (AttrMaskedTextEdit), typeof (AttrMaskedTextEditWrapper), category1, image4));
    string name5 = LocalizationHolder.rm.GetString("FormDesigner_177");
    Bitmap image5 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrMemoEdit.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name5, typeof (AttrMemoEdit), typeof (AttrMemoEditDescriptor), category1, image5));
    string name6 = LocalizationHolder.rm.GetString("FormDesigner_173");
    Bitmap image6 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrPassword.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name6, typeof (AttrPassword), typeof (AttrPasswordWrapper), category1, image6));
    string name7 = LocalizationHolder.rm.GetString("FormDesigner_180");
    Bitmap image7 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrTextBtn.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name7, typeof (AttrTextBtn), typeof (AttrTextBtnControlDescriptor), category1, image7));
    string name8 = LocalizationHolder.rm.GetString("FormDesigner_181");
    Bitmap image8 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrTextBtnComp.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name8, typeof (AttrTextBtnComp), typeof (AttrTextBtnCompWrapper), category1, image8));
    string name9 = LocalizationHolder.rm.GetString("FormDesigner_183");
    Bitmap image9 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrMeasuredEdit.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name9, typeof (AttrMeasuredEdit), typeof (AttrMeasuredEditDescriptor), category1, image9));
    string category2 = LocalizationHolder.rm.GetString("FormDesigner.ToolBoxCategory.Lists");
    string name10 = LocalizationHolder.rm.GetString("FormDesigner_174");
    Bitmap image10 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrListBox.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name10, typeof (AttrListBox), typeof (AttrListBoxDescriptor), category2, image10));
    string name11 = LocalizationHolder.rm.GetString("FormDesigner_176");
    Bitmap image11 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrCheckedListBox.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name11, typeof (AttrCheckedListBox), typeof (AttrCheckedListBoxDescriptor), category2, image11));
    string name12 = LocalizationHolder.rm.GetString("FormDesigner_182");
    Bitmap image12 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrListBoxBtn.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name12, typeof (AttrListBoxBtn), typeof (AttrListBoxBtnDescriptor), category2, image12));
    string name13 = LocalizationHolder.rm.GetString("FormDesigner_182_t");
    Bitmap image13 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrListBoxBtn.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name13, typeof (AttrObjectsList), typeof (AttrObjectsListDescriptor), category2, image13));
    string name14 = LocalizationHolder.rm.GetString("FormDesigner_184");
    Bitmap image14 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrMeasuredListBox.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name14, typeof (AttrMeasuredListBox), typeof (AttrMeasuredListBoxDescriptor), category2, image14));
    string name15 = LocalizationHolder.rm.GetString("FormDesigner_175");
    Bitmap image15 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrComboBox.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name15, typeof (AttrComboBox), typeof (AttrComboBoxDescriptor), category2, image15));
    string name16 = LocalizationHolder.rm.GetString("FormDesigner_178");
    Bitmap image16 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrDateTimePicker.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name16, typeof (AttrDateEdit), typeof (AttrDateEditDescriptor), category2, image16));
    string name17 = LocalizationHolder.rm.GetString("FormDesigner_227");
    Bitmap image17 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrListBox.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name17, typeof (ObjectsList), typeof (ObjectsListDescriptor), category2, image17));
    string category3 = LocalizationHolder.rm.GetString("FormDesigner.ToolBoxCategory.Buttons");
    string name18 = LocalizationHolder.rm.GetString("FormDesigner_179");
    Bitmap image18 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrCheckBox.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name18, typeof (AttrCheckBox), typeof (AttrCheckBoxWrapper), category3, image18));
    string name19 = LocalizationHolder.rm.GetString("FormDesigner_185");
    Bitmap image19 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.AttrButton.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name19, typeof (AttrButton), typeof (AttrButtonDescriptor), category3, image19));
    string category4 = LocalizationHolder.rm.GetString("FormDesigner.ToolBoxCategory.Grouping");
    string name20 = LocalizationHolder.rm.GetString("FormDesigner_169");
    Bitmap image20 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.GroupBox.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name20, typeof (IMGroupBox), typeof (IMGroupBoxWrapper), category4, image20));
    string name21 = LocalizationHolder.rm.GetString("FormDesigner_167");
    Bitmap image21 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.IMPanel.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name21, typeof (IMPanel), typeof (IMPanelWrapper), category4, image21));
    string name22 = LocalizationHolder.rm.GetString("FormDesigner_168");
    Bitmap image22 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.TabControl.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name22, typeof (IMTabControl), typeof (TabControlWrapper), category4, image22));
    string name23 = LocalizationHolder.rm.GetString("FormDesigner.TabPages.Name");
    imToolBoxItemList.Add(new IMToolBoxItem(name23, typeof (System.Windows.Forms.TabPage), typeof (TabPageWrapper), category4));
    string name24 = LocalizationHolder.rm.GetString("FormDesigner_171");
    Bitmap image23 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.Splitter.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name24, typeof (Splitter), typeof (SplitterWrapper), category4, image23));
    string name25 = LocalizationHolder.rm.GetString("FormDesigner_170");
    Bitmap image24 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.PictureBox.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name25, typeof (IMPictureBox), typeof (PictureBoxWrapper), category4, image24));
    string name26 = LocalizationHolder.rm.GetString("FormDesigner_170a");
    Bitmap image25 = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.ToolBoxItemImages.PictureBox.bmp"));
    imToolBoxItemList.Add(new IMToolBoxItem(name26, typeof (IMPreviewBox), typeof (PreviewBoxWrapper), category4, image25));
    this._hash.Add(typeof (DesForm), typeof (DesFormWrapper));
    this._hash4text.Add(typeof (DesForm), LocalizationHolder.rm.GetString("FormDesigner_119"));
    if (this._editorService != null)
      (this._editorService as FormDesignerEditorService).StoreToolBoxItems((object) this, imToolBoxItemList);
    if (this.ToolBoxUpdateEvent != null)
      this.ToolBoxUpdateEvent(this, new FormDesignerControlToolBoxUpdateEventArgs(imToolBoxItemList));
    this.AddToolBoxItems(imToolBoxItemList);
  }

  /// <summary>Добавление ToolBoxItems.</summary>
  /// <param name="items">Список ToolBoxItems</param>
  public void AddToolBoxItems(List<IMToolBoxItem> items)
  {
    foreach (IMToolBoxItem imToolBoxItem in items)
    {
      if (!(imToolBoxItem.ItemType == (System.Type) null) && !(imToolBoxItem.WrapperType == (System.Type) null))
      {
        this._hash.Add(imToolBoxItem.ItemType, imToolBoxItem.WrapperType);
        if (!(imToolBoxItem.WrapperType == typeof (TabPageWrapper)))
        {
          this._lstToolbox.AddToolboxItem((ToolboxItem) imToolBoxItem);
          this._hash4text.Add(imToolBoxItem.ItemType, imToolBoxItem.DisplayName);
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitSurface()
  {
    this.Surface = new DesignSurface();
    this._host = this.Surface.GetService(typeof (IDesignerHost)) as IDesignerHost;
    this._serviceContainer = this.Surface.GetService(typeof (IServiceContainer)) as IServiceContainer;
    this._selectionSrv = this.Surface.GetService(typeof (ISelectionService)) as ISelectionService;
    this._menuCommandService = new MenuCommandService(this._host, this.ReadOnly);
    this._uiSrv = new UIService(this);
    this._serviceContainer.AddService(typeof (INameCreationService), (object) new NameCreationService());
    this._serviceContainer.AddService(typeof (IUIService), (object) this._uiSrv);
    this._serviceContainer.AddService(typeof (IMenuCommandService), (object) this._menuCommandService);
    this._serviceContainer.AddService(typeof (IDesignerSerializationService), (object) new DesignerSerializationService(this._host));
    this._serviceContainer.AddService(typeof (IMessageService), (object) new MessageService());
    this._serviceContainer.AddService(typeof (IFormDesignerEditorHookable), (object) this);
    this._serviceContainer.AddService(typeof (IToolboxService), (object) this._lstToolbox);
    this._serviceContainer.AddService(typeof (IHostView), (object) this);
    this._filter = (IMessageFilter) new KeystrokeMessageFilter(this._host);
    Application.AddMessageFilter(this._filter);
  }

  /// <summary>
  /// 
  /// </summary>
  private void PostInitCommon()
  {
    this._selectionSrv.SelectionChanged += new EventHandler(this.OnSelectionChanged);
    if (!this.ReadOnly)
    {
      this._undoHandler = new UndoHandler();
      this._undoHandler.Attach(this._host);
    }
    IComponentChangeService service = this.Surface.GetService(typeof (IComponentChangeService)) as IComponentChangeService;
    service.ComponentAdded += new ComponentEventHandler(this.OnComponentAdded);
    service.ComponentRemoved += new ComponentEventHandler(this.OnComponentRemoved);
    service.ComponentChanged += new ComponentChangedEventHandler(this.OnComponentChanged);
  }

  public void PrepareToClose() => this.ClearSurface();

  /// <summary>Очистка значений.</summary>
  private void ClearSurface()
  {
    if (this.Surface == null)
      return;
    Application.RemoveMessageFilter(this._filter);
    if (this._serviceContainer != null)
    {
      this._serviceContainer.RemoveService(typeof (IToolboxService));
      this._serviceContainer.RemoveService(typeof (IUIService));
      this._serviceContainer.RemoveService(typeof (IFormDesignerEditorHookable));
      this._serviceContainer.RemoveService(typeof (INameCreationService));
      this._serviceContainer.RemoveService(typeof (IDesignerSerializationService));
      this._serviceContainer.RemoveService(typeof (IMenuCommandService));
      this._serviceContainer.RemoveService(typeof (IMessageService));
      this._serviceContainer.RemoveService(typeof (IHostView));
    }
    if (this._uiSrv != null)
    {
      try
      {
        this._uiSrv.Dispose();
      }
      finally
      {
        this._uiSrv = (UIService) null;
      }
    }
    desForm = (DesForm) null;
    if (this._host != null && this._host.RootComponent is DesForm desForm)
      desForm.DontUseCache = true;
    this._filter = (IMessageFilter) null;
    this._host = (IDesignerHost) null;
    this._serviceContainer = (IServiceContainer) null;
    try
    {
      this.Surface.Dispose();
    }
    catch (Exception ex)
    {
    }
    finally
    {
      this.Surface = (DesignSurface) null;
    }
    if (desForm == null)
      return;
    desForm.DontUseCache = false;
  }

  /// <summary>Загрузка формы.</summary>
  private void LoadForm()
  {
    List<string> selectedComponentNames = this.GetSelectedComponentNames();
    this.ClearSurface();
    this.InitSurface();
    this._label.Text = string.Empty;
    this._lstToolbox._host = this._host;
    this._menuItemForm.MenuCommandSrv = (IMenuCommandService) this._menuCommandService;
    this._fdToolBar.MenuCommandSrv = (IMenuCommandService) this._menuCommandService;
    DesForm form = this.GetForm();
    this._inUndo = true;
    try
    {
      form.FormID = this.FormID;
      form.ControlsLoaded();
      this.LoadFormLinks(form);
      form.TopLevel = false;
      this.View = this.Surface.View as Control;
      this.View.Dock = DockStyle.Fill;
      this.View.Parent = (Control) this._designerPanel;
      this.SetOldSelectedComponents(selectedComponentNames);
    }
    finally
    {
      this._inUndo = false;
    }
    this.Modified = false;
    this.PostInitCommon();
    ICollection components = (ICollection) this._host.Container.Components;
    this._cmbControls.Items.Clear();
    if (components != null)
    {
      foreach (object obj in (IEnumerable) components)
        this._cmbControls.Items.Add(obj);
    }
    this._lstToolbox.Enabled = !this.ReadOnly;
    this._btnAutoplace.Enabled = !this.ReadOnly && !this.IsWorkflowForm;
    this._btnOpen.Enabled = this._btnNewForm.Enabled = !(this.FindForm() is PropertiesWindow) && !this.IsWorkflowForm;
    this.OnSelectionChanged((object) null, (EventArgs) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private DesForm GetForm()
  {
    desForm = (DesForm) null;
    byte[] form = ClientFormsCache.GetForm(this.FormID);
    if (form != null)
    {
      using (MemoryStream memoryStream = new MemoryStream(form))
      {
        try
        {
          if (!(ImXmlReader.Read((Stream) memoryStream, this._host) is DesForm desForm))
          {
            int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("FormDesigner_112"), LocalizationHolder.rm.GetString("FormDesigner_113"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
        }
        catch (Exception ex)
        {
          throw new Exception(LocalizationHolder.rm.GetString("Client.Core_204"), ex);
        }
      }
    }
    return desForm ?? this._host.CreateComponent(typeof (DesForm)) as DesForm;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private List<string> GetSelectedComponentNames()
  {
    List<string> selectedComponentNames = (List<string>) null;
    ICollection selectedComponents = this._selectionSrv != null ? this._selectionSrv.GetSelectedComponents() : (ICollection) null;
    if (selectedComponents != null && selectedComponents.Count > 0)
    {
      selectedComponentNames = new List<string>(selectedComponents.Count);
      foreach (object obj in (IEnumerable) selectedComponents)
      {
        if (obj is Control control)
          selectedComponentNames.Add(control.Name);
      }
    }
    return selectedComponentNames;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="oldSelectedComponentNames"></param>
  private void SetOldSelectedComponents(List<string> oldSelectedComponentNames)
  {
    bool flag = true;
    if (oldSelectedComponentNames != null)
    {
      ICollection components = (ICollection) this._host.Container.Components;
      if (components != null && components.Count > 0)
      {
        List<object> objectList = new List<object>(oldSelectedComponentNames.Count);
        foreach (object obj in (IEnumerable) components)
        {
          if (obj is Control control && oldSelectedComponentNames.Contains(control.Name))
            objectList.Add(obj);
        }
        if (objectList.Count > 0)
        {
          try
          {
            this._selectionSrv.SetSelectedComponents((ICollection) objectList.ToArray());
            flag = false;
          }
          catch
          {
          }
        }
      }
    }
    if (!flag)
      return;
    this._selectionSrv.SetSelectedComponents((ICollection) new object[1]
    {
      (object) this._host.RootComponent
    });
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="renderer"></param>
  private void SetRenderer(IToolBarRenderer renderer) => this._tb.Renderer = renderer;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="form"></param>
  private void LoadFormLinks(DesForm form)
  {
    form.Links = new FormLinks(this.FormID);
    form.Links.Load();
  }

  /// <summary>
  /// 
  /// </summary>
  private void LinkTo()
  {
    DesForm rootComponent = this._host.RootComponent as DesForm;
    using (FormLinksEditorForm formLinksEditorForm = new FormLinksEditorForm(this.ReadOnly))
    {
      formLinksEditorForm.Links = rootComponent.Links;
      if (formLinksEditorForm.ShowDialog() != DialogResult.OK || !formLinksEditorForm.Changed)
        return;
      rootComponent.Links = formLinksEditorForm.Links;
      this.Modified = true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool Undo()
  {
    bool flag = false;
    if (this._undoHandler != null)
    {
      this._undoHandler.Undo();
      this._propertyGrid.Refresh();
      if (this._propertyGrid.SelectedObjects.Length == 0)
        this._dcProperties.Text = this._dcPropertiesText;
      flag = true;
    }
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool Redo()
  {
    bool flag = false;
    if (this._undoHandler != null)
    {
      this._undoHandler.Redo();
      this._propertyGrid.Refresh();
      if (this._propertyGrid.SelectedObjects.Length == 0)
        this._dcProperties.Text = this._dcPropertiesText;
      flag = true;
    }
    return flag;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      ProviderHolder.BarManager.RendererChanged -= new EventHandler(this.OnToolBar_RendererChanged);
      this.SetRenderer((IToolBarRenderer) new EmptyToolbarRenderer());
      this.ClearSurface();
      if (this._editorService != null)
        this._editorService.Remove(this.FormID);
      this._lstToolbox.RemoveAll();
      this._hash.Clear();
      this._hash4text.Clear();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormDesignerControl));
    this._tableLayoutPanel = new TableLayoutPanel();
    this._label = new Label();
    this._toolTip = new ToolTipController(this.components);
    this._panel = new Panel();
    this._tb = new Intermech.Bars.ToolBar();
    this._il = new ImageList(this.components);
    this._btnNewForm = new ButtonItem();
    this._btnOpen = new ButtonItem();
    this._btnAutoplace = new ButtonItem();
    this._btnFormLink = new ButtonItem();
    this._btnPreview = new ButtonItem();
    this._btnUndo = new ButtonItem();
    this._btnRedo = new ButtonItem();
    this.labelItem1 = new LabelItem();
    this._btnCancel = new ButtonItem();
    this._btnOK = new ButtonItem();
    this._designerPanel = new Panel();
    this._leftDock = new DockContainer();
    this._dcToolbox = new DockControl();
    this._lstToolbox = new ToolboxService();
    this._dockManager = new DockManager();
    this._rightDock = new DockContainer();
    this._dcProperties = new DockControl();
    this._propertyGrid = new PropertyGrid();
    this._cmbControls = new ComboBox();
    this._bottomDock = new DockContainer();
    this._topDock = new DockContainer();
    this._tableLayoutPanel.SuspendLayout();
    this._panel.SuspendLayout();
    this._leftDock.SuspendLayout();
    this._dcToolbox.SuspendLayout();
    this._rightDock.SuspendLayout();
    this._dcProperties.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._tableLayoutPanel, "_tableLayoutPanel");
    this._tableLayoutPanel.Controls.Add((Control) this._label, 2, 0);
    this._tableLayoutPanel.Name = "_tableLayoutPanel";
    componentResourceManager.ApplyResources((object) this._label, "_label");
    this._label.MinimumSize = new Size(0, 25);
    this._label.Name = "_label";
    this._toolTip.Style = new ViewStyle("ToolTip style");
    this._panel.Controls.Add((Control) this._tb);
    this._panel.Controls.Add((Control) this._designerPanel);
    componentResourceManager.ApplyResources((object) this._panel, "_panel");
    this._panel.Name = "_panel";
    this._tb.FullMenus = true;
    this._tb.Guid = new Guid("80d2a250-28da-43de-b599-0e5e6195331a");
    this._tb.Hidden = false;
    this._tb.ImageList = this._il;
    this._tb.Items.AddRange(new ToolbarItemBase[10]
    {
      (ToolbarItemBase) this._btnNewForm,
      (ToolbarItemBase) this._btnOpen,
      (ToolbarItemBase) this._btnAutoplace,
      (ToolbarItemBase) this._btnFormLink,
      (ToolbarItemBase) this._btnPreview,
      (ToolbarItemBase) this._btnUndo,
      (ToolbarItemBase) this._btnRedo,
      (ToolbarItemBase) this.labelItem1,
      (ToolbarItemBase) this._btnCancel,
      (ToolbarItemBase) this._btnOK
    });
    componentResourceManager.ApplyResources((object) this._tb, "_tb");
    this._tb.Name = "_tb";
    this._il.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_il.ImageStream");
    this._il.TransparentColor = Color.Magenta;
    this._il.Images.SetKeyName(0, "New.bmp");
    this._il.Images.SetKeyName(1, "Open.bmp");
    this._il.Images.SetKeyName(2, "Master.bmp");
    this._il.Images.SetKeyName(3, "Link.bmp");
    this._il.Images.SetKeyName(4, "Preview.bmp");
    this._il.Images.SetKeyName(5, "Undo.bmp");
    this._il.Images.SetKeyName(6, "Redo.bmp");
    this._il.Images.SetKeyName(7, "Cancel.bmp");
    this._il.Images.SetKeyName(8, "Apply.bmp");
    componentResourceManager.ApplyResources((object) this._btnNewForm, "_btnNewForm");
    this._btnNewForm.ImageIndex = 0;
    this._btnNewForm.Click += new EventHandler(this.On_btnNewForm_Click);
    componentResourceManager.ApplyResources((object) this._btnOpen, "_btnOpen");
    this._btnOpen.ImageIndex = 1;
    this._btnOpen.Click += new EventHandler(this.On_btnOpen_Click);
    this._btnAutoplace.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._btnAutoplace, "_btnAutoplace");
    this._btnAutoplace.ImageIndex = 2;
    this._btnAutoplace.Click += new EventHandler(this.On_btnAutoplace_Click);
    componentResourceManager.ApplyResources((object) this._btnFormLink, "_btnFormLink");
    this._btnFormLink.ImageIndex = 3;
    this._btnFormLink.Click += new EventHandler(this.On_btnFormLink_Click);
    this._btnPreview.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._btnPreview, "_btnPreview");
    this._btnPreview.ImageIndex = 4;
    this._btnPreview.Click += new EventHandler(this.On_btnPreview_Click);
    this._btnUndo.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._btnUndo, "_btnUndo");
    this._btnUndo.Enabled = false;
    this._btnUndo.ImageIndex = 5;
    this._btnUndo.Click += new EventHandler(this.On_btnUndo_ButtonClick);
    componentResourceManager.ApplyResources((object) this._btnRedo, "_btnRedo");
    this._btnRedo.Enabled = false;
    this._btnRedo.ImageIndex = 6;
    this._btnRedo.Click += new EventHandler(this.On_btnRedo_ButtonClick);
    componentResourceManager.ApplyResources((object) this.labelItem1, "labelItem1");
    this.labelItem1.Enabled = false;
    this.labelItem1.Stretch = true;
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.Enabled = false;
    this._btnCancel.ImageIndex = 7;
    this._btnCancel.Click += new EventHandler(this.On_btnCancel_Click);
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.Enabled = false;
    this._btnOK.ImageIndex = 8;
    this._btnOK.Click += new EventHandler(this.On_btnOK_Click);
    this._designerPanel.AllowDrop = true;
    componentResourceManager.ApplyResources((object) this._designerPanel, "_designerPanel");
    this._designerPanel.BackColor = SystemColors.Window;
    this._designerPanel.Name = "_designerPanel";
    this._leftDock.Controls.Add((Control) this._dcToolbox);
    componentResourceManager.ApplyResources((object) this._leftDock, "_leftDock");
    this._leftDock.Guid = new Guid("919e3be1-563f-490b-9572-5dab19f16361");
    this._leftDock.LayoutSystem = new SplitLayoutSystem(250, 400, Orientation.Horizontal, new LayoutSystemBase[1]
    {
      (LayoutSystemBase) new ControlLayoutSystem(245, 665, new DockControl[1]
      {
        this._dcToolbox
      }, this._dcToolbox)
    });
    this._leftDock.Manager = this._dockManager;
    this._leftDock.Name = "_leftDock";
    this._leftDock.Renderer = (RendererBase) null;
    this._dcToolbox.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom | DockLocation.Document;
    componentResourceManager.ApplyResources((object) this._dcToolbox, "_dcToolbox");
    this._dcToolbox.Closable = false;
    this._dcToolbox.Controls.Add((Control) this._lstToolbox);
    this._dcToolbox.ExtraText = (string) null;
    this._dcToolbox.Floatable = false;
    this._dcToolbox.FloatingLocation = new Point(515, 312);
    this._dcToolbox.Guid = new Guid("07bff1bd-f63e-4088-a37a-b77b8cc33ec5");
    this._dcToolbox.HideOnClose = true;
    this._dcToolbox.Name = "_dcToolbox";
    componentResourceManager.ApplyResources((object) this._lstToolbox, "_lstToolbox");
    this._lstToolbox.BackColor = SystemColors.Control;
    this._lstToolbox.Name = "_lstToolbox";
    this._lstToolbox.SelectedCategory = (string) null;
    this._dockManager.OwnerForm = (Form) null;
    this._dockManager.Renderer = (RendererBase) new Intermech.Docking.Rendering.Office2003Renderer();
    this._rightDock.Controls.Add((Control) this._dcProperties);
    componentResourceManager.ApplyResources((object) this._rightDock, "_rightDock");
    this._rightDock.Guid = new Guid("ad0bfc1f-4c50-484d-9f3b-0dab17cebec0");
    this._rightDock.LayoutSystem = new SplitLayoutSystem(250, 400, Orientation.Horizontal, new LayoutSystemBase[1]
    {
      (LayoutSystemBase) new ControlLayoutSystem(250, 665, new DockControl[1]
      {
        this._dcProperties
      }, this._dcProperties)
    });
    this._rightDock.Manager = this._dockManager;
    this._rightDock.Name = "_rightDock";
    this._rightDock.Renderer = (RendererBase) null;
    this._dcProperties.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom | DockLocation.Document;
    componentResourceManager.ApplyResources((object) this._dcProperties, "_dcProperties");
    this._dcProperties.Closable = false;
    this._dcProperties.Controls.Add((Control) this._propertyGrid);
    this._dcProperties.Controls.Add((Control) this._cmbControls);
    this._dcProperties.ExtraText = (string) null;
    this._dcProperties.Floatable = false;
    this._dcProperties.FloatingLocation = new Point(515, 312);
    this._dcProperties.Guid = new Guid("893d5c96-dab1-4941-a7f7-3889b2b6d6a4");
    this._dcProperties.HideOnClose = true;
    this._dcProperties.Name = "_dcProperties";
    this._propertyGrid.Cursor = Cursors.HSplit;
    componentResourceManager.ApplyResources((object) this._propertyGrid, "_propertyGrid");
    this._propertyGrid.LineColor = Color.Silver;
    this._propertyGrid.Name = "_propertyGrid";
    this._propertyGrid.ToolbarVisible = false;
    this._propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.On_propertyGrid_PropertyValueChanged);
    componentResourceManager.ApplyResources((object) this._cmbControls, "_cmbControls");
    this._cmbControls.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cmbControls.FormattingEnabled = true;
    this._cmbControls.Name = "_cmbControls";
    this._cmbControls.Sorted = true;
    this._cmbControls.SelectedIndexChanged += new EventHandler(this.On_cmbControls_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._bottomDock, "_bottomDock");
    this._bottomDock.Guid = new Guid("a1a0f098-6c46-4b00-b9f9-917cc48a14c8");
    this._bottomDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this._bottomDock.Manager = this._dockManager;
    this._bottomDock.Name = "_bottomDock";
    this._bottomDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this._topDock, "_topDock");
    this._topDock.Guid = new Guid("53cd7958-1170-48f4-aeb2-0a768e48deae");
    this._topDock.LayoutSystem = new SplitLayoutSystem(250, 400);
    this._topDock.Manager = this._dockManager;
    this._topDock.Name = "_topDock";
    this._topDock.Renderer = (RendererBase) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._panel);
    this.Controls.Add((Control) this._leftDock);
    this.Controls.Add((Control) this._rightDock);
    this.Controls.Add((Control) this._bottomDock);
    this.Controls.Add((Control) this._topDock);
    this.Controls.Add((Control) this._tableLayoutPanel);
    this.DoubleBuffered = true;
    this.Name = nameof (FormDesignerControl);
    this._tableLayoutPanel.ResumeLayout(false);
    this._panel.ResumeLayout(false);
    this._leftDock.ResumeLayout(false);
    this._leftDock.PerformLayout();
    this._dcToolbox.ResumeLayout(false);
    this._rightDock.ResumeLayout(false);
    this._rightDock.PerformLayout();
    this._dcProperties.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
