
// Type: Intermech.Client.Core.FormDesigner.Controls.PictureBoxImageData
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.IO;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Хранит информацию о загруженном изображении для последющего сохранения в базу при сохранении изменений в форме
/// </summary>
public class PictureBoxImageData : ICloneable
{
  public long BlobID;
  public byte[] Buffer;
  public string FileName = string.Empty;
  public DateTime FileDate = DateTime.MinValue;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="stream">Открытый поток</param>
  /// <param name="filename">имя файла</param>
  /// <param name="filedate">дата</param>
  public PictureBoxImageData(Stream stream, long blobID, string filename, DateTime filedate)
  {
    this.BlobID = blobID;
    this.Buffer = new byte[stream.Length];
    stream.Position = 0L;
    stream.Read(this.Buffer, 0, this.Buffer.Length);
    this.FileName = filename;
    this.FileDate = filedate;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="buffer">данные не клонируются</param>
  /// <param name="blobID"></param>
  /// <param name="filename"></param>
  /// <param name="filedate"></param>
  public PictureBoxImageData(byte[] buffer, long blobID, string filename, DateTime filedate)
  {
    this.BlobID = blobID;
    this.Buffer = buffer;
    this.FileName = filename;
    this.FileDate = filedate;
  }

  public object Clone()
  {
    return (object) new PictureBoxImageData(this.Buffer.Clone() as byte[], this.BlobID, this.FileName, this.FileDate);
  }
}
