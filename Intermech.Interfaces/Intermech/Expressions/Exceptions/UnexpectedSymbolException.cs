
// Type: Intermech.Expressions.Exceptions.UnexpectedSymbolException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech.Expressions.Exceptions
{
    /// <summary>Unexpected symbol.</summary>
    [Serializable]
    public class UnexpectedSymbolException : ParseException
    {
      public UnexpectedSymbolException(int pos, char symbol)
        : base(LocalizationHolder.rm.GetString("Interfaces_52"), pos)
      {
      }

      /// <summary>Initializes a new instance of the <see cref="T:System.SystemException"></see> class with serialized data.</summary>
      /// <param name="context">The contextual information about the source or destination. </param>
      /// <param name="info">The object that holds the serialized object data. </param>
      protected UnexpectedSymbolException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
