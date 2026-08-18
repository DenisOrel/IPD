
// Type: Intermech.Navigator.DiskStorage.FileHistoryNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.DiskStorage;

public class FileHistoryNodeID : INodeID
{
  /// <summary>id файла</summary>
  private long fileID;
  /// <summary>
  /// id истории файла
  /// уникально в пределах id объекта(не версии)
  /// для рабочего и удалённого файла = 0
  /// </summary>
  private int historyID;
  /// <summary>id версии объекта, которому принадлежит файл</summary>
  private long objectID;
  private long realSize;
  private long packedFileSize;
  private ArcMethods arc;
  /// <summary>имя файла</summary>
  private string fileName;
  /// <summary>id шкафа, в котором расположен файл</summary>
  private long storageID;
  private object cookie;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="fileID"> id файла</param>
  /// <param name="historyID">id истории файла</param>
  /// <param name="objectID">id версии объекта, которому принадлежит файл</param>
  /// <param name="realSize"></param>
  /// <param name="zipSize"></param>
  /// <param name="fileName"></param>
  /// <param name="arc"></param>
  /// <param name="storageID"></param>
  public FileHistoryNodeID(
    long fileID,
    int historyID,
    long objectID,
    long realSize,
    long zipSize,
    string fileName,
    ArcMethods arc,
    long storageID)
  {
    this.fileID = fileID;
    this.historyID = historyID;
    this.objectID = objectID;
    this.realSize = realSize;
    this.packedFileSize = zipSize;
    this.fileName = fileName;
    this.arc = arc;
    this.storageID = storageID;
  }

  /// <summary>ID файла в шкафу</summary>
  public long FileID => this.fileID;

  /// <summary>
  /// id истории файла
  /// уникально в пределах id объекта(не версии)
  /// для рабочего и удалённого файла = 0
  /// </summary>
  public int HistoryID => this.historyID;

  /// <summary>id версии объекта, которому принадлжеит файл</summary>
  public long ObjectID => this.objectID;

  public long RealSize => this.realSize;

  public long PackedFileSize => this.packedFileSize;

  public ArcMethods ArcMethod => this.arc;

  public string FileName => this.fileName;

  public long StorageID => this.storageID;

  int INodeID.CategoryID => 21;

  int INodeID.TypeID => 0;

  object INodeID.Cookie
  {
    get => this.cookie;
    set => this.cookie = value;
  }

  public override bool Equals(object obj)
  {
    return obj is FileHistoryNodeID fileHistoryNodeId && this.fileID == fileHistoryNodeId.FileID && this.historyID == fileHistoryNodeId.HistoryID;
  }

  public override int GetHashCode() => base.GetHashCode();
}
