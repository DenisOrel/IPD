// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ImbaseSyncInfo
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>
/// Информация для компонента схемы или платы для синхронизации с Imbase
/// </summary>
public sealed class ImbaseSyncInfo
{
  /// <summary>Идентификатор ссылки на таблицу</summary>
  public long TableID { get; private set; }

  /// <summary>Номер записи в таблице</summary>
  public long RecordID { get; private set; }

  /// <summary>Номер записи в таблице</summary>
  public string ImbaseKey { get; private set; }

  public ImbaseSyncTypes ImbaseSyncType { get; set; }

  public ImbaseSyncInfo(ImbaseSyncTypes imbaseSyncType)
    : this(imbaseSyncType, 0L, -1L)
  {
  }

  public ImbaseSyncInfo(long tableID, long recordID, string imbaseKey)
    : this(ImbaseSyncTypes.Normal, tableID, recordID, imbaseKey)
  {
  }

  public ImbaseSyncInfo(ImbaseSyncTypes imbaseSyncType, long tableID, long recordID)
    : this(imbaseSyncType, tableID, recordID, string.Empty)
  {
  }

  public ImbaseSyncInfo(
    ImbaseSyncTypes imbaseSyncType,
    long tableID,
    long recordID,
    string imbaseKey)
  {
    this.ImbaseSyncType = imbaseSyncType;
    this.TableID = tableID;
    this.RecordID = recordID;
    this.ImbaseKey = imbaseKey;
  }
}
