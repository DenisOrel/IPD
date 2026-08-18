
// Type: Intermech.Files.ViewAreaPublishItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.Files;

internal sealed class ViewAreaPublishItem
{
  private readonly DBObjectState dbObject;
  private readonly ICollection<IViewAreaPublishAction> actions;

  public ViewAreaPublishItem(DBObjectState dbObject, ICollection<IViewAreaPublishAction> actions)
  {
    if (dbObject == null)
      throw new ArgumentNullException(nameof (dbObject));
    if (actions == null)
      throw new ArgumentNullException(nameof (actions));
    this.dbObject = dbObject;
    this.actions = actions;
  }

  public DBObjectState DBObject => this.dbObject;

  public ICollection<IViewAreaPublishAction> Actions => this.actions;
}
