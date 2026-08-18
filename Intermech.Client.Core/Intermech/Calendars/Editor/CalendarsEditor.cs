
// Type: Intermech.Calendars.Editor.CalendarsEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;


namespace Intermech.Calendars.Editor;

internal static class CalendarsEditor
{
  internal static void Init([NotNull] IServiceProvider serviceProvider, [CanBeNull] IUserSession session = null)
  {
    ApplicationServices.Container.GetService<ICalendarsEditorLoader>().Init(serviceProvider, session);
  }
}
