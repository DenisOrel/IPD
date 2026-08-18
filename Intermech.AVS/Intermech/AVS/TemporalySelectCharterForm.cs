// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.TemporalySelectCharterForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces.AVS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

public class TemporalySelectCharterForm : Form
{
  private AVSDocument _avsDocument;
  private Dictionary<CheckedListBoxItem, ProductInfo> _productItemToProductIndexHash;
  private bool _checkIsLocked;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnCancel;
  private Button _btnOK;
  private Label label1;
  private CheckedListBoxControl _listBoxParts;
  private Label label2;
  private CheckedListBoxControl _listBoxSpecificationProducts;
  private Button _buttonUcheckAllProducts;
  private Button _buttonCheckAllProducts;
  private Button _btnUncheckAllParts;
  private Button _btnCheckAllParts;

  public TemporalySelectCharterForm() => this.InitializeComponent();

  public TemporalySelectCharterForm(AVSDocument avsDocument)
    : this()
  {
    this._avsDocument = avsDocument;
    if (this._avsDocument.AvsDocumentForm == AVSDocumentForm.Single)
    {
      this._listBoxSpecificationProducts.Items.Clear();
      this._listBoxSpecificationProducts.Enabled = false;
    }
    else
    {
      this._listBoxSpecificationProducts.Items.BeginUpdate();
      try
      {
        this._listBoxSpecificationProducts.Items.Clear();
        this._productItemToProductIndexHash = new Dictionary<CheckedListBoxItem, ProductInfo>();
        if (this._avsDocument.IsFormB)
        {
          CheckedListBoxItem key = new CheckedListBoxItem((object) "Переменные данные");
          this._listBoxSpecificationProducts.Items.Add((object) key);
          this._productItemToProductIndexHash[key] = this._avsDocument.commonDataChapter.Product;
        }
        else if (this._avsDocument.AvsDocumentForm == AVSDocumentForm.V)
        {
          CheckedListBoxItem key1 = new CheckedListBoxItem((object) "Общие данные");
          this._listBoxSpecificationProducts.Items.Add((object) key1);
          this._productItemToProductIndexHash[key1] = this._avsDocument.commonDataChapter.Product;
          CheckedListBoxItem key2 = new CheckedListBoxItem((object) "Переменные данные");
          this._listBoxSpecificationProducts.Items.Add((object) key2);
          this._productItemToProductIndexHash[key2] = this._avsDocument.variableDataChapter_FormV.Product;
        }
        else if (this._avsDocument.AvsDocumentForm == AVSDocumentForm.A)
        {
          CheckedListBoxItem key3 = new CheckedListBoxItem((object) "Общие данные");
          this._listBoxSpecificationProducts.Items.Add((object) key3);
          this._productItemToProductIndexHash[key3] = this._avsDocument.commonDataChapter.Product;
          for (int index = 0; index < this._avsDocument.productsInfo.Count; ++index)
          {
            CheckedListBoxItem key4 = new CheckedListBoxItem((object) this._avsDocument.productsInfo[index].Designation);
            this._productItemToProductIndexHash[key4] = this._avsDocument.productsInfo[index];
            this._listBoxSpecificationProducts.Items.Add((object) key4);
          }
        }
        if (this._listBoxSpecificationProducts.Items.Count > 0)
          this._listBoxSpecificationProducts.Items[0].CheckState = CheckState.Checked;
      }
      finally
      {
        this._listBoxSpecificationProducts.Items.EndUpdate();
      }
      this._listBoxSpecificationProducts.Enabled = this._listBoxSpecificationProducts.Items.Count > 1;
      this._buttonCheckAllProducts.Enabled = this._buttonUcheckAllProducts.Enabled = avsDocument.IsFormA && this._listBoxSpecificationProducts.Items.Count > 2;
    }
  }

  private CheckedListBoxItem CommonDataItem
  {
    get
    {
      foreach (CheckedListBoxItem key in (CollectionBase) this._listBoxSpecificationProducts.Items)
      {
        ProductInfo productInfo;
        if (this._productItemToProductIndexHash.TryGetValue(key, out productInfo) && productInfo != null && productInfo.IsCommonData)
          return key;
      }
      return (CheckedListBoxItem) null;
    }
  }

