
// Type: Intermech.Interfaces.IVaultFileReader
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Remoting.Compression;


namespace Intermech.Interfaces
{
    [RemotingCompression(false)]
    public interface IVaultFileReader
    {
      BlobInformation OpenBlob(int dataBlockSize, long objectID, int historyID, long storageID);

      byte[] ReadDataBlock();

      byte[] ReadDataBlock(int dataBlockSize);

      void CloseBlob();
    }
}
