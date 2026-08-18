// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ImbaseCatalogsEditor
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.AVSProperties;
using Intermech.AVS.Properties;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

public class ImbaseCatalogsEditor : Form
{
  private long TemplateId;
  private SettingsStructure settingsStructure;
  private bool enableEditTemplate;
  private AVSCommonPropertiesSchema avsCommonPropertiesSchema;
  private SectionItemList ImBaseCatalogs;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private DataGridView dgImBase;
  private DataGridViewTextBoxColumn Column3;
  private Label label4;
  private Button bDeleteImBase;
  private Button bEditImBase;
  private Button bAddImBase;
  private TextBox textInfo;
  private Button bOk;
  private Button bCancel;

  public ImbaseCatalogsEditor(SettingsStructure settingsStructure, long templateId)
  {
    this.InitializeComponent();
    this.settingsStructure = settingsStructure;
    this.TemplateId = templateId;
    int objectType = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      objectType = sessionKeeper.Session.GetObjectInfo(templateId).ObjectTypeID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.avsCommonPropertiesSchema = (AVSCommonPropertiesSchema) settingsStructure.CreateSettingsLevelFromObject(sessionKeeper.Session, templateId, objectType, -1L, AvsIDCache.Attr_ConstructorDocumentProperties, typeof (AVSCommonPropertiesSchema));
      this.enableEditTemplate = !this.avsCommonPropertiesSchema.ReadOnly;
    }
    this.ImBaseCatalogs = new SectionItemList(AvsIDCache.Attr_RefToImBaseDirectory);
    foreach (Guid imbaseCatalog in this.avsCommonPropertiesSchema.ImbaseCatalogs)
    {
      if (imbaseCatalog != Guid.Empty)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this.ImBaseCatalogs.Add(new SectionItem((object) sessionKeeper.Session.GetObjectInfo(imbaseCatalog).ObjectID, false, this.ImBaseCatalogs));
      }
    }
    this.UpdateControls();
  }

  private void bAddImBase_Click(object sender, EventArgs e)
  {
    UITypeEditor editor = this.ImBaseCatalogs.Editor;
    if (this.ImBaseCatalogs.Editor != null)
    {
      object propDescriptorValue = this.ImBaseCatalogs.Describer.GetPropDescriptorValue((IElementInfo) null, this.ImBaseCatalogs.AttrId, (object) null);
      object obj = editor.EditValue((System.IServiceProvider) null, propDescriptorValue);
      if (obj != null && obj.ToString() != null)
        this.ImBaseCatalogs.Add(new SectionItem(obj, true, this.ImBaseCatalogs));
    }
    this.UpdateControls();
  }

  private void bEditImBase_Click(object sender, EventArgs e)
  {
    int index = this.dgImBase.SelectedRows[0].Index;
    UITypeEditor editor = this.ImBaseCatalogs.Editor;
    if (this.ImBaseCatalogs.Editor != null)
    {
      object obj = editor.EditValue((System.IServiceProvider) null, this.ImBaseCatalogs[index].PropValue);
      this.ImBaseCatalogs[index].PropValue = obj;
    }
    this.UpdateControls();
  }

  private void bDeleteImBase_Click(object sender, EventArgs e)
  {
    this.ImBaseCatalogs.RemoveAt(this.dgImBase.SelectedRows[0].Index);
    this.UpdateControls();
  }

  public void UpdateControls()
  {
    this.textInfo.Visible = !this.enableEditTemplate;
    this.bAddImBase.Enabled = this.enableEditTemplate;
    this.bEditImBase.Enabled = this.enableEditTemplate;
    this.bDeleteImBase.Enabled = this.enableEditTemplate;
    this.dgImBase.Rows.Clear();
    foreach (SectionItem imBaseCatalog in (List<SectionItem>) this.ImBaseCatalogs)
      this.dgImBase.Rows.Add(new object[1]
      {
        this.ImBaseCatalogs.Converter.ConvertTo(imBaseCatalog.PropValue, typeof (string))
      });
  }

  private void bOk_Click(object sender, EventArgs e) => this.Save();

  private void Save()
  {
    this.avsCommonPropertiesSchema.ImbaseCatalogs.Clear();
    foreach (SectionItem imBaseCatalog in (List<SectionItem>) this.ImBaseCatalogs)
    {
      long int64 = Convert.ToInt64(imBaseCatalog.Value);
      if (int64 != 0L)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this.avsCommonPropertiesSchema.ImbaseCatalogs.Add(sessionKeeper.Session.GetObjectInfo(int64).VersionGuid);
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.TemplateId, false);
      if (dbObject == null)
        return;
      bool flag = false;
      if (dbObject.CheckoutBy == 0L && dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
      {
        dbObject = dbObject.CheckOut();
        flag = true;
      }
      if (dbObject.CheckoutBy != sessionKeeper.Session.UserID && dbObject.ObjectModifyMode != ObjectModifyModes.InBase)
        return;
      this.avsCommonPropertiesSchema.SaveParams();
      if (AVSPlugin.NotificationService != null)
        AVSPlugin.NotificationService.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs(dbObject.ObjectID, dbObject.ObjectType, new AttributeValues(AvsIDCache.Attr_AllowableSections, (object) null), new AttributeValues(AvsIDCache.Attr_AllowableSections, (object) null)));
      if (!flag)
        return;
      dbObject.CheckIn();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseCatalogsEditor));
    this.dgImBase = new DataGridView();
    this.Column3 = new DataGridViewTextBoxColumn();
    this.label4 = new Label();
    this.bDeleteImBase = new Button();
    this.bEditImBase = new Button();
    this.bAddImBase = new Button();
    this.textInfo = new TextBox();
    this.bOk = new Button();
    this.bCancel = new Button();
    ((ISupportInitialize) this.dgImBase).BeginInit();
    this.SuspendLayout();
    this.dgImBase.AllowUserToAddRows = false;
    this.dgImBase.AllowUserToDeleteRows = false;
    this.dgImBase.AllowUserToResizeColumns = false;
    this.dgImBase.AllowUserToResizeRows = false;
    this.dgImBase.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.dgImBase.BackgroundColor = Color.White;
    this.dgImBase.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dgImBase.ColumnHeadersVisible = false;
    this.dgImBase.Columns.AddRange((DataGridViewColumn) this.Column3);
    this.dgImBase.Location = new Point(5, 21);
    this.dgImBase.MultiSelect = false;
    this.dgImBase.Name = "dgImBase";
    this.dgImBase.ReadOnly = true;
    this.dgImBase.RowHeadersVisible = false;
    this.dgImBase.RowTemplate.Height = 20;
    this.dgImBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dgImBase.Size = new Size(478, 208 /*0xD0*/);
    this.dgImBase.TabIndex = 20;
    this.Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.Column3.HeaderText = "Column3";
    this.Column3.Name = "Column3";
    this.Column3.ReadOnly = true;
    this.label4.AutoSize = true;
    this.label4.Location = new Point(7, 5);
    this.label4.Name = "label4";
    this.label4.Size = new Size(142, 13);
    this.label4.TabIndex = 19;
    this.label4.Text = "Ссылка на каталог ImBase";
    this.bDeleteImBase.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bDeleteImBase.Image = (Image) componentResourceManager.GetObject("bDeleteImBase.Image");
    this.bDeleteImBase.Location = new Point(483, 67);
    this.bDeleteImBase.Name = "bDeleteImBase";
    this.bDeleteImBase.Size = new Size(23, 24);
    this.bDeleteImBase.TabIndex = 16 /*0x10*/;
    this.bDeleteImBase.UseVisualStyleBackColor = true;
    this.bDeleteImBase.Click += new EventHandler(this.bDeleteImBase_Click);
    this.bEditImBase.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bEditImBase.Image = (Image) componentResourceManager.GetObject("bEditImBase.Image");
    this.bEditImBase.Location = new Point(483, 43);
    this.bEditImBase.Name = "bEditImBase";
    this.bEditImBase.Size = new Size(23, 24);
    this.bEditImBase.TabIndex = 17;
    this.bEditImBase.UseVisualStyleBackColor = true;
    this.bEditImBase.Click += new EventHandler(this.bEditImBase_Click);
    this.bAddImBase.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bAddImBase.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.bAddImBase.Image = (Image) Resources.AddStandart;
    this.bAddImBase.Location = new Point(483, 19);
    this.bAddImBase.Name = "bAddImBase";
    this.bAddImBase.Size = new Size(23, 24);
    this.bAddImBase.TabIndex = 18;
    this.bAddImBase.UseVisualStyleBackColor = true;
    this.bAddImBase.Click += new EventHandler(this.bAddImBase_Click);
    this.textInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.textInfo.BackColor = SystemColors.Info;
    this.textInfo.ForeColor = SystemColors.InfoText;
    this.textInfo.Location = new Point(10, 235);
    this.textInfo.Multiline = true;
    this.textInfo.Name = "textInfo";
    this.textInfo.ReadOnly = true;
    this.textInfo.Size = new Size(234, 47);
    this.textInfo.TabIndex = 21;
    this.textInfo.Text = "Редактирование шаблона документа запрещено. Возможно он взят на редактирование другим пользователем\r\n";
    this.bOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOk.DialogResult = DialogResult.Yes;
    this.bOk.Location = new Point(258, (int) byte.MaxValue);
    this.bOk.Name = "bOk";
    this.bOk.Size = new Size(121, 27);
    this.bOk.TabIndex = 23;
    this.bOk.Text = "ОК";
    this.bOk.UseVisualStyleBackColor = true;
    this.bOk.Click += new EventHandler(this.bOk_Click);
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(385, (int) byte.MaxValue);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 22;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(510, 294);
    this.Controls.Add((Control) this.textInfo);
    this.Controls.Add((Control) this.bOk);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.dgImBase);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.bDeleteImBase);
    this.Controls.Add((Control) this.bEditImBase);
    this.Controls.Add((Control) this.bAddImBase);
    this.MinimumSize = new Size(526, 332);
    this.Name = nameof (ImbaseCatalogsEditor);
    this.ShowInTaskbar = false;
    this.Text = "Редактор  каталогов Imbase";
    ((ISupportInitialize) this.dgImBase).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
