
// Type: Intermech.Client.Core.FormDesigner.Controls.ImageFromLibrary
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Thumbnail;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.PropertyEditors;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
public static class ImageFromLibrary
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="imgFromLibrary"></param>
  /// <param name="bounds"></param>
  /// <param name="imageSize"></param>
  /// <param name="layout"></param>
  /// <returns></returns>
  public static Rectangle CalcBackgroundImageRectangle(
    this IImageFromLibrary imgFromLibrary,
    Rectangle bounds,
    Size imageSize,
    ImageLayout layout)
  {
    Rectangle rectangle = bounds;
    if (imageSize != Size.Empty)
    {
      switch (layout)
      {
        case ImageLayout.None:
          rectangle.Size = imageSize;
          break;
        case ImageLayout.Center:
          rectangle.Size = imageSize;
          Size size1 = bounds.Size;
          if (size1.Width > rectangle.Width)
            rectangle.X = (size1.Width - rectangle.Width) / 2;
          if (size1.Height > rectangle.Height)
          {
            rectangle.Y = (size1.Height - rectangle.Height) / 2;
            break;
          }
          break;
        case ImageLayout.Stretch:
          rectangle.Size = bounds.Size;
          break;
        case ImageLayout.Zoom:
          Size size2 = imageSize;
          float num1 = (float) bounds.Width / (float) size2.Width;
          float num2 = (float) bounds.Height / (float) size2.Height;
          if ((double) num1 >= (double) num2)
          {
            rectangle.Height = bounds.Height;
            rectangle.Width = (int) ((double) size2.Width * (double) num2 + 0.5);
            if (bounds.X >= 0)
            {
              rectangle.X = (bounds.Width - rectangle.Width) / 2;
              break;
            }
            break;
          }
          rectangle.Width = bounds.Width;
          rectangle.Height = (int) ((double) size2.Height * (double) num1 + 0.5);
          if (bounds.Y >= 0)
          {
            rectangle.Y = (bounds.Height - rectangle.Height) / 2;
            break;
          }
          break;
      }
    }
    return rectangle;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="attributeID"></param>
  /// <returns></returns>
  public static Image GetImageFromAttribute(
    this IImageFromLibrary imgFromLibrary,
    IElementInfo info,
    AttributeValues attrValues,
    ref PictureBoxImageData imgData,
    ref PictureBoxImageData blobCacheData)
  {
    Guid attributeGuid = attrValues.AttributeGuid;
    Image image = (Image) null;
    if (attrValues.Values[0] == null)
    {
      imgData = (PictureBoxImageData) null;
      return image;
    }
    MemoryStream aDestStream = (MemoryStream) null;
    if (attrValues.Values[0] is string)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute attributeByGuid = (info.ElementKind == AttributableElements.Object ? (IDBAttributable) sessionKeeper.Session.GetObject(info.ElementIdentifier) : (IDBAttributable) sessionKeeper.Session.GetRelation(info.ElementIdentifier)).GetAttributeByGuid(attributeGuid, false);
        if (attributeByGuid != null)
        {
          if (attributeByGuid is IBlobReader)
          {
            bool flag = false;
            if (blobCacheData != null)
            {
              BlobInformation blobInformation = (attributeByGuid as IBlobReader).OpenBlob(-1);
              if (blobCacheData.BlobID == blobInformation.BlobID && blobCacheData.FileDate.Equals(blobInformation.ModifyDate) && blobCacheData.FileName.Equals(blobInformation.FileName))
                flag = true;
            }
            if (!flag)
            {
              aDestStream = new MemoryStream();
              BlobProcReader blobProcReader = new BlobProcReader(attributeByGuid, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
              blobProcReader.ReadData(sessionKeeper.Session);
              imgData = new PictureBoxImageData((Stream) aDestStream, blobProcReader.BlobInformation.BlobID, blobProcReader.BlobInformation.FileName, blobProcReader.BlobInformation.ModifyDate);
              blobCacheData = imgData.Clone() as PictureBoxImageData;
            }
            else
            {
              imgData = blobCacheData.Clone() as PictureBoxImageData;
              aDestStream = new MemoryStream();
              aDestStream.Write(imgData.Buffer, 0, imgData.Buffer.Length);
            }
          }
        }
      }
    }
    else
    {
      aDestStream = new MemoryStream();
      aDestStream.Write(imgData.Buffer, 0, imgData.Buffer.Length);
    }
    if (aDestStream != null)
    {
      string ext = Path.GetExtension(imgData.FileName).Substring(1);
      object fromStream = new BitmapCreator().CreateFromStream((Stream) aDestStream, ext);
      switch (fromStream)
      {
        case Image _:
          image = (fromStream as Image).Clone() as Image;
          break;
        case Icon _:
          image = (Image) (fromStream as Icon).ToBitmap();
          break;
        case IThumbImage thumbImage:
          image = (Image) new Bitmap(thumbImage.Width, thumbImage.Height);
          using (Graphics g = Graphics.FromImage(image))
          {
            GraphicsUnit pageUnit = GraphicsUnit.Pixel;
            Rectangle rectangle = Rectangle.Round(image.GetBounds(ref pageUnit));
            thumbImage.PaintTo(g, rectangle, rectangle);
            break;
          }
      }
    }
    return image;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="imgFromLibrary"></param>
  /// <param name="guid"></param>
  /// <param name="id"></param>
  /// <param name="name"></param>
  /// <returns></returns>
  public static Image GetImageFromLibrary(
    this IImageFromLibrary imgFromLibrary,
    Guid guid,
    ref long id,
    ref string name)
  {
    Image image = (Image) null;
    IObjectsInfoCache service1 = ApplicationServices.Container.GetService<IObjectsInfoCache>();
    QuickObjectInfo objectInfo = service1.GetObjectInfo(guid);
    if ((guid.Equals(Guid.Empty) || !guid.Equals(Guid.Empty) && objectInfo.Empty) && id != 0L)
      objectInfo = service1.GetObjectInfo(id);
    if (!objectInfo.Empty)
    {
      id = objectInfo.ObjectID;
      name = objectInfo.Caption;
      if (ServicesManager.GetService(typeof (IPicturesCache)) is IPicturesCache service2)
      {
        object picture = service2.GetPicture(objectInfo.ObjectID);
        switch (picture)
        {
          case Image _:
            image = (picture as Image).Clone() as Image;
            break;
          case Icon _:
            image = (Image) (picture as Icon).ToBitmap();
            break;
          case IThumbImage thumbImage:
            image = (Image) new Bitmap(thumbImage.Width, thumbImage.Height);
            using (Graphics g = Graphics.FromImage(image))
            {
              GraphicsUnit pageUnit = GraphicsUnit.Pixel;
              Rectangle rectangle = Rectangle.Round(image.GetBounds(ref pageUnit));
              thumbImage.PaintTo(g, rectangle, rectangle);
              break;
            }
        }
      }
    }
    return image;
  }
}
