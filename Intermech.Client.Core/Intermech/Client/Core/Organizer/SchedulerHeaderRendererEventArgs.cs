
// Type: Intermech.Client.Core.Organizer.SchedulerHeaderRendererEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;


namespace Intermech.Client.Core.Organizer;

internal class SchedulerHeaderRendererEventArgs : EventArgs
{
  private SchedulerHeader _header;
  private Graphics _graphics;

  /// <summary>
  /// 
  /// </summary>
  internal Graphics Graphics => this._graphics;

  /// <summary>
  /// 
  /// </summary>
  internal SchedulerHeader Header => this._header;

  internal SchedulerHeaderRendererEventArgs(SchedulerHeader header, Graphics g)
  {
    this._header = header;
    this._graphics = g;
  }
}
