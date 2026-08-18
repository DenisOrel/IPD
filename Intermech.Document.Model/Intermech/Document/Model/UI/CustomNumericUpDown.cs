// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.CustomNumericUpDown
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.UI;

internal class CustomNumericUpDown : NumericUpDown
{
  protected override void OnTextBoxTextChanged(object source, EventArgs e)
  {
    base.OnTextBoxTextChanged(source, e);
  }

  protected override void OnTextChanged(EventArgs e) => base.OnTextChanged(e);

  protected override void OnValidating(CancelEventArgs e) => base.OnValidating(e);

  protected override void OnValueChanged(EventArgs e)
  {
    base.OnValueChanged(e);
    Decimal num1 = this.Value;
    int num2 = num1.ToString().Length - ((int) num1).ToString().Length - 1;
    if (num2 < 0)
      num2 = 0;
    this.DecimalPlaces = num2;
  }

  protected override void OnValidated(EventArgs e) => base.OnValidated(e);

  protected override void OnTextBoxKeyPress(object source, KeyPressEventArgs e)
  {
    base.OnTextBoxKeyPress(source, e);
  }
}
