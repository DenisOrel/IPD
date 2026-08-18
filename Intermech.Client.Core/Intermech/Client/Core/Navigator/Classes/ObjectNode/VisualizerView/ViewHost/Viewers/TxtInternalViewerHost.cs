
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.TxtInternalViewerHost
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers;

internal class TxtInternalViewerHost : InternalViewerHost
{
  private TextBox _textBox;

  private TextBox TextBox
  {
    get
    {
      TextBox textBox1 = this._textBox;
      if (textBox1 != null)
        return textBox1;
      TextBox textBox2 = new TextBox();
      textBox2.Dock = DockStyle.Fill;
      textBox2.Name = "textBox";
      textBox2.Multiline = true;
      textBox2.ScrollBars = ScrollBars.Both;
      textBox2.ReadOnly = true;
      textBox2.BackColor = SystemColors.Window;
      TextBox textBox3 = textBox2;
      this._textBox = textBox2;
      return textBox3;
    }
  }

  public override bool Open(string fileName)
  {
    this.TextBox.Text = TxtInternalViewerHost.GetTextFromFile(fileName);
    if (!((IEnumerable<Control>) this.Controls.Find("textBox", true)).Any<Control>())
      this.Controls.Add((Control) this.TextBox);
    return true;
  }

  /// <summary>Возвращает строку полученную в нужной кодировке</summary>
  /// <param name="fileName"></param>
  /// <returns></returns>
  private static string GetTextFromFile(string fileName)
  {
    byte[] numArray = File.ReadAllBytes(fileName);
    Encoding encoding = (Encoding) null;
    string textFromFile = (string) null;
    UTF8Encoding utF8Encoding1 = new UTF8Encoding(true, true);
    bool flag = true;
    byte[] preamble = utF8Encoding1.GetPreamble();
    int length = preamble.Length;
    if (numArray.Length >= length)
    {
      if (((IEnumerable<byte>) preamble).SequenceEqual<byte>(((IEnumerable<byte>) numArray).Take<byte>(length)))
      {
        try
        {
          textFromFile = utF8Encoding1.GetString(numArray, length, numArray.Length - length);
          encoding = (Encoding) utF8Encoding1;
        }
        catch (ArgumentException ex)
        {
          flag = false;
        }
      }
    }
    if (flag && encoding == null)
    {
      UTF8Encoding utF8Encoding2 = new UTF8Encoding(false, true);
      try
      {
        textFromFile = utF8Encoding2.GetString(numArray);
        encoding = (Encoding) utF8Encoding2;
      }
      catch (ArgumentException ex)
      {
      }
    }
    if (encoding == null)
      textFromFile = Encoding.Default.GetString(numArray);
    return textFromFile;
  }
}
