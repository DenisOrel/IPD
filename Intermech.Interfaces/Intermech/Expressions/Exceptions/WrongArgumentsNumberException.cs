
// Type: Intermech.Expressions.Exceptions.WrongArgumentsNumberException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech.Expressions.Exceptions
{
    /// <summary>Wrong arguments number.</summary>
    [Serializable]
    public class WrongArgumentsNumberException : ParseException
    {
      private int _argsCount;

      public WrongArgumentsNumberException(int pos, int args)
        : base(LocalizationHolder.rm.GetString("Interfaces_55"), pos)
      {
        this._argsCount = args;
      }

      /// <summary>Initializes a new instance of the <see cref="T:System.SystemException"></see> class with serialized data.</summary>
      /// <param name="context">The contextual information about the source or destination. </param>
      /// <param name="info">The object that holds the serialized object data. </param>
      protected WrongArgumentsNumberException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._argsCount = info != null ? info.GetInt32(nameof (ArgsCount)) : throw new ArgumentNullException(nameof (info));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("ArgsCount", this._argsCount);
      }

      public int ArgsCount => this._argsCount;
    }
}
