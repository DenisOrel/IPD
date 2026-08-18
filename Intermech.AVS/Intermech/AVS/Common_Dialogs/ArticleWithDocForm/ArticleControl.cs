// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.ArticleControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Properties;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>Закладка с атрибутами изделия</summary>
internal class ArticleControl : PageUserControl
{
  /// <summary>Текущее значение атрибута "Масса"</summary>
  private MeasuredValue _weight;
  /// <summary>Текущее значение атрибута "Литера"</summary>
  private string _litera = string.Empty;
  /// <summary>Тип изделия</summary>
  private int _articleType;
  /// <summary>Флаг того, что атрибут "Код ОКП" изменялся</summary>
  private bool _okpCodeChanged;
  /// <summary>Флаг того, что атрибут "Материал" изменялся</summary>
  private bool _materialChanged;
  /// <summary>Флаг того, что атрибут "Размер" изменялся</summary>
  private bool _sizeChanged;
  /// <summary>Флаги задизабленных атрибутов</summary>
  private ArticleControl.DisableAttributes _disAttrs;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button bEditMaterial;
  private Label label7;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbMaterial;
  private Button bClassificate;
  private Button bEditName;
  private Button bEditDesignation;
  private Label label2;
  private Label label1;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbName;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbDesignation;
  private Button bEditWeigth;
  private Label label8;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbWeigth;
  private Label label3;
  private Label label6;
  private Label lVersion;
  private Label lLCStep;
  private Label lProject;
  private Label label5;
  private Label label4;
  private Panel panel1;
  private Label lArtType;
  private Panel panel2;
  private ComboBox bEditLitera;

  public ArticleControl(IDBObject article, CommonDataType disableControls, IPageControl firstPage)
  {
    this.InitializeComponent();
    this.Init(article, disableControls, firstPage);
  }

  internal void Init(IDBObject article, CommonDataType disableControls, IPageControl firstPage)
  {
    this.Init(article.ObjectID, AttributableElements.Object, disableControls);
    this._articleType = article.ObjectType;
    firstPage.GetEditorEvent -= new GetEditorDelegate(this.firstPage_GetEditorEvent);
    firstPage.GetEditorEvent += new GetEditorDelegate(this.firstPage_GetEditorEvent);
  }

  private object firstPage_GetEditorEvent(object sender, GetEditorEventArgs args)
  {
    if (args.Handled)
      return (object) null;
    if (args.AttributeID != FormHelper.AttributeMaterialID && args.AttributeID != FormHelper.AttributeSizeID)
      return (object) null;
    args.Handled = true;
    return this.ChangeInEditor(args.AttributeID, args.Value);
  }

