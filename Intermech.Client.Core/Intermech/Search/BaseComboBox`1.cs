
// Type: Intermech.Search.BaseComboBox`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search;

public class BaseComboBox<T> : SingleValueEditor<T>
{
  private List<T> _history = new List<T>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ComboBoxWithButtons _comboBoxWithButtons;

  public BaseComboBox()
  {
    this.InitializeComponent();
    this._comboBoxWithButtons.ComboBox.DisplayMember = "Item2";
    this._comboBoxWithButtons.ComboBox.DropDownStyle = this.SupportedTextInput ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList;
    this._comboBoxWithButtons.ComboBox.ValueMember = "Item1";
    this._comboBoxWithButtons.EditButton.FlatAppearance.BorderSize = 0;
    this._comboBoxWithButtons.EditButton.FlatStyle = FlatStyle.Flat;
    this._comboBoxWithButtons.EditButton.Visible = this.SupportedEditing;
    this._comboBoxWithButtons.ClearButton.FlatAppearance.BorderSize = 0;
    this._comboBoxWithButtons.ClearButton.FlatStyle = FlatStyle.Flat;
    this._comboBoxWithButtons.ClearButton.Visible = this.SupportedClearing && this.AllowEmpty;
    this._comboBoxWithButtons.ComboBox.KeyUp += new KeyEventHandler(this.ComboBox_KeyUp);
    this._comboBoxWithButtons.ComboBox.SelectedIndexChanged += new EventHandler(this.ComboBox_SelectedIndexChanged);
    this._comboBoxWithButtons.EditButton.Click += new EventHandler(this.EditButton_Click);
    this._comboBoxWithButtons.ClearButton.Click += new EventHandler(this.ClearButton_Click);
    this.UpdateControls();
  }

  protected virtual bool SupportedTextInput => true;

  protected virtual bool SupportedEditing => true;

  protected virtual bool SupportedClearing => true;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerable<T> History
  {
    get => (IEnumerable<T>) this._history;
    set
    {
      if (object.Equals((object) this._history, (object) value))
        return;
      this._history.Clear();
      if (value != null)
        this._history.AddRange(value);
      this.FillComboBox();
    }
  }

  protected virtual string GetDisplayValue(T item)
  {
    return (object) item == null ? string.Empty : item.ToString();
  }

  protected virtual void Edit() => throw new NotImplementedException();

  protected virtual bool TryParse(string text, out T result) => throw new NotImplementedException();

  protected override void DoSetAllowEmpty()
  {
    this._comboBoxWithButtons.ClearButton.Visible = this.SupportedClearing && this.AllowEmpty;
  }

  protected override void DoSetValue()
  {
    if (this.Value != null)
    {
      if (!this._history.Contains(this.TypedValue))
      {
        this._history.Add(this.TypedValue);
        this.FillComboBox();
      }
      this.Select((object) this.TypedValue);
    }
    else
      this.Select((object) null);
    this.UpdateControls();
  }

  public override void SetFocus()
  {
    this._comboBoxWithButtons.ActiveControl = (Control) this._comboBoxWithButtons.ComboBox;
  }

  private void ComboBox_KeyUp(object sender, KeyEventArgs e)
  {
    if (this.SupportedTextInput && e.KeyCode == Keys.Return)
    {
      if (this.AllowEmpty && string.IsNullOrEmpty(this._comboBoxWithButtons.ComboBox.Text))
      {
        this.SetValue((object) null, false);
      }
      else
      {
        T result = this.DefaultValue;
        this.TryParse(this._comboBoxWithButtons.ComboBox.Text, out result);
        this.SetValue((object) result, true);
      }
    }
    this.HandleKeyUp(e.KeyCode);
  }

  private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.SetValue(this.GetSelectedValue(), false);
    this.UpdateControls();
  }

  private void EditButton_Click(object sender, EventArgs e) => this.Edit();

  private void ClearButton_Click(object sender, EventArgs e) => this.Clear();

  private void FillComboBox()
  {
    this._comboBoxWithButtons.ComboBox.SelectedIndexChanged -= new EventHandler(this.ComboBox_SelectedIndexChanged);
    this._comboBoxWithButtons.ComboBox.BeginUpdate();
    bool flag = false;
    object obj = (object) null;
    if (this._comboBoxWithButtons.ComboBox.SelectedIndex >= 0)
    {
      flag = true;
      obj = this.GetSelectedValue();
    }
    try
    {
      this._comboBoxWithButtons.ComboBox.Items.Clear();
      if (this.AllowEmpty)
        this._comboBoxWithButtons.ComboBox.Items.Add((object) new Tuple<object, string>((object) null, string.Empty));
      this._comboBoxWithButtons.ComboBox.Items.AddRange((object[]) this._history.Select<T, Tuple<object, string>>((Func<T, Tuple<object, string>>) (o => new Tuple<object, string>((object) o, this.GetDisplayValue(o)))).ToArray<Tuple<object, string>>());
      if (!flag)
        return;
      this.Select(obj);
    }
    finally
    {
      this._comboBoxWithButtons.ComboBox.EndUpdate();
      this._comboBoxWithButtons.ComboBox.SelectedIndexChanged += new EventHandler(this.ComboBox_SelectedIndexChanged);
    }
  }

  private object GetSelectedValue()
  {
    return this._comboBoxWithButtons.ComboBox.SelectedIndex >= 0 ? ((Tuple<object, string>) this._comboBoxWithButtons.ComboBox.Items[this._comboBoxWithButtons.ComboBox.SelectedIndex]).Item1 : (object) null;
  }

  private void Select(object value)
  {
    Tuple<object, string> tuple = this._comboBoxWithButtons.ComboBox.Items.Cast<Tuple<object, string>>().FirstOrDefault<Tuple<object, string>>((Func<Tuple<object, string>, bool>) (o => object.Equals(o.Item1, value)));
    if (tuple == null)
      return;
    this._comboBoxWithButtons.ComboBox.SelectedIndex = this._comboBoxWithButtons.ComboBox.Items.IndexOf((object) tuple);
  }

  private void Clear() => this.SetValue((object) null, true);

  private void UpdateControls() => this._comboBoxWithButtons.ClearButton.Enabled = !this.IsEmpty;

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
    this._comboBoxWithButtons = new ComboBoxWithButtons();
    this.SuspendLayout();
    this._comboBoxWithButtons.Dock = DockStyle.Fill;
    this._comboBoxWithButtons.Location = new Point(0, 0);
    this._comboBoxWithButtons.Name = "_comboBoxWithButtons";
    this._comboBoxWithButtons.Size = new Size(200, 21);
    this._comboBoxWithButtons.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._comboBoxWithButtons);
    this.Name = nameof (BaseComboBox<T>);
    this.Size = new Size(200, 21);
    this.ResumeLayout(false);
  }
}
