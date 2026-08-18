// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.TablesMergingHelper
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.IO;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Interfaces.Imbase;

public static class TablesMergingHelper
{
  public static DataSet UnpackDataSetFromAttribute(IDBAttribute attr)
  {
    IBlobReader blobReader = (IBlobReader) attr;
    BlobInformation blobInformation = blobReader.OpenBlob(0);
    try
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      DataSet dataSet = (DataSet) null;
      using (ImChunkedStream imChunkedStream1 = new ImChunkedStream())
      {
        byte[] buffer = blobReader.ReadDataBlock();
        imChunkedStream1.Write(buffer, 0, buffer.Length);
        imChunkedStream1.Position = 0L;
        if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
        {
          using (ImChunkedStream imChunkedStream2 = new ImChunkedStream())
          {
            ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) imChunkedStream2, (Stream) imChunkedStream1);
            imChunkedStream2.Position = 0L;
            dataSet = (DataSet) binaryFormatter.Deserialize((Stream) imChunkedStream2);
          }
        }
        else
          dataSet = (DataSet) binaryFormatter.Deserialize((Stream) imChunkedStream1);
      }
      return dataSet;
    }
    finally
    {
      blobReader.CloseBlob();
    }
  }
}
