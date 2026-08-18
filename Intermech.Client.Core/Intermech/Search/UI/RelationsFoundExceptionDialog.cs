
// Type: Intermech.Search.UI.RelationsFoundExceptionDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.UI;

public sealed class RelationsFoundExceptionDialog : Form
{
  private RelationsFoundException _exception;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBox _textBox;
  private Button _closeButton;
  private SplitContainer splitContainer1;
  private ListView _listView;
  private PageViewsManager _pageViewsManager;
  private ColumnHeader _relationIDColumnHeader;
  private ColumnHeader _descriptionColumnHeader;

  public RelationsFoundExceptionDialog() => this.InitializeComponent();

  public RelationsFoundException Exception
  {
    get => this._exception;
    set
    {
      if (this._exception == value)
        return;
      this._exception = value;
      if (this._exception != null)
        this._textBox.Text = this._exception.Message;
      this._listView.BeginUpdate();
      try
      {
        this._listView.Items.Clear();
        foreach (long id in this._exception.RelationsID)
        {
          if (!RelationHelper.IsUnknownRelationID(id))
            this._listView.Items.Add(new ListViewItem(id.ToString())
            {
              Tag = (object) id
            });
        }
      }
      finally
      {
        this._listView.EndUpdate();
      }
      this._pageViewsManager.Services = (System.IServiceProvider) ServicesManager.ServiceContainer;
    }
  }

  private void RelationsFoundExceptionDialog_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void RelationsFoundExceptionDialog_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void ListView_SelectedIndexChanged(object sender, EventArgs e)
  {
    ListViewItem listViewItem = this._listView.SelectedItems.Cast<ListViewItem>().FirstOrDefault<ListViewItem>();
    if (listViewItem == null)
      return;
    this._pageViewsManager.UpdateViews((ISelectedItems) new RelationsFoundExceptionDialog.SelectedItemsStub((long) listViewItem.Tag), true);
  }

