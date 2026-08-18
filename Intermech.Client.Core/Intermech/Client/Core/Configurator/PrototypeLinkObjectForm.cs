
// Type: Intermech.Client.Core.Configurator.PrototypeLinkObjectForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace Intermech.Client.Core.Configurator;

public class PrototypeLinkObjectForm : Form
{
  private int ownerObjType = -1;
  private List<long> exclusions;
  private PrototypeList prototypeList = new PrototypeList();
  private List<PrototypeClass> allPrototypeClassList;
  private List<PrototypeClass> prototypeClassList = new List<PrototypeClass>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ListView listView;
  private ColumnHeader objectPrototype;
  private ColumnHeader files;
  private Button btnOk;
  private Button btnCancel;
  private Button btnReread;

  public List<PrototypeClass> PrototypeClassList => this.prototypeClassList;

  public PrototypeLinkObjectForm()
  {
    this.InitializeComponent();
    this.allPrototypeClassList = (List<PrototypeClass>) null;
  }

  public void ResetData() => this.allPrototypeClassList = (List<PrototypeClass>) null;

  private void PrototypeLinkObjectForm_Load(object sender, EventArgs e)
  {
    this.listView.SmallImageList = Statics.IconSrv == null ? (ImageList) null : Statics.IconSrv.ImageList;
    this.FillData(false);
    this.FillListView();
  }

  private void FillData() => this.FillData(true);

  private void FillData(bool reread)
  {
    if (this.allPrototypeClassList == null)
      this.allPrototypeClassList = new List<PrototypeClass>();
    else if (!reread)
      return;
    this.allPrototypeClassList.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.ObjectsSelect(new Guid("cad00346-306c-11d8-b4e9-00304f19f545"), new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) -2
      })).Rows)
      {
        PrototypeClass prototypeClass = new PrototypeClass(Convert.ToInt64(row[0]));
        try
        {
          prototypeClass.CheckInit();
        }
        catch
        {
          continue;
        }
        this.allPrototypeClassList.Add(prototypeClass);
      }
    }
  }

  private void FillListView()
  {
    this.listView.Items.Clear();
    this.listView.BeginUpdate();
    try
    {
      for (int index = 0; index < this.allPrototypeClassList.Count; ++index)
      {
        if (this.exclusions.BinarySearch(this.allPrototypeClassList[index].Id) < 0)
          DocObjTypeForm.FillListViewItem(this.listView.Items.Add(string.Empty), this.allPrototypeClassList[index]);
      }
    }
    finally
    {
      this.listView.EndUpdate();
    }
  }

  public DialogResult Execute(int ownerObjType, List<long> exclusions)
  {
    this.ownerObjType = ownerObjType;
    this.exclusions = exclusions;
    this.exclusions.Sort();
    this.prototypeClassList.Clear();
    return this.ShowDialog();
  }

  private void btnReread_Click(object sender, EventArgs e)
  {
    this.FillData(true);
    this.FillListView();
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
    this.prototypeClassList.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this.ownerObjType);
      if (objectType == null)
        return;
      for (int index = 0; index < this.listView.SelectedItems.Count; ++index)
      {
        PrototypeClass tag = (PrototypeClass) this.listView.SelectedItems[index].Tag;
        IDBObject dbObject = sessionKeeper.Session.GetObject(tag.Id);
        if (dbObject != null)
        {
          IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(new Guid("cad00149-306c-11d8-b4e9-00304f19f545")), false);
          if (dbAttribute != null)
          {
            dbAttribute.AddValue((object) (objectType as IDBGuid).GUID);
            this.prototypeClassList.Add(tag);
          }
        }
      }
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PrototypeLinkObjectForm));
    this.listView = new ListView();
    this.objectPrototype = new ColumnHeader();
    this.files = new ColumnHeader();
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.btnReread = new Button();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.listView, "listView");
    this.listView.Columns.AddRange(new ColumnHeader[2]
    {
      this.objectPrototype,
      this.files
    });
    this.listView.FullRowSelect = true;
    this.listView.Name = "listView";
    this.listView.Sorting = SortOrder.Ascending;
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    componentResourceManager.ApplyResources((object) this.objectPrototype, "objectPrototype");
    componentResourceManager.ApplyResources((object) this.files, "files");
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnReread, "btnReread");
    this.btnReread.Name = "btnReread";
    this.btnReread.UseVisualStyleBackColor = true;
    this.btnReread.Click += new EventHandler(this.btnReread_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.btnReread);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.listView);
    this.Name = nameof (PrototypeLinkObjectForm);
    this.Load += new EventHandler(this.PrototypeLinkObjectForm_Load);
    this.ResumeLayout(false);
  }
}
