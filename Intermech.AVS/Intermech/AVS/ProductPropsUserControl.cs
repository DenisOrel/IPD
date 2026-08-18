// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ProductPropsUserControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Common_Dialogs;
using Intermech.AVS.Common_Dialogs.ArticleWithDocForm;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

[ViewDescriptionProvider(typeof (ProductPropsUserControl.ProductPropsUserControlViewDescriptionProvider))]
public class ProductPropsUserControl : UserControl, IView
{
  private AVSWindow _avsWindow;
  private DataTable _dataTable;
  private ProductInfo info;
  private ProductVariableDataChapter pdc;
  private bool updating;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBox tbName;
  private Label label2;
  private Label label3;
  private ComboBox bEditLitera;
  private Label label1;
  private TextBox tbDesignation;
  private Button btnEditDesignation;

  public ProductPropsUserControl() => this.InitializeComponent();

  public AVSWindow AVSWindow => this._avsWindow;

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (this._avsWindow != null || !(ServicesManager.GetService(typeof (IAVSViewsService)) is IAVSViewsService service))
      return;
    this._avsWindow = service.AVSWindow;
  }

  public void Activate(IView previousView)
  {
    try
    {
      this.updating = true;
      IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
      this.bEditLitera.Items.Clear();
      this.bEditLitera.Items.Add((object) new ArticleControl.LiteraValue((object) null, string.Empty));
      int attrLitera = AvsIDCache.Attr_Litera;
      IDBAttributeTypeInfo attributeType = service.GetAttributeType(attrLitera, false);
      if (attributeType != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) attributeType.GetPossibleValues().Rows)
          this.bEditLitera.Items.Add((object) Convert.ToString(row[attributeType.ValueFieldName]));
      }
      if (this._avsWindow != null && this._avsWindow.DocumentControl != null)
      {
        List<ProductInfo> selectedProducts = this._avsWindow.GetSelectedProducts();
        if (selectedProducts.Count > 0)
        {
          this.info = selectedProducts[0];
          this.tbName.Text = this.info.Name;
          this.tbDesignation.Text = this.info.Designation;
          this.bEditLitera.SelectedItem = (object) this.info.Litera;
        }
        if (!this._avsWindow.AVSDocument.IsSpecification)
        {
          List<DocumentTreeNode> selectedNodes = this._avsWindow.DocumentControl.SelectedNodes;
          if (selectedNodes != null)
          {
            for (int index = 0; index < selectedNodes.Count; ++index)
            {
              DocumentTreeNode productVariableDocNode = AVSDocument.FindParentProductVariableDocNode(selectedNodes[index]);
              if (productVariableDocNode != null)
              {
                Chapter tag = (productVariableDocNode as TableData).Tag as Chapter;
                if (tag is ProductVariableDataChapter)
                  this.pdc = tag as ProductVariableDataChapter;
                if (tag?.Parent is ProductVariableDataChapter)
                  this.pdc = (ProductVariableDataChapter) tag.Parent;
              }
            }
          }
        }
      }
      this.bEditLitera.Enabled = this.info != null;
      bool flag = this.info != null && this.info.Id == -1L;
      this.tbName.ReadOnly = !flag;
      this.btnEditDesignation.Enabled = flag;
    }
    finally
    {
      this.updating = false;
    }
  }

  public void Deactivate(IView nextView)
  {
  }

  public string Caption => "Свойства исполнения";

  public int ImageIndex => -1;

  public int OrderID => 1;

  private void BEditLiteraSelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.updating || this.info == null)
      return;
    this.info.Litera = Convert.ToString(this.bEditLitera.Items[this.bEditLitera.SelectedIndex]);
    if (this.info.Id != -1L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.info.Id);
        if (dbObject != null)
        {
          AttributeValues[] valuesList = new AttributeValues[1]
          {
            new AttributeValues(AvsIDCache.Attr_Litera, (object) this.info.Litera)
          };
          dbObject.SetAttributesValues(valuesList);
        }
      }
    }
    if (this.pdc == null)
      return;
    this.pdc.UpdateCaptionAttributes();
    this._avsWindow.AVSDocument.UpdateProductHeadersOnPages(true, true);
    this.pdc.DocNode.UpdateLayout(true);
  }

  private void btnEditDesignation_Click(object sender, EventArgs e)
  {
    this._dataTable = this._dataTable ?? ProductsListDialog.GetProductInfoDataTable(this._avsWindow.AVSDocument);
    if (this._dataTable == null || this._dataTable.Rows.Count == 0)
      return;
    DataRow dataRow = this._dataTable.Rows.FirstOrDefault((System.Func<DataRow, bool>) (r => Convert.ToString(r[0]) == this.info.Guid.ToString()));
    if (dataRow == null)
      return;
    string productDesignation = Convert.ToString(dataRow["CAPTION"]);
    string productNumber = Convert.ToString(dataRow["NUMBER"]);
    if ((this._avsWindow.AVSDocument.UseSameDesignationForProducts ? BaseProductInfoDlg.Execute<EditProductCaptionForm>("Редактирование обозначения", this._dataTable, ref productDesignation, ref productNumber) : BaseProductInfoDlg.Execute<ProductDesignationAndNumberDlg>("Редактирование обозначения и номера исполнения", this._dataTable, ref productDesignation, ref productNumber)) != DialogResult.OK)
      return;
    object obj = productNumber == "" ? (object) DBNull.Value : (object) productNumber;
    dataRow[1] = (object) productDesignation;
    dataRow[2] = obj;
    new ProductsListDialog(this._avsWindow.AVSDocument, this._dataTable).Save();
    this.tbDesignation.Text = productDesignation;
  }

  private void tbName_Leave(object sender, EventArgs e)
  {
    if (this.tbName.ReadOnly || !(this.tbName.Text != this.info.Name) || this.tbName.Text.Trim().Length <= 0)
      return;
    this.info.Name = this.tbName.Text;
    this.info.SetAttributeValue(AvsIDCache.Attr_Name, (object) this.info.Name, true);
    if (this.pdc == null)
      return;
    this.pdc.UpdateCaptionAttributes();
    this._avsWindow.AVSDocument.UpdateProductHeadersOnPages(true, true);
    this.pdc.DocNode.UpdateLayout(true);
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
    this.bEditLitera = new ComboBox();
    this.label3 = new Label();
    this.label2 = new Label();
    this.tbName = new TextBox();
    this.label1 = new Label();
    this.tbDesignation = new TextBox();
    this.btnEditDesignation = new Button();
    this.SuspendLayout();
    this.bEditLitera.DropDownStyle = ComboBoxStyle.DropDownList;
    this.bEditLitera.FormattingEnabled = true;
    this.bEditLitera.Location = new Point(114, 64 /*0x40*/);
    this.bEditLitera.Name = "bEditLitera";
    this.bEditLitera.Size = new Size(188, 21);
    this.bEditLitera.TabIndex = 63 /*0x3F*/;
    this.bEditLitera.SelectedIndexChanged += new EventHandler(this.BEditLiteraSelectedIndexChanged);
    this.label3.AutoSize = true;
    this.label3.Location = new Point(9, 68);
    this.label3.Name = "label3";
    this.label3.Size = new Size(47, 13);
    this.label3.TabIndex = 62;
    this.label3.Text = "Литера:";
    this.label2.AutoSize = true;
    this.label2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.label2.Location = new Point(9, 12);
    this.label2.Name = "label2";
    this.label2.Size = new Size(86, 13);
    this.label2.TabIndex = 59;
    this.label2.Text = "Наименование:";
    this.tbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbName.Location = new Point(114, 8);
    this.tbName.Name = "tbName";
    this.tbName.ReadOnly = true;
    this.tbName.Size = new Size(435, 20);
    this.tbName.TabIndex = 57;
    this.tbName.Leave += new EventHandler(this.tbName_Leave);
    this.label1.AutoSize = true;
    this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.label1.Location = new Point(9, 38);
    this.label1.Name = "label1";
    this.label1.Size = new Size(77, 13);
    this.label1.TabIndex = 65;
    this.label1.Text = "Обозначение:";
    this.tbDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbDesignation.Location = new Point(114, 34);
    this.tbDesignation.Margin = new Padding(3, 3, 35, 3);
    this.tbDesignation.Name = "tbDesignation";
    this.tbDesignation.ReadOnly = true;
    this.tbDesignation.Size = new Size(406, 20);
    this.tbDesignation.TabIndex = 64 /*0x40*/;
    this.btnEditDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnEditDesignation.Location = new Point(523, 32 /*0x20*/);
    this.btnEditDesignation.Name = "btnEditDesignation";
    this.btnEditDesignation.Size = new Size(28, 24);
    this.btnEditDesignation.TabIndex = 66;
    this.btnEditDesignation.Text = "...";
    this.btnEditDesignation.UseVisualStyleBackColor = true;
    this.btnEditDesignation.Click += new EventHandler(this.btnEditDesignation_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.btnEditDesignation);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.tbDesignation);
    this.Controls.Add((Control) this.bEditLitera);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.tbName);
    this.Name = nameof (ProductPropsUserControl);
    this.Size = new Size(564, 107);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private sealed class ProductPropsUserControlViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = "Свойства исполнения",
        ImageIndex = -1,
        OrderID = 1
      };
    }
  }
}
