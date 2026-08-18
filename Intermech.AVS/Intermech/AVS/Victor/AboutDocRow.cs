// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.AboutDocRow
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using Intermech.Navigator.Controls;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

public class AboutDocRow : Form
{
  public TableData docRow_Curr;
  public ImDocument Document;
  private StringCollection attributes_name;
  private long ObjectId_Main = -1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelForButtons;
  internal Button bCancel;
  private Panel panel_Up;
  private Panel panel_Centr;
  private TabControl tabControl1;
  private TabPage tabPage_Attributes;
  private DataGridView dataGridView_Attrib;
  private DataGridViewTextBoxColumn Column_Name;
  private DataGridViewTextBoxColumn Column_Text;
  private DataGridViewTextBoxColumn Column_IPSAttributi;
  private DataGridViewTextBoxColumn Column_IPSAttributName;
  private Button button_MainObject;

  public AboutDocRow() => this.InitializeComponent();

  private void AboutDocRow_Load(object sender, EventArgs e) => this.draw_Attributes();

  private void draw_Attributes()
  {
    this.dataGridView_Attrib.Rows.Clear();
    if (this.Document != null)
    {
      this.tabPage_Attributes.Text = "Атрибуты документа";
      this.attributes_name = this.Document.GetAttributeNames(false);
      this.dataGridView_Attrib.Columns[3].Width = 0;
      this.dataGridView_Attrib.Columns[2].Width = 0;
      this.dataGridView_Attrib.Columns[1].Width = 800;
      this.dataGridView_Attrib.Columns[0].Width = 300;
      for (int index = 0; index < this.attributes_name.Count; ++index)
      {
        string attributeName = this.attributes_name[index];
        string attributeValue = this.Document.GetAttributeValue(attributeName, true);
        string[] strArray = new string[4]
        {
          attributeName,
          attributeValue,
          null,
          null
        };
        if (attributeName.StartsWith("OneDataVed"))
        {
          int length = attributeValue.IndexOf('=');
          if (length > 1)
          {
            string s = attributeValue.Substring(0, length);
            int attrTypeID = int.Parse(s);
            attributeValue.Substring(length + 1);
            string attributeTypeName = MetaDataHelper.GetAttributeTypeName(attrTypeID);
            strArray[2] = s;
            strArray[3] = attributeTypeName;
          }
        }
        if (attributeName == "ObjectIdIzd")
          this.ObjectId_Main = long.Parse(attributeValue);
        this.dataGridView_Attrib.Rows.Add((object[]) strArray);
      }
      this.dataGridView_Attrib.Sort(this.dataGridView_Attrib.Columns[0], ListSortDirection.Ascending);
    }
    else
    {
      this.tabPage_Attributes.Text = "Атрибуты записи";
      this.attributes_name = this.docRow_Curr.GetAttributeNames(false);
      for (int index = 0; index < this.attributes_name.Count; ++index)
      {
        string attributeName = this.attributes_name[index];
        string attributeValue = this.docRow_Curr.GetAttributeValue(attributeName, true);
        string[] strArray = new string[4]
        {
          attributeName,
          attributeValue,
          null,
          null
        };
        if (attributeName.StartsWith("OneDataVed"))
        {
          int length = attributeValue.IndexOf('=');
          if (length > 1)
          {
            string s = attributeValue.Substring(0, length);
            int attrTypeID = int.Parse(s);
            attributeValue.Substring(length + 1);
            string attributeTypeName = MetaDataHelper.GetAttributeTypeName(attrTypeID);
            strArray[2] = s;
            strArray[3] = attributeTypeName;
          }
        }
        if (attributeName == "ObjectIdIzd")
          this.ObjectId_Main = long.Parse(attributeValue);
        this.dataGridView_Attrib.Rows.Add((object[]) strArray);
      }
      this.dataGridView_Attrib.Sort(this.dataGridView_Attrib.Columns[0], ListSortDirection.Ascending);
    }
    if (this.ObjectId_Main != -1L)
      return;
    this.button_MainObject.Visible = false;
  }

