// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.AddTableRowFromTemplateDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Controls.Grid;
using Intermech.Interfaces.Document;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Диалог добавления строки таблицы по шаблону</summary>
public class AddTableRowFromTemplateDlg : Form
{
  private Button btnOk;
  private Button btnCancel;
  /// <summary>Список строк</summary>
  public ListGrid RowList;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  /// <summary>Конструктор</summary>
  public AddTableRowFromTemplateDlg() => this.InitializeComponent();

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AddTableRowFromTemplateDlg));
    ListColumn listColumn1 = new ListColumn();
    ListColumn listColumn2 = new ListColumn();
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.RowList = new ListGrid();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.RowList.AlternateBackground = Color.DarkGreen;
    componentResourceManager.ApplyResources((object) this.RowList, "RowList");
    this.RowList.BackColor = SystemColors.ControlLightLight;
    listColumn1.Text = "Имя";
    listColumn1.Width = 550;
    listColumn2.Name = "ID";
    listColumn2.Text = "Идентификатор";
    listColumn2.Width = 120;
    this.RowList.Columns.AddRange(new ListColumn[2]
    {
      listColumn1,
      listColumn2
    });
    this.RowList.GridColor = Color.LightGray;
    this.RowList.HeaderHeight = 22;
    this.RowList.HeaderStyle = HeaderStyle.Flat;
    this.RowList.HotTrackingColor = Color.LightGray;
    this.RowList.ImageList = (ImageList) null;
    this.RowList.ItemHeight = 17;
    this.RowList.ItemWordWrap = true;
    this.RowList.Name = "RowList";
    this.RowList.SelectedTextColor = Color.White;
    this.RowList.SelectionColor = Color.DarkBlue;
    this.RowList.SuperFlatHeaderColor = Color.White;
    this.RowList.DoubleClick += new EventHandler(this.rowList_DoubleClick);
    this.RowList.SelectedIndexChanged += new ListGrid.ClickedEventHandler(this.RowList_SelectedIndexChanged);
    this.AcceptButton = (IButtonControl) this.btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.RowList);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AddTableRowFromTemplateDlg);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.AddTableRowFromTemplate_Load);
    this.ResumeLayout(false);
  }

  private void AddTableRowFromTemplate_Load(object sender, EventArgs e)
  {
  }

  private void rowList_DoubleClick(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  /// <summary>Обновить своство Enabled контролов</summary>
  protected void UpdateEnables() => this.btnOk.Enabled = this.SelectedRow != null;

  /// <summary>Выбранная строка</summary>
  public DocumentTreeNode SelectedRow
  {
    [DebuggerStepThrough] get
    {
      Intermech.Controls.Grid.ListItem listItem = this.RowList.SelectedItems.Count <= 0 ? this.RowList.FocusedItem : this.RowList.SelectedItems[0] as Intermech.Controls.Grid.ListItem;
      return listItem != null ? listItem.Tag as DocumentTreeNode : (DocumentTreeNode) null;
    }
  }

  /// <summary>Выполнить диалог</summary>
  /// <param name="templateTable">Таблица с шаблонами строк</param>
  public virtual void Execute(DocumentTreeNode templateTable)
  {
    if (templateTable != null)
    {
      for (int index = 0; index < templateTable.Nodes.Count; ++index)
      {
        DocumentTreeNode node = templateTable.Nodes[index];
        if (node != null)
          this.RowList.Items.Add(new Intermech.Controls.Grid.ListItem()
          {
            Tag = (object) node,
            SubItems = {
              node.GetDefautCaption(),
              node.Id
            }
          });
      }
    }
    this.UpdateEnables();
    int num = (int) this.ShowDialog();
  }

  private void RowList_SelectedIndexChanged(object source, ClickEventArgs e)
  {
    this.UpdateEnables();
  }
}
