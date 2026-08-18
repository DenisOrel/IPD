
// Type: Intermech.Client.Core.CompositionView.CompositionViewRelationEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.CompositionView;

/// <summary>Аргументы для связей</summary>
public class CompositionViewRelationEventArgs : CompositionViewEventArgs
{
  /// <summary>
  /// 
  /// </summary>
  private long _projObjectID = -1;
  /// <summary>
  /// 
  /// </summary>
  private long _partObjectID = -1;
  /// <summary>
  /// 
  /// </summary>
  private long _relationID = -1;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="method"></param>
  /// <param name="projObjectID"></param>
  /// <param name="partObjectID"></param>
  /// <param name="relationID"></param>
  public CompositionViewRelationEventArgs(
    CVButtonMethod method,
    long projObjectID,
    long partObjectID,
    long relationID)
    : base(method)
  {
    this._projObjectID = projObjectID;
    this._partObjectID = partObjectID;
    this._relationID = relationID;
  }

  /// <summary>Идентификатор предка</summary>
  public long ProjObjectID => this._projObjectID;

  /// <summary>Идентификатор потомка</summary>
  public long PartObjectID => this._partObjectID;

  /// <summary>Идентификатор связи</summary>
  public long RelationID => this._relationID;
}
