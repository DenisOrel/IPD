// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.OperTechClass
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>Операция техпроцесса</summary>
/// <summary>
/// 
/// </summary>
/// <param name="objectId"></param>
/// <param name="linkId"></param>
public class OperTechClass(long objectId, long linkId) : CustomTechClass(objectId, linkId)
{
  /// <summary>Конструктор</summary>
  /// <param name="objectId"></param>
  public OperTechClass(long objectId)
    : this(objectId, 0L)
  {
  }
}
