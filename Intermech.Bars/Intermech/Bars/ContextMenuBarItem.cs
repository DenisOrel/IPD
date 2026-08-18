
// Type: Intermech.Bars.ContextMenuBarItem
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Bars
{
    [Designer(typeof (ContextMenuDesigner))]
    public class ContextMenuBarItem : MenuBarItem
    {
      private ImageList _menuImageList;

      public ContextMenuBarItem()
      {
        this.Text = "(context menu)";
        this.Visible = false;
        this._menuImageList = (ImageList) null;
      }

      [DefaultValue("(context menu)")]
      public override string Text
      {
        get => base.Text;
        set => base.Text = value;
      }

      [DefaultValue(false)]
      public override bool Visible
      {
        get => base.Visible;
        set => base.Visible = value;
      }

      [DefaultValue(typeof (ImageList), null)]
      [Description("If specified, any submenus of this item will use this imagelist instead of the one belonging to the parent toolbar.")]
      [Category("Appearance")]
      public ImageList MenuImageList
      {
        get => this._menuImageList;
        set => this._menuImageList = value;
      }
    }
}
