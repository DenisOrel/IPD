// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ObjSelect
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class ObjSelect : Form
{
  public bool addForDoc;
  public string capt = "";
  private List<long> res = new List<long>();
  private IContainer components;
  private Panel panel1;
  private Button button2;
  private Button button1;
  private ListView lv;
  private ColumnHeader IdCol;
  private ColumnHeader DesCol;
  private ColumnHeader CaptCol;
  private CheckBox cbForDoc;
  private ColumnHeader typeCol;

  public ObjSelect() => this.InitializeComponent();

  public List<long> Execute(
    List<long> objIds,
    bool multi,
    bool enableForDoc,
    List<HidingType> mainItems = null)
  {
    if (objIds.Count == 0)
      return this.res;
    this.cbForDoc.Visible = enableForDoc;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      for (int index = 0; index < objIds.Count; ++index)
      {
        IDBObject dbObject = session.GetObject(objIds[index], false) ?? session.GetObject(-objIds[index], false);
        if (dbObject != null)
        {
          string str = "";
          IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), false);
          if (attributeByGuid != null)
            str = attributeByGuid.AsString;
          IMSObjectType objectType = MetaDataHelper.GetObjectType(dbObject.ObjectType);
          ListViewItem listViewItem = new ListViewItem(new string[4]
          {
            Convert.ToString(objIds[index]),
            objectType.ObjectTypeName,
            str,
            dbObject.Caption
          });
          if (mainItems != null && mainItems[index] == HidingType.Disabled)
            listViewItem.BackColor = Color.FromArgb(224 /*0xE0*/, 224 /*0xE0*/, 224 /*0xE0*/);
          this.lv.Items.Add(listViewItem);
        }
      }
      this.lv.MultiSelect = multi;
      if (objIds.Count == 1)
      {
        if (this.lv.Items.Count > 0)
          this.lv.Items[0].Selected = true;
      }
    }
    if (this.lv.Items.Count == 0)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_451"), LocalizationHolder.rm.GetString("ECO.Client_260"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      return this.res;
    }
    if (this.capt != "")
      this.Text = this.capt;
    if (this.ShowDialog() == DialogResult.OK && this.lv.SelectedIndices.Count > 0)
    {
      for (int index = 0; index < this.lv.SelectedIndices.Count; ++index)
        this.res.Add(objIds[this.lv.SelectedIndices[index]]);
      if (enableForDoc)
        this.addForDoc = this.cbForDoc.Checked;
    }
    return this.res;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjSelect));
    this.panel1 = new Panel();
    this.cbForDoc = new CheckBox();
    this.button2 = new Button();
    this.button1 = new Button();
    this.lv = new ListView();
    this.IdCol = new ColumnHeader();
    this.DesCol = new ColumnHeader();
    this.CaptCol = new ColumnHeader();
    this.typeCol = new ColumnHeader();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.cbForDoc);
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.button1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.cbForDoc, "cbForDoc");
    this.cbForDoc.Name = "cbForDoc";
    this.cbForDoc.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    this.lv.Columns.AddRange(new ColumnHeader[4]
    {
      this.IdCol,
      this.typeCol,
      this.DesCol,
      this.CaptCol
    });
    componentResourceManager.ApplyResources((object) this.lv, "lv");
    this.lv.FullRowSelect = true;
    this.lv.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lv.HideSelection = false;
    this.lv.Name = "lv";
    this.lv.UseCompatibleStateImageBehavior = false;
    this.lv.View = View.Details;
    componentResourceManager.ApplyResources((object) this.IdCol, "IdCol");
    componentResourceManager.ApplyResources((object) this.DesCol, "DesCol");
    componentResourceManager.ApplyResources((object) this.CaptCol, "CaptCol");
    componentResourceManager.ApplyResources((object) this.typeCol, "typeCol");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.lv);
    this.Controls.Add((Control) this.panel1);
    this.MinimizeBox = false;
    this.Name = nameof (ObjSelect);
    this.ShowInTaskbar = false;
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
