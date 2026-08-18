
// Type: Intermech.Client.Core.UCFilesComparison
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class UCFilesComparison : UserControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected SplitContainer splitContainer1;
  private Label lblDate1;
  private Label lblSize1;
  private Label label7;
  private Label label5;
  private Label lblFile1Name;
  private Label lblObj1Name;
  private Label lblDate2;
  private Label lblSize2;
  private Label label8;
  private Label label6;
  private Label lblFile2Name;
  private Label lblObj2Name;
  private Label label2;
  private Label label1;
  private Label label3;
  private Label label4;
  protected Panel panel1;

  public UCFilesComparison() => this.InitializeComponent();

  public void Init(ObjectFileInfo fileInfo1, ObjectFileInfo fileInfo2)
  {
    if (Math.Abs(fileInfo1.ObjectId) == fileInfo2.ObjectId)
    {
      this.lblObj1Name.Text = fileInfo1.ObjectCaption + " (рабочая копия)";
      this.lblObj2Name.Text = fileInfo2.ObjectCaption + " (архивная копия)";
    }
    else
    {
      this.lblObj1Name.Text = fileInfo1.ObjectCaption;
      this.lblObj2Name.Text = fileInfo2.ObjectCaption;
    }
    this.lblFile1Name.Text = fileInfo1.FileName;
    this.lblSize1.Text = fileInfo1.Size;
    this.lblDate1.Text = fileInfo1.ModificationDate;
    this.lblFile2Name.Text = fileInfo2.FileName;
    this.lblSize2.Text = fileInfo2.Size;
    this.lblDate2.Text = fileInfo2.ModificationDate;
    if (fileInfo1.Size != fileInfo2.Size)
    {
      this.lblSize1.ForeColor = Color.Red;
      this.lblSize2.ForeColor = Color.Red;
    }
    if (!(fileInfo1.ModificationDate != fileInfo2.ModificationDate))
      return;
    this.lblDate1.ForeColor = Color.Red;
    this.lblDate2.ForeColor = Color.Red;
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
    this.components = (IContainer) new System.ComponentModel.Container();
    this.AutoScaleMode = AutoScaleMode.Font;
    this.splitContainer1 = new SplitContainer();
    this.label2 = new Label();
    this.label1 = new Label();
    this.lblDate1 = new Label();
    this.lblSize1 = new Label();
    this.label7 = new Label();
    this.label5 = new Label();
    this.lblFile1Name = new Label();
    this.lblObj1Name = new Label();
    this.label3 = new Label();
    this.label4 = new Label();
    this.lblDate2 = new Label();
    this.lblSize2 = new Label();
    this.label8 = new Label();
    this.label6 = new Label();
    this.lblFile2Name = new Label();
    this.lblObj2Name = new Label();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.SuspendLayout();
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.BackColor = SystemColors.ControlLight;
    this.splitContainer1.Location = new Point(0, 0);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.AutoScroll = true;
    this.splitContainer1.Panel1.BackColor = SystemColors.Control;
    this.splitContainer1.Panel1.Controls.Add((Control) this.label2);
    this.splitContainer1.Panel1.Controls.Add((Control) this.label1);
    this.splitContainer1.Panel1.Controls.Add((Control) this.lblDate1);
    this.splitContainer1.Panel1.Controls.Add((Control) this.lblSize1);
    this.splitContainer1.Panel1.Controls.Add((Control) this.label7);
    this.splitContainer1.Panel1.Controls.Add((Control) this.label5);
    this.splitContainer1.Panel1.Controls.Add((Control) this.lblFile1Name);
    this.splitContainer1.Panel1.Controls.Add((Control) this.lblObj1Name);
    this.splitContainer1.Panel2.AutoScroll = true;
    this.splitContainer1.Panel2.BackColor = SystemColors.Control;
    this.splitContainer1.Panel2.Controls.Add((Control) this.label3);
    this.splitContainer1.Panel2.Controls.Add((Control) this.label4);
    this.splitContainer1.Panel2.Controls.Add((Control) this.lblDate2);
    this.splitContainer1.Panel2.Controls.Add((Control) this.lblSize2);
    this.splitContainer1.Panel2.Controls.Add((Control) this.label8);
    this.splitContainer1.Panel2.Controls.Add((Control) this.label6);
    this.splitContainer1.Panel2.Controls.Add((Control) this.lblFile2Name);
    this.splitContainer1.Panel2.Controls.Add((Control) this.lblObj2Name);
    this.splitContainer1.Size = new Size(694, 161);
    this.splitContainer1.SplitterDistance = 346;
    this.splitContainer1.TabIndex = 0;
    this.label2.AutoSize = true;
    this.label2.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label2.Location = new Point(6, 45);
    this.label2.Name = "label2";
    this.label2.Size = new Size(39, 13);
    this.label2.TabIndex = 7;
    this.label2.Text = "Файл:";
    this.label1.AutoSize = true;
    this.label1.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label1.Location = new Point(6, 8);
    this.label1.Name = "label1";
    this.label1.Size = new Size(55, 13);
    this.label1.TabIndex = 6;
    this.label1.Text = "Объект:";
    this.lblDate1.AutoSize = true;
    this.lblDate1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.lblDate1.Location = new Point(126, 94);
    this.lblDate1.Name = "lblDate1";
    this.lblDate1.Size = new Size(35, 13);
    this.lblDate1.TabIndex = 5;
    this.lblDate1.Text = "label2";
    this.lblSize1.AutoSize = true;
    this.lblSize1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.lblSize1.Location = new Point(56, 81);
    this.lblSize1.Name = "lblSize1";
    this.lblSize1.Size = new Size(35, 13);
    this.lblSize1.TabIndex = 4;
    this.lblSize1.Text = "label1";
    this.label7.AutoSize = true;
    this.label7.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label7.Location = new Point(3, 94);
    this.label7.Name = "label7";
    this.label7.Size = new Size(123, 13);
    this.label7.TabIndex = 3;
    this.label7.Text = "Дата модификации:";
    this.label5.AutoSize = true;
    this.label5.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label5.Location = new Point(3, 81);
    this.label5.Name = "label5";
    this.label5.Size = new Size(52, 13);
    this.label5.TabIndex = 2;
    this.label5.Text = "Размер:";
    this.lblFile1Name.AutoSize = true;
    this.lblFile1Name.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.lblFile1Name.Location = new Point(6, 58);
    this.lblFile1Name.Name = "lblFile1Name";
    this.lblFile1Name.Size = new Size(56, 13);
    this.lblFile1Name.TabIndex = 1;
    this.lblFile1Name.Text = "File1Name";
    this.lblObj1Name.AutoSize = true;
    this.lblObj1Name.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.lblObj1Name.Location = new Point(6, 22);
    this.lblObj1Name.Name = "lblObj1Name";
    this.lblObj1Name.Size = new Size(57, 13);
    this.lblObj1Name.TabIndex = 0;
    this.lblObj1Name.Text = "Obj1Name";
    this.label3.AutoSize = true;
    this.label3.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label3.Location = new Point(3, 45);
    this.label3.Name = "label3";
    this.label3.Size = new Size(39, 13);
    this.label3.TabIndex = 10;
    this.label3.Text = "Файл:";
    this.label4.AutoSize = true;
    this.label4.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label4.Location = new Point(3, 8);
    this.label4.Name = "label4";
    this.label4.Size = new Size(55, 13);
    this.label4.TabIndex = 9;
    this.label4.Text = "Объект:";
    this.lblDate2.AutoSize = true;
    this.lblDate2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.lblDate2.Location = new Point(126, 94);
    this.lblDate2.Name = "lblDate2";
    this.lblDate2.Size = new Size(35, 13);
    this.lblDate2.TabIndex = 5;
    this.lblDate2.Text = "label4";
    this.lblSize2.AutoSize = true;
    this.lblSize2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.lblSize2.Location = new Point(55, 81);
    this.lblSize2.Name = "lblSize2";
    this.lblSize2.Size = new Size(35, 13);
    this.lblSize2.TabIndex = 4;
    this.lblSize2.Text = "label3";
    this.label8.AutoSize = true;
    this.label8.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label8.ForeColor = SystemColors.ControlText;
    this.label8.Location = new Point(3, 94);
    this.label8.Name = "label8";
    this.label8.Size = new Size(123, 13);
    this.label8.TabIndex = 3;
    this.label8.Text = "Дата модификации:";
    this.label6.AutoSize = true;
    this.label6.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label6.Location = new Point(3, 81);
    this.label6.Name = "label6";
    this.label6.Size = new Size(52, 13);
    this.label6.TabIndex = 2;
    this.label6.Text = "Размер:";
    this.lblFile2Name.AutoSize = true;
    this.lblFile2Name.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.lblFile2Name.Location = new Point(3, 58);
    this.lblFile2Name.Name = "lblFile2Name";
    this.lblFile2Name.Size = new Size(56, 13);
    this.lblFile2Name.TabIndex = 1;
    this.lblFile2Name.Text = "File2Name";
    this.lblObj2Name.AutoSize = true;
    this.lblObj2Name.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.lblObj2Name.Location = new Point(3, 21);
    this.lblObj2Name.Name = "lblObj2Name";
    this.lblObj2Name.Size = new Size(57, 13);
    this.lblObj2Name.TabIndex = 0;
    this.lblObj2Name.Text = "Obj2Name";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.ActiveBorder;
    this.ClientSize = new Size(694, 206);
    this.Controls.Add((Control) this.splitContainer1);
    this.Name = nameof (UCFilesComparison);
    this.Text = "Сравнение файлов";
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel1.PerformLayout();
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.Panel2.PerformLayout();
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
