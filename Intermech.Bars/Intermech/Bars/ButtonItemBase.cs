
// Type: Intermech.Bars.ButtonItemBase
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Bars
{
    [DefaultEvent("Click")]
    [DebuggerDisplay("[{CommandName}] {Text}")]
    public abstract class ButtonItemBase : ToolbarItemBase
    {
      private AutoToggleType _autoToggle;
      private int _imageIndex;
      private Icon _icon;
      private Image _image;
      private Size _iconSize;
      private bool _checked;
      internal Rectangle _imageBounds;
      internal Rectangle _textBounds;

      public event EventHandler Click;

      protected ButtonItemBase()
      {
        this._imageIndex = -1;
        this._icon = (Icon) null;
        this._image = (Image) null;
        this._iconSize = new Size(16 /*0x10*/, 16 /*0x10*/);
        this._checked = false;
        this._showText = false;
      }

      private void CalcBounds(Rectangle buttonBounds, ToolBarTextAlign textAlign, bool vertical)
      {
        if (textAlign == ToolBarTextAlign.Side && !vertical)
        {
          this._imageBounds.Offset(0, (buttonBounds.Height - this._imageBounds.Height) / 2);
          this._textBounds = new Rectangle(this._imageBounds.Right + 2, buttonBounds.Y, buttonBounds.Width - this._imageBounds.Width - 2, buttonBounds.Height);
        }
        else if (!vertical && textAlign == ToolBarTextAlign.Underneath || vertical && textAlign == ToolBarTextAlign.Side)
        {
          this._imageBounds.Offset((buttonBounds.Width - this._imageBounds.Width) / 2, 0);
          this._textBounds = new Rectangle(buttonBounds.X, this._imageBounds.Bottom + 2, buttonBounds.Width, buttonBounds.Height - this._imageBounds.Height - 2);
        }
        else
        {
          this._imageBounds.Offset(buttonBounds.Right - this._imageBounds.Right, 0);
          this._imageBounds.Offset(0, (buttonBounds.Height - this._imageBounds.Height) / 2);
          this._textBounds = this.ButtonInnerBounds;
          this._textBounds.Width -= this._imageBounds.Width + 2;
        }
      }

      protected internal override void ApplyLayout(
        Rectangle buttonBounds,
        Graphics graphics,
        bool vertical,
        bool rightToLeft)
      {
        base.ApplyLayout(buttonBounds, graphics, vertical, rightToLeft);
        this.LayoutImageAndText(this.ButtonInnerBounds, vertical, rightToLeft);
      }

      public override ToolbarItemBase CloneItem()
      {
        ButtonItemBase buttonItemBase = (ButtonItemBase) base.CloneItem();
        buttonItemBase.Click = this.Click;
        buttonItemBase.Checked = this.Checked;
        if (this.Icon != null)
          buttonItemBase.Icon = (Icon) this.Icon.Clone();
        buttonItemBase.IconSize = this.IconSize;
        if (this.Image != null)
          buttonItemBase.Image = (Image) this.Image.Clone();
        buttonItemBase.ImageIndex = this.ImageIndex;
        return (ToolbarItemBase) buttonItemBase;
      }

      protected override void Dispose(bool disposing) => base.Dispose(disposing);

      protected void LayoutImageAndText(Rectangle bounds, bool vertical, bool rightToLeft)
      {
        if (this.ToolBar == null)
          return;
        if (this.Icon != null)
        {
          this._imageBounds = new Rectangle(bounds.Location, this._iconSize);
          this.CalcBounds(bounds, this.ToolBar.TextAlign, vertical);
        }
        else if (this.Image != null)
        {
          this._imageBounds = new Rectangle(bounds.Location, this._image.Size);
          this.CalcBounds(bounds, this.ToolBar.TextAlign, vertical);
        }
        else if (this.ToolBar.ImageList != null && this._imageIndex >= 0 && this._imageIndex < this.ToolBar.ImageList.Images.Count)
        {
          this._imageBounds = new Rectangle(bounds.Location, this.ToolBar.ImageList.ImageSize);
          this.CalcBounds(bounds, this.ToolBar.TextAlign, vertical);
        }
        else
        {
          this._imageBounds = Rectangle.Empty;
          this._textBounds = bounds;
        }
        if (this.Text.Length != 0)
          return;
        this._textBounds = Rectangle.Empty;
      }

      public void PerformClick() => this.OnActivate();

      protected internal virtual void OnActivate()
      {
        this.CheckAutoToggle();
        if (this.ToolBar != null)
        {
          this.ToolBar.OnButtonClick(new ToolBarItemEventArgs((ToolbarItemBase) this));
        }
        else
        {
          for (MenuItemBase menuItemBase = this as MenuItemBase; menuItemBase != null; menuItemBase = menuItemBase.Parent)
          {
            if (menuItemBase.ToolBar != null)
            {
              menuItemBase.ToolBar.OnButtonClick(new ToolBarItemEventArgs((ToolbarItemBase) this));
              break;
            }
          }
        }
        if (this.Click == null)
          return;
        this.Click((object) this, EventArgs.Empty);
      }

      protected void CheckAutoToggle()
      {
        if (this.AutoToggle == AutoToggleType.Single)
        {
          this.Checked = !this.Checked;
        }
        else
        {
          if (this.AutoToggle != AutoToggleType.Radio)
            return;
          this.Checked = true;
        }
      }

      private void UnCheckRadioItems()
      {
        IButtonsSite buttonsSite = (IButtonsSite) this.ToolBar ?? this.Owner;
        if (buttonsSite == null)
          return;
        ArrayList arrayList = new ArrayList();
        int num = buttonsSite.Items.IndexOf((ToolbarItemBase) this);
        if (!this.BeginGroup)
        {
          for (int index = num - 1; index >= 0; --index)
          {
            arrayList.Add((object) buttonsSite.Items[index]);
            if (buttonsSite.Items[index].BeginGroup)
              break;
          }
        }
        for (int index = num + 1; index < buttonsSite.Items.Count && !buttonsSite.Items[index].BeginGroup; ++index)
          arrayList.Add((object) buttonsSite.Items[index]);
        foreach (ToolbarItemBase toolbarItemBase in arrayList)
        {
          if (toolbarItemBase is ButtonItemBase buttonItemBase)
            buttonItemBase.Checked = false;
        }
      }

      [DefaultValue(false)]
      [Description("Indicates the state of the item.")]
      [Category("Appearance")]
      public virtual bool Checked
      {
        get => this._checked;
        set
        {
          if (value == this._checked)
            return;
          this._checked = value;
          this.Invalidate();
          if (!value || this.AutoToggle != AutoToggleType.Radio)
            return;
          this.UnCheckRadioItems();
        }
      }

      [Category("Behavior")]
      [DefaultValue(typeof (AutoToggleType), "None")]
      [Description("Indicates how the button will automatically toggle itself and its neighbours.")]
      public AutoToggleType AutoToggle
      {
        get => this._autoToggle;
        set => this._autoToggle = value;
      }

      [Description("The icon to show in place of an image.")]
      [DefaultValue(typeof (Icon), null)]
      [Category("Image")]
      [AmbientValue(typeof (Icon), null)]
      public virtual Icon Icon
      {
        get => this._icon;
        set
        {
          this._icon = value;
          this.LayoutNeeded();
        }
      }

      [Category("Image")]
      [DefaultValue(typeof (Size), "16, 16")]
      [Description("The desired icon size to extract from the icon.")]
      public virtual Size IconSize
      {
        get => this._iconSize;
        set
        {
          this._iconSize = value;
          this.LayoutNeeded();
        }
      }

      [AmbientValue(typeof (Image), null)]
      [Description("The image assigned to the button.")]
      [Category("Image")]
      [DefaultValue(typeof (Image), null)]
      public virtual Image Image
      {
        get => this._image;
        set
        {
          this._image = value;
          this.LayoutNeeded();
        }
      }

      [DefaultValue(-1)]
      [Category("Image")]
      [TypeConverter(typeof (ImageIndexConverter))]
      [Description("Gets or sets the index value of the image assigned to the button.")]
      [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof (UITypeEditor))]
      public virtual int ImageIndex
      {
        get => this._imageIndex;
        set
        {
          this._imageIndex = value;
          this.LayoutNeeded();
        }
      }

      [Browsable(false)]
      public virtual ImageList ImageList
      {
        get => this.ToolBar != null ? this.ToolBar.ImageList : (ImageList) null;
      }

      [Browsable(false)]
      public virtual bool HasImage
      {
        get
        {
          if (this._icon != null || this._image != null)
            return true;
          return this.ToolBar != null && this.ToolBar.ImageList != null && this._imageIndex >= 0 && this._imageIndex < this.ToolBar.ImageList.Images.Count;
        }
      }

      [Browsable(true)]
      [DefaultValue(false)]
      [Category("Apperiance")]
      [Description("Show text on toolbar.")]
      public virtual bool ShowText
      {
        get => this._showText;
        set
        {
          this._showText = value;
          this.LayoutNeeded();
        }
      }
    }
}
