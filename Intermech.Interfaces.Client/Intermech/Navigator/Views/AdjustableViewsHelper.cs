// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.AdjustableViewsHelper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>
/// Вспомогательный статический класс для работы с коллекциями настраиваемых закладок
/// </summary>
public static class AdjustableViewsHelper
{
  /// <summary>
  /// Словарь содержит перечень типов объектов и назначенные им закладки по умолчанию
  /// </summary>
  private static Dictionary<int, AdjustableView> DefaultViews = new Dictionary<int, AdjustableView>();
  private static AdjustableViews _deafultAdjustableViews = new AdjustableViews();

  /// <summary>
  /// Метод собирает всю информацию из закладок и размещает её в кэш
  /// </summary>
  /// <param name="views">Коллекция закладок</param>
  public static void ProcessViews(List<AdjustableView> views)
  {
    lock (AdjustableViewsHelper.DefaultViews)
    {
      AdjustableViewsHelper.DefaultViews.Clear();
      if (views == null || views.Count == 0)
        return;
      views.ForEach((Action<AdjustableView>) (view => view.ObjectTypes.ForEach((Action<int>) (typeID => AdjustableViewsHelper.DefaultViews[typeID] = view))));
    }
  }

  /// <summary>
  /// Зарегистрировать закладку по умолчанию для указанного типа объекта
  /// </summary>
  /// <param name="objectTypeID">Идентификатор типа объекта</param>
  /// <param name="view">Закладка по умолчанию</param>
  /// <param name="canOverride">true - выполнять замену ранее зарегистрированной закладки</param>
  public static void RegisterView4ObjectType(
    int objectTypeID,
    AdjustableView view,
    bool canOverride)
  {
    if (objectTypeID == -1 || view == null)
      return;
    lock (AdjustableViewsHelper.DefaultViews)
    {
      if (AdjustableViewsHelper.DefaultViews.ContainsKey(objectTypeID) && !canOverride)
        return;
      AdjustableViewsHelper.DefaultViews[objectTypeID] = view;
      if (view.ObjectTypes.IndexOf(objectTypeID) >= 0)
        return;
      view.ObjectTypes.Add(objectTypeID);
    }
  }

  /// <summary>Удалить привязку типов объектов для закладки</summary>
  /// <param name="view">Закладка</param>
  public static void UnregisterViewObjectTypes(AdjustableView view)
  {
    if (view == null)
      return;
    lock (AdjustableViewsHelper.DefaultViews)
    {
      List<int> intList = new List<int>();
      foreach (KeyValuePair<int, AdjustableView> defaultView in AdjustableViewsHelper.DefaultViews)
      {
        if (defaultView.Value != null && defaultView.Value.Name == view.Name)
        {
          intList.Add(defaultView.Key);
          defaultView.Value.ObjectTypes.Remove(defaultView.Key);
        }
      }
      intList.ForEach((Action<int>) (typeID =>
      {
        if (!AdjustableViewsHelper.DefaultViews.ContainsKey(typeID))
          return;
        AdjustableViewsHelper.DefaultViews.Remove(typeID);
      }));
    }
  }

  /// <summary>
  /// Отыскать закладку по умолчанию для указанного типа объекта (или ближайшего по иерархии)
  /// </summary>
  /// <param name="objectTypeID">Тип объекта</param>
  /// <returns></returns>
  public static AdjustableView GetDefaultView(int objectTypeID)
  {
    lock (AdjustableViewsHelper.DefaultViews)
    {
      if (AdjustableViewsHelper.DefaultViews.Count == 0)
        return (AdjustableView) null;
    }
    if (MetaDataHelper.GetObjectType(objectTypeID) == null)
      return (AdjustableView) null;
    lock (AdjustableViewsHelper.DefaultViews)
    {
      if (AdjustableViewsHelper.DefaultViews.ContainsKey(objectTypeID))
        return AdjustableViewsHelper.DefaultViews[objectTypeID];
      Tuple<int, int> tuple = new Tuple<int, int>(-1, -1);
      foreach (int key in AdjustableViewsHelper.DefaultViews.Keys)
      {
        if (MetaDataHelper.IsObjectTypeChildOf(objectTypeID, key) && tuple.Item1 != key)
        {
          int objectTypeLevel = MetaDataHelper.GetObjectTypeLevel(key);
          if (tuple.Item2 < objectTypeLevel)
            tuple = new Tuple<int, int>(key, objectTypeLevel);
        }
      }
      return AdjustableViewsHelper.DefaultViews.ContainsKey(tuple.Item1) ? AdjustableViewsHelper.DefaultViews[tuple.Item1] : (AdjustableView) null;
    }
  }

  /// <summary>
  /// Добавить новую настраиваемую закладку ("вьюшку") "Навигатора" в коллекцию
  /// </summary>
  /// <param name="name">Уникальное в пределах всей системы имя закладки.
  /// ВНИМАНИЕ! Если имя закладки начинается с символа "@", то под фильтрацию попадут
  /// все закладки, имя которых будет начинаться с указанной строки (с учётом регистра, пробелов, т.п.).
  /// Это можно использовать в тех случаях, когда имя закладки генерируется автоматически.</param>
  /// <param name="caption">Краткое текстовое название заладки</param>
  /// <param name="hint">Более подробное текстовое описание закладки</param>
  /// <param name="module">Название модуля (плагина), который создаёт закладку</param>
  /// <param name="imageName">Название значка закладки (из коллекции именованных значков)</param>
  /// <param name="visible">Флажок позволяет прятать или показывать данную закладку на панелях "Навигатора"</param>
  /// <param name="orderID">Порядковый номер закладки на менеджере закладок "Навигатора"</param>
  /// <returns>Ссылка на новую настраиваемую закладку</returns>
  public static AdjustableView RegisterView(
    string name,
    string caption,
    string hint,
    string module,
    string imageName,
    bool visible,
    int orderID)
  {
    if (name == null || caption == null || hint == null || module == null || imageName == null)
      throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Client_150"));
    AdjustableView adjustableView = new AdjustableView(name, caption, visible, hint, module, imageName, orderID);
    lock (AdjustableViewsHelper._deafultAdjustableViews)
    {
      if (AdjustableViewsHelper._deafultAdjustableViews.FindView(name) == null)
        AdjustableViewsHelper._deafultAdjustableViews.Add((AdjustableView) adjustableView.Clone());
    }
    if (!(ServicesManager.GetService(typeof (AdjustableViews)) is AdjustableViews service))
      return (AdjustableView) null;
    AdjustableView view = service.FindView(name);
    if (view != null)
      return view;
    service.Add(adjustableView);
    return adjustableView;
  }

  public static AdjustableViews GetDefaultAdjustableViews()
  {
    lock (AdjustableViewsHelper._deafultAdjustableViews)
      return (AdjustableViews) AdjustableViewsHelper._deafultAdjustableViews.Clone();
  }
}
