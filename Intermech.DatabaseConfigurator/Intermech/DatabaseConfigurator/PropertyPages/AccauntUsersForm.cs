// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.AccauntUsersForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

public class AccauntUsersForm : Form
{
  public bool Changed;
  private IContainer components;
  private Panel panel1;
  private ListView listView1;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private Button bClose;
  private Button bDelete;
  private Button bAdd;
  private CheckBox checkBox1;
  private ImageList imageList1;
  private Panel panel3;

  public List<AccauntUserInfo> Users
  {
    get
    {
      List<AccauntUserInfo> users = new List<AccauntUserInfo>();
      for (int index = 0; index < this.listView1.Items.Count; ++index)
        users.Add((AccauntUserInfo) this.listView1.Items[index].Tag);
      return users;
    }
  }

  public AccauntUsersForm(List<AccauntUserInfo> users)
  {
    this.InitializeComponent();
    if (users == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < users.Count; ++index)
        this.AddUserItem(sessionKeeper.Session, users[index]);
    }
  }

  private void AddUserItem(IUserSession session, AccauntUserInfo userInfo)
  {
    IDBObject dbObject = session.GetObject(userInfo.UserID, false);
    if (dbObject == null)
      return;
    IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0001d-306c-11d8-b4e9-00304f19f545"));
    this.listView1.Items.Add(new ListViewItem(attributeByGuid != null ? attributeByGuid.AsString : string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_223"), (object) userInfo.UserID))
    {
      Tag = (object) userInfo,
      ImageIndex = 0
    });
  }

  private void bClose_Click(object sender, EventArgs e) => this.Close();

  private void listView1_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.listView1.FocusedItem == null || this.listView1.FocusedItem.Tag == null)
      return;
    AccauntUserInfo tag = (AccauntUserInfo) this.listView1.FocusedItem.Tag;
    this.checkBox1.CheckedChanged -= new EventHandler(this.checkBox1_CheckedChanged);
    try
    {
      this.checkBox1.Checked = tag.Owner;
    }
    finally
    {
      this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
    }
  }

  private void checkBox1_CheckedChanged(object sender, EventArgs e)
  {
    if (this.listView1.FocusedItem == null || this.listView1.FocusedItem.Tag == null)
      return;
    ((AccauntUserInfo) this.listView1.FocusedItem.Tag).Owner = this.checkBox1.Checked;
    this.Changed = true;
  }

  private void AddUserToList(IUserSession session, long userID)
  {
    if (this.UserPresent(userID))
      return;
    this.AddUserItem(session, new AccauntUserInfo()
    {
      UserID = userID,
      Owner = this.listView1.Items.Count == 0
    });
  }

  private void bAdd_Click(object sender, EventArgs e)
  {
    IDescriptor rootDescriptor = (IDescriptor) new UsersGroupsDescriptor();
    if (!(SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_1129"), rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.SelectObjects) is IDBTypedObjectID[] dbTypedObjectIdArray))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(sessionKeeper.Session.IdentHelper.SimpleRelationTypeID);
      for (int index = 0; index < dbTypedObjectIdArray.Length; ++index)
      {
        IDBTypedObjectID dbTypedObjectId = dbTypedObjectIdArray[index];
        if (dbTypedObjectId.ObjectType == sessionKeeper.Session.IdentHelper.GroupsTypeID)
        {
          List<long> longList = relationCollection.QuickConsistFrom(new long[1]
          {
            dbTypedObjectId.ObjectID
          }, new List<int>((IEnumerable<int>) new int[1]
          {
            sessionKeeper.Session.IdentHelper.UsersTypeID
          }));
          if (longList.Count != 0)
          {
            foreach (long userID in longList)
              this.AddUserToList(sessionKeeper.Session, userID);
          }
        }
        else
          this.AddUserToList(sessionKeeper.Session, dbTypedObjectId.ObjectID);
      }
    }
    if (this.listView1.Items.Count <= 0)
      return;
    this.listView1.Items[this.listView1.Items.Count - 1].Focused = true;
    this.listView1.Items[this.listView1.Items.Count - 1].Selected = true;
    this.Changed = true;
  }

  private bool UserPresent(long userId)
  {
    for (int index = 0; index < this.listView1.Items.Count; ++index)
    {
      if (((AccauntUserInfo) this.listView1.Items[index].Tag).UserID == userId)
        return true;
    }
    return false;
  }

  private void bDelete_Click(object sender, EventArgs e)
  {
    if (this.listView1.FocusedItem == null || this.listView1.FocusedItem.Tag == null)
      return;
    this.listView1.FocusedItem.Remove();
    if (this.listView1.Items.Count == 0)
    {
      this.checkBox1.Checked = false;
    }
    else
    {
      this.listView1.Items[0].Focused = true;
      this.listView1.Items[0].Selected = true;
    }
    this.Changed = true;
  }

  private void AccauntUsersForm_Shown(object sender, EventArgs e)
  {
    if (this.listView1.Items.Count <= 0)
      return;
    this.listView1.Items[0].Focused = true;
    this.listView1.Items[0].Selected = true;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AccauntUsersForm));
    this.panel1 = new Panel();
    this.bClose = new Button();
    this.bDelete = new Button();
    this.bAdd = new Button();
    this.checkBox1 = new CheckBox();
    this.listView1 = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.imageList1 = new ImageList();
    this.panel3 = new Panel();
    this.panel1.SuspendLayout();
    this.panel3.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((Control) this.bClose);
    this.panel1.Controls.Add((Control) this.bDelete);
    this.panel1.Controls.Add((Control) this.bAdd);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.bClose, "bClose");
    this.bClose.DialogResult = DialogResult.Cancel;
    this.bClose.Name = "bClose";
    this.bClose.UseVisualStyleBackColor = true;
    this.bClose.Click += new EventHandler(this.bClose_Click);
    componentResourceManager.ApplyResources((object) this.bDelete, "bDelete");
    this.bDelete.Name = "bDelete";
    this.bDelete.UseVisualStyleBackColor = true;
    this.bDelete.Click += new EventHandler(this.bDelete_Click);
    componentResourceManager.ApplyResources((object) this.bAdd, "bAdd");
    this.bAdd.Name = "bAdd";
    this.bAdd.UseVisualStyleBackColor = true;
    this.bAdd.Click += new EventHandler(this.bAdd_Click);
    componentResourceManager.ApplyResources((object) this.checkBox1, "checkBox1");
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.UseVisualStyleBackColor = true;
    this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.listView1, "listView1");
    this.listView1.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader1,
      this.columnHeader2
    });
    this.listView1.GridLines = true;
    this.listView1.MultiSelect = false;
    this.listView1.Name = "listView1";
    this.listView1.SmallImageList = this.imageList1;
    this.listView1.UseCompatibleStateImageBehavior = false;
    this.listView1.View = View.List;
    this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "user1.png");
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Controls.Add((Control) this.checkBox1);
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bClose;
    this.Controls.Add((Control) this.listView1);
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.panel1);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (AccauntUsersForm);
    this.Shown += new EventHandler(this.AccauntUsersForm_Shown);
    this.panel1.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.panel3.PerformLayout();
    this.ResumeLayout(false);
  }
}
