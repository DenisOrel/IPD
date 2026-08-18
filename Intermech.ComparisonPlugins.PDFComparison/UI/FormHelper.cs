// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.UI.FormHelper
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison.UI;

public class FormHelper
{
  public static void CheckEnterFormat(KeyPressEventArgs e)
  {
    char keyChar = e.KeyChar;
    if (char.IsDigit(keyChar) || (int) keyChar == (int) Convert.ToChar((object) Keys.Back) || keyChar == '-' || keyChar == ',')
      return;
    if (keyChar == '.')
      e.KeyChar = ',';
    else
      e.Handled = true;
  }
}
