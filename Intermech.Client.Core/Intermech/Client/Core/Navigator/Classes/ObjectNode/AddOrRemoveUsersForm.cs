
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.AddOrRemoveUsersForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

public class AddOrRemoveUsersForm : Form
{
  protected static long _selObjectID;
  protected static List<long> _userList;
  protected static Dictionary<long, string> _UserDictionary;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ListView lvUserList;
  private Button btnAdd;
  private Button btnRemove;
  private Button btnApply;
  private Button btnCancel;

  public AddOrRemoveUsersForm()
  {
    this.InitializeComponent();
    this.lvUserList.View = View.Details;
    this.lvUserList.FullRowSelect = true;
    this.lvUserList.GridLines = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
  }

  public static Dictionary<long, string> Execute(List<MyElement> userList, long selObjId)
  {
    AddOrRemoveUsersForm._selObjectID = selObjId;
    if (AddOrRemoveUsersForm._UserDictionary == null)
      AddOrRemoveUsersForm._UserDictionary = new Dictionary<long, string>();
    if (AddOrRemoveUsersForm._UserDictionary.Count > 0)
      AddOrRemoveUsersForm._UserDictionary.Clear();
    foreach (MyElement user in userList)
      AddOrRemoveUsersForm._UserDictionary.Add((long) user.Value, user.Caption);
    using (AddOrRemoveUsersForm orRemoveUsersForm = new AddOrRemoveUsersForm())
    {
      orRemoveUsersForm.RefreshListViewer();
      return orRemoveUsersForm.ShowDialog() == DialogResult.OK ? AddOrRemoveUsersForm._UserDictionary : (Dictionary<long, string>) null;
    }
  }

  private void RefreshListViewer()
  {
    this.lvUserList.Columns.Add(LocalizationHolder.rm.GetString("Client.Core_1643"), 298);
    foreach (KeyValuePair<long, string> user in AddOrRemoveUsersForm._UserDictionary)
      this.lvUserList.Items.Add(new ListViewItem(user.Value));
  }

  private void btnAdd_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long selObjectId = AddOrRemoveUsersForm._selObjectID;
      SelectionOptions options = SelectionOptions.Default | SelectionOptions.ForceFilterObjectsByRule;
      Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new TypedObjectsSelectedItemsAnalyzer(MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545"), true), true);
      if (!(Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_1644"), LocalizationHolder.rm.GetString("Client.Core_1645"), (IDescriptor) new UsersGroupsDescriptor(), typeof (IDBTypedObjectID), options) is IDBTypedObjectID[] dbTypedObjectIdArray))
        return;
      this.lvUserList.BeginUpdate();
      for (int index = 0; index < dbTypedObjectIdArray.Length; ++index)
      {
        IDBTypedObjectID objectId = dbTypedObjectIdArray[index];
        int levelId = this.GetLCStep(sessionKeeper.Session, objectId).LevelID;
        string lcName = this.GetLCStep(sessionKeeper.Session, objectId).LCName;
        if (!AddOrRemoveUsersForm._UserDictionary.ContainsKey(objectId.ObjectID) && selObjectId != objectId.ObjectID)
        {
          if (levelId != sessionKeeper.Session.GetLifecycleStep(new Guid("cadd9504-306c-11d8-b4e9-00304f19f545")).LevelID)
          {
            AddOrRemoveUsersForm._UserDictionary.Add(objectId.ObjectID, objectId.Caption);
            this.lvUserList.Items.Add(objectId.Caption);
          }
          else
          {
            int num1 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_1647") + " {0} - {1}.", (object) objectId.Caption, (object) lcName), LocalizationHolder.rm.GetString("Client.Core_1650"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          }
        }
        else
        {
          int num2 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_1648") + " {0}", (object) objectId.Caption), LocalizationHolder.rm.GetString("Client.Core_1650"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
      }
      this.lvUserList.EndUpdate();
    }
  }

  protected IDBLifecycleStep GetLCStep(IUserSession session, IDBTypedObjectID objectId)
  {
    int lcStep = session.GetObject(objectId.ObjectID).LCStep;
    return session.GetLifecycleStep(lcStep);
  }

  private void btnRemove_Click(object sender, EventArgs e)
  {
    ListView.SelectedListViewItemCollection selectedItems = this.lvUserList.SelectedItems;
    this.lvUserList.BeginUpdate();
    foreach (ListViewItem listViewItem in selectedItems)
    {
      ListViewItem selextedItem = listViewItem;
      if (AddOrRemoveUsersForm._UserDictionary.ContainsValue(selextedItem.Text))
      {
        AddOrRemoveUsersForm._UserDictionary.Remove(AddOrRemoveUsersForm._UserDictionary.First<KeyValuePair<long, string>>((Func<KeyValuePair<long, string>, bool>) (q => q.Value == selextedItem.Text)).Key);
        this.lvUserList.Items[selextedItem.Index].Remove();
      }
    }
    this.lvUserList.EndUpdate();
  }

  private void AddOrRemoveUsersForm_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Escape)
      return;
    this.DialogResult = DialogResult.Cancel;
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
    this.lvUserList = new ListView();
    this.btnAdd = new Button();
    this.btnRemove = new Button();
    this.btnApply = new Button();
    this.btnCancel = new Button();
    this.SuspendLayout();
    this.lvUserList.Location = new Point(12, 12);
    this.lvUserList.Name = "lvUserList";
    this.lvUserList.Size = new Size(300, 238);
    this.lvUserList.TabIndex = 0;
    this.lvUserList.UseCompatibleStateImageBehavior = false;
    this.btnAdd.Location = new Point(324, 12);
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.Size = new Size(75, 23);
    this.btnAdd.TabIndex = 1;
    this.btnAdd.Text = "Добавить";
    this.btnAdd.UseVisualStyleBackColor = true;
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    this.btnRemove.Location = new Point(324, 41);
    this.btnRemove.Name = "btnRemove";
    this.btnRemove.Size = new Size(75, 23);
    this.btnRemove.TabIndex = 2;
    this.btnRemove.Text = "Удалить";
    this.btnRemove.UseVisualStyleBackColor = true;
    this.btnRemove.Click += new EventHandler(this.btnRemove_Click);
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.Location = new Point(324, 198);
    this.btnApply.Name = "btnApply";
    this.btnApply.Size = new Size(75, 23);
    this.btnApply.TabIndex = 3;
    this.btnApply.Text = "OK";
    this.btnApply.UseVisualStyleBackColor = true;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(324, 227);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 4;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(408, 262);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnApply);
    this.Controls.Add((Control) this.btnRemove);
    this.Controls.Add((Control) this.btnAdd);
    this.Controls.Add((Control) this.lvUserList);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.KeyPreview = true;
    this.Name = nameof (AddOrRemoveUsersForm);
    this.Text = "Редактирование списка \"Исполняет обязанности\"";
    this.KeyDown += new KeyEventHandler(this.AddOrRemoveUsersForm_KeyDown);
    this.ResumeLayout(false);
  }
}
