
// Type: Intermech.ButtonsPanel.PanelButton
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.NavBars;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.ButtonsPanel
{
    [ToolboxItem(false)]
    [TypeConverter(typeof (PanelButtonConverter))]
    [DesignTimeVisible(false)]
    public class PanelButton : Component, IAppItem
    {
      private bool _enabled;
      internal bool _visible;
      private int _imageIndex;
      private Intermech.ButtonsPanel.ButtonsPanel _panel;
      private bool _checked;
      private object _tag;
      private string _text;
      internal string _toolTipText;
      internal Rectangle _imageBounds;
      internal Rectangle _outerBounds;
      internal Rectangle _selectionBounds;
      internal Rectangle _textBounds;

      public PanelButton()
      {
        this._text = "Button";
        this._imageIndex = -1;
        this._checked = false;
        this._enabled = true;
        this._visible = true;
        this._toolTipText = "";
      }

      internal int GetHeight(Graphics g)
      {
        SizeF layoutArea = new SizeF((float) this._panel.ClientRectangle.Width, 100f);
        if (this._panel.ImageList != null && this._panel.LayoutType == ButtonLayoutType.TextRight)
        {
          Size imageSize = this._panel.ImageList.ImageSize;
          layoutArea.Width -= (float) (imageSize.Width + 7);
        }
        int num = Convert.ToInt32(Math.Ceiling((double) g.MeasureString(this._text, this._panel.Font, layoutArea, this._panel._buttonTextFormat).Height));
        if (this._panel.LayoutType == ButtonLayoutType.TextBelow)
          return this._panel.ImageList == null ? num : num + 3 + this._panel.ImageList.ImageSize.Height + 4;
        if (this._panel.ImageList != null)
        {
          Size imageSize = this._panel.ImageList.ImageSize;
          if (imageSize.Height > num)
            num = imageSize.Height;
        }
        return num + 4;
      }

      internal void OnClick()
      {
        if (this.Click == null)
          return;
        this.Click((object) this, EventArgs.Empty);
      }

      [DefaultValue(true)]
      [Description("Indicates whether the button is enabled.")]
      public bool Enabled
      {
        get => this._enabled;
        set
        {
          this._enabled = value;
          if (this._panel == null)
            return;
          this._panel.Invalidate(this._outerBounds);
        }
      }

      [DefaultValue(true)]
      [Description("Determines whether the button is visible or hidden.")]
      public bool Visible
      {
        get => this._visible;
        set
        {
          this._visible = value;
          if (this._panel == null)
            return;
          this._panel.InvalidateLayout();
        }
      }

      private bool ShouldSerializeVisible() => !this._visible;

      [DefaultValue(-1)]
      [TypeConverter(typeof (ImageIndexConverter))]
      [Editor(typeof (ButtonImageEditor), typeof (UITypeEditor))]
      public int ImageIndex
      {
        get => this._imageIndex;
        set
        {
          this._imageIndex = value;
          if (this._panel == null)
            return;
          this._panel.InvalidateLayout();
        }
      }

      [Browsable(false)]
      public ImageList ImageList => this._panel != null ? this._panel.ImageList : (ImageList) null;

      internal Intermech.ButtonsPanel.ButtonsPanel Panel
      {
        get => this._panel;
        set => this._panel = value;
      }

      [Description("Indicates whether the button appears in a toggled state.")]
      [DefaultValue(false)]
      public bool Checked
      {
        get => this._checked;
        set
        {
          this._checked = value;
          if (this._panel == null)
            return;
          this._panel.InvalidateButton(this);
        }
      }

      [DefaultValue(null)]
      [TypeConverter(typeof (StringConverter))]
      public object Tag
      {
        get => this._tag;
        set => this._tag = value;
      }

      [Description("The text contained in the item.")]
      [Localizable(true)]
      [DefaultValue("Button")]
      public string Text
      {
        get => this._text;
        set
        {
          this._text = value;
          if (this._panel == null)
            return;
          this._panel.InvalidateLayout();
        }
      }

      [Description("Contains the text string that can appear when the user moves the mouse over the control.")]
      [Localizable(true)]
      [DefaultValue("")]
      public string ToolTipText
      {
        get => this._toolTipText;
        set => this._toolTipText = value;
      }

      public EventHandler GetClickEvent { get; }

      public event EventHandler Click;
    }
}
