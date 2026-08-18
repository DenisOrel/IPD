// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ParentProductListDialog
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Columns;
using DevExpress.IM.XtraGrid.Views.Base;
using DevExpress.IM.XtraGrid.Views.Grid;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.UI;
using SuperTooltips;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

public class ParentProductListDialog : Form
{
  private AVSDocument avsDocument;
  private DataTable productsDataTable;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ImageList _imageList;
  private Bevel bevel1;
  private SuperTooltip _superTooltip;
  private Panel panel1;
  private GridControl _gridProducts;
  private DataSet _dataSet;
  private GridView _gridView;
  private GridColumn gridColumn1;
  private GridColumn gridColumn2;
  private Button _btnDelete;
  private Button _btnAdd;
  private Button _btnCancel;
  private Button _btnOK;

  public ParentProductListDialog() => this.InitializeComponent();

  public ParentProductListDialog(AVSDocument avsDocument)
    : this()
  {
    this.avsDocument = avsDocument;
    this.InitProductsList();
    this.UpdateVisualControlsState();
  }

  private void _btnOK_Click(object sender, EventArgs e) => this.SaveChanges();

  /// <summary> Обновить состояние визуальных контролов  </summary>
  private void UpdateVisualControlsState()
  {
    this._btnAdd.Enabled = !this.ReadOnly;
    this._btnDelete.Enabled = !this.ReadOnly && this._gridView.SelectedRowsCount > 0;
  }

  /// <summary> Заполнение визуального списка родительских изделий документа</summary>
  private void InitProductsList()
  {
    this._gridProducts.BeginUpdate();
    this._gridProducts.MainView.BeginUpdate();
    try
    {
      this._gridProducts.DataSource = (object) null;
      if (this.avsDocument == null)
        return;
      this.productsDataTable = new DataTable("TABLE");
      this.productsDataTable.Columns.Add("OBJECT_GUID", typeof (Guid));
      this.productsDataTable.Columns.Add("DESIGNATION", typeof (string));
      this.productsDataTable.Columns.Add("NAME", typeof (string));
      object[] objArray = new object[3];
      for (int index = 0; index < this.avsDocument.ParentProducts.Count; ++index)
      {
        objArray[0] = (object) this.avsDocument.ParentProducts[index].Guid;
        objArray[1] = (object) this.avsDocument.ParentProducts[index].Designation;
        objArray[2] = (object) this.avsDocument.ParentProducts[index].Name;
        this.productsDataTable.Rows.Add(objArray);
      }
      this._dataSet.Tables.Clear();
      this._dataSet.Tables.Add(this.productsDataTable);
      this._gridProducts.DataSource = (object) this._dataSet;
      this._gridProducts.DataMember = this.productsDataTable.TableName;
      this._gridView.FocusedRowHandle = 0;
      this._gridView.SelectRow(0);
      this._gridProducts.Select();
    }
    finally
    {
      this._gridProducts.MainView.EndUpdate();
      this._gridProducts.EndUpdate();
    }
  }

