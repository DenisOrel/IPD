
// Type: Intermech.Client.Core.Organizer.ISelectableElement
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Represents a clickable element of <see cref="T:Intermech.Client.Core.Organizer.MonthView" /> control
/// </summary>
public interface ISelectableElement
{
  /// <summary>Gets the bounds of the element</summary>
  Rectangle Bounds { get; }

  /// <summary>Gets if the element is currently selected</summary>
  bool Selected { get; }
}
