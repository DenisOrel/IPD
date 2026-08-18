
// Type: Intermech.PropertyEditors.RTFPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

[Editor(typeof (RTFEditor), typeof (UITypeEditor))]
public class RTFPropertyClass
{
  private static RichTextBox _rtb = new RichTextBox();
  private string _text = string.Empty;

  public string Text
  {
    get => this._text;
    set => this._text = value;
  }

  public RTFPropertyClass(string aValue) => this._text = aValue;

  public override string ToString()
  {
    if (!this._text.StartsWith("{\\rtf1"))
      return this._text;
    RTFPropertyClass._rtb.Rtf = this._text;
    return RTFPropertyClass._rtb.Text.Trim();
  }
}
