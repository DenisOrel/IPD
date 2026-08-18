
// Type: Intermech.Client.Core.ObjectCreator.ObjectCreatorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Commands;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Projects;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Tools.Integrators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.ObjectCreator;

/// <summary>Класс диалога мастера создания объектов.</summary>
public class ObjectCreatorForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  /// <summary>Required designer variable.</summary>
  private Panel _pnlBottom;
  private Button _btnCancel;
  private Button _btnFinish;
  private Button _btnNext;
  private Button _btnPrev;
  private ContextMenu _contextMenu;
  private MenuItem _miAdd;
  private MenuItem _miDel;
  private MenuItem _miDelAll;
  private Label _lb;
  private PictureBox _pictBox;
  private Panel _pnlTop;
  private CheckBox _chb;
  private Panel _pnlChb;
  private Panel _pnlButton;
  private Button _btnSkip;
  private ImageList _imgList;
  private Panel _pnlCtrls;
  private CheckBox cbOpenInNewWindow;
  private CreatedObjectItem _createdObject;
  internal IObjectCreatorRiderCustomService CustomService;
  internal ArrayList CreatorSteps = new ArrayList();
  private int _fixedStepsCount;
  private int _currStep;
  private UserControl _currCtrl;
  private bool _useClassifier;
  /// <summary>
  /// Максимальный размер иконок которые пытаться загрузить для этого типа (баг 334321)
  /// </summary>
  public const int MaxIconWidth = 64 /*0x40*/;
  /// <summary>
  /// Необходимость обновить форму(ы) создаваемого объекта.
  /// Флаг введен для того, чтобы сразу после создания формы она не обновлялась, т.е. не выполнялись дважды одни  те же действия
  /// </summary>
  private bool _needRefreshForm = true;
  private readonly string _configSection = "OBJECT_CREATOR";
  private readonly string _configParamTemplate = "CB{0}";

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.CreatorSteps != null && this.CreatorSteps.Count > 0)
      {
        for (int index = 0; index < this.CreatorSteps.Count; ++index)
        {
          if (this.CreatorSteps[index] is UserControl creatorStep)
            creatorStep.Dispose();
        }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectCreatorForm));
    this._pnlBottom = new Panel();
    this._pnlButton = new Panel();
    this._btnSkip = new Button();
    this._btnFinish = new Button();
    this._btnCancel = new Button();
    this._btnPrev = new Button();
    this._btnNext = new Button();
    this._pnlChb = new Panel();
    this.cbOpenInNewWindow = new CheckBox();
    this._chb = new CheckBox();
    this._contextMenu = new ContextMenu();
    this._miAdd = new MenuItem();
    this._miDel = new MenuItem();
    this._miDelAll = new MenuItem();
    this._imgList = new ImageList(this.components);
    this._pnlTop = new Panel();
    this._lb = new Label();
    this._pictBox = new PictureBox();
    this._pnlCtrls = new Panel();
    this._pnlBottom.SuspendLayout();
    this._pnlButton.SuspendLayout();
    this._pnlChb.SuspendLayout();
    this._pnlTop.SuspendLayout();
    ((ISupportInitialize) this._pictBox).BeginInit();
    this.SuspendLayout();
    this._pnlBottom.Controls.Add((Control) this._pnlButton);
    this._pnlBottom.Controls.Add((Control) this._pnlChb);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    this._pnlButton.Controls.Add((Control) this._btnSkip);
    this._pnlButton.Controls.Add((Control) this._btnFinish);
    this._pnlButton.Controls.Add((Control) this._btnCancel);
    this._pnlButton.Controls.Add((Control) this._btnPrev);
    this._pnlButton.Controls.Add((Control) this._btnNext);
    componentResourceManager.ApplyResources((object) this._pnlButton, "_pnlButton");
    this._pnlButton.Name = "_pnlButton";
    componentResourceManager.ApplyResources((object) this._btnSkip, "_btnSkip");
    this._btnSkip.Name = "_btnSkip";
    this._btnSkip.Click += new EventHandler(this.On_btnSkip_Click);
    componentResourceManager.ApplyResources((object) this._btnFinish, "_btnFinish");
    this._btnFinish.Name = "_btnFinish";
    this._btnFinish.Click += new EventHandler(this.On_btnFinish_Click);
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.Click += new EventHandler(this._btnCancel_Click);
    componentResourceManager.ApplyResources((object) this._btnPrev, "_btnPrev");
    this._btnPrev.Name = "_btnPrev";
    this._btnPrev.Click += new EventHandler(this.On_btnPrev_Click);
    componentResourceManager.ApplyResources((object) this._btnNext, "_btnNext");
    this._btnNext.Name = "_btnNext";
    this._btnNext.Click += new EventHandler(this.On_btnNext_Click);
    this._pnlChb.Controls.Add((Control) this.cbOpenInNewWindow);
    this._pnlChb.Controls.Add((Control) this._chb);
    componentResourceManager.ApplyResources((object) this._pnlChb, "_pnlChb");
    this._pnlChb.Name = "_pnlChb";
    componentResourceManager.ApplyResources((object) this.cbOpenInNewWindow, "cbOpenInNewWindow");
    this.cbOpenInNewWindow.Name = "cbOpenInNewWindow";
    this.cbOpenInNewWindow.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._chb, "_chb");
    this._chb.Name = "_chb";
    this._chb.UseVisualStyleBackColor = true;
    this._contextMenu.MenuItems.AddRange(new MenuItem[3]
    {
      this._miAdd,
      this._miDel,
      this._miDelAll
    });
    this._miAdd.Index = 0;
    componentResourceManager.ApplyResources((object) this._miAdd, "_miAdd");
    componentResourceManager.ApplyResources((object) this._miDel, "_miDel");
    this._miDel.Index = 1;
    componentResourceManager.ApplyResources((object) this._miDelAll, "_miDelAll");
    this._miDelAll.Index = 2;
    this._imgList.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this._imgList, "_imgList");
    this._imgList.TransparentColor = Color.Transparent;
    this._pnlTop.Controls.Add((Control) this._lb);
    this._pnlTop.Controls.Add((Control) this._pictBox);
    componentResourceManager.ApplyResources((object) this._pnlTop, "_pnlTop");
    this._pnlTop.Name = "_pnlTop";
    componentResourceManager.ApplyResources((object) this._lb, "_lb");
    this._lb.ForeColor = SystemColors.GrayText;
    this._lb.Name = "_lb";
    componentResourceManager.ApplyResources((object) this._pictBox, "_pictBox");
    this._pictBox.Name = "_pictBox";
    this._pictBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this._pnlCtrls, "_pnlCtrls");
    this._pnlCtrls.Name = "_pnlCtrls";
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this._btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this._pnlCtrls);
    this.Controls.Add((Control) this._pnlBottom);
    this.Controls.Add((Control) this._pnlTop);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ObjectCreatorForm);
    this.HelpButtonClicked += new CancelEventHandler(this.ObjectCreatorForm_HelpButtonClicked);
    this.FormClosing += new FormClosingEventHandler(this.ObjectCreatorForm_FormClosing);
    this.HelpRequested += new HelpEventHandler(this.ObjectCreatorForm_HelpRequested);
    this.KeyDown += new KeyEventHandler(this.ObjectCreatorForm_KeyDown);
    this._pnlBottom.ResumeLayout(false);
    this._pnlButton.ResumeLayout(false);
    this._pnlChb.ResumeLayout(false);
    this._pnlChb.PerformLayout();
    this._pnlTop.ResumeLayout(false);
    ((ISupportInitialize) this._pictBox).EndInit();
    this.ResumeLayout(false);
  }

  internal CreatedObjectItem CreatedObject => this._createdObject;

  public bool RunEditorAfterCreate => this._chb.Checked;

  internal bool OpenInNewWindowAfterCreate => this.cbOpenInNewWindow.Checked;

  /// <summary>Идентификатор объекта-заготовки.</summary>
  internal long CreatedObjectID => this._createdObject.ObjectID;

  internal void OpenInNewWindowVisible(bool visible) => this.cbOpenInNewWindow.Visible = visible;

  public Button NextButton => this._btnNext;

  public Button PreviousButton => this._btnPrev;

  public Button SkipButton => this._btnSkip;

  public Button FinishButton => this._btnFinish;

  public ObjectCreatorControl CurrentObjectCreatorControl => this._currCtrl as ObjectCreatorControl;

  public void DisableOpenEditor() => this._chb.Enabled = false;

  /// <summary>Конструктор.</summary>
  /// <param name="objectCreator"></param>
  public ObjectCreatorForm(Intermech.Client.Core.ObjectCreator.ObjectCreator objectCreator)
  {
    this.InitializeComponent();
    this._createdObject = new CreatedObjectItem(objectCreator);
    this._createdObject.AfterCommitCreationEvent += new CreatedObjectItem.AfterCommitCreation(this.CreatedObject_OnCommitCreationEvent);
    this._createdObject.BeforeCommitCreationEvent += new CreatedObjectItem.BeforeCommitCreation(this.CreatedObject_BeforeCommitCreationEvent);
    this._createdObject.OnCancelCreationEvent += new CreatedObjectItem.OnCancelCreation(this.CreatedObject_OnCancelCreationEvent);
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>
  /// Завершение мастера создания нового объекта и подтверждение создания экземпляра.
  /// </summary>
  private void On_btnFinish_Click(object sender, EventArgs e)
  {
    bool needClose;
    if (!this.FinishStep(out needClose))
    {
      if (!needClose)
        return;
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }
    else
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
  }

  /// <summary>Переход к следующему шагу мастера создания объектов.</summary>
  private void On_btnNext_Click(object sender, EventArgs e) => this.NextStep();

  /// <summary>Переход к предыдущему шагу мастера создания объектов.</summary>
  private void On_btnPrev_Click(object sender, EventArgs e) => this.PrevStep();

  private void On_btnSkip_Click(object sender, EventArgs e)
  {
    ++this._currStep;
    this.UpdateStepControls();
  }

  private bool CreatedObject_OnCancelCreationEvent(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    return this.CustomService == null || this.CustomService.OnCancelAction(session, newObjectID, nea);
  }

  private bool CreatedObject_OnCommitCreationEvent(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IObjectTemplater objectTemplater = sessionKeeper.Session.GetObject(newObjectID) as IObjectTemplater;
      Dictionary<int, List<CreatedProjectData>> dictionary = new Dictionary<int, List<CreatedProjectData>>();
      foreach (object creatorStep in this.CreatorSteps)
      {
        if (creatorStep is ProjectCreatorControl projectCreatorControl)
        {
          if (projectCreatorControl.TemplateId != 0L)
          {
            dictionary = objectTemplater.AddTemplateObjects(projectCreatorControl.ListOfCreatedObjectsID, projectCreatorControl.TemplateId);
          }
          else
          {
            IDBAttribute attributeByGuid = sessionKeeper.Session.GetObject(newObjectID).GetAttributeByGuid(new Guid("cad00815-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid != null && !attributeByGuid.IsNull)
            {
              long TemplateID = long.Parse(attributeByGuid.Value.ToString());
              dictionary = objectTemplater.AddTemplateObjects(TemplateID);
            }
          }
        }
      }
      foreach (KeyValuePair<int, List<CreatedProjectData>> keyValuePair in dictionary)
      {
        IntegratorObject iobj = IntegratorServices.Find(keyValuePair.Key);
        if (iobj != null)
        {
          IIntegrator integrator = ClientContext.Integrators.GetIntegrator(iobj, false);
          if (integrator != null)
          {
            IEmbedAttributesService service = ServiceUtils.GetService<IEmbedAttributesService>((object) integrator, false);
            if (service != null)
            {
              foreach (CreatedProjectData createdProjectData in keyValuePair.Value)
              {
                long objectId = sessionKeeper.Session.CheckOutCommand(createdProjectData.ObjectID);
                try
                {
                  service.EmbedAttributeValues(objectId, (IList<AttributeValues>) createdProjectData.AttributeValues);
                }
                finally
                {
                  ObjectCopyCommand copyCommandByName = ObjectCommandFactory.CreateObjectCopyCommandByName("Checkin", true);
                  copyCommandByName.ObjectId = objectId;
                  ServiceContainer serviceContainer = new ServiceContainer();
                  serviceContainer.AddService(typeof (ExtendedSaveOptions), (object) new ExtendedSaveOptions(SaveChangesMode.Checkin));
                  copyCommandByName.ContextServices = (System.IServiceProvider) serviceContainer;
                  copyCommandByName.Execute();
                }
              }
            }
          }
        }
      }
    }
    return this.CustomService == null || this.CustomService.OnCommitAction(session, newObjectID, nea);
  }

  private bool CreatedObject_BeforeCommitCreationEvent(IUserSession session, IDBObject newObject)
  {
    return this.CustomService == null || this.CustomService.OnBeforeCommitAction(session, newObject);
  }

  private void ObjectCreatorForm_SetButtonEnabledEvent(ButtonType type, bool enabled)
  {
    this.Invoke((Delegate) new SetButtonEnabledHandler(this.OnSetButtonEnabledEvent), (object) type, (object) enabled);
  }

  /// <summary>Изменение  доступности кнопок</summary>
  /// <param name="type"></param>
  /// <param name="enabled"></param>
  private void OnSetButtonEnabledEvent(ButtonType type, bool enabled)
  {
    if ((type & ButtonType.Next) == ButtonType.Next)
      this._btnNext.Enabled = enabled;
    if ((type & ButtonType.Back) == ButtonType.Back)
      this._btnPrev.Enabled = enabled;
    if ((type & ButtonType.Finish) != ButtonType.Finish)
      return;
    this._btnFinish.Enabled = this.IsFinishReady();
  }

  private void ObjectCreatorForm_StepCompetedHandlerEvent(ButtonType type)
  {
    this.Invoke((Delegate) new StepCompletedHandler(this.OnStepCompletedEvent), (object) type);
  }

  private void OnStepCompletedEvent(ButtonType type)
  {
    switch (type)
    {
      case ButtonType.Next:
        this.NextStep();
        break;
      case ButtonType.Back:
        this.PrevStep();
        break;
      case ButtonType.Finish:
        this.On_btnFinish_Click((object) this, (EventArgs) null);
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  internal void CreateClassifierControl()
  {
    if (!this.IsClassified())
      return;
    ObjectClassifierControl classifierControl = new ObjectClassifierControl(this._createdObject);
    classifierControl.SetButtonEnabledEvent += new SetButtonEnabledHandler(this.ObjectCreatorForm_SetButtonEnabledEvent);
    this.CreatorSteps.Add((object) classifierControl);
  }

  internal void CreateFileRenamedControl(long aTemplateObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(aTemplateObjectID, sessionKeeper.Session.IdentHelper.FileAttributeID);
      bool flag = false;
      if (objectAttributeById != null)
      {
        for (int index = 0; index < objectAttributeById.ValuesCount; ++index)
        {
          objectAttributeById.Index = index;
          if (!objectAttributeById.IsNull)
          {
            flag = true;
            break;
          }
        }
      }
      if (!flag)
        return;
      this.CreatorSteps.Insert(0, (object) new FilesRenameControl(this.CreatedObject));
    }
  }

  internal void CreateFileAttributesControl()
  {
    if (!this._createdObject.FileAttrs.Contains)
      return;
    this.CreatorSteps.Add((object) new ObjectFileAttributesControl(this._createdObject));
  }

  internal void CreateObjectWithRelations(
    int objTypeID,
    long templateObjID,
    ObjectRelationLink[] objRelations,
    DateTime startDate,
    bool isVersion)
  {
    this.Text = isVersion ? LocalizationHolder.rm.GetString("Client.Core_871") : LocalizationHolder.rm.GetString("Client.Core_537");
    this.LoadCheckBoxesState(objTypeID);
    this._createdObject.ObjectTypeID = objTypeID;
    this._createdObject.CreateRelationDate = startDate;
    this._createdObject.Create(templateObjID, isVersion);
    if (this._createdObject.ObjectID == -1L)
      return;
    this._createdObject.ObjectRelationArray.Clear();
    if (objRelations == null)
      return;
    this._createdObject.ObjectRelationArray.AddRange((IEnumerable<ObjectRelationLink>) objRelations);
    this._createdObject.EntersInCreate();
  }

  internal void CreatePropertiesControl()
  {
    ObjectPropertiesControl propertiesControl = new ObjectPropertiesControl(this._createdObject)
    {
      blankMode = true
    };
    propertiesControl.PropertyValueChangedEvent += new PropertyValueChangedHendler(this.TemplateAttributeChange);
    propertiesControl.GridChangedEvent += new GridChangedHandler(this.TemplateAttributeAddOrRemove);
    this.CreatorSteps.Add((object) propertiesControl);
  }

  private void TemplateAttributeAddOrRemove(object sender, GridChangedEventArgs e)
  {
    if (!(this.CreatorSteps[this._currStep] is ObjectPropertiesControl creatorStep1))
      return;
    creatorStep1.Save(new PageSaveArgs(-1));
    creatorStep1.Refresh(new PageRefreshArgs());
    foreach (object creatorStep2 in this.CreatorSteps)
    {
      if (creatorStep2 is ProjectCreatorControl projectCreatorControl)
        projectCreatorControl.Refresh(new PageRefreshArgs());
    }
    if (this.CreatorSteps.Count != this._currStep + 2 || !(this.CreatorSteps[this._currStep + 1] is ProjectCreatorControl))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttribute = sessionKeeper.Session.GetObjectAttribute(this.CreatedObjectID, (object) new Guid("cad00815-306c-11d8-b4e9-00304f19f545"), false, false);
      if (objectAttribute == null || objectAttribute.IsNull || long.Parse(objectAttribute.Value.ToString()) == 0L)
        this._btnNext.Enabled = false;
      else
        this._btnNext.Enabled = true;
    }
  }

  /// <summary>На странице свойств поменялся атрибут с шаблоном</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TemplateAttributeChange(object sender, PropertyValueChangedEventArgs e)
  {
    if (!(this.CreatorSteps[this._currStep] is ObjectPropertiesControl creatorStep))
      return;
    string name = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(new Guid("cad00815-306c-11d8-b4e9-00304f19f545"), true).Name;
    if (!e.ChangedItem.PropertyDescriptor.Name.Equals(name))
      return;
    creatorStep.Save(new PageSaveArgs(-1));
    creatorStep.Refresh(new PageRefreshArgs());
    if (this.CreatorSteps.Count != this._currStep + 2 || !(this.CreatorSteps[this._currStep + 1] is ProjectCreatorControl))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttribute = sessionKeeper.Session.GetObjectAttribute(this.CreatedObjectID, (object) new Guid("cad00815-306c-11d8-b4e9-00304f19f545"), false, false);
      if (objectAttribute == null || objectAttribute.IsNull || long.Parse(objectAttribute.Value.ToString()) == 0L)
        this._btnNext.Enabled = false;
      else
        this._btnNext.Enabled = true;
    }
  }

  /// <summary>
  /// Добавляем контрол создания состава по шаблону.
  /// Всегда должен идти где-то после страницы свойств,
  /// т.к. на странице свойств можно добавить атрибут шаблона и от этого будет зависеть, пропускаем мы этот шаг или нет
  /// </summary>
  internal void CreateTemplateControl()
  {
    this.CreatorSteps.Add((object) new ProjectCreatorControl(this._createdObject));
  }

  internal void CreateRelationsControl()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, this._createdObject.ObjectTypeID, -1).Rows.Count == 0)
        return;
      this.CreatorSteps.Add((object) new ObjectRelationsControl(this._createdObject));
    }
  }

  /// <summary>Создание контролов для шагов мастера.</summary>
  internal void CreateStepControlsByDefault()
  {
    AdjustableViews service = ServicesManager.GetService<AdjustableViews>();
    this.CreateClassifierControl();
    if (service.Find((Predicate<AdjustableView>) (x => x.Name.Equals("ObjectProperties"))).Visible)
      this.CreatePropertiesControl();
    this.CreateTemplateControl();
    this.CreateFileAttributesControl();
    if (!service.Find((Predicate<AdjustableView>) (x => x.Name.Equals("RelationProperties"))).Visible)
      return;
    this.CreateRelationsControl();
  }

  /// <summary>
  /// 
  /// </summary>
  internal void FinallyUpdateTabs()
  {
    this._fixedStepsCount = this.CreatorSteps != null ? this.CreatorSteps.Count : 0;
    this.UpdateCustomFormsTabs((object) null);
    int num = 1;
    if (this._createdObject.ObjectTypeImage != null)
      num = this._createdObject.ObjectTypeImage.Width / this._createdObject.ObjectTypeImage.Height;
    this._pictBox.Width = 32 /*0x20*/ * num;
    this._pictBox.Height = 32 /*0x20*/;
    this._pictBox.Image = this._createdObject.ObjectTypeImage;
    if (num > 1)
      this._lb.Left += 32 /*0x20*/ * (num - 1);
    this._lb.Text = this._createdObject.ObjectTypeCaption;
    if (this.CreatorSteps == null || this.CreatorSteps.Count <= 0)
      return;
    Size size1 = this._pnlCtrls.Size;
    Size size2 = new Size(0, 0);
    Size minimumSize;
    for (int index = 0; index < this.CreatorSteps.Count; ++index)
    {
      if (this.CreatorSteps[index] is Control creatorStep)
      {
        if (creatorStep.Width > size1.Width)
          size1.Width = creatorStep.Width;
        if (creatorStep.Height > size1.Height)
          size1.Height = creatorStep.Height;
        minimumSize = creatorStep.MinimumSize;
        if (minimumSize.Width > size2.Width)
        {
          ref Size local = ref size2;
          minimumSize = creatorStep.MinimumSize;
          int width = minimumSize.Width;
          local.Width = width;
        }
        minimumSize = creatorStep.MinimumSize;
        if (minimumSize.Height > size2.Height)
        {
          ref Size local = ref size2;
          minimumSize = creatorStep.MinimumSize;
          int height = minimumSize.Height;
          local.Height = height;
        }
      }
    }
    if (!size1.Equals((object) this._pnlCtrls.Size))
    {
      this.Height += size1.Height - this._pnlCtrls.Height;
      this.Width += size1.Width - this._pnlCtrls.Width;
    }
    size2.Width = this.Width - this._pnlCtrls.Width + size2.Width;
    minimumSize = this.MinimumSize;
    if (minimumSize.Width > size2.Width)
    {
      ref Size local = ref size2;
      minimumSize = this.MinimumSize;
      int width = minimumSize.Width;
      local.Width = width;
    }
    size2.Height = this.Height - this._pnlCtrls.Height + size2.Height;
    minimumSize = this.MinimumSize;
    if (minimumSize.Height > size2.Height)
    {
      ref Size local = ref size2;
      minimumSize = this.MinimumSize;
      int height = minimumSize.Height;
      local.Height = height;
    }
    this.MinimumSize = size2;
  }

  /// <summary>For creating second step.</summary>
  /// <param name="objTypeID">Идентификатор ипа объекта, по которому будет создан новый экземпляр объекта</param>
  /// <param name="templateObjID">Идентификатор, который задает объект-прототип для создаваемого экземпляра</param>
  /// <param name="objRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="startDate">Дата с которой начинают действовать созданные связи</param>
  /// <param name="isVersion">Признак того, что надо создавать не новый объект, а версию объекта</param>
  internal void SetNewObjectWithRelations(
    int objTypeID,
    long templateObjID,
    ObjectRelationLink[] objRelations,
    DateTime startDate,
    bool isVersion)
  {
    this.CreateObjectWithRelations(objTypeID, templateObjID, objRelations, startDate, isVersion);
    if (this._createdObject.ObjectID == -1L)
      this.Close();
    this.CreateStepControlsByDefault();
  }

  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    Intermech.Client.Core.ObjectCreator.ObjectCreator.SaveSettings((Form) this);
    if (this.DialogResult != DialogResult.Cancel)
      return;
    this._createdObject.Cancel();
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    Intermech.Client.Core.ObjectCreator.ObjectCreator.LoadSettings((Form) this, true);
  }

  private void ObjectCreatorForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this._createdObject != null)
      this.SaveCheckBoxesState(this._createdObject.ObjectTypeID);
    FormStorage.SaveLayout((Control) this);
  }

  private void LoadCheckBoxesState(int createdObjectType)
  {
    long num = (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadInteger("CLIENT", this._configSection, string.Format(this._configParamTemplate, (object) createdObjectType), 0L, DBConfigMode.UserOnly);
    if (num <= 0L)
      return;
    ObjectCreatorForm.FormCheckedCheckBoxes checkedCheckBoxes = (ObjectCreatorForm.FormCheckedCheckBoxes) num;
    this._chb.Checked = (checkedCheckBoxes & ObjectCreatorForm.FormCheckedCheckBoxes.cbOpenEditor) == ObjectCreatorForm.FormCheckedCheckBoxes.cbOpenEditor;
    this.cbOpenInNewWindow.Checked = (checkedCheckBoxes & ObjectCreatorForm.FormCheckedCheckBoxes.cbOpenInNewWindow) == ObjectCreatorForm.FormCheckedCheckBoxes.cbOpenInNewWindow;
  }

  private void SaveCheckBoxesState(int createdObjectType)
  {
    ObjectCreatorForm.FormCheckedCheckBoxes checkedCheckBoxes = ObjectCreatorForm.FormCheckedCheckBoxes.None;
    if (this._chb.Checked)
      checkedCheckBoxes |= ObjectCreatorForm.FormCheckedCheckBoxes.cbOpenEditor;
    if (this.cbOpenInNewWindow.Checked)
      checkedCheckBoxes |= ObjectCreatorForm.FormCheckedCheckBoxes.cbOpenInNewWindow;
    (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).WriteInteger("CLIENT", this._configSection, string.Format(this._configParamTemplate, (object) createdObjectType), (long) checkedCheckBoxes);
  }

  /// <summary>Завершение мастера.</summary>
  /// <returns>Если завершение прошло успешно возвращается true, иначе false</returns>
  private bool FinishStep(out bool needClose)
  {
    int nextStepIndex = -1;
    needClose = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<NotificationEventArgs> nea = (List<NotificationEventArgs>) null;
      List<UserControl> userControlList1 = new List<UserControl>();
      List<UserControl> userControlList2 = new List<UserControl>();
      foreach (UserControl creatorStep in this.CreatorSteps)
      {
        if (creatorStep is IObjectCreator objectCreator && objectCreator.SaveAfterCommitCreation)
          userControlList2.Add(creatorStep);
        if (creatorStep is FormDesignerView)
        {
          if (creatorStep == this._currCtrl)
          {
            FormDesignerView formDesignerView = (FormDesignerView) creatorStep;
            if (formDesignerView.FormChanged)
            {
              string errorMsg = string.Empty;
              try
              {
                if (!formDesignerView.SaveForm(out errorMsg))
                {
                  int num = (int) MessageBox.Show(errorMsg, LocalizationHolder.rm.GetString("Client.Core_875"));
                  return false;
                }
              }
              catch
              {
                throw;
              }
            }
          }
        }
        else if (creatorStep is ObjectCreatorControl && (creatorStep == this._currCtrl || (creatorStep as ObjectCreatorControl).NeedSaveWhenNotVisible))
        {
          if ((creatorStep as ObjectCreatorControl).SaveInTransaction)
            userControlList1.Add(creatorStep);
          else if (!this.SaveObjectCreatorControl((ObjectCreatorControl) creatorStep, nextStepIndex))
            return false;
        }
      }
      bool flag = false;
      if (userControlList1.Count > 0)
      {
        IDBTransactions customService = (IDBTransactions) sessionKeeper.Session.GetCustomService(typeof (IDBTransactions));
        customService.StartTransaction();
        needClose = true;
        try
        {
          for (int index = 0; index < userControlList1.Count; ++index)
          {
            if (!this.SaveObjectCreatorControl((ObjectCreatorControl) userControlList1[index], nextStepIndex))
            {
              customService.Rollback();
              return false;
            }
          }
          if (!this._createdObject.Commit(out nea))
          {
            customService.Rollback();
            this.CreatedObject.FileAttrs.Unassign();
            int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_879"), LocalizationHolder.rm.GetString("Client.Core_880"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return false;
          }
          if (userControlList2.Count > 0)
          {
            try
            {
              for (int index = 0; index < userControlList2.Count; ++index)
              {
                if (!((IObjectCreator) userControlList2[index]).SaveAfterCommit(sessionKeeper.Session, this._createdObject.ObjectID))
                {
                  customService.Rollback();
                  return false;
                }
              }
            }
            catch (Exception ex)
            {
              customService.Rollback();
              int num = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("Client.Core_880"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
              return false;
            }
          }
          customService.Commit();
          this.FireOnCreateEvents(nea);
          return true;
        }
        catch
        {
          customService.Rollback();
          this.CreatedObject.FileAttrs.Unassign();
          throw;
        }
      }
      else
      {
        if (!flag)
        {
          IDBTransactions customService = (IDBTransactions) sessionKeeper.Session.GetCustomService(typeof (IDBTransactions));
          customService.StartTransaction();
          try
          {
            if (!this._createdObject.Commit(out nea))
            {
              customService.Rollback();
              this.CreatedObject.FileAttrs.Unassign();
              int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_879"), LocalizationHolder.rm.GetString("Client.Core_880"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
              return false;
            }
            customService.Commit();
          }
          catch
          {
            customService.Rollback();
            throw;
          }
          for (int index = 0; index < userControlList2.Count; ++index)
            ((IObjectCreator) userControlList2[index]).SaveAfterCommit(sessionKeeper.Session, this._createdObject.ObjectID);
        }
        this.FireOnCreateEvents(nea);
        return true;
      }
    }
  }

  private void FireOnCreateEvents(List<NotificationEventArgs> nea)
  {
    INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (nea == null || nea.Count <= 0 || service == null)
      return;
    foreach (NotificationEventArgs e in nea)
      service.FireEvent((object) null, e);
  }

  private bool SaveObjectCreatorControl(ObjectCreatorControl control, int nextStepIndex)
  {
    PageSaveArgs args = new PageSaveArgs(nextStepIndex)
    {
      currControl = this._currCtrl
    };
    if (control.Save(args))
      return true;
    if (args.Error != null)
    {
      ExceptionHelper.ExceptionService.ShowException(args.Error);
      return false;
    }
    if (args.errorType == ErrorType.CheckNotCompleted)
    {
      this.StartCurrentCheck();
    }
    else
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_876"));
    }
    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool IsClassified()
  {
    if (this._createdObject.IsVersion)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject containerForObjectType = (sessionKeeper.Session.GetCustomService(typeof (IContainerService)) as IContainerService).GetContainerForObjectType((object) sessionKeeper.Session.SessionGUID, this._createdObject.ObjectTypeID);
      if (containerForObjectType == null)
        return false;
      IDBAttribute attributeByGuid = containerForObjectType.GetAttributeByGuid(new Guid("cad001d9-306c-11d8-b4e9-00304f19f545"));
      return attributeByGuid != null && Convert.ToInt32(attributeByGuid.Value) > 0;
    }
  }

  /// <summary>
  /// Проход по всем предопределенным шагам мастера и проверка возможности завершения мастера.
  /// </summary>
  /// <returns>Можно ли завершить создание объекта</returns>
  private bool IsFinishReady()
  {
    foreach (object creatorStep in this.CreatorSteps)
    {
      if (creatorStep is ObjectCreatorControl)
      {
        ObjectCreatorControl objectCreatorControl = (ObjectCreatorControl) creatorStep;
        if (objectCreatorControl.StepIsReadyCheckRequired && !objectCreatorControl.StepIsReady)
          return false;
      }
    }
    return true;
  }

  /// <summary>Переход на следующий шаг мастера.</summary>
  private void NextStep()
  {
    if (this._currStep == this.CreatorSteps.Count - 1)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_877"));
    }
    else
    {
      if (!this.SaveStepControl(this._currStep + 1))
        return;
      if (this.CreatorSteps[this._currStep] is ObjectClassifierControl)
        this.UpdateCustomFormsTabs((object) null);
      if (this.CreatorSteps[this._currStep] is IStepRefreshManager creatorStep1)
        this._needRefreshForm = creatorStep1.RefreshOnNextStep || this._needRefreshForm;
      if (this._currStep + 1 < this.CreatorSteps.Count && this.CreatorSteps[this._currStep + 1] is ProjectCreatorControl creatorStep2)
      {
        creatorStep2.Refresh(new PageRefreshArgs());
        if (creatorStep2.TemplateId == 0L)
          ++this._currStep;
      }
      ++this._currStep;
      this.UpdateStepControls();
    }
  }

  /// <summary>Переход на предыдущий шаг мастера.</summary>
  private void PrevStep()
  {
    if (this._currStep == 0)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_878"));
    }
    else
    {
      if (!this.SaveStepControl(this._currStep - 1))
        return;
      if (this.CreatorSteps[this._currStep] is IStepRefreshManager creatorStep1)
        this._needRefreshForm = creatorStep1.RefreshOnPrevStep || this._needRefreshForm;
      if (this.CreatorSteps[this._currStep - 1] is ProjectCreatorControl creatorStep2)
      {
        creatorStep2.Refresh(new PageRefreshArgs());
        if (creatorStep2.TemplateId == 0L)
          --this._currStep;
      }
      --this._currStep;
      this.UpdateStepControls();
    }
  }

  private void StartCurrentCheck()
  {
    if (!(this.CreatorSteps[this._currStep] is ObjectCreatorControl creatorStep))
      return;
    creatorStep.StartErrorCheck();
  }

  /// <summary>
  /// Сохранение данных на текущем шаге мастера создания объектов.
  /// </summary>
  /// <returns>Если сохранение прошло успешно возвращается true, иначе false</returns>
  private bool SaveStepControl(int nextSetepIndex)
  {
    bool flag = true;
    UserControl creatorStep = (UserControl) this.CreatorSteps[this._currStep];
    if (this._currCtrl != null)
    {
      if (this._currCtrl is FormDesignerView)
      {
        FormDesignerView formDesignerView = (FormDesignerView) creatorStep;
        if (formDesignerView.FormChanged)
        {
          string errorMsg = string.Empty;
          flag = formDesignerView.SaveForm(out errorMsg);
          if (!flag)
          {
            int num = (int) MessageBox.Show(errorMsg, LocalizationHolder.rm.GetString("Client.Core_875"));
          }
        }
      }
      else if (this._currCtrl is ObjectCreatorControl)
      {
        ObjectCreatorControl objectCreatorControl = (ObjectCreatorControl) creatorStep;
        PageSaveArgs pageSaveArgs = new PageSaveArgs(nextSetepIndex);
        PageSaveArgs args = pageSaveArgs;
        flag = objectCreatorControl.Save(args);
        if (!flag)
        {
          if (pageSaveArgs.Error != null)
            throw pageSaveArgs.Error;
          if (pageSaveArgs.errorType == ErrorType.CheckNotCompleted)
          {
            this.StartCurrentCheck();
          }
          else
          {
            int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_876"));
          }
        }
      }
    }
    return flag;
  }

  /// <summary>
  /// Обновление набора закладок для имеющихся форм редактирования у данного типа объектов.
  /// </summary>
  /// <param name="activeStepControl"></param>
  private void UpdateCustomFormsTabs(object activeStepControl)
  {
    if (this._createdObject.ObjectID == -1L)
      return;
    int num1 = ((IEnumerable<object>) this.CreatorSteps.ToArray()).Count<object>((Func<object, bool>) (item => item is ObjectCreatorControl && ((ObjectCreatorControl) item).ShowBeforeDesForms));
    for (int index = this.CreatorSteps.Count - this._fixedStepsCount - 1 + num1; index >= num1; --index)
      this.CreatorSteps.RemoveAt(index);
    int num2 = this._pnlTop.Height + this._pnlBottom.Height + (this.Height - this.ClientSize.Height);
    Size minimumSize1 = this.MinimumSize;
    IFormDesignerStateHolder service = ServicesManager.GetService(typeof (IFormDesignerStateHolder)) as IFormDesignerStateHolder;
    try
    {
      if (service != null)
        service.State |= FormDesignerState.OpenObjectCreateWizard;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ICollection<FormInformation> formsForObject = ((IFormDesignerService) sessionKeeper.Session.GetCustomService(typeof (IFormDesignerService))).GetFormsForObject(this._createdObject.ObjectID, sessionKeeper.Session.SessionGUID);
        int num3 = num1;
        AdvancedServiceContainer serviceContainer = new AdvancedServiceContainer();
        ViewStateService serviceInstance = new ViewStateService(ViewStateFlags.InDialog | ViewStateFlags.InObjectCreatorDialog);
        serviceContainer.AddService(typeof (IViewState), (object) serviceInstance);
        foreach (FormInformation formInformation in (IEnumerable<FormInformation>) formsForObject)
        {
          IDBObject formObj = sessionKeeper.Session.GetObject(formInformation.ID);
          IDBAttribute attributeByGuid = formObj.GetAttributeByGuid(new Guid("cadd9212-306c-11d8-b4e9-00304f19f545"), false);
          if (attributeByGuid == null || !attributeByGuid.AsBoolean)
          {
            FormDesignerView formDesignerView = this._createdObject.ObjectRelationArray.Count > 0 ? new FormDesignerView(this._createdObject.ObjectID, this._createdObject.ObjectRelationArray[0].LinkID, formInformation.ID) : new FormDesignerView(this._createdObject.ObjectID, formInformation.ID);
            formDesignerView.ServiceProvider = serviceContainer;
            string errorMsg = string.Empty;
            if (!formDesignerView.LoadForm(formObj, out errorMsg))
            {
              int num4 = (int) MessageBox.Show(errorMsg, LocalizationHolder.rm.GetString("Client.Core_872"));
            }
            else
            {
              this._needRefreshForm = false;
              formDesignerView.ButtonsVisible(false);
            }
            formDesignerView._blankMode = true;
            Size minimumSize2 = formDesignerView.MinimumSize;
            if (minimumSize2.Width > minimumSize1.Width)
              minimumSize1.Width = minimumSize2.Width;
            if (minimumSize2.Height + num2 > minimumSize1.Height)
              minimumSize1.Height = minimumSize2.Height + num2;
            this.CreatorSteps.Insert(num3++, (object) formDesignerView);
          }
        }
      }
    }
    finally
    {
      if (service != null)
        service.State &= ~FormDesignerState.OpenObjectCreateWizard;
    }
    if (activeStepControl != null)
    {
      this._currStep = 0;
      for (int index = 0; index < this.CreatorSteps.Count; ++index)
      {
        if (this.CreatorSteps[index] == activeStepControl)
        {
          this._currStep = index;
          break;
        }
      }
    }
    for (int index = 0; index < this.CreatorSteps.Count; ++index)
    {
      if (this.CreatorSteps[index] is ObjectCreatorControl)
      {
        ObjectCreatorControl creatorStep = this.CreatorSteps[index] as ObjectCreatorControl;
        Size minimumSize3 = creatorStep.MinimumSize;
        if (minimumSize3.Width > minimumSize1.Width)
        {
          ref Size local = ref minimumSize1;
          minimumSize3 = creatorStep.MinimumSize;
          int width = minimumSize3.Width;
          local.Width = width;
        }
        minimumSize3 = creatorStep.MinimumSize;
        if (minimumSize3.Height + num2 > minimumSize1.Height)
        {
          ref Size local = ref minimumSize1;
          minimumSize3 = creatorStep.MinimumSize;
          int num5 = minimumSize3.Height + num2;
          local.Height = num5;
        }
      }
      if (this.CreatorSteps[index] is IButtonManager && !(this.CreatorSteps[index] is ObjectClassifierControl))
      {
        IButtonManager creatorStep = this.CreatorSteps[index] as IButtonManager;
        if (!creatorStep.IsButtonEnabledEventSubscribed)
          creatorStep.SetButtonEnabledEvent += new SetButtonEnabledHandler(this.ObjectCreatorForm_SetButtonEnabledEvent);
      }
      if (this.CreatorSteps[index] is IStepCompleteManager)
      {
        IStepCompleteManager creatorStep = this.CreatorSteps[index] as IStepCompleteManager;
        if (!creatorStep.IsCompletedEventSubscribed)
          creatorStep.StepCompletedEvent += new StepCompletedHandler(this.ObjectCreatorForm_StepCompetedHandlerEvent);
      }
    }
    this.MinimumSize = minimumSize1;
    this.UpdateStepControls();
  }

  /// <summary>
  /// Установка элемента управления для текущего шага мастера создания объектов. Доступность кнопок.
  /// </summary>
  private void UpdateStepControls()
  {
    this._pnlCtrls.Controls.Remove((Control) this._currCtrl);
    bool flag1 = true;
    bool flag2 = false;
    UserControl creatorStep = this.CreatorSteps.Count != 0 ? (UserControl) this.CreatorSteps[this._currStep] : (UserControl) null;
    if (creatorStep != null)
    {
      if (creatorStep is FormDesignerView)
      {
        FormDesignerView formDesignerView = creatorStep as FormDesignerView;
        string errorMsg = string.Empty;
        if (this._needRefreshForm && !formDesignerView.RefreshForm(RefreshMode.Forced, out errorMsg))
        {
          int num = (int) MessageBox.Show(errorMsg, LocalizationHolder.rm.GetString("Client.Core_873"));
        }
        else
          this._needRefreshForm = true;
      }
      else if (creatorStep is ObjectCreatorControl objectCreatorControl)
      {
        PageRefreshArgs args = new PageRefreshArgs();
        if (!objectCreatorControl.Refresh(args))
        {
          if (args.Error != null)
          {
            ExceptionHelper.ExceptionService.ShowException(args.Error);
          }
          else
          {
            int num = (int) MessageBox.Show($"{LocalizationHolder.rm.GetString("Client.Core_874")} {objectCreatorControl.Name}");
          }
        }
        flag1 = objectCreatorControl.NextIsAccessible;
        if (objectCreatorControl is ObjectClassifierControl)
          flag2 = ((ObjectClassifierControl) objectCreatorControl).SkipIsVisible;
      }
      if (this.CreatorSteps.Count == this._currStep + 2 && this.CreatorSteps[this._currStep + 1] is ProjectCreatorControl)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute objectAttribute = sessionKeeper.Session.GetObjectAttribute(this.CreatedObjectID, (object) new Guid("cad00815-306c-11d8-b4e9-00304f19f545"), false, false);
          if (objectAttribute != null)
          {
            if (long.Parse(objectAttribute.Value.ToString()) != 0L)
              goto label_20;
          }
          flag1 = false;
        }
      }
