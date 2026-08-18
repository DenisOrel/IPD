// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.IIdentityArticleLocatorData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Data;

/// <summary>
/// Позволяет реализовать декодер исходных данных для алгоритмов поиска объекта по
/// обозначению, коду ОКП и наименованию.
/// </summary>
public interface IIdentityArticleLocatorData
{
  /// <summary>Возвращает обозначение объекта.</summary>
  /// <returns>Обозначение объекта</returns>
  string GetDesignation();

  /// <summary>Возвращает код ОКП объекта.</summary>
  /// <returns>Код ОКП объекта</returns>
  string GetOKPCode();

  /// <summary>Возвращает наименование объекта.</summary>
  /// <returns>Наименование объекта</returns>
  string GetName();
}
