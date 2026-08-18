
// Type: Intermech.Navigator.Conditions.InGlobalIndexForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

public class InGlobalIndexForm : ConditionForm
{
  private GlobalIndexSearchValue _params;
  private bool _selfCheck;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label2;
  private TextBox tbSearchString;
  private CheckBox cbStemmedWords;
  private CheckBox cbSubstringSearch;
  private Button bOK;
  private Button bCancel;

  public InGlobalIndexForm() => this.InitializeComponent();

  protected override void OnInitialized()
  {
    this._params = this.conditionStructure.Value != null ? (GlobalIndexSearchValue) this.conditionStructure.Value : new GlobalIndexSearchValue(string.Empty, GlobalIndexSearchOptions.None);
    this.RefreshControls();
  }

  private void RefreshControls()
  {
    this.tbSearchString.Text = this._params.Value;
    try
    {
      this._selfCheck = true;
      this.cbSubstringSearch.Checked = (this._params.SearchOptions & GlobalIndexSearchOptions.SubstringSearch) == GlobalIndexSearchOptions.SubstringSearch;
      this.cbStemmedWords.Checked = (this._params.SearchOptions & GlobalIndexSearchOptions.StemmedWords) == GlobalIndexSearchOptions.StemmedWords;
    }
    finally
    {
      this._selfCheck = false;
    }
  }

  public override ConditionStructure Result
  {
    get
    {
      this.conditionStructure.RelationalOperator = RelationalOperators.InGlobalIndex;
      this.conditionStructure.Value = (object) this._params;
      return this.conditionStructure;
    }
  }

  private void SetOKButton() => this.bOK.Enabled = this.tbSearchString.Text.Length > 0;

  private void tbSearchString_TextChanged(object sender, EventArgs e)
  {
    this._params.Value = this.tbSearchString.Text;
    this.SetOKButton();
  }

  private void cbSubstringSearch_CheckedChanged(object sender, EventArgs e)
  {
    if (this._selfCheck)
      return;
    this.SetOption(this.cbSubstringSearch.Checked, GlobalIndexSearchOptions.SubstringSearch);
  }

  private void cbStemmedWords_CheckedChanged(object sender, EventArgs e)
  {
    if (this._selfCheck)
      return;
    this.SetOption(this.cbStemmedWords.Checked, GlobalIndexSearchOptions.StemmedWords);
  }

  private void SetOption(bool isChecked, GlobalIndexSearchOptions option)
  {
    if (isChecked)
      this._params.SearchOptions |= option;
    else
      this._params.SearchOptions &= ~option;
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
    this.label2 = new Label();
    this.tbSearchString = new TextBox();
    this.cbStemmedWords = new CheckBox();
    this.cbSubstringSearch = new CheckBox();
    this.bOK = new Button();
    this.bCancel = new Button();
    this.SuspendLayout();
    this.label2.AutoSize = true;
    this.label2.Location = new Point(35, 25);
    this.label2.Name = "label2";
    this.label2.Size = new Size(91, 13);
    this.label2.TabIndex = 19;
    this.label2.Text = "Искомая строка";
    this.tbSearchString.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbSearchString.Location = new Point(38, 41);
    this.tbSearchString.Name = "tbSearchString";
    this.tbSearchString.Size = new Size(320, 20);
    this.tbSearchString.TabIndex = 24;
    this.tbSearchString.TextChanged += new EventHandler(this.tbSearchString_TextChanged);
    this.cbStemmedWords.AutoSize = true;
    this.cbStemmedWords.Location = new Point(38, 99);
    this.cbStemmedWords.Name = "cbStemmedWords";
    this.cbStemmedWords.Size = new Size(215, 17);
    this.cbStemmedWords.TabIndex = 23;
    this.cbStemmedWords.Text = "Искать с учётом общей словоформы";
    this.cbStemmedWords.UseVisualStyleBackColor = true;
    this.cbStemmedWords.CheckedChanged += new EventHandler(this.cbStemmedWords_CheckedChanged);
    this.cbSubstringSearch.AutoSize = true;
    this.cbSubstringSearch.Location = new Point(38, 76);
    this.cbSubstringSearch.Name = "cbSubstringSearch";
    this.cbSubstringSearch.Size = new Size(118, 17);
    this.cbSubstringSearch.TabIndex = 22;
    this.cbSubstringSearch.Text = "Искать подстроку";
    this.cbSubstringSearch.UseVisualStyleBackColor = true;
    this.cbSubstringSearch.CheckedChanged += new EventHandler(this.cbSubstringSearch_CheckedChanged);
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(110, 138);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 20;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(237, 138);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 21;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(370, 177);
    this.Controls.Add((Control) this.tbSearchString);
    this.Controls.Add((Control) this.cbStemmedWords);
    this.Controls.Add((Control) this.cbSubstringSearch);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.label2);
    this.MinimumSize = new Size(300, 200);
    this.Name = nameof (InGlobalIndexForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Поиск в общем индексе";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
