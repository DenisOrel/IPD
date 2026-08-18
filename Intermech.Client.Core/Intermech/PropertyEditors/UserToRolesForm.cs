
// Type: Intermech.PropertyEditors.UserToRolesForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for UserToRolesForm.</summary>
public class UserToRolesForm : Form
{
  /// <summary>Были ли изменения в правиле отбора</summary>
  public bool IsChanged;
  /// <summary>ID выделенного объекта</summary>
  public long ObjectID = -1;
  /// <summary>Название выделенного объекта</summary>
  public string ObjectName = "";
  private bool _editorMode;
  private Panel panel1;
  private Panel panel2;
  private Button bAdd;
  private Button bRemove;
  private TreeList tlRoles;
  private Panel panel3;
  private Button btnCancel;
  private Button btnApply;
  private Panel panel4;
  private TreeListColumn treeListColumn1;
  private IContainer components;
  private ArrayList _roles = new ArrayList();
  private byte[] _icon;
  private Icon _roleIcon;

  public UserToRolesForm() => this.InitializeComponent();

  public byte[] IconAsByteArray
  {
    set
    {
      if (this._icon == value)
        return;
      this._icon = value;
      if (this._icon == null || this._icon.Length == 0)
        return;
      using (MemoryStream memoryStream = new MemoryStream(this._icon))
        this._roleIcon = new Icon((Stream) memoryStream);
    }
  }

  public void SetParent(Control aParent)
  {
    if (aParent == null)
    {
      this.AutoScaleMode = AutoScaleMode.None;
      this.TopLevel = true;
      this.Dock = DockStyle.None;
      this.FormBorderStyle = FormBorderStyle.Sizable;
      this.Visible = false;
    }
    else
    {
      this.AutoScaleMode = AutoScaleMode.None;
      this.TopLevel = false;
      this.Dock = DockStyle.Fill;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Visible = true;
    }
    this.Parent = aParent;
  }

  private void UpdateControls()
  {
    this.tlRoles.Nodes.Clear();
    new ImageList().Images.Add(this._roleIcon);
    ICategoryTypeIconService service = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    int num = service.IndexOf(4, MetaDataHelper.GetObjectTypeID("cad00007-306c-11d8-b4e9-00304f19f545"));
    this.tlRoles.SelectImageList = service.ImageList;
    if (this._roles.Count > 0)
    {
      for (int index = 0; index < this._roles.Count; ++index)
      {
        if (((UserToRoles) this._roles[index]).Status != 2)
          this.tlRoles.AppendNode((object) new object[1]
          {
            (object) ((UserToRoles) this._roles[index]).ParentCaption
          }, -1, num, num, 0).Tag = (object) (UserToRoles) this._roles[index];
      }
    }
    if (this._editorMode)
      return;
    this.bAdd.Enabled = false;
    this.bRemove.Enabled = false;
  }

  private void UpdateButtons()
  {
    this.bRemove.Enabled = this.tlRoles.FocusedNode != null && this._roles.Count > 0;
    this.btnApply.Enabled = this.IsChanged && this._roles.Count > 0;
    this.btnCancel.Enabled = this.IsChanged && this._roles.Count > 0;
  }

  public void LoadObjectData(ArrayList roles, bool IsAdmin)
  {
    this._editorMode = IsAdmin;
    this._roles = roles;
    this.UpdateControls();
    this.UpdateButtons();
  }

