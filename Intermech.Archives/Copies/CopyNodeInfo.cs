// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.CopyNodeInfo
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Для передачи информации о копии объекта</summary>
public class CopyNodeInfo
{
  /// <summary>ИД версии копии</summary>
  private readonly long _copyObjectID;
  /// <summary>версия документа, для которого создана копия</summary>
  private readonly long _docObjectID;
  /// <summary>документ, для которого создана копия</summary>
  private readonly long _docID;
  /// <summary>абонент, которому выслана копия</summary>
  private readonly long _subscriberID;
  /// <summary>Заголовок копии</summary>
  private readonly string _copyCaption;
  /// <summary>Шаг жизненного цикла копии</summary>
  private readonly int _lcStepID;

  /// <summary>версия копии документа</summary>
  public long CopyObjectID => this._copyObjectID;

  /// <summary>абонент, которому выслана копия</summary>
  public long SubscriberID => this._subscriberID;

  /// <summary>шаг жц, на котором находится копия</summary>
  public int LСStepID => this._lcStepID;

  /// <summary>версия документа, для которого создана копия</summary>
  public long DocObjectID => this._docObjectID;

  /// <summary>Заголовок копии</summary>
  public string СopyCaption => this._copyCaption;

  /// <summary>документ, для которого создана копия</summary>
  public long DocID => this._docID;

  /// <summary>Конструктор</summary>
  /// <param name="copyObjectID">ИД версии копии</param>
  /// <param name="docID">id документа для которого сделана копия</param>
  /// <param name="docObjectID">id версия документа для которого сделана копия</param>
  /// <param name="subscriberID">абонент, которому выслана копия</param>
  /// <param name="lcStepID">ИД шага ЖЦ</param>
  /// <param name="caption">Заголовок копии</param>
  public CopyNodeInfo(
    long copyObjectID,
    long docID,
    long docObjectID,
    long subscriberID,
    int lcStepID,
    string caption)
  {
    this._copyObjectID = copyObjectID;
    this._docObjectID = docObjectID;
    this._subscriberID = subscriberID;
    this._docID = docID;
    this._copyCaption = caption;
    this._lcStepID = lcStepID;
  }
}
