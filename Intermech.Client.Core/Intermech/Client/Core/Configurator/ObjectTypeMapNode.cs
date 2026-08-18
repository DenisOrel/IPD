
// Type: Intermech.Client.Core.Configurator.ObjectTypeMapNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Map;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace Intermech.Client.Core.Configurator;

/// <summary>для отображения типа объекта</summary>
public class ObjectTypeMapNode : MapIconicNode
{
  /// <summary>тип объекта</summary>
  private int objectTypeID = -1;
  /// <summary>иконка типа объекта</summary>
  private System.Drawing.Icon objectTypeIcon;
  /// <summary>имя типа объекта</summary>
  private string objectTypeName = string.Empty;

  /// <summary>имя типа объекта</summary>
  public string ObjectTypeName
  {
    get => this.objectTypeName;
    set => this.objectTypeName = value;
  }

  /// <summary>тип объекта</summary>
  public int ObjectTypeID
  {
    get => this.objectTypeID;
    set => this.objectTypeID = value;
  }

  /// <summary>иконка типа объекта</summary>
  public System.Drawing.Icon ObjectTypeIcon
  {
    get => this.objectTypeIcon;
    set => this.objectTypeIcon = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectTypeID"></param>
  public ObjectTypeMapNode(int objectTypeID)
  {
    this.objectTypeID = objectTypeID;
    this.objectTypeName = MetaDataHelper.GetObjectTypeName(objectTypeID);
    this.Initialize(string.Empty);
    this.SetImage();
  }

  private void SetImage()
  {
    if (Statics.IconSrv == null)
      return;
    MapImage mapImage = new MapImage();
    this.objectTypeIcon = Statics.IconSrv.GetIcon(4, this.objectTypeID);
    if (this.objectTypeIcon == null)
      return;
    using (Font font = new Font("Tahoma", 12f, FontStyle.Bold, GraphicsUnit.Pixel))
    {
      System.Drawing.Image bitmap1 = (System.Drawing.Image) this.objectTypeIcon.ToBitmap();
      int num1 = 2;
      int num2 = 2;
      using (Bitmap bitmap2 = new Bitmap(300, 50, PixelFormat.Format32bppPArgb))
      {
        using (Graphics graphics = Graphics.FromImage((System.Drawing.Image) bitmap2))
        {
          SizeF size = (SizeF) graphics.MeasureString(this.objectTypeName, font, 268 - num1 * 2).ToSize();
          int width1 = 300;
          int height1 = num2 + 32 /*0x20*/ + num2;
          using (Pen pen = new Pen(Color.DarkGreen, 1f))
            graphics.DrawRectangle(pen, new Rectangle(0, 0, width1 - 1, height1 - 1));
          using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, 0, width1, height1), Color.LightCyan, Color.LightSkyBlue, LinearGradientMode.Horizontal))
            graphics.FillRectangle((Brush) linearGradientBrush, new Rectangle(1, 1, width1 - 2, height1 - 2));
          int y1 = height1 > bitmap1.Height ? height1 / 2 - bitmap1.Height / 2 : num2;
          int height2 = height1 > bitmap1.Height ? bitmap1.Height : 32 /*0x20*/;
          int x = num1;
          int width2 = bitmap1.Width < 32 /*0x20*/ ? bitmap1.Width : 32 /*0x20*/;
          graphics.DrawImageUnscaledAndClipped(bitmap1, new Rectangle(x, y1, width2, height2));
          using (SolidBrush solidBrush = new SolidBrush(Color.Black))
          {
            float y2 = (float) (height1 / 2) - size.Height / 2f;
            RectangleF layoutRectangle = new RectangleF((float) (num1 + 16 /*0x10*/) + (float) (((double) width1 - (double) size.Width) / 2.0), y2, size.Width + 1f, size.Height);
            graphics.DrawString(this.objectTypeName, font, (Brush) solidBrush, layoutRectangle);
          }
          RectangleF rect = new RectangleF(0.0f, 0.0f, (float) width1, (float) height1);
          mapImage.Image = (System.Drawing.Image) bitmap2.Clone(rect, bitmap2.PixelFormat);
          mapImage.Selectable = false;
          mapImage.Resizable = false;
          this.Icon = (MapObject) mapImage;
        }
      }
    }
  }
}
