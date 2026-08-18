
// Type: Intermech.Interfaces.Data.Actions.UploadFileInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Interfaces.Data.Actions;

public class UploadFileInfo
{
  private string fileName;
  private string fullFileName;
  private FileTypes fileType;

  public UploadFileInfo(string fileName, string fullFileName, FileTypes fileType = FileTypes.ftNormal)
  {
    this.fileName = fileName;
    this.fullFileName = fullFileName;
    this.fileType = fileType;
  }

  public string FileName => this.fileName;

  public string FullFileName => this.fullFileName;

  public FileTypes FileType => this.fileType;
}
