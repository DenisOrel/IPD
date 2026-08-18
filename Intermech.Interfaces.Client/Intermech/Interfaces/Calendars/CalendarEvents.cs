// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Calendars.CalendarEvents
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.Client;
using System;

#nullable disable
namespace Intermech.Interfaces.Calendars;

/// <summary>События, связанные с календарём</summary>
public static class CalendarEvents
{
  public const string Changed = "CalendarChanged";

  [Serializable]
  public class ChangedArgs : NotificationEventArgs
  {
    [NotEmpty]
    public long SourceObjectID { get; }

    public CalendarOwnerType OwnerType { get; }

    public ChangedArgs(CalendarOwnerType ownerType, [NotEmpty] long sourceObjectID, bool firePrePostEvents = false)
      : base("CalendarChanged", firePrePostEvents)
    {
      this.OwnerType = ownerType;
      this.SourceObjectID = sourceObjectID;
    }
  }
}
