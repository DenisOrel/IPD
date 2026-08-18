
// Type: Intermech.NavBars.NavigationPane
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Bars;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.NavBars
{
    public class NavigationPane : Panel, INavigationPane
    {
      private Image _largeImage;
      private Image _smallImage;
      private bool _listed;
      internal Rectangle _d;
      internal bool _hided;

      [Description("Determines order in parent's list.")]
      [Category("Appearance")]
      public int Index
      {
        get
        {
          Control parent = this.Parent;
          return parent == null ? -1 : parent.Controls.Count - parent.Controls.IndexOf((Control) this) - 1;
        }
        set
        {
          if (value < 0)
            return;
          Control parent = this.Parent;
          if (parent == null)
            return;
          if (value > parent.Controls.Count)
            value = parent.Controls.Count;
          parent.Controls.SetChildIndex((Control) this, parent.Controls.Count - value - 1);
          parent.ResumeLayout();
        }
      }

      [DefaultValue(null)]
      [Description("A 24x24 pixel image representing the navigation pane.")]
      [Category("Appearance")]
      public Image LargeImage
      {
        get => this._largeImage;
        set
        {
          this._largeImage = Utils.MakeTransparent(value);
          this.CalcLayout();
        }
      }

      [DefaultValue(true)]
      [Description("Indicates whether the Navigation Pane is listed in the visual part of the Navigation Bar.")]
      [Category("Appearance")]
      public bool Listed
      {
        get => this._listed;
        set
        {
          this._listed = value;
          if (!this._listed && this.NavigationBar != null && this.NavigationBar.ShowPanes > this.NavigationBar.ListedCount())
            this.NavigationBar.ShowPanes = this.NavigationBar.ListedCount();
          this.CalcLayout();
        }
      }

      private NavigationBar NavigationBar => this.Parent as NavigationBar;

      [DefaultValue(null)]
      [Category("Appearance")]
      [Description("A 16x16 pixel image representing the navigation pane.")]
      public Image SmallImage
      {
        get => this._smallImage;
        set
        {
          this._smallImage = Utils.MakeTransparent(value);
          this.CalcLayout();
        }
      }

      [Browsable(true)]
      public override string Text
      {
        get => base.Text;
        set
        {
          base.Text = value;
          if (this.NavigationBar == null)
            return;
          if (this.NavigationBar.SelectedPane == this)
            this.NavigationBar.Invalidate();
          else
            this.NavigationBar.InvalidatePane(this);
        }
      }

      public NavigationPane()
      {
        this._largeImage = (Image) null;
        this._smallImage = (Image) null;
        this._listed = true;
        base.Text = "Navigation Pane";
      }

      protected override void OnEnabledChanged(EventArgs e)
      {
        base.OnEnabledChanged(e);
        if (this.NavigationBar == null)
          return;
        this.NavigationBar.Invalidate(this._d);
      }

      private void CalcLayout()
      {
        if (this.NavigationBar == null)
          return;
        this.NavigationBar.CalcLayout();
      }

      private bool ShouldSerializeIndex() => false;

      [SpecialName]
      bool INavigationPane.get_Enabled() => this.Enabled;

      [SpecialName]
      void INavigationPane.set_Enabled(bool value) => this.Enabled = value;
    }
}
