
// Type: Intermech.Search.UI.MessageList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.UI;

public sealed class MessageList : UserControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ListView _listView;
  private ColumnHeader _typeColumn;
  private ColumnHeader _textColumn;
  private Intermech.Bars.ToolBar _toolBar;
  private ButtonItem _clearButton;
  private ImageList _imageList;

  public MessageList()
  {
    this.InitializeComponent();
    this.Messages = new MessageCollection();
    this.Messages.ListChanged += new ListChangedEventHandler(this.Messages_ListChanged);
  }

  public event EventHandler SelectedMessageChanged;

  public event EventHandler<MessageList.MessageEventArgs> MessageDoubleClick;

  [Obsolete]
  public event EventHandler SelectedIndexChanged;

  public MessageCollection Messages { get; private set; }

  public _Message SelectedMessage
  {
    get
    {
      return this._listView.SelectedItems == null || this._listView.SelectedItems.Count <= 0 ? (_Message) null : (_Message) this._listView.SelectedItems[0].Tag;
    }
  }

  public int SelectedIndex
  {
    get
    {
      ListView.SelectedIndexCollection selectedIndices = this._listView.SelectedIndices;
      return selectedIndices.Count <= 0 ? -1 : selectedIndices[0];
    }
  }

  public List<int> SelectedIndexes => this._listView.SelectedIndices.Cast<int>().ToList<int>();

  private void Messages_ListChanged(object sender, ListChangedEventArgs e)
  {
    this._listView.BeginUpdate();
    try
    {
      if (e.ListChangedType == ListChangedType.ItemAdded)
      {
        ListViewItem listViewItem = this.CreateListViewItem(this.Messages[e.NewIndex]);
        this._listView.Items.Insert(e.NewIndex, listViewItem);
      }
      else if (e.ListChangedType == ListChangedType.ItemDeleted)
        this._listView.Items.RemoveAt(e.NewIndex);
      else if (e.ListChangedType == ListChangedType.ItemMoved)
      {
        ListViewItem listViewItem = this._listView.Items[e.OldIndex];
        this._listView.Items.RemoveAt(e.OldIndex);
        this._listView.Items.Insert(e.NewIndex, listViewItem);
      }
      else if (e.ListChangedType == ListChangedType.Reset)
        this._listView.Items.Clear();
    }
    finally
    {
      this._listView.EndUpdate();
    }
    this._clearButton.Enabled = this.Messages.Count > 0;
  }

  private void ClearButton_Click(object sender, EventArgs e) => this.Messages.Clear();

  private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    ListViewItem itemAt = this._listView.GetItemAt(e.X, e.Y);
    if (itemAt == null || !(itemAt.Tag is _Message))
      return;
    this.OnMessageDoubleClick((_Message) itemAt.Tag);
  }

  private void ListView_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.OnSelectedIndexChanged();
    this.OnSelectedMessageChanged();
  }

  private void OnSelectedIndexChanged()
  {
    EventHandler selectedIndexChanged = this.SelectedIndexChanged;
    if (selectedIndexChanged == null)
      return;
    selectedIndexChanged((object) this, new EventArgs());
  }

  private void OnSelectedMessageChanged()
  {
    EventHandler selectedMessageChanged = this.SelectedMessageChanged;
    if (selectedMessageChanged == null)
      return;
    selectedMessageChanged((object) this, new EventArgs());
  }

  private void OnMessageDoubleClick(_Message message)
  {
    EventHandler<MessageList.MessageEventArgs> messageDoubleClick = this.MessageDoubleClick;
    if (messageDoubleClick == null)
      return;
    messageDoubleClick((object) this, new MessageList.MessageEventArgs(message));
  }

  private ListViewItem CreateListViewItem(_Message message)
  {
    return new ListViewItem(new string[2]
    {
      message.Type.GetDescription<_MessageType>(),
      message.Text
    }, this.GetIconName4MessageType(message.Type))
    {
      Tag = (object) message
    };
  }

  private string GetIconName4MessageType(_MessageType type) => type.ToString();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MessageList));
    this._listView = new ListView();
    this._typeColumn = new ColumnHeader();
    this._textColumn = new ColumnHeader();
    this._imageList = new ImageList(this.components);
    this._toolBar = new Intermech.Bars.ToolBar();
    this._clearButton = new ButtonItem();
    this.SuspendLayout();
    this._listView.Columns.AddRange(new ColumnHeader[2]
    {
      this._typeColumn,
      this._textColumn
    });
    componentResourceManager.ApplyResources((object) this._listView, "_listView");
    this._listView.FullRowSelect = true;
    this._listView.GridLines = true;
    this._listView.Groups.AddRange(new ListViewGroup[2]
    {
      (ListViewGroup) componentResourceManager.GetObject("_listView.Groups"),
      (ListViewGroup) componentResourceManager.GetObject("_listView.Groups1")
    });
    this._listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this._listView.HideSelection = false;
    this._listView.Name = "_listView";
    this._listView.ShowGroups = false;
    this._listView.ShowItemToolTips = true;
    this._listView.SmallImageList = this._imageList;
    this._listView.UseCompatibleStateImageBehavior = false;
    this._listView.View = View.Details;
    this._listView.SelectedIndexChanged += new EventHandler(this.ListView_SelectedIndexChanged);
    this._listView.MouseDoubleClick += new MouseEventHandler(this.ListView_MouseDoubleClick);
    componentResourceManager.ApplyResources((object) this._typeColumn, "_typeColumn");
    componentResourceManager.ApplyResources((object) this._textColumn, "_textColumn");
    this._imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imageList.ImageStream");
    this._imageList.TransparentColor = Color.Transparent;
    this._imageList.Images.SetKeyName(0, "Error");
    this._imageList.Images.SetKeyName(1, "Information");
    this._imageList.Images.SetKeyName(2, "Success");
    this._imageList.Images.SetKeyName(3, "Warning");
    this._imageList.Images.SetKeyName(4, "очистить.png");
    this._toolBar.FullMenus = true;
    this._toolBar.Guid = new Guid("b8001dcc-993e-407a-a160-a9630cb5f109");
    this._toolBar.Hidden = false;
    this._toolBar.ImageList = this._imageList;
    this._toolBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this._clearButton
    });
    componentResourceManager.ApplyResources((object) this._toolBar, "_toolBar");
    this._toolBar.Name = "_toolBar";
    componentResourceManager.ApplyResources((object) this._clearButton, "_clearButton");
    this._clearButton.Enabled = false;
    this._clearButton.ImageIndex = 4;
    this._clearButton.Click += new EventHandler(this.ClearButton_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._listView);
    this.Controls.Add((Control) this._toolBar);
    this.Name = nameof (MessageList);
    this.ResumeLayout(false);
  }

  public sealed class MessageEventArgs : EventArgs
  {
    public MessageEventArgs(_Message message)
    {
      this.Message = message != null ? message : throw new ArgumentNullException(nameof (message));
    }

    public _Message Message { get; private set; }
  }
}
