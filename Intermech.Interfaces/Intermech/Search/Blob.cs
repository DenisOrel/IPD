
// Type: Intermech.Search.Blob
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.Data;
using System;


namespace Intermech.Search
{
    public sealed class Blob
    {
      public Blob(BlobKey key)
      {
        this.Key = key != null ? key : throw new ArgumentNullException(nameof (key));
      }

      public BlobKey Key { get; private set; }

      [Column("F_KEY")]
      public long ID { get; set; }

      [Column("F_FILENAME")]
      public string FileName { get; set; }

      [Column("F_VALUE")]
      public byte[] Value { get; set; }

      [Column("F_ARC_METHOD")]
      public ArcMethods ArcMethod { get; set; }

      public FileTypes FileType { get; set; }

      [Column("F_NOTE")]
      public string Note { get; set; }

      [Column("F_ZIPSIZE")]
      public long PackedFileSize { get; set; }

      [Column("F_FILESIZE")]
      public long RealFileSize { get; set; }
    }
}
