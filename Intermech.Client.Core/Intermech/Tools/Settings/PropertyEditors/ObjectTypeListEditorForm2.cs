
// Type: Intermech.Tools.Settings.PropertyEditors.ObjectTypeListEditorForm2
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Tools.Settings.PropertyEditors;

public sealed class ObjectTypeListEditorForm2 : Form
{
  private IObjectTypeListAdapter listAdapter;
  private IList list;
  private ICategoryTypeIconService iconService;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btOK;
  private Button btCancel;
  private ListView lvObjectTypes;
  private Button btAdd;
  private Button btRemove;
  private ColumnHeader chObjectType;
  private Button btProperties;

  public ObjectTypeListEditorForm2() => this.InitializeComponent();

  private void ObjectTypeListEditorForm_Shown(object sender, EventArgs e)
  {
    this.lvObjectTypes.BeginUpdate();
    try
    {
      this.lvObjectTypes.Items.Clear();
      this.btOK.Enabled = false;
      this.btAdd.Enabled = false;
      this.btRemove.Enabled = false;
      this.btProperties.Visible = false;
      this.btProperties.Enabled = false;
      if (this.listAdapter == null || this.list == null)
        return;
      this.btOK.Enabled = true;
      this.btAdd.Enabled = true;
      this.btProperties.Visible = this.EditItem != null;
      this.lvObjectTypes.SmallImageList = this.IconService.ImageList;
      foreach (object listItem in (IEnumerable) this.list)
        this.lvObjectTypes.Items.Add(this.MakeListItem(listItem));
      if (this.lvObjectTypes.Items.Count <= 0)
        return;
      this.lvObjectTypes.Items[0].Selected = true;
    }
    finally
    {
      this.lvObjectTypes.EndUpdate();
    }
  }

  private void lvObjectTypes_SelectedIndexChanged(object sender, EventArgs e)
  {
    bool flag = this.lvObjectTypes.SelectedIndices.Count > 0;
    this.btRemove.Enabled = flag;
    this.btProperties.Enabled = flag;
  }

  private void btOK_Click(object sender, EventArgs e)
  {
    this.list.Clear();
    foreach (ListViewItem listViewItem in this.lvObjectTypes.Items)
      this.list.Add(listViewItem.Tag);
  }

