
// Type: Intermech.Navigator.Controls.LevelsNumForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>Форма для выбора кол-ва уровней, которые надо развернуть</summary>
public class LevelsNumForm : Form
{
  private bool _showLevelBreak;
  private int _levelsBreakOldValue = 1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel Panel2;
  private Button CancButton;
  private Button OkButton;
  private NumericUpDown LevelsEdit;
  private Label LevelsLabel;
  private RadioButton LevelsNumRadioButton;
  private RadioButton LevelsBreakRadioButton;
  private GroupBox groupBox1;
  private RadioButton AllLevelsRadioButton;
  private NumericUpDown LevelsBreakEdit;
  private Label LevelsBreakLabel;

  public static LevelsNumForm.QueryResult Query(bool showLevelBreak = false, int currentLevel = 1)
  {
    using (LevelsNumForm levelsNumForm = new LevelsNumForm(showLevelBreak, currentLevel))
    {
      if (levelsNumForm.ShowDialog() == DialogResult.OK)
        return new LevelsNumForm.QueryResult(levelsNumForm.Result, levelsNumForm.Result != LevelsNumForm.ResultType.LevelsBreak ? levelsNumForm.LevelsNum : levelsNumForm.LevelsBreak);
    }
    return (LevelsNumForm.QueryResult) null;
  }

  public static LevelsNumForm.QueryResult QueryForComposition(bool showLevelBreak = false, int currentLevel = 1)
  {
    using (LevelsNumForm levelsNumForm = new LevelsNumForm(showLevelBreak, currentLevel))
    {
      levelsNumForm.ForComposition();
      if (levelsNumForm.ShowDialog() == DialogResult.OK)
        return new LevelsNumForm.QueryResult(levelsNumForm.Result, levelsNumForm.Result != LevelsNumForm.ResultType.LevelsBreak ? levelsNumForm.LevelsNum : levelsNumForm.LevelsBreak);
    }
    return (LevelsNumForm.QueryResult) null;
  }

  public LevelsNumForm(bool showLevelBreak = false, int currentLevel = 1)
  {
    this.InitializeComponent();
    this._showLevelBreak = showLevelBreak;
    if (!this._showLevelBreak)
    {
      this.LevelsBreakEdit.Enabled = false;
      this.LevelsBreakEdit.Visible = false;
      this.LevelsBreakRadioButton.Enabled = false;
      this.LevelsBreakRadioButton.Visible = false;
      this.LevelsBreakLabel.Visible = false;
      Size size = this.Size;
      int width = size.Width;
      size = this.Size;
      int height = size.Height - (this.AllLevelsRadioButton.Location.Y - this.LevelsBreakRadioButton.Location.Y);
      this.Size = new Size(width, height);
    }
    else
      this.LevelsBreakEdit.Minimum = (Decimal) (currentLevel + 1);
  }

  public int LevelsNum
  {
    get
    {
      int levelsNum = (int) this.LevelsEdit.Value;
      if (this.AllLevelsRadioButton.Checked)
        levelsNum *= -1;
      return levelsNum;
    }
    set
    {
      this.LevelsEdit.Value = (Decimal) Math.Abs(value);
      if (value >= 0)
        return;
      this.AllLevelsRadioButton.Checked = true;
    }
  }

  public int LevelsBreak
  {
    [DebuggerStepThrough] get => (int) this.LevelsBreakEdit.Value;
    [DebuggerStepThrough] set => this.LevelsBreakEdit.Value = (Decimal) Math.Abs(value);
  }

  public LevelsNumForm.ResultType Result
  {
    get
    {
      if (this.LevelsNumRadioButton.Checked)
        return LevelsNumForm.ResultType.Levels;
      return this.LevelsBreakRadioButton.Checked ? LevelsNumForm.ResultType.LevelsBreak : LevelsNumForm.ResultType.All;
    }
    set
    {
      switch (value)
      {
        case LevelsNumForm.ResultType.Levels:
          this.LevelsNumRadioButton.Checked = true;
          break;
        case LevelsNumForm.ResultType.LevelsBreak:
          if (!this.LevelsBreakRadioButton.Visible)
            break;
          this.LevelsBreakRadioButton.Checked = true;
          break;
        default:
          this.AllLevelsRadioButton.Checked = true;
          break;
      }
    }
  }

  private void UpdateLevelsLabel()
  {
    int num = (int) (this.LevelsEdit.Value % 10M);
    this.LevelsLabel.Text = num != 1 ? (num <= 1 || num >= 5 ? "уровней" : "уровня") : "уровень";
    this.LevelsNumRadioButton.Checked = true;
  }

  private void UpdateLevelsBreakEditLabel()
  {
    int num = (int) (this.LevelsBreakEdit.Value % 10M);
    this.LevelsBreakLabel.Text = num != 1 ? (num <= 1 || num >= 5 ? "уровней от корня" : "уровня от корня") : "уровень от корня";
    this.LevelsBreakRadioButton.Checked = true;
  }

