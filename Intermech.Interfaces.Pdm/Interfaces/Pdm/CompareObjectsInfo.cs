// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.CompareObjectsInfo
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Kernel.Search;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Информация для сравнения объектов</summary>
public class CompareObjectsInfo
{
  /// <summary>Все возможные типы связей по которым идет сравнение</summary>
  public Dictionary<int, bool> RelationTypes;
  /// <summary>Атрибуты, по которым идет сравнение</summary>
  public List<int> CompareAttributes;
  /// <summary>Режим отображения составов</summary>
  public CompositionModes CompositionMode;
  /// <summary>Рекурсивный состав</summary>
  public bool Recursive;
  /// <summary>Результаты запроса составов</summary>
  public Dictionary<long, DataTable> Result;
  /// <summary>
  /// Список идентификаторов атрибутов в таблице результатов
  /// </summary>
  public List<NodeColumnID> ColumnAttributes;
  /// <summary>
  /// Индексы колонок в которых присутствует идентифицирующая часть значения атрибута
  /// </summary>
  public Dictionary<int, int> AttrIDIndexes;

  public CompareObjectsInfo(Dictionary<int, bool> relationTypes)
  {
    this.RelationTypes = relationTypes;
    this.CompareAttributes = new List<int>();
    this.CompositionMode = CompositionModes.Composition;
    this.Recursive = false;
  }

  /// <summary>Типы связей по которым идет сравнение</summary>
  public List<int> EnabledRelationTypes
  {
    get
    {
      if (this.RelationTypes == null)
        return (List<int>) null;
      List<int> enabledRelationTypes = new List<int>(this.RelationTypes.Count);
      foreach (KeyValuePair<int, bool> relationType in this.RelationTypes)
      {
        if (relationType.Value)
          enabledRelationTypes.Add(relationType.Key);
      }
      return enabledRelationTypes;
    }
  }
}
