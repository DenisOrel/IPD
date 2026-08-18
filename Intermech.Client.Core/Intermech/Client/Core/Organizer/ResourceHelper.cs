
// Type: Intermech.Client.Core.Organizer.ResourceHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.IO;
using System.Reflection;


namespace Intermech.Client.Core.Organizer;

/// <summary>Класс для получения картинок из ресурсов.</summary>
internal class ResourceHelper
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="assembly"></param>
  /// <param name="resStr"></param>
  /// <returns></returns>
  /// <remarks>В качестве типа Т предполагаются типы принимающие в конструкторе объект Stream, такие как Icon, Bitmap</remarks>
  internal static T GetResourceData<T>(Assembly assembly, string resStr) where T : IDisposable
  {
    T resourceData = default (T);
    Stream manifestResourceStream = assembly.GetManifestResourceStream(resStr);
    try
    {
      object instance = Activator.CreateInstance(typeof (T), (object) manifestResourceStream);
      if (instance != null)
        resourceData = (T) instance;
    }
    finally
    {
      if (typeof (T) == typeof (Icon))
        manifestResourceStream.Close();
    }
    return resourceData;
  }
}
