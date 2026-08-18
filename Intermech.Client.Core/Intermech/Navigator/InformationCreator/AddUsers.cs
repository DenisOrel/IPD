
// Type: Intermech.Navigator.InformationCreator.AddUsers
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraTreeList;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace Intermech.Navigator.InformationCreator;

/// <summary>
/// форма позволяет добавить пользователей в мастере создания узал информационной системы
/// </summary>
public class AddUsers : Form
{
  /// <summary>для хранения идентификаторов выбора</summary>
  private List<long> usersID = new List<long>();
  /// <summary>для хранения заголовков выбора</summary>
  private List<string> users = new List<string>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TreeList tlUsers;
  private Label labelPrompt;
  private Button buttonOK;
  private Button buttonCancel;

  /// <summary>для хранения заголовков выбора</summary>
  public List<string> Users => this.users;

  /// <summary>для хранения идентификаторов выбора</summary>
  public List<long> UsersID => this.usersID;

  public AddUsers()
  {
    this.InitializeComponent();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int objectTypeId1 = MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545");
      int objectTypeId2 = MetaDataHelper.GetObjectTypeID(new Guid("cad0148c-306c-11d8-b4e9-00304f19f545"));
      int relationTypeId = MetaDataHelper.GetRelationTypeID(new Guid("cad00022-306c-11d8-b4e9-00304f19f545"));
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relationTypeId);
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(-2, RelationalOperators.NotEntersInType, (object) objectTypeId2, LogicalOperators.NONE, 0, false),
        new ConditionStructure(-2, RelationalOperators.ObjectTypeFilter, (object) objectTypeId1, LogicalOperators.NONE, 0, false)
      }, new object[2]{ (object) -2, (object) -50 });
      DataTable dataTable = new DataTable()
      {
        Columns = {
          {
            "ID",
            typeof (int)
          }
        }
      };
      DataTable table = relationCollection.Select(paramSet).DefaultView.ToTable(true);
      table.Columns.Add("ParentID", typeof (int), "-1");
      int index = Statics.IconSrv.IndexOf(4, objectTypeId1);
      this.Icon = Statics.IconSrv.GetIndexIcon(index);
      table.Columns.Add("Image", typeof (int), index.ToString());
      this.tlUsers.SelectImageList = Statics.IconSrv.ImageList;
      this.tlUsers.DataSource = (object) table;
      this.tlUsers.PopulateColumns();
      this.tlUsers.KeyFieldName = "ID";
      this.tlUsers.ParentFieldName = "ParentID";
      this.tlUsers.ImageIndexFieldName = "Image";
    }
  }

  private void buttonOK_Click(object sender, EventArgs e)
  {
    for (int index = this.tlUsers.Selection.Count - 1; index >= 0; --index)
    {
      this.usersID.Add((long) this.tlUsers.Selection[index].GetValue((object) 0));
      this.users.Add((string) this.tlUsers.Selection[index].GetValue((object) 1));
    }
    this.tlUsers.Selection.Set(this.tlUsers.Nodes[0]);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AddUsers));
    this.tlUsers = new TreeList();
    this.labelPrompt = new Label();
    this.buttonOK = new Button();
    this.buttonCancel = new Button();
    this.tlUsers.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tlUsers, "tlUsers");
    this.tlUsers.Name = "tlUsers";
    componentResourceManager.ApplyResources((object) this.labelPrompt, "labelPrompt");
    this.labelPrompt.Name = "labelPrompt";
    componentResourceManager.ApplyResources((object) this.buttonOK, "buttonOK");
    this.buttonOK.DialogResult = DialogResult.OK;
    this.buttonOK.Name = "buttonOK";
    this.buttonOK.UseVisualStyleBackColor = true;
    this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.buttonOK);
    this.Controls.Add((Control) this.labelPrompt);
    this.Controls.Add((Control) this.tlUsers);
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.Name = nameof (AddUsers);
    this.tlUsers.EndInit();
    this.ResumeLayout(false);
  }
}
