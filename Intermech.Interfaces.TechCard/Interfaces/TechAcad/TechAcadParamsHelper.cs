// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechAcad.TechAcadParamsHelper
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.IniFiles;
using Intermech.Localization;
using System;
using System.IO;

#nullable disable
namespace Intermech.Interfaces.TechAcad;

/// <summary>Класс для работы с настройками</summary>
public static class TechAcadParamsHelper
{
  /// <summary>Переменная окружения кадмеха</summary>
  internal const string CntSysCadmParam = "CADM";
  /// <summary>Файл с настройками кадмеха по-умолчанию</summary>
  internal const string CntSysCadmConfig = "config_cadm_ips.ini";
  /// <summary>Шаблон с параметрами запуска кадмеха</summary>
  internal const string CntSysParamsTemplate = "/p <<CADMT_IPS>>";
  /// <summary>
  /// 
  /// </summary>
  internal const string CntIniDefault = "Default";
  /// <summary>
  /// 
  /// </summary>
  internal const string CntIniAcadPath = "AcadPath";
  /// <summary>
  /// 
  /// </summary>
  internal const string CntIniProfileName = "ProfileName";
  /// <summary>
  /// 
  /// </summary>
  internal const string CntIniTemplatePath = "TemplatePath";
  /// <summary>
  /// 
  /// </summary>
  internal const string CntIniTemplateShortPath = "TemplateShortPath";

  /// <summary>Сохранение настроек</summary>
  /// <param name="value"></param>
  /// <param name="session"></param>
  /// <param name="service"></param>
  public static bool SaveData(
    TechAcadParamsItem value,
    IUserSession session,
    ITechAcadParamsService service)
  {
    return value != null && session != null && service != null && service.SaveData(value, EnvironmentConsts.MachineName, session.SessionGUID);
  }

  /// <summary>Загрузка настроек</summary>
  /// <param name="value"></param>
  /// <param name="session"></param>
  /// <param name="service"></param>
  /// <returns></returns>
  public static bool LoadData(
    TechAcadParamsItem value,
    IUserSession session,
    ITechAcadParamsService service)
  {
    return TechAcadParamsHelper.LoadData(value, session, service, false);
  }

  /// <summary>Загрузка настроек</summary>
  /// <param name="value"></param>
  /// <param name="session"></param>
  /// <param name="service"></param>
  /// <param name="initSettings">Инициализация параметров настройками по-умолчанию</param>
  /// <returns></returns>
  public static bool LoadData(
    TechAcadParamsItem value,
    IUserSession session,
    ITechAcadParamsService service,
    bool initSettings)
  {
    if (value == null || session == null || service == null)
      return false;
    TechAcadParamsItem source = service.LoadData(EnvironmentConsts.MachineName, session.SessionGUID);
    bool flag = source != null;
    if (flag)
      value.Copy(source);
    if (initSettings && (!flag || value.ApplPath == string.Empty))
      TechAcadParamsHelper.LoadDefData(value, out string _);
    return flag;
  }

  /// <summary>Инициализация настроек по-умолчанию</summary>
  /// <param name="value"></param>
  public static bool LoadDefData(TechAcadParamsItem value, out string errorMessage)
  {
    errorMessage = string.Empty;
    if (value == null)
      return false;
    string path1 = Environment.GetEnvironmentVariable("CADM") ?? string.Empty;
    if (path1 == string.Empty || !Directory.Exists(path1))
    {
      errorMessage = string.Format(LocalizationHolder.rm.GetString("Interfaces.TechCard_24"), (object) "CADM");
      return false;
    }
    string str1 = path1 + "\\config_cadm_ips.ini";
    if (!File.Exists(str1))
    {
      errorMessage = string.Format(LocalizationHolder.rm.GetString("Interfaces.TechCard_25"), (object) str1);
      return false;
    }
    IniFile iniFile = new IniFile(str1);
    string str2 = iniFile.ReadString("Default", "AcadPath", string.Empty);
    value.ApplPath = str2.Replace("\"", string.Empty);
    string str3 = iniFile.ReadString("Default", "TemplatePath", string.Empty);
    value.PrototypeDraft = str3.Replace("\"", string.Empty);
    if (!File.Exists(value.PrototypeDraft))
    {
      string str4 = iniFile.ReadString("Default", "TemplateShortPath", string.Empty);
      string path2 = $"{path1}\\{str4.Replace("\"", string.Empty)}";
      if (File.Exists(path2))
        value.PrototypeDraft = path2;
    }
    value.Params = iniFile.ReadString("Default", "ProfileName", "/p <<CADMT_IPS>>");
    value.FileExtention = "dwg";
    return true;
  }
}
