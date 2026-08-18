
// Type: Intermech.PropertyEditors.ObjectMainAttributesGridTab
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Drawing;


namespace Intermech.PropertyEditors;

/// <summary>
/// Реализует закладку для редактора атрибутов объекта, отображающую
/// наиболее часто используемые атрибуты.
/// </summary>
public class ObjectMainAttributesGridTab : ObjectPropertyGridTab
{
  private static readonly Guid tabGuid = new Guid("AABD6C3D-F920-453e-B334-D1180DDDEBAC");
  private static readonly string tabName = LocalizationHolder.rm.GetString("Client.Core_974");
  private static Bitmap tabBitmap = (Bitmap) null;
  private static readonly GetAttributeValuesModes tabAttributeValuesModes = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGroupName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.CheckVisibility | GetAttributeValuesModes.IncludeCaption;

  public override GetAttributeValuesModes TabAttributeValuesModes
  {
    get => ObjectMainAttributesGridTab.tabAttributeValuesModes;
  }

  public override Guid TabGuid => ObjectMainAttributesGridTab.tabGuid;

  public override string TabName => ObjectMainAttributesGridTab.tabName;

  public override Bitmap Bitmap
  {
    get
    {
      if (ObjectMainAttributesGridTab.tabBitmap == null)
      {
        INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
        if (service != null)
          ObjectMainAttributesGridTab.tabBitmap = new Bitmap(service.ImageList.Images[service.ImageIndex("imgPrintPreview")]);
      }
      return ObjectMainAttributesGridTab.tabBitmap;
    }
  }
}
