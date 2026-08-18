
// Type: Intermech.AttributablesFoundException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech
{
    /// <summary>
    /// Базовый класс исключения при обработке сущностей, содержащих атрибуты (т.е. объектов и связей)
    /// </summary>
    [Serializable]
    public class AttributablesFoundException : KernelException
    {
      protected string _AttributablesListCaption;
      protected long[] _AttributablesID;

      public AttributablesFoundException(string message)
        : base(message)
      {
      }

      public AttributablesFoundException(string message, Exception innerException)
        : base(message, innerException)
      {
      }

      public AttributablesFoundException()
      {
      }

      protected AttributablesFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }

      protected AttributablesFoundException(
        string message,
        string attributableListCaption,
        long[] attributableID)
        : base(message)
      {
        this._AttributablesListCaption = attributableListCaption;
        this._AttributablesID = attributableID;
      }
    }
}
