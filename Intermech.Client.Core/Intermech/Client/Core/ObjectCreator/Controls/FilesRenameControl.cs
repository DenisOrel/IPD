
// Type: Intermech.Client.Core.ObjectCreator.Controls.FilesRenameControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Client.Core.ObjectCreator.Controls;

/// <summary>
/// Контрол переименования файлов при создании объекта по прототипу
/// </summary>
public class FilesRenameControl : ObjectCreatorControl
{
  /// <summary>Произошли изменения</summary>
  private bool _changed;
  /// <summary>
  /// Флаг, указывающий, что сохранение закладки происходит 1 раз
  /// </summary>
  private bool _firstTime = true;
  /// <summary>Автоматическая установка значений в контролы</summary>
  private bool _autoSet;
  private bool _readed;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private SplitContainer splitContainer1;
  private Panel panel1;
  private Label label3;
  private Label label2;
  private TextBox tbComment;
  private TextBox tbName;
  private TreeList treeList1;
  private TreeListColumn treeListColumn1;

  public FilesRenameControl(CreatedObjectItem createdObject)
    : base(createdObject)
  {
    this.InitializeComponent();
    this._showBeforeDesForms = true;
    this._StepIsReadyCheckRequired = true;
  }

  public override bool Refresh(PageRefreshArgs args)
  {
    if (this._readed)
      return true;
    this.treeList1.Nodes.Clear();
    this.treeList1.BeginUpdate();
    this.treeList1.FocusedNodeChanged -= new FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute attributeById = sessionKeeper.Session.GetObject(this.CreatedObject.ObjectID).GetAttributeByID(sessionKeeper.Session.IdentHelper.FileAttributeID);
        if (attributeById != null)
        {
          for (int index = 0; index < attributeById.ValuesCount; ++index)
          {
            if (index > 0)
              attributeById.Index = index;
            if (!attributeById.IsNull)
            {
              BlobInformation blobInformation = (attributeById as IBlobReader).OpenBlob(-1);
              this.treeList1.AppendNode((object) new object[1]
              {
                (object) blobInformation.FileName
              }, (TreeListNode) null).Tag = (object) new FilesRenameControl.FileNodeInfo(blobInformation.FileName, blobInformation.Note);
            }
          }
        }
      }
    }
    finally
    {
      this.treeList1.EndUpdate();
      this.treeList1.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
    }
    if (this.treeList1.Nodes.Count > 0)
      this.treeList1_FocusedNodeChanged((object) this, new FocusedNodeChangedEventArgs((TreeListNode) null, this.treeList1.FocusedNode));
    this._readed = true;
    return true;
  }

  public override bool Save(PageSaveArgs args)
  {
    int num = this.SaveCore(args) ? 1 : 0;
    if (!this._firstTime)
      return num != 0;
    this._firstTime = false;
    return num != 0;
  }

  private bool SaveCore(PageSaveArgs args)
  {
    bool flag = false;
    if (this._changed)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IFileNamesService customService = (IFileNamesService) sessionKeeper.Session.GetCustomService(typeof (IFileNamesService));
        for (int index = 0; index < this.treeList1.Nodes.Count; ++index)
        {
          FilesRenameControl.FileNodeInfo tag = this.treeList1.Nodes[index].Tag as FilesRenameControl.FileNodeInfo;
          long[] objectIdByFileName = customService.GetObjectIDByFileName(tag.Name, sessionKeeper.Session.SessionGUID);
          if (objectIdByFileName.Length != 0 && Array.IndexOf<long>(objectIdByFileName, this.CreatedObject.ObjectID) < 0)
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(objectIdByFileName[0], true);
            args.Error = new Exception(string.Format(LocalizationHolder.rm.GetString("Client.Core_1540"), (object) tag.Name, (object) dbObject.NameInMessages));
            return false;
          }
        }
        IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this.CreatedObject.ObjectID, sessionKeeper.Session.IdentHelper.FileAttributeID);
        if (objectAttributeById != null)
        {
          int index1 = 0;
          for (int index2 = 0; index2 < objectAttributeById.ValuesCount; ++index2)
          {
            if (index2 > 0)
              objectAttributeById.Index = index2;
            if (!objectAttributeById.IsNull)
            {
              IBlobReader blobReader = objectAttributeById as IBlobReader;
              BlobInformation blobInformation = blobReader.OpenBlob(0);
              BlobInformation blobInfo = blobInformation.Clone();
              try
              {
                blobInfo.FileName = ((FilesRenameControl.FileNodeInfo) this.treeList1.Nodes[index1].Tag).Name;
                blobInfo.Note = ((FilesRenameControl.FileNodeInfo) this.treeList1.Nodes[index1].Tag).Note;
              }
              finally
              {
                blobReader.CloseBlob();
              }
              if (blobInformation.FileName != blobInfo.FileName || blobInformation.Note != blobInfo.Note)
              {
                (objectAttributeById as IBlobWriter).OpenBlob(blobInfo, true);
                flag = true;
              }
              ++index1;
            }
          }
        }
        this._StepIsReady = true;
      }
      this._changed = false;
    }
    if ((flag || this._firstTime) && this.CreatedObject.PrototypeID != 0L)
      this.CreatedObject.ObjCreator.FireFilesRenamedEvent(this.CreatedObject.ObjectID, this.CreatedObject.PrototypeID);
    return true;
  }

  private void tbName_TextChanged(object sender, EventArgs e)
  {
    if (this._autoSet || this.treeList1.FocusedNode == null || this.treeList1.FocusedNode.Tag == null)
      return;
    ((FilesRenameControl.FileNodeInfo) this.treeList1.FocusedNode.Tag).Name = this.tbName.Text;
    this.treeList1.FocusedNode.SetValue((object) 0, (object) this.tbName.Text);
    this._changed = true;
  }

  private void tbComment_TextChanged(object sender, EventArgs e)
  {
    if (this._autoSet || this.treeList1.FocusedNode == null || this.treeList1.FocusedNode.Tag == null)
      return;
    ((FilesRenameControl.FileNodeInfo) this.treeList1.FocusedNode.Tag).Note = this.tbComment.Text;
    this._changed = true;
  }

  private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    if (e.Node == null || e.Node.Tag == null)
      return;
    this._autoSet = true;
    try
    {
      FilesRenameControl.FileNodeInfo tag = e.Node.Tag as FilesRenameControl.FileNodeInfo;
      this.tbName.Text = tag.Name;
      this.tbComment.Text = tag.Note;
    }
    finally
    {
      this._autoSet = false;
    }
  }

  private void tbName_Leave(object sender, EventArgs e)
  {
    if (this.treeList1.FocusedNode == null || this.treeList1.FocusedNode.Tag == null)
      return;
    FilesRenameControl.FileNodeInfo tag = this.treeList1.FocusedNode.Tag as FilesRenameControl.FileNodeInfo;
    if (!(this.tbName.Text != string.Empty) || !(Path.GetExtension(this.tbName.Text) == string.Empty))
      return;
    this.tbName.Text += tag.Extention;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FilesRenameControl));
    this.splitContainer1 = new SplitContainer();
    this.treeList1 = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.panel1 = new Panel();
    this.label3 = new Label();
    this.label2 = new Label();
    this.tbComment = new TextBox();
    this.tbName = new TextBox();
    this.label1 = new Label();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.treeList1.BeginInit();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel1, "splitContainer1.Panel1");
    this.splitContainer1.Panel1.Controls.Add((Control) this.treeList1);
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel2, "splitContainer1.Panel2");
    this.splitContainer1.Panel2.Controls.Add((Control) this.panel1);
    componentResourceManager.ApplyResources((object) this.treeList1, "treeList1");
    this.treeList1.Columns.AddRange(new TreeListColumn[1]
    {
      this.treeListColumn1
    });
    this.treeList1.Name = "treeList1";
    this.treeList1.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
    componentResourceManager.ApplyResources((object) this.treeListColumn1, "treeListColumn1");
    this.treeListColumn1.Name = "treeListColumn1";
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((Control) this.label3);
    this.panel1.Controls.Add((Control) this.label2);
    this.panel1.Controls.Add((Control) this.tbComment);
    this.panel1.Controls.Add((Control) this.tbName);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.tbComment, "tbComment");
    this.tbComment.Name = "tbComment";
    this.tbComment.TextChanged += new EventHandler(this.tbComment_TextChanged);
    componentResourceManager.ApplyResources((object) this.tbName, "tbName");
    this.tbName.Name = "tbName";
    this.tbName.TextChanged += new EventHandler(this.tbName_TextChanged);
    this.tbName.Leave += new EventHandler(this.tbName_Leave);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.ForeColor = SystemColors.ControlDarkDark;
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this.label1);
    this.Name = nameof (FilesRenameControl);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.treeList1.EndInit();
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
  }

  private class FileNodeInfo
  {
    public string Name;
    public string Note;
    public string Extention;

    public FileNodeInfo(string name, string note)
    {
      this.Name = name;
      this.Note = note;
      this.Extention = Path.GetExtension(name);
    }
  }
}
