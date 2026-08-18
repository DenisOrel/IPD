
// Type: Intermech.Security.SecurityAccessRightsReportForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Security;

public class SecurityAccessRightsReportForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ListView lvReport;
  private ColumnHeader colObject;
  private ColumnHeader colOwner;
  private ColumnHeader colType;
  private ColumnHeader colRight;
  private ColumnHeader colPermission;
  private Button btnExit;

  public SecurityAccessRightsReportForm() => this.InitializeComponent();

  public void ShowReport(List<List<string>> report)
  {
    this.FillReport(report);
    int num = (int) this.ShowDialog();
  }

  private void FillReport(List<List<string>> report)
  {
    this.lvReport.Items.Clear();
    this.lvReport.BeginUpdate();
    try
    {
      for (int index1 = 0; index1 < report.Count; ++index1)
      {
        ListViewItem listViewItem = new ListViewItem(report[index1][0]);
        for (int index2 = 1; index2 < report[index1].Count; ++index2)
          listViewItem.SubItems.Add(report[index1][index2]);
        this.lvReport.Items.Add(listViewItem);
      }
    }
    finally
    {
      this.lvReport.EndUpdate();
    }
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
    this.lvReport = new ListView();
    this.colObject = new ColumnHeader();
    this.colOwner = new ColumnHeader();
    this.colType = new ColumnHeader();
    this.colRight = new ColumnHeader();
    this.colPermission = new ColumnHeader();
    this.btnExit = new Button();
    this.SuspendLayout();
    this.lvReport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lvReport.Columns.AddRange(new ColumnHeader[5]
    {
      this.colObject,
      this.colOwner,
      this.colType,
      this.colRight,
      this.colPermission
    });
    this.lvReport.Location = new Point(12, 12);
    this.lvReport.MultiSelect = false;
    this.lvReport.Name = "lvReport";
    this.lvReport.Size = new Size(701, 216);
    this.lvReport.TabIndex = 0;
    this.lvReport.UseCompatibleStateImageBehavior = false;
    this.lvReport.View = View.Details;
    this.colObject.Text = "Объект";
    this.colObject.Width = 200;
    this.colOwner.Text = "Владелец прав";
    this.colOwner.Width = 100;
    this.colType.Text = "Тип владельца";
    this.colType.Width = 100;
    this.colRight.Text = "Право";
    this.colRight.Width = 120;
    this.colPermission.Text = "Разрешение";
    this.colPermission.Width = 150;
    this.btnExit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnExit.DialogResult = DialogResult.OK;
    this.btnExit.Location = new Point(638, 237);
    this.btnExit.Name = "btnExit";
    this.btnExit.Size = new Size(75, 23);
    this.btnExit.TabIndex = 1;
    this.btnExit.Text = "Выход";
    this.btnExit.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(725, 272);
    this.Controls.Add((Control) this.btnExit);
    this.Controls.Add((Control) this.lvReport);
    this.Name = nameof (SecurityAccessRightsReportForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Сводка по правам доступа";
    this.ResumeLayout(false);
  }
}
