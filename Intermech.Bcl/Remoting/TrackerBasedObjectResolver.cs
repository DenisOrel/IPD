
// Type: Intermech.Remoting.TrackerBasedObjectResolver
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Threading;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Services;
using System.Threading;


namespace Intermech.Remoting
{
    public sealed class TrackerBasedObjectResolver : ITrackingHandler, IRemotingObjectResolver
    {
      private readonly ReaderWriterLockSlim rwl;
      private readonly Dictionary<string, WeakReference> uriTable;

      public TrackerBasedObjectResolver()
      {
        this.rwl = new ReaderWriterLockSlim();
        this.uriTable = new Dictionary<string, WeakReference>(1024 /*0x0400*/);
      }

      public MarshalByRefObject TryGetObject(string uri)
      {
        using (new DataReadLockSlim(this.rwl))
        {
          WeakReference weakReference;
          if (this.uriTable.TryGetValue(uri, out weakReference))
            return (MarshalByRefObject) weakReference.Target;
        }
        return (MarshalByRefObject) null;
      }

      public void MarshaledObject(object obj, ObjRef or)
      {
        if (!(obj is MarshalByRefObject) || string.IsNullOrEmpty(or.URI))
          return;
        using (new DataWriteLockSlim(this.rwl))
          this.uriTable[or.URI] = new WeakReference(obj);
      }

      public void UnmarshaledObject(object obj, ObjRef or)
      {
      }

      public void DisconnectedObject(object obj)
      {
        string objectUri = RemotingServices.GetObjectUri((MarshalByRefObject) obj);
        if (string.IsNullOrEmpty(objectUri))
          return;
        using (new DataWriteLockSlim(this.rwl))
          this.uriTable.Remove(objectUri);
      }
    }
}
