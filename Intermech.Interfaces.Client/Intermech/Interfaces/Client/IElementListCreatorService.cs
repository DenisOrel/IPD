// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IElementListCreatorService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Сервис по созданию перечня элементов (ПЭ) для электрических CAD
/// </summary>
public interface IElementListCreatorService
{
  /// <summary>Создать перечень элементов</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="documentID">Идентификатор версии документа ПЭ</param>
  /// <param name="documentTypeID">Идентификатор типа документа ПЭ</param>
  /// <param name="assemblyIDs">Список составообразующих сборок</param>
  /// <param name="records">Список элементов ПЭ, не связанных с объектами, например контактные площадки и т.п.</param>
  void CreateElementList(
    IUserSession session,
    long documentID,
    int documentTypeID,
    List<long> assemblyIDs,
    List<SimpleRecord> records);
}
