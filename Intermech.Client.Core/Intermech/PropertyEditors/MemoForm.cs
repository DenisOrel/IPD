
// Type: Intermech.PropertyEditors.MemoForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Controls.SpellCheck;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for MemoForm.</summary>
public class MemoForm : Form
{
  private IContainer components;
  private Button btnOk;
  private Button btnCancel;
  private OpenFileDialog openFileDialog;
  private SaveFileDialog saveFileDialog;
  private Button btnLoad;
  private Button btnSave;
  private int maxMemoSize = CoreConsts.MaxMemoEditorSizeDefault;
  private string memo;
  private bool disableManualEdit;
  private RichTextBox textBox;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem cmiAddToDictionary;
  private bool readonlyFlag;
  private bool lockTextChanged;
  private int oldCursorPos = -1;
  private string oldText = "";

  public int MaxMemoSize
  {
    get => this.maxMemoSize;
    set => this.maxMemoSize = value;
  }

  public string Memo
  {
    get => this.memo;
    set => this.memo = value;
  }

  public bool DisableManualEdit
  {
    get => this.disableManualEdit;
    set => this.disableManualEdit = value;
  }

  public bool ReadonlyFlag
  {
    get => this.readonlyFlag;
    set => this.readonlyFlag = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public bool CanLoad
  {
    get => this.btnLoad.Visible;
    set => this.btnLoad.Visible = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public bool CanSave
  {
    get => this.btnSave.Visible;
    set => this.btnSave.Visible = value;
  }

  public MemoForm() => this.InitializeComponent();

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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MemoForm));
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.openFileDialog = new OpenFileDialog();
    this.saveFileDialog = new SaveFileDialog();
    this.btnLoad = new Button();
    this.btnSave = new Button();
    this.textBox = new RichTextBox();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this.cmiAddToDictionary = new ToolStripMenuItem();
    this.contextMenuStrip1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.openFileDialog.DefaultExt = "txt";
    componentResourceManager.ApplyResources((object) this.openFileDialog, "openFileDialog");
    this.openFileDialog.RestoreDirectory = true;
    this.saveFileDialog.DefaultExt = "txt";
    componentResourceManager.ApplyResources((object) this.saveFileDialog, "saveFileDialog");
    this.saveFileDialog.RestoreDirectory = true;
    componentResourceManager.ApplyResources((object) this.btnLoad, "btnLoad");
    this.btnLoad.Name = "btnLoad";
    this.btnLoad.Click += new EventHandler(this.btnLoad_Click);
    componentResourceManager.ApplyResources((object) this.btnSave, "btnSave");
    this.btnSave.Name = "btnSave";
    this.btnSave.Click += new EventHandler(this.btnSave_Click);
    componentResourceManager.ApplyResources((object) this.textBox, "textBox");
    this.textBox.ContextMenuStrip = this.contextMenuStrip1;
    this.textBox.Name = "textBox";
    this.textBox.TextChanged += new EventHandler(this.textBox_TextChanged);
    this.textBox.Enter += new EventHandler(this.textBox_Enter);
    this.textBox.Leave += new EventHandler(this.textBox_Leave);
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.cmiAddToDictionary
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    componentResourceManager.ApplyResources((object) this.contextMenuStrip1, "contextMenuStrip1");
    this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
    this.cmiAddToDictionary.Name = "cmiAddToDictionary";
    componentResourceManager.ApplyResources((object) this.cmiAddToDictionary, "cmiAddToDictionary");
    this.cmiAddToDictionary.Click += new EventHandler(this.cmiAddToDictionary_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.textBox);
    this.Controls.Add((Control) this.btnSave);
    this.Controls.Add((Control) this.btnLoad);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.Name = nameof (MemoForm);
    this.ShowInTaskbar = false;
    this.Closed += new EventHandler(this.MemoForm_Closed);
    this.Load += new EventHandler(this.MemoForm_Load);
    this.contextMenuStrip1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern int LockWindowUpdate(int hWnd);

  /// <summary>Проверка правописания</summary>
  private void SpellCheck(bool all)
  {
    if (!OptimizationSettings.SpellCheck)
      return;
    string text = this.textBox.Text;
    if (text.Length <= 0)
      return;
    SpellChecker.Instance.WorkInThread = true;
    SpellChecker.Instance.GerErrors(text, this.oldText, all ? -1 : this.textBox.SelectionStart + this.textBox.SelectionLength, this.oldCursorPos, new SpellChecker.SetErrorsDelegate(this.ShowErrors));
  }

