// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ResourceHelper
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Drawing;
using System.IO;
using System.Reflection;

#nullable disable
namespace Intermech.Imbase;

internal class ResourceHelper
{
  internal static T GetResourceData<T>(Assembly assembly, string resStr)
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
