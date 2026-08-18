
// Type: Intermech.Calendars.Editor.CalendarEditor
// Assembly: Intermech.Calendars.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0D5478F2-D4B6-4EDD-A444-F5E197647782
:\IPS\Client\Intermech.Calendars.Editor.dll

using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Navigator.Views;
using System.Runtime.CompilerServices;


namespace Intermech.Calendars.Editor;

public class CalendarEditor : CalendarEditorBase, ICommandTarget, ICommandTarget2, IView
{
  [NotNull]
  public static NavigatorObjectViewDescriptor Descriptor { get; } = new NavigatorObjectViewDescriptor(typeof (CalendarEditor), caption: Localization.GetString("Calendar_Editor"), hint: Localization.GetString("Calendar_edit_or_view"), helpTopicID: 2527);

  [CanBeNull]
  private Intermech.Calendars.Calendar Calendar
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (Intermech.Calendars.Calendar) base.Calendar;
    set => this.Calendar = (CalendarBase) value;
  }

  protected override void CheckCalendarType([NotNull] CalendarBase calendar)
  {
  }

  protected override CalendarOwnerType CalendarOwnerType => CalendarOwnerType.CalendarObject;

  [NotEmpty]
  protected override long CalendarOwnerID { get; set; }

  [NotEmpty]
  public long CalendarObjectID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.CalendarOwnerID;
  }

  public override IBlobWriter GetCalendarWriter([NotNull] IUserSession session)
  {
    Intermech.Diagnostics.Check.NotNull<Intermech.Calendars.Calendar>(this.Calendar, "Calendar");
    return this.Calendar.GetCalendarWriter(session, true);
  }

  [NotNull]
  public override CalendarBase GetCalendar([NotNull] IUserSession session)
  {
    return (CalendarBase) CalendarLoader.GetCalendarByID(session, this.CalendarObjectID);
  }
}
