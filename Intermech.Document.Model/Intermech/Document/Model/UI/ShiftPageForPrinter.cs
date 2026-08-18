// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.ShiftPageForPrinter
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.UI;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.UI;

/// <summary>Диалог настройки смещения для страниц документа под конкретный принтер</summary>
public class ShiftPageForPrinter : Form
{
  /// <summary>Пользователь с админскими правами</summary>
  private bool isAdminRole;
  /// <summary>Данные на форме были изменены</summary>
  private bool isChanged;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private Label lPrinterName;
  private TextBox tbX;
  private Label label3;
  private TextBox tbY;
  private Label label4;
  private Button bCancel;
  private Button bOK;
  private Button bSaveToAll;
  private Button bDefaults;

  /// <summary>Конструктор</summary>
  public ShiftPageForPrinter() => this.InitializeComponent();

  /// <summary>Вызвать диалог</summary>
  /// <returns></returns>
  public static DialogResult Execute(string printerName)
  {
    ShiftPageForPrinter shiftPageForPrinter = new ShiftPageForPrinter();
    shiftPageForPrinter.lPrinterName.Text = printerName;
    shiftPageForPrinter.LoadDialogData();
    DialogResult dialogResult = shiftPageForPrinter.ShowDialog();
    shiftPageForPrinter.Dispose();
    return dialogResult;
  }

  /// <summary>Загрузить данные в контролы</summary>
  private void LoadDialogData()
  {
    PointF pointF = PointF.Empty;
    if (!string.IsNullOrEmpty(this.lPrinterName.Text))
      pointF = ImDocumentEditorConfig.Instance.GetShiftPage(this.lPrinterName.Text);
    this.tbX.Text = pointF.X.ToString();
    this.tbY.Text = pointF.Y.ToString();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.isAdminRole = sessionKeeper.Session.RoleID == sessionKeeper.Session.IdentHelper.AdminRoleID;
    this.UpdateControls();
  }

  /// <summary>Обновить статусы контролов</summary>
  private void UpdateControls()
  {
    float result;
    this.bOK.Enabled = this.isChanged && float.TryParse(this.tbX.Text, out result) && float.TryParse(this.tbY.Text, out result);
    this.bSaveToAll.Enabled = this.isChanged && this.isAdminRole && this.bOK.Enabled;
  }

  private void bOK_Click(object sender, EventArgs e)
  {
    PointF shiftPage = new PointF(float.Parse(this.tbX.Text), float.Parse(this.tbY.Text));
    DocumentPrinterSettings documentPrinterSettings;
    if (ImDocumentEditorConfig.Instance.DocumentPrintersSettings_User.TryGetValue(this.lPrinterName.Text, out documentPrinterSettings))
      documentPrinterSettings.ShiftPage = shiftPage;
    else
      ImDocumentEditorConfig.Instance.DocumentPrintersSettings_User.Add(this.lPrinterName.Text, new DocumentPrinterSettings(shiftPage));
  }

  private void bSaveToAll_Click(object sender, EventArgs e)
  {
    ImDocumentEditorConfig.Instance.DocumentPrintersSettings_User.Remove(this.lPrinterName.Text);
    PointF shiftPage = new PointF(float.Parse(this.tbX.Text), float.Parse(this.tbY.Text));
    DocumentPrinterSettings documentPrinterSettings;
    if (ImDocumentEditorConfig.Instance.DocumentPrintersSettings_Global.TryGetValue(this.lPrinterName.Text, out documentPrinterSettings))
      documentPrinterSettings.ShiftPage = shiftPage;
    else
      ImDocumentEditorConfig.Instance.DocumentPrintersSettings_Global.Add(this.lPrinterName.Text, new DocumentPrinterSettings(shiftPage));
    ImDocumentEditorConfig.Instance.SaveDocumentPrintersSettings(true);
  }

  private void tbX_TextChanged(object sender, EventArgs e)
  {
    this.isChanged = true;
    this.UpdateControls();
  }

  private void bDefaults_Click(object sender, EventArgs e)
  {
    string text1 = this.tbX.Text;
    string text2 = this.tbY.Text;
    ImDocumentEditorConfig.Instance.LoadDocumentPrintersSettings(true);
    DocumentPrinterSettings documentPrinterSettings;
    if (ImDocumentEditorConfig.Instance.DocumentPrintersSettings_Global.TryGetValue(this.lPrinterName.Text, out documentPrinterSettings) && documentPrinterSettings != null)
    {
      this.tbX.Text = documentPrinterSettings.ShiftPage.X.ToString();
      this.tbY.Text = documentPrinterSettings.ShiftPage.Y.ToString();
    }
    else
    {
      this.tbX.Text = "0";
      this.tbY.Text = "0";
    }
    this.isChanged = ((this.isChanged ? 1 : 0) | (this.tbX.Text != text1 ? 1 : (this.tbY.Text != text2 ? 1 : 0))) != 0;
    this.UpdateControls();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ShiftPageForPrinter));
    this.label1 = new Label();
    this.lPrinterName = new Label();
    this.tbX = new TextBox();
    this.label3 = new Label();
    this.tbY = new TextBox();
    this.label4 = new Label();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.bSaveToAll = new Button();
    this.bDefaults = new Button();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.lPrinterName, "lPrinterName");
    this.lPrinterName.Name = "lPrinterName";
    componentResourceManager.ApplyResources((object) this.tbX, "tbX");
    this.tbX.Name = "tbX";
    this.tbX.TextChanged += new EventHandler(this.tbX_TextChanged);
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.tbY, "tbY");
    this.tbY.Name = "tbY";
    this.tbY.TextChanged += new EventHandler(this.tbX_TextChanged);
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Name = "bCancel";
    this.bCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.bOK, "bOK");
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Name = "bOK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    componentResourceManager.ApplyResources((object) this.bSaveToAll, "bSaveToAll");
    this.bSaveToAll.Name = "bSaveToAll";
    this.bSaveToAll.UseVisualStyleBackColor = true;
    this.bSaveToAll.Click += new EventHandler(this.bSaveToAll_Click);
    componentResourceManager.ApplyResources((object) this.bDefaults, "bDefaults");
    this.bDefaults.Name = "bDefaults";
    this.bDefaults.UseVisualStyleBackColor = true;
    this.bDefaults.Click += new EventHandler(this.bDefaults_Click);
    this.AcceptButton = (IButtonControl) this.bOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.bDefaults);
    this.Controls.Add((Control) this.bSaveToAll);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.tbY);
    this.Controls.Add((Control) this.tbX);
    this.Controls.Add((Control) this.lPrinterName);
    this.Controls.Add((Control) this.label1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ShiftPageForPrinter);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
