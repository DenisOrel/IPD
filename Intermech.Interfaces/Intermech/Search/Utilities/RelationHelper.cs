
// Type: Intermech.Search.Utilities.RelationHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


namespace Intermech.Search.Utilities
{
    /// <summary>Хелпер связей</summary>
    public static class RelationHelper
    {
      /// <summary>Проверить идентификатор связи</summary>
      /// <param name="id">Идентификатор связи</param>
      /// <returns></returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool IsUnknownRelationID(long id) => id == 0L || id == -1L;

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool IsAnyUnknownRelationID(IEnumerable<long> relationIds)
      {
        return relationIds.Any<long>((Func<long, bool>) (o => RelationHelper.IsUnknownRelationID(o)));
      }
    }
}
