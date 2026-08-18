
// Type: Intermech.Kernel.Search.ConditionRankIDReplacer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Объект, указывающий ядру на необходимость преобразования ид. должности для преобразования его в идентификаторы пользователей
    /// </summary>
    [TypeConverter(typeof (ToBase64StringTypeConverter<ConditionRankIDReplacer>))]
    [Serializable]
    public class ConditionRankIDReplacer
    {
      /// <summary>Ид. должности</summary>
      public long RankID { get; private set; }

      public ConditionRankIDReplacer(long rankID) => this.RankID = rankID;
    }
}
