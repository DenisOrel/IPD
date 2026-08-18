// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ESApplicability
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Expert;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class ESApplicability : Form
{
  private List<long> attrRulesList = new List<long>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Button button1;
  private ListView lv;
  private ColumnHeader colId;
  private ColumnHeader colName;
  private Button button2;

  public ESApplicability() => this.InitializeComponent();

  public void Execute(long expObjId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.attrRulesList = (sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) as IExpertServer).GetAttrRulesForObject(sessionKeeper.Session.SessionGUID, expObjId);
      foreach (long attrRules in this.attrRulesList)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(attrRules);
        if (dbObject != null)
          this.lv.Items.Add(dbObject.Caption).SubItems.Add(Convert.ToString(dbObject.ObjectID));
      }
    }
    if (this.attrRulesList.Count > 0)
    {
      int num1 = (int) this.ShowDialog();
    }
    else
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_581"), LocalizationHolder.rm.GetString("Expert.Editor_59"), MessageBoxButtons.OK);
    }
  }

  private void button2_Click(object sender, EventArgs e)
  {
    if (this.lv.SelectedIndices == null || this.lv.SelectedIndices.Count == 0)
      return;
    ISelectedItems items = Intermech.Navigator.ContextMenu.Services.GetItems(this.attrRulesList[this.lv.SelectedIndices[0]]);
    ServiceContainer viewServices1 = new ServiceContainer();
    viewServices1.AddService(typeof (IViewState), (object) new ViewStateService());
    ServiceContainer viewServices2 = viewServices1;
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices2);
    if (!commandsTable.Contains("EditDocument"))
      return;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("EditDocument", commandsTable, (System.IServiceProvider) viewServices1);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ESApplicability));
    this.panel1 = new Panel();
    this.button2 = new Button();
    this.button1 = new Button();
    this.lv = new ListView();
    this.colId = new ColumnHeader();
    this.colName = new ColumnHeader();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.button1);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    this.button2.Click += new EventHandler(this.button2_Click);
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.lv, "lv");
    this.lv.Columns.AddRange(new ColumnHeader[2]
    {
      this.colId,
      this.colName
    });
    this.lv.FullRowSelect = true;
    this.lv.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lv.HideSelection = false;
    this.lv.MultiSelect = false;
    this.lv.Name = "lv";
    this.lv.UseCompatibleStateImageBehavior = false;
    this.lv.View = View.Details;
    componentResourceManager.ApplyResources((object) this.colId, "colId");
    componentResourceManager.ApplyResources((object) this.colName, "colName");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.lv);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ESApplicability);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
