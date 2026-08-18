
// Type: Intermech.Expressions.Exceptions.MissingSymbolException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech.Expressions.Exceptions
{
    /// <summary>Missing '"' or '#'.</summary>
    [Serializable]
    public class MissingSymbolException : ParseException
    {
      private char _symbol;

      public MissingSymbolException(char symbol)
        : base(string.Format(LocalizationHolder.rm.GetString("Interfaces_49"), (object) symbol))
      {
        this._symbol = symbol;
      }

      /// <summary>Initializes a new instance of the <see cref="T:System.SystemException"></see> class with serialized data.</summary>
      /// <param name="context">The contextual information about the source or destination. </param>
      /// <param name="info">The object that holds the serialized object data. </param>
      protected MissingSymbolException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._symbol = info != null ? info.GetChar("Symbol") : throw new ArgumentNullException(nameof (info));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("Symbol", this._symbol);
      }

      public char InvalidSymbol
      {
        get => this._symbol;
        set => this._symbol = value;
      }
    }
}
