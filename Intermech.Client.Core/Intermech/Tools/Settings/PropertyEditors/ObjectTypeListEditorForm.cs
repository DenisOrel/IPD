
// Type: Intermech.Tools.Settings.PropertyEditors.ObjectTypeListEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Tools.Settings.PropertyEditors;

public sealed class ObjectTypeListEditorForm : Form
{
  private List<GlobalId<int>> objTypes;
  private int selectorFormRootType = -1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btOK;
  private Button btCancel;
  private Label lbObjectTypes;
  private ListView lvObjectTypes;
  private Button btAdd;
  private Button btRemove;
  private ColumnHeader chObjectType;

  public ObjectTypeListEditorForm()
  {
    this.InitializeComponent();
    this.SelectorFormRootType = -1;
  }

  private void ObjectTypeListEditorForm_Shown(object sender, EventArgs e)
  {
    this.lvObjectTypes.BeginUpdate();
    try
    {
      this.lvObjectTypes.Items.Clear();
      this.lvObjectTypes.SmallImageList = ServiceUtils.GetService<ICategoryTypeIconService>((object) ServicesManager.ServiceContainer, true).ImageList;
      for (int index = 0; index < this.objTypes.Count; ++index)
        this.lvObjectTypes.Items.Add(this.MakeListItem(this.objTypes[index]));
      if (this.lvObjectTypes.Items.Count <= 0)
        return;
      this.lvObjectTypes.Items[0].Selected = true;
    }
    finally
    {
      this.lvObjectTypes.EndUpdate();
    }
  }

  private ListViewItem MakeListItem(GlobalId<int> objType)
  {
    return new ListViewItem(objType.Name)
    {
      Tag = (object) objType,
      ImageIndex = ServiceUtils.GetService<ICategoryTypeIconService>((object) ServicesManager.ServiceContainer, true).IndexOf(4, objType.Id)
    };
  }

  private void lvObjectTypes_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.btRemove.Enabled = this.lvObjectTypes.SelectedIndices.Count > 0;
  }

  private void btOK_Click(object sender, EventArgs e)
  {
    List<GlobalId<int>> collection = new List<GlobalId<int>>(this.lvObjectTypes.Items.Count);
    for (int index = 0; index < this.lvObjectTypes.Items.Count; ++index)
      collection.Add((GlobalId<int>) this.lvObjectTypes.Items[index].Tag);
    this.objTypes.Clear();
    this.objTypes.AddRange((IEnumerable<GlobalId<int>>) collection);
  }

  private void btAdd_Click(object sender, EventArgs e)
  {
    using (SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_1608"), typeof (ObjectTypeFolder), false))
    {
      if (this.selectorFormRootType != -1)
        selectorForm.SelectorFilter = (ISelectorFilter) new ObjectTypeListEditorForm.ObjectTypesFilter(this.selectorFormRootType);
      if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count != 1)
        return;
      GlobalId<int> objectType = this.ConvertToObjectType((int) selectorForm.IDList[0]);
      AppObjectTypeEventArgs e1 = new AppObjectTypeEventArgs(objectType, true);
      if (this.AddObject != null)
        this.AddObject((object) this, e1);
      if (!e1.CanAdd)
        return;
      ListViewItem listItem = this.FindListItem(objectType);
      if (listItem != null)
      {
        listItem.Selected = true;
        this.lvObjectTypes.Focus();
      }
      else
      {
        ListViewItem listViewItem = this.MakeListItem(objectType);
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
  }

  private GlobalId<int> ConvertToObjectType(int objTypeId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(objTypeId, true);
      return new GlobalId<int>(((IDBGuid) objectType).GUID, objTypeId, objectType.ObjectTypeName);
    }
  }

  private ListViewItem FindListItem(GlobalId<int> objType)
  {
    for (int index = 0; index < this.lvObjectTypes.Items.Count; ++index)
    {
      if (this.lvObjectTypes.Items[index].Tag.Equals((object) objType))
        return this.lvObjectTypes.Items[index];
    }
    return (ListViewItem) null;
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

  public List<GlobalId<int>> ObjectTypes
  {
    get => this.objTypes;
    set => this.objTypes = value;
  }

  public int SelectorFormRootType
  {
    set => this.selectorFormRootType = value;
  }

  public event EventHandler<AppObjectTypeEventArgs> AddObject;

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
    this.lbObjectTypes = new Label();
    this.lvObjectTypes = new ListView();
    this.chObjectType = new ColumnHeader();
    this.btAdd = new Button();
    this.btRemove = new Button();
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
    this.lbObjectTypes.AutoSize = true;
    this.lbObjectTypes.Location = new Point(9, 9);
    this.lbObjectTypes.Name = "lbObjectTypes";
    this.lbObjectTypes.Size = new Size(145, 13);
    this.lbObjectTypes.TabIndex = 0;
    this.lbObjectTypes.Text = "Выбранные типы объектов";
    this.lvObjectTypes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lvObjectTypes.Columns.AddRange(new ColumnHeader[1]
    {
      this.chObjectType
    });
    this.lvObjectTypes.FullRowSelect = true;
    this.lvObjectTypes.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lvObjectTypes.HideSelection = false;
    this.lvObjectTypes.Location = new Point(12, 25);
    this.lvObjectTypes.MultiSelect = false;
    this.lvObjectTypes.Name = "lvObjectTypes";
    this.lvObjectTypes.Size = new Size(439, 202);
    this.lvObjectTypes.TabIndex = 1;
    this.lvObjectTypes.UseCompatibleStateImageBehavior = false;
    this.lvObjectTypes.View = View.Details;
    this.lvObjectTypes.SelectedIndexChanged += new EventHandler(this.lvObjectTypes_SelectedIndexChanged);
    this.chObjectType.Text = "Тип объекта";
    this.chObjectType.Width = 407;
    this.btAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btAdd.Location = new Point(457, 25);
    this.btAdd.Name = "btAdd";
    this.btAdd.Size = new Size(75, 23);
    this.btAdd.TabIndex = 2;
    this.btAdd.Text = "Добавить";
    this.btAdd.UseVisualStyleBackColor = true;
    this.btAdd.Click += new EventHandler(this.btAdd_Click);
    this.btRemove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btRemove.Enabled = false;
    this.btRemove.Location = new Point(457, 54);
    this.btRemove.Name = "btRemove";
    this.btRemove.Size = new Size(75, 23);
    this.btRemove.TabIndex = 3;
    this.btRemove.Text = "Удалить";
    this.btRemove.UseVisualStyleBackColor = true;
    this.btRemove.Click += new EventHandler(this.btRemove_Click);
    this.AcceptButton = (IButtonControl) this.btCancel;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.ClientSize = new Size(544, 268);
    this.Controls.Add((Control) this.btRemove);
    this.Controls.Add((Control) this.btAdd);
    this.Controls.Add((Control) this.lvObjectTypes);
    this.Controls.Add((Control) this.lbObjectTypes);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(550, 300);
    this.Name = nameof (ObjectTypeListEditorForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Список типов объектов";
    this.Shown += new EventHandler(this.ObjectTypeListEditorForm_Shown);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private sealed class ObjectTypesFilter : ISelectorFilter
  {
    private List<int> _enableTypes;

    public ObjectTypesFilter(int rootTypeID)
    {
      this._enableTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(rootTypeID);
    }

    public bool IsInFilter(int category, object id)
    {
      return category == 4 && this._enableTypes.IndexOf((int) id) >= 0;
    }
  }
}
