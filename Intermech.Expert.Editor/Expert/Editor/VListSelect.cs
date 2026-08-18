// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.VListSelect
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class VListSelect : Form
{
  private bool _multi;
  private bool lockChange;
  private FieldTypes ft = FieldTypes.ftInteger;
  private List<object> values;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckedListBox clb;
  private Panel panel1;
  private Button button2;
  private Button button1;
  private Label lblAttr;
  private Panel panel2;

  public VListSelect() => this.InitializeComponent();

  public bool Execute(int attrTypeId, bool Multi, List<long> objIds)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attrTypeId);
      if (attributeType == null)
        return false;
      this.ft = attributeType.PropertiesStructure.FieldType;
      DataRow[] possibleValuesRows = attributeType.GetPossibleValuesRows();
      return possibleValuesRows.Length != 0 && this.Execute(possibleValuesRows, attributeType.Name, Multi, objIds);
    }
  }

  public bool Execute(DataRow[] rows, string attrName, bool Multi, List<long> objIds)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attrName);
      if (attributeType != null)
        this.ft = attributeType.PropertiesStructure.FieldType;
    }
    this.lblAttr.Text = $"{this.lblAttr.Text}\"{attrName}\"";
    this._multi = Multi;
    this.lockChange = true;
    try
    {
      int index = 0;
      this.values = new List<object>();
      foreach (DataRow row in rows)
      {
        string str = Convert.ToString(row["F_DESCRIPTION"]);
        object obj = (object) null;
        if (this.ft == FieldTypes.ftInteger)
          obj = (object) Convert.ToInt64(row["F_INTEGER_VALUE"]);
        if (this.ft == FieldTypes.ftString)
          obj = row["F_STRING_VALUE"];
        if (str == "" && obj != null)
          str = Convert.ToString(obj);
        this.values.Add(obj);
        this.clb.Items.Add((object) str);
        if (this.values[index].GetType() == typeof (long) && objIds.Contains(Convert.ToInt64(this.values[index])))
          this.clb.SetItemChecked(index, true);
        ++index;
      }
    }
    finally
    {
      this.lockChange = false;
    }
    return this.ShowDialog() == DialogResult.OK;
  }

  public bool Execute(string[] possibleVals, List<long> selIndices, string attrName, bool Multi)
  {
    this.lblAttr.Text = $"{this.lblAttr.Text}\"{attrName}\"";
    this._multi = Multi;
    this.lockChange = true;
    try
    {
      int index = 0;
      foreach (object possibleVal in possibleVals)
      {
        this.clb.Items.Add(possibleVal);
        if (selIndices.Contains((long) index))
          this.clb.SetItemChecked(index, true);
        ++index;
      }
    }
    finally
    {
      this.lockChange = false;
    }
    return this.ShowDialog() == DialogResult.OK;
  }

  private void clb_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    if (this.lockChange || this._multi || e.NewValue != CheckState.Checked)
      return;
    this.clb.BeginUpdate();
    try
    {
      for (int index = 0; index < this.clb.Items.Count; ++index)
      {
        if (index != e.Index && this.clb.CheckedIndices.Contains(index))
          this.clb.SetItemChecked(index, false);
      }
    }
    finally
    {
      this.clb.EndUpdate();
    }
  }

  public List<string> GetResults(out List<long> Indices)
  {
    Indices = new List<long>();
    List<string> results = new List<string>();
    for (int index = 0; index < this.clb.Items.Count; ++index)
    {
      if (this.clb.CheckedIndices.Contains(index) && this.values != null)
      {
        if (this.values[index].GetType() == typeof (long))
        {
          Indices.Add(Convert.ToInt64(this.values[index]));
          results.Add(this.clb.Items[index].ToString());
        }
        else
          results.Add($"\"{Convert.ToString(this.values[index])}\"");
      }
    }
    return results;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VListSelect));
    this.clb = new CheckedListBox();
    this.panel1 = new Panel();
    this.button2 = new Button();
    this.button1 = new Button();
    this.lblAttr = new Label();
    this.panel2 = new Panel();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.clb.CheckOnClick = true;
    componentResourceManager.ApplyResources((object) this.clb, "clb");
    this.clb.FormattingEnabled = true;
    this.clb.Name = "clb";
    this.clb.ThreeDCheckBoxes = true;
    this.clb.ItemCheck += new ItemCheckEventHandler(this.clb_ItemCheck);
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.button1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.lblAttr, "lblAttr");
    this.lblAttr.Name = "lblAttr";
    this.panel2.Controls.Add((Control) this.lblAttr);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.clb);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (VListSelect);
    this.ShowInTaskbar = false;
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.ResumeLayout(false);
  }
}
