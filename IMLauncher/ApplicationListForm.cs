// Decompiled with JetBrains decompiler
// Type: IMLauncher.ApplicationListForm
// Assembly: IMLauncher, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DAC2135C-3212-4DE0-9552-DF99FF4FD793
// Assembly location: D:\IPS\Client\IMLauncher.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace IMLauncher;

internal class ApplicationListForm : Form
{
  private Launcher imLauncher = new Launcher();
  private IContainer components;
  private Button btnClose;
  private Button btnRun;
  private ImageList imIcon;
  private Panel panel1;
  private DataGridView applicationDataGrid;
  private Panel panel2;
  private Button addApplication;
  private OpenFileDialog addApplicationDialog;
  private TextBox addedProgrammName;
  private Button btnAddPath;
  private ToolStripMenuItem miDelete;
  private ContextMenuStrip cmsProgramm;
  private NotifyIcon notifyIcon1;
  private ContextMenuStrip cmsNotifyIcon;
  private ToolStripMenuItem toolStripMenuItem2;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripMenuItem toolStripMenuItem1;
  private DataGridViewImageColumn icon;
  private DataGridViewTextBoxColumn appName;
  private DataGridViewTextBoxColumn appPath;
  private DataGridViewTextBoxColumn imbaseLibrary;
  private DataGridViewTextBoxColumn appArgs;
  private DataGridViewTextBoxColumn IsCustom;
  private ToolStripMenuItem miCOM;
  private ListBox lbPaths;
  private Button btnRemovePath;
  private Label label2;
  private Label label1;
  private ToolTip toolTip1;

  public ApplicationListForm() => this.InitializeComponent();

  public AppServiceContext AppServices { get; set; }

  private void button1_Click(object sender, EventArgs e) => this.Close();

  private void Form1_Load(object sender, EventArgs e)
  {
    try
    {
      this.imLauncher.MakeListOfProgramm();
      foreach (ProgrammInfo listOfProgramm in this.imLauncher.ListOfProgramms)
      {
        string empty = string.Empty;
        string str = string.Join(Environment.NewLine, listOfProgramm.ProgrammPaths);
        Bitmap bitmap = (Bitmap) null;
        if (File.Exists(listOfProgramm.ProgrammPaths[0]))
        {
          Icon icon;
          try
          {
            icon = Icon.ExtractAssociatedIcon(listOfProgramm.ProgrammPaths[0]);
          }
          catch
          {
            icon = (Icon) null;
          }
          bitmap = icon == null ? (Bitmap) null : icon.ToBitmap();
        }
        int index = this.applicationDataGrid.Rows.Add((object) bitmap, (object) listOfProgramm.ProgrammName, (object) str, (object) listOfProgramm.IMBaseLibrary, (object) listOfProgramm.ProgrammArguments, (object) listOfProgramm.Flags);
        if ((listOfProgramm.Flags & AdditionalInfo.Com) != AdditionalInfo.None)
          this.applicationDataGrid.Rows[index].DefaultCellStyle.BackColor = Color.BlanchedAlmond;
      }
    }
    catch (Exception ex)
    {
      this.AppServices.ExceptionService.ShowException(ex);
    }
  }

  private void button2_Click(object sender, EventArgs e)
  {
    DataGridViewRow row = this.applicationDataGrid.Rows[this.applicationDataGrid.CurrentRow.Index];
    string str = Convert.ToString(row.Cells[this.appPath.Index].Value);
    string[] separator = new string[1]
    {
      Environment.NewLine
    };
    foreach (string exeName in str.Split(separator, StringSplitOptions.RemoveEmptyEntries))
      this.imLauncher.StartProcess(exeName, Convert.ToString(row.Cells[this.imbaseLibrary.Index].Value), Convert.ToString(row.Cells[this.appArgs.Index].Value));
  }

  private void applicationDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
  {
    string[] paths = Convert.ToString(this.applicationDataGrid[this.appPath.Index, e.RowIndex].Value).Split(new string[1]
    {
      Environment.NewLine
    }, StringSplitOptions.RemoveEmptyEntries);
    this.StartProgramm(Convert.ToString(this.applicationDataGrid[this.imbaseLibrary.Index, e.RowIndex].Value), Convert.ToString(this.applicationDataGrid[this.appArgs.Index, e.RowIndex].Value), paths);
  }

