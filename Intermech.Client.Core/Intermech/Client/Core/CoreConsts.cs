
// Type: Intermech.Client.Core.CoreConsts
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;


namespace Intermech.Client.Core;

public class CoreConsts
{
  private static int idGenerator = -100;
  public static readonly string AnyLevel = LocalizationHolder.rm.GetString("Client.Core_976");
  public static readonly string NegativeIdDefaultMCaption = LocalizationHolder.rm.GetString("Client.Core_976");
  public static readonly string NegativeIdDefaultFCaption = LocalizationHolder.rm.GetString("Client.Core_1057");
  public static readonly string UnlimitedCaption = LocalizationHolder.rm.GetString("Client.Core_1058");
  public static readonly string CurrentUserCaption = LocalizationHolder.rm.GetString("Client.Core_CurrentUser");
  public static int ObjectTypeToPaste = -1;
  public static int MaxMemoEditorSizeDefault = 0;
  /// <summary>
  /// флаг на фильтрацию при запросе данных у сервера // в конфигураторе не фильтровать
  /// </summary>
  public static bool FilterRecords = false;
  public const string F_ATTRIBUTE_ID_STR = "F_ATTRIBUTE_ID_STR";
  public const string F_OBJECT_TYPE_STR = "F_OBJECT_TYPE_STR";
  public const string F_RELATION_TYPE_STR = "F_RELATION_TYPE_STR";
  /// <summary>
  /// чуствительность при анализе длительностей на чтение/запись/поиск
  /// </summary>
  public static readonly int StatisticsSensitivity = 5;

  public static int IDGeneratorNextValue => --CoreConsts.idGenerator;
}
