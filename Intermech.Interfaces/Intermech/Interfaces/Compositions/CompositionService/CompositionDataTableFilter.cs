
// Type: Intermech.Interfaces.Compositions.CompositionService.CompositionDataTableFilter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces.Compositions.CompositionService
{
    /// <summary>Класс фильтрации и сортировки DataTable</summary>
    [Serializable]
    public class CompositionDataTableFilter : ICompositionDataFilter
    {
      private string _filterExpression;
      private string _sortExpression;

      public CompositionDataTableFilter([NotNull] string filterExpression, string sortExpression = null)
      {
        this._filterExpression = filterExpression;
        this._sortExpression = sortExpression;
      }

      /// <summary>Фильтрация / сортировка данных по заданным критериям</summary>
      /// <param name="session"></param>
      /// <param name="dataTable"></param>
      /// <returns></returns>
      public DataTable Execute([NotNull] IUserSession session, [CanBeNull] DataTable dataTable)
      {
        if (dataTable == null)
          return (DataTable) null;
        if (string.IsNullOrEmpty(this._filterExpression) && string.IsNullOrEmpty(this._sortExpression))
          return dataTable;
        DataRow[] fromRows = dataTable.Select(this._filterExpression, this._sortExpression);
        if (string.IsNullOrEmpty(this._sortExpression) && fromRows.Length == dataTable.Rows.Count)
          return dataTable;
        DataTable toTable = dataTable.Clone();
        DataSetProcessor.AssignRows(toTable, (IEnumerable<DataRow>) fromRows, true, true);
        return toTable;
      }
    }
}
