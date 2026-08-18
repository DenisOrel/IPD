
// Type: Intermech.Bars.CollectToolbarsEventArgs
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;


namespace Intermech.Bars
{
    public class CollectToolbarsEventArgs : EventArgs
    {
      private ArrayList _toolbars;

      public CollectToolbarsEventArgs(ArrayList toolbars) => this._toolbars = toolbars;

      public ArrayList Toolbars => this._toolbars;
    }
}
