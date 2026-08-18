// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportHelper
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Localization.Xml;
using System;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Класс - помощник для работы с настройками</summary>
public sealed class XmlExchangeExportHelper
{
  /// <summary>Сохранение данных в объект</summary>
  /// <param name="configObjId">Ид. версии объекта с настройками</param>
  /// <param name="session"></param>
  /// <param name="settings"></param>
  public static bool SaveSettings(
    long configObjId,
    IUserSession session,
    XmlExchangeExportSettings settings)
  {
    if (configObjId == 0L)
      return false;
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (settings == null)
      throw new ArgumentNullException(nameof (settings));
    IDBObject dbObject = session.GetObject(configObjId, false);
    return dbObject != null && XmlExchangeExportHelper.SaveSettings(dbObject, settings);
  }

  /// <summary>Загрузка данных из объекта</summary>
  /// <param name="configObjId"></param>
  /// <param name="session"></param>
  /// <param name="settings"></param>
  /// <returns></returns>
  public static bool LoadSettings(
    long configObjId,
    IUserSession session,
    out XmlExchangeExportSettings settings)
  {
    settings = (XmlExchangeExportSettings) null;
    if (configObjId == 0L)
      return false;
    IDBObject dbObject = session != null ? session.GetObject(configObjId, false) : throw new ArgumentNullException(nameof (session));
    return dbObject != null && XmlExchangeExportHelper.LoadSettings(dbObject, out settings);
  }

  /// <summary>Сохранение данных в объект</summary>
  /// <param name="stream"></param>
  /// <param name="settings"></param>
  public static bool SaveSettings(IDBObject dbObject, XmlExchangeExportSettings settings)
  {
    if (dbObject == null)
      throw new ArgumentNullException(nameof (dbObject));
    if (settings == null)
      throw new ArgumentNullException(nameof (settings));
    if (!dbObject.isParentType(XmlExchangeConsts.Common.ExportSettObjTypeGuid) || dbObject.ReadOnly || !(dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(XmlExchangeConsts.Common.DataAttrTypeGuid), false) is IBlobWriter blobWriter))
      return false;
    using (MemoryStream inStream = new MemoryStream())
    {
      try
      {
        if (!XmlExchangeExportHelper.SaveSettings((Stream) inStream, settings))
          return false;
        inStream.Position = 0L;
        using (MemoryStream outStream = new MemoryStream())
        {
          ZLibStreamHelper.PackStream((Stream) inStream, ZLibCompressLevels.LevelMax, (Stream) outStream);
          byte[] array = outStream.ToArray();
          BlobInformation blobInfo = new BlobInformation(inStream.Length, outStream.Length, DateTime.Now, "ExportXmlSettings.xml", ArcMethods.ZLibPacked, string.Empty);
          blobWriter.OpenBlob(blobInfo, false);
          blobWriter.WriteDataBlock(array);
        }
      }
      finally
      {
        inStream.Close();
      }
    }
    return true;
  }

  /// <summary>Загрузка данных из объекта</summary>
  /// <param name="stream"></param>
  /// <param name="validateAndFix">Проверка и корректировка настроек при необходимости</param>
  /// <returns></returns>
  public static bool LoadSettings(
    IDBObject dbObject,
    out XmlExchangeExportSettings settings,
    bool validateAndFix = true)
  {
    if (dbObject == null)
      throw new ArgumentNullException(nameof (dbObject));
    settings = (XmlExchangeExportSettings) null;
    if (!dbObject.isParentType(XmlExchangeConsts.Common.ExportSettObjTypeGuid))
      return false;
    try
    {
      if (!(dbObject.GetAttributeByGuid(XmlExchangeConsts.Common.DataAttrTypeGuid, false) is IBlobReader attributeByGuid))
        return false;
      using (MemoryStream inStream = new MemoryStream())
      {
        BlobInformation blobInformation = attributeByGuid.OpenBlob(0);
        byte[] buffer1;
        try
        {
          if (blobInformation.RealFileSize == 0L)
            return false;
          buffer1 = attributeByGuid.ReadDataBlock(0);
        }
        finally
        {
          attributeByGuid.CloseBlob();
        }
        if (buffer1 != null)
        {
          inStream.Write(buffer1, 0, buffer1.Length);
          inStream.Position = 0L;
          if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
          {
            using (MemoryStream outStream = new MemoryStream())
            {
              ZLibStreamHelper.UnpackStream((Stream) inStream, (Stream) outStream);
              byte[] buffer2 = outStream.GetBuffer();
              inStream.Position = 0L;
              inStream.Write(buffer2, 0, buffer2.Length);
            }
          }
          inStream.Position = 0L;
          XmlExchangeExportHelper.LoadSettings((Stream) inStream, out settings);
        }
      }
    }
    catch (Exception ex)
    {
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces.XmlExchange_2"), (object) ex.Message), ex);
    }
    if (validateAndFix && settings != null)
      settings.ValidateData();
    return settings != null;
  }

  /// <summary>Сохранение данных в поток</summary>
  /// <param name="stream"></param>
  /// <param name="settings"></param>
  public static bool SaveSettings(Stream stream, XmlExchangeExportSettings settings)
  {
    if (stream == null)
      return true;
    XmlDocument xmlDoc = new XmlDocument();
    if (!settings.SaveData(xmlDoc))
      return false;
    xmlDoc.Save(stream);
    return true;
  }

  /// <summary>Загрузка данных из потока</summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static bool LoadSettings(Stream stream, out XmlExchangeExportSettings settings)
  {
    settings = (XmlExchangeExportSettings) null;
    if (stream == null)
      return false;
    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.Load(stream);
    settings = XmlExchangeExportSettings.LoadData(xmlDoc);
    return settings != null;
  }
}
