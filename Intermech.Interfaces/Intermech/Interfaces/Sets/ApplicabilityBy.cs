
// Type: Intermech.Interfaces.Sets.ApplicabilityBy
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Interfaces.Sets
{
    /// <summary>Применяемость по сериям или по датам</summary>
    [Serializable]
    public enum ApplicabilityBy
    {
      /// <summary>Применяемость в сериях</summary>
      [CustomDescription("Attribute.Interfaces_553")] Series,
      /// <summary>Применяемость по датам</summary>
      [CustomDescription("Attribute.Interfaces_554")] Date,
    }
}
