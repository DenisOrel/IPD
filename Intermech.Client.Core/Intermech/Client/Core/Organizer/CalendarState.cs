
// Type: Intermech.Client.Core.Organizer.CalendarState
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.Organizer;

/// <summary>Possible states of the calendar</summary>
public enum CalendarState
{
  /// <summary>Nothing happening</summary>
  Idle,
  /// <summary>
  /// User is currently dragging on view to select a time range
  /// </summary>
  DraggingTimeSelection,
  /// <summary>User is currently dragging an item among the view</summary>
  DraggingItem,
  /// <summary>User is editing an item's Text</summary>
  EditingItemText,
  /// <summary>User is currently resizing an item</summary>
  ResizingItem,
}
