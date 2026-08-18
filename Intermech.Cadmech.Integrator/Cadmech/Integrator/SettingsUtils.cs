// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.SettingsUtils
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal static class SettingsUtils
{
  public const string CommonCategory = "1. Общие настройки";
  public const string MechanicalCategory = "2. Конструкторская документация";
  public const string ConstructionalCategory = "3. Проектная документация";
  public const string StartupConfigsList = "Параметры подключения к приложению";
  public const string MechanicalAssembliesList = "Сборочные чертежи";
  public const string MechanicalPartsList = "Чертежи деталей";
  public const string ConstructionalDrawingsList = "СПДС-Чертежи";
  public const string AuxiliaryDocumentsList = "Вспомогательные документы";

  public static string GetRoleCaption(UserRoleMarker userRole)
  {
    return userRole == null ? "<любая роль>" : userRole.Name;
  }

  public static string TrimStringValue(string value) => value == null ? string.Empty : value.Trim();

  public static string ValidateStmName(string stmName)
  {
    if (!string.IsNullOrEmpty(stmName))
    {
      if (stmName.IndexOfAny(Path.GetInvalidPathChars()) == -1)
      {
        string str;
        try
        {
          str = Path.GetDirectoryName(stmName);
        }
        catch (ArgumentException ex)
        {
          str = "<bad path>";
        }
        if (!string.IsNullOrEmpty(str))
          return "Имя файла с параметрами сканирования штампа чертежа должно быть задано без пути.";
      }
      if (stmName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
        return "Имя файла с параметрами сканирования штампа чертежа содержит недопустимые символы.";
    }
    return (string) null;
  }

  public static string ValidateStmBody(string stmBody)
  {
    if (!string.IsNullOrEmpty(stmBody))
    {
      string[] strArray = stmBody.Split(new string[1]
      {
        Environment.NewLine
      }, StringSplitOptions.None);
      for (int index = 1; index <= strArray.Length; ++index)
      {
        if (string.IsNullOrEmpty(strArray[index - 1].Trim()) && index < strArray.Length)
          return $"Строка {index} не может быть пустой.";
      }
    }
    return (string) null;
  }

  public static string CleanupStmBody(string dirtyStmBody)
  {
    string[] strArray = dirtyStmBody.Split(new string[1]
    {
      Environment.NewLine
    }, StringSplitOptions.None);
    StringBuilder stringBuilder = new StringBuilder(strArray.Length * 80 /*0x50*/);
    bool flag1 = true;
    bool flag2 = false;
    foreach (string str1 in strArray)
    {
      char[] chArray = new char[2]{ ' ', '\t' };
      string str2 = str1.Trim(chArray);
      if (!string.IsNullOrEmpty(str2) || !flag1)
      {
        if (flag2)
          stringBuilder.AppendLine();
        stringBuilder.Append(str2);
        flag1 = false;
        flag2 = true;
      }
    }
    return stringBuilder.ToString();
  }
}
