
// Type: Intermech.Interfaces.Sets.ISeriesDatesSet
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Sets
{
    /// <summary>
    /// Интерфейс диапазона множеств для применяемости в сериях/датах
    /// </summary>
    public interface ISeriesDatesSet : 
      ISet,
      IAssignable,
      ICloneable,
      IDisplayable,
      ICodedStringFrom,
      IEditableString
    {
      /// <summary>Признак применяемости (по датам)</summary>
      ApplicabilityBy Applicability { get; }
    }
}
