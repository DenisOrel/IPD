
// Type: SuperTooltips.SuperTooltipDesigner
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;


namespace SuperTooltips
{
    [Serializable]
    public class SuperTooltipDesigner : ComponentDesigner
    {
      private void SetDefaults()
      {
        if (!(this.Component is SuperTooltip component))
          return;
        component.DefaultFont = new Font("Tahoma", Control.DefaultFont.Size);
      }

      public override void InitializeNewComponent(IDictionary defaultValues)
      {
        base.InitializeNewComponent(defaultValues);
        this.SetDefaults();
      }
    }
}
