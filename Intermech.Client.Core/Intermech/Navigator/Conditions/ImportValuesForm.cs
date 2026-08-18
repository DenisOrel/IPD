
// Type: Intermech.Navigator.Conditions.ImportValuesForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Client.Core.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

public class ImportValuesForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button bCancel;
  private Button bAdd;
  private RichTextBox richTextBox1;
  private Button bSelectFile;
  private ComboBox cbSeparator;
  private Label label1;
  private ToolTip toolTip1;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem miCut;
  private ToolStripMenuItem miCopy;
  private ToolStripMenuItem miPaste;
  private ToolStripMenuItem miSelectAll;
  private OpenFileDialog openFileDialog1;

  public ImportValuesForm()
  {
    this.InitializeComponent();
    this.FillSeparators();
    FormStorage.LoadLayout((Control) this);
  }

  private void Paste(object sender, EventArgs e)
  {
    if (!Clipboard.ContainsText())
      return;
    this.richTextBox1.Paste();
  }

  private void Copy(object sender, EventArgs e)
  {
    if (string.IsNullOrEmpty(this.richTextBox1.SelectedText))
      return;
    Clipboard.SetText(this.richTextBox1.SelectedText.Replace("\n", "\r\n"));
  }

  private void Cut(object sender, EventArgs e) => this.richTextBox1.Cut();

  private void FillSeparators()
  {
    this.cbSeparator.Items.Clear();
    this.cbSeparator.Items.AddRange((object[]) new ImportValuesForm.SeparatorItem[6]
    {
      new ImportValuesForm.SeparatorItem("Перевод строки", "\n"),
      new ImportValuesForm.SeparatorItem("Символ табуляции", "\t"),
      new ImportValuesForm.SeparatorItem("Пробел", " "),
      new ImportValuesForm.SeparatorItem("Точка с запятой (;)", ";"),
      new ImportValuesForm.SeparatorItem("Запятая (,)", ","),
      new ImportValuesForm.SeparatorItem("Точка (.)", ".")
    });
    this.cbSeparator.SelectedIndex = 0;
  }

  public string[] Values
  {
    get
    {
      if (string.IsNullOrEmpty(this.richTextBox1.Text))
        return (string[]) null;
      return this.richTextBox1.Text.Split(new string[1]
      {
        ((ImportValuesForm.SeparatorItem) this.cbSeparator.SelectedItem).Value
      }, StringSplitOptions.RemoveEmptyEntries);
    }
  }

  private void SelectFile_Click(object sender, EventArgs e)
  {
    if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
      return;
    this.richTextBox1.Clear();
    string fileName = this.openFileDialog1.FileName;
    Encoding type = Encoding.Default;
    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
      type = this.GetType(fs);
    this.richTextBox1.Lines = File.ReadAllLines(fileName, type);
  }

  public Encoding GetType(FileStream fs)
  {
    new byte[3]{ byte.MaxValue, (byte) 254, (byte) 65 };
    byte[] numArray = new byte[3]
    {
      (byte) 254,
      byte.MaxValue,
      (byte) 0
    };
    new byte[3]{ (byte) 239, (byte) 187, (byte) 191 };
    Encoding type = Encoding.Default;
    BinaryReader binaryReader = new BinaryReader((Stream) fs, Encoding.Default);
    int result;
    int.TryParse(fs.Length.ToString(), out result);
    byte[] data = binaryReader.ReadBytes(result);
    if (data.Length == 0)
      return type;
    if (this.IsUTF8Bytes(data) || data[0] == (byte) 239 && data[1] == (byte) 187 && data[2] == (byte) 191)
      type = Encoding.UTF8;
    else if (data[0] == (byte) 254 && data[1] == byte.MaxValue && data[2] == (byte) 0)
      type = Encoding.BigEndianUnicode;
    else if (data[0] == byte.MaxValue && data[1] == (byte) 254 && data[2] == (byte) 65)
      type = Encoding.Unicode;
    binaryReader.Close();
    return type;
  }

  private bool IsUTF8Bytes(byte[] data)
  {
    int num1 = 1;
    for (int index = 0; index < data.Length; ++index)
    {
      byte num2 = data[index];
      if (num1 == 1)
      {
        if (num2 >= (byte) 128 /*0x80*/)
        {
          while (((int) (num2 <<= 1) & 128 /*0x80*/) != 0)
            ++num1;
          if (num1 == 1 || num1 > 6)
            return false;
        }
      }
      else
      {
        if (((int) num2 & 192 /*0xC0*/) != 128 /*0x80*/)
          return false;
        --num1;
      }
    }
    if (num1 > 1)
      throw new Exception("Error byte format");
    return true;
  }

  private void ImportValuesForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void SelectAll_Click(object sender, EventArgs e) => this.richTextBox1.SelectAll();

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
    this.bCancel = new Button();
    this.bAdd = new Button();
    this.richTextBox1 = new RichTextBox();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this.miCut = new ToolStripMenuItem();
    this.miCopy = new ToolStripMenuItem();
    this.miPaste = new ToolStripMenuItem();
    this.miSelectAll = new ToolStripMenuItem();
    this.bSelectFile = new Button();
    this.cbSeparator = new ComboBox();
    this.label1 = new Label();
    this.toolTip1 = new ToolTip(this.components);
    this.openFileDialog1 = new OpenFileDialog();
    this.contextMenuStrip1.SuspendLayout();
    this.SuspendLayout();
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(162, 214);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 8;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bAdd.DialogResult = DialogResult.OK;
    this.bAdd.Location = new Point(35, 214);
    this.bAdd.Name = "bAdd";
    this.bAdd.Size = new Size(121, 27);
    this.bAdd.TabIndex = 7;
    this.bAdd.Text = "Добавить";
    this.bAdd.UseVisualStyleBackColor = true;
    this.richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.richTextBox1.ContextMenuStrip = this.contextMenuStrip1;
    this.richTextBox1.Location = new Point(12, 64 /*0x40*/);
    this.richTextBox1.Name = "richTextBox1";
    this.richTextBox1.Size = new Size(271, 144 /*0x90*/);
    this.richTextBox1.TabIndex = 6;
    this.richTextBox1.Text = "";
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.miCut,
      (ToolStripItem) this.miCopy,
      (ToolStripItem) this.miPaste,
      (ToolStripItem) this.miSelectAll
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    this.contextMenuStrip1.Size = new Size(191, 92);
    this.miCut.Name = "miCut";
    this.miCut.ShortcutKeys = Keys.X | Keys.Control;
    this.miCut.Size = new Size(190, 22);
    this.miCut.Text = "Вырезать";
    this.miCut.Click += new EventHandler(this.Cut);
    this.miCopy.Name = "miCopy";
    this.miCopy.ShortcutKeys = Keys.C | Keys.Control;
    this.miCopy.Size = new Size(190, 22);
    this.miCopy.Text = "Копировать";
    this.miCopy.Click += new EventHandler(this.Copy);
    this.miPaste.Name = "miPaste";
    this.miPaste.ShortcutKeys = Keys.V | Keys.Control;
    this.miPaste.Size = new Size(190, 22);
    this.miPaste.Text = "Вставить";
    this.miPaste.Click += new EventHandler(this.Paste);
    this.miSelectAll.Name = "miSelectAll";
    this.miSelectAll.ShortcutKeys = Keys.A | Keys.Control;
    this.miSelectAll.Size = new Size(190, 22);
    this.miSelectAll.Text = "Выделить все";
    this.miSelectAll.Click += new EventHandler(this.SelectAll_Click);
    this.bSelectFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bSelectFile.Image = (Image) Resources.folder_out;
    this.bSelectFile.Location = new Point(240 /*0xF0*/, 15);
    this.bSelectFile.Name = "bSelectFile";
    this.bSelectFile.Size = new Size(43, 43);
    this.bSelectFile.TabIndex = 5;
    this.toolTip1.SetToolTip((Control) this.bSelectFile, "Выбрать файл со значениями");
    this.bSelectFile.UseVisualStyleBackColor = true;
    this.bSelectFile.Click += new EventHandler(this.SelectFile_Click);
    this.cbSeparator.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbSeparator.FormattingEnabled = true;
    this.cbSeparator.Location = new Point(12, 37);
    this.cbSeparator.Name = "cbSeparator";
    this.cbSeparator.Size = new Size(213, 21);
    this.cbSeparator.TabIndex = 9;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(7, 21);
    this.label1.Name = "label1";
    this.label1.Size = new Size(126, 13);
    this.label1.TabIndex = 10;
    this.label1.Text = "Разделитель значений:";
    this.openFileDialog1.DefaultExt = "txt";
    this.openFileDialog1.FileName = "openFileDialog1";
    this.openFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
    this.openFileDialog1.RestoreDirectory = true;
    this.openFileDialog1.Title = "Выберите файл для импорта";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(295, 253);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.cbSeparator);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.bAdd);
    this.Controls.Add((Control) this.richTextBox1);
    this.Controls.Add((Control) this.bSelectFile);
    this.MinimumSize = new Size(311, 292);
    this.Name = nameof (ImportValuesForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Импорт значений";
    this.FormClosing += new FormClosingEventHandler(this.ImportValuesForm_FormClosing);
    this.contextMenuStrip1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private class SeparatorItem
  {
    public string Caption { get; }

    public string Value { get; }

    public SeparatorItem(string caption, string value)
    {
      this.Caption = caption;
      this.Value = value;
    }

    public override string ToString() => this.Caption;
  }
}
