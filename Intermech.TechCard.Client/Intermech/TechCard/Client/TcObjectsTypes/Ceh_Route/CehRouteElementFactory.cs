// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.CehRouteElementFactory
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>
/// Фабрика для создания контейнеров РЭ в зависимости от типа
/// </summary>
internal class CehRouteElementFactory
{
  public CehRouteElementContainer CreateItem([NotNull] ObjInfoItem objInfoItem)
  {
    if (MetaDataHelper.IsObjectTypeChildOf(objInfoItem.ObjTypeID, TechCardConsts.ObjectTypes.CehRouteID))
      return (CehRouteElementContainer) new CehRouteClass(objInfoItem.ObjectID);
    return !MetaDataHelper.IsObjectTypeChildOf(objInfoItem.ObjTypeID, TechCardConsts.ObjectTypes.ElemRouteTemplateId) ? new CehRouteElementContainer(objInfoItem.ObjectID) : (CehRouteElementContainer) new CehRouteTemplateClass(objInfoItem.ObjectID);
  }

  public static CehRouteElementFactory Instance { get; } = new CehRouteElementFactory();
}
