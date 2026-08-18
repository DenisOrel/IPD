
// Type: Intermech.Client.Core.Organizer.SchedulerHeaderButton
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
internal class SchedulerHeaderButton
{
  private Rectangle _bounds;
  private string _text = string.Empty;
  private int _index = -1;
  private bool _active;
  private InputState _state;

  /// <summary>
  /// 
  /// </summary>
  internal bool Active
  {
    get => this._active;
    set => this._active = value;
  }

  /// <summary>
  /// 
  /// </summary>
  internal Rectangle Bounds => this._bounds;

  /// <summary>
  /// 
  /// </summary>
  internal int Height
  {
    get => this._bounds.Height;
    set => this._bounds.Height = value;
  }

  /// <summary>
  /// 
  /// </summary>
  internal int Index => this._index;

  /// <summary>
  /// 
  /// </summary>
  internal InputState State
  {
    get => this._state;
    set => this._state = value;
  }

  /// <summary>
  /// 
  /// </summary>
  internal string Text => this._text;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="bounds"></param>
  /// <param name="text"></param>
  /// <param name="index"></param>
  internal SchedulerHeaderButton(Rectangle bounds, string text, int index)
  {
    this._bounds = bounds;
    this._text = text;
    this._index = index;
  }
}
