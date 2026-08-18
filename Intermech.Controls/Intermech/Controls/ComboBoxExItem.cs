
// Type: Intermech.Controls.ComboBoxExItem
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml


namespace Intermech.Controls;

public class ComboBoxExItem
{
  private int _imageIndex;
  private string _text;

  public ComboBoxExItem()
    : this("")
  {
  }

  public ComboBoxExItem(string text)
    : this(text, -1)
  {
  }

  public ComboBoxExItem(string text, int imageIndex)
  {
    this._text = text;
    this._imageIndex = imageIndex;
  }

  public override string ToString() => this._text;

  public int ImageIndex
  {
    get => this._imageIndex;
    set => this._imageIndex = value;
  }

  public string Text
  {
    get => this._text;
    set => this._text = value;
  }
}
