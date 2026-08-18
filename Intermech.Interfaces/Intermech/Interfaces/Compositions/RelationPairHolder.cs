
// Type: Intermech.Interfaces.Compositions.RelationPairHolder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Вспомогательный класс, позволяющий хранить экземпляр класса RelationPair
    /// </summary>
    [Serializable]
    public sealed class RelationPairHolder
    {
      /// <summary>Ключ</summary>
      private RelationPair _value;

      /// <summary>Ключ</summary>
      public RelationPair Value
      {
        [DebuggerStepThrough] get => this._value;
      }

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="value">Ключ</param>
      public RelationPairHolder(RelationPair value) => this._value = value;
    }
}
