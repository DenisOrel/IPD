
// Type: SuperTooltips.ISuperTooltipInfoProvider
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Drawing;


namespace SuperTooltips
{
    public interface ISuperTooltipInfoProvider
    {
      event EventHandler DisplayTooltip;

      event EventHandler HideTooltip;

      Rectangle ComponentRectangle { get; }
    }
}
