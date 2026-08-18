// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.CondHelper
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>Класс хелпер для работы с условиями / формулами</summary>
public static class CondHelper
{
  /// <summary>Загрузка условия у заданного объекта</summary>
  /// <param name="ius">Пользовательская сессия</param>
  /// <param name="objId">Ид. версии объекта</param>
  /// <returns>Данные с условиями </returns>
  public static TempFormula LoadObjectCond(IUserSession ius, long objId)
  {
    return CondHelper.LoadObjectCond(ius, objId, MetaDataHelper.GetAttributeTypeID(ExpertAttrGUIDs.condObj));
  }

  /// <summary>Загрузка условия у заданного объекта / атрибута</summary>
  /// <param name="ius">Пользовательская сессия</param>
  /// <param name="objId">Ид. версии объекта</param>
  /// <param name="attrID">Ид. типа атрибута</param>
  /// <returns>Данные с условиями  / формулой </returns>
  public static TempFormula LoadObjectCond(IUserSession ius, long objId, int attrID)
  {
    TempFormula tempFormula = (TempFormula) null;
    if (ius == null || objId == 0L || attrID == 0)
      return tempFormula;
    IDBObject dbObject = ius.GetObject(objId, false);
    if (dbObject == null || !(dbObject.GetAttributeByID(attrID) is IBlobReader attributeById))
      return tempFormula;
    BlobInformation blobInformation = attributeById.OpenBlob(0);
    try
    {
      if (blobInformation.PackedFileSize == 0L)
        return tempFormula;
      byte[] zipScr = attributeById.ReadDataBlock((int) blobInformation.PackedFileSize);
      if (zipScr.Length != 0)
      {
        using (Stream stream = ZlibHelper.UnpackBuffer(zipScr))
        {
          stream.Position = 0L;
          using (StreamReader streamReader = new StreamReader(stream))
          {
            char[] buffer = new char[6];
            streamReader.ReadBlock(buffer, 0, 5);
            string str = new string(buffer);
            stream.Position = 0L;
            if (str.StartsWith("<Form"))
            {
              XmlDocument xmlDocument = new XmlDocument();
              xmlDocument.Load(stream);
              tempFormula = new TempFormula((XmlNode) xmlDocument.DocumentElement);
            }
            else
              tempFormula = new BinaryFormatter().Deserialize(stream) as TempFormula;
          }
          tempFormula.FixInfixForm(ius);
          tempFormula.UpdateTokenBegs();
        }
      }
    }
    finally
    {
      attributeById.CloseBlob();
    }
    return tempFormula;
  }

  /// <summary>Сохранение условия у заданного объекта</summary>
  /// <param name="ius">Пользовательская сессия</param>
  /// <param name="objId">Ид. версии объекта</param>
  /// <param name="cond">Данные с условиями</param>
  public static void SaveObjectCond(IUserSession ius, long objId, TempFormula cond)
  {
    CondHelper.SaveObjectCond(ius, objId, MetaDataHelper.GetAttributeTypeID(ExpertAttrGUIDs.condObj), cond);
  }

  /// <summary>Сохранение условия у заданного объекта / атрибута</summary>
  /// <param name="ius">Пользовательская сессия</param>
  /// <param name="objId">Ид. версии объекта</param>
  /// <param name="attrID">Ид. типа атрибута</param>
  /// <param name="cond">Данные с условиями  / формулой </param>
  public static void SaveObjectCond(IUserSession ius, long objId, int attrID, TempFormula cond)
  {
    if (ius == null || cond == null || objId == 0L || attrID == 0)
      return;
    IDBObject dbObject = ius.GetObject(objId, false);
    if (dbObject == null || !((dbObject.GetAttributeByID(ExpertConsts.Consts.attrCondObj) ?? dbObject.Attributes.AddAttribute(ExpertConsts.Consts.attrCondObj, false)) is IBlobWriter blobWriter))
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      XmlTextWriter writer = new XmlTextWriter((Stream) memoryStream, Encoding.UTF8);
      cond.WriteToXML(ref writer);
      writer.Flush();
      byte[] data = ZlibHelper.PackBuffer((Stream) memoryStream);
      BlobInformation blobInfo = new BlobInformation(memoryStream.Length, (long) data.Length, DateTime.Now, "", ArcMethods.ZLibPacked, "");
      try
      {
        if (!blobWriter.OpenBlob(blobInfo, false))
          return;
        blobWriter.WriteDataBlock(data);
      }
      catch
      {
        blobWriter.CancelWrite();
        throw;
      }
    }
  }
}
