
// Type: Intermech.ButtonsPanel.PanelButtonCollection
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Collections;


namespace Intermech.ButtonsPanel
{
    public class PanelButtonCollection : CollectionBase
    {
      private Intermech.ButtonsPanel.ButtonsPanel _panel;

      internal PanelButtonCollection(Intermech.ButtonsPanel.ButtonsPanel panel) => this._panel = panel;

      public int Add(PanelButton button)
      {
        int num = this.List.Add((object) button);
        button.Panel = this._panel;
        if (this._panel == null)
          return num;
        this._panel.InvalidateLayout();
        return num;
      }

      public new void Clear()
      {
        base.Clear();
        if (this._panel == null)
          return;
        this._panel.InvalidateLayout();
      }

      public bool Contains(PanelButton button) => this.List.Contains((object) button);

      public int IndexOf(PanelButton button) => this.List.IndexOf((object) button);

      public void Insert(int Index, PanelButton button)
      {
        this.List.Insert(Index, (object) button);
        button.Panel = this._panel;
        if (this._panel == null)
          return;
        this._panel.InvalidateLayout();
      }

      public void Remove(PanelButton button)
      {
        if (!this.List.Contains((object) button))
          return;
        this.List.Remove((object) button);
        button.Panel = (Intermech.ButtonsPanel.ButtonsPanel) null;
        if (this._panel == null)
          return;
        this._panel.InvalidateLayout();
      }

      public PanelButton this[int Index] => (PanelButton) this.List[Index];
    }
}
