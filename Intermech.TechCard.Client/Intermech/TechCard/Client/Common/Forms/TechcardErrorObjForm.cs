// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Common.Forms.TechcardErrorObjForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Infralution.Controls.VirtualTree;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Common.Forms;

/// <summary>
/// Форма для отображения сообщений об ошибках для списка объектов
/// </summary>
public class TechcardErrorObjForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private Button btnApply;
  private Button btnCancel;
  /// <summary>
  /// 
  /// </summary>
  protected internal TechCardNavTreeViewControl tolcTechObjList;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeData()
  {
    this.tolcTechObjList.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(TechCardNavTreeViewUtils.GetObjectColumnsOnly);
  }

  /// <summary>Конструктор</summary>
  public TechcardErrorObjForm()
  {
    this.InitializeComponent();
    this.InitializeData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="errorMsg"></param>
  /// <param name="descriptor"></param>
  /// <returns></returns>
  public bool LoadData(string errorMsg, IDescriptor descriptor)
  {
    this.Text = errorMsg;
    if (descriptor == null)
      return false;
    this.tolcTechObjList.SetColumns(Utils.VersionColumns(NodeColumnSortOrder.Ascending, false), descriptor);
    this.tolcTechObjList.Build(descriptor);
    return true;
  }

  /// <summary>Управление отображением кнопок</summary>
  public bool ShowBtn_OK
  {
    get => this.btnApply.Visible;
    set => this.btnApply.Visible = value;
  }

  /// <summary>Управление отображением кнопок</summary>
  public bool ShowBtn_Cancel
  {
    get => this.btnCancel.Visible;
    set => this.btnCancel.Visible = value;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TechcardErrorObjForm));
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.btnApply = new Button();
    this.btnCancel = new Button();
    this.tolcTechObjList = new TechCardNavTreeViewControl();
    this.tableLayoutPanel1.SuspendLayout();
    this.tolcTechObjList.BeginInit();
    this.SuspendLayout();
    this.tableLayoutPanel1.AutoSize = true;
    this.tableLayoutPanel1.ColumnCount = 2;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
    this.tableLayoutPanel1.Controls.Add((Control) this.btnApply, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.btnCancel, 1, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Bottom;
    this.tableLayoutPanel1.Location = new Point(0, 287);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 1;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Size = new Size(472, 29);
    this.tableLayoutPanel1.TabIndex = 5;
    this.btnApply.Anchor = AnchorStyles.Right;
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.ImeMode = ImeMode.NoControl;
    this.btnApply.Location = new Point(313, 3);
    this.btnApply.Name = "btnApply";
    this.btnApply.Size = new Size(75, 23);
    this.btnApply.TabIndex = 9;
    this.btnApply.Text = "ОК";
    this.btnCancel.Anchor = AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(394, 3);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 8;
    this.btnCancel.Text = "Отмена";
    this.tolcTechObjList.AllowDrop = true;
    this.tolcTechObjList.AllowMultiSelect = false;
    this.tolcTechObjList.AllowUserPinnedColumns = false;
    this.tolcTechObjList.CheckedNodesStates = (IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper>) componentResourceManager.GetObject("tolcTechObjList.CheckedNodesStates");
    this.tolcTechObjList.CheckoutMode = TechCheckoutMode.Manual;
    this.tolcTechObjList.CheckRootNode = false;
    this.tolcTechObjList.DisableCheckedOutColumn = true;
    this.tolcTechObjList.DisableIMContextMenu = true;
    this.tolcTechObjList.DisableKeyDownEvents = true;
    this.tolcTechObjList.DisableKeyUpEvents = true;
    this.tolcTechObjList.DisablePacketsReading = false;
    this.tolcTechObjList.Dock = DockStyle.Fill;
    this.tolcTechObjList.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.tolcTechObjList.LineStyle = LineStyle.Dot;
    this.tolcTechObjList.Location = new Point(0, 0);
    this.tolcTechObjList.Name = "tolcTechObjList";
    this.tolcTechObjList.RowEvenStyle.WordWrap = false;
    this.tolcTechObjList.RowOddStyle.WordWrap = false;
    this.tolcTechObjList.RowSelectedStyle.WordWrap = false;
    this.tolcTechObjList.RowStyle.BorderColor = SystemColors.Control;
    this.tolcTechObjList.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.tolcTechObjList.RowStyle.BorderWidth = 1;
    this.tolcTechObjList.RowStyle.WordWrap = false;
    this.tolcTechObjList.SelectBeforeEdit = true;
    this.tolcTechObjList.ShowRootRow = false;
    this.tolcTechObjList.Size = new Size(472, 287);
    this.tolcTechObjList.SuppressErrorMessages = true;
    this.tolcTechObjList.TabIndex = 6;
    this.tolcTechObjList.Tag = (object) " ";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(472, 316);
    this.Controls.Add((Control) this.tolcTechObjList);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (TechcardErrorObjForm);
    this.Text = "Put error's message here";
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tolcTechObjList.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