label_20:
      creatorStep.Dock = DockStyle.Fill;
      this._pnlCtrls.Controls.Add((Control) creatorStep);
      creatorStep.Visible = true;
      this._currCtrl = creatorStep;
    }
    this._pnlTop.SendToBack();
    this._pnlBottom.SendToBack();
    this._btnPrev.Enabled = this._currStep > 0;
    this._btnNext.Enabled = ((this._createdObject.ObjectID == -1L ? 0 : (this._currStep < this.CreatorSteps.Count - 1 ? 1 : 0)) & (flag1 ? 1 : 0)) != 0;
    this._btnSkip.Visible = flag2;
    this._btnFinish.Enabled = this._createdObject.ObjectID != -1L && this.IsFinishReady();
  }

  /// <summary>Нажата кнопка вызова помощи - показать справку.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ObjectCreatorForm_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    e.Cancel = true;
    this.ShowhHelpTopic();
  }

  /// <summary>Нажата f1 - показать справку.</summary>
  /// <param name="sender"></param>
  /// <param name="hlpevent"></param>
  private void ObjectCreatorForm_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    this.ShowhHelpTopic();
  }

  /// <summary>
  /// Выбор раздела в зависимости от актиной страницы мастера создания.
  /// </summary>
  private void ShowhHelpTopic()
  {
    if (this.CreatorSteps[this._currStep] is ObjectCreatorControl creatorStep)
      HelpProvidersClass.ShowHelpTopic(creatorStep.HelpTopicID);
    else
      HelpProvidersClass.ShowHelpTopic(686);
  }

  private void ObjectCreatorForm_KeyDown(object sender, KeyEventArgs e)
  {
  }

  private void _btnCancel_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1541"), LocalizationHolder.rm.GetString("Client.Core_1466"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }
    else
      this.DialogResult = DialogResult.None;
  }

  private enum FormCheckedCheckBoxes
  {
    None,
    cbOpenEditor,
    cbOpenInNewWindow,
  }
}
