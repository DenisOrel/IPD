
// Type: Intermech.Interfaces.SelectionService.SelectionsCache
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.Interfaces.SelectionService
{
    internal sealed class SelectionsCache
    {
      private readonly Dictionary<long, ConditionStructure[]> _cache = new Dictionary<long, ConditionStructure[]>();
      private readonly object _syncRoot = new object();

      public void Reload(IUserSession session, SelectionWrapper wrapper)
      {
        lock (this._syncRoot)
        {
          foreach (KeyValuePair<long, ConditionStructure[]> keyValuePair in this._cache)
            this._cache[keyValuePair.Key] = wrapper.LoadConditionStructures(session, keyValuePair.Key);
        }
      }

      public void Reload(IUserSession session, SelectionWrapper wrapper, long selectionID)
      {
        lock (this._syncRoot)
          this._cache[selectionID] = wrapper.LoadConditionStructures(session, selectionID);
      }

      public void Clear()
      {
        lock (this._syncRoot)
          this._cache.Clear();
      }

      public void Set(long selectionID, ConditionStructure[] conditions)
      {
        lock (this._syncRoot)
          this._cache[selectionID] = conditions;
      }

      public ConditionStructure[] Get(long selectionID, IUserSession session, SelectionWrapper wrapper)
      {
        lock (this._syncRoot)
        {
          ConditionStructure[] source;
          if (!this._cache.TryGetValue(selectionID, out source))
          {
            source = wrapper.LoadConditionStructures(session, selectionID);
            this._cache.Add(selectionID, source);
          }
          return this.CloneStructures(source);
        }
      }

      private ConditionStructure[] CloneStructures(ConditionStructure[] source)
      {
        if (source == null || source.Length == 0)
          return source;
        ConditionStructure[] conditionStructureArray = new ConditionStructure[source.Length];
        for (int index = 0; index < source.Length; ++index)
          conditionStructureArray[index] = source[index].Clone();
        return conditionStructureArray;
      }
    }
}
