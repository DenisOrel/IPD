// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SelectProductForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Columns;
using DevExpress.IM.XtraGrid.Views.Base;
using DevExpress.IM.XtraGrid.Views.Grid;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.UI;
using MWCommon;
using MWControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Форма выбора исполнения </summary>
public class SelectProductForm : Form
{
  private AVSDocument _avsDocument;
  private int _lockUpdateVisualControlsCounter;
  private DataTable _dataTable;
  private DataTable _dtProducts;
  private bool _showCommonData;
  private bool _showWithoutPrototype;
  private List<ProductInfo> products;
  /// <summary>Запрет на отображение чек-бокса "Показать все исполнения". Если задан не пустой список исполнений
  /// (свойство Articles) и данное поле установлено в True, то в окне можно будет выбрать только одно
  /// из указанных исполнений
  /// </summary>
  private bool _disableShowAllArticles = true;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnCancel;
  private Button _btnOK;
  private Bevel bevel1;
  private GridControl _gridProducts;
  private GridView _gridView;
  private GridColumn gridColumn1;
  private GridColumn gridColumn2;
  private Label label1;
  private MWLabel mwLabel1;
  private Bevel bevel2;
  private DataSet _dataSet;
  private CheckBox cbShowAll;

  /// <summary>Создать заполненную форму по выбору исполнения</summary>
  /// <param name="avsDocument">Спецификация</param>
  /// <param name="products">Список исполнений для выбора</param>
  /// <param name="selectTemplateProduct">Выбор шаблона</param>
  public SelectProductForm(
    AVSDocument avsDocument,
    List<ProductInfo> products,
    string dlgCaption,
    bool showWithoutPrototype,
    bool showCommonData)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1516);
    this.mwLabel1.Text = dlgCaption;
    this.AcceptButton = (IButtonControl) this._btnOK;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this._avsDocument = avsDocument;
    this._showCommonData = showCommonData;
    this._showWithoutPrototype = showWithoutPrototype;
    this.products = products;
    this.LoadArticles();
  }

  /// <summary>Создать заполненную форму по выбору исполнения</summary>
  /// <param name="avsDocument">Спецификация</param>
  /// <param name="products">Список исполнений для выбора</param>
  /// <param name="selectTemplateProduct">Выбор шаблона</param>
  public SelectProductForm(
    AVSDocument avsDocument,
    DataTable dtProducts,
    string dlgCaption,
    bool showWithoutPrototype,
    bool showCommonData)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1516);
    this.mwLabel1.Text = dlgCaption;
    this.AcceptButton = (IButtonControl) this._btnOK;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this._avsDocument = avsDocument;
    this._showCommonData = showCommonData;
    this._showWithoutPrototype = showWithoutPrototype;
    this._dtProducts = dtProducts;
    this.LoadArticles();
  }

  /// <summary>Создать заполненную форму по выбору исполнения</summary>
  /// <param name="avsDocument">Спецификация</param>
  /// <param name="products">Список исполнений для выбора</param>
  /// <param name="selectTemplateProduct">Выбор шаблона</param>
  /// <param name="disableShowAllArticles">Запрет на отображение чек-бокса "Показать все исполнения".
  /// Если задан не пустой список исполнений (параметр articles) и данное поле установлено в True,
  /// то в окне можно будет выбрать только одно из указанных исполнений</param>
  public SelectProductForm(
    AVSDocument avsDocument,
    List<ProductInfo> products,
    bool selectTemplateProduct,
    bool disableShowAllArticles)
  {
    this.InitializeComponent();
    this._avsDocument = avsDocument;
    this.products = products;
    this._disableShowAllArticles = disableShowAllArticles;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1516);
    if (selectTemplateProduct)
      this.mwLabel1.Text = "Выберите исполнение, которое выступит прототипом для создаваемого";
    else
      this.mwLabel1.Text = "Выберите исполнение";
    this._showWithoutPrototype = selectTemplateProduct;
    this._showCommonData = !selectTemplateProduct;
    this.AcceptButton = (IButtonControl) this._btnOK;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.LoadArticles();
  }

  /// <summary>Список исполнений для выбора</summary>
  public List<ProductInfo> Products
  {
    [DebuggerStepThrough] get => this.products;
    set => this.products = value;
  }

  /// <summary>Запрет на отображение чек-бокса "Показать все исполнения". Если задан не пустой список исполнений
  /// (свойство Articles) и данное поле установлено в True, то в окне можно будет выбрать только одно
  /// из указанных исполнений
  /// </summary>
  public bool DisableShowAllArticles
  {
    get => this._disableShowAllArticles;
    set => this._disableShowAllArticles = value;
  }

  /// <summary>Индекс выбраного исполнения в заданном списке исполнений. -1 - Нет прототипа, -2 - Общие данные</summary>
  public int SelectedProductIndex
  {
    get
    {
      if (this._gridView.FocusedRowHandle == -1)
        return -1;
      DataRow dataRow = this._gridView.GetDataRow(this._gridView.FocusedRowHandle);
      return dataRow != null ? Convert.ToInt32(dataRow[0]) : -1;
    }
  }

  public long SelectedProductId
  {
    [DebuggerStepThrough] get
    {
      if (this.SelectedProductIndex < 0)
        return -1;
      ProductInfo selectedProduct = this.SelectedProduct;
      return selectedProduct == null ? -1L : selectedProduct.Id;
    }
  }

  public ProductInfo SelectedProduct
  {
    [DebuggerStepThrough] get
    {
      if (this.SelectedProductIndex >= 0)
      {
        if (this.Products != null)
          return this.Products[this.SelectedProductIndex];
        if (this._dtProducts == null)
          return (ProductInfo) null;
        return this._avsDocument?.GetProductInfoByObjectGuid((Guid) this._dtProducts.Rows[this.SelectedProductIndex][0]);
      }
      if (this.SelectedProductIndex != -2)
        return (ProductInfo) null;
      if (this._avsDocument.commonDataChapter.Product == null)
        this._avsDocument.commonDataChapter.Product = new ProductInfo(AVSDocument.ChapterCommonDataGuid, -1L, "Общие данные");
      return this._avsDocument.commonDataChapter.Product;
    }
  }

  /// <summary>Загрузить исполнения в таблицу</summary>
  private void LoadArticles()
  {
    this.LockUpdateVisualControls();
    try
    {
      this.InitProductsList();
    }
    finally
    {
      this.UnlockUpdateVisualControls();
    }
  }

  /// <summary> Обновить состояние визуальных котролов  </summary>
  private void UpdateVisualControlsState()
  {
    if (this._lockUpdateVisualControlsCounter != 0)
      return;
    ++this._lockUpdateVisualControlsCounter;
    try
    {
      this._btnOK.Enabled = this._gridView.FocusedRowHandle != -1 && this._gridView.GetDataRow(this._gridView.FocusedRowHandle) != null;
      this.cbShowAll.Visible = !this.DisableShowAllArticles;
      this.cbShowAll.Enabled = this.cbShowAll.Visible;
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

  /// <summary>Заполнение визуального списка исполнений специфицируемого изделия</summary>
  private void InitProductsList()
  {
    this.LockUpdateVisualControls();
    try
    {
      this._gridProducts.BeginUpdate();
      this._gridProducts.MainView.BeginUpdate();
      try
      {
        this._gridProducts.DataSource = (object) null;
        if (this._avsDocument == null || this._avsDocument.AvsDocumentForm == AVSDocumentForm.Single || this._avsDocument.productsInfo.Count == 0)
          return;
        this._dataTable = new DataTable("TABLE");
        this._dataTable.Columns.Add("OBJECT_ID", typeof (long));
        this._dataTable.Columns.Add("CAPTION", typeof (string));
        this._dataTable.Columns.Add("NUMBER", typeof (string));
        object[] objArray = new object[3];
        if (this._showWithoutPrototype)
          this._dataTable.Rows.Add((object) -1, (object) "[Без прототипа]", (object) "");
        if (this._showCommonData && (this._avsDocument.AvsDocumentForm == AVSDocumentForm.A || this._avsDocument.AvsDocumentForm == AVSDocumentForm.V))
          this._dataTable.Rows.Add((object) -2, (object) "Общие данные", (object) "");
        if (this._dtProducts != null && this._dtProducts.Rows.Count > 0)
        {
          int num = -1;
          for (int index = 0; index < this._dtProducts.Rows.Count; ++index)
          {
            ++num;
            DataRow row = this._dtProducts.Rows[index];
            if (row[0] != DBNull.Value)
            {
              objArray[0] = (object) num;
              objArray[1] = row[1];
              objArray[2] = row[2];
              this._dataTable.Rows.Add(objArray);
            }
          }
        }
        else if (this.products != null && this.products.Count > 0)
        {
          int currentNumber = -1;
          for (int index = 0; index < this.products.Count; ++index)
          {
            objArray[0] = (object) index;
            objArray[1] = (object) this.products[index].Designation;
            objArray[2] = (object) this.products[index].GetNumber(currentNumber, out currentNumber, this._avsDocument.BaseProductDesignation, this._avsDocument.UseSameDesignationForProducts);
            this._dataTable.Rows.Add(objArray);
          }
        }
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

  /// <summary> Было выбрано другое исполнение </summary>
  private void _gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
  {
    this.UpdateVisualControlsState();
  }

  /// <summary> Двойной клик по таблице </summary>
  private void _gridProducts_DoubleClick(object sender, EventArgs e)
  {
    if (!this._gridView.CalcHitInfo(this._gridProducts.PointToClient(Control.MousePosition)).InRow)
      return;
    this.DialogResult = DialogResult.OK;
    this.Close();
    this.DialogResult = DialogResult.OK;
  }

  /// <summary>Изменено состояние чек-бокса "Показать все исполнения"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoShowAllArticles(object sender, EventArgs e) => this.LoadArticles();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectProductForm));
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this.bevel1 = new Bevel();
    this._gridProducts = new GridControl();
    this._gridView = new GridView();
    this.gridColumn1 = new GridColumn();
    this.gridColumn2 = new GridColumn();
    this.label1 = new Label();
    this.mwLabel1 = new MWLabel();
    this.bevel2 = new Bevel();
    this._dataSet = new DataSet();
    this.cbShowAll = new CheckBox();
    this._gridProducts.BeginInit();
    this._gridView.BeginInit();
    this._dataSet.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.bevel1, "bevel1");
    this.bevel1.Name = "bevel1";
    componentResourceManager.ApplyResources((object) this._gridProducts, "_gridProducts");
    this._gridProducts.EmbeddedNavigator.Name = "";
    this._gridProducts.MainView = (BaseView) this._gridView;
    this._gridProducts.Name = "_gridProducts";
    this._gridProducts.Styles.AddReplace("Style1", (object) new ViewStyleEx("Style1", "", "", true, false, false, HorzAlignment.Center, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.WindowText, Color.Empty, LinearGradientMode.Horizontal));
    this._gridProducts.Styles.AddReplace("HideSelectionRow", (object) new ViewStyleEx("HideSelectionRow", "Grid", "FocusedRow", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.MidnightBlue, Color.White, Color.DodgerBlue, LinearGradientMode.Horizontal));
    this._gridProducts.Styles.AddReplace("FocusedRow", (object) new ViewStyleEx("FocusedRow", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, Color.MidnightBlue, Color.White, Color.DodgerBlue, LinearGradientMode.Horizontal));
    this._gridProducts.Styles.AddReplace("FocusedCell", (object) new ViewStyleEx("FocusedCell", "Grid", "FocusedRow", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.MidnightBlue, Color.White, Color.DodgerBlue, LinearGradientMode.Horizontal));
    this._gridProducts.Styles.AddReplace("SelectedRow", (object) new ViewStyleEx("SelectedRow", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, Color.FromArgb(182, 182, (int) byte.MaxValue), SystemColors.HighlightText, Color.FromArgb(210, 210, (int) byte.MaxValue), LinearGradientMode.Horizontal));
    this._gridProducts.DoubleClick += new EventHandler(this._gridProducts_DoubleClick);
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
    this._gridView.OptionsView.ShowFilterPanel = false;
    this._gridView.OptionsView.ShowGroupPanel = false;
    this._gridView.OptionsView.ShowHorzLines = false;
    this._gridView.OptionsView.ShowIndicator = false;
    this._gridView.OptionsView.ShowVertLines = false;
    this._gridView.FocusedRowChanged += new FocusedRowChangedEventHandler(this._gridView_FocusedRowChanged);
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
    this.mwLabel1.BackColor = SystemColors.Info;
    componentResourceManager.ApplyResources((object) this.mwLabel1, "mwLabel1");
    this.mwLabel1.FlatStyle = FlatStyle.Flat;
    this.mwLabel1.Name = "mwLabel1";
    this.mwLabel1.StringFrmt = StringFormatEnum.GenericTypographic;
    componentResourceManager.ApplyResources((object) this.bevel2, "bevel2");
    this.bevel2.Name = "bevel2";
    this._dataSet.DataSetName = "NewDataSet";
    componentResourceManager.ApplyResources((object) this.cbShowAll, "cbShowAll");
    this.cbShowAll.Name = "cbShowAll";
    this.cbShowAll.UseVisualStyleBackColor = true;
    this.cbShowAll.CheckedChanged += new EventHandler(this.DoShowAllArticles);
    this.AcceptButton = (IButtonControl) this._btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this.cbShowAll);
    this.Controls.Add((Control) this.bevel2);
    this.Controls.Add((Control) this.mwLabel1);
    this.Controls.Add((Control) this._gridProducts);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.bevel1);
    this.Controls.Add((Control) this._btnCancel);
    this.Controls.Add((Control) this._btnOK);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelectProductForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this._gridProducts.EndInit();
    this._gridView.EndInit();
    this._dataSet.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