  private void UpdateEnabled()
  {
    bool flag1 = true;
    bool flag2 = false;
    if (this._listBoxSpecificationProducts.Enabled)
    {
      foreach (CheckedListBoxItem checkedListBoxItem in (CollectionBase) this._listBoxSpecificationProducts.Items)
      {
        if (checkedListBoxItem.CheckState == CheckState.Checked)
        {
          flag2 = true;
          break;
        }
      }
      flag1 = flag2;
    }
    if (flag1 && this._listBoxParts.Enabled)
    {
      foreach (CheckedListBoxItem checkedListBoxItem in (CollectionBase) this._listBoxParts.Items)
      {
        if (checkedListBoxItem.CheckState == CheckState.Checked)
        {
          flag2 = true;
          break;
        }
      }
      flag1 = flag2;
    }
    this._btnOK.Enabled = flag1;
  }

  public List<ProductInfo> GetSelectedProducts()
  {
    List<ProductInfo> selectedProducts = new List<ProductInfo>();
    foreach (CheckedListBoxItem key in (CollectionBase) this._listBoxSpecificationProducts.Items)
    {
      ProductInfo productInfo;
      if (key.CheckState == CheckState.Checked && this._productItemToProductIndexHash.TryGetValue(key, out productInfo))
        selectedProducts.Add(productInfo);
    }
    return selectedProducts;
  }

  public void UncheckAll()
  {
    this._checkIsLocked = true;
    try
    {
      foreach (CheckedListBoxItem checkedListBoxItem in (CollectionBase) this._listBoxSpecificationProducts.Items)
        checkedListBoxItem.CheckState = CheckState.Unchecked;
    }
    finally
    {
      this.UpdateEnabled();
      this._checkIsLocked = false;
    }
  }

  public void CheckAllProducts()
  {
    if (this._avsDocument == null || !this._avsDocument.IsFormA)
      return;
    this._checkIsLocked = true;
    try
    {
      for (int index = 0; index < this._listBoxSpecificationProducts.Items.Count; ++index)
        this._listBoxSpecificationProducts.Items[index].CheckState = this._listBoxSpecificationProducts.Items[index] == this.CommonDataItem ? CheckState.Unchecked : CheckState.Checked;
    }
    finally
    {
      this.UpdateEnabled();
      this._checkIsLocked = false;
    }
  }

  private void _listBoxSpecificationProducts_ItemCheck(object sender, DevExpress.IM.XtraEditors.Controls.ItemCheckEventArgs e)
  {
    if (this._checkIsLocked)
      return;
    this._checkIsLocked = true;
    try
    {
      if (e != null)
      {
        if (e.Index != -1)
        {
          CheckedListBoxItem checkedListBoxItem1 = this._listBoxSpecificationProducts.Items[e.Index];
          if (checkedListBoxItem1 == this.CommonDataItem)
          {
            if (e.State == CheckState.Checked)
            {
              foreach (CheckedListBoxItem checkedListBoxItem2 in (CollectionBase) this._listBoxSpecificationProducts.Items)
              {
                if (checkedListBoxItem2 != checkedListBoxItem1)
                  checkedListBoxItem2.CheckState = CheckState.Unchecked;
              }
            }
          }
          else if (!this._avsDocument.IsFormB)
          {
            if (e.State == CheckState.Checked)
              this.CommonDataItem.CheckState = CheckState.Unchecked;
          }
        }
      }
    }
    finally
    {
      this._checkIsLocked = false;
    }
    this.UpdateEnabled();
  }

