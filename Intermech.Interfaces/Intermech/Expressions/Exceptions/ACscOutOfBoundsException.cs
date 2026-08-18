
// Type: Intermech.Expressions.Exceptions.ACscOutOfBoundsException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech.Expressions.Exceptions
{
    /// <summary>ACSC argument is out of bounds.</summary>
    [Serializable]
    public class ACscOutOfBoundsException : EvaluateException
    {
      public ACscOutOfBoundsException()
        : base(LocalizationHolder.rm.GetString("Interfaces_18"))
      {
      }

      /// <summary>Initializes a new instance of the <see cref="T:System.SystemException"></see> class with serialized data.</summary>
      /// <param name="context">The contextual information about the source or destination. </param>
      /// <param name="info">The object that holds the serialized object data. </param>
      protected ACscOutOfBoundsException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