  private void StartProgramm(string library, string args, string[] paths)
  {
    foreach (string path in paths)
    {
      try
      {
        if (library.Equals(LauncherConsts.CAD_IM_BASE_LIBRARY))
        {
          using (ShowExistsLibrary showExistsLibrary = new ShowExistsLibrary(this.imLauncher.ListOfImBase_net))
          {
            if (showExistsLibrary.ShowDialog() != DialogResult.OK)
              break;
            library = showExistsLibrary.ImLibrary;
          }
        }
        this.imLauncher.StartProcess(path, library, args);
      }
      catch (Win32Exception ex)
      {
        int num = (int) MessageBox.Show($"{ex.Message}: {path}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }
  }

  private void applicationDataGrid_CurrentCellChanged(object sender, EventArgs e)
  {
    if (this.applicationDataGrid.CurrentRow != null && this.applicationDataGrid[this.appPath.Index, this.applicationDataGrid.CurrentRow.Index].Value != null)
      this.btnRun.Enabled = true;
    else
      this.btnRun.Enabled = false;
  }

  private void addApplication_Click(object sender, EventArgs e)
  {
    List<string> stringList = new List<string>(this.lbPaths.Items.Count);
    foreach (string str in this.lbPaths.Items)
      stringList.Add(str);
    AdditionalInfo xml = this.imLauncher.AddProgrammToXml(this.addedProgrammName.Text, stringList.ToArray());
    string str1 = string.Join(Environment.NewLine, stringList.ToArray());
    Bitmap bitmap = (Bitmap) null;
    if (File.Exists(stringList[0]))
    {
      Icon associatedIcon = Icon.ExtractAssociatedIcon(stringList[0]);
      bitmap = associatedIcon == null ? (Bitmap) null : associatedIcon.ToBitmap();
    }
    this.applicationDataGrid.Rows.Add((object) bitmap, (object) this.addedProgrammName.Text, (object) str1, (object) string.Empty, (object) string.Empty, (object) xml);
    this.addedProgrammName.Text = string.Empty;
    this.lbPaths.Items.Clear();
    this.UpdateControls();
  }

  private void button1_Click_1(object sender, EventArgs e)
  {
    if (this.addApplicationDialog.ShowDialog() != DialogResult.OK || this.addApplicationDialog.FileNames.Length == 0)
      return;
    foreach (object fileName in this.addApplicationDialog.FileNames)
      this.lbPaths.Items.Add(fileName);
    this.UpdateControls();
  }

  private void UpdateControls()
  {
    this.addApplication.Enabled = this.lbPaths.Items.Count > 0;
    this.btnRemovePath.Enabled = this.lbPaths.SelectedItem != null;
  }

  private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    if (this.WindowState != FormWindowState.Minimized)
      return;
    this.Show();
    this.WindowState = FormWindowState.Normal;
    this.notifyIcon1.Visible = false;
  }

  private void ApplicationListForm_Resize(object sender, EventArgs e)
  {
    if (this.WindowState != FormWindowState.Minimized)
      return;
    this.notifyIcon1.Visible = true;
    this.Hide();
  }

  private void toolStripMenuItem1_Click(object sender, EventArgs e) => this.Close();

  private void toolStripMenuItem2_Click(object sender, EventArgs e)
  {
    if (this.WindowState != FormWindowState.Minimized)
      return;
    this.Show();
    this.WindowState = FormWindowState.Normal;
    this.notifyIcon1.Visible = false;
  }

  private void miDelete_Click(object sender, EventArgs e)
  {
    if (this.applicationDataGrid.SelectedRows == null || this.applicationDataGrid.SelectedRows.Count != 1)
      return;
    DataGridViewRow selectedRow = this.applicationDataGrid.SelectedRows[0];
    this.imLauncher.RemoveProgrammFromXml(selectedRow.Cells[this.appName.Index].Value.ToString(), selectedRow.Cells[this.appPath.Index].Value.ToString().Split(new string[1]
    {
      Environment.NewLine
    }, StringSplitOptions.RemoveEmptyEntries));
    this.applicationDataGrid.Rows.Remove(selectedRow);
  }

  private void miCOM_Click(object sender, EventArgs e)
  {
    if (this.applicationDataGrid.SelectedRows == null)
      return;
    if (Process.GetProcessesByName("IMClient").Length != 0)
    {
      int num1 = (int) MessageBox.Show("Для выполнения команды необходимо закрыть все запущенные клиенты IPS", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      DataGridViewRow selectedRow = this.applicationDataGrid.SelectedRows[0];
      this.ResetComRow();
      AdditionalInfo additionalInfo = (AdditionalInfo) selectedRow.Cells[this.IsCustom.Index].Value;
      string paths = Convert.ToString(selectedRow.Cells[this.appPath.Index].Value);
      try
      {
        this.imLauncher.TurnOnCom(paths);
        selectedRow.Cells[this.IsCustom.Index].Value = (object) (additionalInfo | AdditionalInfo.Com);
        this.applicationDataGrid.Rows[selectedRow.Index].DefaultCellStyle.BackColor = Color.BlanchedAlmond;
      }
      catch
      {
        int num2 = (int) MessageBox.Show("Не удалось влючить COM", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }
  }

  private void ResetComRow()
  {
    foreach (DataGridViewRow row in (IEnumerable) this.applicationDataGrid.Rows)
    {
      AdditionalInfo additionalInfo1 = (AdditionalInfo) row.Cells[this.IsCustom.Index].Value;
      if ((additionalInfo1 & AdditionalInfo.Com) != AdditionalInfo.None)
      {
        row.DefaultCellStyle.BackColor = Color.White;
        AdditionalInfo additionalInfo2 = additionalInfo1 & ~AdditionalInfo.Com;
        row.Cells[this.IsCustom.Index].Value = (object) additionalInfo2;
      }
    }
  }

  private void applicationDataGrid_Resize(object sender, EventArgs e)
  {
    int num = this.applicationDataGrid.Width - this.icon.Width - this.appName.Width;
    this.appPath.Width = num > 160 /*0xA0*/ ? num : 160 /*0xA0*/;
  }

  private void lbPaths_SelectedIndexChanged(object sender, EventArgs e) => this.UpdateControls();

  private void btnRemovePath_Click(object sender, EventArgs e)
  {
    if (this.lbPaths.SelectedItem == null)
      return;
    this.lbPaths.Items.Remove(this.lbPaths.SelectedItem);
  }

  private void cmsProgramm_Opening(object sender, CancelEventArgs e)
  {
    if (this.applicationDataGrid.SelectedRows == null || this.applicationDataGrid.SelectedRows.Count != 1)
    {
      this.miDelete.Enabled = this.miCOM.Enabled = false;
      e.Cancel = true;
    }
    else
    {
      AdditionalInfo additionalInfo = (AdditionalInfo) this.applicationDataGrid.SelectedRows[0].Cells[this.IsCustom.Index].Value;
      this.miDelete.Enabled = (additionalInfo & AdditionalInfo.Custom) == AdditionalInfo.Custom;
      this.miCOM.Enabled = false;
      if ((additionalInfo & AdditionalInfo.IMClient) == AdditionalInfo.None)
        return;
      this.miCOM.Enabled = true;
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ApplicationListForm));
    DataGridViewCellStyle gridViewCellStyle = new DataGridViewCellStyle();
    this.btnClose = new Button();
    this.btnRun = new Button();
    this.imIcon = new ImageList(this.components);
    this.panel1 = new Panel();
    this.applicationDataGrid = new DataGridView();
    this.icon = new DataGridViewImageColumn();
    this.appName = new DataGridViewTextBoxColumn();
    this.appPath = new DataGridViewTextBoxColumn();
    this.imbaseLibrary = new DataGridViewTextBoxColumn();
    this.appArgs = new DataGridViewTextBoxColumn();
    this.IsCustom = new DataGridViewTextBoxColumn();
    this.cmsProgramm = new ContextMenuStrip(this.components);
    this.miDelete = new ToolStripMenuItem();
    this.miCOM = new ToolStripMenuItem();
    this.panel2 = new Panel();
    this.label2 = new Label();
    this.label1 = new Label();
    this.btnRemovePath = new Button();
    this.lbPaths = new ListBox();
    this.btnAddPath = new Button();
    this.addedProgrammName = new TextBox();
    this.addApplication = new Button();
    this.addApplicationDialog = new OpenFileDialog();
    this.notifyIcon1 = new NotifyIcon(this.components);
    this.cmsNotifyIcon = new ContextMenuStrip(this.components);
    this.toolStripMenuItem2 = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.toolStripMenuItem1 = new ToolStripMenuItem();
    this.toolTip1 = new ToolTip(this.components);
    this.panel1.SuspendLayout();
    ((ISupportInitialize) this.applicationDataGrid).BeginInit();
    this.cmsProgramm.SuspendLayout();
    this.panel2.SuspendLayout();
    this.cmsNotifyIcon.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnClose, "btnClose");
    this.btnClose.Name = "btnClose";
    this.btnClose.UseVisualStyleBackColor = true;
    this.btnClose.Click += new EventHandler(this.button1_Click);
    componentResourceManager.ApplyResources((object) this.btnRun, "btnRun");
    this.btnRun.Name = "btnRun";
    this.btnRun.UseVisualStyleBackColor = true;
    this.btnRun.Click += new EventHandler(this.button2_Click);
    this.imIcon.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imIcon.ImageStream");
    this.imIcon.TransparentColor = Color.Transparent;
    this.imIcon.Images.SetKeyName(0, "s4.ico");
    this.imIcon.Images.SetKeyName(1, "App.ico");
    this.panel1.Controls.Add((Control) this.btnClose);
    this.panel1.Controls.Add((Control) this.btnRun);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.applicationDataGrid.AllowUserToAddRows = false;
    this.applicationDataGrid.AllowUserToDeleteRows = false;
    this.applicationDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
    this.applicationDataGrid.BackgroundColor = SystemColors.ControlLightLight;
    this.applicationDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.applicationDataGrid.Columns.AddRange((DataGridViewColumn) this.icon, (DataGridViewColumn) this.appName, (DataGridViewColumn) this.appPath, (DataGridViewColumn) this.imbaseLibrary, (DataGridViewColumn) this.appArgs, (DataGridViewColumn) this.IsCustom);
    this.applicationDataGrid.ContextMenuStrip = this.cmsProgramm;
    componentResourceManager.ApplyResources((object) this.applicationDataGrid, "applicationDataGrid");
    this.applicationDataGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
    this.applicationDataGrid.GridColor = SystemColors.ButtonShadow;
    this.applicationDataGrid.MultiSelect = false;
    this.applicationDataGrid.Name = "applicationDataGrid";
    this.applicationDataGrid.ReadOnly = true;
    this.applicationDataGrid.RowHeadersVisible = false;
    this.applicationDataGrid.RowTemplate.Height = 24;
    this.applicationDataGrid.RowTemplate.Resizable = DataGridViewTriState.True;
    this.applicationDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.applicationDataGrid.CellDoubleClick += new DataGridViewCellEventHandler(this.applicationDataGrid_CellDoubleClick);
    this.applicationDataGrid.CurrentCellChanged += new EventHandler(this.applicationDataGrid_CurrentCellChanged);
    this.applicationDataGrid.Resize += new EventHandler(this.applicationDataGrid_Resize);
    this.icon.Frozen = true;
    componentResourceManager.ApplyResources((object) this.icon, "icon");
    this.icon.Name = "icon";
    this.icon.ReadOnly = true;
    this.icon.Resizable = DataGridViewTriState.False;
    this.appName.Frozen = true;
    componentResourceManager.ApplyResources((object) this.appName, "appName");
    this.appName.Name = "appName";
    this.appName.ReadOnly = true;
    this.appPath.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
    gridViewCellStyle.WrapMode = DataGridViewTriState.True;
    this.appPath.DefaultCellStyle = gridViewCellStyle;
    this.appPath.Frozen = true;
    componentResourceManager.ApplyResources((object) this.appPath, "appPath");
    this.appPath.Name = "appPath";
    this.appPath.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.imbaseLibrary, "imbaseLibrary");
    this.imbaseLibrary.Name = "imbaseLibrary";
    this.imbaseLibrary.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.appArgs, "appArgs");
    this.appArgs.Name = "appArgs";
    this.appArgs.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.IsCustom, "IsCustom");
    this.IsCustom.Name = "IsCustom";
    this.IsCustom.ReadOnly = true;
    this.cmsProgramm.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.miDelete,
      (ToolStripItem) this.miCOM
    });
    this.cmsProgramm.Name = "contextMenuStrip1";
    componentResourceManager.ApplyResources((object) this.cmsProgramm, "cmsProgramm");
    this.cmsProgramm.Opening += new CancelEventHandler(this.cmsProgramm_Opening);
    this.miDelete.Name = "miDelete";
    componentResourceManager.ApplyResources((object) this.miDelete, "miDelete");
    this.miDelete.Click += new EventHandler(this.miDelete_Click);
    this.miCOM.Name = "miCOM";
    componentResourceManager.ApplyResources((object) this.miCOM, "miCOM");
    this.miCOM.Click += new EventHandler(this.miCOM_Click);
    this.panel2.BorderStyle = BorderStyle.Fixed3D;
    this.panel2.Controls.Add((Control) this.label2);
    this.panel2.Controls.Add((Control) this.label1);
    this.panel2.Controls.Add((Control) this.btnRemovePath);
    this.panel2.Controls.Add((Control) this.lbPaths);
    this.panel2.Controls.Add((Control) this.btnAddPath);
    this.panel2.Controls.Add((Control) this.addedProgrammName);
    this.panel2.Controls.Add((Control) this.addApplication);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.btnRemovePath, "btnRemovePath");
    this.btnRemovePath.Name = "btnRemovePath";
    this.toolTip1.SetToolTip((Control) this.btnRemovePath, componentResourceManager.GetString("btnRemovePath.ToolTip"));
    this.btnRemovePath.UseVisualStyleBackColor = true;
    this.btnRemovePath.Click += new EventHandler(this.btnRemovePath_Click);
    componentResourceManager.ApplyResources((object) this.lbPaths, "lbPaths");
    this.lbPaths.FormattingEnabled = true;
    this.lbPaths.Name = "lbPaths";
    this.lbPaths.SelectedIndexChanged += new EventHandler(this.lbPaths_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.btnAddPath, "btnAddPath");
    this.btnAddPath.Name = "btnAddPath";
    this.toolTip1.SetToolTip((Control) this.btnAddPath, componentResourceManager.GetString("btnAddPath.ToolTip"));
    this.btnAddPath.UseVisualStyleBackColor = true;
    this.btnAddPath.Click += new EventHandler(this.button1_Click_1);
    componentResourceManager.ApplyResources((object) this.addedProgrammName, "addedProgrammName");
    this.addedProgrammName.Name = "addedProgrammName";
    componentResourceManager.ApplyResources((object) this.addApplication, "addApplication");
    this.addApplication.Name = "addApplication";
    this.addApplication.UseVisualStyleBackColor = true;
    this.addApplication.Click += new EventHandler(this.addApplication_Click);
    componentResourceManager.ApplyResources((object) this.addApplicationDialog, "addApplicationDialog");
    this.addApplicationDialog.Multiselect = true;
    this.addApplicationDialog.RestoreDirectory = true;
    this.notifyIcon1.ContextMenuStrip = this.cmsNotifyIcon;
    componentResourceManager.ApplyResources((object) this.notifyIcon1, "notifyIcon1");
    this.notifyIcon1.MouseDoubleClick += new MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
    this.cmsNotifyIcon.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.toolStripMenuItem2,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.toolStripMenuItem1
    });
    this.cmsNotifyIcon.Name = "contextMenuStrip2";
    componentResourceManager.ApplyResources((object) this.cmsNotifyIcon, "cmsNotifyIcon");
    this.toolStripMenuItem2.Name = "toolStripMenuItem2";
    componentResourceManager.ApplyResources((object) this.toolStripMenuItem2, "toolStripMenuItem2");
    this.toolStripMenuItem2.Click += new EventHandler(this.toolStripMenuItem2_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    componentResourceManager.ApplyResources((object) this.toolStripMenuItem1, "toolStripMenuItem1");
    this.toolStripMenuItem1.Click += new EventHandler(this.toolStripMenuItem1_Click);
    this.AcceptButton = (IButtonControl) this.btnRun;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.applicationDataGrid);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (ApplicationListForm);
    this.Load += new EventHandler(this.Form1_Load);
    this.Resize += new EventHandler(this.ApplicationListForm_Resize);
    this.panel1.ResumeLayout(false);
    ((ISupportInitialize) this.applicationDataGrid).EndInit();
    this.cmsProgramm.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.cmsNotifyIcon.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
