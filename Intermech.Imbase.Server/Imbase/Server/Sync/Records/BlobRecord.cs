// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.Records.BlobRecord
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Imbase.Server.Sync.Records;

internal class BlobRecord
{
  public int Key;
  public int Used;
  public byte[] Blob;
  public string Source;
  public int Hash;
  public int Length;
  public bool IsPicture;
  public string Memo = string.Empty;

  public BlobRecord(DataRow row)
  {
    this.Key = Convert.ToInt32(row["F_KEY"]);
    this.Used = Convert.ToInt32(row["F_USED"]);
    this.Source = Path.GetFileName(Convert.ToString(row["F_SOURCE"]).Trim());
    this.Hash = Convert.ToInt32(row["F_HASH"]);
    string upper = Path.GetExtension(this.Source).ToUpper();
    this.IsPicture = upper.Equals(".BMP") || upper.Equals(".GIF") || upper.Equals(".TIF") || upper.Equals(".TIFF") || upper.Equals(".PNG") || upper.Equals(".JPG") || upper.Equals(".JPEG") || upper.Equals(".EXIF") || upper.Equals(".ICO") || upper.Equals(".EMF") || upper.Equals(".WMF") || upper.Equals(".SLD");
    if (!Convert.IsDBNull(row["F_BLOB"]))
    {
      byte[] array = (byte[]) row["F_BLOB"];
      if ((array.Length <= 4 || array[0] != (byte) 90 || array[1] != (byte) 76 || array[2] != (byte) 73 ? 0 : (array[3] == (byte) 66 ? 1 : 0)) != 0)
      {
        List<byte> byteList = new List<byte>((IEnumerable<byte>) array);
        byteList.RemoveRange(0, 4);
        using (MemoryStream inStream = new MemoryStream(byteList.ToArray()))
        {
          using (MemoryStream outStream = new MemoryStream())
          {
            ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) outStream, (Stream) inStream);
            outStream.Position = 0L;
            array = outStream.ToArray();
            if (!this.IsPicture)
            {
              using (StreamReader streamReader = new StreamReader((Stream) outStream, Encoding.Default))
                this.Memo = streamReader.ReadToEnd();
            }
          }
        }
      }
      this.Blob = array;
      this.Length = array.Length;
    }
    else
      this.Length = 0;
  }
}
