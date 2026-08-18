// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ElectricalTypesHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Набор методов для определения типа схемы</summary>
public static class ElectricalTypesHelper
{
  private static List<int> _elementListTypeIDs;

  /// <summary>
  ///  Ищет среди настроек тип, у которого суффикс равен искомому и если находит - возвращает его Guid, если нет - возвращает Guid.Empty.
  /// </summary>
  /// <param name="settings"></param>
  /// <param name="suffix"></param>
  /// <returns></returns>
  public static Guid GetSchemaType(List<Tuple<Guid, string>> settings, string suffix)
  {
    Tuple<Guid, string> tuple = settings.Find((Predicate<Tuple<Guid, string>>) (current => current.Item2.Equals(suffix)));
    return tuple == null ? Guid.Empty : tuple.Item1;
  }

  /// <summary>
  /// Получить суфикс из обозначения который идентифицирует тип схемы
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public static string GetSuffix(string designation)
  {
    return designation != null && designation != string.Empty ? new Regex("(?<suffix>Э\\d{1})[\\.\\d]*$").Match(designation).Groups["suffix"].Value : string.Empty;
  }

  /// <summary>
  /// Определить тип создаваемого перечня по суфиксу в обозначении схемы
  /// </summary>
  /// <param name="suffix"></param>
  /// <returns></returns>
  public static Guid GetElementListType(string suffix)
  {
    switch (suffix.ToUpper())
    {
      case "Э0":
        return ElectricalGuids.elementList0;
      case "Э1":
        return ElectricalGuids.elementList1;
      case "Э2":
        return ElectricalGuids.elementList2;
      case "Э3":
        return ElectricalGuids.elementList3;
      case "Э4":
        return ElectricalGuids.elementList4;
      case "Э5":
        return ElectricalGuids.elementList5;
      case "Э6":
        return ElectricalGuids.elementList6;
      case "Э7":
        return ElectricalGuids.elementList7;
      default:
        return Guid.Empty;
    }
  }

  public static List<int> ElementListTypeIDs
  {
    get
    {
      if (ElectricalTypesHelper._elementListTypeIDs == null)
      {
        ElectricalTypesHelper._elementListTypeIDs = new List<int>();
        ElectricalTypesHelper._elementListTypeIDs.Add(MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList0));
        ElectricalTypesHelper._elementListTypeIDs.Add(MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList1));
        ElectricalTypesHelper._elementListTypeIDs.Add(MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList2));
        ElectricalTypesHelper._elementListTypeIDs.Add(MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList3));
        ElectricalTypesHelper._elementListTypeIDs.Add(MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList4));
        ElectricalTypesHelper._elementListTypeIDs.Add(MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList5));
        ElectricalTypesHelper._elementListTypeIDs.Add(MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList6));
        ElectricalTypesHelper._elementListTypeIDs.Add(MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList7));
      }
      return ElectricalTypesHelper._elementListTypeIDs;
    }
  }

  /// <summary>
  /// Функция пользовательского определения типа перечня элементов
  /// </summary>
  /// <param name="createdElementListType">Глобальный идентификатор типа ПЭ</param>
  /// <returns>Определил пользователь тип или отказался</returns>
  public static bool SelectElementListType(
    ref Guid createdElementListType,
    string designation,
    string name)
  {
    if (IMMessageBox.Show("Внимание", $"Невозможно определить тип перечня элементов{(string.IsNullOrEmpty(designation) ? string.Empty : " для " + (string.IsNullOrEmpty(name) ? designation : $"{designation} {name}"))}. Выбрать тип вручную или отменить создание ПЭ?", new IMMessageBoxButton[2]
    {
      new IMMessageBoxButton("Выбор типа", DialogResult.Yes),
      new IMMessageBoxButton("Отмена создания", DialogResult.No)
    }, IMMessageBoxImage.Question) == DialogResult.Yes)
    {
      SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Типы объектов", typeof (ObjectTypeFolder), false);
      selectorForm.ExpandLevelsOnLoad = 4;
      selectorForm.SelectorFilter = (ISelectorFilter) new ElectricalSchemaElementListTypesFilter();
      if (selectorForm.ShowDialog() == DialogResult.OK && selectorForm.IDList.Count == 1)
      {
        createdElementListType = MetaDataHelper.GetObjectTypeGuid((int) selectorForm.IDList[0]);
        return true;
      }
    }
    return false;
  }
}
