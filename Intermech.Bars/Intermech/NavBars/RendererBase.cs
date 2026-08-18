
// Type: Intermech.NavBars.RendererBase
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Microsoft.Win32;
using System;


namespace Intermech.NavBars
{
    public abstract class RendererBase : IDisposable
    {
      private bool _customColors;

      public RendererBase()
      {
        this._customColors = false;
        SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(this.a);
      }

      private void a(object A_0, UserPreferenceChangedEventArgs A_1)
      {
        if (A_1.Category != UserPreferenceCategory.Color || this._customColors)
          return;
        this.OnSystemColorsChanged();
      }

      public void Dispose()
      {
        SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(this.a);
      }

      protected virtual void OnSystemColorsChanged()
      {
      }

      public bool CustomColors
      {
        get => this._customColors;
        set
        {
          this._customColors = value;
          if (this._customColors)
            return;
          this.OnSystemColorsChanged();
        }
      }
    }
}
