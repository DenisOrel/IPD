
// Type: Intermech.Expressions.Exceptions.InvalidArgumentTypeException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech.Expressions.Exceptions
{
    /// <summary>Invalid Argument type.</summary>
    [Serializable]
    public class InvalidArgumentTypeException : ParseException
    {
      private int _argIndex;

      public InvalidArgumentTypeException(int nPos, int nIndex)
        : base(LocalizationHolder.rm.GetString("Interfaces_34"), nPos)
      {
        this._argIndex = nIndex;
      }

      /// <summary>Initializes a new instance of the <see cref="T:System.SystemException"></see> class with serialized data.</summary>
      /// <param name="context">The contextual information about the source or destination. </param>
      /// <param name="info">The object that holds the serialized object data. </param>
      protected InvalidArgumentTypeException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._argIndex = info != null ? info.GetInt32("ArgIndex") : throw new ArgumentNullException(nameof (info));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("ArgIndex", this._argIndex);
      }

      /// <summary>Invalid argument index.</summary>
      public int InvalidArgumentIndex => this._argIndex;
    }
}
