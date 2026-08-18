
// Type: Intermech.ButtonsPanel.ButtonImageEditor
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.ButtonsPanel
{
    internal class ButtonImageEditor : UITypeEditor
    {
      private ImageList _imageList;
      private UITypeEditor _imageEditor;

      public ButtonImageEditor()
      {
        this._imageEditor = (UITypeEditor) TypeDescriptor.GetEditor(typeof (Image), typeof (UITypeEditor));
      }

      public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
      {
        this._imageList = ((PanelButton) context.Instance).ImageList;
        return this._imageEditor.GetEditStyle(context);
      }

      public override bool GetPaintValueSupported(ITypeDescriptorContext context)
      {
        return this._imageEditor.GetPaintValueSupported(context);
      }

      public override void PaintValue(PaintValueEventArgs e)
      {
        if (this._imageList == null || this._imageList.Images.Count == 0 || !(this._imageEditor != null & e.Value != null))
          return;
        int index = (int) e.Value;
        if (!(index >= 0 & index <= this._imageList.Images.Count - 1))
          return;
        this._imageEditor.PaintValue(new PaintValueEventArgs(e.Context, (object) this._imageList.Images[index], e.Graphics, e.Bounds));
      }
    }
}
