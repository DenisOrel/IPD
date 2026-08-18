
// Type: Intermech.Bars.n
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Drawing;


namespace Intermech.Bars
{
    internal abstract class n : IDisposable
    {
      private PopupMenu _popupMenu;

      public n(PopupMenu menu) => this._popupMenu = menu;

      protected internal abstract Rectangle ConstraintArea();

      protected internal virtual Rectangle ModifyParentBounds(Rectangle parentBounds) => parentBounds;

      protected internal virtual bool ShouldHighlightItem(MenuButtonItem item) => false;

      protected internal abstract void Show(ref int maximumMenuCount, MenuAnimation desiredAnimation);

      protected internal abstract bool AllowLowImportanceMenuItems();

      public virtual void Dispose()
      {
      }

      protected internal virtual void LowImportanceItemsExpanded()
      {
      }

      protected PopupMenu PopupMenu => this._popupMenu;
    }
}
