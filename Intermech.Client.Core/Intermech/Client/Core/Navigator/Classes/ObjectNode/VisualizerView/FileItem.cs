
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.FileItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.Drawing;
using System.IO;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView;

/// <summary>Информация об отображаемом файле</summary>
public class FileItem
{
  /// <summary>Заголовок объекта</summary>
  public string Caption { get; }

  /// <summary>цвет текста заголовка</summary>
  public Color ColorText { get; set; }

  /// <summary>Имя файла</summary>
  public string FileName { get; set; }

  /// <summary>Полный путь к файлу</summary>
  public string FileFullName { get; set; }

  /// <summary>Идентификатор записи о файле (BLOB-поля)</summary>
  public long BlobID { get; }

  /// <summary>Идентификатор объекта</summary>
  public long ObjectId { get; set; }

  /// <summary>Тип объекта</summary>
  public int ObjectType { get; }

  /// <summary>Идентификатор атрибута</summary>
  public int AttId { get; }

  /// <summary>
  /// Индекс текущего значения в списке атрибутов. По умолчанию = 0, только заголовок = -1</summary>
  public int ValueIndex { get; }

  /// <summary>изображение для строки</summary>
  public Icon Icon { get; }

  /// <summary>индекс во внешней коллекции изображений для строки</summary>
  public int ImageIndex { get; }

  /// <summary>Является ли файл файлом ImViewer</summary>
  public bool IsImViewerFile { get; }

  /// <summary>
  /// 
  /// Состояние файла ImViewer - актуален или нет
  /// </summary>
  public bool? IsViewerFileActual { get; }

  /// <summary>Имя конфигурации CAD-модели</summary>
  public string CadModelNameConfiguration { get; set; }

  /// <summary>true - когда запись с файлом</summary>
  internal bool IsFile => this.ValueIndex != -1;

  /// <summary>Тип файла- основной, аутентичный и т.д.</summary>
  public FileTypes FileType { get; } = FileTypes.ftUnknown;

  /// <summary>создать запись без файла</summary>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="caption">Заголовок объекта</param>
  /// <param name="imageIndex">индекс во внешней коллекции изображений для строки</param>
  /// <param name="valueIndex">-1 = запись без файла</param>
  public FileItem(long objectId, int objectType, string caption, int imageIndex, int valueIndex = -1)
  {
    this.ObjectId = objectId;
    this.ObjectType = objectType;
    this.AttId = -1;
    this.ValueIndex = valueIndex;
    this.ColorText = Color.Empty;
    this.Caption = caption;
    this.ImageIndex = imageIndex;
  }

  /// <summary>создать запись с файлом</summary>
  /// <param name="objectId"></param>
  /// <param name="objectType"></param>
  /// <param name="attId"></param>
  /// <param name="information"></param>
  /// <param name="index"></param>
  public FileItem(
    long objectId,
    int objectType,
    int attId,
    BlobInformation information,
    int index)
  {
    this.ImageIndex = -1;
    this.ObjectId = objectId;
    this.ObjectType = objectType;
    this.AttId = attId;
    this.ValueIndex = index;
    this.ColorText = Color.Empty;
    this.Caption = this.FileName = information.FileName;
    this.BlobID = information.BlobID;
    this.FileType = information.FileType;
    this.Icon = this.ExtractIcon(this.FileName);
  }

  /// <summary>Создать запись для Imv файла</summary>
  /// <param name="objectId"></param>
  /// <param name="objectType"></param>
  /// <param name="attId"></param>
  /// <param name="index"></param>
  public FileItem(
    long objectId,
    int objectType,
    string fullFileName,
    long bolbId,
    bool isImViwerFile,
    bool ImViewerFileState)
  {
    this.ImageIndex = -1;
    this.ObjectId = objectId;
    this.ObjectType = objectType;
    this.AttId = MetaDataHelper.GetAttributeTypeID("cad0004b-306c-11d8-b4e9-00304f19f545");
    this.ValueIndex = 0;
    this.ColorText = SystemColors.ControlText;
    this.FileFullName = fullFileName;
    this.Caption = this.FileName = Path.GetFileName(fullFileName);
    this.BlobID = bolbId;
    this.IsImViewerFile = isImViwerFile;
    this.IsViewerFileActual = new bool?(ImViewerFileState);
    this.Icon = this.ExtractIcon(this.FileName);
  }

  private Icon ExtractIcon(string fileName)
  {
    string lower = Path.GetExtension(fileName)?.ToLower();
    if (string.IsNullOrEmpty(lower))
      return (Icon) null;
    return ServiceUtils.GetService<IIconReader>((object) ServicesManager.ServiceContainer, false)?.GetIconByFileExt(lower);
  }

  public override string ToString() => this.Caption;
}
