// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.Ceh_Route.ICehRouteStringTemplItem
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

#nullable disable
namespace Intermech.Interfaces.TechCard.Ceh_Route;

/// <summary>
/// Интерфейс шаблона (настройки) генерации строки расцеховки для типа объекта
/// </summary>
public interface ICehRouteStringTemplItem
{
  /// <summary>Идентификатор типа объекта</summary>
  int ObjTypeID { get; set; }

  /// <summary>Правило генерации (заполнения) строки шаблона</summary>
  string RouteTemplate { get; set; }

  /// <summary>Порядок элемента в списке</summary>
  int OrderID { get; set; }
}
