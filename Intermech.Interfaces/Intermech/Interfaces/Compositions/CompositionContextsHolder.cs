
// Type: Intermech.Interfaces.Compositions.CompositionContextsHolder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// В классе хранится список контекстов составов, которые могут назначаться связи при её создании
    /// </summary>
    [Serializable]
    public sealed class CompositionContextsHolder
    {
      /// <summary>Список контекстов</summary>
      private List<long> contexts;

      /// <summary>Список контекстов</summary>
      public List<long> Contexts
      {
        [DebuggerStepThrough] get => this.contexts;
      }

      /// <summary>Создать список контекстов составов по умолчанию</summary>
      public CompositionContextsHolder()
      {
        this.contexts = new List<long>((IEnumerable<long>) new long[2]
        {
          0L,
          1L
        });
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="source"></param>
      public CompositionContextsHolder(IList<long> source)
      {
        if (source == null || source.Count == 0)
          return;
        this.contexts = new List<long>((IEnumerable<long>) source);
      }
    }
}
