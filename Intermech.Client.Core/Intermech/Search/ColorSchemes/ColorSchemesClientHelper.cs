
// Type: Intermech.Search.ColorSchemes.ColorSchemesClientHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Search.Utilities;
using System;
using System.Drawing;


namespace Intermech.Search.ColorSchemes;

public static class ColorSchemesClientHelper
{
  public static NavGradientBrush GetNavGradientBrush(object item, Rectangle rectangle)
  {
    if (item == null)
      throw new ArgumentNullException(nameof (item));
    long versionID = 0;
    if (item is _Object)
      versionID = ((_Object) item).CheckOutByVersionID;
    else if (item is CompositionPart)
      versionID = ((RelationObjectBase) item).Object.CheckOutByVersionID;
    else if (item is IObjectHolder)
      versionID = ((IObjectHolder) item).Object.CheckOutByVersionID;
    NavGradientBrush navGradientBrush = (NavGradientBrush) null;
    if (!ObjectHelper.IsUnknownObjectVersionID(versionID))
    {
      ICurrentUserAndRole currentUserAndRole = ServiceLocator.Get<ICurrentUserAndRole>();
      INavGraphicsCache navGraphicsCache = ServiceLocator.Get<INavGraphicsCache>();
      navGradientBrush = versionID != currentUserAndRole.UserID ? navGraphicsCache.GetNavGradientBrush(navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkStartColor, navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkEndColor, navGraphicsCache.CurrentColorsScheme.CheckedOutOtherGradientMode, rectangle, navGraphicsCache.CurrentColorsScheme.Gradient.HasFlag((Enum) GradientUsing.CheckedOutOther)) : navGraphicsCache.GetNavGradientBrush(navGraphicsCache.CurrentColorsScheme.CheckedOutBkStartColor, navGraphicsCache.CurrentColorsScheme.CheckedOutBkEndColor, navGraphicsCache.CurrentColorsScheme.CheckedOutGradientMode, rectangle, navGraphicsCache.CurrentColorsScheme.Gradient.HasFlag((Enum) GradientUsing.CheckOut));
    }
    return navGradientBrush;
  }
}
