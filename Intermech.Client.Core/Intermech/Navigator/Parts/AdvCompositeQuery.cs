
// Type: Intermech.Navigator.Parts.AdvCompositeQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Navigator.Parts;

/// <summary>Расширеная составная выборка данных</summary>
public class AdvCompositeQuery : CompositeQuery, INodeQuery
{
  /// <summary>Constructor</summary>
  /// <param name="subqueries"></param>
  /// <param name="afterExecute"></param>
  public AdvCompositeQuery(
    List<QuerySlot> subqueries,
    AdvCompositeQuery.delegateResultQueriesPostProcessing afterExecute = null)
    : base(subqueries)
  {
    if (afterExecute == null)
      return;
    this.AfterExecute += afterExecute;
  }

  /// <summary>Вызывается после каждого выполнения запроса в БД</summary>
  public event AdvCompositeQuery.delegateResultQueriesPostProcessing AfterExecute;

  /// <summary>Выполняет запрос на чтение порции дочерних элементов. Позиция для чтения определяется закладкой (bookmark). Если закладка =
  /// null, то будет прочитана первая порция, иначе будет прочитана порция с позиции, указанной в закладке.</summary>
  /// <param name="bookmark">Закладка, указывающая позицию для чтения</param>
  /// <param name="count">Количество записей в порции.</param>
  void INodeQuery.Execute(object bookmark, int count)
  {
    this.Execute(bookmark, count);
    if (this.AfterExecute == null)
      return;
    this.AfterExecute(this._resultQueries);
  }

  /// <summary>Выполняет запрос на чтение значений колонок для указанных дочерних элементов. Этот метод используется навигатором при
  /// операциях обновления содержимого дерева и других элементов визуального интерфейса.</summary>
  /// <param name="nodeIDs">Коллекция идентификаторов дочерних элементов</param>
  void INodeQuery.Execute(NodeIDCollection nodeIDs)
  {
    this.Execute(nodeIDs);
    if (this.AfterExecute == null)
      return;
    this.AfterExecute(this._resultQueries);
  }

  public delegate void delegateResultQueriesPostProcessing(List<QuerySlot> resultQueries);
}
