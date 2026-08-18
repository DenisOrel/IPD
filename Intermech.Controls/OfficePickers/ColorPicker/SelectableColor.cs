
// Type: OfficePickers.ColorPicker.SelectableColor
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Drawing;


namespace OfficePickers.ColorPicker;

internal class SelectableColor
{
  private Color _color;
  private bool _selected;
  private bool _hotTrack;

  public Color Color
  {
    get => this._color;
    set => this._color = value;
  }

  public bool Selected
  {
    get => this._selected;
    set => this._selected = value;
  }

  public bool HotTrack
  {
    get => this._hotTrack;
    set => this._hotTrack = value;
  }

  public SelectableColor(Color color) => this._color = color;
}
