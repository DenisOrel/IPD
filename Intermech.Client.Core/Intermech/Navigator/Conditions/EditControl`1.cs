
// Type: Intermech.Navigator.Conditions.EditControl`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Conditions.AttributeConditionControls;
using Intermech.Navigator.SelectionView;
using System;
using System.Collections;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

/// <summary>Базовый класс для контролов</summary>
/// <typeparam name="TControl"></typeparam>
internal abstract class EditControl<TControl> : IEditControl where TControl : new()
{
  protected TControl control;
  protected bool firstValue;

  public event ValueChangedEventHandler ValueChangedEvent;

  public Control Control => (object) this.control as Control;

  public bool IsFirstValue => this.firstValue;

  public EditControl(bool firstValue) => this.firstValue = firstValue;

  /// <summary>Функция создания контрола</summary>
  protected abstract void OnCreateControl();

  protected virtual object OnGetValue(object value) => value;

  public virtual object Value
  {
    get => this.OnGetValue(this.Control.Tag);
    set
    {
      if (object.Equals(this.Control.Tag, value))
        return;
      this.Control.Tag = value;
      this.OnSetValue(value);
    }
  }

  protected virtual void OnSetValue(object value)
  {
  }

  protected abstract bool ValidValue(object value);

  protected virtual object PrepareValue(object value) => value;

  protected abstract object defaultValue { get; }

  /// <summary>Проинициализировать контрол значением</summary>
  protected virtual void InitializeValue(object value)
  {
    if (value != null && this.ValidValue(value))
      this.Value = this.PrepareValue(value);
    else
      this.Value = (object) null;
  }

  /// <summary>
  /// Функция вызывается, когда значение в контроле изменяется.
  /// Вызывается только при необходимости, которая установлена при создании контрола.
  /// </summary>
  protected virtual void OnValueChanged(object sender, EventArgs e)
  {
    ValueChangedEventHandler valueChangedEvent = this.ValueChangedEvent;
    if (valueChangedEvent == null)
      return;
    valueChangedEvent((object) this, new ValueChangedEventArgs(this.Value, this.IsFirstValue));
  }

  public void CreateControl(ShowValueMode valueMode, object value)
  {
    this.OnCreateControl();
    if (value == null || ((valueMode & ShowValueMode.svmMulti) != ShowValueMode.svmMulti || !(value is IList)) && ((valueMode & ShowValueMode.svmMulti) == ShowValueMode.svmMulti || value is IList))
      return;
    this.InitializeValue(value);
  }

  public virtual bool OnAddNewValue(OnOpenDialogEventArgs e)
  {
    using (EditControlDialog editControlDialog = new EditControlDialog())
    {
      Control controlForDialog = this.GetControlForDialog();
      editControlDialog.SetControl(controlForDialog);
      if (editControlDialog.ShowDialog() == DialogResult.OK)
      {
        this.OnOKControlDialog(controlForDialog);
        this.OnValueChanged((object) this, new EventArgs());
        return true;
      }
    }
    return false;
  }

  protected virtual Control GetControlForDialog() => (object) new TControl() as Control;

  protected virtual void OnOKControlDialog(Control control)
  {
  }
}
