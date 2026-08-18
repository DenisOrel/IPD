
// Type: Intermech.Client.Core.CompositionView.CompositionViewObjectEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.CompositionView;

/// <summary>Аргументы для объектов</summary>
public class CompositionViewObjectEventArgs : CompositionViewEventArgs
{
  /// <summary>
  /// 
  /// </summary>
  private long _objectID = -1;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="method"></param>
  /// <param name="objectID"></param>
  public CompositionViewObjectEventArgs(CVButtonMethod method, long objectID)
    : base(method)
  {
    this._objectID = objectID;
  }

  /// <summary>Идентификатор объекта</summary>
  public long ObjectID => this._objectID;
}
