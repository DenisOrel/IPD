
// Type: Intermech.Bars.DropDownMenuItem
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    public class DropDownMenuItem : TopLevelMenuItemBase
    {
      private ImageList _menuImageList;

      public DropDownMenuItem() => this._menuImageList = (ImageList) null;

      protected internal override void ApplyLayout(
        Rectangle buttonBounds,
        Graphics graphics,
        bool vertical,
        bool rightToLeft)
      {
        base.ApplyLayout(buttonBounds, graphics, vertical, rightToLeft);
        Rectangle buttonInnerBounds = this.ButtonInnerBounds;
        buttonInnerBounds.Width -= 11;
        this.LayoutImageAndText(buttonInnerBounds, vertical, rightToLeft);
      }

      public override ToolbarItemBase CloneItem()
      {
        DropDownMenuItem dropDownMenuItem = (DropDownMenuItem) base.CloneItem();
        dropDownMenuItem.MenuImageList = this.MenuImageList;
        return (ToolbarItemBase) dropDownMenuItem;
      }

      public void DisposeChildren()
      {
        if (!this.HasChildren)
          return;
        MenuButtonItem[] array = new MenuButtonItem[this.Items.Count];
        this.Items.CopyTo((ToolbarItemBase[]) array, 0);
        this.Items.Clear();
        for (int index = 0; index < array.Length; ++index)
          array[index].Dispose();
      }

      [DefaultValue(typeof (ImageList), null)]
      [Description("If specified, any submenus of this item will use this imagelist instead of the one belonging to the parent toolbar.")]
      [Category("Appearance")]
      public ImageList MenuImageList
      {
        get => this._menuImageList;
        set => this._menuImageList = value;
      }

      [Browsable(true)]
      public override string ToolTipText
      {
        get => base.ToolTipText;
        set => base.ToolTipText = value;
      }
    }
}
