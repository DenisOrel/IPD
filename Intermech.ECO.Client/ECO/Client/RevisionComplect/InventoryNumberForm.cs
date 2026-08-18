// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.RevisionComplect.InventoryNumberForm
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Search.Interfaces.Copies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client.RevisionComplect;

[Obsolete]
public class InventoryNumberForm : Form
{
  private Dictionary<string, long> counters = new Dictionary<string, long>();
  private IContainer components;
  private TextBox tbNumber;
  private Label label1;
  private Button btnOk;
  private Button btnCancel;
  private GroupBox groupBox1;
  private RadioButton rbManual;
  private RadioButton rbAutoGeneration;

  public InventoryNumberForm(IDBTypedObjectID typedObject)
  {
    this.InitializeComponent();
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IInventoryNumberGenerator)) is IInventoryNumberGenerator customService))
      return;
    string formula = (ServicesManager.GetService(typeof (IEcoPropertiesService)) as IEcoPropertiesService).Current.KIInventoryNumberTemplate.Replace("[#]", "#");
    this.counters = customService.ParseFormula(ref formula, typedObject.ObjectID, (long) typedObject.ObjectType);
    this.tbNumber.Text = formula;
  }

  public static string Execute(IDBTypedObjectID typedObject)
  {
    string str = "";
    InventoryNumberForm inventoryNumberForm = new InventoryNumberForm(typedObject);
    if (inventoryNumberForm.ShowDialog() == DialogResult.OK)
      str = inventoryNumberForm.tbNumber.Text;
    return str;
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
  }

  private void btnCancel_Click(object sender, EventArgs e)
  {
  }

  protected override void OnClosed(EventArgs e)
  {
    if (this.DialogResult == DialogResult.Cancel && this.counters != null && this.counters.Count > 0)
      ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IInventoryNumberGenerator)) as IInventoryNumberGenerator).RestoreCounters(this.counters);
    base.OnClosed(e);
  }

  private void rbAutoGeneration_CheckedChanged(object sender, EventArgs e)
  {
    this.tbNumber.ReadOnly = !this.rbManual.Checked;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.tbNumber = new TextBox();
    this.label1 = new Label();
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.groupBox1 = new GroupBox();
    this.rbManual = new RadioButton();
    this.rbAutoGeneration = new RadioButton();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.tbNumber.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbNumber.Location = new Point(16 /*0x10*/, 112 /*0x70*/);
    this.tbNumber.Name = "tbNumber";
    this.tbNumber.ReadOnly = true;
    this.tbNumber.Size = new Size(397, 20);
    this.tbNumber.TabIndex = 4;
    this.label1.AutoSize = true;
    this.label1.ImeMode = ImeMode.NoControl;
    this.label1.Location = new Point(16 /*0x10*/, 96 /*0x60*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(111, 13);
    this.label1.TabIndex = 6;
    this.label1.Text = "Инвентарный номер";
    this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.ImeMode = ImeMode.NoControl;
    this.btnOk.Location = new Point(165, 148);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(121, 27);
    this.btnOk.TabIndex = 5;
    this.btnOk.Text = "Применить";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(292, 148);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 3;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox1.Controls.Add((Control) this.rbManual);
    this.groupBox1.Controls.Add((Control) this.rbAutoGeneration);
    this.groupBox1.Location = new Point(12, 11);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(401, 69);
    this.groupBox1.TabIndex = 7;
    this.groupBox1.TabStop = false;
    this.rbManual.AutoSize = true;
    this.rbManual.ImeMode = ImeMode.NoControl;
    this.rbManual.Location = new Point(7, 43);
    this.rbManual.Name = "rbManual";
    this.rbManual.Size = new Size(124, 17);
    this.rbManual.TabIndex = 2;
    this.rbManual.TabStop = true;
    this.rbManual.Text = "Присвоить вручную";
    this.rbManual.UseVisualStyleBackColor = true;
    this.rbManual.CheckedChanged += new EventHandler(this.rbAutoGeneration_CheckedChanged);
    this.rbAutoGeneration.AutoSize = true;
    this.rbAutoGeneration.Checked = true;
    this.rbAutoGeneration.ImeMode = ImeMode.NoControl;
    this.rbAutoGeneration.Location = new Point(7, 20);
    this.rbAutoGeneration.Name = "rbAutoGeneration";
    this.rbAutoGeneration.Size = new Size(182, 17);
    this.rbAutoGeneration.TabIndex = 0;
    this.rbAutoGeneration.TabStop = true;
    this.rbAutoGeneration.Text = "Автоматически сгенерировать";
    this.rbAutoGeneration.UseVisualStyleBackColor = true;
    this.rbAutoGeneration.CheckedChanged += new EventHandler(this.rbAutoGeneration_CheckedChanged);
    this.AcceptButton = (IButtonControl) this.btnOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(425, 187);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.tbNumber);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.btnCancel);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(349, 136);
    this.Name = nameof (InventoryNumberForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Установка Инвентарного номера";
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
