// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ChooseSynchroDlg
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class ChooseSynchroDlg : Form
{
  private List<long> savedList;
  private IContainer components;
  private CheckedListBox clb;
  private Button btnCancel;
  private Button btnOK;
  private Label label1;
  private Label label2;
  private TextBox tbRoot;

  public ChooseSynchroDlg() => this.InitializeComponent();

  public bool Execute(List<long> synchroList, long rootObjId)
  {
    this.savedList = synchroList;
    this.PopulateControls(synchroList, rootObjId);
    return this.ShowDialog() == DialogResult.OK;
  }

  private void PopulateControls(List<long> synchroList, long rootObjId)
  {
    this.clb.Items.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo1 = sessionKeeper.Session.GetObjectInfo(rootObjId);
      this.tbRoot.Text = $"[{Convert.ToString(rootObjId)}] {objectInfo1.Caption} [{MetaDataHelper.GetObjectTypeName(objectInfo1.ObjectTypeID)}]";
      foreach (long synchro in synchroList)
      {
        QuickObjectInfo objectInfo2 = sessionKeeper.Session.GetObjectInfo(synchro);
        this.clb.Items.Add((object) $"[{Convert.ToString(synchro)}] {objectInfo2.Caption} [{MetaDataHelper.GetObjectTypeName(objectInfo2.ObjectTypeID)}]");
      }
    }
  }

  public List<long> ComposeChosenList()
  {
    List<long> longList = new List<long>();
    foreach (int checkedIndex in this.clb.CheckedIndices)
      longList.Add(this.savedList[checkedIndex]);
    return longList;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.clb = new CheckedListBox();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.label1 = new Label();
    this.label2 = new Label();
    this.tbRoot = new TextBox();
    this.SuspendLayout();
    this.clb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.clb.CheckOnClick = true;
    this.clb.FormattingEnabled = true;
    this.clb.IntegralHeight = false;
    this.clb.Location = new Point(9, 33);
    this.clb.Name = "clb";
    this.clb.Size = new Size(519, 93);
    this.clb.TabIndex = 0;
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(450, 132);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(369, 132);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(75, 23);
    this.btnOK.TabIndex = 2;
    this.btnOK.Text = "Да";
    this.btnOK.UseVisualStyleBackColor = true;
    this.label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(6, 137);
    this.label1.Name = "label1";
    this.label1.Size = new Size(360, 13);
    this.label1.TabIndex = 3;
    this.label1.Text = "Нажатие кнопки \"Отмена\" отменит включение объекта в извещение";
    this.label2.AutoSize = true;
    this.label2.Location = new Point(6, 9);
    this.label2.Name = "label2";
    this.label2.Size = new Size(112 /*0x70*/, 13);
    this.label2.TabIndex = 4;
    this.label2.Text = "Включаемый объект";
    this.tbRoot.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbRoot.Location = new Point(124, 6);
    this.tbRoot.Name = "tbRoot";
    this.tbRoot.ReadOnly = true;
    this.tbRoot.Size = new Size(404, 20);
    this.tbRoot.TabIndex = 5;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(537, 164);
    this.Controls.Add((Control) this.tbRoot);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.clb);
    this.MinimizeBox = false;
    this.MinimumSize = new Size(550, 200);
    this.Name = nameof (ChooseSynchroDlg);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выбор связанных объектов, которым будет присвоена та же литера";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
