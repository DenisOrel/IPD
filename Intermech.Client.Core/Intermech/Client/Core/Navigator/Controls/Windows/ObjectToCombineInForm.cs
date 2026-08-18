
// Type: Intermech.Client.Core.Navigator.Controls.Windows.ObjectToCombineInForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Client.Core.Navigator.Controls.Windows;

/// <summary>
/// Форма выбора объекта, который остается в БД после объединения объектов
/// </summary>
public class ObjectToCombineInForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnCancel;
  private Button btnOk;
  private Label label1;
  private iGrid grid;
  private iGCellStyle iGrid1DefaultCellStyle;
  private iGColHdrStyle iGrid1DefaultColHdrStyle;
  private iGCellStyle iGrid1RowTextColCellStyle;

  /// <summary>
  /// ID версии объекта, в которую нужно перекинуть все ссылки и связи группы объектов
  /// </summary>
  public long ObjectToCombineInID { get; private set; }

  public ObjectToCombineInForm() => this.InitializeComponent();

  public ObjectToCombineInForm(List<MyElement> objectsInfo)
  {
    this.InitializeComponent();
    this.InitializeGrid(objectsInfo);
    this.btnOk.Enabled = false;
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  private void btnCancel_Click(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.Cancel;
    this.Close();
  }

  private void grid_CellClick(object sender, iGCellClickEventArgs e)
  {
    this.ObjectToCombineInID = this.GetSelectedObjectID();
    if (this.btnOk.Enabled)
      return;
    this.btnOk.Enabled = true;
  }

  private void grid_CellDoubleClick(object sender, iGCellDoubleClickEventArgs e)
  {
    long selectedObjectId = this.GetSelectedObjectID();
    int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, selectedObjectId, true);
  }

  /// <summary>Инициализация грида</summary>
  /// <param name="objectIDs">ID объектов для отображения</param>
  private void InitializeGrid(List<MyElement> objectsInfo)
  {
    this.grid.FillWithData(ObjectToCombineInForm.GetTable(objectsInfo));
    this.grid.Cols[0].CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.grid.Cols[1].CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
  }

  /// <summary>Формирует таблицу для отображения в гриде</summary>
  /// <returns></returns>
  private static DataTable GetTable(List<MyElement> objectsInfo)
  {
    DataTable table = new DataTable();
    DataColumn dataColumn1 = new DataColumn("F_OBJECT_ID", typeof (long))
    {
      Caption = EnumDescConverter.GetEnumDescription((Enum) ObligatoryObjectAttributes.F_OBJECT_ID)
    };
    DataColumn dataColumn2 = new DataColumn("F_CAPTION_ATTRIBUTE", typeof (string))
    {
      Caption = EnumDescConverter.GetEnumDescription((Enum) ObligatoryObjectAttributes.CAPTION)
    };
    table.Columns.AddRange(new DataColumn[2]
    {
      dataColumn1,
      dataColumn2
    });
    foreach (MyElement myElement in objectsInfo)
    {
      DataRow row = table.NewRow();
      row["F_OBJECT_ID"] = (object) (long) myElement.Value;
      row["F_CAPTION_ATTRIBUTE"] = (object) myElement.Caption;
      table.Rows.Add(row);
    }
    return table;
  }

  /// <summary>Получает ИД выделенного в гриде объекта</summary>
  /// <returns></returns>
  private long GetSelectedObjectID() => (long) this.grid.SelectedCells[0].Row.Cells[0].Value;

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
    this.btnCancel = new Button();
    this.btnOk = new Button();
    this.label1 = new Label();
    this.grid = new iGrid();
    this.iGrid1DefaultCellStyle = new iGCellStyle(true);
    this.iGrid1DefaultColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1RowTextColCellStyle = new iGCellStyle(true);
    ((ISupportInitialize) this.grid).BeginInit();
    this.SuspendLayout();
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(485, 238);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 9;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.ImeMode = ImeMode.NoControl;
    this.btnOk.Location = new Point(358, 238);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(121, 27);
    this.btnOk.TabIndex = 8;
    this.btnOk.Text = "OK";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.label1.AutoSize = true;
    this.label1.Location = new Point(20, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(279, 13);
    this.label1.TabIndex = 11;
    this.label1.Text = "Выберите объект, который останется в базе данных:";
    this.grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.grid.AutoResizeCols = true;
    this.grid.AutoWidthColMode = iGAutoWidthColMode.Cells;
    this.grid.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle;
    this.grid.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle;
    this.grid.Header.Height = 19;
    this.grid.Location = new Point(23, 42);
    this.grid.Name = "grid";
    this.grid.ReadOnly = true;
    this.grid.RowMode = true;
    this.grid.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle;
    this.grid.Size = new Size(583, 190);
    this.grid.TabIndex = 12;
    this.grid.CellDoubleClick += new iGCellDoubleClickEventHandler(this.grid_CellDoubleClick);
    this.grid.CellClick += new iGCellClickEventHandler(this.grid_CellClick);
    this.AcceptButton = (IButtonControl) this.btnOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(618, 277);
    this.Controls.Add((Control) this.grid);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.MaximizeBox = false;
    this.Name = nameof (ObjectToCombineInForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выбор конечного объекта";
    ((ISupportInitialize) this.grid).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
