
// Type: Intermech.Interfaces.RelationOperatorOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Опции для оператора</summary>
    [Flags]
    public enum RelationOperatorOptions
    {
      None = 0,
      /// <summary>Является оператором входимостей</summary>
      InRelation = 1,
      /// <summary>Не требует значения</summary>
      NoneValue = 2,
    }
}