  public void ShowErrors(List<ErrorStruct> errors, int startIndex, int length)
  {
    if (this.IsDisposed || this.Disposing)
      return;
    if (!this.InvokeRequired)
    {
      int selectionStart = this.textBox.SelectionStart;
      MemoForm.LockWindowUpdate(this.textBox.Handle.ToInt32());
      try
      {
        this.lockTextChanged = true;
        this.textBox.SelectionStart = startIndex;
        this.textBox.SelectionLength = length;
        this.textBox.SelectionColor = this.textBox.ForeColor;
        foreach (ErrorStruct error in errors)
        {
          this.textBox.SelectionStart = error.Start;
          this.textBox.SelectionLength = error.End - error.Start + 1;
          this.textBox.SelectionColor = Color.Red;
        }
      }
      finally
      {
        this.textBox.SelectionStart = selectionStart;
        this.textBox.SelectionLength = 0;
        MemoForm.LockWindowUpdate(0);
        this.textBox.Focus();
        this.lockTextChanged = false;
      }
    }
    else
      this.BeginInvoke((Delegate) new SpellChecker.SetErrorsDelegate(this.ShowErrors), (object) errors, (object) startIndex, (object) length);
  }

  private void textBox_TextChanged(object sender, EventArgs e)
  {
    if (this.lockTextChanged)
      return;
    if (OptimizationSettings.SpellCheck && this.textBox.Focused)
      this.SpellCheck(false);
    this.oldText = this.textBox.Text;
    this.oldCursorPos = this.textBox.SelectionStart + this.textBox.SelectionLength;
  }

  private void textBox_Enter(object sender, EventArgs e) => this.SpellCheck(true);

  private void textBox_Leave(object sender, EventArgs e)
  {
    MemoForm.LockWindowUpdate(this.textBox.Handle.ToInt32());
    int selectionStart = this.textBox.SelectionStart;
    this.textBox.SelectAll();
    this.textBox.SelectionColor = this.textBox.ForeColor;
    this.textBox.SelectionStart = selectionStart;
    this.textBox.SelectionLength = 0;
    MemoForm.LockWindowUpdate(0);
  }

  private void MemoForm_Load(object sender, EventArgs e)
  {
    this.textBox.MaxLength = this.maxMemoSize;
    this.textBox.Text = this.memo;
    FormStorage.LoadLayout((Control) this);
    this.btnOk.Enabled = !this.disableManualEdit && !this.readonlyFlag;
  }

  private void btnOk_Click(object sender, EventArgs e) => this.memo = this.textBox.Text;

  private void btnLoad_Click(object sender, EventArgs e)
  {
    if (this.openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    string fileName = this.openFileDialog.FileName;
    string str1 = string.Empty;
    try
    {
      using (StreamReader streamReader = new StreamReader(fileName, Encoding.Default))
      {
        string str2;
        while ((str2 = streamReader.ReadLine()) != null)
          str1 = str1 + str2 + Environment.NewLine;
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("Client.Core_82"));
    }
    this.textBox.Text = str1;
  }

  private void btnSave_Click(object sender, EventArgs e)
  {
    if (this.saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    string fileName = this.saveFileDialog.FileName;
    try
    {
      using (StreamWriter text = File.CreateText(fileName))
        text.WriteLine(this.textBox.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("Client.Core_82"));
    }
  }

  private void MemoForm_Closed(object sender, EventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
  {
    this.cmiAddToDictionary.Visible = this.textBox.SelectionColor == Color.Red;
  }

  private void cmiAddToDictionary_Click(object sender, EventArgs e) => this.AddToDictionary();

  private void AddToDictionary()
  {
    int selectionStart = this.textBox.SelectionStart;
    int selectionLength = this.textBox.SelectionLength;
    this.lockTextChanged = true;
    try
    {
      int num1 = selectionStart;
      while (this.textBox.SelectionColor == Color.Red)
      {
        --num1;
        if (num1 >= 0)
        {
          this.textBox.SelectionStart = num1;
          this.textBox.SelectionLength = selectionStart - num1;
        }
        else
          break;
      }
      int num2 = num1 + 1;
      this.textBox.SelectionStart = num2;
      int num3 = selectionStart - num2;
      while (this.textBox.SelectionColor == Color.Red)
      {
        ++num3;
        if (num2 + num3 <= this.textBox.TextLength)
          this.textBox.SelectionLength = num3;
        else
          break;
      }
      this.textBox.SelectionLength = num3 - 1;
      SpellChecker.Instance.Dict.UserFileAdd(this.textBox.SelectedText);
    }
    finally
    {
      this.textBox.SelectionStart = selectionStart;
      this.textBox.SelectionLength = selectionLength;
      this.textBox.Focus();
      this.lockTextChanged = false;
      this.SpellCheck(false);
    }
  }
}
