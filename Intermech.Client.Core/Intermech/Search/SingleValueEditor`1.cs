
// Type: Intermech.Search.SingleValueEditor`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Search;

public class SingleValueEditor<T> : 
  UserControl,
  ISingleValueEditor<T>,
  ISingleValueEditor,
  IKeyUpHandler
{
  private object _value;
  private bool _allowEmpty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public SingleValueEditor() => this.InitializeComponent();

  protected virtual T DefaultValue => default (T);

  protected virtual void DoSetAllowEmpty()
  {
  }

  protected virtual void DoSetValue()
  {
  }

  protected virtual bool DoValidate() => true;

  protected void SetValue(object value, bool doSetValue)
  {
    if (this._value != value)
    {
      this._value = value;
      if (doSetValue)
        this.DoSetValue();
      this.OnValueChanged();
    }
    else
    {
      if (!doSetValue)
        return;
      this.DoSetValue();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool AllowEmpty
  {
    get => this._allowEmpty;
    set
    {
      if (this._allowEmpty == value)
        return;
      this._allowEmpty = value;
      this.DoSetAllowEmpty();
      this.DoSetValue();
    }
  }

  public virtual bool IsEmpty => this.Value == null;

  public virtual bool IsValid
  {
    get
    {
      if (this.AllowEmpty && this.IsEmpty)
        return true;
      return (this.AllowEmpty || !this.AllowEmpty && !this.IsEmpty) && this.DoValidate();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public T TypedValue
  {
    get => this.Value == null ? this.DefaultValue : (T) this.Value;
    set => this.Value = (object) value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public object Value
  {
    get => this._value;
    set => this.SetValue(value, true);
  }

  public event EventHandler ValueChanged;

  public virtual void SetFocus()
  {
  }

  public void HandleKeyUp(Keys keyCode)
  {
    if (!(this.Parent is IKeyUpHandler))
      return;
    ((IKeyUpHandler) this.Parent).HandleKeyUp(keyCode);
  }

  private void OnValueChanged()
  {
    EventHandler valueChanged = this.ValueChanged;
    if (valueChanged == null)
      return;
    valueChanged((object) this, EventArgs.Empty);
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
  }
}
