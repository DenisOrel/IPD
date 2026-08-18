using System;

namespace Intermech.ComparisonPlugins.PDFComparison.Common
{
    public class FileDescription
    {
      public static readonly FileDescription Empty = new FileDescription(string.Empty, string.Empty, 0L, FileTypes.ftUnknown, new DateTime(), new byte[0]);

      public string Caption { get; }

      public string FileName { get; }

      public long RealFileSize { get; }

      public FileTypes FileType { get; }

      public DateTime ModifyDate { get; }

      public byte[] FileData { get; }

      public FileDescription(
        string caption,
        string fileName,
        long realFileSize,
        FileTypes fileType,
        DateTime modifyDate,
        byte[] fileData)
      {
        this.Caption = $"{caption} [{fileName}]";
        this.FileName = fileName;
        this.RealFileSize = realFileSize;
        this.FileType = fileType;
        this.ModifyDate = modifyDate;
        this.FileData = fileData;
      }
    }
}
