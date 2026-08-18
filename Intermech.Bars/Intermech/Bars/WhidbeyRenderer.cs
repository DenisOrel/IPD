
// Type: Intermech.Bars.WhidbeyRenderer
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Drawing;


namespace Intermech.Bars
{
    public class WhidbeyRenderer : Office2003Renderer
    {
      public override string ToString() => "Whidbey";

      internal override void ApplyLunaBlueColors()
      {
        base.ApplyLunaBlueColors();
        this.InternalBlue();
      }

      internal override void ApplyLunaOliveColors()
      {
        base.ApplyLunaOliveColors();
        this.InternalOlive();
      }

      internal override void ApplyLunaSilverColors()
      {
        base.ApplyLunaSilverColors();
        this.InternalSilver();
      }

      internal override void ApplyStandardColors() => base.ApplyStandardColors();

      private void InternalBlue()
      {
        this._backgroundGradientColor1 = Color.FromArgb(191, 219, (int) byte.MaxValue);
        this._backgroundGradientColor2 = Color.FromArgb(111, 157, 217);
        this._toolBarGradientColor1 = Color.FromArgb(227, 239, (int) byte.MaxValue);
        this._toolBarGradientColor2 = Color.FromArgb(177, 211, (int) byte.MaxValue);
        this._grabHandleColor = Color.FromArgb(111, 157, 217);
        this._actionsButtonColor1 = Color.FromArgb(215, 232, (int) byte.MaxValue);
        this._actionsButtonColor2 = Color.FromArgb(111, 157, 217);
        this._borderColor = Color.FromArgb(111, 157, 217);
        this._formCaptionBackColor = Color.FromArgb(55, 100, 160 /*0xA0*/);
        this._formCaptionForeColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        this._toolBarSeparatorColor = Color.FromArgb(154, 198, (int) byte.MaxValue);
        this._containerBarBorderColor = Color.Transparent;
        this._containerBarBackgroundColor1 = Color.FromArgb(191, 219, (int) byte.MaxValue);
        this._containerBarBackgroundColor2 = Color.FromArgb(191, 219, (int) byte.MaxValue);
        this._containerBarToolBarBackgroundColor = Color.FromArgb(184, 207, 233);
        this._selectedTextColor = SystemColors.ControlLightLight;
        this._highlightBorderColor = Color.FromArgb((int) byte.MaxValue, 189, 105);
      }

      private void InternalOlive()
      {
        this._backgroundGradientColor1 = Color.FromArgb(231, 231, 214);
        this._backgroundGradientColor2 = Color.FromArgb((int) byte.MaxValue, 251, 247);
        this._toolBarGradientColor1 = Color.FromArgb(253, 253, 251);
        this._toolBarGradientColor2 = Color.FromArgb(181, 182, 156);
        this._grabHandleColor = Color.FromArgb(198, 190, 181);
        this._actionsButtonColor1 = Color.FromArgb(239, 239, 239);
        this._actionsButtonColor2 = Color.FromArgb(156, 154, 123);
        this._borderColor = Color.FromArgb(148, 146, 115);
        this._formCaptionBackColor = SystemColors.ControlDark;
        this._formCaptionForeColor = SystemColors.Window;
        this._toolBarSeparatorColor = Color.FromArgb(198, 195, 189);
        this._containerBarBorderColor = Color.Transparent;
        this._containerBarBackgroundColor1 = this._backgroundGradientColor2;
        this._containerBarBackgroundColor2 = this._backgroundGradientColor1;
        this._containerBarToolBarBackgroundColor = this._toolBarGradientColor2;
        this._selectedTextColor = SystemColors.ControlText;
        this._highlightBorderColor = Color.FromArgb(49, 105, 198);
        this._highlightButtonBackgroundColor1 = Color.FromArgb(198, 211, 239);
        this._highlightButtonBackgroundColor2 = this._highlightButtonBackgroundColor1;
        this._selectedButtonBackgroundColor1 = Color.FromArgb(156, 182, 231);
        this._selectedButtonBackgroundColor2 = this._selectedButtonBackgroundColor1;
        this._buttonBackgroundColor1 = Color.FromArgb(231, 231, 239);
        this._buttonBackgroundColor2 = this._buttonBackgroundColor1;
        this._highlightMenuItemBorderColor = this._highlightButtonBackgroundColor1;
      }

      private void InternalSilver()
      {
        this._backgroundGradientColor1 = Color.FromArgb(214, 215, 231);
        this._backgroundGradientColor2 = Color.FromArgb(247, 243, 247);
        this._toolBarGradientColor1 = Color.FromArgb(247, 247, (int) byte.MaxValue);
        this._toolBarGradientColor2 = Color.FromArgb(156, 150, 181);
        this._grabHandleColor = Color.FromArgb(82, 85, 115);
        this._actionsButtonColor1 = Color.FromArgb(181, 178, 206);
        this._actionsButtonColor2 = Color.FromArgb(115, 117, 148);
        this._borderColor = Color.FromArgb(123, 125, 148);
        this._formCaptionBackColor = SystemColors.ControlDark;
        this._formCaptionForeColor = SystemColors.Window;
        this._toolBarSeparatorColor = Color.FromArgb(107, 109, 140);
        this._containerBarBorderColor = Color.Transparent;
        this._containerBarBackgroundColor1 = this._backgroundGradientColor2;
        this._containerBarBackgroundColor2 = this._backgroundGradientColor1;
        this._containerBarToolBarBackgroundColor = this._toolBarGradientColor2;
        this._selectedTextColor = SystemColors.ControlText;
        this._highlightBorderColor = Color.FromArgb(74, 73, 107);
      }
    }
}
