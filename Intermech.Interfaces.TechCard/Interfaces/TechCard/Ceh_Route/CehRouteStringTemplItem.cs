// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.Ceh_Route.CehRouteStringTemplItem
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Interfaces.TechCard.Ceh_Route;

/// <summary>
/// Класс шаблона (настройки) генерации строки расцеховки для типа объекта
/// </summary>
[Serializable]
public class CehRouteStringTemplItem : ICehRouteStringTemplItem
{
  /// <summary>Шаблон атрибута в настройке строки РМ</summary>
  /// <remarks>Строка вида {Название атрибута}</remarks>
  public static readonly string AttributeTemplate = "{{{0}}}";
  /// <summary>
  /// Шаблон / префикс наименования атрибута связи в строке РМ
  /// </summary>
  public static readonly string LinkAttributePrefix = LocalizationHolder.rm.GetString("Interfaces.TechCard_23");
  /// <summary>Идентификатор типа объекта</summary>
  private int _objTypeID;
  /// <summary>Порядок шаблона в списке</summary>
  private int _orderID = -1;
  /// <summary>Правило заполнения строки шаблона</summary>
  private string _routeTemplate = "";

  /// <summary>Инициализация данных класса</summary>
  private void InitData()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objTypeId"></param>
  public CehRouteStringTemplItem(int objTypeId)
  {
    this._objTypeID = objTypeId;
    this.InitData();
  }

  /// <summary>Идентификатор типа объекта</summary>
  public int ObjTypeID
  {
    get => this._objTypeID;
    set => this._objTypeID = value;
  }

  /// <summary>Правило генерации (заполнения) строки шаблона</summary>
  public string RouteTemplate
  {
    get => this._routeTemplate;
    set => this._routeTemplate = value;
  }

  /// <summary>Порядок шаблона в списке</summary>
  public int OrderID
  {
    get => this._orderID;
    set => this._orderID = value;
  }
}
