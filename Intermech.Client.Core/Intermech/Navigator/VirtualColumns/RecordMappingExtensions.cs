
// Type: Intermech.Navigator.VirtualColumns.RecordMappingExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Navigator.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


namespace Intermech.Navigator.VirtualColumns;

/// <summary>Расширения класса RecordMapping</summary>
public static class RecordMappingExtensions
{
  /// <summary>Последовательность всех вирутальных колонок в запросе</summary>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<VirtualQueryResultColumn> VirtualColumns([NotNull] this RecordMapping mapping)
  {
    return mapping.Fields.OfType<VirtualQueryResultColumn>();
  }
}
