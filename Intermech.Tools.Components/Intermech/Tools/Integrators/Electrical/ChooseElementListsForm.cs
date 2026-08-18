// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ChooseElementListsForm
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

public class ChooseElementListsForm : Form
{
  private ElectricalSchemeDescriptors _schemes;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private CheckedListBox checkedListBox1;
  private Button button1;
  private Button button2;

  public ChooseElementListsForm() => this.InitializeComponent();

  public List<Tuple<ElectricalSchemeDescriptor, bool>> Result
  {
    get
    {
      List<Tuple<ElectricalSchemeDescriptor, bool>> result = new List<Tuple<ElectricalSchemeDescriptor, bool>>();
      for (int index = 0; index < this.checkedListBox1.Items.Count; ++index)
        result.Add(new Tuple<ElectricalSchemeDescriptor, bool>(this._schemes.ElementAt<ElectricalSchemeDescriptor>(index), this.checkedListBox1.CheckedIndices.Contains(index)));
      return result;
    }
  }

  public void LoadData(ElectricalSchemeDescriptors schemes)
  {
    this.checkedListBox1.Items.Clear();
    this._schemes = schemes;
    foreach (ElectricalSchemeDescriptor scheme in (List<ElectricalSchemeDescriptor>) schemes)
      this.checkedListBox1.Items.Add((object) $"{scheme.Designation} {scheme.Name}", true);
  }

  private void ChooseElementListsForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void ChooseElementListsForm_Shown(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
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
    this.label1 = new Label();
    this.checkedListBox1 = new CheckedListBox();
    this.button1 = new Button();
    this.button2 = new Button();
    this.SuspendLayout();
    this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.label1.Location = new Point(29, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(502, 31 /*0x1F*/);
    this.label1.TabIndex = 0;
    this.label1.Text = "Проект содержит несколько схем. Выберите схемы, для которых система должна создать перечни элементов.";
    this.checkedListBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.checkedListBox1.CheckOnClick = true;
    this.checkedListBox1.FormattingEnabled = true;
    this.checkedListBox1.Location = new Point(32 /*0x20*/, 49);
    this.checkedListBox1.Name = "checkedListBox1";
    this.checkedListBox1.Size = new Size(499, 184);
    this.checkedListBox1.TabIndex = 1;
    this.button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Location = new Point(283, 245);
    this.button1.Name = "button1";
    this.button1.Size = new Size(121, 27);
    this.button1.TabIndex = 2;
    this.button1.Text = "OK";
    this.button1.UseVisualStyleBackColor = true;
    this.button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Location = new Point(410, 245);
    this.button2.Name = "button2";
    this.button2.Size = new Size(121, 27);
    this.button2.TabIndex = 3;
    this.button2.Text = "Отмена";
    this.button2.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.button1;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.button2;
    this.ClientSize = new Size(551, 284);
    this.Controls.Add((Control) this.button2);
    this.Controls.Add((Control) this.button1);
    this.Controls.Add((Control) this.checkedListBox1);
    this.Controls.Add((Control) this.label1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(364, 300);
    this.Name = nameof (ChooseElementListsForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Создание перечней элементов";
    this.FormClosing += new FormClosingEventHandler(this.ChooseElementListsForm_FormClosing);
    this.Shown += new EventHandler(this.ChooseElementListsForm_Shown);
    this.ResumeLayout(false);
  }
}
