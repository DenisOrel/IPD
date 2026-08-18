// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSViews.SetupObjectTypesView
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Common_Dialogs;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.AVSViews;

[ViewDescriptionProvider(typeof (SetupObjectTypesView.CustomViewDescriptionProvider))]
public class SetupObjectTypesView : UserControl, IView
{
  protected ICategoryTypeIconService _objtypesIcons;
  private static IAVSTemplatesViewsService avsTemplateService;
  private DocumentType docType;
  private bool changed;
  private List<ObjectType> types = new List<ObjectType>();
  /// <summary>Идентификатор выделенного объекта</summary>
  private int _imageIndex = -1;
  private string _viewName = "Типы документов";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private DataGridView dataGridView1;
  private Panel panel1;
  private Button _BtnCancel;
  private Button bAdd;
  private Button _BtnOK;
  private DataGridViewImageColumn ColImage;
  private DataGridViewTextBoxColumn ColName;
  private DataGridViewButtonColumn ColButtonDelete;
  private DataGridViewLinkColumn ColTemplate;
  private DataGridViewButtonColumn ColSelectTemplate;

  public SetupObjectTypesView()
  {
    this.InitializeComponent();
    this._objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this.dataGridView1.Refresh();
    this.dataGridView1.RowCount = 0;
    this.dataGridView1.Focus();
    this.dataGridView1.Select();
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (!(provider.GetService(typeof (IAVSTemplatesViewsService)) is IAVSTemplatesViewsService templatesViewsService))
      templatesViewsService = SetupObjectTypesView.avsTemplateService;
    SetupObjectTypesView.avsTemplateService = templatesViewsService;
    this.docType = SetupObjectTypesView.avsTemplateService?.DocumentType;
    this.ColSelectTemplate.Visible = false;
    this.ColTemplate.Visible = false;
    this.Changed = false;
    this.UpdateTypes();
  }

  private void UpdateTypes()
  {
    this.types.Clear();
    if (this.docType != null)
    {
      foreach (Guid dbObjectType in this.docType.DBObjectTypeList)
      {
        ObjectType type = this.CreateType(dbObjectType);
        if (type != null)
          this.types.Add(type);
      }
    }
    this.UpdateGrid();
  }

  private void Save()
  {
    this.docType.DBObjectTypeList.Clear();
    foreach (ObjectType type in this.types)
      this.docType.DBObjectTypeList.Add(type.Guid);
    this.docType.Changed = true;
    this.Changed = false;
  }

  public bool Changed
  {
    get => this.changed;
    set
    {
      this.changed = value;
      this._BtnOK.Enabled = this.changed;
      this._BtnCancel.Enabled = this.changed;
    }
  }

  public void Activate(IView previousView)
  {
    this.UpdateTypes();
    this.Changed = false;
  }

