
// Type: Intermech.Interfaces.Client.StampChangingForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Interfaces.Client;

/// <summary>Форма выбора грифа документа</summary>
public class StampChangingForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel btnPanel;
  private Panel listPanel;
  private Button btnOK;
  private Button btnCancel;
  private ComboBox comboBox1;

  public int AttrValueIndex => this.comboBox1.SelectedIndex;

  public StampChangingForm()
  {
    this.InitializeComponent();
    this.InitializeComboBox();
  }

  private void InitializeComboBox()
  {
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(MetaDataHelper.GetAttributeTypeID("cadd9ac2-306c-11d8-b4e9-00304f19f545"));
    for (int index = 0; index < attributeType.PossibleValues.Count; ++index)
      this.comboBox1.Items.Add(attributeType.PossibleValuesDescriptions[index]);
    this.comboBox1.SelectedIndex = 0;
  }

  private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
  {
  }

  private void btnCancel_Click(object sender, EventArgs e) => this.comboBox1.SelectedIndex = -1;

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
    this.btnPanel = new Panel();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.listPanel = new Panel();
    this.comboBox1 = new ComboBox();
    this.btnPanel.SuspendLayout();
    this.listPanel.SuspendLayout();
    this.SuspendLayout();
    this.btnPanel.Controls.Add((Control) this.btnOK);
    this.btnPanel.Controls.Add((Control) this.btnCancel);
    this.btnPanel.Dock = DockStyle.Bottom;
    this.btnPanel.Location = new Point(0, 52);
    this.btnPanel.Name = "btnPanel";
    this.btnPanel.Size = new Size(359, 39);
    this.btnPanel.TabIndex = 0;
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(118, 4);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(108, 23);
    this.btnOK.TabIndex = 19;
    this.btnOK.Text = "ОК";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(232, 4);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(115, 23);
    this.btnCancel.TabIndex = 18;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.listPanel.Controls.Add((Control) this.comboBox1);
    this.listPanel.Dock = DockStyle.Fill;
    this.listPanel.Location = new Point(0, 0);
    this.listPanel.Name = "listPanel";
    this.listPanel.Size = new Size(359, 52);
    this.listPanel.TabIndex = 1;
    this.comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.comboBox1.FormattingEnabled = true;
    this.comboBox1.Location = new Point(12, 12);
    this.comboBox1.Name = "comboBox1";
    this.comboBox1.Size = new Size(335, 21);
    this.comboBox1.TabIndex = 0;
    this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(359, 91);
    this.Controls.Add((Control) this.listPanel);
    this.Controls.Add((Control) this.btnPanel);
    this.MinimumSize = new Size(375, 130);
    this.Name = nameof (StampChangingForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выбор грифа";
    this.btnPanel.ResumeLayout(false);
    this.listPanel.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
