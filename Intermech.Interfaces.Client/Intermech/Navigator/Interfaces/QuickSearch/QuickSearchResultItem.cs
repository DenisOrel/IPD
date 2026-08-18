// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.QuickSearch.QuickSearchResultItem
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces.QuickSearch;

/// <summary>
/// 
/// </summary>
public sealed class QuickSearchResultItem
{
  /// <summary>
  /// 
  /// </summary>
  public string Caption { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public int ImageIndex { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public object Item { get; private set; }

  /// <summary>Конструктор.</summary>
  /// <param name="caption"></param>
  /// <param name="imgIndex"></param>
  /// <param name="item"></param>
  public QuickSearchResultItem(string caption, int imgIndex, object item)
  {
    this.Caption = caption;
    this.ImageIndex = imgIndex;
    this.Item = item;
  }
}
