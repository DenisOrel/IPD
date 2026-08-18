// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup.EtpProcRoute2ArtInfo
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.Compositions;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;

/// <summary>Класс для описания привязки ЕТП к ГТП</summary>
internal class EtpProcRoute2ArtInfo
{
  /// <summary>Constructor</summary>
  protected EtpProcRoute2ArtInfo()
  {
  }

  /// <summary>Constructor</summary>
  /// <param name="objArtInfo"></param>
  /// <param name="objProcRouteInfo"></param>
  /// <param name="objEtpInfo"></param>
  public EtpProcRoute2ArtInfo(
    ObjInfoItem objArtInfo,
    ObjInfoItem objProcRouteInfo,
    ObjInfoItem objEtpInfo)
  {
    this.ObjArtInfo = objArtInfo;
    this.ObjProcRouteInfo = objProcRouteInfo;
    this.ObjEtpInfo = objEtpInfo;
  }

  /// <summary>Описание связи между МО и изделием</summary>
  public RelInfoItem LinkProcRoute2ArtInfo { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public ObjInfoItem ObjArtInfo { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public ObjInfoItem ObjProcRouteInfo { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public ObjInfoItem ObjEtpInfo { get; set; }
}