  private void TemporalySelectCharterForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this.DialogResult != DialogResult.OK)
      return;
    this.UpdateEnabled();
    if (this._btnOK.Enabled)
      return;
    e.Cancel = true;
  }

  private void _buttonCheckAllProducts_Click(object sender, EventArgs e)
  {
    this._checkIsLocked = true;
    try
    {
      foreach (CheckedListBoxItem key in (CollectionBase) this._listBoxSpecificationProducts.Items)
      {
        ProductInfo productInfo;
        if (this._productItemToProductIndexHash.TryGetValue(key, out productInfo) && productInfo != null && !productInfo.IsCommonData)
        {
          if (key.CheckState != CheckState.Checked)
            key.CheckState = CheckState.Checked;
        }
        else
          key.CheckState = CheckState.Unchecked;
      }
    }
    finally
    {
      this.UpdateEnabled();
      this._checkIsLocked = false;
    }
  }

  private void _buttonUcheckAllProducts_Click(object sender, EventArgs e) => this.UncheckAll();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TemporalySelectCharterForm));
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this.label1 = new Label();
    this._listBoxParts = new CheckedListBoxControl();
    this.label2 = new Label();
    this._listBoxSpecificationProducts = new CheckedListBoxControl();
    this._buttonUcheckAllProducts = new Button();
    this._buttonCheckAllProducts = new Button();
    this._btnUncheckAllParts = new Button();
    this._btnCheckAllParts = new Button();
    ((ISupportInitialize) this._listBoxParts).BeginInit();
    ((ISupportInitialize) this._listBoxSpecificationProducts).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this._listBoxParts, "_listBoxParts");
    this._listBoxParts.Items.AddRange(new CheckedListBoxItem[1]
    {
      new CheckedListBoxItem((object) "Общая часть", CheckState.Checked)
    });
    this._listBoxParts.Name = "_listBoxParts";
    this._listBoxParts.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Near, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this._listBoxSpecificationProducts, "_listBoxSpecificationProducts");
    this._listBoxSpecificationProducts.CheckOnClick = true;
    this._listBoxSpecificationProducts.Items.AddRange(new CheckedListBoxItem[1]
    {
      new CheckedListBoxItem((object) "Общие данные")
    });
    this._listBoxSpecificationProducts.Name = "_listBoxSpecificationProducts";
    this._listBoxSpecificationProducts.ItemCheck += new DevExpress.IM.XtraEditors.Controls.ItemCheckEventHandler(this._listBoxSpecificationProducts_ItemCheck);
    componentResourceManager.ApplyResources((object) this._buttonUcheckAllProducts, "_buttonUcheckAllProducts");
    this._buttonUcheckAllProducts.Name = "_buttonUcheckAllProducts";
    this._buttonUcheckAllProducts.UseVisualStyleBackColor = true;
    this._buttonUcheckAllProducts.Click += new EventHandler(this._buttonUcheckAllProducts_Click);
    componentResourceManager.ApplyResources((object) this._buttonCheckAllProducts, "_buttonCheckAllProducts");
    this._buttonCheckAllProducts.Name = "_buttonCheckAllProducts";
    this._buttonCheckAllProducts.UseVisualStyleBackColor = true;
    this._buttonCheckAllProducts.Click += new EventHandler(this._buttonCheckAllProducts_Click);
    componentResourceManager.ApplyResources((object) this._btnUncheckAllParts, "_btnUncheckAllParts");
    this._btnUncheckAllParts.Name = "_btnUncheckAllParts";
    this._btnUncheckAllParts.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnCheckAllParts, "_btnCheckAllParts");
    this._btnCheckAllParts.Name = "_btnCheckAllParts";
    this._btnCheckAllParts.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this._btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._btnUncheckAllParts);
    this.Controls.Add((Control) this._btnCheckAllParts);
    this.Controls.Add((Control) this._buttonUcheckAllProducts);
    this.Controls.Add((Control) this._buttonCheckAllProducts);
    this.Controls.Add((Control) this._listBoxSpecificationProducts);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this._listBoxParts);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this._btnOK);
    this.Controls.Add((Control) this._btnCancel);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (TemporalySelectCharterForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosing += new FormClosingEventHandler(this.TemporalySelectCharterForm_FormClosing);
    ((ISupportInitialize) this._listBoxParts).EndInit();
    ((ISupportInitialize) this._listBoxSpecificationProducts).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
