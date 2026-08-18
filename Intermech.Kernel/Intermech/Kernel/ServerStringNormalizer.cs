// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ServerStringNormalizer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Text;


namespace Intermech.Kernel;

public class ServerStringNormalizer
{
  private static bool deleteSpaces = true;
  private static bool upperCase = true;
  private static bool cyrillicReplace = true;
  private static string[] deleteDuplicates = new string[0];
  private static string[] replaceSymbols = new string[0];
  private static bool Inited = false;
  public static string RusLettersUpper = "ЕТОРАНКХСВМ";
  public static string LatLettersUpper = "ETOPAHKXCBM";
  public static char[] RusLettersUpperChars = new char[11]
  {
    'Е',
    'Т',
    'О',
    'Р',
    'А',
    'Н',
    'К',
    'Х',
    'С',
    'В',
    'М'
  };
  public static char[] LatLettersUpperChars = new char[11]
  {
    'E',
    'T',
    'O',
    'P',
    'A',
    'H',
    'K',
    'X',
    'C',
    'B',
    'M'
  };
  public static string RusLettersAll = "ЕТОРАНКХСВМеоракхсм";
  public static string LatLettersAll = "ETOPAHKXCBMeopakxcm";
  public static string RusLettersLower = ServerStringNormalizer.RusLettersUpper.ToLower();
  public static string LatLettersLower = ServerStringNormalizer.LatLettersUpper.ToLower();
  public static char[] rus_chars_lower = new char[33]
  {
    'ё',
    'й',
    'ц',
    'у',
    'к',
    'е',
    'н',
    'г',
    'ш',
    'щ',
    'з',
    'х',
    'ъ',
    'ф',
    'ы',
    'в',
    'а',
    'п',
    'р',
    'о',
    'л',
    'д',
    'ж',
    'э',
    'я',
    'ч',
    'с',
    'м',
    'и',
    'т',
    'ь',
    'б',
    'ю'
  };
  public static string rus_chars_upper_str = "ЁЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ";

  public static bool IsUpperRus(char letter)
  {
    return ServerStringNormalizer.rus_chars_upper_str.IndexOf(letter) >= 0;
  }

  public static string NormalizeToUpperRus(string strToRus)
  {
    int num = strToRus.IndexOfAny(ServerStringNormalizer.LatLettersUpperChars);
    if (num < 0)
      return strToRus;
    StringBuilder stringBuilder = new StringBuilder(strToRus);
    for (int index1 = num; index1 < strToRus.Length; ++index1)
    {
      for (int index2 = 0; index2 < ServerStringNormalizer.LatLettersUpperChars.Length; ++index2)
      {
        if ((int) strToRus[index1] == (int) ServerStringNormalizer.LatLettersUpperChars[index2])
        {
          stringBuilder[index1] = ServerStringNormalizer.RusLettersUpperChars[index2];
          break;
        }
      }
    }
    return stringBuilder.ToString();
  }

  public static string NormalizeToUpperLat(string strToLat)
  {
    int num = strToLat.IndexOfAny(ServerStringNormalizer.RusLettersUpperChars);
    if (num < 0)
      return strToLat;
    StringBuilder stringBuilder = new StringBuilder(strToLat);
    for (int index1 = num; index1 < strToLat.Length; ++index1)
    {
      for (int index2 = 0; index2 < ServerStringNormalizer.RusLettersUpperChars.Length; ++index2)
      {
        if ((int) strToLat[index1] == (int) ServerStringNormalizer.RusLettersUpperChars[index2])
        {
          stringBuilder[index1] = ServerStringNormalizer.LatLettersUpperChars[index2];
          break;
        }
      }
    }
    return stringBuilder.ToString();
  }

  public static void LoadSettings(NormalizerSettings settings)
  {
    ServerStringNormalizer.deleteSpaces = settings.DeleteSpaces;
    ServerStringNormalizer.upperCase = settings.UpperCase;
    ServerStringNormalizer.cyrillicReplace = settings.CyrillicReplace;
    ServerStringNormalizer.deleteDuplicates = settings.DeleteDuplicates;
    ServerStringNormalizer.replaceSymbols = settings.ReplaceSymbols;
    ServerStringNormalizer.Inited = true;
  }

  public static NormalizerSettings CaptureSettings()
  {
    return new NormalizerSettings()
    {
      DeleteSpaces = ServerStringNormalizer.deleteSpaces,
      UpperCase = ServerStringNormalizer.upperCase,
      CyrillicReplace = ServerStringNormalizer.cyrillicReplace,
      DeleteDuplicates = (string[]) ServerStringNormalizer.deleteDuplicates.Clone(),
      ReplaceSymbols = (string[]) ServerStringNormalizer.replaceSymbols.Clone()
    };
  }

  public static string GetIndexedString(string str_to_index)
  {
    if (!ServerStringNormalizer.Inited)
      throw new Exception("ServerStringNormalizer not initialized! Call ServerStringNormalizer.LoadSettings first.");
    StringBuilder stringBuilder1 = new StringBuilder(str_to_index);
    if (ServerStringNormalizer.deleteSpaces)
      stringBuilder1.Replace(" ", string.Empty);
    if (stringBuilder1.Length > 0)
    {
      foreach (string deleteDuplicate in ServerStringNormalizer.deleteDuplicates)
      {
        while (stringBuilder1.ToString().IndexOf(deleteDuplicate + deleteDuplicate) >= 0)
          stringBuilder1.Replace(deleteDuplicate + deleteDuplicate, deleteDuplicate);
      }
      int num1 = 0;
      while (num1 < ServerStringNormalizer.replaceSymbols.Length)
      {
        StringBuilder stringBuilder2 = stringBuilder1;
        string[] replaceSymbols1 = ServerStringNormalizer.replaceSymbols;
        int index1 = num1;
        int num2 = index1 + 1;
        string oldValue = replaceSymbols1[index1];
        string[] replaceSymbols2 = ServerStringNormalizer.replaceSymbols;
        int index2 = num2;
        num1 = index2 + 1;
        string newValue = replaceSymbols2[index2];
        stringBuilder2.Replace(oldValue, newValue);
      }
      if (ServerStringNormalizer.upperCase)
      {
        stringBuilder1 = new StringBuilder(stringBuilder1.ToString().ToUpper());
        if (ServerStringNormalizer.cyrillicReplace)
        {
          for (int index = 0; index < ServerStringNormalizer.RusLettersUpper.Length; ++index)
            stringBuilder1.Replace(ServerStringNormalizer.RusLettersUpper[index], ServerStringNormalizer.LatLettersUpper[index]);
        }
      }
      else if (ServerStringNormalizer.cyrillicReplace)
      {
        for (int index = 0; index < ServerStringNormalizer.RusLettersAll.Length; ++index)
          stringBuilder1.Replace(ServerStringNormalizer.RusLettersAll[index], ServerStringNormalizer.LatLettersAll[index]);
      }
    }
    return stringBuilder1.ToString();
  }
}
