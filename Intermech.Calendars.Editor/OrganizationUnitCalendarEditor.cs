
// Type: Intermech.Calendars.Editor.OrganizationUnitCalendarEditor
// Assembly: Intermech.Calendars.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0D5478F2-D4B6-4EDD-A444-F5E197647782
:\IPS\Client\Intermech.Calendars.Editor.dll

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


namespace Intermech.Calendars.Editor;

[ViewDescriptionProvider(typeof (OrganizationUnitCalendarEditor.Description))]
public class OrganizationUnitCalendarEditor : 
  CalendarEditorBase,
  ICommandTarget,
  ICommandTarget2,
  IView
{
  [NotNull]
  public static NavigatorObjectViewDescriptor Descriptor { get; } = new NavigatorObjectViewDescriptor(typeof (OrganizationUnitCalendarEditor), caption: Localization.GetString("Calendar_Editor_organization"), hint: Localization.GetString("Calendar_edit_or_view_organization"), orderID: 25, helpTopicID: 2527, filter: new NavigatorViewDescriptor<IDBTypedObjectID>.CanShowForItemsDelegate(OrganizationUnitCalendarEditor.FilterVisibility));

  public static bool FilterVisibility(
    [NotNull] IServiceProvider services,
    [NotNull] IReadOnlyCollection<IDBTypedObjectID> selectedObjects)
  {
    if (selectedObjects.Count != 1)
      return false;
    IDBTypedObjectID dbTypedObjectId = selectedObjects.First<IDBTypedObjectID>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.CheckObjectHasObjLink(dbTypedObjectId.ObjectID, Intermech.Metadata.Attributes.Calendar);
  }

  [CanBeNull]
  private OrganizationUnitCalendar Calendar
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (OrganizationUnitCalendar) base.Calendar;
    }
    set => this.Calendar = (CalendarBase) value;
  }

  protected override void CheckCalendarType([NotNull] CalendarBase calendar)
  {
  }

  protected override CalendarOwnerType CalendarOwnerType => CalendarOwnerType.OrganizationUnit;

  [NotEmpty]
  protected override long CalendarOwnerID { get; set; }

  [NotEmpty]
  public long UnitID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.CalendarOwnerID;
  }

  public override IBlobWriter GetCalendarWriter([NotNull] IUserSession session)
  {
    Intermech.Diagnostics.Check.NotNull<OrganizationUnitCalendar>(this.Calendar, "Calendar");
    return this.Calendar.GetCalendarWriter(session, true);
  }

  [NotNull]
  public override CalendarBase GetCalendar([NotNull] IUserSession session)
  {
    return (CalendarBase) CalendarLoader.GetOrganizationUnitCalendar(session, this.UnitID);
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
        Caption = Localization.GetString("Calendar_Editor_organization"),
        ImageIndex = -1,
        OrderID = 0
      };
    }
  }
}