  protected override void OnSave(IUserSession session, OpenModes mode, CreatedPair pair)
  {
    if (mode == OpenModes.View)
      return;
    try
    {
      if ((this._disAttrs & ArticleControl.DisableAttributes.Weight) == ArticleControl.DisableAttributes.None && this.aProcessor.FindAttributeValues(FormHelper.AttributeWeightID) == null)
        this.aProcessor.ActualAttributeValues.Add(AttributeProcessor.CreateAttributeValues(FormHelper.AttributeWeightID, this.attributableElementID, this.attributableElement));
      if ((this._disAttrs & ArticleControl.DisableAttributes.Litera) == ArticleControl.DisableAttributes.None && this.aProcessor.FindAttributeValues(FormHelper.AttributeLiteraID) == null)
        this.aProcessor.ActualAttributeValues.Add(AttributeProcessor.CreateAttributeValues(FormHelper.AttributeLiteraID, this.attributableElementID, this.attributableElement));
      if (this.aProcessor.FindAttributeValues(FormHelper.AttributeNameID) == null)
        this.aProcessor.ActualAttributeValues.Add(AttributeProcessor.CreateAttributeValues(FormHelper.AttributeNameID, this.attributableElementID, this.attributableElement));
      if (this._materialChanged)
      {
        if (this.aProcessor.FindAttributeValues(FormHelper.AttributeMaterialID) == null)
          this.aProcessor.ActualAttributeValues.Add(AttributeProcessor.CreateAttributeValues(FormHelper.AttributeMaterialID, this.attributableElementID, this.attributableElement));
        if (this.commonData.Material != null && this.commonData.Material.ObjectID != 0L)
          this.aProcessor.SetValue(FormHelper.AttributeMaterialID, (object) this.commonData.Material.ObjectID);
        else
          this.aProcessor.SetValue(FormHelper.AttributeMaterialID, (object) null);
      }
      if (this._sizeChanged)
      {
        if (this.aProcessor.FindAttributeValues(FormHelper.AttributeSizeID) == null)
          this.aProcessor.ActualAttributeValues.Add(AttributeProcessor.CreateAttributeValues(FormHelper.AttributeSizeID, this.attributableElementID, this.attributableElement));
        this.aProcessor.SetValue(FormHelper.AttributeSizeID, (object) this.commonData.Size);
      }
      if (this._okpCodeChanged)
      {
        if (this.aProcessor.FindAttributeValues(FormHelper.AttributeOKPCodeID) == null)
          this.aProcessor.ActualAttributeValues.Add(AttributeProcessor.CreateAttributeValues(FormHelper.AttributeOKPCodeID, this.attributableElementID, this.attributableElement));
        this.aProcessor.SetValue(FormHelper.AttributeOKPCodeID, (object) this.commonData.OKPCode);
      }
      if ((this._disAttrs & ArticleControl.DisableAttributes.Weight) == ArticleControl.DisableAttributes.None)
        this.aProcessor.SetValue(FormHelper.AttributeWeightID, (object) this._weight);
      if ((this._disAttrs & ArticleControl.DisableAttributes.Litera) == ArticleControl.DisableAttributes.None)
      {
        if (this.bEditLitera.SelectedItem is ArticleControl.LiteraValue selectedItem)
          this.aProcessor.SetValue(FormHelper.AttributeLiteraID, selectedItem.Value);
        else
          this.aProcessor.SetValue(FormHelper.AttributeLiteraID, (object) null);
      }
      this.aProcessor.SetValue(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"), (object) this.commonData.Designation);
      this.aProcessor.SetValue(FormHelper.AttributeNameID, (object) this.commonData.Name);
      this.aProcessor.Save();
      if (mode != OpenModes.Create)
        return;
      IDBObject dbObject = session.GetObject(this.attributableElementID);
      dbObject.CommitCreation(false);
      if (this.classifierID != 0L && session.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService)
      {
        // ISSUE: variable of a boxed type
        __Boxed<Guid> sessionGuid = (System.ValueType) session.SessionGUID;
        long classifierId = this.classifierID;
        long[] objectIDs = new long[1]{ dbObject.ObjectID };
        customService.IncludeObjects((object) sessionGuid, classifierId, objectIDs);
      }
      try
      {
        dbObject = dbObject.CheckOut(false);
      }
      catch (Exception ex)
      {
      }
      this.attributableElementID = dbObject.ObjectID;
      pair.ArticleID = dbObject.ObjectID;
    }
    catch
    {
      this.aProcessor.Load(this.attributableElementID, this.attributableElement, ClientConsts.GetAttributeValuesModes, false);
      throw;
    }
  }

  protected override void OnReload(IUserSession session, OpenModes mode)
  {
    if (mode == OpenModes.InViewReadOnly)
      this.SetAllReadOnly((Control) this);
    this._disAttrs = ArticleControl.DisableAttributes.None;
    IDBObjectType objectType = session.GetObjectType(this._articleType);
    if (!objectType.AnyAttributes)
    {
      if (objectType.Attributes.GetAttributeByID(FormHelper.AttributeWeightID) == null)
        this._disAttrs |= ArticleControl.DisableAttributes.Weight;
      if (objectType.Attributes.GetAttributeByID(FormHelper.AttributeLiteraID) == null)
        this._disAttrs |= ArticleControl.DisableAttributes.Litera;
    }
    this.ReloadCommonData(CommonDataType.All);
    if ((this._disAttrs & ArticleControl.DisableAttributes.Weight) == ArticleControl.DisableAttributes.None && this.aProcessor.FindAttributeValues(FormHelper.AttributeWeightID) != null)
    {
      object obj = this.aProcessor.GetValue(FormHelper.AttributeWeightID);
      if (CompareValuesHelper.NormalizedValue(obj) != null && obj is MeasuredValue)
      {
        this.tbWeigth.Text = ((MeasuredValue) obj).ToString();
        this._weight = (MeasuredValue) obj;
      }
      else
      {
        this.tbWeigth.Text = string.Empty;
        this._weight = (MeasuredValue) null;
      }
    }
    this.commonData.Material = new MaterialInfo(0L, string.Empty);
    if (this.aProcessor.FindAttributeValues(FormHelper.AttributeMaterialID) != null)
    {
      try
      {
        object obj = this.aProcessor.GetValue(FormHelper.AttributeMaterialID);
        if (obj != null)
        {
          if (obj != DBNull.Value)
          {
            long int64 = Convert.ToInt64(this.aProcessor.GetValue(FormHelper.AttributeMaterialID));
            QuickObjectInfo objectInfo = session.GetObjectInfo(int64);
            if (!objectInfo.Empty)
              this.commonData.Material = new MaterialInfo(int64, objectInfo.Caption);
          }
        }
      }
      catch
      {
      }
    }
    else
    {
      int height = this.tbWeigth.Top - this.tbMaterial.Top;
      this.label7.Visible = false;
      this.tbMaterial.Visible = false;
      this.bEditMaterial.Visible = false;
      Label label3 = this.label3;
      label3.Location = label3.Location - new Size(0, height);
      Label label8 = this.label8;
      label8.Location = label8.Location - new Size(0, height);
      Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbWeigth = this.tbWeigth;
      tbWeigth.Location = tbWeigth.Location - new Size(0, height);
      Button bEditWeigth = this.bEditWeigth;
      bEditWeigth.Location = bEditWeigth.Location - new Size(0, height);
      ComboBox bEditLitera = this.bEditLitera;
      bEditLitera.Location = bEditLitera.Location - new Size(0, height);
      this.Height -= height;
    }
    this._materialChanged = false;
    if (this.aProcessor.FindAttributeValues(FormHelper.AttributeSizeID) != null)
      this.commonData.Size = Convert.ToString(this.aProcessor.GetValue(FormHelper.AttributeSizeID));
    else
      this.commonData.Size = string.Empty;
    this._sizeChanged = false;
    if (this.aProcessor.FindAttributeValues(FormHelper.AttributeOKPCodeID) != null)
      this.commonData.OKPCode = Convert.ToString(this.aProcessor.GetValue(FormHelper.AttributeOKPCodeID));
    else
      this.commonData.OKPCode = string.Empty;
    this._okpCodeChanged = false;
    if ((this._disAttrs & ArticleControl.DisableAttributes.Litera) == ArticleControl.DisableAttributes.None)
    {
      int num = 0;
      this.bEditLitera.Items.Clear();
      this.bEditLitera.Items.Add((object) new ArticleControl.LiteraValue((object) null, string.Empty));
      IDBAttributeType attributeType = session.GetAttributeType(FormHelper.AttributeLiteraID, false);
      if (attributeType != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) attributeType.GetPossibleValues().Rows)
          this.bEditLitera.Items.Add((object) new ArticleControl.LiteraValue(row[attributeType.ValueFieldName], Convert.ToString(row["F_DESCRIPTION"])));
      }
      if (this.aProcessor.FindAttributeValues(FormHelper.AttributeLiteraID) != null)
      {
        object obj = this.aProcessor.GetValue(FormHelper.AttributeLiteraID);
        for (int index = 0; index < this.bEditLitera.Items.Count; ++index)
        {
          if (((ArticleControl.LiteraValue) this.bEditLitera.Items[index]).Value != obj)
          {
            num = index;
            break;
          }
        }
      }
      if (num > 0)
      {
        this.bEditLitera.SelectedIndexChanged -= new EventHandler(this.bEditLitera_SelectedIndexChanged);
        this.bEditLitera.SelectedIndex = num;
        this.bEditLitera.SelectedIndexChanged += new EventHandler(this.bEditLitera_SelectedIndexChanged);
      }
    }
    IDBObject dbObject = session.GetObject(this.attributableElementID, true);
    if (dbObject.ReadOnly)
    {
      this.Enabled = false;
    }
    else
    {
      if ((this.disableControls & CommonDataType.Designation) == CommonDataType.Designation)
      {
        this.tbDesignation.Enabled = false;
        this.bEditDesignation.Enabled = false;
      }
      if ((this.disableControls & CommonDataType.Name) == CommonDataType.Name)
      {
        this.tbName.Enabled = false;
        this.bEditName.Enabled = false;
      }
    }
    this.tbName.ReadOnly |= this.IsReadOnly(FormHelper.AttributeNameID);
    this.tbDesignation.ReadOnly |= this.IsReadOnly(FormHelper.AttributeDesignationID);
    this.bEditLitera.Enabled = (this._disAttrs & ArticleControl.DisableAttributes.Litera) == ArticleControl.DisableAttributes.None;
    this.tbWeigth.Enabled = this.bEditWeigth.Enabled = (this._disAttrs & ArticleControl.DisableAttributes.Litera) == ArticleControl.DisableAttributes.None;
    if (dbObject.ProjectID != 0L)
      this.lProject.Text = session.GetObject(dbObject.ProjectID).Caption;
    else
      this.lProject.Text = string.Empty;
    this.lLCStep.Text = session.GetLifecycleStep(dbObject.LCStep).LCName;
    this.lArtType.Text = session.GetObjectType(dbObject.ObjectType).ObjectTypeName;
    this.lVersion.Text = Convert.ToString(dbObject.VersionID);
    if (mode != OpenModes.View)
      return;
    this.Enabled = false;
  }

  /// <summary>Пришло сообщение об изменениии общих атрибутов</summary>
  protected override void OnCommonDataChanged(CommonDataType type) => this.ReloadCommonData(type);

  /// <summary>Перечитать общие атрибуты</summary>
  private void ReloadCommonData(CommonDataType type)
  {
    switch (type)
    {
      case CommonDataType.All:
        this.tbDesignation.Text = this.commonData.Designation;
        this.tbName.Text = this.commonData.Name;
        this.tbMaterial.Text = this.commonData.Material.Caption;
        this.OnChanged();
        break;
      case CommonDataType.Designation:
        this.tbDesignation.Text = this.commonData.Designation;
        this.OnChanged();
        break;
      case CommonDataType.Name:
        this.tbName.Text = this.commonData.Name;
        this.OnChanged();
        break;
      case CommonDataType.OKPCode:
        this._okpCodeChanged = true;
        this.OnChanged();
        break;
      case CommonDataType.Material:
        this._materialChanged = true;
        this.tbMaterial.Text = this.commonData.Material.Caption;
        this.OnChanged();
        break;
      case CommonDataType.Size:
        this._sizeChanged = true;
        this.OnChanged();
        break;
    }
  }

  /// <summary>Изменить значение атрибута "Масса"</summary>
  /// <param name="newWeigth"></param>
  private void ChangeWeigth(MeasuredValue newWeigth)
  {
    if (this._weight == null && newWeigth == null || (this._weight != null || newWeigth == null) && (this._weight == null || newWeigth != null) && MeasureHelper.Compare(this._weight, newWeigth) == CompareResult.Equal)
      return;
    if (newWeigth != null && MeasureHelper.FindDescriptor(newWeigth).Empty)
    {
      int num = (int) MessageBox.Show("Не найден описатель для введенной единицы измерения", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      this.tbWeigth.Text = this._weight != null ? this._weight.ToString() : string.Empty;
    }
    else
    {
      this.aProcessor.SetValue(FormHelper.AttributeWeightID, (object) newWeigth);
      this._weight = newWeigth;
      this.tbWeigth.Text = this._weight != null ? this._weight.ToString() : string.Empty;
      this.OnChanged();
    }
  }

  protected override void OnClassifier(ClassificatedObjects classif)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long clasifID = 0;
      IObjectClassificator classificator = this.GetClassificator(sessionKeeper.Session, classif, ref clasifID);
      if (classificator == null)
        return;
      this.OnSetClassifyAttributes(classificator, clasifID);
    }
  }

  /// <summary>Обработка события применения классификации</summary>
  /// <param name="values"></param>
  public override void OnSetClassifyAttributes(IObjectClassificator oc, long clasifID)
  {
    this.classifierID = clasifID;
    this.commonData.ClassifierID = clasifID;
    AttributeValues[] clasificatorAttributes = oc.GetClasificatorAttributes(this.attributableElementID);
    if (clasificatorAttributes == null || clasificatorAttributes.Length == 0)
      return;
    foreach (AttributeValues av in clasificatorAttributes)
    {
      if (av.Values != null && av.Values.Length != 0)
      {
        if (av.AttributeID == FormHelper.AttributeDesignationID)
          this.commonData.Designation = Convert.ToString(av.Values[0]);
        else if (av.AttributeID == FormHelper.AttributeNameID)
          this.commonData.Name = Convert.ToString(av.Values[0]);
        else if (av.AttributeID == FormHelper.AttributeWeightID)
        {
          MeasuredValue newWeigth = (MeasuredValue) null;
          if (av.Values[0] is MeasuredValue)
          {
            newWeigth = (MeasuredValue) av.Values[0];
          }
          else
          {
            string mValue = Convert.ToString(av.Values[0]);
            try
            {
              if (mValue != string.Empty)
                newWeigth = MeasureHelper.ConvertToMeasuredValue(mValue);
            }
            catch
            {
              continue;
            }
          }
          this.ChangeWeigth(newWeigth);
        }
        else if (av.AttributeID == FormHelper.AttributeLiteraID)
        {
          for (int index = 0; index < this.bEditLitera.Items.Count; ++index)
          {
            if (((ArticleControl.LiteraValue) this.bEditLitera.Items[index]).Value.Equals(av.Values[0]))
            {
              this.bEditLitera.SelectedIndex = index;
              break;
            }
          }
        }
        else if (this.aProcessor.FindAttributeValues(av.AttributeID) == null)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            try
            {
              this.CheckEnableAddAttribute(sessionKeeper.Session, oc, this._articleType, av);
            }
            catch
            {
              this.OnReloadData();
              throw;
            }
          }
        }
        else
          this.aProcessor.SetValues(av.AttributeID, av.Values);
      }
    }
  }

  /// <summary>Нажали кновку вызова редактора атрибута "Обозначение"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bEditDesignation_Click(object sender, EventArgs e)
  {
    this.OnEditDesignation(this.attributableElementID);
  }

  /// <summary>
  /// Нажали кновку вызова редактора атрибута "Наименование"
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bEditName_Click(object sender, EventArgs e)
  {
    this.OnEditName(this.attributableElementID);
  }

  /// <summary>Вышли из поля редактирования атрибута "Масса"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbWeigth_Leave(object sender, EventArgs e)
  {
    MeasuredValue newWeigth = (MeasuredValue) null;
    try
    {
      MeasureDescriptor descriptor = MeasureHelper.FindDescriptor("кг");
      if (this.tbWeigth.Text != string.Empty)
        newWeigth = MeasureHelper.ConvertToMeasuredValue(this.tbWeigth.Text, descriptor, false);
    }
    catch (Exception ex)
    {
      double result = 0.0;
      bool flag = false;
      string text = ex.Message;
      if (double.TryParse(this.tbWeigth.Text, out result) && this.attributableElement == AttributableElements.Object)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this.attributableElementID);
          IDBAttributeType4Object attributeById = sessionKeeper.Session.GetObjectType(dbObject.ObjectType).Attributes.GetAttributeByID(FormHelper.AttributeWeightID) as IDBAttributeType4Object;
          if ((attributeById as IDBMeasureAttributeType).DefaultMeasureID != 0L)
          {
            newWeigth = new MeasuredValue(result, (attributeById as IDBMeasureAttributeType).DefaultMeasureID);
            flag = true;
          }
        }
      }
      else
        text = $"Невозможно преобразовать \"{this.tbWeigth.Text}\" в вещественное значение";
      if (!flag)
      {
        int num = (int) MessageBox.Show(text, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.tbWeigth.Text = this._weight != null ? this._weight.ToString() : string.Empty;
        return;
      }
    }
    this.ChangeWeigth(newWeigth);
  }

  private void bEditWeigth_Click(object sender, EventArgs e)
  {
    object obj = this.ChangeInEditor(FormHelper.AttributeWeightID, (object) this._weight);
    MeasuredValue newWeigth = (MeasuredValue) null;
    if (obj is MeasuredValue)
    {
      newWeigth = (MeasuredValue) obj;
    }
    else
    {
      string mValue = Convert.ToString(obj);
      try
      {
        if (mValue != string.Empty)
          newWeigth = MeasureHelper.ConvertToMeasuredValue(mValue);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.tbWeigth.Text = this._weight != null ? this._weight.ToString() : string.Empty;
        return;
      }
    }
    this.ChangeWeigth(newWeigth);
  }

  /// <summary>
  /// Вышли из поля для редактирования атрибута "Обозначение"
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbDesignation_Leave(object sender, EventArgs e)
  {
    this.OnDesignationLeave(this.tbDesignation.Text);
  }

  /// <summary>
  /// Вышли из поля для редактирования атрибута "Наименование"
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbName_Leave(object sender, EventArgs e) => this.OnNameLeave(this.tbName.Text);

  /// <summary>Нажали кнопку "Классифицировать"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bClassificate_Click(object sender, EventArgs e)
  {
    this.OnClassifier(new ClassificatedObjects()
    {
      articleID = this.attributableElementID,
      articleType = this._articleType
    });
  }

  /// <summary>Изменили атрибут "Литера"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bEditLitera_SelectedIndexChanged(object sender, EventArgs e) => this.OnChanged();

  private void BEditMaterialClick(object sender, EventArgs e)
  {
    object obj = this.ChangeInEditor(FormHelper.AttributeMaterialID, (object) this.commonData.Material.ObjectID);
    try
    {
      if (CompareValuesHelper.NormalizedValue(obj) == null)
      {
        this.commonData.Material = new MaterialInfo(0L, string.Empty);
      }
      else
      {
        long int64 = Convert.ToInt64(obj);
        if (this.commonData.Material.ObjectID == int64)
          return;
        MaterialInfo material = this.GetMaterial(int64);
        this.tbMaterial.Text = material.Caption;
        this.commonData.Material = material;
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

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
    this.bClassificate = new Button();
    this.bEditName = new Button();
    this.bEditDesignation = new Button();
    this.label2 = new Label();
    this.label1 = new Label();
    this.tbName = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.tbDesignation = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.bEditWeigth = new Button();
    this.label8 = new Label();
    this.tbWeigth = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label3 = new Label();
    this.label6 = new Label();
    this.lVersion = new Label();
    this.lLCStep = new Label();
    this.lProject = new Label();
    this.label5 = new Label();
    this.label4 = new Label();
    this.panel1 = new Panel();
    this.lArtType = new Label();
    this.panel2 = new Panel();
    this.bEditLitera = new ComboBox();
    this.tbMaterial = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.label7 = new Label();
    this.bEditMaterial = new Button();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.bClassificate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bClassificate.Image = (Image) Resources.classify;
    this.bClassificate.Location = new Point(533, 11);
    this.bClassificate.Name = "bClassificate";
    this.bClassificate.Size = new Size(26, 26);
    this.bClassificate.TabIndex = 6;
    this.bClassificate.UseVisualStyleBackColor = true;
    this.bClassificate.Click += new EventHandler(this.bClassificate_Click);
    this.bEditName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditName.Location = new Point(532, 97);
    this.bEditName.Name = "bEditName";
    this.bEditName.Size = new Size(24, 23);
    this.bEditName.TabIndex = 26;
    this.bEditName.TabStop = false;
    this.bEditName.Text = "...";
    this.bEditName.UseVisualStyleBackColor = true;
    this.bEditName.Click += new EventHandler(this.bEditName_Click);
    this.bEditDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditDesignation.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.bEditDesignation.Location = new Point(532, 70);
    this.bEditDesignation.Name = "bEditDesignation";
    this.bEditDesignation.Size = new Size(24, 23);
    this.bEditDesignation.TabIndex = 6;
    this.bEditDesignation.TabStop = false;
    this.bEditDesignation.Text = "...";
    this.bEditDesignation.UseVisualStyleBackColor = true;
    this.bEditDesignation.Click += new EventHandler(this.bEditDesignation_Click);
    this.label2.AutoSize = true;
    this.label2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label2.Location = new Point(25, 102);
    this.label2.Name = "label2";
    this.label2.Size = new Size(99, 13);
    this.label2.TabIndex = 24;
    this.label2.Text = "Наименование:";
    this.label1.AutoSize = true;
    this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label1.Location = new Point(25, 76);
    this.label1.Name = "label1";
    this.label1.Size = new Size(89, 13);
    this.label1.TabIndex = 23;
    this.label1.Text = "Обозначение:";
    this.tbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbName.BackColor = Color.White;
    this.tbName.Location = new Point(145, 98);
    this.tbName.Name = "tbName";
    this.tbName.Size = new Size(387, 20);
    this.tbName.TabIndex = 1;
    this.tbName.Leave += new EventHandler(this.tbName_Leave);
    this.tbDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbDesignation.BackColor = Color.White;
    this.tbDesignation.Location = new Point(145, 72);
    this.tbDesignation.Name = "tbDesignation";
    this.tbDesignation.Size = new Size(387, 20);
    this.tbDesignation.TabIndex = 0;
    this.tbDesignation.Leave += new EventHandler(this.tbDesignation_Leave);
    this.bEditWeigth.Location = new Point(294, 152);
    this.bEditWeigth.Name = "bEditWeigth";
    this.bEditWeigth.Size = new Size(24, 23);
    this.bEditWeigth.TabIndex = 4;
    this.bEditWeigth.TabStop = false;
    this.bEditWeigth.Text = "...";
    this.bEditWeigth.UseVisualStyleBackColor = true;
    this.bEditWeigth.Click += new EventHandler(this.bEditWeigth_Click);
    this.label8.AutoSize = true;
    this.label8.Location = new Point(25, 157);
    this.label8.Name = "label8";
    this.label8.Size = new Size(43, 13);
    this.label8.TabIndex = 29;
    this.label8.Text = "Масса:";
    this.tbWeigth.BackColor = Color.White;
    this.tbWeigth.Location = new Point(145, 153);
    this.tbWeigth.Name = "tbWeigth";
    this.tbWeigth.Size = new Size(149, 20);
    this.tbWeigth.TabIndex = 4;
    this.tbWeigth.Leave += new EventHandler(this.tbWeigth_Leave);
    this.label3.AutoSize = true;
    this.label3.Location = new Point(25, 183);
    this.label3.Name = "label3";
    this.label3.Size = new Size(47, 13);
    this.label3.TabIndex = 32 /*0x20*/;
    this.label3.Text = "Литера:";
    this.label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.label6.AutoSize = true;
    this.label6.Location = new Point(409, 16 /*0x10*/);
    this.label6.Name = "label6";
    this.label6.Size = new Size(47, 13);
    this.label6.TabIndex = 38;
    this.label6.Text = "Версия:";
    this.lVersion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.lVersion.AutoSize = true;
    this.lVersion.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lVersion.Location = new Point(461, 16 /*0x10*/);
    this.lVersion.Name = "lVersion";
    this.lVersion.Size = new Size(14, 13);
    this.lVersion.TabIndex = 39;
    this.lVersion.Text = "0";
    this.lLCStep.AutoSize = true;
    this.lLCStep.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lLCStep.Location = new Point(150, 40);
    this.lLCStep.Name = "lLCStep";
    this.lLCStep.Size = new Size(41, 13);
    this.lLCStep.TabIndex = 52;
    this.lLCStep.Text = "label7";
    this.lProject.AutoSize = true;
    this.lProject.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lProject.Location = new Point(150, 15);
    this.lProject.Name = "lProject";
    this.lProject.Size = new Size(41, 13);
    this.lProject.TabIndex = 51;
    this.lProject.Text = "label7";
    this.label5.AutoSize = true;
    this.label5.Location = new Point(22, 40);
    this.label5.Name = "label5";
    this.label5.Size = new Size((int) sbyte.MaxValue, 13);
    this.label5.TabIndex = 50;
    this.label5.Text = "Шаг жизненного цикла:";
    this.label4.AutoSize = true;
    this.label4.Location = new Point(22, 15);
    this.label4.Name = "label4";
    this.label4.Size = new Size(122, 13);
    this.label4.TabIndex = 49;
    this.label4.Text = "Принадлежит проекту:";
    this.panel1.BackColor = SystemColors.ControlLight;
    this.panel1.Controls.Add((Control) this.lArtType);
    this.panel1.Controls.Add((Control) this.bClassificate);
    this.panel1.Controls.Add((Control) this.label6);
    this.panel1.Controls.Add((Control) this.lVersion);
    this.panel1.Dock = DockStyle.Top;
    this.panel1.Location = new Point(3, 3);
    this.panel1.Name = "panel1";
    this.panel1.Padding = new Padding(3);
    this.panel1.Size = new Size(574, 47);
    this.panel1.TabIndex = 6;
    this.lArtType.AutoSize = true;
    this.lArtType.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lArtType.Location = new Point(17, 16 /*0x10*/);
    this.lArtType.Name = "lArtType";
    this.lArtType.Size = new Size(41, 13);
    this.lArtType.TabIndex = 0;
    this.lArtType.Text = "label7";
    this.panel2.BackColor = SystemColors.ControlLight;
    this.panel2.Controls.Add((Control) this.label4);
    this.panel2.Controls.Add((Control) this.label5);
    this.panel2.Controls.Add((Control) this.lLCStep);
    this.panel2.Controls.Add((Control) this.lProject);
    this.panel2.Dock = DockStyle.Bottom;
    this.panel2.Location = new Point(3, 267);
    this.panel2.Margin = new Padding(0);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(574, 80 /*0x50*/);
    this.panel2.TabIndex = 54;
    this.bEditLitera.DropDownStyle = ComboBoxStyle.DropDownList;
    this.bEditLitera.FormattingEnabled = true;
    this.bEditLitera.Location = new Point(145, 179);
    this.bEditLitera.Name = "bEditLitera";
    this.bEditLitera.Size = new Size(173, 21);
    this.bEditLitera.TabIndex = 5;
    this.bEditLitera.SelectedIndexChanged += new EventHandler(this.bEditLitera_SelectedIndexChanged);
    this.tbMaterial.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbMaterial.Location = new Point(145, 125);
    this.tbMaterial.Name = "tbMaterial";
    this.tbMaterial.ReadOnly = true;
    this.tbMaterial.Size = new Size(387, 20);
    this.tbMaterial.TabIndex = 2;
    this.label7.AutoSize = true;
    this.label7.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label7.Location = new Point(25, 129);
    this.label7.Name = "label7";
    this.label7.Size = new Size(65, 13);
    this.label7.TabIndex = 24;
    this.label7.Text = "Материал";
    this.bEditMaterial.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditMaterial.Location = new Point(532, 124);
    this.bEditMaterial.Name = "bEditMaterial";
    this.bEditMaterial.Size = new Size(24, 23);
    this.bEditMaterial.TabIndex = 3;
    this.bEditMaterial.TabStop = false;
    this.bEditMaterial.Text = "...";
    this.bEditMaterial.UseVisualStyleBackColor = true;
    this.bEditMaterial.Click += new EventHandler(this.BEditMaterialClick);
    this.AutoScaleDimensions = new SizeF(96f, 96f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.AutoScroll = true;
    this.Controls.Add((Control) this.bEditLitera);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.bEditWeigth);
    this.Controls.Add((Control) this.label8);
    this.Controls.Add((Control) this.tbWeigth);
    this.Controls.Add((Control) this.bEditMaterial);
    this.Controls.Add((Control) this.bEditName);
    this.Controls.Add((Control) this.label7);
    this.Controls.Add((Control) this.bEditDesignation);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.tbMaterial);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.tbName);
    this.Controls.Add((Control) this.tbDesignation);
    this.MinimumSize = new Size(580, 350);
    this.Name = nameof (ArticleControl);
    this.Padding = new Padding(3);
    this.Size = new Size(580, 350);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  internal class LiteraValue
  {
    public object Value;
    public string Description;

    public LiteraValue(object val, string descr)
    {
      this.Value = val;
      this.Description = descr != string.Empty ? descr : Convert.ToString(val);
    }

    public override string ToString() => this.Description;
  }

  /// <summary>Флаги атрибутов которые необходимо дизаблить</summary>
  private enum DisableAttributes
  {
    None,
    Weight,
    Litera,
  }
}
