// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.Ceh_Route.ICehRouteStringItem
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.TechCard.Ceh_Route;

/// <summary>
/// Интерфейс для правила построения всей строки расцеховки
/// </summary>
public interface ICehRouteStringItem
{
  /// <summary>Разделитель между элементами строк расцеховок</summary>
  string RouteSeparator { get; set; }

  /// <summary>Список элементов</summary>
  IList<ICehRouteStringTemplItem> Items { get; }

  /// <summary>Создание нового элемента (без добавление в список)</summary>
  /// <param name="objTypeId">Ид. типа объекта</param>
  /// <returns></returns>
  ICehRouteStringTemplItem CreateTemplItem(int objTypeId);
}
