// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator.TechObjectCreatorBaseControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.ObjectCreator;
using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;

/// <summary>
/// Базовый control для создателя технологических объектов
/// </summary>
public class TechObjectCreatorBaseControl : ObjectCreatorControl, IStepRefreshManager, IButtonManager
{
  /// <summary>Идентификатор объекта - прототипа</summary>
  protected long _prototypeObjId;
  /// <summary>
  /// Признак необходимости копирования атрибутов у прототипа
  /// </summary>
  protected bool _prototypeNeedCopyAttrs;
  /// <summary>id раздела справки</summary>
  protected int _helpTopicId;
  /// <summary>Список контролов проходящих проверки</summary>
  protected readonly List<Control> _validateControls = new List<Control>();
  /// <summary>Доп. параметры создания объекта</summary>
  protected readonly IObjectCreatorParams _creatorExtraParams;
  /// <summary>
  /// 
  /// </summary>
  protected readonly List<NotificationEventArgs> _notificationEvents = new List<NotificationEventArgs>();
  /// <summary>
  /// 
  /// </summary>
  protected bool _needRefreshOnStepChange;
  /// <summary>
  /// Описание объектов со связями для последующего автоподбора
  /// </summary>
  protected readonly List<RelObjInfoItem> _relObjInfo4AutoSelect = new List<RelObjInfoItem>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  /// <summary>
  /// 
  /// </summary>
  protected ErrorProvider errorProvider;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeControlData()
  {
    this._StepIsReadyCheckRequired = true;
    this._StepIsReady = false;
    if (this.CreatedObject == null)
      return;
    this.CreatedObject.AfterCommitCreationEvent += new CreatedObjectItem.AfterCommitCreation(this.CreatedObject_DoAfterCommitCreation);
    this.CreatedObject.BeforeCommitCreationEvent += new CreatedObjectItem.BeforeCommitCreation(this.CreatedObject_DoBeforeCommitCreation);
    this.CreatedObject.OnCancelCreationEvent += new CreatedObjectItem.OnCancelCreation(this.CreatedObject_DoCancelCreation);
    this._prototypeObjId = this.CreatedObject.PrototypeID;
  }

