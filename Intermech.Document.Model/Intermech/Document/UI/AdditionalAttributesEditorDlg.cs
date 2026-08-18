// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.AdditionalAttributesEditorDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Диалог реактирования дополнительных атрибутов</summary>
public class AdditionalAttributesEditorDlg : Form
{
  private AdditionalAttributeCollection attributes;
  private PropertyGrid propertyGrid;
  private Button btnAddAttribute;
  private Button btnRemoveAttribute;
  private Button btnClose;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  /// <summary>Выполнить диалог</summary>
  /// <param name="attributes">Коллекция атрибутов</param>
  public static void Execute(AdditionalAttributeCollection attributes)
  {
    int num = (int) new AdditionalAttributesEditorDlg()
    {
      attributes = attributes,
      propertyGrid = {
        SelectedObject = ((object) attributes)
      }
    }.ShowDialog();
  }

  /// <summary>Контруктор</summary>
  public AdditionalAttributesEditorDlg() => this.InitializeComponent();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AdditionalAttributesEditorDlg));
    this.propertyGrid = new PropertyGrid();
    this.btnAddAttribute = new Button();
    this.btnRemoveAttribute = new Button();
    this.btnClose = new Button();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.propertyGrid, "propertyGrid");
    this.propertyGrid.LineColor = SystemColors.ScrollBar;
    this.propertyGrid.Name = "propertyGrid";
    this.propertyGrid.SelectedGridItemChanged += new SelectedGridItemChangedEventHandler(this.propertyGrid_SelectedGridItemChanged);
    componentResourceManager.ApplyResources((object) this.btnAddAttribute, "btnAddAttribute");
    this.btnAddAttribute.Name = "btnAddAttribute";
    this.btnAddAttribute.Click += new EventHandler(this.btnAddAttribute_Click);
    componentResourceManager.ApplyResources((object) this.btnRemoveAttribute, "btnRemoveAttribute");
    this.btnRemoveAttribute.Name = "btnRemoveAttribute";
    this.btnRemoveAttribute.Click += new EventHandler(this.btnRemoveAttribute_Click);
    componentResourceManager.ApplyResources((object) this.btnClose, "btnClose");
    this.btnClose.DialogResult = DialogResult.Cancel;
    this.btnClose.Name = "btnClose";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btnClose;
    this.Controls.Add((Control) this.btnClose);
    this.Controls.Add((Control) this.btnAddAttribute);
    this.Controls.Add((Control) this.btnRemoveAttribute);
    this.Controls.Add((Control) this.propertyGrid);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AdditionalAttributesEditorDlg);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.ResumeLayout(false);
  }

  private void btnAddAttribute_Click(object sender, EventArgs e)
  {
    string attributeName = (string) null;
    string attributeValue = (string) null;
    if (!AddAttributeDlg.ExecuteDialog(this.attributes.Owner, out attributeName, out attributeValue))
      return;
    this.attributes.Owner.SetAttributeValue(attributeName, attributeValue);
    this.propertyGrid.SelectedObject = (object) this.attributes;
  }

  private void btnRemoveAttribute_Click(object sender, EventArgs e)
  {
    if (this.propertyGrid.SelectedGridItem == null || this.propertyGrid.SelectedGridItem.GridItemType != GridItemType.Property || !(this.propertyGrid.SelectedGridItem.PropertyDescriptor is AttributeDescriptor) || MessageBox.Show($"{LocalizationHolder.rm.GetString("Document.Model_3")}{this.propertyGrid.SelectedGridItem.Label}\"?", LocalizationHolder.rm.GetString("Document.Model_4"), MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    if (this.attributes.Owner.RemoveAttribute(this.propertyGrid.SelectedGridItem.Label, true, true))
    {
      this.propertyGrid.SelectedObject = (object) this.attributes;
    }
    else
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Document.Model_5"));
    }
  }

  private void propertyGrid_SelectedGridItemChanged(
    object sender,
    SelectedGridItemChangedEventArgs e)
  {
    this.btnRemoveAttribute.Enabled = e.NewSelection != null;
  }
}
