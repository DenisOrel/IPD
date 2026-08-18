
// Type: Intermech.Files.ReusableSubArea
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.Files;

internal sealed class ReusableSubArea
{
  private readonly SubArea subArea;
  private readonly List<ViewAreaPublishItem> missingItems;

  public ReusableSubArea(SubArea subArea, List<ViewAreaPublishItem> missingItems)
  {
    if (subArea == null)
      throw new ArgumentNullException();
    if (missingItems == null)
      throw new ArgumentNullException();
    this.subArea = subArea;
    this.missingItems = missingItems;
  }

  public SubArea SubArea => this.subArea;

  public List<ViewAreaPublishItem> MissingItems => this.missingItems;
}
