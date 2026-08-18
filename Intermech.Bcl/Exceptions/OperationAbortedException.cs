
// Type: Intermech.Exceptions.OperationAbortedException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Runtime.Serialization;


namespace Intermech.Exceptions
{
    /// <summary>(Serializable) Операция отменена в силу описанных причин.</summary>
    [Serializable]
    public class OperationAbortedException : SystemException, ISerializable
    {
      /// <summary>Описание отменённой операции</summary>
      [CanBeNull]
      [CanBeEmpty]
      public string OperationName { get; }

      /// <summary>Причина отмены операции</summary>
      [CanBeNull]
      [CanBeEmpty]
      public string Reason { get; }

      public OperationAbortedException()
      {
      }

      [NotNull]
      [NotEmpty]
      private static string GetMessage([CanBeNull, CanBeEmpty] string operationName, [CanBeNull, CanBeEmpty] string reason = null)
      {
        if (!string.IsNullOrWhiteSpace(operationName))
        {
          if (string.IsNullOrWhiteSpace(reason))
            return $"Операция \"{operationName}\" отменена";
          return $"Операция \"{operationName}\" отменена: {Environment.NewLine}{reason}";
        }
        return !string.IsNullOrWhiteSpace(reason) ? $"Операция отменена: {Environment.NewLine}{reason}" : "Операция отменена";
      }

      public OperationAbortedException([CanBeNull, CanBeEmpty] string operationName, [CanBeNull, CanBeEmpty] string reason = null, [CanBeNull] Exception innerException = null)
        : base(OperationAbortedException.GetMessage(operationName, reason), innerException)
      {
        this.OperationName = operationName;
        this.Reason = reason;
      }

      public OperationAbortedException([CanBeNull, CanBeEmpty] string operationName, [CanBeNull] Exception innerException)
        : base(OperationAbortedException.GetMessage(operationName), innerException)
      {
        this.OperationName = operationName;
      }

      protected OperationAbortedException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.OperationName = info.GetString(nameof (OperationName));
        this.Reason = info.GetString(nameof (Reason));
      }

      public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("OperationName", (object) this.OperationName);
        info.AddValue("Reason", (object) this.Reason);
      }

      [CanBeNull]
      protected string OriginalMessage => this.Message;
    }
}
