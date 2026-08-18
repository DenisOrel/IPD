// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ECO.ECOHolder
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

#nullable disable
namespace Intermech.Interfaces.ECO;

/// <summary>Держатель констант плагина ЕСО</summary>
public class ECOHolder
{
  /// <summary>
  /// Режим копирования листа рассылки извещения в документы.
  /// </summary>
  public static bool CopyDeliveryListToDoc;
  /// <summary>Имя модуля для извещений</summary>
  public const string ModuleECO = "ECO";
  /// <summary>Секция настроек для листа рассылки</summary>
  public const string SectionDeliveryList = "DELIVERYLIST";
  /// <summary>
  /// Параметр режима копирования листа рассылки извещения в документы
  /// </summary>
  public const string ParamCopyDeliveryListToDoc = "COPY_DELIVERY_LIST_TO_DOC";
  /// <summary>
  /// Значение параметра копирования листа рассылки по умолчанию
  /// </summary>
  public const bool DefaultParamCopyDeliveryListToDoc = false;

  /// <summary>Инициализация параметра копирования листа рассылки</summary>
  /// <param name="session">Пользовательская сессия</param>
  public static void DeliveryListParametersInit(IUserSession session)
  {
    ECOHolder.CopyDeliveryListToDoc = session.Configurations.ReadBool("ECO", "DELIVERYLIST", "COPY_DELIVERY_LIST_TO_DOC", false, DBConfigMode.GlobalOnly);
  }
}
