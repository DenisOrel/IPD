// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ProductsListDialog
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Columns;
using DevExpress.IM.XtraGrid.Views.Base;
using DevExpress.IM.XtraGrid.Views.Grid;
using DevExpress.IM.XtraGrid.Views.Grid.ViewInfo;
using Intermech.AVS.Common_Dialogs;
using Intermech.AVS.Properties;
using Intermech.AVS.Sorting;
using Intermech.Document.Client;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Pdm;
using Intermech.UI;
using SuperTooltips;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Диалог редактирования списка исполнений </summary>
public class ProductsListDialog : Form
{
  private AVSDocument avsDocument;
  private int _lockUpdateVisualControlsCounter;
  private DataTable _dataTable;
  private bool _readOnly;
  private List<Guid> _deletedProducts = new List<Guid>();
  private List<DataRow> _createdProducts = new List<DataRow>();
  private List<DataRow> _renamedProducts = new List<DataRow>();
  private Dictionary<DataRow, int> _newProductToPrototypeIndexDictionary = new Dictionary<DataRow, int>();
  private Dictionary<DataRow, int> _lockedProducts = new Dictionary<DataRow, int>();
  private SuperTooltipControl toolTipControl;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button bAddRange;
  private Button _btnCancel;
  private Button _btnOK;
  private DataSet _dataSet;
  private GridControl _gridProducts;
  private GridView _gridView;
  private GridColumn gridColumn1;
  private GridColumn gridColumn2;
  private Label label1;
  private Button _btnAdd;
  private Button _btnRename;
  private Button _btnDelete;
  private Button _btnMoveDown;
  private Button _btnMoveUp;
  private Panel panel1;
  private Bevel bevel1;
  private SuperTooltip _superTooltip;
  private Button _btnMoveToFinish;
  private Button _btnMoveToStart;
  private ImageList _imageList;
  private Button _btnSort;

  public ProductsListDialog(AVSDocument avsDocument)
  {
    this.InitializeComponent();
    this.LockUpdateVisualControls();
    try
    {
      this.avsDocument = avsDocument;
      this.InitProductsList();
    }
    finally
    {
      this.UnlockUpdateVisualControls();
    }
  }

  public ProductsListDialog(AVSDocument avsDocument, DataTable model)
  {
    this.InitializeComponent();
    this.LockUpdateVisualControls();
    try
    {
      this.avsDocument = avsDocument;
      this.InitProductsList(model);
    }
    finally
    {
      this.UnlockUpdateVisualControls();
    }
  }

