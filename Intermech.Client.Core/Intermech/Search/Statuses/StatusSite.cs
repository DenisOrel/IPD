
// Type: Intermech.Search.Statuses.StatusSite
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;


namespace Intermech.Search.Statuses;

public class StatusSite
{
  public StatusSite(Status status, Rectangle rectangle)
  {
    if (status == null)
      throw new ArgumentNullException(nameof (status));
    if (rectangle == Rectangle.Empty)
      throw new ArgumentException();
    this.Status = status;
    this.Rectangle = rectangle;
  }

  public Status Status { get; private set; }

  public Rectangle Rectangle { get; private set; }
}
