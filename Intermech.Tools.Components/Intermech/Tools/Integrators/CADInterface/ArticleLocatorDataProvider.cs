// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ArticleLocatorDataProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.Data;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Позволяет реализовать провайдер исходных данных для алгоритмов поиска изделия в базе IPS в зависимости от разновидности изделия и способа его обработки.
/// </summary>
public class ArticleLocatorDataProvider
{
  /// <summary>
  /// Создает декодер для поиска изделия по внешнему ключу изделия.
  /// Если такой вариант поиска изделия не поддерживается, то метод может вернуть null.
  /// </summary>
  /// <returns>Ссылка на объект декодера или null</returns>
  public virtual IExternalKeyLocatorData TryCreateExternalKeyDecoder()
  {
    return (IExternalKeyLocatorData) null;
  }

  /// <summary>
  /// Создает декодер для поиска изделия по ключу Imbase.
  /// Если такой вариант поиска изделия не поддерживается, то метод может вернуть null.
  /// </summary>
  /// <returns>Ссылка на объект декодера или null</returns>
  public virtual IImbaseKeyLocatorData TryCreateImbaseKeyDecoder() => (IImbaseKeyLocatorData) null;

  /// <summary>
  /// Создает декодер для поиска изделия по обозначению, коду ОКП или наименованию.
  /// Если такой вариант поиска изделия не поддерживается, то метод может вернуть null.
  /// </summary>
  /// <returns>Ссылка на объект декодера или null</returns>
  public virtual IIdentityArticleLocatorData TryCreateIdentityDecoder()
  {
    return (IIdentityArticleLocatorData) null;
  }
}
