
// Type: Intermech.Expressions.Exceptions.SqrtNegNumberException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Expressions.Exceptions
{
    /// <summary>Negative argument for SQRT.</summary>
    [Serializable]
    public class SqrtNegNumberException : EvaluateException
    {
      public SqrtNegNumberException()
        : base("Negative argument of square root.")
      {
      }

      /// <summary>Initializes a new instance of the <see cref="T:System.SystemException"></see> class with serialized data.</summary>
      /// <param name="context">The contextual information about the source or destination. </param>
      /// <param name="info">The object that holds the serialized object data. </param>
      protected SqrtNegNumberException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
