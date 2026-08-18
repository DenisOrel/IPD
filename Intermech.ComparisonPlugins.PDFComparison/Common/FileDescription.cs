// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.Common.FileDescription
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using System;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison.Common;

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
