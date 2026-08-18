
// Type: Intermech.Search.Concretization.VersionCheckingForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Search.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Intermech.Search.Concretization;

public sealed class VersionCheckingForm : Form
{
  private NodeID _objectNodeID;
  private NavigatorTreeView _navigatorTreeView;
  private volatile NavigatorTreeNode _foundNavigatorTreeNode;
  private volatile bool _cancelSearch;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label2;
  private Label label3;
  private Button _findButton;
  private Button _goToFoundButton;
  private CheckBox _loadChildrenCheckBox;
  private PropertyGrid _foundVersionPropertyGrid;
  private PropertyGrid _checkedVersionPropertyGrid;
  private TableLayoutPanel tableLayoutPanel3;
  private FlowLayoutPanel flowLayoutPanel2;
  private TableLayoutPanel tableLayoutPanel4;
  private TableLayoutPanel tableLayoutPanel5;
  private TableLayoutPanel tableLayoutPanel6;

  public VersionCheckingForm()
  {
    this.InitializeComponent();
    this.UpdateControls();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NodeID ObjectNodeID
  {
    get => this._objectNodeID;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (this._objectNodeID == value)
        return;
      this._objectNodeID = value;
      this._checkedVersionPropertyGrid.SelectedObject = (object) new VersionCheckingForm.ObjectVersionInfo(this._objectNodeID);
      this.UpdateControls();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NavigatorTreeView NavigatorTreeView
  {
    get => this._navigatorTreeView;
    set
    {
      if (value == null || value.RootNode == null)
        throw new ArgumentException();
      if (this._navigatorTreeView == value)
        return;
      this._navigatorTreeView = value;
      this.UpdateControls();
    }
  }

  private void VersionCheckingForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void VersionCheckingForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void GoToFoundButton_Click(object sender, EventArgs e)
  {
    if (this._foundNavigatorTreeNode == null)
      return;
    this._foundNavigatorTreeNode.FocusThenExpand();
  }

  private void FindButton_Click(object sender, EventArgs e)
  {
    this._cancelSearch = false;
    using (ProgressDialog progressDialog = new ProgressDialog())
    {
      progressDialog.ButtonClick += (EventHandler) ((s, ee) => this._cancelSearch = true);
      progressDialog.FormClosed += (FormClosedEventHandler) ((s, ee) => this._cancelSearch = true);
      progressDialog.Style = ProgressBarStyle.Marquee;
      bool loadChildren = this._loadChildrenCheckBox.Checked;
      new Task((Action) (() =>
      {
        try
        {
          try
          {
            try
            {
              foreach (NavigatorTreeNode navigatorTreeNode in (this._foundNavigatorTreeNode ?? this._navigatorTreeView.RootNode).GetAllNextAndSelf(loadChildren).Skip<NavigatorTreeNode>(1))
              {
                if (this._cancelSearch)
                  return;
                if (navigatorTreeNode.NodeID is NodeID)
                {
                  NodeID nextNodeID = (NodeID) navigatorTreeNode.NodeID;
                  if (nextNodeID.ID == this._objectNodeID.ID && nextNodeID.ObjectID != this._objectNodeID.ObjectID)
                  {
                    this._foundNavigatorTreeNode = navigatorTreeNode;
                    this.Invoke((Delegate) (() =>
                    {
                      this._foundVersionPropertyGrid.SelectedObject = (object) new VersionCheckingForm.ObjectVersionInfo(nextNodeID);
                      this.UpdateControls();
                      progressDialog.Close();
                    }));
                    return;
                  }
                }
              }
              this._foundNavigatorTreeNode = (NavigatorTreeNode) null;
              this.Invoke((Delegate) (() =>
              {
                this._foundVersionPropertyGrid.SelectedObject = (object) null;
                this.UpdateControls();
                progressDialog.Close();
                int num = (int) MessageBox.Show("Поиск завершен", "Intermech Professional Solution", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              }));
            }
            finally
            {
              this.Invoke((Delegate) (() =>
              {
                if (!progressDialog.IsHandleCreated || progressDialog.IsDisposed)
                  return;
                progressDialog.Close();
              }));
            }
          }
          catch (Exception ex)
          {
            ExceptionHelper.ExceptionService.ShowException(ex);
          }
        }
        catch
        {
        }
      })).Start();
      int num1 = (int) progressDialog.ShowDialog();
    }
  }

  private void UpdateControls()
  {
    this._goToFoundButton.Enabled = this._navigatorTreeView != null && this._foundNavigatorTreeNode != null;
    this._findButton.Enabled = this._objectNodeID != null && this._navigatorTreeView != null;
    this._findButton.Text = this._foundNavigatorTreeNode != null ? "Продолжить" : "Найти";
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
    this.label2 = new Label();
    this.label3 = new Label();
    this._findButton = new Button();
    this._goToFoundButton = new Button();
    this._loadChildrenCheckBox = new CheckBox();
    this._foundVersionPropertyGrid = new PropertyGrid();
    this._checkedVersionPropertyGrid = new PropertyGrid();
    this.tableLayoutPanel3 = new TableLayoutPanel();
    this.flowLayoutPanel2 = new FlowLayoutPanel();
    this.tableLayoutPanel4 = new TableLayoutPanel();
    this.tableLayoutPanel5 = new TableLayoutPanel();
    this.tableLayoutPanel6 = new TableLayoutPanel();
    this.tableLayoutPanel3.SuspendLayout();
    this.flowLayoutPanel2.SuspendLayout();
    this.tableLayoutPanel4.SuspendLayout();
    this.tableLayoutPanel5.SuspendLayout();
    this.tableLayoutPanel6.SuspendLayout();
    this.SuspendLayout();
    this.label2.AutoSize = true;
    this.label2.Dock = DockStyle.Fill;
    this.label2.ImeMode = ImeMode.NoControl;
    this.label2.Location = new Point(3, 0);
    this.label2.Name = "label2";
    this.label2.Size = new Size(279, 13);
    this.label2.TabIndex = 0;
    this.label2.Text = "Проверяемая версия:";
    this.label3.AutoSize = true;
    this.label3.Dock = DockStyle.Fill;
    this.label3.ImeMode = ImeMode.NoControl;
    this.label3.Location = new Point(3, 0);
    this.label3.Name = "label3";
    this.label3.Size = new Size(280, 13);
    this.label3.TabIndex = 1;
    this.label3.Text = "Найденная версия:";
    this._findButton.AutoSize = true;
    this._findButton.ImeMode = ImeMode.NoControl;
    this._findButton.Location = new Point(487, 3);
    this._findButton.Name = "_findButton";
    this._findButton.Size = new Size(93, 23);
    this._findButton.TabIndex = 0;
    this._findButton.Text = "Поиск";
    this._findButton.UseVisualStyleBackColor = true;
    this._findButton.Click += new EventHandler(this.FindButton_Click);
    this._goToFoundButton.AutoSize = true;
    this._goToFoundButton.Enabled = false;
    this._goToFoundButton.ImeMode = ImeMode.NoControl;
    this._goToFoundButton.Location = new Point(348, 3);
    this._goToFoundButton.Name = "_goToFoundButton";
    this._goToFoundButton.Size = new Size(133, 23);
    this._goToFoundButton.TabIndex = 1;
    this._goToFoundButton.Text = "Перейти к найденному";
    this._goToFoundButton.UseVisualStyleBackColor = true;
    this._goToFoundButton.Click += new EventHandler(this.GoToFoundButton_Click);
    this._loadChildrenCheckBox.AutoSize = true;
    this._loadChildrenCheckBox.CheckAlign = ContentAlignment.TopLeft;
    this._loadChildrenCheckBox.Dock = DockStyle.Fill;
    this._loadChildrenCheckBox.ImeMode = ImeMode.NoControl;
    this._loadChildrenCheckBox.Location = new Point(3, 266);
    this._loadChildrenCheckBox.Name = "_loadChildrenCheckBox";
    this._loadChildrenCheckBox.Size = new Size(583, 17);
    this._loadChildrenCheckBox.TabIndex = 5;
    this._loadChildrenCheckBox.Text = "Подгружать содержимое узлов. Установка флажка позволяет  подгружать  дочерние узлы, если они еще не загружены";
    this._loadChildrenCheckBox.TextAlign = ContentAlignment.TopLeft;
    this._loadChildrenCheckBox.UseVisualStyleBackColor = true;
    this._foundVersionPropertyGrid.Dock = DockStyle.Fill;
    this._foundVersionPropertyGrid.Location = new Point(3, 16 /*0x10*/);
    this._foundVersionPropertyGrid.Name = "_foundVersionPropertyGrid";
    this._foundVersionPropertyGrid.PropertySort = PropertySort.Alphabetical;
    this._foundVersionPropertyGrid.Size = new Size(280, 232);
    this._foundVersionPropertyGrid.TabIndex = 6;
    this._checkedVersionPropertyGrid.Dock = DockStyle.Fill;
    this._checkedVersionPropertyGrid.Location = new Point(3, 16 /*0x10*/);
    this._checkedVersionPropertyGrid.Name = "_checkedVersionPropertyGrid";
    this._checkedVersionPropertyGrid.PropertySort = PropertySort.Alphabetical;
    this._checkedVersionPropertyGrid.Size = new Size(279, 232);
    this._checkedVersionPropertyGrid.TabIndex = 7;
    this.tableLayoutPanel3.ColumnCount = 1;
    this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel3.Controls.Add((Control) this.flowLayoutPanel2, 0, 2);
    this.tableLayoutPanel3.Controls.Add((Control) this._loadChildrenCheckBox, 0, 1);
    this.tableLayoutPanel3.Controls.Add((Control) this.tableLayoutPanel4, 0, 0);
    this.tableLayoutPanel3.Dock = DockStyle.Fill;
    this.tableLayoutPanel3.Location = new Point(0, 0);
    this.tableLayoutPanel3.Name = "tableLayoutPanel3";
    this.tableLayoutPanel3.RowCount = 3;
    this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel3.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel3.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel3.Size = new Size(589, 321);
    this.tableLayoutPanel3.TabIndex = 2;
    this.flowLayoutPanel2.AutoSize = true;
    this.flowLayoutPanel2.Controls.Add((Control) this._findButton);
    this.flowLayoutPanel2.Controls.Add((Control) this._goToFoundButton);
    this.flowLayoutPanel2.Dock = DockStyle.Fill;
    this.flowLayoutPanel2.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel2.Location = new Point(3, 289);
    this.flowLayoutPanel2.Name = "flowLayoutPanel2";
    this.flowLayoutPanel2.Size = new Size(583, 29);
    this.flowLayoutPanel2.TabIndex = 0;
    this.tableLayoutPanel4.AutoSize = true;
    this.tableLayoutPanel4.ColumnCount = 2;
    this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
    this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
    this.tableLayoutPanel4.Controls.Add((Control) this.tableLayoutPanel5, 0, 0);
    this.tableLayoutPanel4.Controls.Add((Control) this.tableLayoutPanel6, 1, 0);
    this.tableLayoutPanel4.Dock = DockStyle.Fill;
    this.tableLayoutPanel4.Location = new Point(3, 3);
    this.tableLayoutPanel4.Name = "tableLayoutPanel4";
    this.tableLayoutPanel4.RowCount = 1;
    this.tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tableLayoutPanel4.Size = new Size(583, 257);
    this.tableLayoutPanel4.TabIndex = 6;
    this.tableLayoutPanel5.AutoSize = true;
    this.tableLayoutPanel5.ColumnCount = 1;
    this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel5.Controls.Add((Control) this.label2, 0, 0);
    this.tableLayoutPanel5.Controls.Add((Control) this._checkedVersionPropertyGrid, 0, 1);
    this.tableLayoutPanel5.Dock = DockStyle.Fill;
    this.tableLayoutPanel5.Location = new Point(3, 3);
    this.tableLayoutPanel5.Name = "tableLayoutPanel5";
    this.tableLayoutPanel5.RowCount = 2;
    this.tableLayoutPanel5.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel5.Size = new Size(285, 251);
    this.tableLayoutPanel5.TabIndex = 0;
    this.tableLayoutPanel6.AutoSize = true;
    this.tableLayoutPanel6.ColumnCount = 1;
    this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel6.Controls.Add((Control) this.label3, 0, 0);
    this.tableLayoutPanel6.Controls.Add((Control) this._foundVersionPropertyGrid, 0, 1);
    this.tableLayoutPanel6.Dock = DockStyle.Fill;
    this.tableLayoutPanel6.Location = new Point(294, 3);
    this.tableLayoutPanel6.Name = "tableLayoutPanel6";
    this.tableLayoutPanel6.RowCount = 2;
    this.tableLayoutPanel6.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel6.Size = new Size(286, 251);
    this.tableLayoutPanel6.TabIndex = 1;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(589, 321);
    this.Controls.Add((Control) this.tableLayoutPanel3);
    this.Name = nameof (VersionCheckingForm);
    this.ShowIcon = false;
    this.Text = "Проверка версии объекта в составе";
    this.FormClosed += new FormClosedEventHandler(this.VersionCheckingForm_FormClosed);
    this.Load += new EventHandler(this.VersionCheckingForm_Load);
    this.tableLayoutPanel3.ResumeLayout(false);
    this.tableLayoutPanel3.PerformLayout();
    this.flowLayoutPanel2.ResumeLayout(false);
    this.flowLayoutPanel2.PerformLayout();
    this.tableLayoutPanel4.ResumeLayout(false);
    this.tableLayoutPanel4.PerformLayout();
    this.tableLayoutPanel5.ResumeLayout(false);
    this.tableLayoutPanel5.PerformLayout();
    this.tableLayoutPanel6.ResumeLayout(false);
    this.tableLayoutPanel6.PerformLayout();
    this.ResumeLayout(false);
  }

  private sealed class ObjectVersionInfo
  {
    private NodeID _objectNodeID;

    public ObjectVersionInfo(NodeID objectNodeID)
    {
      this._objectNodeID = objectNodeID != null ? objectNodeID : throw new ArgumentNullException(nameof (objectNodeID));
    }

    [DisplayName("Идентификатор версии")]
    public long ObjectVersionID => this._objectNodeID.ObjectID;

    [DisplayName("Номер версии")]
    public long VersionNumber => this._objectNodeID.Version;

    [DisplayName("Заголовок объекта")]
    public string Caption => this._objectNodeID.Caption;

    [DisplayName("Тип конкретизации")]
    public string ConcretizationType
    {
      get
      {
        switch (this._objectNodeID.State)
        {
          case ObjectFiltrationState.fsCompositeVersion:
            return "Жесткая конкретизация";
          case ObjectFiltrationState.fsSoftConcretised:
            return "Мягкая конкретизация";
          default:
            return "Абстрактная";
        }
      }
    }
  }
}
