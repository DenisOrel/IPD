
// Type: Intermech.Client.Core.BrowseFolderDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Summary description for BrowseFolderDialog.</summary>
public class BrowseFolderDialog : Form
{
  private IBrowseFolder _browser;
  private string _path;
  private Button btOK;
  private Button btCancel;
  private ImageList imageList1;
  private TreeView treeView;
  private System.Timers.Timer timer;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem miNewFolder;
  private IContainer components;
  private BrowseFolderDialogOptions _options;

  public BrowseFolderDialog(IBrowseFolder browser)
    : this(browser, BrowseFolderDialogOptions.None)
  {
  }

  public BrowseFolderDialog(IBrowseFolder browser, BrowseFolderDialogOptions options)
  {
    this._browser = browser;
    this._options = options;
    this.InitializeComponent();
  }

  private void Prescan()
  {
    foreach (string drives in this._browser.DrivesList)
    {
      TreeNode treeNode = new TreeNode(drives)
      {
        SelectedImageIndex = 0,
        ImageIndex = 0
      };
      this.treeView.Nodes.Add(treeNode);
      this.PopulateNode(treeNode);
    }
  }

  private void PopulateNode(TreeNode parent)
  {
    try
    {
      if (parent.Nodes.Count == 0)
      {
        foreach (string folder in this._browser.GetFolders(parent.FullPath))
        {
          TreeNode node = new TreeNode(folder)
          {
            ImageIndex = 1,
            SelectedImageIndex = 2
          };
          parent.Nodes.Add(node);
        }
      }
      foreach (TreeNode node1 in parent.Nodes)
      {
        if (node1.Nodes.Count == 0)
        {
          foreach (string folder in this._browser.GetFolders(node1.FullPath))
          {
            TreeNode node2 = new TreeNode(folder)
            {
              ImageIndex = 1,
              SelectedImageIndex = 2
            };
            node1.Nodes.Add(node2);
          }
        }
      }
    }
    catch
    {
    }
  }

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (BrowseFolderDialog));
    this.btOK = new Button();
    this.btCancel = new Button();
    this.treeView = new TreeView();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this.miNewFolder = new ToolStripMenuItem();
    this.imageList1 = new ImageList(this.components);
    this.timer = new System.Timers.Timer();
    this.contextMenuStrip1.SuspendLayout();
    this.timer.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btOK, "btOK");
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Name = "btOK";
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Name = "btCancel";
    componentResourceManager.ApplyResources((object) this.treeView, "treeView");
    this.treeView.ContextMenuStrip = this.contextMenuStrip1;
    this.treeView.ImageList = this.imageList1;
    this.treeView.Name = "treeView";
    this.treeView.BeforeSelect += new TreeViewCancelEventHandler(this.treeView_BeforeSelect);
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.miNewFolder
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    componentResourceManager.ApplyResources((object) this.contextMenuStrip1, "contextMenuStrip1");
    this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
    this.miNewFolder.Name = "miNewFolder";
    componentResourceManager.ApplyResources((object) this.miNewFolder, "miNewFolder");
    this.miNewFolder.Click += new EventHandler(this.miNewFolder_Click);
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Magenta;
    this.imageList1.Images.SetKeyName(0, "");
    this.imageList1.Images.SetKeyName(1, "");
    this.imageList1.Images.SetKeyName(2, "");
    this.timer.Enabled = true;
    this.timer.SynchronizingObject = (ISynchronizeInvoke) this;
    this.timer.Elapsed += new ElapsedEventHandler(this.timer1_Elapsed);
    this.AcceptButton = (IButtonControl) this.btOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btCancel;
    this.Controls.Add((Control) this.treeView);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btOK);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (BrowseFolderDialog);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.contextMenuStrip1.ResumeLayout(false);
    this.timer.EndInit();
    this.ResumeLayout(false);
  }

  private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
  {
    this.PopulateNode(e.Node);
    this._path = e.Node.FullPath;
  }

  private void timer1_Elapsed(object sender, ElapsedEventArgs e)
  {
    this.timer.Enabled = false;
    this.Prescan();
  }

  public string Path => this._path;

  private void miNewFolder_Click(object sender, EventArgs e)
  {
    using (NewFolderNameDialog folderNameDialog = new NewFolderNameDialog())
    {
      if (folderNameDialog.ShowDialog() != DialogResult.OK)
        return;
      TreeNode selectedNode = this.treeView.SelectedNode;
      this._browser.CreateFolder(selectedNode.FullPath, folderNameDialog.FolderName);
      this.treeView.SelectedNode.Nodes.Clear();
      this.PopulateNode(this.treeView.SelectedNode);
      this._path = System.IO.Path.Combine(selectedNode.FullPath, folderNameDialog.FolderName);
      foreach (TreeNode node in this.treeView.SelectedNode.Nodes)
      {
        if ((node.Level == 1 ? node.FullPath.Replace("\\\\", "\\") : node.FullPath).Equals(this._path))
        {
          this.treeView.SelectedNode = node;
          break;
        }
      }
    }
  }

  private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
  {
    this.miNewFolder.Enabled = this.treeView.SelectedNode != null && (this._options & BrowseFolderDialogOptions.CreateFolderEnable) == BrowseFolderDialogOptions.CreateFolderEnable;
  }

  private enum ImagesIndex
  {
    Drive,
    Folder,
    FolderOpen,
  }
}
