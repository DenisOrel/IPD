// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.ArchiveParametersControl
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;
using Intermech.Mvp.Winforms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal class ArchiveParametersControl : MvpUserControl, IArchiveParametersView, IView
{
  private IContainer components;
  private TableLayoutPanel tlpMainGrid;
  private TableLayoutPanel tlpArchiveLocation;
  private Button btArchiveLocation;
  private TextBox tbArchiveLocation;
  private Label lbArchiveLocation;
  private TableLayoutPanel tableLayoutPanel1;
  private Label lbImportBatchSize;
  private NumericUpDown nudImportBatchSize;

  public ArchiveParametersControl() => this.InitializeComponent();

  private void OnPageItemChanged(object sender, EventArgs e) => this.RaisePageChanged();

  private void RaisePageChanged()
  {
    if (this.EditableStateChanged == null)
      return;
    this.EditableStateChanged((object) this, EventArgs.Empty);
  }

  string IArchiveParametersView.ArchiveLocation
  {
    get => this.tbArchiveLocation.Text;
    set => this.tbArchiveLocation.Text = value;
  }

  void IArchiveParametersView.AttachPageChangedHandlers()
  {
    this.tbArchiveLocation.TextChanged += new EventHandler(this.OnPageItemChanged);
    this.nudImportBatchSize.ValueChanged += new EventHandler(this.OnPageItemChanged);
  }

  void IArchiveParametersView.DetachPageChangesHandlers()
  {
    this.tbArchiveLocation.TextChanged -= new EventHandler(this.OnPageItemChanged);
    this.nudImportBatchSize.ValueChanged -= new EventHandler(this.OnPageItemChanged);
  }

  void IArchiveParametersView.EnableArchiveLocation(bool enabled)
  {
    this.tbArchiveLocation.Enabled = enabled;
    this.btArchiveLocation.Enabled = enabled;
  }

  int IArchiveParametersView.ImportBatchSize
  {
    get => (int) this.nudImportBatchSize.Value;
    set => this.nudImportBatchSize.Value = (Decimal) value;
  }

  void IArchiveParametersView.EnableImportBatchSize(bool enabled)
  {
    this.nudImportBatchSize.Enabled = enabled;
    this.lbImportBatchSize.Enabled = enabled;
  }

  event EventHandler IArchiveParametersView.SelectLocation
  {
    add => this.btArchiveLocation.Click += value;
    remove => this.btArchiveLocation.Click -= value;
  }

  public event EventHandler EditableStateChanged;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.tlpMainGrid = new TableLayoutPanel();
    this.tlpArchiveLocation = new TableLayoutPanel();
    this.btArchiveLocation = new Button();
    this.tbArchiveLocation = new TextBox();
    this.lbArchiveLocation = new Label();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.lbImportBatchSize = new Label();
    this.nudImportBatchSize = new NumericUpDown();
    this.tlpMainGrid.SuspendLayout();
    this.tlpArchiveLocation.SuspendLayout();
    this.tableLayoutPanel1.SuspendLayout();
    this.nudImportBatchSize.BeginInit();
    this.SuspendLayout();
    this.tlpMainGrid.ColumnCount = 1;
    this.tlpMainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpMainGrid.Controls.Add((Control) this.tlpArchiveLocation, 0, 0);
    this.tlpMainGrid.Controls.Add((Control) this.tableLayoutPanel1, 0, 1);
    this.tlpMainGrid.Dock = DockStyle.Fill;
    this.tlpMainGrid.Location = new Point(4, 4);
    this.tlpMainGrid.Margin = new Padding(0);
    this.tlpMainGrid.Name = "tlpMainGrid";
    this.tlpMainGrid.RowCount = 2;
    this.tlpMainGrid.RowStyles.Add(new RowStyle());
    this.tlpMainGrid.RowStyles.Add(new RowStyle());
    this.tlpMainGrid.Size = new Size(418, 286);
    this.tlpMainGrid.TabIndex = 0;
    this.tlpArchiveLocation.AutoSize = true;
    this.tlpArchiveLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.tlpArchiveLocation.ColumnCount = 2;
    this.tlpArchiveLocation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpArchiveLocation.ColumnStyles.Add(new ColumnStyle());
    this.tlpArchiveLocation.Controls.Add((Control) this.btArchiveLocation, 1, 1);
    this.tlpArchiveLocation.Controls.Add((Control) this.tbArchiveLocation, 0, 1);
    this.tlpArchiveLocation.Controls.Add((Control) this.lbArchiveLocation, 0, 0);
    this.tlpArchiveLocation.Dock = DockStyle.Fill;
    this.tlpArchiveLocation.Location = new Point(0, 0);
    this.tlpArchiveLocation.Margin = new Padding(0);
    this.tlpArchiveLocation.Name = "tlpArchiveLocation";
    this.tlpArchiveLocation.RowCount = 2;
    this.tlpArchiveLocation.RowStyles.Add(new RowStyle());
    this.tlpArchiveLocation.RowStyles.Add(new RowStyle());
    this.tlpArchiveLocation.Size = new Size(418, 60);
    this.tlpArchiveLocation.TabIndex = 0;
    this.btArchiveLocation.AutoSize = true;
    this.btArchiveLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.btArchiveLocation.Location = new Point(389, 34);
    this.btArchiveLocation.Name = "btArchiveLocation";
    this.btArchiveLocation.Size = new Size(26, 23);
    this.btArchiveLocation.TabIndex = 0;
    this.btArchiveLocation.Text = "...";
    this.btArchiveLocation.UseVisualStyleBackColor = true;
    this.tbArchiveLocation.Dock = DockStyle.Top;
    this.tbArchiveLocation.Location = new Point(5, 35);
    this.tbArchiveLocation.Margin = new Padding(5, 4, 3, 3);
    this.tbArchiveLocation.Name = "tbArchiveLocation";
    this.tbArchiveLocation.Size = new Size(378, 20);
    this.tbArchiveLocation.TabIndex = 1;
    this.lbArchiveLocation.AutoSize = true;
    this.tlpArchiveLocation.SetColumnSpan((Control) this.lbArchiveLocation, 2);
    this.lbArchiveLocation.Location = new Point(3, 3);
    this.lbArchiveLocation.Margin = new Padding(3);
    this.lbArchiveLocation.Name = "lbArchiveLocation";
    this.lbArchiveLocation.Padding = new Padding(0, 8, 0, 4);
    this.lbArchiveLocation.Size = new Size(243, 25);
    this.lbArchiveLocation.TabIndex = 2;
    this.lbArchiveLocation.Text = "Расположение исходного архива предприятия";
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.lbImportBatchSize, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.nudImportBatchSize, 0, 1);
    this.tableLayoutPanel1.Dock = DockStyle.Top;
    this.tableLayoutPanel1.Location = new Point(0, 68);
    this.tableLayoutPanel1.Margin = new Padding(0, 8, 0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 2;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.Size = new Size(418, 100);
    this.tableLayoutPanel1.TabIndex = 1;
    this.lbImportBatchSize.AutoSize = true;
    this.lbImportBatchSize.Dock = DockStyle.Fill;
    this.lbImportBatchSize.Location = new Point(3, 3);
    this.lbImportBatchSize.Margin = new Padding(3);
    this.lbImportBatchSize.Name = "lbImportBatchSize";
    this.lbImportBatchSize.Padding = new Padding(0, 8, 0, 4);
    this.lbImportBatchSize.Size = new Size(412, 25);
    this.lbImportBatchSize.TabIndex = 0;
    this.lbImportBatchSize.Text = "Количество файлов, выделяемое пользователю из очереди импорта";
    this.nudImportBatchSize.Dock = DockStyle.Left;
    this.nudImportBatchSize.Increment = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this.nudImportBatchSize.Location = new Point(5, 35);
    this.nudImportBatchSize.Margin = new Padding(5, 4, 3, 3);
    this.nudImportBatchSize.Maximum = new Decimal(new int[4]
    {
      1000,
      0,
      0,
      0
    });
    this.nudImportBatchSize.Minimum = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this.nudImportBatchSize.Name = "nudImportBatchSize";
    this.nudImportBatchSize.Size = new Size(100, 20);
    this.nudImportBatchSize.TabIndex = 1;
    this.nudImportBatchSize.Value = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tlpMainGrid);
    this.Margin = new Padding(8);
    this.Name = nameof (ArchiveParametersControl);
    this.Padding = new Padding(4);
    this.Size = new Size(426, 294);
    this.tlpMainGrid.ResumeLayout(false);
    this.tlpMainGrid.PerformLayout();
    this.tlpArchiveLocation.ResumeLayout(false);
    this.tlpArchiveLocation.PerformLayout();
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.nudImportBatchSize.EndInit();
    this.ResumeLayout(false);
  }
}
