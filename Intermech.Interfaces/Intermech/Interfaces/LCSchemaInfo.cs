
// Type: Intermech.Interfaces.LCSchemaInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Информация по схеме ЖЦ</summary>
    [Serializable]
    public class LCSchemaInfo
    {
      /// <summary>Идентификатор схемы</summary>
      public int SchemaID;
      /// <summary>Идентификатор первого шага ЖЦ</summary>
      public int FirtsLCStep;
      private List<Tuple<int, string, int>> _lcSteps;

      public LCSchemaInfo(int schemaID)
      {
        this.SchemaID = schemaID;
        this.FirtsLCStep = -1;
        this._lcSteps = new List<Tuple<int, string, int>>();
      }

      public bool AddStep(int stepID, string name, int levelID)
      {
        if (this._lcSteps.Exists((Predicate<Tuple<int, string, int>>) (x => x.Item1 == stepID)))
          return false;
        this._lcSteps.Add(new Tuple<int, string, int>(stepID, name, levelID));
        return true;
      }

      public int GetStep(int levelID)
      {
        Tuple<int, string, int> tuple = this._lcSteps.Find((Predicate<Tuple<int, string, int>>) (x => x.Item3 == levelID));
        return tuple == null ? -1 : tuple.Item1;
      }

      public int GetStep(string name)
      {
        Tuple<int, string, int> tuple = this._lcSteps.Find((Predicate<Tuple<int, string, int>>) (x => x.Item2 == name));
        return tuple == null ? -1 : tuple.Item1;
      }
    }
}
