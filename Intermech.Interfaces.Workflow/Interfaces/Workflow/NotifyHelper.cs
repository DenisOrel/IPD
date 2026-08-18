// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.NotifyHelper
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Interfaces.Workflow;

/// <summary>Хелпер</summary>
public class NotifyHelper
{
  public static string MessageSubject = LocalizationHolder.rm.GetString("Interfaces.Workflow_5");
  public static string MessageBody = LocalizationHolder.rm.GetString("Interfaces.Workflow_6") + LocalizationHolder.rm.GetString("Interfaces.Workflow_7") + LocalizationHolder.rm.GetString("Interfaces.Workflow_8");
  /// <summary>Разделитель ГУИДов в строке атрибута ""</summary>
  public const char GuidSeparator = ',';
  /// <summary>
  /// Максимально возможная длина строки атрибута "Перечень ГУИДов атрибутов для уведомления"
  /// </summary>
  public const int GuidAttributeMaxLength = 20;
  /// <summary>
  /// Максимальное количество атрибутов, которые можно сохранить в атрибут "Перечень ГУИДов атрибутов для уведомления" (12)
  /// Максимальная длина строки атрибута / (Количество символов в ГУИДе + ",")  450 / 37
  /// </summary>
  public const int MaxAttrsCount = 12;

  public static void SaveListAttributes(IDBAttribute attrAttributes, List<int> attributes)
  {
    List<Guid> guids = new List<Guid>();
    for (int index = 0; index < attributes.Count; ++index)
      guids.Add(MetaDataHelper.GetAttributeTypeGuid(attributes[index]));
    NotifyHelper.SaveListAttributes(attrAttributes, guids);
  }

