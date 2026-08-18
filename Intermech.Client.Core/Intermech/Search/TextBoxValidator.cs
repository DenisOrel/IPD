
// Type: Intermech.Search.TextBoxValidator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.Search;

public abstract class TextBoxValidator
{
  public TextBoxValidator(TextBox textBox)
  {
    this.TextBox = textBox != null ? textBox : throw new ArgumentNullException(nameof (TextBox));
  }

  protected TextBox TextBox { get; private set; }
}
