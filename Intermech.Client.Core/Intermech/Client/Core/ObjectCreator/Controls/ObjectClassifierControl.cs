
// Type: Intermech.Client.Core.ObjectCreator.Controls.ObjectClassifierControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Selections.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core.ObjectCreator.Controls;

/// <summary>Шаг классификации в диалоге создания объекта</summary>
internal class ObjectClassifierControl : ObjectCreatorControl, IButtonManager, IStepRefreshManager
{
  /// <summary>Признак классификации</summary>
  private bool _isClassified;
  /// <summary>Тип классификации</summary>
  private ObjectsClassifyType _classifyType;
  /// <summary>Сообщение об ошибке</summary>
  private Exception _error;
  /// <summary>
  /// Массив классификаторов по которым уже проклассифицировали
  /// </summary>
  private List<long> _lastClassifs = new List<long>();
  private Panel panel2;
  private CheckBox checkBox1;
  private Panel panel1;
  private ClassifyingControl classifyingControl1;
  /// <summary>Флаг инициализации контрола</summary>
  private bool _initialized;
  private IContainer components;

  /// <summary>Тип классификации (выборочная, обязательная)</summary>
  private ObjectsClassifyType ClassifyType
  {
    get
    {
      if (this._classifyType == ObjectsClassifyType.None)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this._classifyType = ObjectsClassifyHelper.GetClassifierType(sessionKeeper.Session, this.CreatedObject.ObjectTypeID);
      }
      return this._classifyType;
    }
  }

  public ObjectClassifierControl(CreatedObjectItem cretedObject)
    : base(cretedObject)
  {
    this.InitializeComponent();
    this._showBeforeDesForms = true;
    this._StepIsReadyCheckRequired = true;
    this.FillTreeList();
  }

  /// <summary>
  /// Признак завершенности данного шага мастера создания объектов
  /// (т.е. если true, то по данной закладке можно можно разрешить нажатие на кнопку "Готово")
  /// </summary>
  public override bool StepIsReady
  {
    get => this.ClassifyType != ObjectsClassifyType.Obligatory || this._isClassified;
  }

  /// <summary>Признак видимости кновки "Пропустить"</summary>
  internal bool SkipIsVisible => this.ClassifyType == ObjectsClassifyType.Selective;

  public override bool NextIsAccessible
  {
    get
    {
      ISelectedItemsHost classifyingControl1 = (ISelectedItemsHost) this.classifyingControl1;
      if (classifyingControl1.SelectedItems != null && classifyingControl1.SelectedItems.Count > 0)
      {
        this._isClassified = true;
        for (int index = 0; index < classifyingControl1.SelectedItems.Count; ++index)
        {
          if (!(classifyingControl1.SelectedItems.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID itemData))
            return false;
          if (!this._lastClassifs.Contains(itemData.Value))
            this._isClassified = false;
        }
      }
      if (this.ClassifyType == ObjectsClassifyType.None || this._isClassified)
        return true;
      return classifyingControl1.SelectedItems != null && classifyingControl1.SelectedItems.Count > 0;
    }
  }

  private void FillTreeList()
  {
    if (this._initialized)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long[] classifierForObjType = (sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService).GetClassifierForObjType((object) sessionKeeper.Session.SessionGUID, this.CreatedObject.ObjectTypeID);
      if (classifierForObjType != null && classifierForObjType.Length != 0)
      {
        this.classifyingControl1.RootClassifiers = classifierForObjType;
        if (ServicesManager.GetService(typeof (ICurrentNavWindow)) is ICurrentNavWindow service && service.TreeView is NavigatorTreeView treeView && treeView.FocusedItem != null && treeView.FocusedItem.ItemID is SelectionNodeID)
        {
          SelectionNodeID itemId = (SelectionNodeID) treeView.FocusedItem.ItemID;
          if (MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00157-306c-11d8-b4e9-00304f19f545")).Contains(itemId.TypeID))
            this.classifyingControl1.SelectClassifier(sessionKeeper.Session, itemId.ObjectID);
        }
        ServicesManager.GetService(typeof (NavigatorTreeView));
        this._initialized = true;
      }
      else if (this.ClassifyType == ObjectsClassifyType.Obligatory)
      {
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this.CreatedObject.ObjectTypeID);
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Client.Core_848"), (object) objectType.ObjectTypeName));
      }
    }
  }

  /// <summary>Сохранение данных</summary>
  /// <param name="args">Информации для сохранения </param>
  /// <returns>Если сохранение прошло успешно - true, иначе - false</returns>
  public override bool Save(PageSaveArgs args)
  {
    if (this._isClassified)
      return base.Save(args);
    if (this.checkBox1.Checked)
    {
      ISelectedItemsHost classifyingControl1 = (ISelectedItemsHost) this.classifyingControl1;
      this.CreatedObject.ClassifiersToAdd.Clear();
      if (classifyingControl1.SelectedItems == null || classifyingControl1.SelectedItems.Count == 0)
        base.Save(args);
      (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ISelectionsService));
      for (int index = 0; index < classifyingControl1.SelectedItems.Count; ++index)
      {
        if (classifyingControl1.SelectedItems.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID itemData)
          this.CreatedObject.ClassifiersToAdd.Add(itemData.Value);
      }
    }
    else if (this.ClassifyType != ObjectsClassifyType.None && !this._isClassified)
    {
      this._isClassified = this.Classification();
      if (!this._isClassified)
      {
        args.Error = this._error;
        return false;
      }
      this.CreatedObject.ClassifiersToAdd = this._lastClassifs;
    }
    return base.Save(args);
  }

  /// <summary>Классификация</summary>
  /// <returns></returns>
  private bool Classification()
  {
    this._error = (Exception) null;
    try
    {
      ISelectedItemsHost classifyingControl1 = (ISelectedItemsHost) this.classifyingControl1;
      if (classifyingControl1.SelectedItems == null || classifyingControl1.SelectedItems.Count == 0)
        return true;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ISelectionsService customService = sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
        for (int index = 0; index < classifyingControl1.SelectedItems.Count; ++index)
        {
          if (classifyingControl1.SelectedItems.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID itemData)
          {
            IObjectClassificator objectClassificator = customService.GetObjectClassificator((object) sessionKeeper.Session.SessionGUID, itemData.Value);
            if (objectClassificator != null)
            {
              ClassifiedError classifiedError = Intermech.Navigator.Selections.Consts.ObjectClassify(sessionKeeper.Session, objectClassificator, this.CreatedObject.ObjectID, false);
              if (objectClassificator.NonClassifiedObjects != null && objectClassificator.NonClassifiedObjects.Length != 0 && classifiedError.Exception != null)
                throw classifiedError.Exception;
              this._lastClassifs.Add(itemData.Value);
            }
          }
        }
        return true;
      }
    }
    catch (Exception ex)
    {
      this._error = ex;
      return false;
    }
  }

  /// <summary>Выбор нода в трилисте</summary>
  private void ClassifyingControl1_SelectedItemsChanged(
    object sender,
    ClassifierSelectedEventArgs e)
  {
    SetButtonEnabledHandler buttonEnabledEvent = this.SetButtonEnabledEvent;
    if (buttonEnabledEvent != null)
      buttonEnabledEvent(ButtonType.Next, e.EnableClassify && this.NextIsAccessible);
    this.Refresh();
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectClassifierControl));
    this.panel2 = new Panel();
    this.checkBox1 = new CheckBox();
    this.panel1 = new Panel();
    this.classifyingControl1 = new ClassifyingControl();
    this.panel2.SuspendLayout();
    this.panel1.SuspendLayout();
    ((ISupportInitialize) this.classifyingControl1).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Controls.Add((Control) this.checkBox1);
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.checkBox1, "checkBox1");
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((Control) this.classifyingControl1);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.classifyingControl1, "classifyingControl1");
    this.classifyingControl1.Name = "classifyingControl1";
    this.classifyingControl1.SupportedEvents = IOEventTypes.evKeyUp | IOEventTypes.evMouseDoubleClick;
    this.classifyingControl1.ClassifierSelected += new ClassifierSelectedEventHandler(this.ClassifyingControl1_SelectedItemsChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panel2);
    this.Name = nameof (ObjectClassifierControl);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.panel1.ResumeLayout(false);
    ((ISupportInitialize) this.classifyingControl1).EndInit();
    this.ResumeLayout(false);
  }

  public event SetButtonEnabledHandler SetButtonEnabledEvent;

  /// <summary>будем возвращать id раздела справки для данного шага</summary>
  /// <returns></returns>
  public override int HelpTopicID => 693;

  /// <summary>
  /// 
  /// </summary>
  public bool RefreshOnNextStep => true;

  /// <summary>
  /// 
  /// </summary>
  public bool RefreshOnPrevStep => false;

  public bool IsButtonEnabledEventSubscribed => this.SetButtonEnabledEvent != null;
}