  private void CloseButton_Click(object sender, EventArgs e) => this.Close();

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
    this._textBox = new TextBox();
    this._closeButton = new Button();
    this.splitContainer1 = new SplitContainer();
    this._listView = new ListView();
    this._relationIDColumnHeader = new ColumnHeader();
    this._descriptionColumnHeader = new ColumnHeader();
    this._pageViewsManager = new PageViewsManager();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.SuspendLayout();
    this._textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._textBox.Location = new Point(12, 12);
    this._textBox.Multiline = true;
    this._textBox.Name = "_textBox";
    this._textBox.ReadOnly = true;
    this._textBox.Size = new Size(521, 55);
    this._textBox.TabIndex = 0;
    this._closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._closeButton.Location = new Point(458, 321);
    this._closeButton.Name = "_closeButton";
    this._closeButton.Size = new Size(75, 23);
    this._closeButton.TabIndex = 1;
    this._closeButton.Text = "Закрыть";
    this._closeButton.UseVisualStyleBackColor = true;
    this._closeButton.Click += new EventHandler(this.CloseButton_Click);
    this.splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.splitContainer1.Location = new Point(12, 73);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Orientation = Orientation.Horizontal;
    this.splitContainer1.Panel1.Controls.Add((Control) this._listView);
    this.splitContainer1.Panel2.Controls.Add((Control) this._pageViewsManager);
    this.splitContainer1.Size = new Size(521, 242);
    this.splitContainer1.SplitterDistance = 121;
    this.splitContainer1.TabIndex = 2;
    this._listView.Columns.AddRange(new ColumnHeader[2]
    {
      this._relationIDColumnHeader,
      this._descriptionColumnHeader
    });
    this._listView.Dock = DockStyle.Fill;
    this._listView.FullRowSelect = true;
    this._listView.GridLines = true;
    this._listView.Location = new Point(0, 0);
    this._listView.MultiSelect = false;
    this._listView.Name = "_listView";
    this._listView.Size = new Size(521, 121);
    this._listView.TabIndex = 0;
    this._listView.UseCompatibleStateImageBehavior = false;
    this._listView.View = View.Details;
    this._listView.SelectedIndexChanged += new EventHandler(this.ListView_SelectedIndexChanged);
    this._relationIDColumnHeader.Text = "Идентификатор связи";
    this._relationIDColumnHeader.Width = 150;
    this._descriptionColumnHeader.Text = "Описание";
    this._descriptionColumnHeader.Width = 350;
    this._pageViewsManager.ActiveViewPage = (IViewPage) null;
    this._pageViewsManager.CausesValidation = false;
    this._pageViewsManager.Dock = DockStyle.Fill;
    this._pageViewsManager.Font = new Font("Tahoma", 8.25f);
    this._pageViewsManager.Location = new Point(0, 0);
    this._pageViewsManager.Name = "_pageViewsManager";
    this._pageViewsManager.Padding = new Padding(10, 0, 0, 0);
    this._pageViewsManager.Size = new Size(521, 117);
    this._pageViewsManager.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(545, 356);
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this._closeButton);
    this.Controls.Add((Control) this._textBox);
    this.Name = nameof (RelationsFoundExceptionDialog);
    this.Text = "Внимание";
    this.FormClosed += new FormClosedEventHandler(this.RelationsFoundExceptionDialog_FormClosed);
    this.Load += new EventHandler(this.RelationsFoundExceptionDialog_Load);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private sealed class SelectedItemsStub : ISelectedItems, ISimpleSelectedItems
  {
    private long _relationID;
    private NodeID _nodeID;
    private IDBRelationID _dbRelationID;
    private INode _node;

    public SelectedItemsStub(long relationID)
    {
      this._relationID = !RelationHelper.IsUnknownRelationID(relationID) ? relationID : throw new ArgumentException();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(this._relationID);
        this._nodeID = new NodeID(-1, 0L, relation.PartID, 0L, relation.RelationID, -1, (string) null, relation.TypeID, 0L, 0L, ObjectFiltrationState.fsNotRequired, 0L, 0L, (string) null, relation.ProjID, relation.GUID, 0L);
        this._dbRelationID = (IDBRelationID) new DBRelationID(relation.RelationID, relation.PartID, relation.TypeID, 0L, relation.GUID, relation.ProjID);
        this._node = (INode) new RelationsFoundExceptionDialog.NodeStub(this._nodeID, this._dbRelationID);
      }
    }

    public bool IsCollage => false;

    public INodeID GetItemID(int index)
    {
      if (index != 0)
        throw new IndexOutOfRangeException();
      return (INodeID) this._nodeID;
    }

    public object GetParentData(int index, System.Type dataFormat) => (object) null;

    public NodeIDPath GetParentPath(int index) => (NodeIDPath) null;

    public int Count => 1;

    public object GetItemData(int index, System.Type dataFormat)
    {
      if (index != 0)
        throw new IndexOutOfRangeException();
      if (dataFormat == typeof (INodeID))
        return (object) this._nodeID;
      if (dataFormat == typeof (IDBRelationID))
        return (object) this._dbRelationID;
      return dataFormat == typeof (INode) ? (object) this._node : (object) null;
    }
  }

  private sealed class NodeStub : ObjectNode
  {
    private NodeID _nodeID;
    private IDBRelationID _relationID;

    public NodeStub(NodeID nodeID, IDBRelationID relationID)
      : base(nodeID.ObjectTypeID, nodeID.ObjectID)
    {
      if (nodeID == null)
        throw new ArgumentNullException(nameof (nodeID));
      if (relationID == null)
        throw new ArgumentNullException(nameof (relationID));
      this._nodeID = nodeID;
      this._relationID = relationID;
    }

    public override object GetData(INodeID nodeID, System.Type dataFormat)
    {
      if (this._nodeID == nodeID)
      {
        if (dataFormat == typeof (INodeID))
          return (object) this._nodeID;
        if (dataFormat == typeof (IDBRelationID))
          return (object) this._relationID;
      }
      return base.GetData(nodeID, dataFormat);
    }
  }
}
