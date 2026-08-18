// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.CreateVersion.TechCardCreateVersionParams
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.Compositions;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Services.CreateVersion;

/// <summary>Параметры создания версии технологических объектов</summary>
public class TechCardCreateVersionParams
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="ecoObjectInfo"></param>
  /// <param name="relObjInfoItems"></param>
  public TechCardCreateVersionParams(
    ObjInfoItem ecoObjectInfo,
    IEnumerable<RelObjInfoItem> relObjInfoItems)
  {
    this.EcoObjectInfo = ecoObjectInfo;
    this.RelObjInfoItems = relObjInfoItems;
  }

  /// <summary>Описание объекта ИИ</summary>
  public ObjInfoItem EcoObjectInfo { get; private set; }

  /// <summary>
  /// Описание объектов (вместе со связями), для которых требуется выпуск версии
  /// </summary>
  public IEnumerable<RelObjInfoItem> RelObjInfoItems { get; private set; }

  /// <summary>
  /// Перечень подписываемых объектов (добавляемых в извещение)
  /// </summary>
  public IEnumerable<ObjInfoIDItem> SignedObjInfoItems { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public IEnumerable<RelObjInfoItem> CompositionRelInfoItems { get; set; }
}
