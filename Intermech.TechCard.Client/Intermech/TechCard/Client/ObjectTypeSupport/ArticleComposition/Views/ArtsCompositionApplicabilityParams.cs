// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Views.ArtsCompositionApplicabilityParams
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.Compositions;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Views;

/// <summary>Параметры вызова закладки "Применяемость в ТП"</summary>
internal class ArtsCompositionApplicabilityParams
{
  /// <summary>
  /// 
  /// </summary>
  public IServiceProvider ServiceProvider;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="techProcObjInfo"></param>
  /// <param name="serviceProvider"></param>
  public ArtsCompositionApplicabilityParams(
    ObjInfoItem techProcObjInfo,
    IServiceProvider serviceProvider)
  {
    this.TechProcObjInfo = techProcObjInfo;
    this.ServiceProvider = serviceProvider;
  }

  /// <summary>Информация о ТП</summary>
  public ObjInfoItem TechProcObjInfo { get; }

  /// <summary>
  /// Список вида : Единица состава ТП -&gt; Соответствующее ДСЕ
  /// </summary>
  public IList<Tuple<ObjInfoItem, ObjInfoItem>> TechElemObj2ArticleList { get; } = (IList<Tuple<ObjInfoItem, ObjInfoItem>>) new List<Tuple<ObjInfoItem, ObjInfoItem>>();
}
