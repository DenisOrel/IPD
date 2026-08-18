// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.LevelsNumForm
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Project.Controls.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class LevelsNumForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _panel2;
  private Button _cancButton;
  private Button _okButton;
  private NumericUpDown _levelsEdit;
  private Label _levelsLabel;
  private RadioButton _levelsNumRadioButton;
  private RadioButton _allLevelsRadioButton;
  private GroupBox _groupBox1;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Panel Panel2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panel2.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button CancButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._cancButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button OkButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._okButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal NumericUpDown LevelsEdit
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._levelsEdit.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LevelsLabel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._levelsLabel.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RadioButton LevelsNumRadioButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._levelsNumRadioButton.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RadioButton AllLevelsRadioButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._allLevelsRadioButton.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal GroupBox GroupBox1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._groupBox1.CheckInitializedIn<GroupBox>((object) this);
    }
  }

  public static int Query()
  {
    using (LevelsNumForm levelsNumForm = new LevelsNumForm())
    {
      if (levelsNumForm.ShowDialog() == DialogResult.OK)
        return levelsNumForm.LevelsNum;
    }
    return 0;
  }

  public LevelsNumForm()
  {
    this.InitializeComponent();
    this.ActiveControl = (Control) this.LevelsEdit;
  }

  private void UpdateLevelsLabel()
  {
    int num = (int) (this.LevelsEdit.Value % 10M);
    this.LevelsLabel.Text = (num != 1 ? (num <= 1 || num >= 5 ? Resources.Level5 : Resources.Level2) : Resources.Level1) ?? string.Empty;
    this.LevelsNumRadioButton.Checked = true;
  }

  private void UnitsEdit_ValueChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateLevelsLabel();
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

  private void LevelsEdit_KeyDown([CanBeNull] object sender, [NotNull] KeyEventArgs e)
  {
    this.UpdateLevelsLabel();
  }

  private void LevelsNumForm_FormClosed([CanBeNull] object sender, [NotNull] FormClosedEventArgs e)
  {
    Intermech.Client.Core.FormStorage.SaveLayout((Control) this, (IDictionary) new Dictionary<string, object>()
    {
      {
        "Levels",
        (object) this.LevelsNum
      }
    });
  }

  private void LevelsNumForm_Load([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    Dictionary<string, object> dictionary = new Dictionary<string, object>();
    dictionary.Add("Levels", (object) this.LevelsNum);
    Intermech.Client.Core.FormStorage.LoadLayout((Control) this, (IDictionary) dictionary);
    this.LevelsNum = Convert.ToInt32(dictionary["Levels"]);
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
    this._panel2 = new Panel();
    this._cancButton = new Button();
    this._okButton = new Button();
    this._levelsEdit = new NumericUpDown();
    this._levelsLabel = new Label();
    this._levelsNumRadioButton = new RadioButton();
    this._allLevelsRadioButton = new RadioButton();
    this._groupBox1 = new GroupBox();
    this._panel2.SuspendLayout();
    this._levelsEdit.BeginInit();
    this._groupBox1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._panel2, "_panel2");
    this._panel2.BackColor = Color.Transparent;
    this._panel2.Controls.Add((Control) this._cancButton);
    this._panel2.Controls.Add((Control) this._okButton);
    this._panel2.Name = "_panel2";
    componentResourceManager.ApplyResources((object) this._cancButton, "_cancButton");
    this._cancButton.DialogResult = DialogResult.Cancel;
    this._cancButton.ImageKey = Resources.False;
    this._cancButton.Name = "_cancButton";
    componentResourceManager.ApplyResources((object) this._okButton, "_okButton");
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.ImageKey = Resources.False;
    this._okButton.Name = "_okButton";
    componentResourceManager.ApplyResources((object) this._levelsEdit, "_levelsEdit");
    this._levelsEdit.Maximum = new Decimal(new int[4]
    {
      10000,
      0,
      0,
      0
    });
    this._levelsEdit.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._levelsEdit.Name = "_levelsEdit";
    this._levelsEdit.Value = new Decimal(new int[4]
    {
      3,
      0,
      0,
      0
    });
    this._levelsEdit.ValueChanged += new EventHandler(this.UnitsEdit_ValueChanged);
    this._levelsEdit.KeyDown += new KeyEventHandler(this.LevelsEdit_KeyDown);
    componentResourceManager.ApplyResources((object) this._levelsLabel, "_levelsLabel");
    this._levelsLabel.ImageKey = Resources.False;
    this._levelsLabel.Name = "_levelsLabel";
    componentResourceManager.ApplyResources((object) this._levelsNumRadioButton, "_levelsNumRadioButton");
    this._levelsNumRadioButton.Checked = true;
    this._levelsNumRadioButton.ImageKey = Resources.False;
    this._levelsNumRadioButton.Name = "_levelsNumRadioButton";
    this._levelsNumRadioButton.TabStop = true;
    this._levelsNumRadioButton.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._allLevelsRadioButton, "_allLevelsRadioButton");
    this._allLevelsRadioButton.ImageKey = Resources.False;
    this._allLevelsRadioButton.Name = "_allLevelsRadioButton";
    this._allLevelsRadioButton.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._groupBox1, "_groupBox1");
    this._groupBox1.Controls.Add((Control) this._allLevelsRadioButton);
    this._groupBox1.Controls.Add((Control) this._levelsEdit);
    this._groupBox1.Controls.Add((Control) this._levelsNumRadioButton);
    this._groupBox1.Controls.Add((Control) this._levelsLabel);
    this._groupBox1.Name = "_groupBox1";
    this._groupBox1.TabStop = false;
    this.AcceptButton = (IButtonControl) this._okButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._cancButton;
    this.Controls.Add((Control) this._groupBox1);
    this.Controls.Add((Control) this._panel2);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (LevelsNumForm);
    this.FormClosed += new FormClosedEventHandler(this.LevelsNumForm_FormClosed);
    this.Load += new EventHandler(this.LevelsNumForm_Load);
    this._panel2.ResumeLayout(false);
    this._levelsEdit.EndInit();
    this._groupBox1.ResumeLayout(false);
    this._groupBox1.PerformLayout();
    this.ResumeLayout(false);
  }
}
