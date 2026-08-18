
// Type: Intermech.Interfaces.BlobAttributeValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Информация о двоичном данном, хранящаяся в самом атрибуте (за исключением ид. файлового шкафа)
    /// </summary>
    [Serializable]
    public class BlobAttributeValue
    {
      public long BlobID;
      public string FileName;
      public DateTime FileModifyDate;

      public BlobAttributeValue(long blobID, string fileName, DateTime fileModifyDate)
      {
        this.BlobID = blobID;
        this.FileName = fileName;
        this.FileModifyDate = fileModifyDate;
      }
    }
}
