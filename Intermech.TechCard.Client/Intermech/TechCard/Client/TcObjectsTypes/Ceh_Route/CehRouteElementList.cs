// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.CehRouteElementList
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>Список расцеховочных элементов</summary>
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец / родительский объект</param>
public class CehRouteElementList(CustomTechClass owner) : CustomTechClassList<CehRouteElementClass>(owner)
{
  /// <summary>Корректировка значения сортировки у объектов</summary>
  /// <remarks>Метод назначает значение сортировки согласно порядку элементов в списке</remarks>
  public void CorrectOrders()
  {
    for (int index = 0; index < this.Count; ++index)
      this[index].OrderID = (long) (index + 1) * 1000000L;
  }
}
