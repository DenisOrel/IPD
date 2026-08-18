// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfGraphicsElement
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    public abstract class PdfGraphicsElement
    {
      public void Draw(PdfGraphics graphics)
      {
        if (graphics == null)
          throw new ArgumentNullException(nameof (graphics));
        this.Draw(graphics, PointF.Empty);
      }

      public void Draw(PdfGraphics graphics, PointF location)
      {
        if (graphics == null)
          throw new ArgumentNullException(nameof (graphics));
        this.Draw(graphics, location.X, location.Y);
      }

      public virtual void Draw(PdfGraphics graphics, float x, float y)
      {
        int num = (double) x != 0.0 ? 1 : ((double) y != 0.0 ? 1 : 0);
        PdfGraphicsState state = (PdfGraphicsState) null;
        if (num != 0)
        {
          state = graphics.Save();
          graphics.TranslateTransform(x, y);
        }
        this.DrawInternal(graphics);
        if (num == 0)
          return;
        graphics.Restore(state);
      }

      protected abstract void DrawInternal(PdfGraphics graphics);
    }
}
