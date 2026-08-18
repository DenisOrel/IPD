
// Type: Intermech.Search.CategoryTypeIcons.CategoryTypeIconsClientHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.Windows.Forms;


namespace Intermech.Search.CategoryTypeIcons;

public static class CategoryTypeIconsClientHelper
{
  public static Tuple<ImageList, int> GetImageListImageIndexTuple(object item)
  {
    ICategoryTypeIconService categoryTypeIconService = ServiceLocator.Get<ICategoryTypeIconService>();
    int type = -1;
    switch (item)
    {
      case _Object _:
        type = ((_Object) item).TypeID;
        break;
      case CompositionPart _:
        type = ((RelationObjectBase) item).Object.TypeID;
        break;
      case IObjectHolder _:
        type = ((IObjectHolder) item).Object.TypeID;
        break;
    }
    return new Tuple<ImageList, int>(categoryTypeIconService.ImageList, categoryTypeIconService.IndexOf(4, type));
  }
}
