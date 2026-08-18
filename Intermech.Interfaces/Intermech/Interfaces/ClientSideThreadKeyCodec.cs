
// Type: Intermech.Interfaces.ClientSideThreadKeyCodec
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Concurrent;


namespace Intermech.Interfaces
{
    internal sealed class ClientSideThreadKeyCodec
    {
      private ConcurrentDictionary<(Guid, int), string> encodeCache;
      private Func<(Guid, int), string> addToEncodeCacheMethod;

      public ClientSideThreadKeyCodec()
      {
        this.encodeCache = new ConcurrentDictionary<(Guid, int), string>();
        this.addToEncodeCacheMethod = new Func<(Guid, int), string>(this.EncodeSlow);
      }

      public string Encode(Guid appKey, int threadId)
      {
        return this.encodeCache.GetOrAdd((appKey, threadId), this.addToEncodeCacheMethod);
      }

      private string EncodeSlow((Guid, int) clientAppKeyAndThreadId)
      {
        (Guid guid, int num) = clientAppKeyAndThreadId;
        return $"{guid.ToString("N")}-{num.ToString()}";
      }
    }
}
