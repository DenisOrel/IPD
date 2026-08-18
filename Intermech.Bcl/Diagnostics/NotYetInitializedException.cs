
// Type: Intermech.Diagnostics.NotYetInitializedException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    [Serializable]
    public class NotYetInitializedException : InvalidOperationException, ISerializable
    {
      [CanBeNull]
      [NotWhitespace]
      public string ContainerName { get; }

      protected NotYetInitializedException()
      {
      }

      protected NotYetInitializedException([NotNull] Type containerType)
        : this()
      {
        this.ContainerName = containerType.FullName;
      }

      [NotNull]
      public static NotYetInitializedException ForContainer(
        [NotNull, NotWhitespace] string containerName,
        [CanBeNull] Exception innerException = null)
      {
        return new NotYetInitializedException(containerName, (string) null, innerException);
      }

      public NotYetInitializedException([NotNull, NotWhitespace] string containerName, [CanBeNull, NotWhitespace] string message)
        : this(containerName, message, (Exception) null)
      {
        this.ContainerName = message;
      }

      public NotYetInitializedException([NotNull, NotWhitespace] string containerName, [CanBeNull, NotWhitespace] string message, [CanBeNull] Exception innerException)
        : base(string.IsNullOrWhiteSpace(message) ? (string.IsNullOrWhiteSpace(containerName) ? "Container not yet initialized!" : containerName + " not yet initialized!") : message, innerException)
      {
        this.ContainerName = containerName;
      }

      protected NotYetInitializedException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.ContainerName = info.GetString(nameof (ContainerName));
      }

      public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("ContainerName", (object) this.ContainerName);
      }

      [NotNull]
      public override string Message
      {
        get
        {
          return !string.IsNullOrWhiteSpace(this.ContainerName) ? this.ContainerName + " not yet initialized!" : "Container not yet initialized!";
        }
      }
    }
}