  private void UnitsEdit_ValueChanged(object sender, EventArgs e) => this.UpdateLevelsLabel();

  private void LevelsEdit_KeyDown(object sender, KeyEventArgs e) => this.UpdateLevelsLabel();

  private void LevelsBreakEdit_ValueChanged(object sender, EventArgs e)
  {
    this.UpdateLevelsBreakEditLabel();
  }

  private void LevelsBreakEdit_KeyDown(object sender, KeyEventArgs e)
  {
    this.UpdateLevelsBreakEditLabel();
  }

  private void LevelsNumForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    Dictionary<string, object> dictionary = new Dictionary<string, object>();
    dictionary.Add("Result", (object) this.Result);
    dictionary.Add("Levels", (object) this.LevelsEdit.Value);
    dictionary.Add("LevelsBreak", (object) (this.Result == LevelsNumForm.ResultType.LevelsBreak ? this.LevelsBreakEdit.Value : (Decimal) this._levelsBreakOldValue));
    string configName = $"{this.GetType().ToString()}_{this.Name}";
    if (this._showLevelBreak)
      configName += "_WithBreak";
    FormStorage.SaveLayout((Control) this, configName, (IDictionary) dictionary);
  }

  private void LevelsNumForm_Load(object sender, EventArgs e)
  {
    Dictionary<string, object> dictionary = new Dictionary<string, object>();
    dictionary.Add("Levels", (object) this.LevelsEdit.Value);
    dictionary.Add("LevelsBreak", (object) this.LevelsBreakEdit.Value);
    dictionary.Add("Result", (object) (int) this.Result);
    string configName = $"{this.GetType().ToString()}_{this.Name}";
    if (this._showLevelBreak)
      configName += "_WithBreak";
    FormStorage.LoadLayout((Control) this, configName, (IDictionary) dictionary, false, out Point _, out Size _);
    object obj;
    if (dictionary.TryGetValue("Result", out obj))
      this.Result = (LevelsNumForm.ResultType) Convert.ToInt32(obj);
    if (dictionary.TryGetValue("Levels", out obj))
      this.LevelsNum = Convert.ToInt32(obj);
    if (!dictionary.TryGetValue("LevelsBreak", out obj))
      return;
    this._levelsBreakOldValue = Convert.ToInt32(obj);
    this.LevelsBreak = this._levelsBreakOldValue;
  }

  private void LevelsBreakRadioButton_CheckedChanged(object sender, EventArgs e)
  {
    this.LevelsBreakEdit.Enabled = true;
    this.LevelsBreakEdit.BackColor = SystemColors.Window;
    this.LevelsEdit.Enabled = false;
    this.LevelsEdit.BackColor = SystemColors.ButtonFace;
    this.ActiveControl = (Control) this.LevelsBreakEdit;
  }

  private void LevelsNumRadioButton_CheckedChanged(object sender, EventArgs e)
  {
    this.LevelsBreakEdit.Enabled = false;
    this.LevelsBreakEdit.BackColor = SystemColors.ButtonFace;
    this.LevelsEdit.Enabled = true;
    this.LevelsEdit.BackColor = SystemColors.Window;
    this.ActiveControl = (Control) this.LevelsEdit;
  }

  private void AllLevelsRadioButton_CheckedChanged(object sender, EventArgs e)
  {
    this.LevelsBreakEdit.Enabled = false;
    this.LevelsBreakEdit.BackColor = SystemColors.ButtonFace;
    this.LevelsEdit.Enabled = false;
    this.LevelsEdit.BackColor = SystemColors.ButtonFace;
  }

  public void ForComposition()
  {
    this.Text = "Загрузка состава";
    this.LevelsNumRadioButton.Text = "Загрузить состав на";
    this.LevelsBreakRadioButton.Text = "Загрузить состав до";
    this.AllLevelsRadioButton.Text = "Загрузить состав полностью";
    this.groupBox1.Text = "Укажите, на сколько уровней требуется загрузить состав";
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LevelsNumForm));
    this.Panel2 = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.LevelsEdit = new NumericUpDown();
    this.LevelsLabel = new Label();
    this.LevelsNumRadioButton = new RadioButton();
    this.LevelsBreakRadioButton = new RadioButton();
    this.groupBox1 = new GroupBox();
    this.AllLevelsRadioButton = new RadioButton();
    this.LevelsBreakEdit = new NumericUpDown();
    this.LevelsBreakLabel = new Label();
    this.Panel2.SuspendLayout();
    this.LevelsEdit.BeginInit();
    this.groupBox1.SuspendLayout();
    this.LevelsBreakEdit.BeginInit();
    this.SuspendLayout();
    this.Panel2.BackColor = Color.Transparent;
    this.Panel2.Controls.Add((Control) this.CancButton);
    this.Panel2.Controls.Add((Control) this.OkButton);
    componentResourceManager.ApplyResources((object) this.Panel2, "Panel2");
    this.Panel2.Name = "Panel2";
    componentResourceManager.ApplyResources((object) this.CancButton, "CancButton");
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Name = "CancButton";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    componentResourceManager.ApplyResources((object) this.LevelsEdit, "LevelsEdit");
    this.LevelsEdit.Maximum = new Decimal(new int[4]
    {
      10000,
      0,
      0,
      0
    });
    this.LevelsEdit.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.LevelsEdit.Name = "LevelsEdit";
    this.LevelsEdit.Value = new Decimal(new int[4]
    {
      3,
      0,
      0,
      0
    });
    this.LevelsEdit.ValueChanged += new EventHandler(this.UnitsEdit_ValueChanged);
    this.LevelsEdit.KeyDown += new KeyEventHandler(this.LevelsEdit_KeyDown);
    componentResourceManager.ApplyResources((object) this.LevelsLabel, "LevelsLabel");
    this.LevelsLabel.Name = "LevelsLabel";
    componentResourceManager.ApplyResources((object) this.LevelsNumRadioButton, "LevelsNumRadioButton");
    this.LevelsNumRadioButton.Checked = true;
    this.LevelsNumRadioButton.Name = "LevelsNumRadioButton";
    this.LevelsNumRadioButton.TabStop = true;
    this.LevelsNumRadioButton.UseVisualStyleBackColor = true;
    this.LevelsNumRadioButton.CheckedChanged += new EventHandler(this.LevelsNumRadioButton_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.LevelsBreakRadioButton, "LevelsBreakRadioButton");
    this.LevelsBreakRadioButton.Name = "LevelsBreakRadioButton";
    this.LevelsBreakRadioButton.UseVisualStyleBackColor = true;
    this.LevelsBreakRadioButton.CheckedChanged += new EventHandler(this.LevelsBreakRadioButton_CheckedChanged);
    this.groupBox1.Controls.Add((Control) this.AllLevelsRadioButton);
    this.groupBox1.Controls.Add((Control) this.LevelsBreakRadioButton);
    this.groupBox1.Controls.Add((Control) this.LevelsBreakEdit);
    this.groupBox1.Controls.Add((Control) this.LevelsEdit);
    this.groupBox1.Controls.Add((Control) this.LevelsNumRadioButton);
    this.groupBox1.Controls.Add((Control) this.LevelsBreakLabel);
    this.groupBox1.Controls.Add((Control) this.LevelsLabel);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.AllLevelsRadioButton, "AllLevelsRadioButton");
    this.AllLevelsRadioButton.Name = "AllLevelsRadioButton";
    this.AllLevelsRadioButton.UseVisualStyleBackColor = true;
    this.AllLevelsRadioButton.CheckedChanged += new EventHandler(this.AllLevelsRadioButton_CheckedChanged);
    this.LevelsBreakEdit.BackColor = SystemColors.ButtonFace;
    componentResourceManager.ApplyResources((object) this.LevelsBreakEdit, "LevelsBreakEdit");
    this.LevelsBreakEdit.Maximum = new Decimal(new int[4]
    {
      10000,
      0,
      0,
      0
    });
    this.LevelsBreakEdit.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.LevelsBreakEdit.Name = "LevelsBreakEdit";
    this.LevelsBreakEdit.Value = new Decimal(new int[4]
    {
      3,
      0,
      0,
      0
    });
    this.LevelsBreakEdit.ValueChanged += new EventHandler(this.LevelsBreakEdit_ValueChanged);
    this.LevelsBreakEdit.KeyDown += new KeyEventHandler(this.LevelsBreakEdit_KeyDown);
    componentResourceManager.ApplyResources((object) this.LevelsBreakLabel, "LevelsBreakLabel");
    this.LevelsBreakLabel.Name = "LevelsBreakLabel";
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.Panel2);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (LevelsNumForm);
    this.FormClosed += new FormClosedEventHandler(this.LevelsNumForm_FormClosed);
    this.Load += new EventHandler(this.LevelsNumForm_Load);
    this.Panel2.ResumeLayout(false);
    this.LevelsEdit.EndInit();
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.LevelsBreakEdit.EndInit();
    this.ResumeLayout(false);
  }

  public enum ResultType
  {
    Levels = 1,
    LevelsBreak = 2,
    All = 3,
  }

  public class QueryResult
  {
    public readonly LevelsNumForm.ResultType ResultType;
    public readonly int Levels;

    public QueryResult(LevelsNumForm.ResultType resultType, int levels)
    {
      this.ResultType = resultType;
      this.Levels = levels;
    }
  }
}
