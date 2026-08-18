
// Type: Intermech.Search.Navigator.NavigatorColorSchemesFeature
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Search.Utilities;
using System;
using System.Drawing;


namespace Intermech.Search.Navigator;

public sealed class NavigatorColorSchemesFeature
{
  private LazyService<ICurrentUserAndRole> _currentUserAndRole = new LazyService<ICurrentUserAndRole>();
  private LazyService<INavGraphicsCache> _navGraphicsCache = new LazyService<INavGraphicsCache>();

  public NavGradientBrush GetNavGradientBrush(_Object @object, Rectangle rectangle)
  {
    if (@object == null)
      throw new ArgumentNullException(nameof (@object));
    NavGradientBrush navGradientBrush = (NavGradientBrush) null;
    if (!ObjectHelper.IsUnknownObjectVersionID(@object.CheckOutByVersionID))
      navGradientBrush = @object.CheckOutByVersionID != this._currentUserAndRole.Value.UserID ? this._navGraphicsCache.Value.GetNavGradientBrush(this._navGraphicsCache.Value.CurrentColorsScheme.CheckedOutOtherBkStartColor, this._navGraphicsCache.Value.CurrentColorsScheme.CheckedOutOtherBkEndColor, this._navGraphicsCache.Value.CurrentColorsScheme.CheckedOutOtherGradientMode, rectangle, this._navGraphicsCache.Value.CurrentColorsScheme.Gradient.HasFlag((Enum) GradientUsing.CheckedOutOther)) : this._navGraphicsCache.Value.GetNavGradientBrush(this._navGraphicsCache.Value.CurrentColorsScheme.CheckedOutBkStartColor, this._navGraphicsCache.Value.CurrentColorsScheme.CheckedOutBkEndColor, this._navGraphicsCache.Value.CurrentColorsScheme.CheckedOutGradientMode, rectangle, this._navGraphicsCache.Value.CurrentColorsScheme.Gradient.HasFlag((Enum) GradientUsing.CheckOut));
    return navGradientBrush;
  }
}
