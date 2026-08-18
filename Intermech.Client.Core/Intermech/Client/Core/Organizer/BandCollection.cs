
// Type: Intermech.Client.Core.Organizer.BandCollection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class BandCollection : NavigationControlCollection
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  public NavigationBand this[int index]
  {
    get => this.List[index] as NavigationBand;
    set => this.List[index] = (object) value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="band"></param>
  public void Add(NavigationBand band)
  {
    band.OriginalOrder = band != null ? this.List.Add((object) band) : throw new ArgumentNullException();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="band"></param>
  /// <returns></returns>
  public bool Contains(NavigationBand band) => this.List.Contains((object) band);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="band"></param>
  public void Remove(NavigationBand band)
  {
    if (band == null)
      throw new ArgumentNullException();
    this.List.Remove((object) band);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  public void SilentAdd(NavigationBand value)
  {
    if (value == null)
      throw new ArgumentNullException();
    try
    {
      this._notify = false;
      value.OriginalOrder = this.List.Add((object) value);
    }
    finally
    {
      this._notify = true;
    }
  }
}
