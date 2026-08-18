
// Type: SuperTooltips.SuperTooltipInfo
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.ComponentModel;
using System.Drawing;


namespace SuperTooltips
{
    [TypeConverter(typeof (SuperTooltipInfoConverter))]
    [DesignTimeVisible(false)]
    [ToolboxItem(false)]
    public class SuperTooltipInfo
    {
      private bool _headerVisible;
      private bool _footerVisible;
      private string _headerText;
      private string _footerText;
      private Image _footerImage;
      private string _bodyText;
      private Image _bodyImage;
      private Size _customSize;
      private TooltipColorScheme _colorScheme;

      public SuperTooltipInfo()
      {
        this._headerVisible = true;
        this._footerVisible = true;
        this._headerText = "";
        this._footerText = "";
        this._bodyText = "";
        this._customSize = Size.Empty;
        this._colorScheme = TooltipColorScheme.Gray;
      }

      public SuperTooltipInfo(
        string headerText,
        string footerText,
        string bodyText,
        Image bodyImage,
        Image footerImage,
        TooltipColorScheme color)
      {
        this._headerVisible = true;
        this._footerVisible = true;
        this._customSize = Size.Empty;
        this._headerText = headerText;
        this._footerText = footerText;
        this._bodyText = bodyText;
        this._bodyImage = bodyImage;
        this._footerImage = footerImage;
        this._colorScheme = color;
      }

      public SuperTooltipInfo(
        string headerText,
        string footerText,
        string bodyText,
        Image bodyImage,
        Image footerImage,
        TooltipColorScheme color,
        bool headerVisible,
        bool footerVisible,
        Size customSize)
      {
        this._headerText = headerText;
        this._footerText = footerText;
        this._bodyText = bodyText;
        this._bodyImage = bodyImage;
        this._footerImage = footerImage;
        this._headerVisible = headerVisible;
        this._footerVisible = footerVisible;
        this._customSize = customSize;
        this._colorScheme = color;
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      public bool ShouldSerializeCustomSize() => !this._customSize.IsEmpty;

      [Description("Indicates body image displayed to the left of body text.")]
      [Browsable(true)]
      [DefaultValue(null)]
      public Image BodyImage
      {
        get => this._bodyImage;
        set => this._bodyImage = value;
      }

      [DefaultValue("")]
      [Browsable(true)]
      [Description("Indicates body text.")]
      public string BodyText
      {
        get => this._bodyText;
        set => this._bodyText = value;
      }

      [Browsable(true)]
      [Description("Indicates predefined tooltip color.")]
      [DefaultValue(17)]
      public TooltipColorScheme Color
      {
        get => this._colorScheme;
        set => this._colorScheme = value;
      }

      [Description("Indicates custom size for tooltip.")]
      [Browsable(true)]
      public Size CustomSize
      {
        get => this._customSize;
        set => this._customSize = value;
      }

      [DefaultValue(null)]
      [Browsable(true)]
      [Description("Indicates footer image displayed to the left of footer text.")]
      public Image FooterImage
      {
        get => this._footerImage;
        set => this._footerImage = value;
      }

      [Browsable(true)]
      [Description("Indicates footer text.")]
      [DefaultValue("")]
      public string FooterText
      {
        get => this._footerText;
        set => this._footerText = value;
      }

      [DefaultValue(true)]
      [Description("Indicates whether footer text is visible.")]
      [Browsable(true)]
      public bool FooterVisible
      {
        get => this._footerVisible;
        set => this._footerVisible = value;
      }

      [Description("Indicates header text.")]
      [DefaultValue("")]
      [Browsable(true)]
      public string HeaderText
      {
        get => this._headerText;
        set => this._headerText = value;
      }

      [Description("Indicates whether header text is visible.")]
      [Browsable(true)]
      [DefaultValue(true)]
      public bool HeaderVisible
      {
        get => this._headerVisible;
        set => this._headerVisible = value;
      }
    }
}
