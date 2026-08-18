
// Type: Intermech.ButtonsPanel.PanelButtonClickEventArgs
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;


namespace Intermech.ButtonsPanel
{
    public class PanelButtonClickEventArgs : EventArgs
    {
      private PanelButton _button;

      public PanelButtonClickEventArgs(PanelButton Button) => this._button = Button;

      public PanelButton Button => this._button;
    }
}
