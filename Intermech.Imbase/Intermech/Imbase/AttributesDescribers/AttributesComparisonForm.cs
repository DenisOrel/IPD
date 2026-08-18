// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.AttributesComparisonForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Navigator.SelectionView;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

internal sealed class AttributesComparisonForm : Form
{
  private IContainer components;
  private GroupBox groupBox1;
  private MaskedTextBox tbSourceGuid;
  private Label label2;
  private MaskedTextBox tbSourceName;
  private Label label1;
  private GroupBox groupBox2;
  private Button button1;
  private TextBox tbDest;
  private Button bCancel;
  private Button bOK;
  private Button button2;

  public AttributesComparison Comparison { get; private set; }

  public AttributesComparisonForm(AttributesComparison comparison)
  {
    this.InitializeComponent();
    this.Comparison = comparison != null ? new AttributesComparison(comparison.SourceGuid, comparison.SourceName, comparison.DestinationGuid) : new AttributesComparison(Guid.Empty, string.Empty, Guid.Empty);
    this.tbDest.Text = this.Comparison.DestinationGuid != Guid.Empty ? MetaDataHelper.GetAttributeTypeName(this.Comparison.DestinationGuid) : string.Empty;
    this.tbSourceName.Text = this.Comparison.SourceName;
    this.tbSourceGuid.Text = this.Comparison.SourceGuid != Guid.Empty ? this.Comparison.SourceGuid.ToString() : string.Empty;
  }

  private void OnChanged() => this.bOK.Enabled = true;

  private void button1_Click(object sender, EventArgs e)
  {
    using (AdvSelectorForm advSelectorForm = new AdvSelectorForm(AdvSelector.AttributeType, AttributableElements.None))
    {
      advSelectorForm.Text = "Выберите атрибут";
      if (advSelectorForm.ShowDialog() != DialogResult.OK)
        return;
      int attributeType = advSelectorForm.AttributeTypes[0];
      Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attributeType);
      if (attributeTypeGuid.Equals(this.Comparison.DestinationGuid))
        return;
      this.Comparison.DestinationGuid = attributeTypeGuid;
      this.tbDest.Text = MetaDataHelper.GetAttributeTypeName(attributeType);
      this.OnChanged();
    }
  }

  private void button2_Click(object sender, EventArgs e)
  {
    using (GuidSelector guidSelector = new GuidSelector(this.Comparison.SourceGuid))
    {
      if (guidSelector.ShowDialog() != DialogResult.OK || !(this.Comparison.SourceGuid != guidSelector.ResultGuid))
        return;
      this.Comparison.SourceGuid = guidSelector.ResultGuid;
      this.tbSourceGuid.Text = guidSelector.ResultGuid.ToString();
      this.OnChanged();
    }
  }

  private void tbSourceName_TextChanged(object sender, EventArgs e)
  {
    this.Comparison.SourceName = this.tbSourceName.Text;
    this.OnChanged();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.groupBox1 = new GroupBox();
    this.button2 = new Button();
    this.label2 = new Label();
    this.tbSourceName = new MaskedTextBox();
    this.label1 = new Label();
    this.tbSourceGuid = new MaskedTextBox();
    this.groupBox2 = new GroupBox();
    this.button1 = new Button();
    this.tbDest = new TextBox();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.groupBox1.Controls.Add((Control) this.button2);
    this.groupBox1.Controls.Add((Control) this.label2);
    this.groupBox1.Controls.Add((Control) this.tbSourceName);
    this.groupBox1.Controls.Add((Control) this.label1);
    this.groupBox1.Controls.Add((Control) this.tbSourceGuid);
    this.groupBox1.Location = new Point(12, 12);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(411, 137);
    this.groupBox1.TabIndex = 1;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Атрибут базы-источника ";
    this.button2.Location = new Point(352, 46);
    this.button2.Name = "button2";
    this.button2.Size = new Size(24, 23);
    this.button2.TabIndex = 5;
    this.button2.Text = "...";
    this.button2.UseVisualStyleBackColor = true;
    this.button2.Click += new EventHandler(this.button2_Click);
    this.label2.AutoSize = true;
    this.label2.Location = new Point(37, 76);
    this.label2.Name = "label2";
    this.label2.Size = new Size(83, 13);
    this.label2.TabIndex = 4;
    this.label2.Text = "Наименование";
    this.tbSourceName.Location = new Point(40, 92);
    this.tbSourceName.Name = "tbSourceName";
    this.tbSourceName.Size = new Size(310, 20);
    this.tbSourceName.TabIndex = 1;
    this.tbSourceName.TextChanged += new EventHandler(this.tbSourceName_TextChanged);
    this.label1.AutoSize = true;
    this.label1.Location = new Point(37, 31 /*0x1F*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(150, 13);
    this.label1.TabIndex = 2;
    this.label1.Text = "Глобальный идентификатор";
    this.tbSourceGuid.BackColor = SystemColors.Window;
    this.tbSourceGuid.Location = new Point(40, 47);
    this.tbSourceGuid.Name = "tbSourceGuid";
    this.tbSourceGuid.ReadOnly = true;
    this.tbSourceGuid.Size = new Size(310, 20);
    this.tbSourceGuid.TabIndex = 0;
    this.groupBox2.Controls.Add((Control) this.button1);
    this.groupBox2.Controls.Add((Control) this.tbDest);
    this.groupBox2.Location = new Point(12, 155);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(411, 78);
    this.groupBox2.TabIndex = 2;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Атрибут текущей базы данных";
    this.button1.Location = new Point(352, 29);
    this.button1.Name = "button1";
    this.button1.Size = new Size(24, 23);
    this.button1.TabIndex = 3;
    this.button1.Text = "...";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.button1_Click);
    this.tbDest.BackColor = SystemColors.Window;
    this.tbDest.Location = new Point(40, 31 /*0x1F*/);
    this.tbDest.Name = "tbDest";
    this.tbDest.ReadOnly = true;
    this.tbDest.Size = new Size(310, 20);
    this.tbDest.TabIndex = 2;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(302, 251);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 5;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Enabled = false;
    this.bOK.Location = new Point(175, 251);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 4;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(439, 299);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.groupBox2);
    this.Controls.Add((Control) this.groupBox1);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = nameof (AttributesComparisonForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Сопоставление атрибутов";
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.ResumeLayout(false);
  }
}