  /// <summary>Извлечь данные из формы в указанный объект</summary>
  public void SaveObjectData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationType relationType = sessionKeeper.Session.GetRelationType(new Guid("cad00022-306c-11d8-b4e9-00304f19f545"));
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relationType.RelationType);
      bool flag = true;
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList();
      for (int index = 0; index < this._roles.Count; ++index)
      {
        if (((UserToRoles) this._roles[index]).Status == 1)
        {
          IDBRelation dbRelation = relationCollection.Create(((UserToRoles) this._roles[index]).ParentID, this.ObjectID);
          if (dbRelation != null)
          {
            ((UserToRoles) this._roles[index]).RelationID = dbRelation.RelationID;
            ((UserToRoles) this._roles[index]).Status = 0;
          }
          else
            flag = false;
        }
        if (((UserToRoles) this._roles[index]).Status == 2)
        {
          arrayList2.Add((object) (UserToRoles) this._roles[index]);
          arrayList1.Add((object) ((UserToRoles) this._roles[index]).RelationID);
        }
      }
      if (arrayList1.Count > 0)
      {
        relationCollection.Delete((long[]) arrayList1.ToArray(typeof (long)), true, 0L);
        foreach (UserToRoles userToRoles in arrayList2)
          this._roles.Remove((object) userToRoles);
      }
      if (!flag)
        return;
      this.IsChanged = false;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserToRolesForm));
    this.panel1 = new Panel();
    this.bRemove = new Button();
    this.bAdd = new Button();
    this.panel2 = new Panel();
    this.tlRoles = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.panel3 = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.panel4 = new Panel();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.tlRoles.BeginInit();
    this.panel3.SuspendLayout();
    this.panel4.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.bRemove);
    this.panel1.Controls.Add((Control) this.bAdd);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.panel1.Paint += new PaintEventHandler(this.panel1_Paint);
    componentResourceManager.ApplyResources((object) this.bRemove, "bRemove");
    this.bRemove.Name = "bRemove";
    this.bRemove.Click += new EventHandler(this.bRemove_Click);
    componentResourceManager.ApplyResources((object) this.bAdd, "bAdd");
    this.bAdd.Name = "bAdd";
    this.bAdd.Click += new EventHandler(this.bAdd_Click);
    this.panel2.Controls.Add((Control) this.tlRoles);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.tlRoles, "tlRoles");
    this.tlRoles.Columns.AddRange(new TreeListColumn[1]
    {
      this.treeListColumn1
    });
    this.tlRoles.Name = "tlRoles";
    this.tlRoles.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.tlRoles_FocusedNodeChanged);
    componentResourceManager.ApplyResources((object) this.treeListColumn1, "treeListColumn1");
    this.treeListColumn1.Name = "treeListColumn1";
    this.panel3.Controls.Add((Control) this.btnCancel);
    this.panel3.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Hand;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Cursor = Cursors.Hand;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.panel4.Controls.Add((Control) this.panel2);
    this.panel4.Controls.Add((Control) this.panel1);
    componentResourceManager.ApplyResources((object) this.panel4, "panel4");
    this.panel4.Name = "panel4";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panel4);
    this.Controls.Add((Control) this.panel3);
    this.Name = nameof (UserToRolesForm);
    this.ShowInTaskbar = false;
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.tlRoles.EndInit();
    this.panel3.ResumeLayout(false);
    this.panel4.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void bRemove_Click(object sender, EventArgs e)
  {
    TreeListMultiSelection selection = this.tlRoles.Selection;
    ArrayList arrayList = new ArrayList();
    if (selection == null)
      return;
    foreach (UserToRoles role in this._roles)
    {
      for (int index = 0; index < selection.Count; ++index)
      {
        if (role == (UserToRoles) selection[index].Tag)
        {
          if (role.Status == 0)
            role.Status = 2;
          else
            arrayList.Add((object) role);
        }
      }
    }
    foreach (UserToRoles userToRoles in arrayList)
      this._roles.Remove((object) userToRoles);
    foreach (UserToRoles role in this._roles)
    {
      if (role.Status == 2)
        this.IsChanged = true;
    }
    this.UpdateControls();
    this.UpdateButtons();
  }

  private void bAdd_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Client.Core_777"), LocalizationHolder.rm.GetString("Client.Core_778"), sessionKeeper.Session.IdentHelper.RolesTypeID, SelectionOptions.Default);
      if (numArray == null || numArray.Length == 0)
        return;
      List<UserToRoles> first = new List<UserToRoles>();
      foreach (long num in numArray)
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(num);
        first.Add(new UserToRoles(num, objectInfo.Caption, -1L, 1));
      }
      IEnumerable<UserToRoles> second = this._roles.Cast<UserToRoles>();
      UserToRoles[] array = first.Except<UserToRoles>(second, (IEqualityComparer<UserToRoles>) new UserRolesComparer()).ToArray<UserToRoles>();
      if (((IEnumerable<UserToRoles>) array).Any<UserToRoles>())
      {
        this._roles.AddRange((ICollection) array);
        this.IsChanged = true;
        this.UpdateControls();
        this.UpdateButtons();
      }
      else
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (UserToRoles userToRoles in first)
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(userToRoles.ParentID);
          stringBuilder.Append($"\n'{objectInfo.Caption}'");
        }
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_779") + stringBuilder.ToString(), LocalizationHolder.rm.GetString("Client.Core_82"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }
  }

  private void tlRoles_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    TreeListNode focusedNode = this.tlRoles.FocusedNode;
    this.UpdateButtons();
  }

  private void btnApply_Click(object sender, EventArgs e)
  {
    this.SaveObjectData();
    this.UpdateControls();
    this.UpdateButtons();
  }

  private void btnCancel_Click(object sender, EventArgs e)
  {
    ArrayList arrayList = new ArrayList();
    foreach (UserToRoles role in this._roles)
    {
      if (role.Status != 1)
      {
        role.Status = 0;
        arrayList.Add((object) role);
      }
    }
    this._roles = arrayList;
    this.IsChanged = false;
    this.UpdateControls();
    this.UpdateButtons();
  }

  private void panel1_Paint(object sender, PaintEventArgs e)
  {
  }
}
