
// Type: Intermech.Navigator.Selections.Implementation.PreparePasteErrors
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Selections.Implementation;

/// <summary>
/// Форма, отображающая список ошибок и замечаний, возникших при работе с выборками и классификаторами
/// </summary>
public class PreparePasteErrors : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label labelInfo;
  private Panel panel1;
  private Button BtnClose;
  private ImageList imageList;
  private ListView listView;
  private ColumnHeader columnError;
  private Button bOK;

  /// <summary>Создать экземпляр формы</summary>
  public PreparePasteErrors() => this.InitializeComponent();

  /// <summary>Создать экземпляр формы, заполнить контролы</summary>
  /// <param name="info">Сообщение</param>
  /// <param name="preparePasteErrors">Список возникших предупреждений</param>
  public PreparePasteErrors(string info, List<string> preparePasteErrors)
  {
    this.InitializeComponent();
    this.labelInfo.Text = info;
    for (int index = 0; index < preparePasteErrors.Count; ++index)
      this.listView.Items.Add(preparePasteErrors[index], 0);
    this.listView_Resize((object) this, (EventArgs) null);
  }

  private void listView_Resize(object sender, EventArgs e)
  {
    this.listView.Columns[0].Width = this.listView.ClientSize.Width - 30;
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PreparePasteErrors));
    this.labelInfo = new Label();
    this.panel1 = new Panel();
    this.BtnClose = new Button();
    this.imageList = new ImageList(this.components);
    this.listView = new ListView();
    this.columnError = new ColumnHeader();
    this.bOK = new Button();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.labelInfo, "labelInfo");
    this.labelInfo.Name = "labelInfo";
    this.panel1.Controls.Add((Control) this.labelInfo);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.BtnClose, "BtnClose");
    this.BtnClose.Cursor = Cursors.Default;
    this.BtnClose.DialogResult = DialogResult.Cancel;
    this.BtnClose.Name = "BtnClose";
    this.BtnClose.UseVisualStyleBackColor = true;
    this.imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList.ImageStream");
    this.imageList.TransparentColor = Color.Transparent;
    this.imageList.Images.SetKeyName(0, "document_error.png");
    componentResourceManager.ApplyResources((object) this.listView, "listView");
    this.listView.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnError
    });
    this.listView.FullRowSelect = true;
    this.listView.GridLines = true;
    this.listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.listView.HideSelection = false;
    this.listView.LargeImageList = this.imageList;
    this.listView.MultiSelect = false;
    this.listView.Name = "listView";
    this.listView.ShowGroups = false;
    this.listView.ShowItemToolTips = true;
    this.listView.SmallImageList = this.imageList;
    this.listView.Sorting = SortOrder.Ascending;
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    this.listView.Resize += new EventHandler(this.listView_Resize);
    componentResourceManager.ApplyResources((object) this.columnError, "columnError");
    componentResourceManager.ApplyResources((object) this.bOK, "bOK");
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Name = "bOK";
    this.bOK.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.BtnClose;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.listView);
    this.Controls.Add((Control) this.BtnClose);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (PreparePasteErrors);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
