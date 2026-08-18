
// Type: Intermech.Client.Core.Navigator.Controls.Windows.RedlinigEditForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Controls.Windows;

public class RedlinigEditForm : Form
{
  /// <summary>имя типа файлов</summary>
  public string FileName = string.Empty;
  /// <summary>папка в которой искать</summary>
  public string Folder = string.Empty;
  /// <summary>маска поиска</summary>
  public string Mask = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnOk;
  private Label label1;
  private Label label2;
  private Label label3;
  private Label label4;
  private TextBox tbMask;
  private TextBox tbName;
  private TextBox tbFolder;
  private Button btnCancel;
  private ComboBox cbMask;
  private Button btnAdd;

  /// <summary>
  /// 
  /// </summary>
  public RedlinigEditForm()
  {
    this.InitializeComponent();
    this.cbMask.Items.Add((object) new MyElement((object) "%name%", LocalizationHolder.rm.GetString("Client.Core_1439"), (object) null));
    this.cbMask.Items.Add((object) new MyElement((object) "%fullname%", LocalizationHolder.rm.GetString("Client.Core_1440"), (object) null));
    this.cbMask.SelectedIndex = 0;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <param name="folder"></param>
  /// <param name="mask"></param>
  public RedlinigEditForm(string name, string folder, string mask)
    : this()
  {
    this.Text = LocalizationHolder.rm.GetString("Client.Core_1441");
    this.tbName.Text = name;
    this.tbFolder.Text = folder;
    this.tbMask.Text = mask;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnOk_Click(object sender, EventArgs e)
  {
    this.FileName = this.tbName.Text;
    this.Folder = this.tbFolder.Text;
    this.Mask = this.tbMask.Text;
    if (StringsHelper.ContainsCount(this.Mask, RedliningFiles.NAME) + StringsHelper.ContainsCount(this.Mask, RedliningFiles.FULLNAME) > 1)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1442"), LocalizationHolder.rm.GetString("Client.Core_1261"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
    }
    else
    {
      this.Mask = this.Mask.ToLower();
      this.DialogResult = DialogResult.OK;
    }
  }

  /// <summary>Изменение имени типа файлов</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbName_TextChanged(object sender, EventArgs e)
  {
    this.btnOk.Enabled = this.tbName.Text != string.Empty && this.tbMask.Text != string.Empty;
  }

  /// <summary>Изменение маски для поиска файлов</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbMask_TextChanged(object sender, EventArgs e)
  {
    this.btnOk.Enabled = this.tbName.Text != string.Empty && this.tbMask.Text != string.Empty;
  }

  /// <summary>добавить макрос в строку шаблона</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnAdd_Click(object sender, EventArgs e)
  {
    string str1 = (this.cbMask.SelectedItem as MyElement).Value.ToString();
    string str2 = this.tbMask.Text;
    int startIndex1 = str2.IndexOf(RedliningFiles.NAME, 0, StringComparison.InvariantCultureIgnoreCase);
    int startIndex2 = str2.IndexOf(RedliningFiles.FULLNAME, 0, StringComparison.InvariantCultureIgnoreCase);
    if (startIndex1 == -1 && startIndex2 == -1)
      str2 += str1;
    else if (startIndex1 < startIndex2 && startIndex1 != -1 || startIndex2 == -1)
      str2 = str2.Remove(startIndex1, RedliningFiles.NAME.Length).Insert(startIndex1, str1);
    else if (startIndex2 < startIndex1 && startIndex2 != -1 || startIndex1 == -1)
      str2 = str2.Remove(startIndex2, RedliningFiles.FULLNAME.Length).Insert(startIndex2, str1);
    this.tbMask.Text = str2;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RedlinigEditForm));
    this.btnOk = new Button();
    this.label1 = new Label();
    this.label2 = new Label();
    this.label3 = new Label();
    this.label4 = new Label();
    this.tbMask = new TextBox();
    this.tbName = new TextBox();
    this.tbFolder = new TextBox();
    this.btnCancel = new Button();
    this.cbMask = new ComboBox();
    this.btnAdd = new Button();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.tbMask, "tbMask");
    this.tbMask.Name = "tbMask";
    this.tbMask.TextChanged += new EventHandler(this.tbMask_TextChanged);
    componentResourceManager.ApplyResources((object) this.tbName, "tbName");
    this.tbName.Name = "tbName";
    this.tbName.TextChanged += new EventHandler(this.tbName_TextChanged);
    componentResourceManager.ApplyResources((object) this.tbFolder, "tbFolder");
    this.tbFolder.Name = "tbFolder";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cbMask, "cbMask");
    this.cbMask.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbMask.FormattingEnabled = true;
    this.cbMask.Name = "cbMask";
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.UseVisualStyleBackColor = true;
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    this.AcceptButton = (IButtonControl) this.btnAdd;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.btnAdd);
    this.Controls.Add((Control) this.cbMask);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.tbFolder);
    this.Controls.Add((Control) this.tbName);
    this.Controls.Add((Control) this.tbMask);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnOk);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (RedlinigEditForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
