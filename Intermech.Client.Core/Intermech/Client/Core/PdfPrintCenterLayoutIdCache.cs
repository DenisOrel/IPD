
// Type: Intermech.Client.Core.PdfPrintCenterLayoutIdCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Client.Core;

/// <summary>
/// Класс для хранения GUID-ов, относящихся к макетам центра печати pdf,
/// а также для получения локальных идентификаторов макетов
/// </summary>
internal sealed class PdfPrintCenterLayoutIdCache
{
  public static readonly Guid LayoutGuid = new Guid("cadd9ac1-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid LayoutNameGuid = new Guid("cad00020-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid LayoutContentGuid = new Guid("cadd9ac3-306c-11d8-b4e9-00304f19f545");
  private IMetaDataHelper metaDataHelper;

  public PdfPrintCenterLayoutIdCache(IMetaDataHelper metaDataHelper)
  {
    this.metaDataHelper = metaDataHelper != null ? metaDataHelper : throw new ArgumentNullException(nameof (metaDataHelper));
    this.LayoutLocalId = metaDataHelper.GetObjectTypeID(PdfPrintCenterLayoutIdCache.LayoutGuid);
    this.LayoutNameLocalId = metaDataHelper.GetAttributeTypeID(PdfPrintCenterLayoutIdCache.LayoutNameGuid);
    this.LayoutContentLocalId = metaDataHelper.GetAttributeTypeID(PdfPrintCenterLayoutIdCache.LayoutContentGuid);
  }

  public int LayoutLocalId { get; }

  public int LayoutNameLocalId { get; }

  public int LayoutContentLocalId { get; }
}
