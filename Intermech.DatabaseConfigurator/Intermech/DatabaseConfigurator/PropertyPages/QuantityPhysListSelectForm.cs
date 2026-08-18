// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.QuantityPhysListSelectForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

public class QuantityPhysListSelectForm : Form
{
  private List<Guid> excludeList = new List<Guid>();
  private Dictionary<Guid, string> selectedList = new Dictionary<Guid, string>();
  private IContainer components;
  private Panel panel1;
  private Button btnCancel;
  private Button btnOk;
  private ListView listView;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;

  public Dictionary<Guid, string> SelectedList => this.selectedList;

  public QuantityPhysListSelectForm() => this.InitializeComponent();

  public DialogResult ShowDialog(List<Guid> list)
  {
    this.excludeList = list;
    this.selectedList.Clear();
    this.FillData();
    return this.ShowDialog();
  }

  private void FillData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(new Guid("cad00048-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[0], new ColumnDescriptor[2]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_GUID),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION)
      })
      {
        SortColumns = new object[1]
        {
          (object) ObligatoryObjectAttributes.CAPTION
        },
        Orders = new SortOrders[1]{ SortOrders.ASC }
      });
      this.listView.BeginUpdate();
      try
      {
        this.listView.Items.Clear();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          Guid guid = new Guid(Convert.ToString(row[0]));
          if (!this.excludeList.Contains(guid))
          {
            ListViewItem listViewItem = this.listView.Items.Add(Convert.ToString(row[1]));
            listViewItem.SubItems.Add(Convert.ToString(row[0]));
            listViewItem.Tag = (object) guid;
          }
        }
      }
      finally
      {
        this.listView.EndUpdate();
      }
    }
  }

  private void btnOk_Click(object sender, EventArgs e) => this.FillResult();

  private void FillResult()
  {
    this.selectedList.Clear();
    if (this.listView.SelectedItems.Count == 0)
    {
      this.DialogResult = DialogResult.None;
    }
    else
    {
      foreach (ListViewItem selectedItem in this.listView.SelectedItems)
        this.selectedList.Add((Guid) selectedItem.Tag, selectedItem.Text);
    }
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
    this.btnCancel = new Button();
    this.btnOk = new Button();
    this.listView = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Controls.Add((Control) this.btnOk);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 394);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(705, 41);
    this.panel1.TabIndex = 0;
    this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(618, 6);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnOk.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Location = new Point(537, 6);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(75, 23);
    this.btnOk.TabIndex = 0;
    this.btnOk.Text = "OK";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.listView.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader1,
      this.columnHeader2
    });
    this.listView.Dock = DockStyle.Fill;
    this.listView.FullRowSelect = true;
    this.listView.HideSelection = false;
    this.listView.Location = new Point(0, 0);
    this.listView.Name = "listView";
    this.listView.Size = new Size(705, 394);
    this.listView.TabIndex = 9;
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    this.columnHeader1.Text = "Наименование";
    this.columnHeader1.Width = 300;
    this.columnHeader2.Text = "Глобальный идентификатор";
    this.columnHeader2.Width = 300;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(705, 435);
    this.Controls.Add((Control) this.listView);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (QuantityPhysListSelectForm);
    this.Text = "Выберите физические величины";
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