  private void button_MainObject_Click(object sender, EventArgs e)
  {
    int num = (int) PropertiesWindow.Execute("Свойства объекта", "", this.ObjectId_Main, "ObjectProperties");
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
    this.panelForButtons = new Panel();
    this.button_MainObject = new Button();
    this.bCancel = new Button();
    this.panel_Up = new Panel();
    this.panel_Centr = new Panel();
    this.tabControl1 = new TabControl();
    this.tabPage_Attributes = new TabPage();
    this.dataGridView_Attrib = new DataGridView();
    this.Column_Name = new DataGridViewTextBoxColumn();
    this.Column_Text = new DataGridViewTextBoxColumn();
    this.Column_IPSAttributi = new DataGridViewTextBoxColumn();
    this.Column_IPSAttributName = new DataGridViewTextBoxColumn();
    this.panelForButtons.SuspendLayout();
    this.panel_Centr.SuspendLayout();
    this.tabControl1.SuspendLayout();
    this.tabPage_Attributes.SuspendLayout();
    ((ISupportInitialize) this.dataGridView_Attrib).BeginInit();
    this.SuspendLayout();
    this.panelForButtons.BorderStyle = BorderStyle.Fixed3D;
    this.panelForButtons.Controls.Add((Control) this.button_MainObject);
    this.panelForButtons.Controls.Add((Control) this.bCancel);
    this.panelForButtons.Dock = DockStyle.Bottom;
    this.panelForButtons.Location = new Point(0, 584);
    this.panelForButtons.Name = "panelForButtons";
    this.panelForButtons.Size = new Size(1175, 42);
    this.panelForButtons.TabIndex = 33;
    this.button_MainObject.Location = new Point(10, 6);
    this.button_MainObject.Name = "button_MainObject";
    this.button_MainObject.Size = new Size(244, 27);
    this.button_MainObject.TabIndex = 3;
    this.button_MainObject.Text = "Карточка ключевого объекта";
    this.button_MainObject.UseVisualStyleBackColor = true;
    this.button_MainObject.Click += new EventHandler(this.button_MainObject_Click);
    this.bCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(522, 6);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 2;
    this.bCancel.Text = "Cancel";
    this.bCancel.UseVisualStyleBackColor = true;
    this.panel_Up.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.panel_Up.Location = new Point(0, 1);
    this.panel_Up.Name = "panel_Up";
    this.panel_Up.Size = new Size(1175, 16 /*0x10*/);
    this.panel_Up.TabIndex = 34;
    this.panel_Centr.Controls.Add((Control) this.tabControl1);
    this.panel_Centr.Location = new Point(0, 23);
    this.panel_Centr.Name = "panel_Centr";
    this.panel_Centr.Size = new Size(1175, 544);
    this.panel_Centr.TabIndex = 35;
    this.tabControl1.Controls.Add((Control) this.tabPage_Attributes);
    this.tabControl1.Dock = DockStyle.Fill;
    this.tabControl1.Location = new Point(0, 0);
    this.tabControl1.Name = "tabControl1";
    this.tabControl1.SelectedIndex = 0;
    this.tabControl1.Size = new Size(1175, 544);
    this.tabControl1.TabIndex = 0;
    this.tabPage_Attributes.Controls.Add((Control) this.dataGridView_Attrib);
    this.tabPage_Attributes.Location = new Point(4, 22);
    this.tabPage_Attributes.Name = "tabPage_Attributes";
    this.tabPage_Attributes.Padding = new Padding(3);
    this.tabPage_Attributes.Size = new Size(1167, 518);
    this.tabPage_Attributes.TabIndex = 0;
    this.tabPage_Attributes.Text = "Атрибуты в записи";
    this.tabPage_Attributes.UseVisualStyleBackColor = true;
    this.dataGridView_Attrib.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView_Attrib.Columns.AddRange((DataGridViewColumn) this.Column_Name, (DataGridViewColumn) this.Column_Text, (DataGridViewColumn) this.Column_IPSAttributi, (DataGridViewColumn) this.Column_IPSAttributName);
    this.dataGridView_Attrib.Location = new Point(3, 3);
    this.dataGridView_Attrib.Name = "dataGridView_Attrib";
    this.dataGridView_Attrib.Size = new Size(1161, 571);
    this.dataGridView_Attrib.TabIndex = 0;
    this.Column_Name.HeaderText = "Имя атрибута";
    this.Column_Name.Name = "Column_Name";
    this.Column_Name.ReadOnly = true;
    this.Column_Name.Width = 150;
    this.Column_Text.HeaderText = "Текст атрибута";
    this.Column_Text.Name = "Column_Text";
    this.Column_Text.ReadOnly = true;
    this.Column_Text.Width = 430;
    this.Column_IPSAttributi.HeaderText = "IPS Atr";
    this.Column_IPSAttributi.Name = "Column_IPSAttributi";
    this.Column_IPSAttributi.Width = 65;
    this.Column_IPSAttributName.HeaderText = "IPS атрибут имя";
    this.Column_IPSAttributName.Name = "Column_IPSAttributName";
    this.Column_IPSAttributName.Width = 430;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(1175, 626);
    this.Controls.Add((Control) this.panel_Centr);
    this.Controls.Add((Control) this.panel_Up);
    this.Controls.Add((Control) this.panelForButtons);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AboutDocRow);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Свойства записи";
    this.Load += new EventHandler(this.AboutDocRow_Load);
    this.panelForButtons.ResumeLayout(false);
    this.panel_Centr.ResumeLayout(false);
    this.tabControl1.ResumeLayout(false);
    this.tabPage_Attributes.ResumeLayout(false);
    ((ISupportInitialize) this.dataGridView_Attrib).EndInit();
    this.ResumeLayout(false);
  }
}
