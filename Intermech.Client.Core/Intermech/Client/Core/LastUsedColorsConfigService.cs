
// Type: Intermech.Client.Core.LastUsedColorsConfigService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Controls;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Client.Core;

/// <summary>Сервис сохранения/чтения списков последних выбранных цветов в рамках различных именнованных операций</summary>
public static class LastUsedColorsConfigService
{
  private const string ConfigId = "LastUsedColors";
  private const string valueTag = "ColorsArray";
  private static bool _wasInit;

  public static void Init()
  {
    if (LastUsedColorsConfigService._wasInit)
      return;
    ColorSelectionUserControl.InitLastColorsLoadSave(new Func<string, Color[]>(LastUsedColorsConfigService.LoadConfig), new Action<string, Color[]>(LastUsedColorsConfigService.SaveConfig));
    LastUsedColorsConfigService._wasInit = true;
  }

  private static void SaveConfig(string operationName, Color[] lastUsedColors)
  {
    IConfiguration namedConfiguration = LastUsedColorsConfigService.GetNamedConfiguration(operationName, true);
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) lastUsedColors);
      string base64String = Convert.ToBase64String(serializationStream.ToArray());
      namedConfiguration.SetProperty("ColorsArray", base64String);
    }
  }

  private static Color[] LoadConfig(string operationName)
  {
    IConfiguration namedConfiguration = LastUsedColorsConfigService.GetNamedConfiguration(operationName);
    if (namedConfiguration != null && namedConfiguration.HasProperty("ColorsArray"))
    {
      string property = namedConfiguration.GetProperty("ColorsArray");
      if (string.IsNullOrEmpty(property))
        return (Color[]) null;
      byte[] buffer = Convert.FromBase64String(property);
      if (buffer == null || buffer.Length == 0)
        return (Color[]) null;
      using (MemoryStream serializationStream = new MemoryStream(buffer))
      {
        object obj = new BinaryFormatter().Deserialize((Stream) serializationStream);
        if (obj is Color[])
          return (Color[]) obj;
      }
    }
    return (Color[]) null;
  }

  private static IConfiguration LastUsedColorsConfiguration
  {
    get
    {
      return !(ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service) ? (IConfiguration) null : service.Open("LastUsedColors") ?? service.Create("LastUsedColors");
    }
  }

  private static IConfiguration GetNamedConfiguration(string operationName, bool autoCreate = false)
  {
    IConfiguration colorsConfiguration = LastUsedColorsConfigService.LastUsedColorsConfiguration;
    operationName = string.IsNullOrEmpty(operationName) ? "Empty" : operationName;
    if (colorsConfiguration == null)
      return (IConfiguration) null;
    IConfiguration namedConfiguration = colorsConfiguration.Open(operationName);
    if (namedConfiguration == null & autoCreate)
      namedConfiguration = colorsConfiguration.Add(operationName);
    return namedConfiguration;
  }
}