  private void SaveChanges()
  {
    List<ProductInfo> products = new List<ProductInfo>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (DataRow row in (InternalDataCollectionBase) this.productsDataTable.Rows)
      {
        ProductInfo productInfo = new ProductInfo(sessionKeeper.Session.GetObject((Guid) row[0]));
        products.Add(productInfo);
      }
    }
    this.avsDocument.SetParentProducts(products);
  }

  private bool ReadOnly => this.avsDocument == null || this.avsDocument.ReadOnly;

  private void _btnAdd_Click(object sender, EventArgs e)
  {
    List<ProductInfo> productInfoList = new List<ProductInfo>();
    IDBTypedObjectID[] dbTypedObjectIdArray = this.SelectDBObjects();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < dbTypedObjectIdArray.Length; ++index)
      {
        IDBObject productObj = sessionKeeper.Session.GetObject(dbTypedObjectIdArray[index].ObjectID);
        if (this.productsDataTable.Select($"OBJECT_GUID = '{productObj.ObjectGUID.ToString()}'").Length == 0)
        {
          ProductInfo productInfo = new ProductInfo(productObj);
          this.productsDataTable.Rows.Add((object) productInfo.Guid, (object) productInfo.Designation, (object) productInfo.Name);
        }
      }
    }
  }

  /// <summary>Выбор изделий из БД</summary>
  private IDBTypedObjectID[] SelectDBObjects()
  {
    List<int> intList = new List<int>()
    {
      AvsIDCache.ObjType_AssemblyUnit
    };
    DescriptorCollection descriptors = new DescriptorCollection();
    for (int index = 0; index < intList.Count; ++index)
      descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(intList[index]));
    object[] objArray = SelectionWindow.Select("Выберите объект", descriptors.Count != 1 ? (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor("Допустимые типы объектов", descriptors) : descriptors[0], typeof (IDBTypedObjectID), SelectionOptions.Default | SelectionOptions.ForceFilterObjectsByRule);
    IDBTypedObjectID[] dbTypedObjectIdArray;
    if (objArray != null)
    {
      dbTypedObjectIdArray = new IDBTypedObjectID[objArray.Length];
      objArray.CopyTo((Array) dbTypedObjectIdArray, 0);
    }
    else
      dbTypedObjectIdArray = new IDBTypedObjectID[0];
    return dbTypedObjectIdArray;
  }

  private void _btnDelete_Click(object sender, EventArgs e)
  {
    if (this._gridView.SelectedRowsCount <= 0)
      return;
    int[] selectedRows = this._gridView.GetSelectedRows();
    DataRow[] dataRowArray = new DataRow[selectedRows.Length];
    for (int index = 0; index < selectedRows.Length; ++index)
      dataRowArray[index] = this._gridView.GetDataRow(selectedRows[index]);
    for (int index = 0; index < dataRowArray.Length; ++index)
      this.productsDataTable.Rows.Remove(dataRowArray[index]);
  }

  private void _gridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    this.UpdateVisualControlsState();
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ParentProductListDialog));
    this._imageList = new ImageList(this.components);
    this.panel1 = new Panel();
    this._gridProducts = new GridControl();
    this._dataSet = new DataSet();
    this._gridView = new GridView();
    this.gridColumn1 = new GridColumn();
    this.gridColumn2 = new GridColumn();
    this._btnDelete = new Button();
    this._btnAdd = new Button();
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this._superTooltip = new SuperTooltip();
    this.bevel1 = new Bevel();
    this.panel1.SuspendLayout();
    this._gridProducts.BeginInit();
    this._dataSet.BeginInit();
    this._gridView.BeginInit();
    this.SuspendLayout();
    this._imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imageList.ImageStream");
    this._imageList.TransparentColor = Color.Transparent;
    this._imageList.Images.SetKeyName(0, "Lock.png");
    this._imageList.Images.SetKeyName(1, "new- style 2.gif");
    this._imageList.Images.SetKeyName(2, "edit.gif");
    this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.panel1.Controls.Add((Control) this._gridProducts);
    this.panel1.Location = new Point(1, 1);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(613, 188);
    this.panel1.TabIndex = 14;
    this._gridProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._gridProducts.DataMember = "_dataTable";
    this._gridProducts.DataSource = (object) this._dataSet;
    this._gridProducts.EmbeddedNavigator.Name = "";
    this._gridProducts.Location = new Point(3, 0);
    this._gridProducts.MainView = (BaseView) this._gridView;
    this._gridProducts.Name = "_gridProducts";
    this._gridProducts.Size = new Size(610, 185);
    this._gridProducts.TabIndex = 0;
    this._dataSet.DataSetName = "NewDataSet";
    this._gridView.Columns.AddRange(new GridColumn[2]
    {
      this.gridColumn1,
      this.gridColumn2
    });
    this._gridView.GridControl = this._gridProducts;
    this._gridView.Name = "_gridView";
    this._gridView.OptionsBehavior.Editable = false;
    this._gridView.OptionsCustomization.AllowFilter = false;
    this._gridView.OptionsCustomization.AllowGroup = false;
    this._gridView.OptionsCustomization.AllowSort = false;
    this._gridView.OptionsMenu.EnableColumnMenu = false;
    this._gridView.OptionsMenu.EnableFooterMenu = false;
    this._gridView.OptionsMenu.EnableGroupPanelMenu = false;
    this._gridView.OptionsSelection.MultiSelect = true;
    this._gridView.OptionsView.ShowFilterPanel = false;
    this._gridView.OptionsView.ShowGroupPanel = false;
    this._gridView.OptionsView.ShowHorzLines = false;
    this._gridView.OptionsView.ShowIndicator = false;
    this._gridView.OptionsView.ShowVertLines = false;
    this._gridView.SelectionChanged += new DevExpress.IM.XtraGrid.SelectionChangedEventHandler(this._gridView_SelectionChanged);
    this.gridColumn1.Caption = "Обозначение";
    this.gridColumn1.FieldName = "DESIGNATION";
    this.gridColumn1.MinWidth = 200;
    this.gridColumn1.Name = "gridColumn1";
    this.gridColumn1.Options = ColumnOptions.CanMoved | ColumnOptions.CanFocused | ColumnOptions.ShowInCustomizationForm;
    this.gridColumn1.VisibleIndex = 0;
    this.gridColumn1.Width = 300;
    this.gridColumn2.Caption = "Наименование";
    this.gridColumn2.FieldName = "NAME";
    this.gridColumn2.MinWidth = 200;
    this.gridColumn2.Name = "gridColumn2";
    this.gridColumn2.Options = ColumnOptions.CanResized | ColumnOptions.CanFocused | ColumnOptions.ShowInCustomizationForm;
    this.gridColumn2.StyleName = "Style1";
    this.gridColumn2.VisibleIndex = 1;
    this.gridColumn2.Width = 300;
    this._btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._btnDelete.FlatStyle = FlatStyle.System;
    this._btnDelete.ImeMode = ImeMode.NoControl;
    this._btnDelete.Location = new Point(137, 199);
    this._btnDelete.Name = "_btnDelete";
    this._btnDelete.Size = new Size(121, 27);
    this._superTooltip.SetSuperTooltip((IComponent) this._btnDelete, new SuperTooltipInfo("Удалить", "", "Удалить выбранное изделие из списка", (Image) null, (Image) null, TooltipColorScheme.Lemon, true, false, new Size(0, 0)));
    this._btnDelete.TabIndex = 18;
    this._btnDelete.Text = "&Удалить";
    this._btnDelete.UseVisualStyleBackColor = true;
    this._btnDelete.Click += new EventHandler(this._btnDelete_Click);
    this._btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._btnAdd.FlatStyle = FlatStyle.System;
    this._btnAdd.ImeMode = ImeMode.NoControl;
    this._btnAdd.Location = new Point(10, 199);
    this._btnAdd.Name = "_btnAdd";
    this._btnAdd.Size = new Size(121, 27);
    this._superTooltip.SetSuperTooltip((IComponent) this._btnAdd, new SuperTooltipInfo("Добавить", "", "Добавить изделие по составу которого формируется документ", (Image) null, (Image) null, TooltipColorScheme.Lemon, true, false, new Size(0, 0)));
    this._btnAdd.TabIndex = 16 /*0x10*/;
    this._btnAdd.Text = "&Добавить...";
    this._btnAdd.UseVisualStyleBackColor = true;
    this._btnAdd.Click += new EventHandler(this._btnAdd_Click);
    this._btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.FlatStyle = FlatStyle.System;
    this._btnCancel.ImeMode = ImeMode.NoControl;
    this._btnCancel.Location = new Point(482, 244);
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.Size = new Size(121, 27);
    this._btnCancel.TabIndex = 21;
    this._btnCancel.Text = "О&тмена";
    this._btnCancel.UseVisualStyleBackColor = true;
    this._btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.FlatStyle = FlatStyle.System;
    this._btnOK.ImeMode = ImeMode.NoControl;
    this._btnOK.Location = new Point(355, 244);
    this._btnOK.Name = "_btnOK";
    this._btnOK.Size = new Size(121, 27);
    this._btnOK.TabIndex = 19;
    this._btnOK.Text = "&ОК";
    this._btnOK.UseVisualStyleBackColor = true;
    this._btnOK.Click += new EventHandler(this._btnOK_Click);
    this._superTooltip.DefaultFont = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.bevel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.bevel1.Location = new Point(0, 237);
    this.bevel1.Name = "bevel1";
    this.bevel1.Shape = BevelShape.TopLine;
    this.bevel1.Size = new Size(616, 2);
    this.bevel1.TabIndex = 22;
    this.bevel1.Text = "bevel1";
    this.AcceptButton = (IButtonControl) this._btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.ClientSize = new Size(615, 278);
    this.Controls.Add((Control) this.bevel1);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this._btnDelete);
    this.Controls.Add((Control) this._btnAdd);
    this.Controls.Add((Control) this._btnCancel);
    this.Controls.Add((Control) this._btnOK);
    this.MinimizeBox = false;
    this.MinimumSize = new Size(400, 250);
    this.Name = nameof (ParentProductListDialog);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Список изделий, состав которых формирует документ";
    this.panel1.ResumeLayout(false);
    this._gridProducts.EndInit();
    this._dataSet.EndInit();
    this._gridView.EndInit();
    this.ResumeLayout(false);
  }
}
