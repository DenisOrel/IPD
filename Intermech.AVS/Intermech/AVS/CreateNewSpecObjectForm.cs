// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.CreateNewSpecObjectForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using ImSSP;
using Intermech.Client.Core;
using Intermech.Client.Core.History;
using Intermech.DataFormats;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.PropertyEditors;
using Intermech.UI;
using Intermech.UI.Winforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Форма для создания нового объекта и новой связи, либо для создания новой связи
/// </summary>
/// <summary>
/// Форма для создания нового объекта и новой связи, либо для создания новой связи
/// </summary>
public class CreateNewSpecObjectForm : Form
{
  /// <summary>Требуется ли подавление обрабоки событий</summary>
  protected bool _supressEvents;
  /// <summary>Сервис значков для типов и категорий</summary>
  protected ICategoryTypeIconService _objtypesIcons;
  /// <summary>Кэш графических элементов "Навигатора"</summary>
  protected INavGraphicsCache _navGraphicsCache;
  /// <summary>Служба по работе со спецификациями (со стороны PDM)</summary>
  protected IPDMSpecificationsService _specServices;
  /// <summary>Параметры формы</summary>
  protected NewSpecObjectParams _formParams;
  /// <summary>Контейнер компонентов</summary>
  private IContainer components;
  private Panel panelControls;
  private Panel panelBottom;
  private Bevel bevel;
  private Button btnCancel;
  private Button btnApply;
  private ButtonEdit edit_Zone;
  private Label labelZone;
  private ButtonEdit edit_Format;
  private Label labelFormat;
  private ButtonEdit edit_Position;
  private Label labelPosition;
  private ButtonEdit edit_Quantity;
  private Label labelQuantity;
  private Label labelRemark;
  private MemoEdit edit_Remark;
  private ButtonEdit edit_Name;
  private Label labelName;
  private ButtonEdit edit_Designation;
  private Label labelDesignation;
  private ErrorProvider errorProvider;

  /// <summary>Создать экземпляр формы</summary>
  public CreateNewSpecObjectForm()
    : this((NewSpecObjectParams) null)
  {
  }

