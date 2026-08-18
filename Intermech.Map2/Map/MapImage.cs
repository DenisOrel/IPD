// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapImage
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public class MapImage : MapObject
    {
      public const int ChangedAlignment = 1604;
      public const int ChangedAutoResizes = 1605;
      public const int ChangedImage = 1601;
      public const int ChangedImageList = 1606;
      public const int ChangedIndex = 1607;
      public const int ChangedName = 1603;
      public const int ChangedResourceManager = 1602;
      private const int flagAutoResizes = 1048576 /*0x100000*/;
      private int myAlignment;
      private static ImageList myDefaultImageList = (ImageList) null;
      private static ResourceManager myDefaultResourceManager = (ResourceManager) null;
      [NonSerialized]
      private Image myImage;
      [NonSerialized]
      private ImageList myImageList;
      private int myIndex;
      private string myName;
      [NonSerialized]
      private ResourceManager myResourceManager;

      public MapImage()
      {
        this.myAlignment = 2;
        this.myResourceManager = MapImage.DefaultResourceManager;
        this.myName = (string) null;
        this.myImageList = MapImage.DefaultImageList;
        this.myIndex = -1;
        this.myImage = (Image) null;
        this.InternalFlags &= -33;
        this.InternalFlags |= 1048576 /*0x100000*/;
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1601:
            this.Image = (Image) e.GetValue(undo);
            break;
          case 1602:
            this.ResourceManager = (ResourceManager) e.GetValue(undo);
            break;
          case 1603:
            this.Name = (string) e.GetValue(undo);
            break;
          case 1604:
            this.Alignment = e.GetInt(undo);
            break;
          case 1605:
            this.AutoResizes = (bool) e.GetValue(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      private Image ConvertIconToImage(Icon icon) => (Image) icon.ToBitmap();

      public override RectangleF ExpandPaintBounds(RectangleF rect, MapView view)
      {
        if (this.Shadowed)
        {
          SizeF shadowOffset = this.GetShadowOffset(view);
          if ((double) shadowOffset.Width < 0.0)
          {
            rect.X += shadowOffset.Width;
            rect.Width -= shadowOffset.Width;
          }
          else
            rect.Width += shadowOffset.Width;
          if ((double) shadowOffset.Height < 0.0)
          {
            rect.Y += shadowOffset.Height;
            rect.Height -= shadowOffset.Height;
            return rect;
          }
          rect.Height += shadowOffset.Height;
        }
        return rect;
      }

      private void GetImage()
      {
        if (this.myImage != null)
          return;
        this.myImage = this.LoadImage();
        if (this.myImage == null || (double) this.Width != 0.0 && (double) this.Height != 0.0)
          return;
        this.UpdateSize();
      }

      public virtual Image LoadImage()
      {
        int index = this.Index;
        if (index >= 0)
        {
          ImageList imageList = this.ImageList;
          if (imageList != null && index < imageList.Images.Count)
            return imageList.Images[index];
          if (MapImage.DefaultImageList != null && index < MapImage.DefaultImageList.Images.Count)
            return MapImage.DefaultImageList.Images[index];
        }
        string name = this.Name;
        if (name == null)
          return (Image) null;
        pattern_2 = (Image) null;
        if (this.ResourceManager != null)
        {
          try
          {
            switch (this.ResourceManager.GetObject(name, CultureInfo.CurrentCulture))
            {
              case Icon icon:
                pattern_2 = this.ConvertIconToImage(icon);
                break;
            }
          }
          catch (MissingManifestResourceException ex)
          {
          }
        }
        if (pattern_2 == null)
        {
          if (MapImage.DefaultResourceManager != null)
          {
            try
            {
              switch (MapImage.DefaultResourceManager.GetObject(name, CultureInfo.CurrentCulture))
              {
                case Icon icon:
                  pattern_2 = this.ConvertIconToImage(icon);
                  break;
              }
            }
            catch (MissingManifestResourceException ex)
            {
            }
          }
          if (pattern_2 == null)
          {
            try
            {
              pattern_2 = Image.FromFile(name);
            }
            catch (OutOfMemoryException ex)
            {
              MapObject.Trace("LoadImage: " + ex.ToString());
            }
            catch (IOException ex)
            {
              MapObject.Trace("LoadImage: " + ex.ToString());
            }
            catch (ArgumentException ex)
            {
              MapObject.Trace("LoadImage: " + ex.ToString());
            }
          }
        }
        return pattern_2;
      }

      public override void Paint(Graphics g, MapView view)
      {
        RectangleF bounds = this.Bounds;
        Image image = this.Image;
        int index = this.Index;
        if (image == null && index >= 0)
        {
          ImageList imageList = view.ImageList;
          if (imageList != null && index < imageList.Images.Count)
            image = imageList.Images[index];
        }
        if (image == null)
          return;
        if (this.Shadowed)
        {
          SizeF shadowOffset = this.GetShadowOffset(view);
          ColorMatrix newColorMatrix = new ColorMatrix();
          newColorMatrix.Matrix00 = 0.0f;
          newColorMatrix.Matrix11 = 0.0f;
          newColorMatrix.Matrix22 = 0.0f;
          if (this.GetShadowBrush(view) is SolidBrush shadowBrush)
          {
            Color color = shadowBrush.Color;
            newColorMatrix.Matrix30 = (float) color.R / (float) byte.MaxValue;
            newColorMatrix.Matrix31 = (float) color.G / (float) byte.MaxValue;
            newColorMatrix.Matrix32 = (float) color.B / (float) byte.MaxValue;
            newColorMatrix.Matrix33 = (float) color.A / (float) byte.MaxValue;
          }
          else
          {
            newColorMatrix.Matrix30 = 0.5f;
            newColorMatrix.Matrix31 = 0.5f;
            newColorMatrix.Matrix32 = 0.5f;
            newColorMatrix.Matrix33 = 0.5f;
          }
          ImageAttributes imageAttr = new ImageAttributes();
          imageAttr.SetColorMatrix(newColorMatrix);
          g.DrawImage(image, new Rectangle((int) ((double) bounds.X + (double) shadowOffset.Width), (int) ((double) bounds.Y + (double) shadowOffset.Height), (int) bounds.Width, (int) bounds.Height), 0, 0, image.Size.Width, image.Size.Height, GraphicsUnit.Pixel, imageAttr);
        }
        bounds = this.Bounds;
        g.DrawImage(image, bounds);
      }

      private void ResetImage() => this.myImage = (Image) null;

      public override void SetSizeKeepingLocation(SizeF s)
      {
        this.Bounds = this.SetRectangleSpotLocation(this.Bounds with
        {
          Width = s.Width,
          Height = s.Height
        }, this.Alignment, this.Location);
      }

      private void UpdateSize()
      {
        if (!this.AutoResizes)
          return;
        Image image = this.Image;
        if (image == null)
          return;
        SizeF size = this.Size;
        SizeF s = new SizeF((float) image.Size.Width, (float) image.Size.Height);
        SizeF sizeF = s;
        if (!(size != sizeF))
          return;
        this.SetSizeKeepingLocation(s);
      }

      [Description("The image alignment")]
      [Category("Appearance")]
      [DefaultValue(2)]
      public virtual int Alignment
      {
        get => this.myAlignment;
        set
        {
          int alignment = this.myAlignment;
          if (alignment == value)
            return;
          this.myAlignment = value;
          this.Changed(1604, alignment, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
        }
      }

      [Description("Whether the bounds are recalculated when the image changes.")]
      [DefaultValue(true)]
      [Category("Behavior")]
      public virtual bool AutoResizes
      {
        get => (this.InternalFlags & 1048576 /*0x100000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 1048576 /*0x100000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 1048576 /*0x100000*/;
          else
            this.InternalFlags &= -1048577;
          this.Changed(1605, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("The initial ImageList for newly constructed MapImage objects.")]
      public static ImageList DefaultImageList
      {
        get => MapImage.myDefaultImageList;
        set => MapImage.myDefaultImageList = value;
      }

      [Description("The initial ResourceManager for newly constructed MapImage objects.")]
      public static ResourceManager DefaultResourceManager
      {
        get => MapImage.myDefaultResourceManager;
        set => MapImage.myDefaultResourceManager = value;
      }

      [Description("The Image displayed by this MapImage.")]
      [Category("Appearance")]
      public virtual Image Image
      {
        get
        {
          this.GetImage();
          return this.myImage;
        }
        set
        {
          this.GetImage();
          Image image = this.myImage;
          if (image == value)
            return;
          this.myImage = value;
          this.Changed(1601, 0, (object) image, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.UpdateSize();
        }
      }

      [DefaultValue(null)]
      [Category("Appearance")]
      [Description("The ImageList used to hold a collection of images, selected by Index.")]
      public virtual ImageList ImageList
      {
        get => this.myImageList;
        set
        {
          ImageList imageList = this.myImageList;
          if (imageList == value)
            return;
          this.myImageList = value;
          this.ResetImage();
          this.Changed(1606, 0, (object) imageList, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.UpdateSize();
        }
      }

      [Description("The index of the image in an ImageList.")]
      [Category("Appearance")]
      [DefaultValue(-1)]
      public virtual int Index
      {
        get => this.myIndex;
        set
        {
          int index = this.myIndex;
          if (index == value)
            return;
          this.myIndex = value;
          this.ResetImage();
          this.Changed(1607, index, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
          this.UpdateSize();
        }
      }

      public override PointF Location
      {
        get => this.GetSpotLocation(this.Alignment);
        set => this.SetSpotLocation(this.Alignment, value);
      }

      [Description("The Resource name or filename for loading images.")]
      [DefaultValue(null)]
      [Category("Appearance")]
      public virtual string Name
      {
        get => this.myName;
        set
        {
          string name = this.myName;
          if (!(name != value))
            return;
          this.myName = value;
          this.ResetImage();
          this.Changed(1603, 0, (object) name, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.UpdateSize();
        }
      }

      [Category("Appearance")]
      [DefaultValue(null)]
      [Description("The ResourceManager used to look up and load images by Name.")]
      public virtual ResourceManager ResourceManager
      {
        get => this.myResourceManager;
        set
        {
          ResourceManager resourceManager = this.myResourceManager;
          if (resourceManager == value)
            return;
          this.myResourceManager = value;
          this.ResetImage();
          this.Changed(1602, 0, (object) resourceManager, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.UpdateSize();
        }
      }
    }
}
