// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.SelTemplateList
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces.Document;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class SelTemplateList : Form
{
  private List<int> indexList = new List<int>();
  private IContainer components;
  private Panel panel1;
  private Button btnOK;
  private Button btnCancel;
  private ListBox lb;

  public SelTemplateList() => this.InitializeComponent();

  public int Execute(DocumentTreeNode template)
  {
    this.lb.Items.Clear();
    for (int index = 0; index < template.NodesCount; ++index)
    {
      DocumentTreeNode node = template.Nodes[index];
      this.indexList.Add(index);
      this.lb.Items.Add((object) node.Name);
    }
    this.lb.SelectedIndex = 0;
    return this.ShowDialog() == DialogResult.OK ? this.indexList[this.lb.SelectedIndex] : -1;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.panel1 = new Panel();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.lb = new ListBox();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.btnOK);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 372);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(736, 36);
    this.panel1.TabIndex = 0;
    this.btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(568, 6);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(75, 23);
    this.btnOK.TabIndex = 1;
    this.btnOK.Text = "ОК";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(649, 6);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 0;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.lb.Dock = DockStyle.Fill;
    this.lb.FormattingEnabled = true;
    this.lb.Location = new Point(0, 0);
    this.lb.Name = "lb";
    this.lb.Size = new Size(736, 372);
    this.lb.TabIndex = 1;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(736, 408);
    this.Controls.Add((Control) this.lb);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelTemplateList);
    this.Text = "Выбор листа из шаблона извещения";
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