  /// <summary>Создать экземпляр формы</summary>
  public CreateNewSpecObjectForm(NewSpecObjectParams formParams)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1513);
    this._formParams = formParams;
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Location = new Point((workingArea.Width - this.Size.Width) / 2, (workingArea.Height - this.Size.Height) / 2);
    FormStorage.LoadLayout((Control) this);
    this._objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this._navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this._specServices = ServicesManager.GetService(typeof (IPDMSpecificationsService)) as IPDMSpecificationsService;
    if (!this.IsDesignerHosted())
      this.LoadData();
    else
      this.UpdateControls();
    this.edit_Designation.TextChanged += new EventHandler(this.edit_Designation_TextChanged);
    this.edit_Designation.Leave += new EventHandler(this.edit_Designation_Leave);
    this.UpdateName();
    this.UpdateFormatControlState();
  }

  /// <summary>
  /// Нужно сделать поле Формат только для чтения для типа "Деталь БЧ".
  /// </summary>
  private void UpdateFormatControlState()
  {
    if ((this._formParams?.OldPart == null ? 0 : (this._formParams.OldPart.Format.Equals("БЧ", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)) == 0)
      return;
    this.edit_Format.Properties.ReadOnly = true;
    foreach (EditorButton button in (CollectionBase) this.edit_Format.Properties.Buttons)
      button.Enabled = false;
  }

  private void UpdateName()
  {
    string designation = this.edit_Designation.Text.Trim();
    if (designation != string.Empty)
    {
      long objectWithDesignation = this._specServices.GetObjectWithDesignation(this._formParams.OldPart.ObjectType, designation);
      switch (objectWithDesignation)
      {
        case -1:
        case 0:
          this.edit_Name.Enabled = objectWithDesignation == 0L;
          break;
        default:
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBAttribute attributeById = sessionKeeper.Session.GetObject(objectWithDesignation).GetAttributeByID(AvsIDCache.Attr_Name);
            if (attributeById != null)
            {
              this.edit_Name.Text = attributeById.AsString;
              goto case -1;
            }
            this.edit_Name.Text = "";
            goto case -1;
          }
      }
    }
    else
      this.edit_Name.Enabled = true;
  }

  private void edit_Designation_Leave(object sender, EventArgs e) => this.UpdateName();

  private void edit_Designation_TextChanged(object sender, EventArgs e)
  {
    BackgroundWorker backgroundWorker = new BackgroundWorker();
    backgroundWorker.WorkerReportsProgress = true;
    backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.bw_ProgressChanged);
    backgroundWorker.DoWork += new DoWorkEventHandler(this.bw_DoWork);
    backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);
    backgroundWorker.RunWorkerAsync((object) this.edit_Designation.Text.Trim());
  }

  private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    BackgroundWorker backgroundWorker = sender as BackgroundWorker;
    backgroundWorker.ProgressChanged -= new ProgressChangedEventHandler(this.bw_ProgressChanged);
    backgroundWorker.DoWork -= new DoWorkEventHandler(this.bw_DoWork);
    backgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);
  }

  private void bw_DoWork(object sender, DoWorkEventArgs e)
  {
    BackgroundWorker backgroundWorker = sender as BackgroundWorker;
    string designation = (string) e.Argument;
    if (designation != string.Empty)
    {
      long objectWithDesignation = this._specServices.GetObjectWithDesignation(this._formParams.OldPart.ObjectType, designation);
      backgroundWorker.ReportProgress(0, (object) objectWithDesignation);
    }
    else
      backgroundWorker.ReportProgress(0, (object) 0);
  }

  private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
  {
    this.edit_Name.Enabled = Convert.ToInt64(e.UserState) == 0L;
  }

  /// <summary>Вызвать форму "Создание нового объекта/связи"</summary>
  /// <param name="formParams">Параметры, с которыми работает форма</param>
  /// <returns>Результат вызова формы</returns>
  public static DialogResult Execute(NewSpecObjectParams formParams)
  {
    if (formParams == null || formParams.AVSDocument == null || formParams.OldPart == null)
      return DialogResult.Cancel;
    using (CreateNewSpecObjectForm newSpecObjectForm = new CreateNewSpecObjectForm(formParams))
      return newSpecObjectForm.ShowDialog();
  }

  /// <summary>Обновить статус контролов в окне</summary>
  protected void UpdateControls()
  {
    this.btnApply.Enabled = true;
    this.btnCancel.Enabled = true;
  }

  /// <summary>Форма закрывается</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void CreateNewSpecObjectForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Вернуть значок для указанного типа объекта</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Значок для указанного типа объекта</returns>
  protected virtual Icon GetObjTypeIcon(int objTypeID)
  {
    objTypeID = Math.Max(objTypeID, -1);
    return this._objtypesIcons.IndexOf(4, objTypeID) < 0 ? (Icon) null : ImagesResizeHelper.ResizeIconTo32x16(this._objtypesIcons.GetIcon(4, objTypeID), SystemColors.Window);
  }

  /// <summary>
  /// Вернуть значок для указанного типа объекта без изменения его размеров
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Значок для указанного типа объекта</returns>
  protected virtual Icon GetObjTypeIconOriginalSize(int objTypeID)
  {
    objTypeID = Math.Max(objTypeID, -1);
    return this._objtypesIcons.IndexOf(4, objTypeID) < 0 ? (Icon) null : this._objtypesIcons.GetIcon(4, objTypeID);
  }

  /// <summary>Загрузить информацию в поля формы</summary>
  protected void LoadData()
  {
    try
    {
      this._supressEvents = true;
      this.Icon = this.GetObjTypeIconOriginalSize(this._formParams.OldPart.ObjectType);
      this.ShowIcon = true;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute4ObjectTypeCollection attributes = sessionKeeper.Session.GetObjectType(this._formParams.OldPart.ObjectType).Attributes as IDBAttribute4ObjectTypeCollection;
        this.edit_Designation.Text = this._formParams.OldPart.Designation;
        this.edit_Designation.Tag = (object) new AttributeSource(AttributableElements.Object, AvsIDCache.Attr_Designation);
        this.edit_Designation.Enabled = true;
        this.labelDesignation.Enabled = this.edit_Designation.Enabled;
        this.edit_Name.Text = this._formParams.OldPart.Name;
        this.edit_Name.Tag = (object) new AttributeSource(AttributableElements.Object, AvsIDCache.Attr_Name);
        int attrName = AvsIDCache.Attr_Name;
        IDBAttributeType4 attributeById = attributes.GetAttributeByID(attrName, false);
        this.edit_Name.Enabled = attributeById != null && attributeById.Computed == ComputeValueModes.NotComputableValue;
        this.labelName.Enabled = this.edit_Name.Enabled;
        this.edit_Format.Text = this._formParams.OldPart.Format;
        this.edit_Format.Tag = (object) new AttributeSource(AttributableElements.Object, AvsIDCache.Attr_Format);
        this.edit_Format.Enabled = true;
        this.labelFormat.Enabled = this.edit_Format.Enabled;
        this.edit_Zone.Text = this._formParams.OldPart.Zone;
        this.edit_Zone.Tag = (object) new AttributeSource(AttributableElements.Relation, AvsIDCache.Attr_Zone);
        this.edit_Zone.Enabled = true;
        this.labelZone.Enabled = this.edit_Zone.Enabled;
        this.edit_Position.Text = "";
        this.edit_Position.Tag = (object) new AttributeSource(AttributableElements.Relation, AvsIDCache.Attr_Position);
        this.edit_Position.Enabled = true;
        this.labelPosition.Enabled = this.edit_Position.Enabled;
        if (!this._formParams.SameSpecification)
          this.edit_Quantity.Text = this._formParams.OldPart.Quantity;
        this.edit_Quantity.Tag = (object) new AttributeSource(AttributableElements.Relation, AvsIDCache.Attr_Count);
        this.edit_Quantity.Enabled = true;
        this.labelQuantity.Enabled = this.edit_Quantity.Enabled;
        this.edit_Remark.Text = this._formParams.OldPart.Remark;
        this.edit_Remark.Tag = (object) new AttributeSource(AttributableElements.Relation, AvsIDCache.Attr_Note);
        this.edit_Remark.Enabled = true;
        this.labelRemark.Enabled = this.edit_Remark.Enabled;
      }
    }
    finally
    {
      this._supressEvents = false;
      this.UpdateControls();
    }
  }

  /// <summary>
  /// Изменился текст в редакторе, очистим текст в провайдере ошибок
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoClearErrorProviderText(object sender, EventArgs e)
  {
    if (this._supressEvents)
      return;
    this.errorProvider.Clear();
    this.UpdateControls();
  }

  /// <summary>Нажата кнопка в редакторе текста</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoEditorButtonPressed(object sender, ButtonPressedEventArgs e)
  {
    this.errorProvider.Clear();
    ButtonEdit buttonEdit = sender as ButtonEdit;
    int num = buttonEdit.Properties.Buttons.IndexOf(e.Button);
    int objectType1 = this._formParams.OldPart.ObjectType;
    AttributeSource tag = buttonEdit.Tag as AttributeSource;
    if (num == 0)
    {
      using (ObjectsHistory objectsHistory = new ObjectsHistory((object) objectType1, tag.Source, (object) tag.ID))
      {
        objectsHistory.SelectedValue = (object) buttonEdit.Text.Trim();
        if (objectsHistory.ShowDialog() == DialogResult.OK)
          buttonEdit.Text = (string) objectsHistory.SelectedValue;
      }
    }
    if (buttonEdit == this.edit_Designation && num == 1)
    {
      bool flag = false;
      string objectTypeName = MetaDataHelper.GetObjectTypeName(this._formParams.OldPart.ObjectType);
      long[] classifierForObjType;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ISelectionsService customService = sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
        ObjectsClassifyType classifierType = ObjectsClassifyHelper.GetClassifierType(sessionKeeper.Session, this._formParams.OldPart.ObjectType);
        // ISSUE: variable of a boxed type
        __Boxed<Guid> sessionGuid = (System.ValueType) sessionKeeper.Session.SessionGUID;
        int objectType2 = this._formParams.OldPart.ObjectType;
        classifierForObjType = customService.GetClassifierForObjType((object) sessionGuid, objectType2);
        if ((classifierForObjType == null || classifierForObjType.Length == 0) && classifierType == ObjectsClassifyType.Obligatory)
          throw new Exception($"Не найдено ни одного классификатора для объекта типа \"{objectTypeName}\"");
        if (this._formParams.NewObjectID == 0L)
        {
          flag = true;
          IDBObject blank = this.TryCreateBlank(sessionKeeper.Session, true);
          if (blank != null)
          {
            this._formParams.NewObjectID = blank.ObjectID;
            this._formParams.IsBlank = blank.IsCreationMode;
          }
        }
      }
      using (ClassifySelectionForm classifySelectionForm = new ClassifySelectionForm(classifierForObjType))
      {
        if (classifySelectionForm.ShowDialog() != DialogResult.OK)
        {
          if (!flag)
            return;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            sessionKeeper.Session.GetObject(this._formParams.NewObjectID).Delete(0L);
            this._formParams.NewObjectID = 0L;
            this._formParams.IsBlank = false;
            return;
          }
        }
        this._formParams.ClassifierID = (classifySelectionForm.SelectedItems.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
      }
      AttributeValues[] classificationAttributes = ClassificationHelper.GetClassificationAttributes(this._formParams.ClassifierID, this._formParams.NewObjectID);
      int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545");
      int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545");
      if (classificationAttributes != null)
      {
        for (int index = 0; index < classificationAttributes.Length; ++index)
        {
          AttributeValues attributeValues = classificationAttributes[index];
          if (attributeValues.AttributeID == attributeTypeId1 && attributeValues.Values != null && attributeValues.Values.Length != 0 && attributeValues.Values[0] != DBNull.Value)
            this.edit_Designation.Text = attributeValues.Values[0].ToString();
          if (attributeValues.AttributeID == attributeTypeId2 && attributeValues.Values != null && attributeValues.Values.Length != 0 && attributeValues.Values[0] != DBNull.Value)
            this.edit_Name.Text = attributeValues.Values[0].ToString();
        }
      }
    }
    if (buttonEdit != this.edit_Quantity || num != 1)
      return;
    using (MeasureForm measureForm = new MeasureForm())
    {
      if (MeasureHelper.Measures == null || MeasureHelper.Measures.Length == 0)
        return;
      MeasureDescriptor measure = MeasureHelper.Measures[0];
      MeasuredValue aMeasureValue;
      try
      {
        if (double.TryParse(this.edit_Quantity.Text.Trim(), out double _))
          this.edit_Quantity.Text = this.edit_Quantity.Text.Trim() + " шт";
        aMeasureValue = AVSRow.ConvertCountToMeasuredValue((object) this.edit_Quantity.Text.Trim());
      }
      catch
      {
        aMeasureValue = new MeasuredValue(0.0, measure.MeasureID);
      }
      ArrayList listByAttributeId = MeasureEditor.GetMeasureDescriptorListByAttributeId(AvsIDCache.Attr_Count);
      MeasureDescriptor[] aMeasureDescriptorList = listByAttributeId == null ? MeasureHelper.Instance.Measures : (MeasureDescriptor[]) listByAttributeId.ToArray(typeof (MeasureDescriptor));
      if (measureForm.ExecuteDialog(ref aMeasureValue, aMeasureDescriptorList) != DialogResult.OK)
        return;
      this.edit_Quantity.Text = MeasureHelper.ConvertToString(aMeasureValue.Value, aMeasureValue.MeasureID, false);
    }
  }

  /// <summary>Попытаться создать заготовку нового объекта</summary>
  /// <param name="session">Сессия</param>
  /// <param name="forceCreation">true - не искать старый объект, а создавать новый</param>
  /// <returns>Вновь созданная заготовка или null</returns>
  private IDBObject TryCreateBlank(IUserSession session, bool forceCreation)
  {
    if (session == null)
      return (IDBObject) null;
    string str = this.edit_Designation.Text.Trim();
    string initValue = this.edit_Name.Text.Trim();
    if (!forceCreation && str == this._formParams.OldPart.Designation && initValue == this._formParams.OldPart.Name)
      return session.GetObject(this._formParams.OldPart.ObjectID, false);
    IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(this._formParams.OldPart.ObjectType, MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"));
    bool flag = attribute4ObjectType != null && attribute4ObjectType.Unique != 0;
    if (!forceCreation & flag && !string.IsNullOrEmpty(str))
      this._specServices.GetObjectWithDesignation(this._formParams.OldPart.ObjectType, str);
    if (this._formParams.NewObjectID != 0L)
      return session.GetObject(this._formParams.NewObjectID, false);
    IDBObject dbObj = session.GetObjectCollection(this._formParams.OldPart.ObjectType).Create(this._formParams.OldPart.ObjectID);
    if (dbObj.ObjectModifyMode == ObjectModifyModes.Checkout && dbObj.CheckoutBy == 0L)
      dbObj = dbObj.CheckOut();
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    if (!forceCreation)
      attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"), (object) str));
    if (this.edit_Name.Enabled)
      attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), (object) initValue));
    if (dbObj.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad00255-306c-11d8-b4e9-00304f19f545")) != null)
      attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00255-306c-11d8-b4e9-00304f19f545"), (object) this.edit_Format.Text.Trim()));
    if (attributeValuesList.Count > 0)
      DBObjectHelper.SetDBAttributeValues(dbObj, attributeValuesList.ToArray());
    return dbObj;
  }

  /// <summary>Нажата кнопка "ОК"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoTryCreate(object sender, EventArgs e)
  {
    if (!this.TryCreateLink())
      return;
    this.DialogResult = DialogResult.OK;
  }

  /// <summary>
  /// Получить объект с указанным значением атрибута "Обозначение"
  /// </summary>
  /// <param name="objectType">Идентификатор типа объекта</param>
  /// <param name="designation">Обозначение</param>
  /// <returns>Идентификатор версии объекта или Intermech.Consts.UnknownObjectId</returns>
  public long GetObjectWithName(int objectType, string name)
  {
    if (!MetaDataHelper.ExistsObjectType(objectType))
      return 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(objectType);
      objectCollection.ShowAllModifications = true;
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) name, LogicalOperators.NONE, 0, false)
      }, new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1)
      }, recordCount: 1);
      DataTable dataTable = objectCollection.Select(paramSet);
      if (dataTable == null)
        return 0;
      try
      {
        return dataTable.Rows.Count == 0 ? 0L : Convert.ToInt64(dataTable.Rows[0][0]);
      }
      finally
      {
        dataTable.Dispose();
      }
    }
  }

  /// <summary>
  /// Попытаться создать связь, либо пару новый объект + связь
  /// </summary>
  /// <returns>true, если всё успешно создано</returns>
  protected virtual bool TryCreateLink()
  {
    if (this._specServices == null)
      return false;
    this._formParams.NewPart = (IDBSpecificationObjectID) null;
    MetaDataHelper.GetAttribute4ObjectType(this._formParams.OldPart.ObjectType, AvsIDCache.Attr_Designation);
    string designation = this.edit_Designation.Text.Trim();
    string str1 = this.edit_Name.Text.Trim();
    if (string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(designation))
    {
      this.errorProvider.SetError((Control) this.labelDesignation, "Не задано \"Обозначение\" и \"Наименование\"");
      return false;
    }
    long num1 = !string.IsNullOrEmpty(designation) ? this._specServices.GetObjectWithDesignation(this._formParams.OldPart.ObjectType, designation) : 0L;
    if (num1 == 0L && string.IsNullOrEmpty(designation))
      num1 = this.GetObjectWithName(this._formParams.OldPart.ObjectType, str1);
    IDBObject dbObj = (IDBObject) null;
    string str2 = string.Empty;
    bool flag1 = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTransactions customService = sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
      bool flag2 = false;
      try
      {
        if (num1 == 0L)
        {
          if (MessageBox.Show($"Объект с обозначением \"{designation}\" Отсутствует в базе данных, Создать?", "Создание нового объекта", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            return false;
        }
        else
        {
          dbObj = sessionKeeper.Session.GetObject(num1);
          if (this.edit_Name.Enabled)
          {
            IDBAttribute attributeById = dbObj.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"));
            if (attributeById != null)
              str2 = attributeById.AsString;
            flag1 = str2 != str1;
            if (flag1)
            {
              long checkoutBy = dbObj.CheckoutBy;
              if (checkoutBy == 0L)
              {
                if (dbObj.ObjectModifyMode != ObjectModifyModes.Checkout)
                {
                  if (dbObj.ObjectModifyMode == ObjectModifyModes.CreateVersion)
                  {
                    this.errorProvider.SetError((Control) this.labelName, $"Для изменения существующего объекта с обозначением \"{designation}\" требуется выпускать новую версию.");
                    int num2 = (int) MessageBox.Show(string.Format(sc_874.ssp_avs_875(), (object) designation), "Изменение существующего объекта", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                  }
                  if (dbObj.ObjectModifyMode == ObjectModifyModes.CantModify)
                  {
                    this.errorProvider.SetError((Control) this.labelName, $"Существующий объект с обозначением \"{designation}\" нельзя модифицировать.");
                    int num3 = (int) MessageBox.Show(string.Format(sc_874.ssp_avs_876(), (object) designation), "Изменение существующего объекта", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                  }
                }
              }
              else if (checkoutBy != sessionKeeper.Session.UserID)
              {
                QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(checkoutBy);
                this.errorProvider.SetError((Control) this.labelName, $"Объект с обозначением \"{designation}\" уже присутствует в базе данных и он взят на редактирование пользователем \"{objectInfo.Caption}\"");
                int num4 = (int) MessageBox.Show($"Объект с обозначением \"{designation}\" уже присутствует в базе данных и он взят на редактирование пользователем \"{objectInfo.Caption}\"", "Изменение существующего объекта", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
              }
            }
          }
        }
        customService?.StartTransaction();
        if (num1.IsUndefinedId())
          dbObj = this.TryCreateBlank(sessionKeeper.Session, false);
        if (dbObj != null && flag1)
        {
          if (dbObj.ObjectModifyMode == ObjectModifyModes.Checkout && dbObj.CheckoutBy == 0L)
          {
            long objectId = dbObj.ObjectID;
            dbObj = dbObj.CheckOut();
            (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
            {
              objectId
            }, (IList<long>) new long[1]{ dbObj.ObjectID }));
          }
          DBObjectHelper.SetDBAttributeValues(dbObj, new AttributeValues[1]
          {
            new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), (object) str1)
          });
          (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", dbObj.ObjectID));
        }
        int num5 = this._formParams.OldPart.RelationTypeID;
        if (num5 == -1)
          num5 = !MetaDataHelper.IsObjectTypeChildOf(this._formParams.OldPart.ObjectType, AvsIDCache.ObjType_Document) ? AvsIDCache.Relation_Project : AvsIDCache.Relation_Document;
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(num5);
        if (this._formParams.DestinationProducts == null || this._formParams.DestinationProducts.Count == 0)
          this._formParams.DestinationProducts = new List<long>()
          {
            this._formParams.OldPart.ProjID
          };
        long initValue = -1;
        List<AttributeValues> attributeValuesList = new List<AttributeValues>();
        long sectionIdForObject = AVSDocument.GetDefaultSectionIdForObject(this._formParams.OldPart.ObjectType, (string) null, this._formParams.ContextSectionID, this._formParams.AVSDocument.GetAllowableDocumentSections());
        this._formParams.NewRelations = new List<long>();
        long relationID = -1;
        for (int index1 = 0; index1 < this._formParams.DestinationProducts.Count; ++index1)
        {
          --initValue;
          attributeValuesList.Clear();
          IDBAttribute4RelationTypeCollection attributes = sessionKeeper.Session.GetRelationType(num5).Attributes as IDBAttribute4RelationTypeCollection;
          if (double.TryParse(this.edit_Quantity.Text.Trim(), out double _))
            this.edit_Quantity.Text = this.edit_Quantity.Text.Trim() + " шт";
          bool flag3 = string.IsNullOrEmpty(this.edit_Quantity.Text);
          attributeValuesList.Add(new AttributeValues(AvsIDCache.Attr_SortIndex, (object) initValue));
          if (sectionIdForObject != -1L)
            attributeValuesList.Add(new AttributeValues(AvsIDCache.Attr_SpecificationSection, (object) sectionIdForObject));
          else
            attributeValuesList.Add(new AttributeValues(AvsIDCache.Attr_SpecificationSection, (object) null));
          if (attributes.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545"), false) != null)
            attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545"), this.edit_Quantity.Text.Trim() != "" ? (object) this.edit_Quantity.Text.Trim() : (object) (string) null));
          if (attributes.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad00270-306c-11d8-b4e9-00304f19f545"), false) != null)
            attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00270-306c-11d8-b4e9-00304f19f545"), this.edit_Position.Text.Trim() != "" ? (object) this.edit_Position.Text.Trim() : (object) (string) null));
          if (attributes.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad0027a-306c-11d8-b4e9-00304f19f545"), false) != null)
            attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad0027a-306c-11d8-b4e9-00304f19f545"), this.edit_Zone.Text.Trim() != "" ? (object) this.edit_Zone.Text.Trim() : (object) (string) null));
          if (attributes.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad00021-306c-11d8-b4e9-00304f19f545"), false) != null)
            attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad00021-306c-11d8-b4e9-00304f19f545"), (object) this.edit_Remark.Text.Trim()));
          for (int index2 = 0; index2 < this._formParams.AVSDocument.productsInfo.Count; ++index2)
          {
            long num6 = this._formParams.DestinationProducts[index1] != -1L ? this._formParams.DestinationProducts[index1] : this._formParams.AVSDocument.productsInfo[index2].Id;
            IDBRelation dbRelation = (IDBRelation) null;
            if (relationCollection.RelationTypeID == AvsIDCache.Relation_Document)
              dbRelation = sessionKeeper.Session.GetRelation(num6, dbObj.ID, AvsIDCache.Relation_Document, false);
            if (dbRelation == null && (!flag3 || this._formParams.AlwaysCreateRelations))
              dbRelation = relationCollection.Create(new NewRelationProperties(0L, num6, dbObj.ID, DateTime.MinValue, DateTime.MaxValue, dbObj.ObjectID)
              {
                ValuesList = attributeValuesList.ToArray()
              });
            if (dbRelation != null)
            {
              relationID = dbRelation.RelationID;
              this._formParams.NewRelations.Add(relationID);
            }
            if (this._formParams.DestinationProducts[index1] != -1L)
              break;
          }
        }
        if (this._formParams.NewRelations.Count > 0)
          ((INotificationService) ServicesManager.GetService(typeof (INotificationService))).FireEvent((object) this._formParams.AVSDocument, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) this._formParams.NewRelations));
        if (dbObj.IsCreationMode)
        {
          if (this._formParams.ClassifierID != 0L)
          {
            ClassificationHelper.Classification(this._formParams.ClassifierID, dbObj.ObjectID);
            dbObj.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545")).Value = (object) designation;
            dbObj.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545")).Value = (object) str1;
          }
          dbObj.CommitCreation(true, true);
          long objectId = dbObj.ObjectID;
          (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectId));
        }
        long destinationProduct = this._formParams.DestinationProducts[0];
        this._formParams.NewPart = (IDBSpecificationObjectID) new DBSpecificationObjectID(dbObj.ObjectType, dbObj.ObjectID, dbObj.ID, dbObj.Caption, relationID, num5, destinationProduct, designation, str1, this.edit_Zone.Text.Trim(), this.edit_Position.Text.Trim(), this.edit_Format.Text.Trim(), this.edit_Quantity.Text.Trim(), this.edit_Remark.Text.Trim(), sectionIdForObject, 0L, 0L);
        flag2 = true;
      }
      finally
      {
        if (customService != null)
        {
          if (flag2)
            customService.Commit();
          else
            customService.Rollback();
        }
      }
    }
    return true;
  }

  /// <summary>Освободить ресурсы</summary>
  /// <param name="disposing">true, если требуется освободить управляемые ресурсы</param>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CreateNewSpecObjectForm));
    this.panelControls = new Panel();
    this.edit_Name = new ButtonEdit();
    this.labelName = new Label();
    this.edit_Designation = new ButtonEdit();
    this.labelDesignation = new Label();
    this.edit_Remark = new MemoEdit();
    this.labelRemark = new Label();
    this.edit_Quantity = new ButtonEdit();
    this.labelQuantity = new Label();
    this.edit_Position = new ButtonEdit();
    this.labelPosition = new Label();
    this.edit_Format = new ButtonEdit();
    this.labelFormat = new Label();
    this.edit_Zone = new ButtonEdit();
    this.labelZone = new Label();
    this.panelBottom = new Panel();
    this.bevel = new Bevel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.errorProvider = new ErrorProvider(this.components);
    this.panelControls.SuspendLayout();
    this.edit_Name.Properties.BeginInit();
    this.edit_Designation.Properties.BeginInit();
    this.edit_Remark.Properties.BeginInit();
    this.edit_Quantity.Properties.BeginInit();
    this.edit_Position.Properties.BeginInit();
    this.edit_Format.Properties.BeginInit();
    this.edit_Zone.Properties.BeginInit();
    this.panelBottom.SuspendLayout();
    ((ISupportInitialize) this.errorProvider).BeginInit();
    this.SuspendLayout();
    this.panelControls.Controls.Add((Control) this.edit_Name);
    this.panelControls.Controls.Add((Control) this.labelName);
    this.panelControls.Controls.Add((Control) this.edit_Designation);
    this.panelControls.Controls.Add((Control) this.labelDesignation);
    this.panelControls.Controls.Add((Control) this.edit_Remark);
    this.panelControls.Controls.Add((Control) this.labelRemark);
    this.panelControls.Controls.Add((Control) this.edit_Quantity);
    this.panelControls.Controls.Add((Control) this.labelQuantity);
    this.panelControls.Controls.Add((Control) this.edit_Position);
    this.panelControls.Controls.Add((Control) this.labelPosition);
    this.panelControls.Controls.Add((Control) this.edit_Format);
    this.panelControls.Controls.Add((Control) this.labelFormat);
    this.panelControls.Controls.Add((Control) this.edit_Zone);
    this.panelControls.Controls.Add((Control) this.labelZone);
    componentResourceManager.ApplyResources((object) this.panelControls, "panelControls");
    this.panelControls.Name = "panelControls";
    componentResourceManager.ApplyResources((object) this.edit_Name, "edit_Name");
    this.edit_Name.Name = "edit_Name";
    this.edit_Name.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("edit_Name.Properties.Buttons"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "История значений")
    });
    this.edit_Name.ButtonClick += new ButtonPressedEventHandler(this.DoEditorButtonPressed);
    this.edit_Name.EditValueChanged += new EventHandler(this.DoClearErrorProviderText);
    componentResourceManager.ApplyResources((object) this.labelName, "labelName");
    this.labelName.Name = "labelName";
    componentResourceManager.ApplyResources((object) this.edit_Designation, "edit_Designation");
    this.edit_Designation.Name = "edit_Designation";
    this.edit_Designation.Properties.Buttons.AddRange(new EditorButton[2]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("edit_Designation.Properties.Buttons"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "История значений"),
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("edit_Designation.Properties.Buttons1"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Классификация")
    });
    this.edit_Designation.ButtonClick += new ButtonPressedEventHandler(this.DoEditorButtonPressed);
    this.edit_Designation.EditValueChanged += new EventHandler(this.DoClearErrorProviderText);
    componentResourceManager.ApplyResources((object) this.labelDesignation, "labelDesignation");
    this.labelDesignation.Name = "labelDesignation";
    componentResourceManager.ApplyResources((object) this.edit_Remark, "edit_Remark");
    this.edit_Remark.Name = "edit_Remark";
    this.edit_Remark.EditValueChanged += new EventHandler(this.DoClearErrorProviderText);
    componentResourceManager.ApplyResources((object) this.labelRemark, "labelRemark");
    this.labelRemark.Name = "labelRemark";
    componentResourceManager.ApplyResources((object) this.edit_Quantity, "edit_Quantity");
    this.edit_Quantity.Name = "edit_Quantity";
    this.edit_Quantity.Properties.Buttons.AddRange(new EditorButton[2]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("edit_Quantity.Properties.Buttons"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "История значений"),
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("edit_Quantity.Properties.Buttons1"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "Задать новое значение")
    });
    this.edit_Quantity.ButtonClick += new ButtonPressedEventHandler(this.DoEditorButtonPressed);
    this.edit_Quantity.EditValueChanged += new EventHandler(this.DoClearErrorProviderText);
    componentResourceManager.ApplyResources((object) this.labelQuantity, "labelQuantity");
    this.labelQuantity.Name = "labelQuantity";
    componentResourceManager.ApplyResources((object) this.edit_Position, "edit_Position");
    this.edit_Position.Name = "edit_Position";
    this.edit_Position.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("edit_Position.Properties.Buttons"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "История значений")
    });
    this.edit_Position.ButtonClick += new ButtonPressedEventHandler(this.DoEditorButtonPressed);
    this.edit_Position.EditValueChanged += new EventHandler(this.DoClearErrorProviderText);
    componentResourceManager.ApplyResources((object) this.labelPosition, "labelPosition");
    this.labelPosition.Name = "labelPosition";
    componentResourceManager.ApplyResources((object) this.edit_Format, "edit_Format");
    this.edit_Format.Name = "edit_Format";
    this.edit_Format.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("edit_Format.Properties.Buttons"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "История значений")
    });
    this.edit_Format.ButtonClick += new ButtonPressedEventHandler(this.DoEditorButtonPressed);
    this.edit_Format.EditValueChanged += new EventHandler(this.DoClearErrorProviderText);
    componentResourceManager.ApplyResources((object) this.labelFormat, "labelFormat");
    this.labelFormat.Name = "labelFormat";
    componentResourceManager.ApplyResources((object) this.edit_Zone, "edit_Zone");
    this.edit_Zone.Name = "edit_Zone";
    this.edit_Zone.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("edit_Zone.Properties.Buttons"), new KeyShortcut(Keys.None), new ViewStyle("EditorButtonStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText), "История значений")
    });
    this.edit_Zone.ButtonClick += new ButtonPressedEventHandler(this.DoEditorButtonPressed);
    this.edit_Zone.EditValueChanged += new EventHandler(this.DoClearErrorProviderText);
    componentResourceManager.ApplyResources((object) this.labelZone, "labelZone");
    this.labelZone.Name = "labelZone";
    this.panelBottom.Controls.Add((Control) this.bevel);
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.bevel, "bevel");
    this.bevel.Name = "bevel";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.DoTryCreate);
    this.errorProvider.ContainerControl = (ContainerControl) this;
    componentResourceManager.ApplyResources((object) this.errorProvider, "errorProvider");
    this.AcceptButton = (IButtonControl) this.btnApply;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panelControls);
    this.Controls.Add((Control) this.panelBottom);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (CreateNewSpecObjectForm);
    this.ShowInTaskbar = false;
    this.FormClosed += new FormClosedEventHandler(this.CreateNewSpecObjectForm_FormClosed);
    this.panelControls.ResumeLayout(false);
    this.panelControls.PerformLayout();
    this.edit_Name.Properties.EndInit();
    this.edit_Designation.Properties.EndInit();
    this.edit_Remark.Properties.EndInit();
    this.edit_Quantity.Properties.EndInit();
    this.edit_Position.Properties.EndInit();
    this.edit_Format.Properties.EndInit();
    this.edit_Zone.Properties.EndInit();
    this.panelBottom.ResumeLayout(false);
    ((ISupportInitialize) this.errorProvider).EndInit();
    this.ResumeLayout(false);
  }
}