  public static void SaveListAttributes(IDBAttribute attrAttributes, List<Guid> guids)
  {
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) guids);
      IBlobWriter blobWriter = attrAttributes as IBlobWriter;
      serializationStream.Position = 0L;
      BlobInformation blobInfo = new BlobInformation(serializationStream.Length, serializationStream.Length, DateTime.Now, string.Empty, ArcMethods.NotPacked, string.Empty);
      if (!blobWriter.OpenBlob(blobInfo, false))
        return;
      blobWriter.WriteDataBlock(serializationStream.ToArray());
    }
  }

  /// <summary>
  /// Сохранить список идентификаторов атрибутов уведомления в строковый атрибут с перечнем ГУИДов
  /// </summary>
  /// <param name="attrGuidsAttributes">Атрибут с перечнем ГУИДов.</param>
  /// <param name="attributes">Список идентификаторов атрибутов</param>
  public static void SaveGuidsAttributes(IDBAttribute attrGuidsAttributes, List<int> attributes)
  {
    List<Guid> guids = new List<Guid>();
    for (int index = 0; index < attributes.Count; ++index)
      guids.Add(MetaDataHelper.GetAttributeTypeGuid(attributes[index]));
    NotifyHelper.SaveGuidsAttributes(attrGuidsAttributes, guids);
  }

  /// <summary>
  /// Сохранить ГУИДы атрибутов уведомления в строковый атрибут с перечнем ГУИДов
  /// </summary>
  /// <param name="attrGuidsAttributes">Атрибут с перечнем ГУИДов.</param>
  /// <param name="guids">Список ГУИДов атрибутов</param>
  public static void SaveGuidsAttributes(IDBAttribute attrGuidsAttributes, List<Guid> guids)
  {
    string str = string.Empty;
    foreach (Guid guid in guids)
      str = !(str == string.Empty) ? str + ','.ToString((IFormatProvider) CultureInfo.InvariantCulture) + (object) guid : str + guid.ToString();
    attrGuidsAttributes.Value = (object) str;
  }

  /// <summary>
  /// Получает список ГУИДов атрибутов для уведомления из блоба.
  /// </summary>
  /// <param name="attrAttributes">Атрибут.</param>
  /// <returns>Список ГУИДов атрибутов</returns>
  public static List<Guid> GetListAttributesFromBlob(IDBAttribute attrAttributes)
  {
    List<Guid> attributesFromBlob = new List<Guid>();
    IBlobReader blobReader = attrAttributes as IBlobReader;
    if (blobReader.OpenBlob(0).RealFileSize > 0L)
    {
      using (MemoryStream serializationStream = new MemoryStream(blobReader.ReadDataBlock()))
        attributesFromBlob = (List<Guid>) new BinaryFormatter().Deserialize((Stream) serializationStream);
    }
    return attributesFromBlob;
  }

  /// <summary>
  /// Получает список ИД атрибутов для уведомления из строкового значения атрибутов
  /// </summary>
  /// <param name="attrGuidsAttributes">Интерфейс строкового атрибута с ГУИДами атрибутов для подписки</param>
  /// <returns>Список ИД атрибутов</returns>
  public static List<int> GetAttributesIDsFromGuidsAttribute(IDBAttribute attrGuidsAttributes)
  {
    List<int> fromGuidsAttribute1 = new List<int>();
    List<Guid> fromGuidsAttribute2 = NotifyHelper.GetAttributesListFromGuidsAttribute(attrGuidsAttributes);
    for (int index = 0; index < fromGuidsAttribute2.Count; ++index)
      fromGuidsAttribute1.Add(MetaDataHelper.GetAttributeTypeID(fromGuidsAttribute2[index]));
    return fromGuidsAttribute1;
  }

  /// <summary>
  /// Получает список ГУИДОВ из строкового значения атрибута.
  /// </summary>
  /// <param name="attrGuidsAttribute">Интерфейс строкового атрибута с ГУИДами атрибутов для подписки</param>
  /// <returns>Список ГУИДов</returns>
  public static List<Guid> GetAttributesListFromGuidsAttribute(IDBAttribute attrGuidsAttribute)
  {
    return NotifyHelper.GetGuidsFromString(attrGuidsAttribute.AsString);
  }

  /// <summary>Формирует список ГУИДов из строки</summary>
  /// <param name="strGuids">Строка с перечнем ГУИДов</param>
  /// <returns>Список ГУИДов</returns>
  private static List<Guid> GetGuidsFromString(string strGuids)
  {
    List<Guid> guidsFromString = new List<Guid>();
    if (string.IsNullOrWhiteSpace(strGuids))
      return guidsFromString;
    strGuids = strGuids.Trim(' ');
    string str = strGuids;
    char[] chArray = new char[1]{ ',' };
    foreach (string g in str.Split(chArray))
    {
      Guid guid = new Guid(g);
      guidsFromString.Add(guid);
    }
    return guidsFromString;
  }

  /// <summary>Считывает и записывает в списки атрибуты уведомления.</summary>
  /// <param name="notificationAttrValues">The notification attr values.</param>
  /// <param name="currentNotifUserIDs">The current notif user I ds.</param>
  /// <param name="currentNotifOptions">The current notif options.</param>
  /// <param name="currentNotifAttributes">The current notif attributes.</param>
  public static void ReadNotifyAttributes(
    AttributeValues[] notificationAttrValues,
    ref List<long> currentNotifUserIDs,
    ref List<NotifyOptions> currentNotifOptions,
    ref List<List<Guid>> currentNotifAttributes)
  {
    foreach (AttributeValues notificationAttrValue in notificationAttrValues)
    {
      if (notificationAttrValue.AttributeID == wfConsts.AttrAddresseeNoticeID)
        currentNotifUserIDs.AddRange(notificationAttrValue.Values.Cast<long>());
      if (notificationAttrValue.AttributeID == wfConsts.AttrNotifyOptionsID)
      {
        foreach (object obj in notificationAttrValue.Values)
        {
          int int32 = Convert.ToInt32(obj);
          currentNotifOptions.Add((NotifyOptions) int32);
        }
      }
      if (notificationAttrValue.AttributeID == wfConsts.AttrGUIDsAttributesID)
      {
        foreach (object obj in notificationAttrValue.Values)
        {
          if (obj != null)
          {
            if (obj == DBNull.Value)
            {
              currentNotifAttributes.Add(new List<Guid>());
            }
            else
            {
              List<Guid> guidsFromString = NotifyHelper.GetGuidsFromString(Convert.ToString(obj));
              currentNotifAttributes.Add(guidsFromString);
            }
          }
        }
      }
    }
  }

  public static List<Notify> InitNotifyList(IDBAttribute attrUsers)
  {
    List<Notify> notifyList = new List<Notify>();
    for (int index = 0; index < attrUsers.ValuesCount; ++index)
    {
      attrUsers.Index = index;
      if (attrUsers.IsNull)
        notifyList.Add(new Notify(-1L, string.Empty));
      notifyList.Add(new Notify(attrUsers.AsInteger, attrUsers.AsString));
    }
    return notifyList;
  }
}
