// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.ImageListSource
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Определяет imagelist, из которого будет браться иконка
/// </summary>
public enum ImageListSource
{
  /// <summary>Глобальный список именованных изображений</summary>
  NamedImageList = 1,
  /// <summary>Глобальный список изображений для категорий и типов</summary>
  CategoryImageList = 2,
}
