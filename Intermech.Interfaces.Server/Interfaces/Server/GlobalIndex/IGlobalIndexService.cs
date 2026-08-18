// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.GlobalIndex.IGlobalIndexService
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Kernel.Search;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Server.GlobalIndex;

public interface IGlobalIndexService
{
  WordLanguageEnum GetIndexWords(string word, out string normalForm, out string stemmedForm);

  void ClearTrash(IDbManager db);

  void AddToQueue(IDBAttributeType attrType);

  void DeleteFromIndex(IDBAttributeType attrType);

  void RegisterFileConverter(IIndexerFileConverter converter);

  string[] GetWords(ConditionStructure cond);

  void GetSQLforWord(
    IUserSession session,
    string word1,
    GlobalIndexSearchValue condition,
    List<long> words_id);

  int MinWordLength { get; }

  string[] ConvertersList { get; }

  void SaveSearchQuery(SearchQueryProperties query, IDbManager db);
}
