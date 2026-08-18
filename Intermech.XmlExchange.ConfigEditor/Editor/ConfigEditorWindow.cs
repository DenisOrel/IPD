// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.Editor.ConfigEditorWindow
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Search;
using Intermech.Search.Configuration;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.Editor;

public class ConfigEditorWindow : DockControl
{
  private static readonly Guid _persistStateGuid = new Guid("{B2CECD1F-358C-4C12-954D-682F30C9AB62}");
  private long _idObjectConfig;
  private string _pathFile = string.Empty;
  private IConfigEditor _configEditor;
  private bool _readOnly;
  private ConfigEditorHelper _helper;
  private IContainer components;
  private ContextMenuStrip _contextMenu;
  private Button btSaveConfig;
  private Button btLoadConfig;
  private TreeView treeView;
  private SplitContainer splitContainer1;
  private Panel pnlRight;
  private EditorTabControl editorTabControl;
  private Button btSaveInFile;
  private Button btLoadInFile;
  private Button btSaveAsInFile;

  public ConfigEditorWindow()
  {
    this.Guid = ConfigEditorWindow._persistStateGuid;
    this.InitializeComponent();
  }

  private void InitializeCustomComponent(bool readOnly)
  {
    this._readOnly = readOnly;
    if (this.DesignMode)
      return;
    this.TabImage = (Image) ImageResources.configEditor.ToBitmap();
    this.ShowImageInDocumentTab = true;
    this._helper = ConfigEditorHelper.GetHelper();
    if (this._helper != null)
    {
      this.treeView.ImageList = this._helper.CategoryIcons.ImageList;
      this._contextMenu.ImageList = this._helper.CategoryIcons.ImageList;
    }
    this.ReadOnly();
    this.editorTabControl.InitializeCustomComponent(this._readOnly);
    this.editorTabControl.UpdateTreeView += new EventHandler(this.EditorTabControl_UpdateTreeView);
    if (!ServiceLocator.IsRegistered<IConfigurationOptionRepository>() || !(ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.UI_TreeFont) is Font font))
      return;
    this.treeView.Font = font;
  }

  public static void ShowEditorWindow(IDBTypedObjectID objId, bool readOnly)
  {
    if (!readOnly)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session?.GetObject(objId.ObjectID).CheckEdit();
    }
    try
    {
      ConfigEditorWindow configEditorWindow = new ConfigEditorWindow();
      configEditorWindow.InitializeCustomComponent(readOnly);
      configEditorWindow.LoadDataInObject(objId);
      configEditorWindow.btLoadInFile.Enabled = false;
      configEditorWindow.btSaveInFile.Enabled = false;
      configEditorWindow.btSaveAsInFile.Enabled = false;
      configEditorWindow.Show(ServicesManager.GetService(typeof (DockManager)) as DockManager);
      configEditorWindow.Activate();
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, "Редактор конфигураций экспорта/импорта XML", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  public static void ShowEditorWindow()
  {
    try
    {
      ConfigEditorWindow configEditorWindow = new ConfigEditorWindow();
      configEditorWindow.InitializeCustomComponent(false);
      configEditorWindow.Text = "Редактор конфигураций экспорта/импорта XML";
      configEditorWindow.Show(ServicesManager.GetService(typeof (DockManager)) as DockManager);
      configEditorWindow.btLoadConfig.Enabled = false;
      configEditorWindow.btSaveConfig.Enabled = false;
      configEditorWindow.btLoadInFile.Enabled = true;
      configEditorWindow.btSaveInFile.Enabled = false;
      configEditorWindow.btSaveAsInFile.Enabled = false;
      configEditorWindow.Activate();
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, "Редактор конфигураций экспорта/импорта XML", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private void LoadDataInObject(IDBTypedObjectID objId)
  {
    this.Text = objId.Caption;
    this._idObjectConfig = objId.ObjectID;
    this._configEditor = FactoryConfigEditor.CreateConfigEditor(this.treeView, objId.ObjectID, objId.ObjectType, this._contextMenu);
    this.TabImage = this._configEditor.GetTabImage();
  }

  private void LoadDataInFile()
  {
    this._configEditor = FactoryConfigEditor.CreateConfigEditor(this.treeView, this._pathFile, this._contextMenu);
    if (this._configEditor == null)
      return;
    this.Text = new FileInfo(this._pathFile).Name;
    this.TabImage = this._configEditor.GetTabImage();
    this.btSaveInFile.Enabled = true;
    this.btSaveAsInFile.Enabled = true;
  }

  private void EditorTabControl_UpdateTreeView(object sender, EventArgs e)
  {
    this._configEditor.UpdateTreeView(sender, e);
  }

  private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.editorTabControl.SelectNode(this.treeView.SelectedNode.Tag);
  }

  private void ContextMenu_Opening(object sender, CancelEventArgs e)
  {
    Point client = this.treeView.PointToClient(this._contextMenu.Bounds.Location);
    TreeNode nodeAt = this.treeView.GetNodeAt(client.X, client.Y);
    if (nodeAt == null)
    {
      e.Cancel = true;
    }
    else
    {
      this.treeView.SelectedNode = nodeAt;
      this._configEditor.Menu_Opening(sender, e, nodeAt);
    }
  }

  private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
  {
    this.editorTabControl.SelectionChanged();
  }

  private void ReadOnly()
  {
    if (!this._readOnly)
      return;
    this.btLoadConfig.Enabled = false;
    this.btSaveConfig.Enabled = false;
    this._contextMenu.Enabled = false;
  }

  private void ConfigEditorWindow_Enter(object sender, EventArgs e)
  {
    this._configEditor?.EnterEditorWindow(sender, e);
  }

  private void btLoadConfig_Click(object sender, EventArgs e)
  {
    this._configEditor.LoadConfigInObject(this._idObjectConfig);
  }

  private void btSaveConfig_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show("Сохранить изменения?", "Конфигурация XML-экспорта", MessageBoxButtons.OKCancel) != DialogResult.OK)
      return;
    this._configEditor.SaveConfigInObject();
  }

  private void btLoadInFile_Click(object sender, EventArgs e)
  {
    using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
      openFileDialog.Title = "Открыть файл конфигурации";
      openFileDialog.Multiselect = false;
      openFileDialog.Filter = "Файл конфигурации(*.blb)|*.blb|Файл конфигурации(*.xml)|*.xml|Все файлы(*.*)|*.*";
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this._pathFile = openFileDialog.FileName;
      this.LoadDataInFile();
    }
  }

  private void btSaveInFile_Click(object sender, EventArgs e)
  {
    this._configEditor.SaveConfigInFile(string.Empty);
  }

  private void btSaveAsInFile_Click(object sender, EventArgs e)
  {
    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
    {
      saveFileDialog.Title = "Сохранить файл конфигурации";
      saveFileDialog.Filter = "Файл конфигурации(*.blb)|*.blb|Файл конфигурации(*.xml)|*.xml|Все файлы(*.*)|*.*";
      if (saveFileDialog.ShowDialog() != DialogResult.OK)
        return;
      string fileName = saveFileDialog.FileName;
      if (!(fileName != string.Empty) || !this._configEditor.SaveConfigInFile(fileName))
        return;
      this.Text = new FileInfo(fileName).Name;
    }
  }

  private void ConfigEditorWindow_Closing(object sender, CancelEventArgs e)
  {
    if (this._readOnly || this._configEditor == null)
      return;
    this.editorTabControl.SelectionChanged();
    switch (MessageBox.Show("Сохранить конфигурацию при закрытии редактора?", "Конфигурация XML-экспорта", MessageBoxButtons.YesNoCancel))
    {
      case DialogResult.Cancel:
        e.Cancel = true;
        break;
      case DialogResult.Yes:
        if (string.IsNullOrEmpty(this._pathFile))
        {
          this._configEditor.SaveConfigInObject();
          break;
        }
        this._configEditor.SaveConfigInFile(string.Empty);
        break;
    }
  }

  public override string Text
  {
    get => base.Text;
    set => base.Text = value + (this._readOnly ? " (только чтение)" : "");
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this._contextMenu = new ContextMenuStrip(this.components);
    this.btLoadConfig = new Button();
    this.btSaveConfig = new Button();
    this.treeView = new TreeView();
    this.splitContainer1 = new SplitContainer();
    this.btSaveAsInFile = new Button();
    this.btSaveInFile = new Button();
    this.btLoadInFile = new Button();
    this.pnlRight = new Panel();
    this.editorTabControl = new EditorTabControl();
    this._contextMenu.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.pnlRight.SuspendLayout();
    this.SuspendLayout();
    this._contextMenu.ImageScalingSize = new Size(32 /*0x20*/, 16 /*0x10*/);
    this._contextMenu.Name = "_contextMenu";
    this._contextMenu.AutoSize = true;
    this._contextMenu.Opening += new CancelEventHandler(this.ContextMenu_Opening);
    this.btLoadConfig.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btLoadConfig.Location = new Point(6, 572);
    this.btLoadConfig.Name = "btLoadConfig";
    this.btLoadConfig.Size = new Size(199, 23);
    this.btLoadConfig.TabIndex = 5;
    this.btLoadConfig.Text = "Загрузить конфигурацию из базы";
    this.btLoadConfig.UseVisualStyleBackColor = true;
    this.btLoadConfig.Click += new EventHandler(this.btLoadConfig_Click);
    this.btSaveConfig.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btSaveConfig.Location = new Point(211, 572);
    this.btSaveConfig.Name = "btSaveConfig";
    this.btSaveConfig.Size = new Size(199, 23);
    this.btSaveConfig.TabIndex = 6;
    this.btSaveConfig.Text = "Сохранить конфигурацию в базу";
    this.btSaveConfig.UseVisualStyleBackColor = true;
    this.btSaveConfig.Click += new EventHandler(this.btSaveConfig_Click);
    this.treeView.ContextMenuStrip = this._contextMenu;
    this.treeView.Dock = DockStyle.Fill;
    this.treeView.HideSelection = false;
    this.treeView.ItemHeight = 18;
    this.treeView.Location = new Point(0, 0);
    this.treeView.Name = "treeView";
    this.treeView.ShowNodeToolTips = true;
    this.treeView.Size = new Size(253, 609);
    this.treeView.TabIndex = 0;
    this.treeView.BeforeSelect += new TreeViewCancelEventHandler(this.treeView_BeforeSelect);
    this.treeView.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.Location = new Point(0, 0);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.treeView);
    this.splitContainer1.Panel1.RightToLeft = RightToLeft.No;
    this.splitContainer1.Panel2.Controls.Add((Control) this.btSaveAsInFile);
    this.splitContainer1.Panel2.Controls.Add((Control) this.btSaveInFile);
    this.splitContainer1.Panel2.Controls.Add((Control) this.btLoadInFile);
    this.splitContainer1.Panel2.Controls.Add((Control) this.btSaveConfig);
    this.splitContainer1.Panel2.Controls.Add((Control) this.pnlRight);
    this.splitContainer1.Panel2.Controls.Add((Control) this.btLoadConfig);
    this.splitContainer1.Panel2.RightToLeft = RightToLeft.No;
    this.splitContainer1.Size = new Size(1104, 609);
    this.splitContainer1.SplitterDistance = 253;
    this.splitContainer1.TabIndex = 7;
    this.btSaveAsInFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btSaveAsInFile.Location = new Point(659, 572);
    this.btSaveAsInFile.Name = "btSaveAsInFile";
    this.btSaveAsInFile.Size = new Size(105, 23);
    this.btSaveAsInFile.TabIndex = 9;
    this.btSaveAsInFile.Text = "Сохранить как";
    this.btSaveAsInFile.UseVisualStyleBackColor = true;
    this.btSaveAsInFile.Click += new EventHandler(this.btSaveAsInFile_Click);
    this.btSaveInFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btSaveInFile.Location = new Point(548, 572);
    this.btSaveInFile.Name = "btSaveInFile";
    this.btSaveInFile.Size = new Size(105, 23);
    this.btSaveInFile.TabIndex = 8;
    this.btSaveInFile.Text = "Сохранить";
    this.btSaveInFile.UseVisualStyleBackColor = true;
    this.btSaveInFile.Click += new EventHandler(this.btSaveInFile_Click);
    this.btLoadInFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btLoadInFile.Location = new Point(416, 572);
    this.btLoadInFile.Name = "btLoadInFile";
    this.btLoadInFile.Size = new Size(126, 23);
    this.btLoadInFile.TabIndex = 7;
    this.btLoadInFile.Text = "Загрузить из файла";
    this.btLoadInFile.UseVisualStyleBackColor = true;
    this.btLoadInFile.Click += new EventHandler(this.btLoadInFile_Click);
    this.pnlRight.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.pnlRight.AutoSize = true;
    this.pnlRight.Controls.Add((Control) this.editorTabControl);
    this.pnlRight.Location = new Point(0, 0);
    this.pnlRight.Name = "pnlRight";
    this.pnlRight.Size = new Size(847, 561);
    this.pnlRight.TabIndex = 0;
    this.editorTabControl.AutoSize = true;
    this.editorTabControl.BackgroundImageLayout = ImageLayout.Center;
    this.editorTabControl.Dock = DockStyle.Fill;
    this.editorTabControl.Location = new Point(0, 0);
    this.editorTabControl.Name = "editorTabControl";
    this.editorTabControl.Size = new Size(847, 561);
    this.editorTabControl.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoSize = true;
    this.Controls.Add((Control) this.splitContainer1);
    this.Name = nameof (ConfigEditorWindow);
    this.Size = new Size(1104, 609);
    this.Closing += new CancelEventHandler(this.ConfigEditorWindow_Closing);
    this.Enter += new EventHandler(this.ConfigEditorWindow_Enter);
    this._contextMenu.ResumeLayout(false);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.Panel2.PerformLayout();
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.pnlRight.ResumeLayout(false);
    this.pnlRight.PerformLayout();
    this.ResumeLayout(false);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }
}