  /// <summary> Доступность для редактирования </summary>
  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      if (this._readOnly == value)
        return;
      this.LockUpdateVisualControls();
      try
      {
        this._readOnly = value;
        if (this._readOnly)
        {
          this._btnCancel.Text = "&Закрыть";
          this.AcceptButton = (IButtonControl) null;
          this._btnOK.Enabled = false;
          this._btnOK.Visible = false;
        }
        else
        {
          this._btnCancel.Text = "О&тмена";
          this.AcceptButton = (IButtonControl) this._btnOK;
          this._btnOK.Enabled = true;
          this._btnOK.Visible = true;
        }
      }
      finally
      {
        this.UnlockUpdateVisualControls();
      }
    }
  }

  /// <summary> Заполнение визуального списка исполнений специфицируемого изделия </summary>
  private void InitProductsList(DataTable dataTable = null)
  {
    this.LockUpdateVisualControls();
    try
    {
      this._gridProducts.BeginUpdate();
      this._gridProducts.MainView.BeginUpdate();
      try
      {
        this._gridProducts.DataSource = (object) null;
        if (this.avsDocument == null || this.avsDocument.AvsDocumentForm == AVSDocumentForm.Single)
          return;
        this._dataTable = dataTable ?? ProductsListDialog.GetProductInfoDataTable(this.avsDocument);
        this._dataSet.Tables.Clear();
        this._dataSet.Tables.Add(this._dataTable);
        this._gridProducts.DataMember = this._dataTable.TableName;
        this._gridProducts.DataSource = (object) this._dataSet;
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
    finally
    {
      this.UnlockUpdateVisualControls();
    }
  }

  internal static DataTable GetProductInfoDataTable(AVSDocument avsDocument)
  {
    if (avsDocument == null)
      return (DataTable) null;
    DataTable productInfoDataTable = new DataTable("TABLE");
    productInfoDataTable.Columns.Add("OBJECT_ID", typeof (Guid));
    productInfoDataTable.Columns.Add("CAPTION", typeof (string));
    productInfoDataTable.Columns.Add("NUMBER", typeof (string));
    object[] objArray = new object[3];
    int currentNumber = -1;
    for (int index = 0; index < avsDocument.productsInfo.Count; ++index)
    {
      objArray[0] = (object) avsDocument.productsInfo[index].Guid;
      objArray[1] = (object) avsDocument.productsInfo[index].Designation;
      objArray[2] = (object) avsDocument.productsInfo[index].GetNumber(currentNumber, out currentNumber, avsDocument.BaseProductDesignation, avsDocument.UseSameDesignationForProducts);
      productInfoDataTable.Rows.Add(objArray);
    }
    return productInfoDataTable;
  }

  /// <summary> Обновить состояние визуальных котролов  </summary>
  private void UpdateVisualControlsState()
  {
    if (this._lockUpdateVisualControlsCounter != 0)
      return;
    ++this._lockUpdateVisualControlsCounter;
    try
    {
      DataRow dataRow = (DataRow) null;
      if (this._gridView.FocusedRowHandle != -1)
        dataRow = this._gridView.GetDataRow(this._gridView.FocusedRowHandle);
      int num = this._dataTable == null || dataRow == null ? -1 : this._dataTable.Rows.IndexOf(dataRow);
      this.bAddRange.Enabled = this.avsDocument.UseSameDesignationForProducts && this.avsDocument.AvsDocumentForm != AVSDocumentForm.Mirror;
      this._btnMoveToStart.Enabled = !this.ReadOnly && dataRow != null && num > 0;
      this._btnMoveUp.Enabled = !this.ReadOnly && dataRow != null && num > 0;
      this._btnMoveDown.Enabled = !this.ReadOnly && dataRow != null && num < this._dataTable.Rows.Count - 1;
      this._btnMoveToFinish.Enabled = !this.ReadOnly && dataRow != null && num < this._dataTable.Rows.Count - 1;
      this._btnAdd.Enabled = !this.ReadOnly && this.avsDocument.AvsDocumentForm != AVSDocumentForm.Mirror && this.avsDocument.AvsDocumentForm != 0;
      this._btnRename.Enabled = !this.ReadOnly && dataRow != null;
      this._btnDelete.Enabled = !this.ReadOnly && this.avsDocument.AvsDocumentForm != AVSDocumentForm.Mirror && dataRow != null && this._dataTable.Rows.Count > 1 && !this._lockedProducts.ContainsKey(dataRow);
    }
    finally
    {
      --this._lockUpdateVisualControlsCounter;
    }
  }

  /// <summary> Заблокировать обновление визуальных контролов </summary>
  private void LockUpdateVisualControls() => ++this._lockUpdateVisualControlsCounter;

  /// <summary> Разблокировать обновление визуальных контролов </summary>
  private void UnlockUpdateVisualControls() => this.UnlockUpdateVisualControls(true);

  /// <summary> Разблокировать обновление визуальных контролов </summary>
  private void UnlockUpdateVisualControls(bool updateControlsIfUnlocked)
  {
    if (this._lockUpdateVisualControlsCounter == 0)
    {
      if (!updateControlsIfUnlocked)
        return;
      this.UpdateVisualControlsState();
    }
    else
    {
      --this._lockUpdateVisualControlsCounter;
      if (this._lockUpdateVisualControlsCounter != 0 || !updateControlsIfUnlocked)
        return;
      this.UpdateVisualControlsState();
    }
  }

  /// <summary>Получить исполнение по индеку в списке исполнений документа</summary>
  /// <param name="productIndex">Индекс исполнения в списке исполнений документа</param>
  /// <returns></returns>
  private ProductInfo GetProduct(object productID)
  {
    switch (productID)
    {
      case null:
      case DBNull _:
        return (ProductInfo) null;
      case Guid productGuid:
        return this.avsDocument.GetProductInfoByObjectGuid(productGuid);
      default:
        return this.avsDocument.GetProductInfoByObjectID(Convert.ToInt64(productID));
    }
  }

  /// <summary> Сохранить изменения в БД </summary>
  public void Save()
  {
    List<DBObjectsExtendedEventArgs> extendedEventArgsList = new List<DBObjectsExtendedEventArgs>();
    this.avsDocument.SuspendDocumentAndGridUpdates();
    try
    {
      List<ProductInfo> productInfoList = new List<ProductInfo>();
      foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
        productInfoList.Add(this.GetProduct(row[0]));
      if (this._deletedProducts.Count > 0)
      {
        List<ProductInfo> products = new List<ProductInfo>();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          for (int index = 0; index < this._deletedProducts.Count; ++index)
          {
            ProductInfo infoByObjectGuid = this.avsDocument.GetProductInfoByObjectGuid(this._deletedProducts[index]);
            if (infoByObjectGuid != null)
              products.Add(infoByObjectGuid);
            else
              sessionKeeper.Session.GetObject(this._deletedProducts[index], false)?.Delete(0L);
          }
        }
        this.avsDocument.RemoveProductVersions((IList<ProductInfo>) products, true, true);
      }
      int productIndex = 0;
      int index1 = 0;
      int num = 0;
      List<NewProductParams> newProductParams = new List<NewProductParams>();
      foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
      {
        Guid empty = Guid.Empty;
        if (row[0] != null && row[0] is Guid)
          empty = (Guid) row[0];
        if (this.GetProduct((object) empty) == null && row[1] != null && !(row[1] is DBNull))
        {
          string productDesignation = Convert.ToString(row[1]);
          string productNumber = Convert.ToString(row[2]);
          long productID = -1;
          if (empty != Guid.Empty)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              productID = sessionKeeper.Session.GetObjectInfo(empty).ObjectID;
          }
          if (this._newProductToPrototypeIndexDictionary.ContainsKey(row))
            newProductParams.Add(new NewProductParams(productID, this._newProductToPrototypeIndexDictionary[row], productDesignation, productNumber, productIndex));
        }
        else if (row[1] != null && !(row[1] is DBNull))
        {
          ProductInfo product = productInfoList[index1];
          string str1 = Convert.ToString(row[1]);
          string str2 = Convert.ToString(row[2]).Replace("-", string.Empty);
          if (product != null)
          {
            this.avsDocument.SetProductIndex(product, num++, true, false);
            if (this.avsDocument.IsSpecification && product.Id != -1L)
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                IDBObject dbObj = sessionKeeper.Session.GetObject(product.Id);
                if (dbObj != null)
                {
                  if (!this.avsDocument.UseSameDesignationForProducts)
                  {
                    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
                    if (product.Number != null && product.Number != "" && product.Number != str2 || product.generatedNumber != str2)
                    {
                      attributeValuesList.Add(new AttributeValues(AvsIDCache.Attr_ProductCode, (object) str2));
                      extendedEventArgsList.Add(new DBObjectsExtendedEventArgs(dbObj.ObjectID, dbObj.ObjectType, new AttributeValues(AvsIDCache.Attr_ProductCode, (object) product.ProductKod), new AttributeValues(AvsIDCache.Attr_ProductCode, (object) str2)));
                    }
                    if (product.Designation != str1)
                    {
                      attributeValuesList.Add(new AttributeValues(AvsIDCache.Attr_Designation, (object) str1));
                      extendedEventArgsList.Add(new DBObjectsExtendedEventArgs(dbObj.ObjectID, dbObj.ObjectType, new AttributeValues(AvsIDCache.Attr_Designation, (object) product.Designation), new AttributeValues(AvsIDCache.Attr_Designation, (object) str1)));
                    }
                    if (attributeValuesList.Count > 0)
                      DBObjectHelper.SetDBAttributeValues(dbObj, attributeValuesList.ToArray());
                  }
                  else if (product.Designation != str1)
                  {
                    AttributeValues[] values = new AttributeValues[2]
                    {
                      new AttributeValues(AvsIDCache.Attr_Designation, (object) str1),
                      new AttributeValues(AvsIDCache.Attr_ProductCode, (object) null)
                    };
                    DBObjectHelper.SetDBAttributeValues(dbObj, values);
                    extendedEventArgsList.Add(new DBObjectsExtendedEventArgs(dbObj.ObjectID, dbObj.ObjectType, new AttributeValues(AvsIDCache.Attr_Designation, (object) product.Designation), new AttributeValues(AvsIDCache.Attr_Designation, (object) str1)));
                    this.avsDocument.SetProductDesignation(product, str1, str2, false);
                  }
                }
              }
            }
            else
              this.avsDocument.SetProductDesignation(product, str1, str2, false);
          }
        }
        ++productIndex;
        ++index1;
      }
      if (extendedEventArgsList.Count > 0 && AVSPlugin.NotificationService != null)
      {
        for (int index2 = 0; index2 < extendedEventArgsList.Count; ++index2)
          AVSPlugin.NotificationService.FireEvent((object) null, (NotificationEventArgs) extendedEventArgsList[index2]);
      }
      if (newProductParams.Count > 0)
      {
        this.avsDocument.InsertNewProducts((IList<NewProductParams>) newProductParams);
      }
      else
      {
        bool reCreateListNode = false;
        if ((this.avsDocument.IsFormB || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V) && this.avsDocument.IsGridViewMode)
        {
          this.avsDocument.AVSWindow.NeedToLoadColumnParams = true;
          reCreateListNode = true;
          this.avsDocument.AVSWindow.LoadColumnsStateIfNeeded();
          this.avsDocument.SaveExpanded();
        }
        this.avsDocument.UpdateViewNodes(false, reCreateListNode, true, true, false, EmptyRowUpdateMode.DontChange);
        this.avsDocument.UpdateProductHeadersOnPages(false, false);
      }
    }
    finally
    {
      this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true, true);
    }
  }

  /// <summary> Была нажата кнопка "Переместить в начало списка" </summary>
  private void _btnMoveToStart_Click(object sender, EventArgs e)
  {
    this.LockUpdateVisualControls();
    try
    {
      if (this._gridView.FocusedRowHandle <= 0)
        return;
      DataRow dataRow = this._gridView.GetDataRow(this._gridView.FocusedRowHandle);
      if (dataRow == null)
        return;
      object[] itemArray = dataRow.ItemArray;
      this._dataTable.Rows.Remove(dataRow);
      this._dataTable.Rows.InsertAt(dataRow, 0);
      dataRow.ItemArray = itemArray;
      this._gridView.FocusedRowHandle = 0;
      this._gridView.SelectRow(0);
    }
    finally
    {
      this.UnlockUpdateVisualControls();
    }
  }

  /// <summary> Была нажата кнопка "Переместить выше" </summary>
  private void _btnMoveUp_Click(object sender, EventArgs e)
  {
    this.LockUpdateVisualControls();
    try
    {
      if (this._gridView.FocusedRowHandle <= 0)
        return;
      DataRow dataRow = this._gridView.GetDataRow(this._gridView.FocusedRowHandle);
      if (dataRow == null)
        return;
      int num = this._dataTable.Rows.IndexOf(dataRow);
      if (num <= 0)
        return;
      object[] itemArray = dataRow.ItemArray;
      this._dataTable.Rows.Remove(dataRow);
      this._dataTable.Rows.InsertAt(dataRow, num - 1);
      dataRow.ItemArray = itemArray;
      this._gridView.FocusedRowHandle = num - 1;
      this._gridView.SelectRow(num - 1);
    }
    finally
    {
      this.UnlockUpdateVisualControls();
    }
  }

  /// <summary> Была нажата кнопка "Переместить ниже" </summary>
  private void _btnMoveDown_Click(object sender, EventArgs e)
  {
    this.LockUpdateVisualControls();
    try
    {
      if (this._gridView.FocusedRowHandle <= -1 || this._gridView.FocusedRowHandle >= this._dataTable.Rows.Count - 1)
        return;
      DataRow dataRow = this._gridView.GetDataRow(this._gridView.FocusedRowHandle);
      if (dataRow == null)
        return;
      int num = this._dataTable.Rows.IndexOf(dataRow);
      if (num < 0 || num >= this._dataTable.Rows.Count - 1)
        return;
      object[] itemArray = dataRow.ItemArray;
      this._dataTable.Rows.Remove(dataRow);
      this._dataTable.Rows.InsertAt(dataRow, num + 1);
      dataRow.ItemArray = itemArray;
      this._gridView.FocusedRowHandle = num + 1;
      this._gridView.SelectRow(num + 1);
    }
    finally
    {
      this.UnlockUpdateVisualControls();
    }
  }

  /// <summary> Была нажата кнопка "Переместить в конец списка" </summary>
  private void _btnMoveToFinish_Click(object sender, EventArgs e)
  {
    this.LockUpdateVisualControls();
    try
    {
      if (this._gridView.FocusedRowHandle <= -1 || this._gridView.FocusedRowHandle >= this._dataTable.Rows.Count - 1)
        return;
      DataRow dataRow = this._gridView.GetDataRow(this._gridView.FocusedRowHandle);
      if (dataRow == null)
        return;
      object[] itemArray = dataRow.ItemArray;
      this._dataTable.Rows.Remove(dataRow);
      this._dataTable.Rows.InsertAt(dataRow, this._dataTable.Rows.Count);
      dataRow.ItemArray = itemArray;
      this._gridView.FocusedRowHandle = this._dataTable.Rows.Count - 1;
      this._gridView.SelectRow(this._dataTable.Rows.Count - 1);
    }
    finally
    {
      this.UnlockUpdateVisualControls();
    }
  }

  /// <summary> Было выбрано другое исполнение </summary>
  private void _gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
  {
    this.UpdateVisualControlsState();
  }

  /// <summary> Нажата кнопка "удалить" </summary>
  private void _btnDelete_Click(object sender, EventArgs e)
  {
    if (this.avsDocument.IsSpecification && this.avsDocument.productsInfo.Count > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (PDMHelper.Validation3DModelInComposition(sessionKeeper.Session, this.avsDocument.productsInfo[0].Id))
        {
          int num = (int) MessageBox.Show("Невозможно удалить исполнение, так как это изделие создано на основе электронной модели и его изменение должно проводиться через эту модель.", "Удаление исполнения");
          return;
        }
      }
    }
    this.LockUpdateVisualControls();
    try
    {
      if (this._gridView.SelectedRowsCount > 0 && this._gridView.SelectedRowsCount < this._dataTable.Rows.Count)
      {
        if (MessageBox.Show("Удалить выбранные исполнения?", "Удаление исполнений", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
          return;
        List<long> longList = new List<long>();
        int[] selectedRows = this._gridView.GetSelectedRows();
        DataRow[] dataRowArray = new DataRow[selectedRows.Length];
        for (int index = 0; index < selectedRows.Length; ++index)
          dataRowArray[index] = this._gridView.GetDataRow(selectedRows[index]);
        foreach (DataRow dataRow in dataRowArray)
        {
          Guid empty = Guid.Empty;
          if (dataRow[0] is Guid)
            empty = (Guid) dataRow[0];
          if (!this._lockedProducts.ContainsKey(dataRow))
          {
            int focusedRowHandle = this._gridView.FocusedRowHandle;
            ProductInfo productInfo = (ProductInfo) null;
            if (this._newProductToPrototypeIndexDictionary.ContainsKey(dataRow))
              productInfo = this.avsDocument.GetProductInfoByIndex(this._newProductToPrototypeIndexDictionary[dataRow]);
            ProductInfo product = this.GetProduct((object) empty);
            if (this.avsDocument.IsSpecification && product == null && productInfo != null && empty != Guid.Empty)
            {
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                long objectId = sessionKeeper.Session.GetObjectInfo(empty).ObjectID;
                sessionKeeper.Session.GetObject(Math.Abs(objectId), false)?.Delete(0L);
              }
            }
            else if (!this._deletedProducts.Contains(empty))
              this._deletedProducts.Add(empty);
            int index1 = this._createdProducts.IndexOf(dataRow);
            if (index1 != -1)
              this._createdProducts.RemoveAt(index1);
            int index2 = this._renamedProducts.IndexOf(dataRow);
            if (index2 != -1)
              this._renamedProducts.RemoveAt(index2);
            if (productInfo != null)
            {
              DataRow key = (DataRow) null;
              foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
              {
                if (!(row[0] is DBNull) && (Guid) row[0] == productInfo.Guid)
                {
                  key = row;
                  break;
                }
              }
              if (key != null && this._lockedProducts.ContainsKey(key))
              {
                int num = this._lockedProducts[key] - 1;
                if (num <= 0)
                  this._lockedProducts.Remove(key);
                else
                  this._lockedProducts[key] = num;
              }
            }
            this._dataTable.Rows.Remove(dataRow);
            this._gridView.FocusedRowHandle = focusedRowHandle <= this._dataTable.Rows.Count - 1 ? focusedRowHandle : focusedRowHandle - 1;
          }
        }
      }
      else
      {
        int num1 = (int) MessageBox.Show("Невозможно удалить все исполнения", "Удаление исполнения");
      }
    }
    finally
    {
      this.UnlockUpdateVisualControls();
    }
  }

  /// <summary> Нажата кнопка "Добавить" </summary>
  private void _btnAdd_Click(object sender, EventArgs e)
  {
    if (this.avsDocument.IsSpecification && this.avsDocument.productsInfo.Count > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (PDMHelper.Validation3DModelInComposition(sessionKeeper.Session, this.avsDocument.productsInfo[0].Id))
        {
          int num = (int) MessageBox.Show("Невозможно добавить исполнение, так как это изделие создано на основе электронной модели и его изменение должно проводиться через эту модель.", "Добавление исполнения");
          return;
        }
      }
    }
    SelectProductForm selectProductForm = new SelectProductForm(this.avsDocument, this._dataTable, "Выберите исполнение, которое выступит прототипом для создаваемого", true, false);
    if (selectProductForm.ShowDialog() != DialogResult.OK)
      return;
    int selectedProductIndex = selectProductForm.SelectedProductIndex;
    this.LockUpdateVisualControls();
    try
    {
      DataRow dataRow = this._gridView.GetDataRow(this._dataTable.Rows.Count - 1);
      if (dataRow == null)
        return;
      string s = dataRow[2] == null || dataRow[2] is DBNull ? string.Empty : Convert.ToString(dataRow[2]);
      string productDesignation = this.avsDocument.BaseProductDesignation;
      int? nullable1 = new int?();
      if (s.Equals("-"))
      {
        nullable1 = new int?(0);
      }
      else
      {
        int result;
        if (!s.Equals(string.Empty) && int.TryParse(s, out result))
          nullable1 = new int?(result);
      }
      string str1;
      if (nullable1.HasValue)
      {
        int? nullable2 = nullable1;
        nullable1 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + 1) : new int?();
        str1 = !this.avsDocument.UseSameDesignationForProducts ? nullable1.Value.ToString() : nullable1.Value.ToString("d2");
      }
      else
        str1 = string.Empty;
      EditProductCaptionForm productCaptionForm = (EditProductCaptionForm) null;
      ProductDesignationAndNumberDlg designationAndNumberDlg = (ProductDesignationAndNumberDlg) null;
      Form form;
      if (!this.avsDocument.UseSameDesignationForProducts)
      {
        form = (Form) (designationAndNumberDlg = new ProductDesignationAndNumberDlg());
        designationAndNumberDlg.Text = "Создание нового исполнения";
        designationAndNumberDlg.ProductDesignation = productDesignation;
        designationAndNumberDlg.ProductNumber = str1;
      }
      else
      {
        form = (Form) (productCaptionForm = new EditProductCaptionForm());
        productCaptionForm.Text = "Создание нового исполнения";
        productCaptionForm.ProductDesignationBase = productDesignation;
        productCaptionForm.ProductNumber = str1;
      }
      ProductInfo selectedProduct1 = selectProductForm.SelectedProduct;
      if (ImDocumentData.ShowDebugInfo)
      {
        if (selectedProduct1 != null)
          productCaptionForm.Text = $"{productCaptionForm.Text} прототип {selectedProduct1.Designation}";
        else
          productCaptionForm.Text += " без прототипа";
      }
      while (form.ShowDialog() == DialogResult.OK)
      {
        string str2;
        string productNumber;
        if (!this.avsDocument.UseSameDesignationForProducts)
        {
          str2 = designationAndNumberDlg.ProductDesignation;
          productNumber = designationAndNumberDlg.ProductNumber;
        }
        else
        {
          str2 = productCaptionForm.ProductCaption;
          productNumber = productCaptionForm.ProductNumber;
        }
        bool flag = this._dataTable.Select($"CAPTION='{str2}'").Length == 0;
        if (flag)
        {
          DataRow key = (DataRow) null;
          ProductInfo selectedProduct2 = selectProductForm.SelectedProduct;
          if (selectedProduct2 != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
            {
              if (!(row[0] is DBNull) && (Guid) row[0] == selectedProduct2.Guid)
              {
                key = row;
                break;
              }
            }
          }
          if (key != null || selectedProductIndex == -1)
          {
            object obj = productNumber == "" ? (object) DBNull.Value : (object) productNumber;
            this._dataTable.Columns[0].AllowDBNull = true;
            this._dataTable.Rows.Add((object) DBNull.Value, (object) str2, obj);
            this._gridView.FocusedRowHandle = this._dataTable.Rows.Count - 1;
            this._gridView.SelectRow(this._dataTable.Rows.Count - 1);
            this._createdProducts.Add(this._dataTable.Rows[this._gridView.FocusedRowHandle]);
            if (key != null)
            {
              if (!this._lockedProducts.ContainsKey(key))
                this._lockedProducts[key] = 1;
              else
                ++this._lockedProducts[key];
            }
            DataRow row = this._dataTable.Rows[this._gridView.FocusedRowHandle];
            if (!this._newProductToPrototypeIndexDictionary.ContainsKey(row))
              this._newProductToPrototypeIndexDictionary[row] = selectedProductIndex;
          }
        }
        else
        {
          int num = (int) MessageBox.Show($"Исполнение \"{str2}\" уже существует", "Создание исполнения", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        if (flag)
          break;
      }
    }
    finally
    {
      this.UnlockUpdateVisualControls();
    }
  }

  /// <summary> Нажата кнопка "Переименовать" </summary>
  private void _btnRename_Click(object sender, EventArgs e)
  {
    this.LockUpdateVisualControls();
    try
    {
      if (this._gridView.FocusedRowHandle == -1)
        return;
      DataRow dataRow = this._gridView.GetDataRow(this._gridView.FocusedRowHandle);
      if (dataRow == null)
        return;
      string productDesignation = dataRow[1] == null || dataRow[1] is DBNull ? string.Empty : Convert.ToString(dataRow[1]);
      string productNumber = dataRow[2] == null || dataRow[2] is DBNull ? string.Empty : Convert.ToString(dataRow[2]);
      if ((this.avsDocument.UseSameDesignationForProducts ? BaseProductInfoDlg.Execute<EditProductCaptionForm>("Редактирование обозначения", this._dataTable, ref productDesignation, ref productNumber) : BaseProductInfoDlg.Execute<ProductDesignationAndNumberDlg>("Редактирование обозначения и номера исполнения", this._dataTable, ref productDesignation, ref productNumber)) != DialogResult.OK)
        return;
      if (!this._createdProducts.Contains(dataRow) && !this._renamedProducts.Contains(dataRow))
        this._renamedProducts.Add(dataRow);
      object obj = productNumber == "" ? (object) "-" : (object) productNumber;
      dataRow[1] = (object) productDesignation;
      dataRow[2] = obj;
    }
    finally
    {
      this.UnlockUpdateVisualControls();
    }
  }

  /// <summary> Двойной клик по таблице </summary>
  private void _gridView_DoubleClick(object sender, EventArgs e)
  {
    if (!this._gridView.CalcHitInfo(this._gridProducts.PointToClient(Control.MousePosition)).InRow)
      return;
    this._btnRename_Click((object) null, (EventArgs) null);
  }

  private void _gridView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
  {
    e.Handled = false;
    if (e.RowHandle == -1 || e.Column != this.gridColumn1)
      return;
    DataRow dataRow = this._gridView.GetDataRow(e.RowHandle);
    if (dataRow == null)
      return;
    int index1 = -1;
    int index2 = -1;
    int left = e.Bounds.Left;
    if (this._lockedProducts.ContainsKey(dataRow))
      index1 = 0;
    else if (this._createdProducts.Contains(dataRow))
      index2 = 1;
    if (index2 == -1 && this._renamedProducts.Contains(dataRow))
      index2 = 2;
    if (index1 < 0 && index2 < 0)
      return;
    string s = Convert.ToString(dataRow[1]);
    if (index1 != -1)
    {
      this._imageList.Draw(e.Graphics, left + 2, (e.Bounds.Top + e.Bounds.Bottom) / 2 - 8, index1);
      left += 19;
    }
    if (index2 != -1)
    {
      this._imageList.Draw(e.Graphics, left + 2, (e.Bounds.Top + e.Bounds.Bottom) / 2 - 8, index2);
      left += 19;
    }
    RectangleF bounds = (RectangleF) e.Bounds with
    {
      X = (float) left
    };
    bounds.Y += 2f;
    e.Graphics.DrawString(s, e.Style.Font, e.Style.ForeBrush, bounds, new StringFormat(StringFormatFlags.NoWrap));
    e.Handled = true;
  }

  private void _gridView_MouseMove(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.None)
    {
      if (this.toolTipControl == null)
        return;
      this.toolTipControl.Hide();
      this.toolTipControl = (SuperTooltipControl) null;
    }
    else
    {
      GridHitInfo gridHitInfo = this._gridView.CalcHitInfo(e.Location);
      if (gridHitInfo.RowHandle != -1 && gridHitInfo.Column == this.gridColumn1)
      {
        DataRow dataRow = this._gridView.GetDataRow(gridHitInfo.RowHandle);
        if (dataRow != null)
        {
          int num1 = -1;
          int num2 = -1;
          int num3 = 2;
          if (this._lockedProducts.ContainsKey(dataRow))
            num1 = 0;
          else if (this._createdProducts.Contains(dataRow))
            num2 = 1;
          if (num2 == -1 && this._renamedProducts.Contains(dataRow))
            num2 = 2;
          if (num1 >= 0 || num2 >= 0)
          {
            Button button = new Button();
            SuperTooltipInfo info = new SuperTooltipInfo();
            info.FooterVisible = false;
            info.HeaderVisible = false;
            info.Color = TooltipColorScheme.Lemon;
            if (num1 != -1 && e.X > num3 && e.X < num3 + 19)
            {
              info.BodyText = "Исполнение заблокировано";
              num3 += 19;
            }
            if (num2 != -1 && e.X > num3 && e.X < num3 + 19)
            {
              if (num2 == 1)
                info.BodyText = "Новое исполнение";
              if (num2 == 2)
                info.BodyText = "Исполнение изменено";
              num3 += 19;
            }
            if (info.BodyText != "")
            {
              if (this.toolTipControl != null && !(this.toolTipControl.Text != info.BodyText) && this.toolTipControl.Tag == dataRow)
                return;
              if (this.toolTipControl != null && this.toolTipControl.Visible)
                this.toolTipControl.Hide();
              this.toolTipControl = new SuperTooltipControl();
              Point screen = this._gridProducts.PointToScreen(new Point(num3 + 2, e.Y));
              this.toolTipControl.ShowTooltip(info, screen.X, screen.Y, true);
              this.toolTipControl.Tag = (object) dataRow;
              return;
            }
          }
        }
      }
      if (this.toolTipControl == null)
        return;
      this.toolTipControl.Hide();
      this.toolTipControl = (SuperTooltipControl) null;
    }
  }

  private void BAddRangeClick(object sender, EventArgs e)
  {
    if (this.avsDocument.IsSpecification && this.avsDocument.productsInfo.Count > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (PDMHelper.Validation3DModelInComposition(sessionKeeper.Session, this.avsDocument.productsInfo[0].Id))
        {
          int num = (int) MessageBox.Show("Невозможно добавить исполнение, так как это изделие создано на основе электронной модели и его изменение должно проводиться через эту модель.", "Добавление исполнения");
          return;
        }
      }
    }
    this.LockUpdateVisualControls();
    try
    {
      SelectProductForm selectProductForm = new SelectProductForm(this.avsDocument, this._dataTable, "Выберите исполнение, которое выступит прототипом для создаваемого", false, false);
      if (selectProductForm.ShowDialog() != DialogResult.OK)
        return;
      int selectedProductIndex = selectProductForm.SelectedProductIndex;
      ProductInfo selectedProduct = selectProductForm.SelectedProduct;
      IInstancesClientService instancesClientService = (IInstancesClientService) null;
      if (this.avsDocument.IsSpecification && selectedProduct != null)
        instancesClientService = ServicesManager.GetService(typeof (IInstancesClientService)) as IInstancesClientService;
      if (instancesClientService != null)
      {
        long[] instances = instancesClientService.CreateInstances(selectedProduct.Id, this.avsDocument.DocFID);
        List<ProductInfo> productInfoList = (List<ProductInfo>) null;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          productInfoList = AVSDocument.LoadProductsForSpecification(new List<long>((IEnumerable<long>) instances), (List<int>) null, true, this.avsDocument.FiltrationOwnerID, sessionKeeper.Session);
        object[] objArray = new object[3];
        int currentNumber = -1;
        DataRow key1 = (DataRow) null;
        foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
        {
          if (!(row[0] is DBNull) && (Guid) row[0] == selectedProduct.Guid)
          {
            key1 = row;
            break;
          }
        }
        if (key1 == null && selectedProductIndex != -1)
          return;
        this._dataTable.Columns[0].AllowDBNull = true;
        for (int index = 0; index < productInfoList.Count; ++index)
        {
          objArray[0] = (object) productInfoList[index].Guid;
          objArray[1] = (object) productInfoList[index].Designation;
          objArray[2] = (object) productInfoList[index].GetNumber(currentNumber, out currentNumber, this.avsDocument.BaseProductDesignation, this.avsDocument.UseSameDesignationForProducts);
          DataRow key2 = this._dataTable.Rows.Add(objArray);
          this._createdProducts.Add(key2);
          if (!this._newProductToPrototypeIndexDictionary.ContainsKey(key2))
            this._newProductToPrototypeIndexDictionary[key2] = selectedProductIndex;
          if (key1 != null)
          {
            if (!this._lockedProducts.ContainsKey(key1))
              this._lockedProducts[key1] = 1;
            else
              this._lockedProducts[key1]++;
          }
        }
        this._gridView.FocusedRowHandle = this._dataTable.Rows.Count - 1;
        this._gridView.SelectRow(this._dataTable.Rows.Count - 1);
      }
      else
      {
        DataRow dataRow = this._gridView.GetDataRow(this._dataTable.Rows.Count - 1);
        if (dataRow == null)
          return;
        string str1 = dataRow[1] == null || dataRow[1] is DBNull ? string.Empty : Convert.ToString(dataRow[1]);
        string s = dataRow[2] == null || dataRow[2] is DBNull ? string.Empty : Convert.ToString(dataRow[2]);
        string productDesignation = this.avsDocument.BaseProductDesignation;
        int? nullable1 = new int?();
        int? nullable2 = new int?();
        if (s.Equals("-"))
        {
          nullable1 = new int?(0);
        }
        else
        {
          int result;
          if (!s.Equals(string.Empty) && int.TryParse(s, out result))
            nullable1 = new int?(result);
        }
        string empty1 = string.Empty;
        int num;
        string empty2;
        if (nullable1.HasValue)
        {
          int? nullable3 = nullable1;
          nullable1 = nullable3.HasValue ? new int?(nullable3.GetValueOrDefault() + 1) : new int?();
          if (this.avsDocument.UseSameDesignationForProducts)
          {
            num = nullable1.Value;
            empty2 = num.ToString("d2");
            num = nullable1.Value + 1;
            empty1 = num.ToString("d2");
          }
          else
          {
            num = nullable1.Value;
            empty2 = num.ToString();
            num = nullable1.Value + 1;
            empty1 = num.ToString();
          }
        }
        else
          empty2 = string.Empty;
        CreateMultiplyProductsForm multiplyProductsForm1;
        CreateMultiplyProductsForm multiplyProductsForm2 = multiplyProductsForm1 = new CreateMultiplyProductsForm();
        str1 = $"{productDesignation}-{empty2}";
        multiplyProductsForm2.Text = "Создание новых исполнений";
        multiplyProductsForm2.ProductBaseCaption = productDesignation;
        multiplyProductsForm2.ProductNumber = empty2;
        multiplyProductsForm2.EndProductNumber = empty1;
        if (multiplyProductsForm1.ShowDialog() != DialogResult.OK)
          return;
        string productBaseCaption = multiplyProductsForm2.ProductBaseCaption;
        string str2 = multiplyProductsForm2.ProductNumber.Trim();
        string endProductNumber = multiplyProductsForm2.EndProductNumber;
        nullable1 = new int?(multiplyProductsForm2.ProductNumberValue);
        nullable2 = new int?(multiplyProductsForm2.EndProductNumberValue);
        string format = "d" + (object) str2.Length;
        DataRow key3 = (DataRow) null;
        if (selectedProduct != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
          {
            if (!(row[0] is DBNull) && (Guid) row[0] == selectedProduct.Guid)
            {
              key3 = row;
              break;
            }
          }
        }
        if (key3 == null && selectedProductIndex != -1)
          return;
        this._dataTable.Columns[0].AllowDBNull = true;
        for (int index = nullable1.Value; index <= nullable2.Value; ++index)
        {
          string startNumber;
          if (index < nullable2.Value)
          {
            num = index;
            startNumber = num.ToString(format);
          }
          else
            startNumber = endProductNumber;
          object obj = startNumber == "" ? (object) DBNull.Value : (object) startNumber;
          string designation = multiplyProductsForm2.GetDesignation(productBaseCaption, startNumber);
          DataRow key4 = this._dataTable.Rows.Add((object) DBNull.Value, (object) designation, obj);
          this._createdProducts.Add(key4);
          if (!this._newProductToPrototypeIndexDictionary.ContainsKey(key4))
            this._newProductToPrototypeIndexDictionary[key4] = selectedProductIndex;
          if (key3 != null)
          {
            if (!this._lockedProducts.ContainsKey(key3))
              this._lockedProducts[key3] = 1;
            else
              num = this._lockedProducts[key3]++;
          }
        }
        this._gridView.FocusedRowHandle = this._dataTable.Rows.Count - 1;
        this._gridView.SelectRow(this._dataTable.Rows.Count - 1);
      }
    }
    finally
    {
      this.UnlockUpdateVisualControls();
    }
  }

  private void _btnSort_Click(object sender, EventArgs e)
  {
    List<ProductInfo> productInfoList = new List<ProductInfo>();
    int num = 0;
    foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
    {
      if (row[1] != null && !(row[1] is DBNull))
      {
        ProductInfo productInfo;
        productInfoList.Add(productInfo = new ProductInfo(Guid.Empty, -1L, this.avsDocument.DocumentName));
        productInfo.Designation = Convert.ToString(row[1]);
        if (!this.avsDocument.UseSameDesignationForProducts)
          productInfo.Number = Convert.ToString(row[2]);
        productInfo.Tag = (object) row;
      }
      ++num;
    }
    if (productInfoList.Count > 1)
    {
      if (!this.avsDocument.UseSameDesignationForProducts)
      {
        productInfoList.Sort((IComparer<ProductInfo>) new AutoPromProductsComparer(this.avsDocument));
      }
      else
      {
        productInfoList.Sort((IComparer<ProductInfo>) new ProductsComparer(this.avsDocument));
        if (this.avsDocument.DocumentDesignation == "" || this.avsDocument.DocumentDesignation == null)
        {
          this.avsDocument.DocumentDesignation = productInfoList[0].Designation;
          productInfoList.Sort((IComparer<ProductInfo>) new ProductsComparer(this.avsDocument));
        }
      }
    }
    this.LockUpdateVisualControls();
    try
    {
      for (int index1 = 0; index1 < productInfoList.Count; ++index1)
      {
        DataRow tag = (DataRow) productInfoList[index1].Tag;
        int index2 = this._dataTable.Rows.IndexOf(tag);
        if (index2 != -1 && index2 != index1)
        {
          object[] itemArray = tag.ItemArray;
          this._dataTable.Rows.RemoveAt(index2);
          this._dataTable.Rows.InsertAt(tag, index1);
          tag.ItemArray = itemArray;
        }
      }
    }
    finally
    {
      this.UnlockUpdateVisualControls();
    }
  }

  private void _btnOK_Click(object sender, EventArgs e)
  {
    this.Save();
    this.DialogResult = DialogResult.OK;
  }

  private void _btnCancel_Click(object sender, EventArgs e)
  {
    if (!this.avsDocument.IsSpecification)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (DataRow row in (InternalDataCollectionBase) this._dataTable.Rows)
      {
        int num = -1;
        if (this._newProductToPrototypeIndexDictionary.ContainsKey(row))
          num = this._newProductToPrototypeIndexDictionary[row];
        if (num != -1)
        {
          Guid empty = Guid.Empty;
          if (row[0] != null && row[0] is Guid)
            empty = (Guid) row[0];
          if (this.GetProduct((object) empty) == null && empty != Guid.Empty)
          {
            long objectId = sessionKeeper.Session.GetObjectInfo(empty).ObjectID;
            sessionKeeper.Session.GetObject(Math.Abs(objectId), false)?.Delete(0L);
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProductsListDialog));
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this._dataSet = new DataSet();
    this._gridProducts = new GridControl();
    this._gridView = new GridView();
    this.gridColumn1 = new GridColumn();
    this.gridColumn2 = new GridColumn();
    this.label1 = new Label();
    this._btnAdd = new Button();
    this._btnRename = new Button();
    this._btnDelete = new Button();
    this.panel1 = new Panel();
    this._btnSort = new Button();
    this._btnMoveToFinish = new Button();
    this._btnMoveToStart = new Button();
    this._btnMoveDown = new Button();
    this._btnMoveUp = new Button();
    this.bevel1 = new Bevel();
    this._superTooltip = new SuperTooltip();
    this.bAddRange = new Button();
    this._imageList = new ImageList(this.components);
    this._dataSet.BeginInit();
    this._gridProducts.BeginInit();
    this._gridView.BeginInit();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    this._btnCancel.Click += new EventHandler(this._btnCancel_Click);
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    this._btnOK.Click += new EventHandler(this._btnOK_Click);
    this._dataSet.DataSetName = "NewDataSet";
    componentResourceManager.ApplyResources((object) this._gridProducts, "_gridProducts");
    this._gridProducts.DataSource = (object) this._dataSet;
    this._gridProducts.EmbeddedNavigator.Name = "";
    this._gridProducts.MainView = (BaseView) this._gridView;
    this._gridProducts.Name = "_gridProducts";
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
    this._gridView.OptionsSelection.MultiSelect = true;
    this._gridView.OptionsView.ShowFilterPanel = false;
    this._gridView.OptionsView.ShowGroupPanel = false;
    this._gridView.OptionsView.ShowHorzLines = false;
    this._gridView.OptionsView.ShowIndicator = false;
    this._gridView.OptionsView.ShowVertLines = false;
    this._gridView.CustomDrawCell += new RowCellCustomDrawEventHandler(this._gridView_CustomDrawCell);
    this._gridView.FocusedRowChanged += new FocusedRowChangedEventHandler(this._gridView_FocusedRowChanged);
    this._gridView.MouseMove += new MouseEventHandler(this._gridView_MouseMove);
    this._gridView.DoubleClick += new EventHandler(this._gridView_DoubleClick);
    componentResourceManager.ApplyResources((object) this.gridColumn1, "gridColumn1");
    this.gridColumn1.Name = "gridColumn1";
    this.gridColumn1.Options = ColumnOptions.CanMoved | ColumnOptions.CanFocused | ColumnOptions.ShowInCustomizationForm;
    this.gridColumn1.VisibleIndex = 0;
    this.gridColumn1.Width = 373;
    componentResourceManager.ApplyResources((object) this.gridColumn2, "gridColumn2");
    this.gridColumn2.Name = "gridColumn2";
    this.gridColumn2.Options = ColumnOptions.CanResized | ColumnOptions.CanFocused | ColumnOptions.ShowInCustomizationForm;
    this.gridColumn2.StyleName = "Style1";
    this.gridColumn2.VisibleIndex = 1;
    this.gridColumn2.Width = 120;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this._btnAdd, "_btnAdd");
    this._btnAdd.Name = "_btnAdd";
    this._superTooltip.SetSuperTooltip((IComponent) this._btnAdd, new SuperTooltipInfo("Добавить", "", "Создать новое исполнение специфицируемого изделия", (Image) null, (Image) null, TooltipColorScheme.Lemon, true, false, new Size(0, 0)));
    this._btnAdd.UseVisualStyleBackColor = true;
    this._btnAdd.Click += new EventHandler(this._btnAdd_Click);
    componentResourceManager.ApplyResources((object) this._btnRename, "_btnRename");
    this._btnRename.Name = "_btnRename";
    this._superTooltip.SetSuperTooltip((IComponent) this._btnRename, new SuperTooltipInfo("Переименовать", "", "Переименовать выбранное исполнение специфицируемого изделия", (Image) null, (Image) null, TooltipColorScheme.Lemon, true, false, new Size(0, 0)));
    this._btnRename.UseVisualStyleBackColor = true;
    this._btnRename.Click += new EventHandler(this._btnRename_Click);
    componentResourceManager.ApplyResources((object) this._btnDelete, "_btnDelete");
    this._btnDelete.Name = "_btnDelete";
    this._superTooltip.SetSuperTooltip((IComponent) this._btnDelete, new SuperTooltipInfo("Удалить", "", "Удалить выбранное исполнение специфицируемого изделия", (Image) null, (Image) null, TooltipColorScheme.Lemon, true, false, new Size(0, 0)));
    this._btnDelete.UseVisualStyleBackColor = true;
    this._btnDelete.Click += new EventHandler(this._btnDelete_Click);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((Control) this._btnSort);
    this.panel1.Controls.Add((Control) this._btnMoveToFinish);
    this.panel1.Controls.Add((Control) this._btnMoveToStart);
    this.panel1.Controls.Add((Control) this._gridProducts);
    this.panel1.Controls.Add((Control) this._btnMoveDown);
    this.panel1.Controls.Add((Control) this._btnMoveUp);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this._btnSort, "_btnSort");
    this._btnSort.Image = (Image) Resources.SortAscend;
    this._btnSort.Name = "_btnSort";
    this._superTooltip.SetSuperTooltip((IComponent) this._btnSort, new SuperTooltipInfo("Сортировать", "", "Сортировать все исполнения в списке", (Image) Resources.SortAscend, (Image) null, TooltipColorScheme.Lemon));
    this._btnSort.Click += new EventHandler(this._btnSort_Click);
    componentResourceManager.ApplyResources((object) this._btnMoveToFinish, "_btnMoveToFinish");
    this._btnMoveToFinish.Name = "_btnMoveToFinish";
    this._superTooltip.SetSuperTooltip((IComponent) this._btnMoveToFinish, new SuperTooltipInfo("Переместить в конец списка", "", "Переместить выбранное исполнение в конец списка", (Image) componentResourceManager.GetObject("_btnMoveToFinish.SuperTooltip"), (Image) null, TooltipColorScheme.Lemon, true, false, new Size(0, 0)));
    this._btnMoveToFinish.Click += new EventHandler(this._btnMoveToFinish_Click);
    componentResourceManager.ApplyResources((object) this._btnMoveToStart, "_btnMoveToStart");
    this._btnMoveToStart.Name = "_btnMoveToStart";
    this._superTooltip.SetSuperTooltip((IComponent) this._btnMoveToStart, new SuperTooltipInfo("Переместить в начало списка", "ctrl + Home", "Переместить выбранное исполнение в начало списка", (Image) componentResourceManager.GetObject("_btnMoveToStart.SuperTooltip"), (Image) null, TooltipColorScheme.Lemon));
    this._btnMoveToStart.Click += new EventHandler(this._btnMoveToStart_Click);
    componentResourceManager.ApplyResources((object) this._btnMoveDown, "_btnMoveDown");
    this._btnMoveDown.Name = "_btnMoveDown";
    this._superTooltip.SetSuperTooltip((IComponent) this._btnMoveDown, new SuperTooltipInfo("Переместить вниз", "", "Переместить выбранное исполнение вниз в списке", (Image) componentResourceManager.GetObject("_btnMoveDown.SuperTooltip"), (Image) null, TooltipColorScheme.Lemon, true, false, new Size(0, 0)));
    this._btnMoveDown.Click += new EventHandler(this._btnMoveDown_Click);
    componentResourceManager.ApplyResources((object) this._btnMoveUp, "_btnMoveUp");
    this._btnMoveUp.Name = "_btnMoveUp";
    this._superTooltip.SetSuperTooltip((IComponent) this._btnMoveUp, new SuperTooltipInfo("Переместить вверх", "", "Переместить выбранное исполнение вверх в списке", (Image) componentResourceManager.GetObject("_btnMoveUp.SuperTooltip"), (Image) null, TooltipColorScheme.Lemon, true, false, new Size(0, 0)));
    this._btnMoveUp.Click += new EventHandler(this._btnMoveUp_Click);
    componentResourceManager.ApplyResources((object) this.bevel1, "bevel1");
    this.bevel1.Name = "bevel1";
    this._superTooltip.DefaultFont = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    componentResourceManager.ApplyResources((object) this.bAddRange, "bAddRange");
    this.bAddRange.Name = "bAddRange";
    this._superTooltip.SetSuperTooltip((IComponent) this.bAddRange, new SuperTooltipInfo("Добавить список", "", "Добавить список исполнений", (Image) null, (Image) null, TooltipColorScheme.Lemon));
    this.bAddRange.UseVisualStyleBackColor = true;
    this.bAddRange.Click += new EventHandler(this.BAddRangeClick);
    this._imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imageList.ImageStream");
    this._imageList.TransparentColor = Color.Transparent;
    this._imageList.Images.SetKeyName(0, "Lock.png");
    this._imageList.Images.SetKeyName(1, "new- style 2.gif");
    this._imageList.Images.SetKeyName(2, "edit.gif");
    this.AcceptButton = (IButtonControl) this._btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this.bevel1);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this._btnDelete);
    this.Controls.Add((Control) this._btnRename);
    this.Controls.Add((Control) this.bAddRange);
    this.Controls.Add((Control) this._btnAdd);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this._btnCancel);
    this.Controls.Add((Control) this._btnOK);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ProductsListDialog);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this._dataSet.EndInit();
    this._gridProducts.EndInit();
    this._gridView.EndInit();
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
