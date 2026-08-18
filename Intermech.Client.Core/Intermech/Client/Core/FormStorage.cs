
// Type: Intermech.Client.Core.FormStorage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// Класс для сохранения и восстановления позиции и размера модальных окон
/// </summary>
public class FormStorage
{
  private static readonly string configId = nameof (FormStorage);
  private static readonly string locationTag = "location";
  private static readonly string sizeTag = "size";
  private static readonly string dataTag = "data";

  private static IConfiguration GetControlStorageConfiguration()
  {
    return !(ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service) ? (IConfiguration) null : service.Open(FormStorage.configId) ?? service.Create(FormStorage.configId);
  }

  /// <summary>
  /// Проверить, вписываются ли координаты в отображаемые и скорректировать при необходимости:
  /// в условиях нескольких мониторов и их отключения-подключения позиция уходить за грани видимого
  /// </summary>
  /// <param name="lLocation"></param>
  /// <returns></returns>
  public static Point ValidateLocation(Point lLocation)
  {
    Rectangle virtualScreen;
    if (lLocation.X >= SystemInformation.VirtualScreen.X)
    {
      int x1 = lLocation.X;
      int x2 = SystemInformation.VirtualScreen.X;
      virtualScreen = SystemInformation.VirtualScreen;
      int width = virtualScreen.Width;
      int num1 = x2 + width;
      if (x1 <= num1)
      {
        int y1 = lLocation.Y;
        virtualScreen = SystemInformation.VirtualScreen;
        int y2 = virtualScreen.Y;
        if (y1 >= y2)
        {
          int y3 = lLocation.Y;
          virtualScreen = SystemInformation.VirtualScreen;
          int y4 = virtualScreen.Y;
          virtualScreen = SystemInformation.VirtualScreen;
          int height = virtualScreen.Height;
          int num2 = y4 + height;
          if (y3 <= num2)
            goto label_10;
        }
      }
    }
    int x3 = lLocation.X;
    virtualScreen = SystemInformation.VirtualScreen;
    int x4 = virtualScreen.X;
    if (x3 >= x4)
    {
      int x5 = lLocation.X;
      virtualScreen = SystemInformation.VirtualScreen;
      int x6 = virtualScreen.X;
      virtualScreen = SystemInformation.VirtualScreen;
      int width = virtualScreen.Width;
      int num = x6 + width;
      if (x5 < num)
        goto label_7;
    }
    ref Point local1 = ref lLocation;
    virtualScreen = SystemInformation.VirtualScreen;
    int x7 = virtualScreen.X;
    local1.X = x7;
label_7:
    int y5 = lLocation.Y;
    virtualScreen = SystemInformation.VirtualScreen;
    int y6 = virtualScreen.Y;
    if (y5 >= y6)
    {
      int y7 = lLocation.Y;
      virtualScreen = SystemInformation.VirtualScreen;
      int y8 = virtualScreen.Y;
      virtualScreen = SystemInformation.VirtualScreen;
      int height = virtualScreen.Height;
      int num = y8 + height;
      if (y7 < num)
        goto label_10;
    }
    ref Point local2 = ref lLocation;
    virtualScreen = SystemInformation.VirtualScreen;
    int y9 = virtualScreen.Y;
    local2.Y = y9;
label_10:
    return lLocation;
  }

  /// <summary>Загрузить состояние указанного элемента управления</summary>
  /// <param name="control">Элемент управления</param>
  public static bool LoadLayout(Control control)
  {
    return FormStorage.LoadLayout(control, (IDictionary) null);
  }

  /// <summary>
  /// Загрузить состояние указанного элемента управления, а также его дополнительные настройки
  /// </summary>
  /// <param name="control">Элемент управления</param>
  /// <param name="iDictionary">Дополнительные настройки для элемента управления</param>
  public static bool LoadLayout(Control control, IDictionary iDictionary)
  {
    return FormStorage.LoadLayout(control, iDictionary, false, out Point _, out Size _);
  }

  public static bool LoadLayout(
    Control control,
    IDictionary iDictionary,
    bool returnDataOnly,
    out Point lLocation,
    out Size lSize)
  {
    return FormStorage.LoadLayout(control, $"{control.GetType().ToString()}_{control.Name}", iDictionary, returnDataOnly, out lLocation, out lSize);
  }

