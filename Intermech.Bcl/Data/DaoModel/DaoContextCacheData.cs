
// Type: Intermech.Data.DaoModel.DaoContextCacheData
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Diagnostics;


namespace Intermech.Data.DaoModel
{
    internal sealed class DaoContextCacheData
    {
      private readonly object firstOpenSyncObject;
      private bool firstOpenComplete;

      public DaoContextCacheData()
      {
        this.firstOpenSyncObject = new object();
        this.firstOpenComplete = false;
      }

      public object FirstOpenSyncObject
      {
        [DebuggerStepThrough] get => this.firstOpenSyncObject;
      }

      public bool FirstOpenComplete
      {
        [DebuggerStepThrough] get => this.firstOpenComplete;
        [DebuggerStepThrough] set => this.firstOpenComplete = value;
      }
    }
}
