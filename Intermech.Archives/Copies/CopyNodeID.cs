// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.CopyNodeID
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Для передачи информации о копии объекта</summary>
public class CopyNodeID : NodeID, ICopyNodeID
{
  /// <summary>версия документа, для которого создана копия</summary>
  private long docObjectID;
  /// <summary>документ, для которого создана копия</summary>
  private long docID;
  /// <summary>абонент, которому выслана копия</summary>
  private long subscriberID;
  /// <summary>Заголовок копии</summary>
  private string copyCaption = string.Empty;

  /// <summary>версия копии документа</summary>
  public long CopyObjectID => this.ObjectID;

  /// <summary>абонент, которому выслана копия</summary>
  public long SubscriberID => this.subscriberID;

  /// <summary>шаг жц, на котором находится копия</summary>
  public int LСStepID => this.LCStepID;

  /// <summary>версия документа, для которого создана копия</summary>
  public long DocObjectID => this.docObjectID;

  /// <summary>Заголовок копии</summary>
  public string СopyCaption => this.Caption;

  /// <summary>документ, для которого создана копия</summary>
  public long DocID => this.docID;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  /// <param name="docID"> id документа для которого сделана копия</param>
  /// <param name="docObjectID">id версия документа для которого сделана копия</param>
  /// <param name="subscriberID">абонент, которому выслана копия </param>
  public CopyNodeID(CreateObjectNodeParams e, long docID, long docObjectID, long subscriberID)
    : base(e)
  {
    this.docObjectID = docObjectID;
    this.subscriberID = subscriberID;
    this.docID = docID;
  }
}
