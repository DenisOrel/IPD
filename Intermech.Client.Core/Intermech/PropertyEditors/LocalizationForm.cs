
// Type: Intermech.PropertyEditors.LocalizationForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class LocalizationForm : Form
{
  private bool blockOnCheck;
  private string langs = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnOk;
  private Button btnCancel;
  private CheckedListBox languageCLB;
  private Label label1;

  public LocalizationForm() => this.InitializeComponent();

  public DialogResult ExecuteDialog(ref string languages)
  {
    this.langs = languages;
    int num = (int) this.ShowDialog();
    if (num != 1)
      return (DialogResult) num;
    languages = this.langs;
    return (DialogResult) num;
  }

  private void LocalizationForm_Load(object sender, EventArgs e) => this.FillCheckedListBox();

  private void FillCheckedListBox()
  {
    this.languageCLB.Items.Clear();
    this.languageCLB.Items.Add((object) new LanguageObj(LocalizationHolder.rm.GetString("Client.Core_116"), string.Empty), this.langs == string.Empty ? CheckState.Checked : CheckState.Unchecked);
    this.blockOnCheck = true;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBLanguageCollection languageCollection = sessionKeeper.Session.GetLanguageCollection();
        if (languageCollection == null)
          return;
        foreach (DataRow row in (InternalDataCollectionBase) languageCollection.Select("F_LANGUAGE_NAME").Rows)
          this.languageCLB.Items.Add((object) new LanguageObj(Convert.ToString(row["F_LANGUAGE_NAME"]), Convert.ToString(row["F_LANGUAGE_ID"])), this.IsChecked(this.langs, Convert.ToString(row["F_LANGUAGE_ID"])));
      }
    }
    finally
    {
      this.blockOnCheck = false;
    }
  }

  private CheckState IsChecked(string langs, string lang)
  {
    if (langs == string.Empty)
      return CheckState.Checked;
    if (lang == string.Empty)
      return CheckState.Unchecked;
    int num = (int) lang[0];
    return langs.IndexOf(lang) == -1 ? CheckState.Unchecked : CheckState.Checked;
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
    if (this.languageCLB.CheckedIndices.Count == 0)
    {
      int num = (int) IMMessageBox.Show(MessageDialogs.msgWarning, LocalizationHolder.rm.GetString("Client.Core_117"), MessageBoxButtons.OK, IMMessageBoxImage.Warning);
      this.DialogResult = DialogResult.None;
    }
    else
    {
      this.langs = string.Empty;
      if (this.languageCLB.GetItemChecked(0))
        return;
      for (int index = 1; index < this.languageCLB.Items.Count; ++index)
      {
        if (this.languageCLB.GetItemChecked(index))
          this.langs += ((LanguageObj) this.languageCLB.Items[index]).Id;
      }
    }
  }

  private void languageCLB_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    if (this.blockOnCheck)
      return;
    if (e.NewValue == CheckState.Checked)
    {
      if (!(((LanguageObj) this.languageCLB.Items[e.Index]).Id == string.Empty))
        return;
      this.blockOnCheck = true;
      try
      {
        for (int index = 0; index < this.languageCLB.Items.Count; ++index)
        {
          if (((LanguageObj) this.languageCLB.Items[index]).Id != string.Empty)
            this.languageCLB.SetItemChecked(index, true);
        }
      }
      finally
      {
        this.blockOnCheck = false;
      }
    }
    else
    {
      if (e.Index == 0 || !this.languageCLB.GetItemChecked(0))
        return;
      this.blockOnCheck = true;
      try
      {
        this.languageCLB.SetItemChecked(0, false);
      }
      finally
      {
        this.blockOnCheck = false;
      }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LocalizationForm));
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.languageCLB = new CheckedListBox();
    this.label1 = new Label();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.languageCLB, "languageCLB");
    this.languageCLB.CheckOnClick = true;
    this.languageCLB.FormattingEnabled = true;
    this.languageCLB.Name = "languageCLB";
    this.languageCLB.ItemCheck += new ItemCheckEventHandler(this.languageCLB_ItemCheck);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.languageCLB);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.Name = nameof (LocalizationForm);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.LocalizationForm_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
