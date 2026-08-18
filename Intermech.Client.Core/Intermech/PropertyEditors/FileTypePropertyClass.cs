
// Type: Intermech.PropertyEditors.FileTypePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.PropertyEditors;

public class FileTypePropertyClass
{
  private FileTypes fileType;

  public FileTypes FileType => this.fileType;

  public FileTypePropertyClass(FileTypes aFileType) => this.fileType = aFileType;

  public override string ToString() => EnumDescConverter.GetEnumDescription((Enum) this.fileType);
}
