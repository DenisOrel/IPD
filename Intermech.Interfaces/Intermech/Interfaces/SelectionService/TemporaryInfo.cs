
// Type: Intermech.Interfaces.SelectionService.TemporaryInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.SelectionService
{
    /// <summary>Временные значения для выборок</summary>
    [Serializable]
    internal sealed class TemporaryInfo
    {
      /// <summary>Индексы задизабленых условий</summary>
      public List<int> DisableIndexes;
      /// <summary>Темповые значения</summary>
      public List<object[]> Values;

      public TemporaryInfo()
      {
      }

      public TemporaryInfo(List<int> disableIndexes)
      {
        this.DisableIndexes = disableIndexes;
        this.Values = (List<object[]>) null;
      }

      public TemporaryInfo(List<object[]> values)
      {
        this.DisableIndexes = (List<int>) null;
        this.Values = values;
      }
    }
}
