
// Type: Intermech.Navigator.Parts.PartGuidMapper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Threading;


namespace Intermech.Navigator.Parts;

public class PartGuidMapper
{
  private static Dictionary<Guid, int> guidToId = new Dictionary<Guid, int>();
  private static Dictionary<int, Guid> idToGuid = new Dictionary<int, Guid>();
  private static int nextId = 0;
  private static ReaderWriterLock rwl = new ReaderWriterLock();

  public static int GetUniqueId(Guid guid)
  {
    PartGuidMapper.rwl.AcquireReaderLock(-1);
    try
    {
      if (!PartGuidMapper.guidToId.ContainsKey(guid))
      {
        LockCookie writerLock = PartGuidMapper.rwl.UpgradeToWriterLock(-1);
        try
        {
          if (!PartGuidMapper.guidToId.ContainsKey(guid))
          {
            int key = PartGuidMapper.nextId++;
            PartGuidMapper.guidToId.Add(guid, key);
            PartGuidMapper.idToGuid.Add(key, guid);
          }
        }
        finally
        {
          PartGuidMapper.rwl.DowngradeFromWriterLock(ref writerLock);
        }
      }
      return PartGuidMapper.guidToId[guid];
    }
    finally
    {
      PartGuidMapper.rwl.ReleaseReaderLock();
    }
  }

  public static Guid GetGuid(int uniqueId)
  {
    PartGuidMapper.rwl.AcquireReaderLock(-1);
    try
    {
      return PartGuidMapper.idToGuid.ContainsKey(uniqueId) ? PartGuidMapper.idToGuid[uniqueId] : Guid.Empty;
    }
    finally
    {
      PartGuidMapper.rwl.ReleaseReaderLock();
    }
  }
}
