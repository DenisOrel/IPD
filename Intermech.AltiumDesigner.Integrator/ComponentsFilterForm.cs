// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ComponentsFilterForm
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Client.Core;
using Intermech.Tools.Integrators.Electrical;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal class ComponentsFilterForm : Form
{
  private IContainer components;
  private Button bOK;
  private Button bCancel;
  private PropertyGrid propertyGrid1;
  private GroupBox groupBox1;
  private Label label2;
  private Label label1;
  private TextBox tbParamName;
  private TextBox tbParamValue;
  private GroupBox groupBox2;
  private Label label4;
  private Label label3;

  public ComponentsFilterForm() => this.InitializeComponent();

  public void Initialize(
    ComponentsFilterSettings<ADComponentsCompositionVariants> settings)
  {
    this.propertyGrid1.SelectedObject = settings.Table.Clone();
    if (settings.OnlyElementListCondition == null)
      return;
    this.tbParamName.Text = (string) settings.OnlyElementListCondition.Item1;
    this.tbParamValue.Text = settings.OnlyElementListCondition.Item2;
  }

  public ComponentsFilterSettings<ADComponentsCompositionVariants> Value
  {
    get
    {
      return new ComponentsFilterSettings<ADComponentsCompositionVariants>((ADComponentsCompositionVariants) this.propertyGrid1.SelectedObject, new Tuple<StringKey, string>((StringKey) this.tbParamName.Text, this.tbParamValue.Text));
    }
  }

  private void ComponentsFilterForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void ComponentsFilterForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.bOK = new Button();
    this.bCancel = new Button();
    this.propertyGrid1 = new PropertyGrid();
    this.groupBox1 = new GroupBox();
    this.label2 = new Label();
    this.label1 = new Label();
    this.tbParamName = new TextBox();
    this.tbParamValue = new TextBox();
    this.groupBox2 = new GroupBox();
    this.label4 = new Label();
    this.label3 = new Label();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(211, 346);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 3;
    this.bOK.Text = "ОК";
    this.bOK.UseVisualStyleBackColor = true;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(338, 346);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 4;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.propertyGrid1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.propertyGrid1.HelpVisible = false;
    this.propertyGrid1.Location = new Point(9, 60);
    this.propertyGrid1.Name = "propertyGrid1";
    this.propertyGrid1.PropertySort = PropertySort.NoSort;
    this.propertyGrid1.Size = new Size(424, 126);
    this.propertyGrid1.TabIndex = 0;
    this.propertyGrid1.ToolbarVisible = false;
    this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox1.Controls.Add((Control) this.label2);
    this.groupBox1.Controls.Add((Control) this.propertyGrid1);
    this.groupBox1.Location = new Point(12, 12);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(439, 197);
    this.groupBox1.TabIndex = 3;
    this.groupBox1.TabStop = false;
    this.label2.Dock = DockStyle.Top;
    this.label2.Location = new Point(3, 16 /*0x10*/);
    this.label2.Name = "label2";
    this.label2.Size = new Size(433, 41);
    this.label2.TabIndex = 3;
    this.label2.Text = "Настройка использования компонентов схемы в документах в зависимости от значения свойства Type";
    this.label1.Dock = DockStyle.Top;
    this.label1.Location = new Point(3, 16 /*0x10*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(433, 49);
    this.label1.TabIndex = 4;
    this.label1.Text = "Параметр компонента и его значение, при котором компонент будет использован только в Перечне элементов";
    this.tbParamName.Location = new Point(24, 74);
    this.tbParamName.Name = "tbParamName";
    this.tbParamName.Size = new Size(189, 20);
    this.tbParamName.TabIndex = 1;
    this.tbParamValue.Location = new Point(220, 74);
    this.tbParamValue.Name = "tbParamValue";
    this.tbParamValue.Size = new Size(213, 20);
    this.tbParamValue.TabIndex = 2;
    this.groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox2.Controls.Add((Control) this.label4);
    this.groupBox2.Controls.Add((Control) this.label3);
    this.groupBox2.Controls.Add((Control) this.label1);
    this.groupBox2.Controls.Add((Control) this.tbParamValue);
    this.groupBox2.Controls.Add((Control) this.tbParamName);
    this.groupBox2.Location = new Point(12, 213);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(439, 116);
    this.groupBox2.TabIndex = 7;
    this.groupBox2.TabStop = false;
    this.label4.AutoSize = true;
    this.label4.Location = new Point(220, 58);
    this.label4.Name = "label4";
    this.label4.Size = new Size(58, 13);
    this.label4.TabIndex = 8;
    this.label4.Text = "Значение:";
    this.label3.AutoSize = true;
    this.label3.Location = new Point(24, 58);
    this.label3.Name = "label3";
    this.label3.Size = new Size(90, 13);
    this.label3.TabIndex = 7;
    this.label3.Text = "Имя параметра:";
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(471, 394);
    this.Controls.Add((Control) this.groupBox2);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.bOK);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(487, 432);
    this.Name = nameof (ComponentsFilterForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Настройки фильтрации состава";
    this.FormClosing += new FormClosingEventHandler(this.ComponentsFilterForm_FormClosing);
    this.Load += new EventHandler(this.ComponentsFilterForm_Load);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.ResumeLayout(false);
  }
}
