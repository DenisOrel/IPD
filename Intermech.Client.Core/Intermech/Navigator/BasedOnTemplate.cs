
// Type: Intermech.Navigator.BasedOnTemplate
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>
/// для создания в составе проекта структуры объектов,
/// на основе указанного шаблона
/// </summary>
public class BasedOnTemplate : Form
{
  /// <summary>id версии шаблона</summary>
  private long tempID;
  /// <summary>id версии проекта</summary>
  private long projectObjectID;
  /// <summary>Можно ли закрыть форму</summary>
  private bool canClose = true;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Panel panel2;
  private ProjectCreatorControl template;
  private Button btnCancel;
  private Button btnOK;
  private Label label1;
  private ButtonEdit beTemplate;

  /// <summary>
  /// список id-ков объектов из шаблона,
  /// которые пользователь выбрал для добавления в проект
  /// </summary>
  public ArrayList CreatedIDs => this.template.ListOfCreatedObjectsID;

  /// <summary>ID выбранного шаблона</summary>
  public long TemplateID => this.tempID;

  public BasedOnTemplate(long projectObjectID)
  {
    this.InitializeComponent();
    this.projectObjectID = projectObjectID;
    this.template.ProjectObjectID = projectObjectID;
    this.SetTemplateIdFromAttribute();
    this.UpdateTemplateInfoOnControl();
    this.UpdateControls();
  }

  private void BasedOnTemplate_FormClosing(object sender, FormClosingEventArgs e)
  {
    e.Cancel = !this.canClose;
    this.canClose = true;
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    this.canClose = this.template.SaveObjectData();
  }

  /// <summary>выбрать шаблон и загрузить его структуру в дерево</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void beTemplate_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    this.SetTemplateIdFromSelectionWindow();
    this.UpdateTemplateInfoOnControl();
    this.UpdateControls();
  }

  /// <summary>
  /// Установить ид шаблона исходя из значения атрибута "Шаблон проекта".
  /// </summary>
  private void SetTemplateIdFromAttribute()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.projectObjectID, false);
      if (dbObject == null)
        return;
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad00815-306c-11d8-b4e9-00304f19f545"), false);
      if (attributeByGuid == null)
        return;
      this.tempID = attributeByGuid.AsInteger;
    }
  }

  /// <summary>Обновляет наименование и дерево шаблона.</summary>
  private void UpdateTemplateInfoOnControl()
  {
    if (this.tempID == 0L)
      return;
    this.SetTemplateNameToControl();
    this.template.SelectionLoad(this.tempID);
  }

  private void SetTemplateIdFromSelectionWindow()
  {
    Intermech.Navigator.DBObjectTypes.Descriptor rootDescriptor = new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00813-306c-11d8-b4e9-00304f19f545"));
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Client.Core_1494"), LocalizationHolder.rm.GetString("Client.Core_1495"), (IDescriptor) rootDescriptor, SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableMultiselect);
    if (numArray != null && numArray.Length == 1)
      this.tempID = numArray[0];
    else
      this.tempID = 0L;
  }

  /// <summary>Установить заголовок шаблона в текстбокс.</summary>
  private void SetTemplateNameToControl()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.beTemplate.Text = sessionKeeper.Session.GetObject(this.tempID).NameInMessages;
  }

  private void UpdateControls() => this.btnOK.Enabled = this.tempID != 0L;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void BasedOnTemplate_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void BasedOnTemplate_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (BasedOnTemplate));
    this.panel1 = new Panel();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.panel2 = new Panel();
    this.label1 = new Label();
    this.beTemplate = new ButtonEdit();
    this.template = new ProjectCreatorControl();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.beTemplate.Properties.BeginInit();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Controls.Add((Control) this.btnOK);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.panel2.Controls.Add((Control) this.label1);
    this.panel2.Controls.Add((Control) this.beTemplate);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.beTemplate, "beTemplate");
    this.beTemplate.Name = "beTemplate";
    this.beTemplate.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beTemplate.ButtonClick += new ButtonPressedEventHandler(this.beTemplate_ButtonClick);
    componentResourceManager.ApplyResources((object) this.template, "template");
    this.template.Name = "template";
    this.template.ProjectObjectID = 0L;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.CancelButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.template);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (BasedOnTemplate);
    this.ShowInTaskbar = false;
    this.FormClosing += new FormClosingEventHandler(this.BasedOnTemplate_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.BasedOnTemplate_FormClosed);
    this.Load += new EventHandler(this.BasedOnTemplate_Load);
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.beTemplate.Properties.EndInit();
    this.ResumeLayout(false);
  }
}
