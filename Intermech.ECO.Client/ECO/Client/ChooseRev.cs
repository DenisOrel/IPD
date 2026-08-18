// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ChooseRev
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class ChooseRev : Form
{
  public int sel_index = -1;
  private IContainer components;
  private Label label1;
  private ListBox lb;
  private Panel panel1;
  private Button button3;
  private Button button2;
  private Button btnOK;
  private Label label2;
  private ListBox lbObjects;

  public ChooseRev() => this.InitializeComponent();

  public DialogResult Execute(List<string> stringList, List<long> objIds)
  {
    this.lb.Items.Clear();
    foreach (object obj in stringList)
      this.lb.Items.Add(obj);
    this.ShowObjects(objIds);
    return this.ShowDialog();
  }

  private void ShowObjects(List<long> objIds)
  {
    this.lbObjects.Items.Clear();
    if (objIds == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objId in objIds)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objId);
        if (dbObject != null)
          this.lbObjects.Items.Add((object) $"[{Convert.ToString(objId)}] {dbObject.Caption}");
      }
    }
    this.btnOK.Enabled = false;
  }

  private void button1_Click(object sender, EventArgs e)
  {
    if (this.lb.SelectedIndex >= 0)
      this.sel_index = this.lb.SelectedIndex;
    else
      this.DialogResult = DialogResult.None;
  }

  private void lb_SelectedIndexChanged(object sender, EventArgs e) => this.btnOK.Enabled = true;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ChooseRev));
    this.label1 = new Label();
    this.lb = new ListBox();
    this.panel1 = new Panel();
    this.button3 = new Button();
    this.button2 = new Button();
    this.btnOK = new Button();
    this.label2 = new Label();
    this.lbObjects = new ListBox();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.lb, "lb");
    this.lb.FormattingEnabled = true;
    this.lb.Name = "lb";
    this.lb.SelectedIndexChanged += new EventHandler(this.lb_SelectedIndexChanged);
    this.panel1.Controls.Add((Control) this.button3);
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.btnOK);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.button3, "button3");
    this.button3.DialogResult = DialogResult.Yes;
    this.button3.Name = "button3";
    this.button3.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.button1_Click);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.label2.Tag = (object) "";
    componentResourceManager.ApplyResources((object) this.lbObjects, "lbObjects");
    this.lbObjects.FormattingEnabled = true;
    this.lbObjects.Name = "lbObjects";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.lbObjects);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.lb);
    this.Controls.Add((Control) this.label1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ChooseRev);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
