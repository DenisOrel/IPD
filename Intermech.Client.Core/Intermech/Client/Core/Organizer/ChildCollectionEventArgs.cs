
// Type: Intermech.Client.Core.Organizer.ChildCollectionEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class ChildCollectionEventArgs : EventArgs
{
  private object _item;

  /// <summary>Конструктор.</summary>
  public ChildCollectionEventArgs()
  {
  }

  /// <summary>Конструктор.</summary>
  /// <param name="item"></param>
  public ChildCollectionEventArgs(object item) => this._item = item;

  /// <summary>
  /// 
  /// </summary>
  public object Item
  {
    get => this._item;
    set => this._item = value;
  }
}
