// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Navigator.Windows.ObjectVersionCategoryWindowSettingsProvider
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Search.Navigator.Windows;

/// <summary>
/// <para>Провайдер настроек окон навигатора для категории узлов "Типы объектов"</para>
/// <para>Алгоритм работы предусматривает прохождение по всей цепочке наследования до нахождения нужных настроек</para>
/// </summary>
internal sealed class ObjectVersionCategoryWindowSettingsProvider : IWindowSettingsProvider
{
  public WindowSettingsBase Get(int typeID, WindowSettingsCollection collection)
  {
    Dictionary<int, WindowSettingsBase> settings1 = collection != null ? collection.Get(1) : throw new ArgumentNullException(nameof (collection));
    if (settings1 == null)
    {
      settings1 = new Dictionary<int, WindowSettingsBase>();
      collection.AddOrSet(4, settings1);
    }
    WindowSettingsBase settings2 = (WindowSettingsBase) null;
    if (settings1.TryGetValue(typeID, out settings2))
      return settings2;
    foreach (int key in MetaDataHelper.GetObjectTypeParentsID(typeID))
    {
      if (settings1.TryGetValue(key, out settings2))
      {
        WindowSettingsBase windowSettingsBase = settings2.Clone() as WindowSettingsBase;
        this.Set(typeID, settings2, collection);
        return windowSettingsBase;
      }
    }
    return (WindowSettingsBase) null;
  }

  public void Set(int typeID, WindowSettingsBase settings, WindowSettingsCollection collection)
  {
    if (settings == null)
      throw new ArgumentNullException(nameof (settings));
    if (collection == null)
      throw new ArgumentNullException(nameof (collection));
    collection.AddOrSet(1, typeID, settings);
  }
}
