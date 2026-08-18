// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Reports.ArtsCompositionReportObjectItem
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Reports;

/// <summary>Класс - технологическая единица состава</summary>
internal class ArtsCompositionReportObjectItem : IEquatable<ArtsCompositionReportObjectItem>
{
  /// <summary>Кол-во в ед. измерения</summary>
  public readonly MeasuredValue MValue;
  /// <summary>Описание версии изделия</summary>
  public readonly ITypedInfoItem ArtObjectInfo;
  /// <summary>
  /// Описание версии родительского объекта по конструкторской входимости
  /// </summary>
  public readonly ITypedInfoItem ArtProjObjectInfo;
  /// <summary>Описание версии объекта единицы состава</summary>
  public ITypedInfoItem ObjectInfo;
  /// <summary>Описание версии родительского объекта</summary>
  public ITypedInfoItem ProjObjectInfo;

  /// <summary>Конструктор</summary>
  /// <param name="mValue">Кол-во в ед. измерения</param>
  /// <param name="artObjectInfo">Описание изделия</param>
  /// <param name="artProjObjectInfo">Описание родительского изделия</param>
  public ArtsCompositionReportObjectItem(
    MeasuredValue mValue,
    ITypedInfoItem artObjectInfo,
    ITypedInfoItem artProjObjectInfo)
  {
    this.MValue = mValue;
    this.ArtObjectInfo = artObjectInfo;
    this.ArtProjObjectInfo = artProjObjectInfo;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    return obj is ArtsCompositionReportObjectItem other && this.Equals(other);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode()
  {
    return this.ArtObjectInfo.GetHashCode() & this.ArtProjObjectInfo.GetHashCode() << 16 /*0x10*/;
  }

  /// <summary>
  /// 
  /// </summary>
  public IDictionary<int, object> ExtraFields { get; } = (IDictionary<int, object>) new Dictionary<int, object>();

  public bool Equals(ArtsCompositionReportObjectItem other)
  {
    return other != null && this.ArtObjectInfo.Equals((object) other.ArtObjectInfo) && this.ArtProjObjectInfo.Equals((object) other.ArtProjObjectInfo);
  }
}
