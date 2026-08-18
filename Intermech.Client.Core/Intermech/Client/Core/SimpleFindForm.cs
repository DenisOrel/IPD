
// Type: Intermech.Client.Core.SimpleFindForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Configuration;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Реализует простейшую форму поиска по тексту в окне.</summary>
public class SimpleFindForm : Form, IFindData, IFindController
{
  private static readonly string locationTag = "location";
  private IWindowWithFind window;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btFind;
  private Button btCancel;
  private TextBox tbText;
  private Label lbText;

  /// <summary>Создает объект.</summary>
  public SimpleFindForm() => this.InitializeComponent();

  /// <summary>Возвращает искомый пользователем текст.</summary>
  public string FindWhat
  {
    get => this.tbText.Text;
    set => this.tbText.Text = value;
  }

  private void btFind_Click(object sender, EventArgs e)
  {
    if (this.FindWhat.Length <= 0)
      return;
    this.DoFind();
  }

  private void DoFind() => this.window.FindNext((IFindController) this);

  private void btCancel_Click(object sender, EventArgs e) => this.Close();

  public object InterfaceObject => (object) this;

  public void AttachToWindow(IWindowWithFind iWindowWithFind) => this.window = iWindowWithFind;

  public new void Hide() => this.Close();

  public virtual void SaveConfiguration(IConfiguration iConfiguration)
  {
    iConfiguration.SetProperty(SimpleFindForm.locationTag, (string) TypeDescriptor.GetConverter(typeof (Point)).ConvertTo((object) this.Location, typeof (string)));
  }

  public virtual void LoadConfiguration(IConfiguration iConfiguration)
  {
    string empty = string.Empty;
    if (!iConfiguration.HasProperty(SimpleFindForm.locationTag))
      return;
    string property = iConfiguration.GetProperty(SimpleFindForm.locationTag);
    if (!(property != string.Empty))
      return;
    this.Location = (Point) TypeDescriptor.GetConverter(typeof (Point)).ConvertFrom((object) property);
  }

  public bool IsVisible => this.Visible;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SimpleFindForm));
    this.btFind = new Button();
    this.btCancel = new Button();
    this.tbText = new TextBox();
    this.lbText = new Label();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btFind, "btFind");
    this.btFind.Name = "btFind";
    this.btFind.UseVisualStyleBackColor = true;
    this.btFind.Click += new EventHandler(this.btFind_Click);
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Name = "btCancel";
    this.btCancel.UseVisualStyleBackColor = true;
    this.btCancel.Click += new EventHandler(this.btCancel_Click);
    componentResourceManager.ApplyResources((object) this.tbText, "tbText");
    this.tbText.Name = "tbText";
    componentResourceManager.ApplyResources((object) this.lbText, "lbText");
    this.lbText.Name = "lbText";
    this.AcceptButton = (IButtonControl) this.btFind;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.Controls.Add((Control) this.lbText);
    this.Controls.Add((Control) this.tbText);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btFind);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SimpleFindForm);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  void IFindController.Show() => this.Show();
}
