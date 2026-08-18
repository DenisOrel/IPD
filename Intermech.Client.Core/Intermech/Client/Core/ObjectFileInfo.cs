
// Type: Intermech.Client.Core.ObjectFileInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;


namespace Intermech.Client.Core;

/// <summary>Класс с данными о файле</summary>
public class ObjectFileInfo
{
  /// <summary>
  /// Наименование объекта, к которому относится файл с признаком версии
  /// </summary>
  public string ObjectCaption;
  /// <summary>Индекс файла в атрибуте объекта</summary>
  public int FileIndex;
  private BlobInformation _blobInformation;
  /// <summary>К какому объекту относится файл</summary>
  private long _objectId;

  /// <summary>К какому объекту относится файл</summary>
  public long ObjectId => this._objectId;

  /// <summary>Наименование файла</summary>
  public string FileName => this._blobInformation.FileName;

  /// <summary>Размер файла</summary>
  public string Size
  {
    get
    {
      return string.Format(LocalizationHolder.rm.GetString("Client.Core_949"), (object) Win32Subst.StrFormatByteSize(this._blobInformation.RealFileSize), (object) this._blobInformation.RealFileSize);
    }
  }

  /// <summary>Дата модификации файла</summary>
  public string ModificationDate => this._blobInformation.ModifyDate.ToString();

  public ObjectFileInfo(
    BlobInformation blobInformation,
    int index,
    long objectId,
    string objectCaption)
  {
    this._objectId = objectId;
    this._blobInformation = blobInformation;
    this.FileIndex = index;
    this.ObjectCaption = objectCaption;
  }
}
