// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.IDocumentTypesLocatorData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Data;

/// <summary>
/// Позволяет реализовать декодер исходных данных для алгоритма поиска документа, входящего в
/// документацию на изделие, используя тип документа.
/// </summary>
public interface IDocumentTypesLocatorData
{
  /// <summary>Возвращает версию исследуемого изделия.</summary>
  /// <returns>Идентификатор версии изделия</returns>
  long GetArticleId();

  /// <summary>Возвращает коллекцию подходящих типов документов.</summary>
  /// <returns>Коллекция идентификаторов типов документов</returns>
  ICollection<int> GetDocumentTypes();
}