  /// <summary>
  /// /// Загрузить состояние указанного элемента управления, а также его дополнительные настройки
  /// </summary>
  /// <param name="control">Элемент управления</param>
  /// <param name="configName">Имя конфигурации формы</param>
  /// <param name="iDictionary">Дополнительные настройки для элемента управления</param>
  /// <param name="returnDataOnly">true - только возврат данных, без присвоения значений в форму</param>
  /// <param name="lLocation">Местонахождение</param>
  /// <param name="lSize">Размеры</param>
  public static bool LoadLayout(
    Control control,
    string configName,
    IDictionary iDictionary,
    bool returnDataOnly,
    out Point lLocation,
    out Size lSize)
  {
    lLocation = new Point();
    lSize = new Size();
    IConfiguration storageConfiguration = FormStorage.GetControlStorageConfiguration();
    if (storageConfiguration == null)
      return false;
    IConfiguration configuration = storageConfiguration.Open(configName);
    if (configuration == null)
      return false;
    try
    {
      string str1 = string.Empty;
      if (configuration.HasProperty(FormStorage.locationTag))
        str1 = configuration.GetProperty(FormStorage.locationTag);
      if (str1 != string.Empty)
      {
        lLocation = (Point) TypeDescriptor.GetConverter(typeof (Point)).ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) str1.Replace(";", ","));
        if (control is Form)
          lLocation = FormStorage.ValidateLocation(lLocation);
        if (!returnDataOnly)
          control.Location = lLocation;
      }
      string str2 = string.Empty;
      if (configuration.HasProperty(FormStorage.sizeTag))
        str2 = configuration.GetProperty(FormStorage.sizeTag);
      if (str2 != string.Empty)
      {
        lSize = (Size) TypeDescriptor.GetConverter(typeof (Size)).ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) str2.Replace(";", ","));
        bool flag = !returnDataOnly;
        if (flag && control is Form)
        {
          Form form = (Form) control;
          if (form.FormBorderStyle != FormBorderStyle.Sizable && form.FormBorderStyle != FormBorderStyle.SizableToolWindow)
            flag = false;
        }
        if (flag)
          control.Size = lSize;
      }
      if (configuration.HasProperty(FormStorage.dataTag) && iDictionary != null)
      {
        object obj = new BinaryFormatter().Deserialize((Stream) new MemoryStream(Convert.FromBase64String(configuration.GetProperty(FormStorage.dataTag))));
        if (obj is IDictionary)
        {
          IDictionary dictionary = obj as IDictionary;
          foreach (object key in (IEnumerable) dictionary.Keys)
            iDictionary[key] = dictionary[key];
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("Client.Core_1087"));
      return false;
    }
  }

  /// <summary>Сохранить настройки указанного элемента управления</summary>
  /// <param name="control">Элемент управления</param>
  public static void SaveLayout(Control control)
  {
    FormStorage.SaveLayout(control, (IDictionary) null);
  }

  public static void SaveLayout(Control control, IDictionary iDictionary)
  {
    FormStorage.SaveLayout(control, $"{control.GetType().ToString()}_{control.Name}", iDictionary);
  }

  /// <summary>
  /// Сохранить настройки указанного элемента управления, включая дополнительные данные
  /// </summary>
  /// <param name="control">Элемент управления</param>
  /// <param name="configName">Имя конфигурации формы</param>
  /// <param name="iDictionary">Дополнительные данные</param>
  public static void SaveLayout(Control control, string configName, IDictionary iDictionary)
  {
    IConfiguration storageConfiguration = FormStorage.GetControlStorageConfiguration();
    if (storageConfiguration == null)
      return;
    IConfiguration configuration = storageConfiguration.Open(configName) ?? storageConfiguration.Add(configName);
    if (configuration == null)
      return;
    try
    {
      configuration.SetProperty(FormStorage.locationTag, (string) TypeDescriptor.GetConverter(typeof (Point)).ConvertTo((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) control.Location, typeof (string)));
      configuration.SetProperty(FormStorage.sizeTag, (string) TypeDescriptor.GetConverter(typeof (Size)).ConvertTo((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) control.Size, typeof (string)));
      if (iDictionary == null)
        return;
      Hashtable graph = new Hashtable(iDictionary);
      MemoryStream serializationStream = new MemoryStream();
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) graph);
      string base64String = Convert.ToBase64String(serializationStream.ToArray());
      configuration.SetProperty(FormStorage.dataTag, base64String);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("Client.Core_1087"));
    }
  }
}
