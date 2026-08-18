
// Type: Intermech.Client.Core.Organizer.InputState
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.Organizer;

/// <summary>Indicates what input has been given to the control</summary>
public enum InputState
{
  /// <summary>Indicates that no input has been given</summary>
  Normal,
  /// <summary>
  /// Indicates that the user is currently clicking on the control
  /// </summary>
  Clicked,
  /// <summary>
  /// Indicates that the user is currently hovering the control with the mouse
  /// </summary>
  Hovered,
}
