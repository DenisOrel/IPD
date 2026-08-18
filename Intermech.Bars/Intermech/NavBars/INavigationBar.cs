
// Type: Intermech.NavBars.INavigationBar
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;


namespace Intermech.NavBars
{
    public interface INavigationBar
    {
      event EventHandler SelectedPaneChanged;

      event EventHandler ShowNavigationPaneOptions;

      INavigationPane FindPane(string name);

      INavigationPane CreatePane(string name);

      IAppPane CeateAppPane(string name);

      INavigationPane[] Panes { get; }
    }
}
