
// Type: Intermech.Navigator.Conditions.ConditionTypeSelector
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Navigator.Conditions;

/// <summary>
/// Диалог выбора типа условия выборки по нажатию на кнопки Добавить условие И и Добавить условие ИЛИ
/// </summary>
internal sealed class ConditionTypeSelector : Form
{
  private SelectionDataSource _dataSource;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button bCancel;
  private Button bOK;

  public ConditionTypeSelector()
  {
    this.InitializeComponent();
    if (!FormStorage.LoadLayout((Control) this))
      return;
    this.StartPosition = FormStartPosition.Manual;
  }

  public ConditionTypeSelector(SelectionDataSource dataSource)
    : this()
  {
    this._dataSource = dataSource;
  }

  public void Initialize(LogicalOperators aLogOp, IConditionController[] controllers)
  {
    this.Text = $"Добавить условие \"{(aLogOp == LogicalOperators.AND ? (object) "И" : (object) "ИЛИ")}\"";
    RadioButton lastRadioButton = (RadioButton) null;
    for (int index = 0; index < controllers.Length; ++index)
      lastRadioButton = this.CreateRadioButton(controllers[index], lastRadioButton, index);
    int num = lastRadioButton.Location.Y + lastRadioButton.Size.Height;
    if (num < this.bOK.Location.Y)
      return;
    Size size = this.Size;
    int width = size.Width;
    size = this.Size;
    int height = size.Height + num - this.bOK.Location.Y + 30;
    this.Size = new Size(width, height);
  }

  private RadioButton CreateRadioButton(
    IConditionController controller,
    RadioButton lastRadioButton,
    int index)
  {
    RadioButton radioButton1 = new RadioButton();
    radioButton1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    Size size;
    int y1;
    if (lastRadioButton == null)
    {
      y1 = 25;
    }
    else
    {
      int y2 = lastRadioButton.Location.Y;
      size = lastRadioButton.Size;
      int height = size.Height;
      y1 = y2 + height;
    }
    radioButton1.Location = new Point(35, y1);
    radioButton1.Name = $"radioButton{index + 1}";
    size = this.Size;
    radioButton1.Size = new Size(size.Width - 45, 35);
    radioButton1.TabIndex = index;
    radioButton1.TabStop = true;
    radioButton1.Text = controller.VisibleName;
    radioButton1.UseVisualStyleBackColor = true;
    radioButton1.Tag = (object) controller;
    RadioButton radioButton2 = radioButton1;
    this.Controls.Add((Control) radioButton2);
    if (lastRadioButton == null)
      radioButton2.Checked = true;
    return radioButton2;
  }

  /// <summary>
  /// Интерфейс на контроллер выбранного на форме типа условия выборки.
  /// Не может быть null
  /// </summary>
  public IConditionController SelectedController
  {
    get
    {
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        if (control is RadioButton && ((RadioButton) control).Checked)
          return (IConditionController) control.Tag;
      }
      return (IConditionController) null;
    }
  }

  private void ConditionTypeSelector_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
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
    this.bCancel = new Button();
    this.bOK = new Button();
    this.SuspendLayout();
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(328, 222);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 1;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(201, 222);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 0;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(461, 261);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.bOK);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.MinimumSize = new Size(400, 300);
    this.Name = nameof (ConditionTypeSelector);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = nameof (ConditionTypeSelector);
    this.FormClosing += new FormClosingEventHandler(this.ConditionTypeSelector_FormClosing);
    this.ResumeLayout(false);
  }
}
