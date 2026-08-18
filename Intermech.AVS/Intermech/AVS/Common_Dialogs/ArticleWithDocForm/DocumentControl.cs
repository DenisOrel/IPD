// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.DocumentControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Properties;
using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>Закладка с атрибутами документа</summary>
internal class DocumentControl : PageUserControl
{
  /// <summary>Тип документа</summary>
  private int _documentType;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button bClassificate;
  private Button bEditFormat;
  private Label label8;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbFormat;
  private Panel panel1;
  private Label lDocType;
  private Label label7;
  private Label lVersion;
  private Button bEditName;
  private Button bEditDesignation;
  private Label label2;
  private Label label1;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbName;
  private Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox tbDesignation;
  private Panel panel2;
  private Label label4;
  private Label label5;
  private Label lLCStep;
  private Label lProject;

  public DocumentControl(
    IDBObject document,
    CommonDataType disableControls,
    IPageControl firstPage)
    : base(document.ObjectID, AttributableElements.Object, disableControls)
  {
    this.InitializeComponent();
    this.Init(document, disableControls, firstPage);
  }

  internal void Init(IDBObject document, CommonDataType disableControls, IPageControl firstPage)
  {
    this.Init(document.ObjectID, AttributableElements.Object, disableControls);
    this._documentType = document.ObjectType;
    firstPage.GetEditorEvent -= new GetEditorDelegate(this.firstPage_GetEditorEvent);
    firstPage.GetEditorEvent += new GetEditorDelegate(this.firstPage_GetEditorEvent);
  }

