
// Type: Intermech.Calendars.Editor.UserCalendarEditor
// Assembly: Intermech.Calendars.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0D5478F2-D4B6-4EDD-A444-F5E197647782
:\IPS\Client\Intermech.Calendars.Editor.dll

using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Runtime.CompilerServices;


namespace Intermech.Calendars.Editor;

[ViewDescriptionProvider(typeof (UserCalendarEditor.Description))]
public class UserCalendarEditor : CalendarEditorBase, ICommandTarget, ICommandTarget2, IView
{
  [NotNull]
  public static NavigatorObjectViewDescriptor Descriptor { get; } = new NavigatorObjectViewDescriptor(typeof (UserCalendarEditor), caption: Localization.GetString("Calendar_Editor_user"), hint: Localization.GetString("Calendar_edit_or_view_user"), orderID: 25, helpTopicID: 2527);

  [CanBeNull]
  private UserCalendar Calendar
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (UserCalendar) base.Calendar;
    set => this.Calendar = (CalendarBase) value;
  }

  protected override void CheckCalendarType([NotNull] CalendarBase calendar)
  {
  }

  protected override CalendarOwnerType CalendarOwnerType => CalendarOwnerType.User;

  [NotEmpty]
  protected override long CalendarOwnerID { get; set; }

  [NotEmpty]
  public long UserID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.CalendarOwnerID;
  }

  public override IBlobWriter GetCalendarWriter([NotNull] IUserSession session)
  {
    Intermech.Diagnostics.Check.NotNull<UserCalendar>(this.Calendar, "Calendar");
    return this.Calendar.GetCalendarWriter(session, true);
  }

  [NotNull]
  public override CalendarBase GetCalendar([NotNull] IUserSession session)
  {
    return (CalendarBase) CalendarLoader.GetUserCalendar(session, this.UserID, false) ?? (CalendarBase) new UserCalendar(this.UserID);
  }

  protected new class Description : CalendarEditorBase.Description
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList))
        ServicesManager.GetService(typeof (INamedImageList));
      return new ViewDescription()
      {
        Caption = Localization.GetString("Calendar_Editor_user"),
        ImageIndex = -1,
        OrderID = 0
      };
    }
  }
}
