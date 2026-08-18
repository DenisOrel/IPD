
// Type: Intermech.Settings.SyncValueCell`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Threading;
using System;


namespace Intermech.Settings
{
    public class SyncValueCell<T> : ValueCell<T>
    {
      private readonly object syncRoot;

      public SyncValueCell(object syncRoot, T value)
        : base(value)
      {
        this.syncRoot = syncRoot != null ? syncRoot : throw new ArgumentNullException(nameof (syncRoot));
      }

      public override void Invalidate()
      {
        using (new FastSyncRootLock(this.syncRoot))
          base.Invalidate();
      }

      public override void Validate()
      {
        using (new FastSyncRootLock(this.syncRoot))
          base.Validate();
      }

      public override T Value
      {
        get
        {
          using (new FastSyncRootLock(this.syncRoot))
            return base.Value;
        }
      }

      public override ValueCellState State
      {
        get
        {
          using (new FastSyncRootLock(this.syncRoot))
            return base.State;
        }
      }

      public override string Error
      {
        get
        {
          using (new FastSyncRootLock(this.syncRoot))
            return base.Error;
        }
        set
        {
          using (new FastSyncRootLock(this.syncRoot))
            base.Error = value;
        }
      }

      public override T RawValue
      {
        get
        {
          using (new FastSyncRootLock(this.syncRoot))
            return base.RawValue;
        }
        set
        {
          using (new FastSyncRootLock(this.syncRoot))
            base.RawValue = value;
        }
      }
    }
}
