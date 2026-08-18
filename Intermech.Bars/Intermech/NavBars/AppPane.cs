
// Type: Intermech.NavBars.AppPane
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Bars;
using Intermech.ButtonsPanel;
using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.NavBars
{
    public class AppPane : NavigationPane, IAppPane, INavigationPane
    {
      private Intermech.ButtonsPanel.ButtonsPanel _buttonsPanel;

      public AppPane()
      {
        this._buttonsPanel = new Intermech.ButtonsPanel.ButtonsPanel();
        this._buttonsPanel.ImageList = new ImageList();
        this._buttonsPanel.ImageList.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
        this._buttonsPanel.ImageList.ColorDepth = ColorDepth.Depth24Bit;
        this._buttonsPanel.Dock = DockStyle.Fill;
        this._buttonsPanel.ButtonSpacing = 4;
        this.Controls.Add((Control) this._buttonsPanel);
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
          this._buttonsPanel.Dispose();
        base.Dispose(disposing);
      }

      public IAppItem Add(string text, EventHandler clickHandler, Icon icon)
      {
        PanelButton button = new PanelButton();
        button.Text = text;
        if (clickHandler != null)
          button.Click += clickHandler;
        if (icon != null)
        {
          this._buttonsPanel.ImageList.Images.Add(icon);
          button.ImageIndex = this._buttonsPanel.ImageList.Images.Count - 1;
        }
        this._buttonsPanel.Buttons.Add(button);
        return (IAppItem) button;
      }

      IAppItem IAppPane.Add(string text, EventHandler clickHandler, Image image)
      {
        PanelButton button = new PanelButton();
        button.Text = text;
        if (clickHandler != null)
          button.Click += clickHandler;
        if (image != null && image.Width > 0 && image.Height > 0)
        {
          this._buttonsPanel.ImageList.Images.Add(Utils.MakeTransparent(image));
          button.ImageIndex = this._buttonsPanel.ImageList.Images.Count - 1;
        }
        this._buttonsPanel.Buttons.Add(button);
        return (IAppItem) button;
      }

      IAppItem IAppPane.Add(string text, EventHandler clickHandler, int imageIndex)
      {
        PanelButton button = new PanelButton();
        button.Text = text;
        if (clickHandler != null)
          button.Click += clickHandler;
        button.ImageIndex = imageIndex;
        this._buttonsPanel.Buttons.Add(button);
        return (IAppItem) button;
      }

      public IAppItem[] GetItems()
      {
        ArrayList arrayList = new ArrayList();
        arrayList.AddRange((ICollection) this._buttonsPanel.Buttons);
        return (IAppItem[]) arrayList.ToArray(typeof (IAppItem));
      }

      public Intermech.ButtonsPanel.ButtonsPanel ButtonsPanel => this._buttonsPanel;

      [SpecialName]
      bool INavigationPane.get_Enabled() => this.Enabled;

      [SpecialName]
      void INavigationPane.set_Enabled(bool value) => this.Enabled = value;
    }
}