  public void Deactivate(IView nextView)
  {
    if (!this.changed || MessageBox.Show("Сохранить данные перед выходом", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this.Save();
  }

  private ObjectType Selected
  {
    get
    {
      if (this.dataGridView1.SelectedRows.Count > 0)
      {
        int index = this.dataGridView1.SelectedRows[0].Index;
        if (index >= 0 && index < this.types.Count)
          return this.types[index];
      }
      return (ObjectType) null;
    }
  }

  private void ReplaceType(ObjectType old, ObjectType newType)
  {
    if (old != null)
      this.types[this.types.IndexOf(old)] = newType;
    else
      this.types.Add(newType);
    this.Changed = true;
    this.UpdateGrid();
  }

  private void AddType(ObjectType type)
  {
    if (type == null)
      return;
    this.types.Add(type);
    this.Changed = true;
    this.UpdateGrid();
  }

  private void UpdateGrid()
  {
    this.dataGridView1.RowCount = this.types.Count;
    this.dataGridView1.Refresh();
  }

  private void RemoveType(ObjectType type)
  {
    this.types.Remove(type);
    this.Changed = true;
    this.UpdateGrid();
  }

  public string Caption => this._viewName;

  public int ImageIndex => this._imageIndex;

  public int OrderID => 0;

  private void dataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
  {
    if (e.RowIndex == this.types.Count)
    {
      switch (e.ColumnIndex)
      {
        case 0:
          e.Value = (object) new Bitmap(16 /*0x10*/, 16 /*0x10*/);
          break;
        case 2:
          e.Value = (object) "...";
          break;
        case 3:
          e.Value = (object) "";
          break;
      }
    }
    else
    {
      if (e.RowIndex < 0 || e.RowIndex >= this.types.Count)
        return;
      ObjectType type = this.types[e.RowIndex];
      switch (e.ColumnIndex)
      {
        case 0:
          e.Value = (object) type.Image;
          break;
        case 1:
          e.Value = (object) type.Caption;
          break;
        case 2:
          e.Value = (object) "Удалить";
          break;
      }
    }
  }

  /// <summary>Вернуть значок для указанного типа объекта</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Значок для указанного типа объекта</returns>
  protected virtual Bitmap GetObjTypeIcon(int objTypeID)
  {
    objTypeID = Math.Max(objTypeID, -1);
    if (this._objtypesIcons.IndexOf(4, objTypeID) < 0)
      return (Bitmap) null;
    return ImagesResizeHelper.ResizeIconTo16x16(this._objtypesIcons.GetIcon(4, objTypeID), Color.Transparent)?.ToBitmap();
  }

  private ObjectType CreateType(Guid guid)
  {
    ObjectType type = (ObjectType) null;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(guid);
    if (objectType != null)
    {
      Bitmap objTypeIcon = this.GetObjTypeIcon(objectType.ObjectTypeID);
      string objectName = objectType.ObjectName;
      int objectTypeId = objectType.ObjectTypeID;
      Guid guid1 = guid;
      string caption = objectName;
      type = new ObjectType((Image) objTypeIcon, objectTypeId, guid1, caption);
    }
    return type;
  }

  private ObjectType CreateType(int id)
  {
    ObjectType type = (ObjectType) null;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(id);
    if (objectType != null)
    {
      Bitmap objTypeIcon = this.GetObjTypeIcon(id);
      string objectName = objectType.ObjectName;
      int typeId = id;
      Guid guid = objectType.Guid;
      string caption = objectName;
      type = new ObjectType((Image) objTypeIcon, typeId, guid, caption);
    }
    return type;
  }

  private void SelectType(bool isNew)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Все объекты", typeof (ObjectTypeFolder), false);
    selectorForm.Text = "Выберите тип объекта";
    List<int> intList = new List<int>();
    bool flag = false;
    if (this.docType.Type.HasValue)
      flag = AVSDocumentsSettings.IsSpecificationDocType(this.docType.Type.Value);
    if (flag)
      intList.Add(AvsIDCache.ObjType_Specification);
    else
      intList.Add(AvsIDCache.ObjType_Document);
    selectorForm.SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(intList.ToArray(), true, true);
    selectorForm.NodeSelectorFilter = (INodeSelectorFilter) new NodeSelectorFilter();
    int num = (int) selectorForm.ShowDialog();
    int id = -1;
    if (num == 1 && selectorForm.IDList.Count > 0)
      id = (int) selectorForm.IDList[0];
    if (id == -1)
      return;
    ObjectType type = this.CreateType(id);
    if (type == null)
      return;
    if (isNew)
      this.AddType(type);
    else
      this.ReplaceType(this.Selected, type);
  }

  private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
  {
  }

  private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
  {
    if (e.ColumnIndex != 2)
      return;
    this.RemoveType(this.types[e.RowIndex]);
  }

  private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
  {
  }

  private void _BtnOK_Click(object sender, EventArgs e) => this.Save();

  private void _BtnCancel_Click(object sender, EventArgs e)
  {
    this.Changed = false;
    this.UpdateTypes();
  }

  private void bAdd_Click(object sender, EventArgs e) => this.SelectType(true);

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
    this.dataGridView1 = new DataGridView();
    this.ColImage = new DataGridViewImageColumn();
    this.ColName = new DataGridViewTextBoxColumn();
    this.ColButtonDelete = new DataGridViewButtonColumn();
    this.ColTemplate = new DataGridViewLinkColumn();
    this.ColSelectTemplate = new DataGridViewButtonColumn();
    this.panel1 = new Panel();
    this._BtnCancel = new Button();
    this.bAdd = new Button();
    this._BtnOK = new Button();
    ((ISupportInitialize) this.dataGridView1).BeginInit();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.dataGridView1.AllowUserToAddRows = false;
    this.dataGridView1.AllowUserToDeleteRows = false;
    this.dataGridView1.AllowUserToResizeColumns = false;
    this.dataGridView1.AllowUserToResizeRows = false;
    this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView1.Columns.AddRange((DataGridViewColumn) this.ColImage, (DataGridViewColumn) this.ColName, (DataGridViewColumn) this.ColButtonDelete, (DataGridViewColumn) this.ColTemplate, (DataGridViewColumn) this.ColSelectTemplate);
    this.dataGridView1.Dock = DockStyle.Fill;
    this.dataGridView1.Location = new Point(0, 0);
    this.dataGridView1.MultiSelect = false;
    this.dataGridView1.Name = "dataGridView1";
    this.dataGridView1.RowHeadersVisible = false;
    this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dataGridView1.Size = new Size(551, 360);
    this.dataGridView1.TabIndex = 0;
    this.dataGridView1.VirtualMode = true;
    this.dataGridView1.CellValueNeeded += new DataGridViewCellValueEventHandler(this.dataGridView1_CellValueNeeded);
    this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
    this.dataGridView1.CellEnter += new DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
    this.dataGridView1.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
    this.ColImage.HeaderText = "";
    this.ColImage.Name = "ColImage";
    this.ColImage.ReadOnly = true;
    this.ColImage.Resizable = DataGridViewTriState.False;
    this.ColImage.Width = 30;
    this.ColName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.ColName.FillWeight = 50f;
    this.ColName.HeaderText = "Тип объекта";
    this.ColName.Name = "ColName";
    this.ColName.ReadOnly = true;
    this.ColName.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.ColButtonDelete.HeaderText = "";
    this.ColButtonDelete.Name = "ColButtonDelete";
    this.ColButtonDelete.Width = 90;
    this.ColTemplate.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.ColTemplate.FillWeight = 50f;
    this.ColTemplate.HeaderText = "Шаблон";
    this.ColTemplate.Name = "ColTemplate";
    this.ColTemplate.TrackVisitedState = false;
    this.ColSelectTemplate.HeaderText = "";
    this.ColSelectTemplate.Name = "ColSelectTemplate";
    this.ColSelectTemplate.Width = 30;
    this.panel1.Controls.Add((Control) this._BtnCancel);
    this.panel1.Controls.Add((Control) this.bAdd);
    this.panel1.Controls.Add((Control) this._BtnOK);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 360);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(551, 53);
    this.panel1.TabIndex = 1;
    this._BtnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnCancel.Enabled = false;
    this._BtnCancel.FlatStyle = FlatStyle.System;
    this._BtnCancel.Location = new Point(415, 11);
    this._BtnCancel.Name = "_BtnCancel";
    this._BtnCancel.Size = new Size(121, 27);
    this._BtnCancel.TabIndex = 4;
    this._BtnCancel.Text = "Отмена";
    this._BtnCancel.Click += new EventHandler(this._BtnCancel_Click);
    this.bAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.bAdd.FlatStyle = FlatStyle.System;
    this.bAdd.Location = new Point(16 /*0x10*/, 11);
    this.bAdd.Name = "bAdd";
    this.bAdd.Size = new Size(121, 27);
    this.bAdd.TabIndex = 3;
    this.bAdd.Text = "Добавить";
    this.bAdd.Click += new EventHandler(this.bAdd_Click);
    this._BtnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnOK.Enabled = false;
    this._BtnOK.FlatStyle = FlatStyle.System;
    this._BtnOK.Location = new Point(288, 11);
    this._BtnOK.Name = "_BtnOK";
    this._BtnOK.Size = new Size(121, 27);
    this._BtnOK.TabIndex = 3;
    this._BtnOK.Text = "Применить";
    this._BtnOK.Click += new EventHandler(this._BtnOK_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.dataGridView1);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (SetupObjectTypesView);
    this.Size = new Size(551, 413);
    ((ISupportInitialize) this.dataGridView1).EndInit();
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private sealed class CustomViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = "Типы документов",
        ImageIndex = -1,
        OrderID = 0
      };
    }
  }
}
