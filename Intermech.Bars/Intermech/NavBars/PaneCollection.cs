
// Type: Intermech.NavBars.PaneCollection
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Windows.Forms;


namespace Intermech.NavBars
{
    internal class PaneCollection : Control.ControlCollection
    {
      private NavigationBar _bar;

      public PaneCollection(NavigationBar parent)
        : base((Control) parent)
      {
        this._bar = parent;
      }

      public override void Add(Control control)
      {
        if (!(control is NavigationPane))
          throw new ArgumentException("value");
        int num = this._bar.ShowPanes == this._bar.ListedCount() ? 1 : 0;
        this._bar.SuspendLayout();
        base.Add(control);
        if (num != 0)
          this._bar.ShowPanes = this._bar.ListedCount();
        if (this._bar.SelectedPane == null)
          this._bar.SelectedPane = (NavigationPane) control;
        this._bar.ResumeLayout();
      }

      public override void AddRange(Control[] control)
      {
        foreach (Control control1 in control)
        {
          if (!(control1 is NavigationPane))
            throw new ArgumentException("value");
        }
        base.AddRange(control);
      }

      public override void Remove(Control control)
      {
        if (!(control is NavigationPane))
          throw new ArgumentException("value");
        this._bar.SuspendLayout();
        base.Remove(control);
        if (this._bar.ShowPanes > this._bar.ListedCount())
          this._bar.ShowPanes = this._bar.ListedCount();
        if (this._bar.SelectedPane == control)
          this._bar.SelectedPane = this._bar.FirstPane();
        this._bar.ResumeLayout();
      }
    }
}
