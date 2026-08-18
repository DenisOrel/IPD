// Decompiled with JetBrains decompiler
// Type: Intermech.Search.EventLogFilters.EventLogFiltersServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Search.EventLogFilters;

public sealed class EventLogFiltersServerService : LongLifeObject, IEventLogFiltersServerService
{
  private const string EventLogFiltersFileName = "EventLogFilters";

  public EventLogFilter[] GetAllFilters(Guid userSessionGuid)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this.GetAllFilters();
  }

  public void SaveFilters(Guid userSessionGuid, EventLogFilter[] filters)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (filters == null)
        throw new ArgumentNullException(nameof (filters));
      this.SaveFilters(filters);
    }
  }

  private EventLogFilter[] GetAllFilters()
  {
    List<EventLogFilter> eventLogFilterList = new List<EventLogFilter>()
    {
      EventLogFilter.AllEventsFilter
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new BlobProcReader(sessionKeeper.Session.Configurations.GetConfigAttribute("EventLogFilters"), 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
        if (memoryStream.Length > 0L)
        {
          memoryStream.Seek(0L, SeekOrigin.Begin);
          BinaryFormatter binaryFormatter = new BinaryFormatter();
          try
          {
            eventLogFilterList.AddRange((IEnumerable<EventLogFilter>) (EventLogFilter[]) binaryFormatter.Deserialize((Stream) memoryStream));
          }
          catch
          {
          }
        }
      }
    }
    return eventLogFilterList.ToArray();
  }

  private void SaveFilters(EventLogFilter[] filters)
  {
    filters = ((IEnumerable<EventLogFilter>) filters).Where<EventLogFilter>((Func<EventLogFilter, bool>) (o => o.Guid != EventLogFilter.AllEventsFilter.Guid)).ToArray<EventLogFilter>();
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) filters);
      serializationStream.Seek(0L, SeekOrigin.Begin);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute configAttribute;
        BlobInformation aBlobInformation = ((IBlobReader) (configAttribute = sessionKeeper.Session.Configurations.GetConfigAttribute("EventLogFilters"))).OpenBlob(-1) with
        {
          ArcMethod = ArcMethods.ZLibPacked
        };
        MemoryStream aSourceStream = serializationStream;
        new BlobProcWriter(configAttribute, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      }
    }
  }
}
