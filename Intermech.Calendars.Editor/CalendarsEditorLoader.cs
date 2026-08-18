
// Type: Intermech.Calendars.Editor.CalendarsEditorLoader
// Assembly: Intermech.Calendars.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0D5478F2-D4B6-4EDD-A444-F5E197647782
:\IPS\Client\Intermech.Calendars.Editor.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Metadata;
using System;


namespace Intermech.Calendars.Editor;

public class CalendarsEditorLoader : ICalendarsEditorLoader
{
  public void Init([NotNull] IServiceProvider serviceProvider, [CanBeNull] IUserSession session = null)
  {
    Intermech.Extensions.Client.Library.Init(serviceProvider, session);
    Intermech.Calendars.Library.Init(serviceProvider, session);
    Intermech.Client.Services.NotificationService.Subscribe("CalendarChanged", new NotificationEventHandler(CalendarsEditorLoader.Notification_OnCalendarChanged));
    Navigator.RegisterViewForObjectTypes<CalendarEditor>((OneOrMore<int>) (IpsMetadataEntityBase<int>) ObjectTypes.Calendar);
    Navigator.RegisterViewForObjectTypes<UserCalendarEditor>((OneOrMore<int>) (IpsMetadataEntityBase<int>) ObjectTypes.User);
    Navigator.RegisterViewForObjectTypes<OrganizationUnitCalendarEditor>((OneOrMore<int>) (IpsMetadataEntityBase<int>) ObjectTypes.OrganizationUnits);
  }

  private static void Notification_OnCalendarChanged([CanBeNull] object sender, [NotNull] NotificationEventArgs e)
  {
  }
}
