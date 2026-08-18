// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.CreateAttributeForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class CreateAttributeForm : Form
{
  private IContainer components;
  private PropertyGrid propertyGrid1;
  private Button bCancel;
  private Button bOK;

  public CreateAttributeForm() => this.InitializeComponent();

  public void Load(DataRow attributeRow, DataTable pv)
  {
    TreeView aNodeParent1 = new TreeView();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributesGroup attributesGroup = sessionKeeper.Session.GetAttributesGroup(new Guid("cad00341-306c-11d8-b4e9-00304f19f545"));
      TreeNode aNodeParent2 = aNodeParent1.Nodes.Add("");
      aNodeParent2.Tag = (object) new AttributeGroupFolder(Guid.NewGuid(), attributesGroup.GroupName, (object) aNodeParent1, attributesGroup.GroupID, false, attributesGroup.Note, "", "", (attributesGroup as IDBGuid).GUID);
      this.propertyGrid1.SelectedObject = (object) new AttributeFolder(new Guid(Convert.ToString(attributeRow["F_GUID"])), Convert.ToString(attributeRow["F_NAME"]), (object) aNodeParent2, 0, false, Convert.ToString(attributeRow["F_SHORT_NAME"]), Convert.ToString(attributeRow["F_ALIAS"]), Convert.ToString(attributeRow["F_NOTE"]), (FieldTypes) Convert.ToInt32(attributeRow["F_ATTRIBUTE_TYPE"]), attributeRow["F_DEFAULT_VALUE"], (MultiValueModes) Convert.ToInt32(attributeRow["F_MULTIPLE_VALUED"]), (ComputeValueModes) Convert.ToInt32(attributeRow["F_COMPUTED"]), (long) Convert.ToInt32(attributeRow["F_SIZE_TYPE"]), Convert.ToString(attributeRow["F_FORMULA"]), (UniqueValueModes) Convert.ToInt32(attributeRow["F_UNIQUE"]), Convert.ToInt32(attributeRow["F_LEVEL_ID"]), Convert.ToString(attributeRow["F_LANGUAGE_ID"]), new Guid(Convert.ToString(attributeRow["F_GUID"])), Convert.ToString(attributeRow["F_AREA_ID"]), pv, (OptimizationModes) Convert.ToInt32(attributeRow["F_INVIEW"]), Convert.ToInt32(attributeRow["F_CONTENT"]) == 1, (AttributeOptions) Convert.ToInt32(attributeRow["F_OPTIONS"]), Convert.ToString(attributeRow["F_MASK"]));
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.propertyGrid1 = new PropertyGrid();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.SuspendLayout();
    this.propertyGrid1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.propertyGrid1.Location = new Point(12, 12);
    this.propertyGrid1.Name = "propertyGrid1";
    this.propertyGrid1.Size = new Size(545, 414);
    this.propertyGrid1.TabIndex = 0;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(436, 445);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 1;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(309, 445);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 2;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(569, 484);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.propertyGrid1);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (CreateAttributeForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Создание нового атрибута";
    this.ResumeLayout(false);
  }
}
