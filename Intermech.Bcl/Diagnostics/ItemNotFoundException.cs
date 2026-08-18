
// Type: Intermech.Diagnostics.ItemNotFoundException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    /// <summary>Ошибка поиска элемента</summary>
    [Serializable]
    public class ItemNotFoundException : Exception, ISerializable
    {
      public ItemNotFoundException()
      {
      }

      public ItemNotFoundException([CanBeNull] string message)
        : base(message)
      {
      }

      public ItemNotFoundException([CanBeNull] string message, [CanBeNull] Exception innerException)
        : base(message, innerException)
      {
      }

      protected ItemNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }

      [CanBeNull]
      protected string OriginalMessage => base.Message;

      [NotNull]
      public override string Message
      {
        get
        {
          return !string.IsNullOrWhiteSpace(this.OriginalMessage) ? this.OriginalMessage : "Item not found!";
        }
      }
    }
}
