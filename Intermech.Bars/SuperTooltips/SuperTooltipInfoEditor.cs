
// Type: SuperTooltips.SuperTooltipInfoEditor
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Localization;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace SuperTooltips
{
    public class SuperTooltipInfoEditor : UITypeEditor
    {
      public override object EditValue(
        ITypeDescriptorContext context,
        System.IServiceProvider provider,
        object value)
      {
        if (context != null && context.Instance != null && provider != null)
        {
          IWindowsFormsEditorService service = provider.GetService(typeof (IWindowsFormsEditorService)) as IWindowsFormsEditorService;
          if (!(value is SuperTooltipInfo superTooltipInfo1))
          {
            superTooltipInfo1 = new SuperTooltipInfo();
            if (!(context.Instance is SuperTooltip) && SuperTooltip.DefaultSuperTooltipInfo != null)
            {
              superTooltipInfo1.BodyImage = SuperTooltip.DefaultSuperTooltipInfo.BodyImage;
              superTooltipInfo1.BodyText = SuperTooltip.DefaultSuperTooltipInfo.BodyText;
              superTooltipInfo1.Color = SuperTooltip.DefaultSuperTooltipInfo.Color;
              superTooltipInfo1.CustomSize = SuperTooltip.DefaultSuperTooltipInfo.CustomSize;
              superTooltipInfo1.FooterImage = SuperTooltip.DefaultSuperTooltipInfo.FooterImage;
              superTooltipInfo1.FooterText = SuperTooltip.DefaultSuperTooltipInfo.FooterText;
              superTooltipInfo1.FooterVisible = SuperTooltip.DefaultSuperTooltipInfo.FooterVisible;
              superTooltipInfo1.HeaderText = SuperTooltip.DefaultSuperTooltipInfo.HeaderText;
              superTooltipInfo1.HeaderVisible = SuperTooltip.DefaultSuperTooltipInfo.HeaderVisible;
            }
          }
          if (service == null)
            return value;
          SuperTooltipVisualEditor tooltipVisualEditor = new SuperTooltipVisualEditor();
          tooltipVisualEditor.EditorProvider = new CustomTypeEditorProvider(context.Container, provider);
          tooltipVisualEditor.EditorService = service;
          tooltipVisualEditor.SuperTooltipInfo = superTooltipInfo1;
          Form dialog = new Form();
          dialog.Controls.Add((Control) tooltipVisualEditor);
          dialog.Size = new Size(tooltipVisualEditor.Size.Width + SystemInformation.Border3DSize.Width * 4, tooltipVisualEditor.Size.Height + SystemInformation.Border3DSize.Height * 4 + SystemInformation.CaptionHeight);
          tooltipVisualEditor.Dock = DockStyle.Fill;
          dialog.StartPosition = FormStartPosition.CenterScreen;
          dialog.MinimizeBox = false;
          dialog.MaximizeBox = false;
          dialog.Text = LocalizationHolder.rm.GetString("Bars_16");
          int num = (int) service.ShowDialog(dialog);
          if (!tooltipVisualEditor.Canceled)
          {
            SuperTooltipInfo superTooltipInfo2 = tooltipVisualEditor.SuperTooltipInfo;
            dialog.Dispose();
            return (object) superTooltipInfo2;
          }
          dialog.Dispose();
        }
        return value;
      }

      public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
      {
        return context != null && context.Instance != null ? UITypeEditorEditStyle.Modal : base.GetEditStyle(context);
      }
    }
}
