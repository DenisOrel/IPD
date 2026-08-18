
// Type: Intermech.Search.Data.Repositories.FindCompositionOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public sealed class FindCompositionOptions
    {
      public FindCompositionOptions(long projectVersionID)
      {
        this.ProjectVersionID = !ObjectHelper.IsUnknownObjectVersionID(projectVersionID) ? projectVersionID : throw new ArgumentException();
      }

      public long ProjectVersionID { get; private set; }

      public int RelationTypeID { get; set; }

      public int PartTypeID { get; set; }

      public int PageNumber { get; set; }

      public int CountOnPage { get; set; }

      public List<ColumnInfo> SortColumnsInfo { get; set; }

      public List<SortOrders> SortDirections { get; set; }

      public string SearchText { get; set; }
    }
}
