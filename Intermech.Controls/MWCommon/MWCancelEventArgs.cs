
// Type: MWCommon.MWCancelEventArgs
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.ComponentModel;


namespace MWCommon;

/// <summary>
/// MWCancelEventArgs class.
/// The MWCancelEventArgs takes two objects as arguments. These two objects are the current value and the proposed value. These objects
/// 	can be used when setting up EventHandlers for the properties that use them so that the programmer will know what the current
/// 	and proposed values are.
/// Note that the MWCancelEventArgs should be used in an OnBeforePROPERTYChanged property - BEFORE the value of the property is changed.
/// </summary>
public class MWCancelEventArgs : CancelEventArgs
{
  /// <summary>The current object before the property is changed.</summary>
  private object oCurrent;
  /// <summary>
  /// The proposed object that will be used if the property is changed.
  /// </summary>
  private object oProposed;

  /// <summary>Standard constructor.</summary>
  public MWCancelEventArgs()
  {
  }

  /// <summary>
  /// Standard Constructor taking the current value of the property and the proposed value of the property as arguments.
  /// </summary>
  /// <param name="current">The current object before the property is changed.</param>
  /// <param name="proposed">The proposed object that will be used if the property is changed.</param>
  public MWCancelEventArgs(object current, object proposed)
  {
    this.oCurrent = current;
    this.oProposed = proposed;
  }

  /// <summary>The current object before the property is changed.</summary>
  public object Current => this.oCurrent;

  /// <summary>
  /// The proposed object that will be used if the property is changed.
  /// </summary>
  public object Proposed => this.oProposed;
}
