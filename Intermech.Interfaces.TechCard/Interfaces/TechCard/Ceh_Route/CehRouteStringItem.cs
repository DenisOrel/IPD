// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.Ceh_Route.CehRouteStringItem
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.TechCard.Ceh_Route;

/// <summary>Класс для правила построения всей строки расцеховки</summary>
[Serializable]
public class CehRouteStringItem : ICehRouteStringItem
{
  /// <summary>Разделитель между элементами строк расцеховок</summary>
  private string _routeSeparator = "-";
  /// <summary>Список элементов</summary>
  private readonly IList<ICehRouteStringTemplItem> _list;

  /// <summary>Инициализация данных класса</summary>
  private void InitData()
  {
  }

  /// <summary>Конструктор</summary>
  public CehRouteStringItem()
  {
    this._list = (IList<ICehRouteStringTemplItem>) new List<ICehRouteStringTemplItem>();
    this.InitData();
  }

  /// <summary>Разделитель между элементами строк расцеховок</summary>
  public string RouteSeparator
  {
    get => this._routeSeparator;
    set => this._routeSeparator = value;
  }

  /// <summary>Список элементов</summary>
  public IList<ICehRouteStringTemplItem> Items => this._list;

  /// <summary>Создание нового элемента (без добавление в список)</summary>
  /// <param name="objTypeId">Ид. типа объекта</param>
  /// <returns></returns>
  public ICehRouteStringTemplItem CreateTemplItem(int objTypeId)
  {
    return (ICehRouteStringTemplItem) new CehRouteStringTemplItem(objTypeId);
  }
}
