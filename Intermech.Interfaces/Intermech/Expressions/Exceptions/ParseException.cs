
// Type: Intermech.Expressions.Exceptions.ParseException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Expressions.Exceptions
{
    /// <summary>
    /// The base class for any exception thrown in <see cref="M:Intermech.Expressions.Parser.Parse(System.String)" /> method.
    /// </summary>
    [Serializable]
    public class ParseException : ApplicationException
    {
      private int _errorPos;
      protected string _token;

      public ParseException(string message)
        : base(message)
      {
        this._errorPos = -1;
        this._token = string.Empty;
      }

      public ParseException(string message, int errPos)
        : this(message, errPos, string.Empty)
      {
      }

      public ParseException(string message, int errPos, string token)
        : base(message)
      {
        this._token = token;
        this._errorPos = errPos;
      }

      /// <summary>Initializes a new instance of the <see cref="T:System.SystemException"></see> class with serialized data.</summary>
      /// <param name="context">The contextual information about the source or destination. </param>
      /// <param name="info">The object that holds the serialized object data. </param>
      protected ParseException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._errorPos = info != null ? info.GetInt32("ErrorPos") : throw new ArgumentNullException(nameof (info));
        this._token = info.GetString(nameof (Token));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("ErrorPos", this._errorPos);
        info.AddValue("Token", (object) this._token);
      }

      /// <summary>Position of invalid character.</summary>
      public virtual int InvalidCharacterPosition => this._errorPos;

      /// <summary>Token</summary>
      public string Token => this._token;
    }
}