  private void btAdd_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_1608"), typeof (ObjectTypeFolder), false);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count != 1)
      return;
    int id = (int) selectorForm.IDList[0];
    GlobalId<int> globalId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(id, true);
      globalId = new GlobalId<int>(((IDBGuid) objectType).GUID, id, objectType.ObjectTypeName);
    }
    ListViewItem listItem = this.FindListItem(id);
    if (listItem != null)
    {
      listItem.Selected = true;
      this.lvObjectTypes.Focus();
    }
    else
    {
      ListViewItem listViewItem = this.MakeListItem(this.listAdapter.Create(globalId.Guid, globalId.Id, globalId.ToString()));
      this.lvObjectTypes.BeginUpdate();
      try
      {
        this.lvObjectTypes.Items.Add(listViewItem);
        listViewItem.Selected = true;
        this.lvObjectTypes.Focus();
      }
      finally
      {
        this.lvObjectTypes.EndUpdate();
      }
    }
  }

  private void btRemove_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.lvObjectTypes.SelectedIndices[0];
    this.lvObjectTypes.BeginUpdate();
    try
    {
      this.lvObjectTypes.Items.RemoveAt(selectedIndex);
      if (this.lvObjectTypes.Items.Count > 0)
      {
        if (selectedIndex > 0)
          --selectedIndex;
        this.lvObjectTypes.Items[selectedIndex].Selected = true;
      }
      this.lvObjectTypes.Focus();
    }
    finally
    {
      this.lvObjectTypes.EndUpdate();
    }
  }

  private void btProperties_Click(object sender, EventArgs e)
  {
    ListViewItem selectedItem = this.lvObjectTypes.SelectedItems[0];
    ObjectTypeListEditorForm2.ChangeItemEventArgs e1 = new ObjectTypeListEditorForm2.ChangeItemEventArgs(selectedItem.Tag);
    this.EditItem((object) this, e1);
    this.lvObjectTypes.BeginUpdate();
    try
    {
      if (e1.ChangedItem == null)
        return;
      ListViewItem listViewItem = this.MakeListItem(e1.ChangedItem);
      this.lvObjectTypes.Items.Insert(selectedItem.Index, listViewItem);
      listViewItem.Selected = true;
      this.lvObjectTypes.Items.Remove(selectedItem);
    }
    finally
    {
      this.lvObjectTypes.EndUpdate();
      this.lvObjectTypes.Focus();
    }
  }

  private void lvObjectTypes_DoubleClick(object sender, EventArgs e)
  {
    if (!this.btProperties.Enabled)
      return;
    this.btProperties.PerformClick();
  }

  private ListViewItem MakeListItem(object listItem)
  {
    return new ListViewItem(this.listAdapter.GetObjectTypeName(listItem))
    {
      ImageIndex = this.IconService.IndexOf(4, this.listAdapter.GetObjectTypeId(listItem)),
      Tag = listItem
    };
  }

  private ListViewItem FindListItem(int objectTypeId)
  {
    foreach (ListViewItem listItem in this.lvObjectTypes.Items)
    {
      if (this.listAdapter.GetObjectTypeId(listItem.Tag) == objectTypeId)
        return listItem;
    }
    return (ListViewItem) null;
  }

  /// <summary>
  /// Возвращает или задает редактируемый список типов объектов.
  /// </summary>
  public IList List
  {
    get => this.list;
    set => this.list = value;
  }

  /// <summary>
  /// Возвращает или задает объект адаптера для списка типов объектов.
  /// </summary>
  public IObjectTypeListAdapter ListAdapter
  {
    get => this.listAdapter;
    set => this.listAdapter = value;
  }

  private ICategoryTypeIconService IconService
  {
    get
    {
      if (this.iconService == null)
        this.iconService = ServiceUtils.GetService<ICategoryTypeIconService>((object) ServicesManager.ServiceContainer, true);
      return this.iconService;
    }
  }

  public event EventHandler<ObjectTypeListEditorForm2.ChangeItemEventArgs> EditItem;

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
    this.btOK = new Button();
    this.btCancel = new Button();
    this.lvObjectTypes = new ListView();
    this.chObjectType = new ColumnHeader();
    this.btAdd = new Button();
    this.btRemove = new Button();
    this.btProperties = new Button();
    this.SuspendLayout();
    this.btOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Location = new Point(376, 233);
    this.btOK.Name = "btOK";
    this.btOK.Size = new Size(75, 23);
    this.btOK.TabIndex = 4;
    this.btOK.Text = "OK";
    this.btOK.UseVisualStyleBackColor = true;
    this.btOK.Click += new EventHandler(this.btOK_Click);
    this.btCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Location = new Point(457, 233);
    this.btCancel.Name = "btCancel";
    this.btCancel.Size = new Size(75, 23);
    this.btCancel.TabIndex = 5;
    this.btCancel.Text = "Отмена";
    this.btCancel.UseVisualStyleBackColor = true;
    this.lvObjectTypes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lvObjectTypes.Columns.AddRange(new ColumnHeader[1]
    {
      this.chObjectType
    });
    this.lvObjectTypes.FullRowSelect = true;
    this.lvObjectTypes.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lvObjectTypes.HideSelection = false;
    this.lvObjectTypes.Location = new Point(12, 12);
    this.lvObjectTypes.MultiSelect = false;
    this.lvObjectTypes.Name = "lvObjectTypes";
    this.lvObjectTypes.Size = new Size(439, 215);
    this.lvObjectTypes.TabIndex = 0;
    this.lvObjectTypes.UseCompatibleStateImageBehavior = false;
    this.lvObjectTypes.View = View.Details;
    this.lvObjectTypes.SelectedIndexChanged += new EventHandler(this.lvObjectTypes_SelectedIndexChanged);
    this.lvObjectTypes.DoubleClick += new EventHandler(this.lvObjectTypes_DoubleClick);
    this.chObjectType.Text = "Наименование типа объектов";
    this.chObjectType.Width = 407;
    this.btAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btAdd.Location = new Point(457, 12);
    this.btAdd.Name = "btAdd";
    this.btAdd.Size = new Size(75, 23);
    this.btAdd.TabIndex = 1;
    this.btAdd.Text = "Добавить";
    this.btAdd.UseVisualStyleBackColor = true;
    this.btAdd.Click += new EventHandler(this.btAdd_Click);
    this.btRemove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btRemove.Enabled = false;
    this.btRemove.Location = new Point(457, 41);
    this.btRemove.Name = "btRemove";
    this.btRemove.Size = new Size(75, 23);
    this.btRemove.TabIndex = 2;
    this.btRemove.Text = "Удалить";
    this.btRemove.UseVisualStyleBackColor = true;
    this.btRemove.Click += new EventHandler(this.btRemove_Click);
    this.btProperties.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btProperties.Location = new Point(457, 70);
    this.btProperties.Name = "btProperties";
    this.btProperties.Size = new Size(75, 23);
    this.btProperties.TabIndex = 3;
    this.btProperties.Text = "Свойства";
    this.btProperties.UseVisualStyleBackColor = true;
    this.btProperties.Click += new EventHandler(this.btProperties_Click);
    this.AcceptButton = (IButtonControl) this.btCancel;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.ClientSize = new Size(544, 272);
    this.Controls.Add((Control) this.btProperties);
    this.Controls.Add((Control) this.btRemove);
    this.Controls.Add((Control) this.btAdd);
    this.Controls.Add((Control) this.lvObjectTypes);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(550, 300);
    this.Name = nameof (ObjectTypeListEditorForm2);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Список типов объектов";
    this.Shown += new EventHandler(this.ObjectTypeListEditorForm_Shown);
    this.ResumeLayout(false);
  }

  public class ItemEventArgs : EventArgs
  {
    private readonly object listItem;

    public ItemEventArgs(object listItem)
    {
      this.listItem = listItem != null ? listItem : throw new ArgumentNullException();
    }

    public object ListItem => this.listItem;
  }

  public class ChangeItemEventArgs(object listItem) : ObjectTypeListEditorForm2.ItemEventArgs(listItem)
  {
    private object changedItem;

    public object ChangedItem
    {
      get => this.changedItem;
      set => this.changedItem = value;
    }
  }
}
