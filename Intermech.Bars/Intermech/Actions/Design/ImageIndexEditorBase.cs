
// Type: Intermech.Actions.Design.ImageIndexEditorBase
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Actions.Design
{
    public abstract class ImageIndexEditorBase : UITypeEditor
    {
      public override bool GetPaintValueSupported(ITypeDescriptorContext context) => true;

      public override void PaintValue(PaintValueEventArgs pe)
      {
        if (!(pe.Value is int))
          return;
        Image image = this.GetImage(pe.Context, (int) pe.Value);
        if (image == null)
          return;
        pe.Graphics.DrawImage(image, pe.Bounds);
      }

      private Image GetImage(ITypeDescriptorContext context, int index)
      {
        ImageList imageList = this.GetImageList(context);
        return imageList == null || index < 0 || index >= imageList.Images.Count ? (Image) null : imageList.Images[index];
      }

      protected abstract ImageList GetImageList(ITypeDescriptorContext context);
    }
}
