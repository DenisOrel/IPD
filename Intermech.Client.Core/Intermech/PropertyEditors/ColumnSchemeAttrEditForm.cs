
// Type: Intermech.PropertyEditors.ColumnSchemeAttrEditForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class ColumnSchemeAttrEditForm : Form
{
  public Guid Attribute = Guid.Empty;
  public AttributeSourceTypes AttributeSource;
  private List<int> _enabledObjTypes;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Panel panel2;
  private Button bCancel;
  private Button bOK;
  private NumericUpDown nudWidth;
  private Label label2;
  private Label label1;
  private ButtonEdit beAttribute;
  private Label label3;
  private System.Windows.Forms.ComboBox cbAttributeSource;

  public int ColumnWidth => (int) this.nudWidth.Value;

  public ColumnSchemeAttrEditForm()
    : this((List<int>) null, Guid.Empty, 150, AttributeSourceTypes.Object)
  {
  }

  public ColumnSchemeAttrEditForm(
    List<int> enabledObjTypes,
    Guid attribute,
    int columnWidth,
    AttributeSourceTypes sourceType)
  {
    this.InitializeComponent();
    this._enabledObjTypes = enabledObjTypes;
    this.cbAttributeSource.SelectedIndexChanged += new EventHandler(this.cbAttributeSource_SelectedIndexChanged);
    this.cbAttributeSource.Items.Add((object) EnumTypeHelper.GetCaption((Enum) AttributeSourceTypes.Object));
    this.cbAttributeSource.Items.Add((object) EnumTypeHelper.GetCaption((Enum) AttributeSourceTypes.Relation));
    this.cbAttributeSource.SelectedIndex = this.GetIndex(sourceType);
    this.AttributeSource = sourceType;
    this.Attribute = attribute;
    this.nudWidth.Value = (Decimal) columnWidth;
    if (this.Attribute != Guid.Empty)
    {
      IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(this.Attribute, false);
      if (attributeType != null)
      {
        this.beAttribute.Text = attributeType.Name;
        this.cbAttributeSource.Enabled = !ObligatoryObjectAttributesHelper.IsObligatoryAttribute(attributeType.Name);
      }
    }
    this.bOK.Enabled = this.Attribute != Guid.Empty;
  }

  private void cbAttributeSource_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.AttributeSource = this.cbAttributeSource.SelectedIndex == 0 ? AttributeSourceTypes.Object : AttributeSourceTypes.Relation;
  }

  private int GetIndex(AttributeSourceTypes type) => type != AttributeSourceTypes.Object ? 1 : 0;

  private void beAttribute_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    try
    {
      AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false);
      if (this._enabledObjTypes != null && this._enabledObjTypes.Count > 0)
        attributesSelectDlg.LoadAttrDialogForObjectsTypes(this._enabledObjTypes);
      attributesSelectDlg.SelectorFilter = (ISelectorFilter) new WithoutObligatoryFilter(new AttributeSourceTypes[2]
      {
        AttributeSourceTypes.Object,
        AttributeSourceTypes.Relation
      });
      if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count != 1)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attributesSelectDlg.SelectedAttributesID[0], false);
        if (attributeType != null)
        {
          if (ObligatoryObjectAttributesHelper.IsObligatoryAttribute(attributeType.Name))
          {
            this.cbAttributeSource.SelectedIndex = this.GetIndex(ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attributeType.AttributeID));
            this.cbAttributeSource.Enabled = false;
          }
          else
            this.cbAttributeSource.Enabled = true;
          this.Attribute = (attributeType as IDBGuid).GUID;
          this.beAttribute.Text = attributeType.Name;
        }
        else
        {
          this.cbAttributeSource.SelectedIndex = this.GetIndex(AttributeSourceTypes.Object);
          this.Attribute = Guid.Empty;
          this.beAttribute.Text = string.Empty;
        }
      }
    }
    finally
    {
      this.bOK.Enabled = this.Attribute != Guid.Empty;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ColumnSchemeAttrEditForm));
    this.panel1 = new Panel();
    this.label3 = new Label();
    this.cbAttributeSource = new System.Windows.Forms.ComboBox();
    this.nudWidth = new NumericUpDown();
    this.label2 = new Label();
    this.label1 = new Label();
    this.beAttribute = new ButtonEdit();
    this.panel2 = new Panel();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.panel1.SuspendLayout();
    this.nudWidth.BeginInit();
    this.beAttribute.Properties.BeginInit();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.label3);
    this.panel1.Controls.Add((Control) this.cbAttributeSource);
    this.panel1.Controls.Add((Control) this.nudWidth);
    this.panel1.Controls.Add((Control) this.label2);
    this.panel1.Controls.Add((Control) this.label1);
    this.panel1.Controls.Add((Control) this.beAttribute);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    this.cbAttributeSource.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbAttributeSource.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.cbAttributeSource, "cbAttributeSource");
    this.cbAttributeSource.Name = "cbAttributeSource";
    componentResourceManager.ApplyResources((object) this.nudWidth, "nudWidth");
    this.nudWidth.Maximum = new Decimal(new int[4]
    {
      -1981284352,
      -1966660860,
      0,
      0
    });
    this.nudWidth.Minimum = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this.nudWidth.Name = "nudWidth";
    this.nudWidth.Value = new Decimal(new int[4]
    {
      150,
      0,
      0,
      0
    });
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.beAttribute, "beAttribute");
    this.beAttribute.Name = "beAttribute";
    this.beAttribute.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beAttribute.Properties.ReadOnly = true;
    this.beAttribute.ButtonClick += new ButtonPressedEventHandler(this.beAttribute_ButtonClick);
    this.panel2.Controls.Add((Control) this.bCancel);
    this.panel2.Controls.Add((Control) this.bOK);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Name = "bCancel";
    this.bCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.bOK, "bOK");
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Name = "bOK";
    this.bOK.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.bOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panel2);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ColumnSchemeAttrEditForm);
    this.ShowInTaskbar = false;
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.nudWidth.EndInit();
    this.beAttribute.Properties.EndInit();
    this.panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
