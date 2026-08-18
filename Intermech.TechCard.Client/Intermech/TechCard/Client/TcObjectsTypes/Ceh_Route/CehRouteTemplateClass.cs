// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.CehRouteTemplateClass
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>Шаблон расцеховки</summary>
/// <summary>Конструктор</summary>
/// <param name="objId"> Ид. версии объекта</param>
/// <param name="linkId">Ид. версии связи с родительским объектом</param>
public class CehRouteTemplateClass(long objectId, long linkId = 0) : CehRouteElementContainer(objectId, linkId)
{
}
