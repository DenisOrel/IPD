
// Type: Intermech.DocumentView.ObjectEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.DocumentView;

/// <summary>
/// Holds information for the "Intermech.Map.MapView" events involving both
/// a "Intermech.Map.MapObjectEventArgs.GoObject" and some input event.
/// </summary>
/// <remarks>
/// This class knows about the "Intermech.Map.MapObjectEventArgs.GoObject" that got clicked as well
/// as about how and where the click happened.
/// </remarks>
[Serializable]
public class ObjectEventArgs : InputEventArgs
{
  private IObject myObject;

  public ObjectEventArgs(IObject obj, InputEventArgs evt)
    : base(evt)
  {
    this.myObject = obj;
  }

  public IObject GoObject => this.myObject;
}