  /// <summary>
  /// 
  /// </summary>
  private void LoadObjectData()
  {
    if (this.CreatedObject == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.DoLoadObjectData(sessionKeeper.Session.GetObjectActualCopy(this.CreatedObject.ObjectID, true));
    this.FirstTimeDataLoading = false;
  }

  /// <summary>Загрузка параметров объекта</summary>
  /// <param name="dbObject"></param>
  protected virtual void DoLoadObjectData(IDBObject dbObject)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  private void SaveObjectData()
  {
    if (this.CreatedObject == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.DoSaveObjectData(sessionKeeper.Session.GetObjectActualCopy(this.CreatedObject.ObjectID, true));
  }

  /// <summary>Сохранение параметров объекта</summary>
  /// <param name="dbObject"></param>
  protected virtual void DoSaveObjectData(IDBObject dbObject)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="control"></param>
  /// <param name="message"></param>
  protected virtual void SetControlErrorMsg(Control control, string message)
  {
    if (control == null)
      return;
    if (!this._validateControls.Contains(control))
      this._validateControls.Add(control);
    this.errorProvider.SetError(control, message);
    if (this.SetButtonEnabledEvent == null || this.FirstTimeDataLoading)
      return;
    this.SetButtonEnabledEvent(ButtonType.Finish, this.StepIsReady);
    this.SetButtonEnabledEvent(ButtonType.Next, this.NextIsAccessible);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected virtual bool HasControlErrorMsg()
  {
    return this._validateControls.Any<Control>((Func<Control, bool>) (item => this.errorProvider.GetError(item) != string.Empty));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected virtual bool HasControlErrorMsg(out string errorMsg)
  {
    List<string> errorList = new List<string>();
    this._validateControls.Where<Control>((Func<Control, bool>) (item => this.errorProvider.GetError(item) != string.Empty)).InvokeForAll<Control>((Action<Control>) (item => errorList.Add(this.errorProvider.GetError(item))));
    if (errorList.Count == 0)
    {
      errorMsg = string.Empty;
      return false;
    }
    errorMsg = string.Join(Environment.NewLine, (IEnumerable<string>) errorList);
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  protected virtual void ShowHelpTopic()
  {
    if (this._helpTopicId == 0)
      return;
    HelpProvidersClass.ShowHelpTopic(this._helpTopicId);
  }

  /// <summary>Инициализация</summary>
  protected void InitializeCustomControls()
  {
    this._showBeforeDesForms = true;
    this._SaveInTransaction = true;
    this._StepIsReadyCheckRequired = true;
    if (this._helpTopicId == 0)
      return;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, this._helpTopicId);
  }

  /// <summary>Копирование атрибутов прототипа</summary>
  /// <param name="targetDbObject"></param>
  protected void CreateObject_CopyPrototypeAttributes(IDBObject targetDbObject)
  {
    if (targetDbObject == null || !this._prototypeNeedCopyAttrs)
      return;
    IDBObject objectActualCopy = targetDbObject.Session.GetObjectActualCopy(this._prototypeObjId, false);
    if (objectActualCopy != null)
    {
      targetDbObject.Attributes.AssignPossibleAttributes(objectActualCopy.Attributes, 0);
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cadd9668-306c-11d8-b4e9-00304f19f545");
      if (MetaDataHelper.GetAttribute4ObjectType(targetDbObject.ObjectType, attributeTypeId) != null)
        targetDbObject.Attributes.AddAttribute(attributeTypeId, false, new object[1]
        {
          (object) Math.Abs(objectActualCopy.ObjectID)
        });
    }
    this._prototypeNeedCopyAttrs = false;
  }

  /// <summary>Копирование состава прототипа</summary>
  /// <param name="session"></param>
  protected void CreateObject_CopyPrototypeComposition(IUserSession session)
  {
    if (this.CreatedObject.IsVersion || this._prototypeObjId == 0L || this._prototypeObjId == -1L || this._prototypeObjId == this.CreatedObject.PrototypeID)
      return;
    ServiceUtils.GetService<ITechUtilsService>((object) session, false)?.CreateObjectComposition(this._prototypeObjId, this.CreatedObject.ObjectID, session.SessionGUID);
    this.CreatedObject.PrototypeID = this._prototypeObjId;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="newObjectId"></param>
  /// <param name="nea"></param>
  /// <returns></returns>
  protected virtual bool CreatedObject_DoCancelCreation(
    IUserSession session,
    long newObjectId,
    List<NotificationEventArgs> nea)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    nea.AddRange((IEnumerable<NotificationEventArgs>) this._notificationEvents);
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="newObject"></param>
  /// <returns></returns>
  protected virtual bool CreatedObject_DoBeforeCommitCreation(
    IUserSession session,
    IDBObject newObject)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (newObject == null)
      throw new ArgumentNullException(nameof (newObject));
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="newObjectId"></param>
  /// <param name="nea"></param>
  /// <returns></returns>
  protected virtual bool CreatedObject_DoAfterCommitCreation(
    IUserSession session,
    long newObjectId,
    List<NotificationEventArgs> nea)
  {
    IAutoSelectionService service = ServiceUtils.GetService<IAutoSelectionService>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      foreach (RelObjInfoItem relObjInfoItem in this._relObjInfo4AutoSelect)
      {
        IDBObject objectActualCopy = session.GetObjectActualCopy(relObjInfoItem.PartInfo.ObjectID, false);
        if (objectActualCopy != null)
        {
          List<RelObjInfoItem> source = service.ExecuteSelection(new AutoSelectionParams(objectActualCopy.ObjectID, relObjInfoItem.RelationID, AutoSelectionMode.AutoObject));
          if (source != null && source.Count > 0)
            this._notificationEvents.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) source.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) source.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.ProjInfo.ObjectID)).ToList<long>(), (IList<int>) source.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.ProjInfo.ObjTypeID)).ToList<int>(), (IList<int>) source.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.RelTypeID)).ToList<int>()));
        }
      }
    }
    nea.AddRange((IEnumerable<NotificationEventArgs>) this._notificationEvents);
    return true;
  }

  /// <summary>Конструктор</summary>
  /// <remarks>Специально для дизайнера форм</remarks>
  protected TechObjectCreatorBaseControl()
  {
    this.InitializeComponent();
    this.InitializeCustomControls();
  }

  /// <summary>Конструктор</summary>
  protected TechObjectCreatorBaseControl(
    CreatedObjectItem createdObject,
    IObjectCreatorParams creatorExtraParams = null)
    : base(createdObject)
  {
    this._creatorExtraParams = creatorExtraParams;
    this.InitializeComponent();
    this.InitializeCustomControls();
    this.InitializeControlData();
  }

  /// <summary>
  /// 
  /// </summary>
  protected bool FirstTimeDataLoading { get; private set; } = true;

  /// <summary>нажата f1 - показать справку</summary>
  /// <param name="sender"></param>
  /// <param name="helpEvent"></param>
  private void TechBaseObjectCreatorControl_HelpRequested(object sender, HelpEventArgs helpEvent)
  {
    this.ShowHelpTopic();
  }

  /// <summary>
  /// Признак разрешения на данном шаге мастера создания объектов переходить к следующему
  /// </summary>
  public override bool NextIsAccessible => base.NextIsAccessible && !this.HasControlErrorMsg();

  /// <summary>
  /// Признак завершенности данного шага мастера создания объектов
  /// (т.е. если true, то по данной закладке можно можно разрешить нажатие на кнопку "Готово")
  /// </summary>
  public override bool StepIsReady => base.StepIsReady && !this.HasControlErrorMsg();

  /// <summary>
  /// Обновление элементов управления в соответствии с данными полей объекта CreatedObject
  /// </summary>
  /// <param name="args">Информации для метода обновления шага мастера создания объектов</param>
  /// <returns></returns>
  public override bool Refresh(PageRefreshArgs args)
  {
    if (!base.Refresh(args))
      return false;
    this._needRefreshOnStepChange = false;
    this.LoadObjectData();
    this._NextIsAccessible = this._StepIsReady = true;
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="args"></param>
  /// <returns></returns>
  public override bool Save(PageSaveArgs args)
  {
    if (!base.Save(args))
      return false;
    string errorMsg;
    if (this.HasControlErrorMsg(out errorMsg))
    {
      args.Error = new Exception(errorMsg);
      return false;
    }
    this._needRefreshOnStepChange = true;
    this.SaveObjectData();
    return true;
  }

  /// <summary>Заполнение combo</summary>
  /// <param name="session">User session</param>
  /// <param name="comboBox">ComboBox to fill</param>
  /// <param name="attrGuid">Attribute type's guid</param>
  protected static void FillComboBoxList(IUserSession session, ComboBox comboBox, Guid attrGuid)
  {
    TechObjectCreatorBaseControl.FillComboBoxList(comboBox, (IEnumerable<object>) session.GetAttributeType(attrGuid)?.GetPossibleValuesArray());
  }

  /// <summary>Заполнение combo</summary>
  /// <param name="comboBox">ComboBox to fill</param>
  /// <param name="values"></param>
  /// <param name="selectedValue"></param>
  protected static void FillComboBoxList(
    ComboBox comboBox,
    IEnumerable<object> values,
    object selectedValue = null)
  {
    comboBox.BeginUpdate();
    try
    {
      comboBox.Items.Clear();
      if (values == null)
        return;
      foreach (object obj in values)
        comboBox.Items.Add(obj);
    }
    finally
    {
      comboBox.EndUpdate();
      if (comboBox.Items.Count > 0)
      {
        if (selectedValue == null)
          comboBox.SelectedIndex = 0;
        else
          comboBox.SelectedItem = selectedValue;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public bool RefreshOnNextStep => this._needRefreshOnStepChange;

  /// <summary>
  /// 
  /// </summary>
  public bool RefreshOnPrevStep => this._needRefreshOnStepChange;

  /// <summary>Установка свойства Enabled кнопке</summary>
  public event SetButtonEnabledHandler SetButtonEnabledEvent;

  /// <summary>
  /// 
  /// </summary>
  public bool IsButtonEnabledEventSubscribed { get; }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
    this.components = (IContainer) new System.ComponentModel.Container();
    this.errorProvider = new ErrorProvider(this.components);
    ((ISupportInitialize) this.errorProvider).BeginInit();
    this.SuspendLayout();
    this.errorProvider.ContainerControl = (ContainerControl) this;
    this.errorProvider.RightToLeft = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (TechObjectCreatorBaseControl);
    this.HelpRequested += new HelpEventHandler(this.TechBaseObjectCreatorControl_HelpRequested);
    ((ISupportInitialize) this.errorProvider).EndInit();
    this.ResumeLayout(false);
  }
}