  private object firstPage_GetEditorEvent(object sender, GetEditorEventArgs args)
  {
    if (args.Handled)
      return (object) null;
    if (args.AttributeID != FormHelper.AttributeFormatID)
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
      IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(session.GetObjectInfo(this.attributableElementID).ObjectTypeID, FormHelper.AttributeFormatID);
      if (this.aProcessor.FindAttributeValues(FormHelper.AttributeFormatID) == null && attribute4ObjectType != null)
        this.aProcessor.ActualAttributeValues.Add(AttributeProcessor.CreateAttributeValues(FormHelper.AttributeFormatID, this.attributableElementID, this.attributableElement));
      if (attribute4ObjectType != null)
        this.aProcessor.SetValue(FormHelper.AttributeFormatID, (object) this.commonData.Format);
      this.aProcessor.SetValue(FormHelper.AttributeDesignationID, (object) this.commonData.Designation);
      this.aProcessor.SetValue(FormHelper.AttributeNameID, (object) this.commonData.Name);
      this.aProcessor.Save();
      if (mode != OpenModes.Create)
        return;
      IDBObject dBObject = session.GetObject(this.attributableElementID);
      foreach (IDBAttribute attr in dBObject.Attributes.GetAttributesByType(FieldTypes.ftFile))
      {
        if (attr.IsNull)
          SetFileAttrPrototype.Execute(attr, session, dBObject);
      }
      dBObject.CommitCreation(false);
      if (this.classifierID != 0L && session.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService)
      {
        // ISSUE: variable of a boxed type
        __Boxed<Guid> sessionGuid = (ValueType) session.SessionGUID;
        long classifierId = this.classifierID;
        long[] objectIDs = new long[1]{ dBObject.ObjectID };
        customService.IncludeObjects((object) sessionGuid, classifierId, objectIDs);
      }
      IDBObject dbObject = dBObject.CheckOut(false);
      this.attributableElementID = dbObject.ObjectID;
      pair.DocumentID = dbObject.ObjectID;
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
    this.ReloadCommonData(CommonDataType.All);
    if (this.aProcessor.FindAttributeValues(FormHelper.AttributeFormatID) != null)
    {
      object obj = this.aProcessor.GetValue(FormHelper.AttributeFormatID);
      this.tbFormat.Text = CompareValuesHelper.NormalizedValue(obj) != null ? Convert.ToString(obj) : string.Empty;
      this.commonData.Format = this.tbFormat.Text;
    }
    IDBObject dbObject = session.GetObject(this.attributableElementID);
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
    this.lDocType.Text = session.GetObjectType(dbObject.ObjectType).ObjectTypeName;
    if (dbObject.ProjectID != 0L)
      this.lProject.Text = session.GetObject(dbObject.ProjectID).Caption;
    else
      this.lProject.Text = string.Empty;
    this.lLCStep.Text = session.GetLifecycleStep(dbObject.LCStep).LCName;
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
        this.tbFormat.Text = this.commonData.Format;
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
      case CommonDataType.Format:
        this.tbFormat.Text = this.commonData.Format;
        this.OnChanged();
        break;
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
        else if (av.AttributeID == FormHelper.AttributeFormatID)
          this.commonData.Format = Convert.ToString(av.Values[0]);
        else if (this.aProcessor.FindAttributeValues(av.AttributeID) == null)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            try
            {
              this.CheckEnableAddAttribute(sessionKeeper.Session, oc, this._documentType, av);
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

  /// <summary>Нажали кновку вызова редактора атрибута "Формат"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bEditFormat_Click(object sender, EventArgs e)
  {
    this.OnEditFormat(this.attributableElementID);
  }

  /// <summary>Вышли из поля для редактирования атрибута "Формат"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbFormat_Leave(object sender, EventArgs e) => this.OnFormatLeave(this.tbFormat.Text);

  /// <summary>Нажали кнопку "Классифицировать"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bClassificate_Click(object sender, EventArgs e)
  {
    this.OnClassifier(new ClassificatedObjects()
    {
      documentID = this.attributableElementID,
      documentType = this._documentType
    });
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
    this.bEditFormat = new Button();
    this.label8 = new Label();
    this.tbFormat = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.panel1 = new Panel();
    this.lDocType = new Label();
    this.label7 = new Label();
    this.lVersion = new Label();
    this.bEditName = new Button();
    this.bEditDesignation = new Button();
    this.label2 = new Label();
    this.label1 = new Label();
    this.tbName = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.tbDesignation = new Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox();
    this.panel2 = new Panel();
    this.label4 = new Label();
    this.label5 = new Label();
    this.lLCStep = new Label();
    this.lProject = new Label();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.bClassificate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bClassificate.Image = (Image) Resources.classify;
    this.bClassificate.Location = new Point(533, 11);
    this.bClassificate.Name = "bClassificate";
    this.bClassificate.Size = new Size(26, 26);
    this.bClassificate.TabIndex = 58;
    this.bClassificate.UseVisualStyleBackColor = true;
    this.bClassificate.Click += new EventHandler(this.bClassificate_Click);
    this.bEditFormat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditFormat.Location = new Point(294, 138);
    this.bEditFormat.Name = "bEditFormat";
    this.bEditFormat.Size = new Size(24, 23);
    this.bEditFormat.TabIndex = 3;
    this.bEditFormat.TabStop = false;
    this.bEditFormat.Text = "...";
    this.bEditFormat.UseVisualStyleBackColor = true;
    this.bEditFormat.Click += new EventHandler(this.bEditFormat_Click);
    this.label8.AutoSize = true;
    this.label8.Location = new Point(25, 143);
    this.label8.Name = "label8";
    this.label8.Size = new Size(52, 13);
    this.label8.TabIndex = 38;
    this.label8.Text = "Формат:";
    this.tbFormat.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbFormat.BackColor = Color.White;
    this.tbFormat.Location = new Point(147, 139);
    this.tbFormat.Name = "tbFormat";
    this.tbFormat.Size = new Size(147, 20);
    this.tbFormat.TabIndex = 2;
    this.tbFormat.Leave += new EventHandler(this.tbFormat_Leave);
    this.panel1.BackColor = SystemColors.ControlLight;
    this.panel1.Controls.Add((Control) this.lDocType);
    this.panel1.Controls.Add((Control) this.label7);
    this.panel1.Controls.Add((Control) this.lVersion);
    this.panel1.Controls.Add((Control) this.bClassificate);
    this.panel1.Dock = DockStyle.Top;
    this.panel1.Location = new Point(3, 3);
    this.panel1.Name = "panel1";
    this.panel1.Padding = new Padding(3);
    this.panel1.Size = new Size(574, 47);
    this.panel1.TabIndex = 4;
    this.lDocType.AutoSize = true;
    this.lDocType.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lDocType.Location = new Point(17, 16 /*0x10*/);
    this.lDocType.Name = "lDocType";
    this.lDocType.Size = new Size(41, 13);
    this.lDocType.TabIndex = 0;
    this.lDocType.Text = "label7";
    this.label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.label7.AutoSize = true;
    this.label7.Location = new Point(409, 16 /*0x10*/);
    this.label7.Name = "label7";
    this.label7.Size = new Size(47, 13);
    this.label7.TabIndex = 38;
    this.label7.Text = "Версия:";
    this.lVersion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.lVersion.AutoSize = true;
    this.lVersion.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lVersion.Location = new Point(461, 16 /*0x10*/);
    this.lVersion.Name = "lVersion";
    this.lVersion.Size = new Size(14, 13);
    this.lVersion.TabIndex = 39;
    this.lVersion.Text = "0";
    this.bEditName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditName.Location = new Point(532, 97);
    this.bEditName.Name = "bEditName";
    this.bEditName.Size = new Size(24, 23);
    this.bEditName.TabIndex = 60;
    this.bEditName.TabStop = false;
    this.bEditName.Text = "...";
    this.bEditName.UseVisualStyleBackColor = true;
    this.bEditName.Click += new EventHandler(this.bEditName_Click);
    this.bEditDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditDesignation.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.bEditDesignation.Location = new Point(532, 70);
    this.bEditDesignation.Name = "bEditDesignation";
    this.bEditDesignation.Size = new Size(24, 23);
    this.bEditDesignation.TabIndex = 59;
    this.bEditDesignation.TabStop = false;
    this.bEditDesignation.Text = "...";
    this.bEditDesignation.UseVisualStyleBackColor = true;
    this.bEditDesignation.Click += new EventHandler(this.bEditDesignation_Click);
    this.label2.AutoSize = true;
    this.label2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label2.Location = new Point(25, 102);
    this.label2.Name = "label2";
    this.label2.Size = new Size(99, 13);
    this.label2.TabIndex = 58;
    this.label2.Text = "Наименование:";
    this.label1.AutoSize = true;
    this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label1.Location = new Point(25, 76);
    this.label1.Name = "label1";
    this.label1.Size = new Size(89, 13);
    this.label1.TabIndex = 57;
    this.label1.Text = "Обозначение:";
    this.tbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbName.BackColor = Color.White;
    this.tbName.Location = new Point(147, 98);
    this.tbName.Name = "tbName";
    this.tbName.Size = new Size(385, 20);
    this.tbName.TabIndex = 1;
    this.tbName.Leave += new EventHandler(this.tbName_Leave);
    this.tbDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbDesignation.BackColor = Color.White;
    this.tbDesignation.Location = new Point(147, 72);
    this.tbDesignation.Name = "tbDesignation";
    this.tbDesignation.Size = new Size(385, 20);
    this.tbDesignation.TabIndex = 0;
    this.tbDesignation.Leave += new EventHandler(this.tbDesignation_Leave);
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
    this.panel2.TabIndex = 61;
    this.label4.AutoSize = true;
    this.label4.Location = new Point(22, 15);
    this.label4.Name = "label4";
    this.label4.Size = new Size(122, 13);
    this.label4.TabIndex = 49;
    this.label4.Text = "Принадлежит проекту:";
    this.label5.AutoSize = true;
    this.label5.Location = new Point(22, 40);
    this.label5.Name = "label5";
    this.label5.Size = new Size((int) sbyte.MaxValue, 13);
    this.label5.TabIndex = 50;
    this.label5.Text = "Шаг жизненного цикла:";
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
    this.AutoScaleDimensions = new SizeF(96f, 96f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.AutoScroll = true;
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.bEditName);
    this.Controls.Add((Control) this.bEditDesignation);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.tbName);
    this.Controls.Add((Control) this.tbDesignation);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.bEditFormat);
    this.Controls.Add((Control) this.label8);
    this.Controls.Add((Control) this.tbFormat);
    this.MinimumSize = new Size(580, 350);
    this.Name = nameof (DocumentControl);
    this.Padding = new Padding(3);
    this.Size = new Size(580, 350);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
