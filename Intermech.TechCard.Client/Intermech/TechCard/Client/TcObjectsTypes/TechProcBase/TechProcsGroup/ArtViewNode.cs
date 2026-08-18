// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup.ArtViewNode
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.Compositions;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;

/// <summary>
/// 
/// </summary>
/// <remarks>Для совместимости со старым кодом</remarks>
internal class ArtViewNode : EtpProcRoute2ArtInfo
{
  /// <summary>Constructor</summary>
  public ArtViewNode()
  {
  }

  /// <summary>Constructor</summary>
  /// <param name="objArtInfo"></param>
  /// <param name="objProcRouteInfo"></param>
  /// <param name="objEtpInfo"></param>
  public ArtViewNode(ObjInfoItem objArtInfo, ObjInfoItem objProcRouteInfo, ObjInfoItem objEtpInfo)
    : base(objArtInfo, objProcRouteInfo, objEtpInfo)
  {
  }
}
