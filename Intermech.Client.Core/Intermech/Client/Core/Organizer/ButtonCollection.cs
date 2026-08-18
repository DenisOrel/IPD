
// Type: Intermech.Client.Core.Organizer.ButtonCollection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class ButtonCollection : NavigationControlCollection
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  public NavigationButton this[int index]
  {
    get => this.List[index] as NavigationButton;
    set => this.List[index] = (object) value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  public void Add(NavigationButton button)
  {
    if (button == null)
      throw new ArgumentNullException();
    this.List.Add((object) button);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  /// <returns></returns>
  public bool Contains(NavigationButton button) => this.List.Contains((object) button);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  internal void Remove(NavigationButton button)
  {
    if (button == null)
      throw new ArgumentNullException();
    this.List.Remove((object) button);
  }
}
