// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.CehTechClassList
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>Список цехов техпроцесса</summary>
/// <summary>Конструктор</summary>
/// <param name="owner"></param>
public class CehTechClassList(CustomTechClass owner) : CustomTechClassList<CehTechClass>(owner)
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="attrLinkGuid"></param>
  /// <returns></returns>
  public int GetIndexByAttrLink(Guid attrLinkGuid)
  {
    int indexByAttrLink = -1;
    for (int index = 0; index < this.Count; ++index)
    {
      if (this[index].AttrLinkGuid == attrLinkGuid)
      {
        indexByAttrLink = index;
        break;
      }
    }
    return indexByAttrLink;
  }
}
