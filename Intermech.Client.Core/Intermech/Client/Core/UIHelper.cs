
// Type: Intermech.Client.Core.UIHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.PropertyEditors;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary> Методы, облегчающие разработку пользовательсткого интерфейса </summary>
public static class UIHelper
{
  /// <summary> Получение иконки типа объекта </summary>
  /// <param name="objTypeID"> Идентификатор типа объекта </param>
  /// <returns> Иконка типа объекта. Может вернуть null !!! </returns>
  public static Icon GetObjTypeIcon(int objTypeID)
  {
    if (objTypeID != 0)
    {
      if (objTypeID != -1)
      {
        try
        {
          return Holder.IconService?.GetIcon(4, objTypeID);
        }
        catch
        {
          return (Icon) null;
        }
      }
    }
    return (Icon) null;
  }

  /// <summary> Получение иконки типа связи </summary>
  /// <param name="relTypeID"> Идентификатор типа связи </param>
  /// <returns> Иконка типа объекта. Может вернуть null !!! </returns>
  public static Icon GetRelTypeIcon(int relTypeID)
  {
    if (relTypeID == -1)
      return (Icon) null;
    try
    {
      return Holder.IconService?.GetIcon(6, relTypeID);
    }
    catch
    {
      return (Icon) null;
    }
  }

  /// <summary> Получение иконки типа объекта </summary>
  /// <param name="relTypeGuid"> Guid типа объекта </param>
  /// <returns> Иконка типа объекта. Может вернуть null !!! </returns>
  public static Icon GetRelTypeIcon(Guid relTypeGuid)
  {
    return UIHelper.GetRelTypeIcon(DBHelper.GetRelTypeIDByGuid(relTypeGuid));
  }

  /// <summary> Получение иконки типа объекта </summary>
  /// <param name="objTypeGuid"> Guid типа объекта </param>
  /// <returns> Иконка типа объекта. Может вернуть null !!! </returns>
  public static Icon GetObjTypeIcon(Guid objTypeGuid)
  {
    return UIHelper.GetObjTypeIcon(DBHelper.GetObjTypeIDByGuid(objTypeGuid));
  }

  /// <summary> Получение иконки экземпляра объекта </summary>
  /// <param name="objID"> Идентификатор объекта </param>
  /// <returns> Иконка экземпляра объекта. Может вернуть null !!! </returns>
  public static Icon GetObjIcon(long objID)
  {
    int objTypeId = DBHelper.GetObjTypeID(objID);
    switch (objTypeId)
    {
      case -1:
      case 0:
        return (Icon) null;
      default:
        return UIHelper.GetObjTypeIcon(objTypeId);
    }
  }

  /// <summary> Получение иконки экземпляра объекта </summary>
  /// <param name="objGuid"> Guid объекта </param>
  /// <returns> Иконка экземпляра объекта. Может вернуть null !!! </returns>
  public static Icon GetObjIcon(Guid objGuid)
  {
    int objTypeId = DBHelper.GetObjTypeID(objGuid);
    switch (objTypeId)
    {
      case -1:
      case 0:
        return (Icon) null;
      default:
        return UIHelper.GetObjTypeIcon(objTypeId);
    }
  }

  /// <summary> Вызвать диалог выбора любого атрибута </summary>
  /// <returns> Идентификатор выбраного атрибута. -1 если ничего не было выбрано </returns>
  public static int SelectAttributeInTotalList()
  {
    SelectorForm selectorForm = new SelectorForm(typeof (AttributesFolder), LocalizationHolder.rm.GetString("Client.Core_54"), typeof (AttributeFolder), false);
    selectorForm.TopMost = true;
    return selectorForm.ShowDialog() == DialogResult.OK && selectorForm.IDList.Count > 0 ? (int) selectorForm.IDList[0] : -1;
  }

  /// <summary> Вызвать диалог выбора любого атрибута (MultiSelect) </summary>
  /// <returns> Список идентификаторов выбранных атрибутов. Возвращает null, если операция выбора была отменена. </returns>
  public static IntList SelectAttributesInTotalList()
  {
    SelectorForm selectorForm = new SelectorForm(typeof (AttributesFolder), LocalizationHolder.rm.GetString("Client.Core_54"), typeof (AttributeFolder), true);
    selectorForm.TopMost = true;
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return (IntList) null;
    IntList intList = new IntList(selectorForm.IDList.Count);
    foreach (int id in selectorForm.IDList)
      intList.Add((object) id);
    return intList;
  }

  /// <summary> Вызвать диалог выбора нескольких атрибутов, принадлежащего связи или типу объекта </summary>
  /// <param name="coreObjType"> Идентификатор типа объекта от которого должны накладываться связи </param>
  /// <param name="scanRelationIDs">
  /// Список идентификаторов типов связей параметры которых должны попасть в общий список.
  /// Связи сканируются на корректность и из этого списка удаляются те из них,
  /// которые не могут исходить из объекта типа coreObjType в типы объектов scanObjTypeIDs.
  /// </param>
  /// <param name="scanObjTypeIDs"> Список идентификаторов типов объектов </param>
  /// <returns> Список идентификаторов выбранных атрибутов. Null, если операция выбора была отменена </returns>
  public static IntList SelectAttributesOfRelationTypesAndObjectTypes(
    int coreObjType,
    IntList scanRelationIDs,
    IntList scanObjTypeIDs)
  {
    return UIHelper.SelectAttributesInternal(coreObjType, scanRelationIDs, scanObjTypeIDs, false, true);
  }

  /// <summary> Вызвать диалог выбора нескольких атрибутов, принадлежащего связи или типу объекта </summary>
  /// <param name="coreObjType"> Идентификатор типа объекта от которого должны накладываться связи </param>
  /// <param name="scanRelationIDs">
  /// Список идентификаторов типов связей параметры которых должны попасть в общий список.
  /// Связи сканируются на корректность и из этого списка удаляются те из них,
  /// которые не могут исходить из объекта типа coreObjType в типы объектов scanObjTypeIDs.
  /// </param>
  /// <param name="scanObjTypeIDs"> Список идентификаторов типов объектов </param>
  /// <param name="showAllAttributesButton"> Давать ли возможность выбирать атрибуты не попавшие в основной список (открывать список всех атрибутов) </param>
  /// <returns> Список идентификаторов выбранных атрибутов. Null, если операция выбора была отменена </returns>
  public static IntList SelectAttributesOfRelationTypesAndObjectTypes(
    int coreObjType,
    IntList scanRelationIDs,
    IntList scanObjTypeIDs,
    bool showAllAttributesButton)
  {
    return UIHelper.SelectAttributesInternal(coreObjType, scanRelationIDs, scanObjTypeIDs, showAllAttributesButton, true);
  }

  /// <summary> Вызвать диалог выбора нескольких атрибутов, принадлежащего связи или типу объекта </summary>
  /// <param name="scanRelationIDs"> Список идентификаторов типов связей параметры которых должны попасть в общий список. </param>
  /// <param name="scanObjTypeIDs"> Список идентификаторов типов объектов </param>
  /// <returns> Список идентификаторов выбранных атрибутов. Null, если операция выбора была отменена </returns>
  public static IntList SelectAttributesOfRelationTypesAndObjectTypes(
    IntList scanRelationIDs,
    IntList scanObjTypeIDs)
  {
    return UIHelper.SelectAttributesInternal(-1, scanRelationIDs, scanObjTypeIDs, false, true);
  }

  /// <summary> Вызвать диалог выбора нескольких атрибутов, принадлежащего связи или типу объекта </summary>
  /// <param name="scanRelationIDs"> Список идентификаторов типов связей параметры которых должны попасть в общий список. </param>
  /// <param name="scanObjTypeIDs"> Список идентификаторов типов объектов </param>
  /// <param name="showAllAttributesButton"> Давать ли возможность выбирать атрибуты не попавшие в основной список (открывать список всех атрибутов) </param>
  /// <returns> Список идентификаторов выбранных атрибутов. Null, если операция выбора была отменена </returns>
  public static IntList SelectAttributesOfRelationTypesAndObjectTypes(
    IntList scanRelationIDs,
    IntList scanObjTypeIDs,
    bool showAllAttributesButton)
  {
    return UIHelper.SelectAttributesInternal(-1, scanRelationIDs, scanObjTypeIDs, showAllAttributesButton, true);
  }

  /// <summary> Вызвать диалог выбора нескольких атрибутов, принадлежащего связи или типу объекта </summary>
  /// <param name="scanRelationID"> Идентификатор типа связей параметры которых должны попасть в общий список </param>
  /// <param name="scanObjTypeID"> Идентификатор типа объекта параметры которого должны попасть в общий список </param>
  /// <returns> Список идентификаторов выбранных атрибутов. Null, если операция выбора была отменена </returns>
  public static IntList SelectAttributesOfRelationTypeAndObjectType(
    int scanRelationID,
    int scanObjTypeID)
  {
    return UIHelper.SelectAttributesOfRelationTypeAndObjectType(scanRelationID, scanObjTypeID, false);
  }

  /// <summary> Вызвать диалог выбора нескольких атрибутов, принадлежащего связи или типу объекта </summary>
  /// <param name="scanRelationID"> Идентификатор типа связей параметры которых должны попасть в общий список </param>
  /// <param name="scanObjTypeID"> Идентификатор типа объекта параметры которого должны попасть в общий список </param>
  /// <param name="showAllAttributesButton"> Давать ли возможность выбирать атрибуты не попавшие в основной список (открывать список всех атрибутов) </param>
  /// <returns> Список идентификаторов выбранных атрибутов. Null, если операция выбора была отменена </returns>
  public static IntList SelectAttributesOfRelationTypeAndObjectType(
    int scanRelationID,
    int scanObjTypeID,
    bool showAllAttributesButton)
  {
    IntList scanRelationIDs = (IntList) null;
    if (scanRelationID != -1)
    {
      scanRelationIDs = new IntList(1);
      scanRelationIDs.Add((object) scanRelationID);
    }
    IntList scanObjTypeIDs = (IntList) null;
    if (scanObjTypeID != -1)
    {
      scanObjTypeIDs = new IntList(1);
      scanObjTypeIDs.Add((object) scanObjTypeID);
    }
    return UIHelper.SelectAttributesInternal(-1, scanRelationIDs, scanObjTypeIDs, showAllAttributesButton, true);
  }

  /// <summary> Вызвать диалог выбора нескольких атрибутов, принадлежащих связи </summary>
  /// <param name="scanRelationID"> Идентификатор типа связей параметры которых должны попасть в общий список </param>
  /// <returns> Список идентификаторов выбранных атрибутов. Null, если операция выбора была отменена </returns>
  public static IntList SelectAttributesOfRelationType(int scanRelationID)
  {
    return UIHelper.SelectAttributesOfRelationType(scanRelationID, false);
  }

  /// <summary> Вызвать диалог выбора нескольких атрибутов, принадлежащего связи </summary>
  /// <param name="scanRelationID"> Идентификатор типа связей параметры которых должны попасть в общий список </param>
  /// <param name="showAllAttributesButton"> Давать ли возможность выбирать атрибуты не попавшие в основной список (открывать список всех атрибутов) </param>
  /// <returns> Список идентификаторов выбранных атрибутов. Null, если операция выбора была отменена </returns>
  public static IntList SelectAttributesOfRelationType(
    int scanRelationID,
    bool showAllAttributesButton)
  {
    return UIHelper.SelectAttributesOfRelationTypeAndObjectType(scanRelationID, -1, showAllAttributesButton);
  }

  /// <summary> Вызвать диалог выбора нескольких атрибутов, принадлежащих типу объекта </summary>
  /// <param name="scanObjTypeID"> Идентификатор типа объекта параметры которого должны попасть в общий список </param>
  /// <returns> Список идентификаторов выбранных атрибутов. Null, если операция выбора была отменена </returns>
  public static IntList SelectAttributesOfObjectType(int scanObjTypeID)
  {
    return UIHelper.SelectAttributesOfObjectType(scanObjTypeID, false);
  }

  /// <summary> Вызвать диалог выбора нескольких атрибутов, принадлежащего типу объекта </summary>
  /// <param name="scanObjTypeID"> Идентификатор типа объекта параметры которого должны попасть в общий список </param>
  /// <param name="showAllAttributesButton"> Давать ли возможность выбирать атрибуты не попавшие в основной список (открывать список всех атрибутов) </param>
  /// <returns> Список идентификаторов выбранных атрибутов. Null, если операция выбора была отменена </returns>
  public static IntList SelectAttributesOfObjectType(
    int scanObjTypeID,
    bool showAllAttributesButton)
  {
    return UIHelper.SelectAttributesOfRelationTypeAndObjectType(-1, scanObjTypeID, showAllAttributesButton);
  }

  /// <summary> Вызвать диалог выбора атрибута, принадлежащего связи или типу объекта </summary>
  /// <param name="scanRelationID"> Идентификатор типа связей параметры которых должны попасть в общий список </param>
  /// <param name="scanObjTypeID"> Идентификатор типа объекта параметры которого должны попасть в общий список </param>
  /// <returns> Идентификатор выбраного атрибутов. -1, если операция выбора была отменена </returns>
  public static int SelectAttributeOfRelationTypeAndObjectType(
    int scanRelationID,
    int scanObjTypeID)
  {
    return UIHelper.SelectAttributeOfRelationTypeAndObjectType(scanRelationID, scanObjTypeID, false);
  }

  /// <summary> Вызвать диалог выбора атрибутf, принадлежащего связи или типу объекта </summary>
  /// <param name="scanRelationID"> Идентификатор типа связей параметры которых должны попасть в общий список </param>
  /// <param name="scanObjTypeID"> Идентификатор типа объекта параметры которого должны попасть в общий список </param>
  /// <param name="showAllAttributesButton"> Давать ли возможность выбирать атрибуты не попавшие в основной список (открывать список всех атрибутов) </param>
  /// <returns> Идентификатор выбраного атрибутов. -1, если операция выбора была отменена </returns>
  public static int SelectAttributeOfRelationTypeAndObjectType(
    int scanRelationID,
    int scanObjTypeID,
    bool showAllAttributesButton)
  {
    IntList scanRelationIDs = (IntList) null;
    if (scanRelationID != -1)
    {
      scanRelationIDs = new IntList(1);
      scanRelationIDs.Add((object) scanRelationID);
    }
    IntList scanObjTypeIDs = (IntList) null;
    if (scanObjTypeID != -1)
    {
      scanObjTypeIDs = new IntList(1);
      scanObjTypeIDs.Add((object) scanObjTypeID);
    }
    return UIHelper.SelectAttributeOfRelationTypesAndObjectTypes(-1, scanRelationIDs, scanObjTypeIDs, showAllAttributesButton);
  }

  /// <summary> Вызвать диалог выбора атрибута, принадлежащего типу объекта </summary>
  /// <param name="scanObjTypeID"> Идентификатор типа объекта параметры которого должны попасть в общий список </param>
  /// <returns> Идентификатор выбраного атрибутов. -1, если операция выбора была отменена </returns>
  public static int SelectAttributeOfObjectType(int scanObjTypeID)
  {
    return UIHelper.SelectAttributeOfRelationTypeAndObjectType(-1, scanObjTypeID);
  }

  /// <summary> Вызвать диалог выбора атрибутf, принадлежащего типу объекта </summary>
  /// <param name="scanObjTypeID"> Идентификатор типа объекта параметры которого должны попасть в общий список </param>
  /// <param name="showAllAttributesButton"> Давать ли возможность выбирать атрибуты не попавшие в основной список (открывать список всех атрибутов) </param>
  /// <returns> Идентификатор выбраного атрибутов. -1, если операция выбора была отменена </returns>
  public static int SelectAttributeOfObjectType(int scanObjTypeID, bool showAllAttributesButton)
  {
    return UIHelper.SelectAttributeOfRelationTypeAndObjectType(-1, scanObjTypeID, showAllAttributesButton);
  }

  /// <summary> Вызвать диалог выбора атрибута, принадлежащего связи </summary>
  /// <param name="scanRelationID"> Идентификатор типа связей параметры которых должны попасть в общий список </param>
  /// <returns> Идентификатор выбраного атрибутов. -1, если операция выбора была отменена </returns>
  public static int SelectAttributeOfRelationType(int scanRelationID)
  {
    return UIHelper.SelectAttributeOfRelationTypeAndObjectType(scanRelationID, -1);
  }

  /// <summary> Вызвать диалог выбора нескольких атрибутов, принадлежащего связи </summary>
  /// <param name="scanRelationID"> Идентификатор типа связей параметры которых должны попасть в общий список </param>
  /// <param name="showAllAttributesButton"> Давать ли возможность выбирать атрибуты не попавшие в основной список (открывать список всех атрибутов) </param>
  /// <returns> Идентификатор выбраного атрибутов. -1, если операция выбора была отменена </returns>
  public static int SelectAttributeOfRelationType(int scanRelationID, bool showAllAttributesButton)
  {
    return UIHelper.SelectAttributeOfRelationTypeAndObjectType(scanRelationID, -1, showAllAttributesButton);
  }

  /// <summary> Вызвать диалог выбора нескольких любого из атрибутов, принадлежащего связи или типу объекта </summary>
  /// <param name="scanRelationIDs"> Список идентификаторов типов связей параметры которых должны попасть в общий список. </param>
  /// <param name="scanObjTypeIDs"> Список идентификаторов типов объектов </param>
  /// <param name="showAllAttributesButton"> Давать ли возможность выбирать атрибуты не попавшие в основной список (открывать список всех атрибутов) </param>
  /// <returns> Идентификатор выбраного атрибута. -1, если операция выбора была отменена </returns>
  public static int SelectAttributeOfRelationTypesAndObjectTypes(
    IntList scanRelationIDs,
    IntList scanObjTypeIDs,
    bool showAllAttributesButton)
  {
    return UIHelper.SelectAttributeOfRelationTypesAndObjectTypes(-1, scanRelationIDs, scanObjTypeIDs, false);
  }

  /// <summary> Вызвать диалог выбора нескольких любого из атрибутов, принадлежащего связи или типу объекта </summary>
  /// <param name="scanRelationIDs"> Список идентификаторов типов связей параметры которых должны попасть в общий список. </param>
  /// <param name="scanObjTypeIDs"> Список идентификаторов типов объектов </param>
  /// <returns> Идентификатор выбраного атрибута. -1, если операция выбора была отменена </returns>
  public static int SelectAttributeOfRelationTypesAndObjectTypes(
    IntList scanRelationIDs,
    IntList scanObjTypeIDs)
  {
    return UIHelper.SelectAttributeOfRelationTypesAndObjectTypes(scanRelationIDs, scanObjTypeIDs, false);
  }

  /// <summary> Вызвать диалог выбора нескольких любого из атрибутов, принадлежащего связи или типу объекта </summary>
  /// <param name="coreObjType"> Идентификатор типа объекта от которого должны накладываться связи </param>
  /// <param name="scanRelationIDs">
  /// Список идентификаторов типов связей параметры которых должны попасть в общий список.
  /// Связи сканируются на корректность и из этого списка удаляются те из них,
  /// которые не могут исходить из объекта типа coreObjType в типы объектов scanObjTypeIDs.
  /// </param>
  /// <param name="scanObjTypeIDs"> Список идентификаторов типов объектов </param>
  /// <param name="showAllAttributesButton"> Давать ли возможность выбирать атрибуты не попавшие в основной список (открывать список всех атрибутов) </param>
  /// <returns> Идентификатор выбраного атрибута. -1, если операция выбора была отменена </returns>
  public static int SelectAttributeOfRelationTypesAndObjectTypes(
    int coreObjType,
    IntList scanRelationIDs,
    IntList scanObjTypeIDs,
    bool showAllAttributesButton)
  {
    IntList intList = UIHelper.SelectAttributesInternal(coreObjType, scanRelationIDs, scanObjTypeIDs, showAllAttributesButton, false);
    return intList != null && intList.Count > 0 ? intList[0] : -1;
  }

  /// <summary> Вызвать диалог выбора нескольких любого из атрибутов, принадлежащего связи или типу объекта </summary>
  /// <param name="coreObjType"> Идентификатор типа объекта от которого должны накладываться связи </param>
  /// <param name="scanRelationIDs">
  /// Список идентификаторов типов связей параметры которых должны попасть в общий список.
  /// Связи сканируются на корректность и из этого списка удаляются те из них,
  /// которые не могут исходить из объекта типа coreObjType в типы объектов scanObjTypeIDs.
  /// </param>
  /// <param name="scanObjTypeIDs"> Список идентификаторов типов объектов </param>
  /// <returns> Идентификатор выбраного атрибута. -1, если операция выбора была отменена </returns>
  public static int SelectAttributeOfRelationTypesAndObjectTypes(
    int coreObjType,
    IntList scanRelationIDs,
    IntList scanObjTypeIDs)
  {
    return UIHelper.SelectAttributeOfRelationTypesAndObjectTypes(coreObjType, scanRelationIDs, scanObjTypeIDs, false);
  }

  /// <summary> Вызвать диалог выбора нескольких атрибутов, принадлежащего связи или типу объекта </summary>
  /// <param name="coreObjType"> Идентификатор типа объекта от которого должны накладываться связи </param>
  /// <param name="scanRelationIDs">
  /// Список идентификаторов типов связей параметры которых должны попасть в общий список.
  /// Связи сканируются на корректность и из этого списка удаляются те из них,
  /// которые не могут исходить из объекта типа coreObjType в типы объектов scanObjTypeIDs.
  /// </param>
  /// <param name="scanObjTypeIDs"> Список идентификаторов типов объектов </param>
  /// <param name="showAllAttributesButton"> Давать ли возможность выбирать атрибуты не попавшие в основной список (открывать список всех атрибутов) </param>
  /// <param name="multiSelectAllAttributesButton"> Давать ли возможность выбирать сразу несколько атрибутов </param>
  /// <returns> Список идентификаторов выбранных атрибутов. Null, если операция выбора была отменена </returns>
  private static IntList SelectAttributesInternal(
    int coreObjType,
    IntList scanRelationIDs,
    IntList scanObjTypeIDs,
    bool showAllAttributesButton,
    bool multiSelectAllAttributesButton)
  {
    FormSelectAttribute formSelectAttribute = new FormSelectAttribute();
    formSelectAttribute.ShowAllAttributesButton = showAllAttributesButton;
    formSelectAttribute.MultiSelect = true;
    formSelectAttribute.CoreObjType = coreObjType;
    if (scanObjTypeIDs != null)
      formSelectAttribute.ScanObjTypeIDs = scanObjTypeIDs;
    if (scanRelationIDs != null)
      formSelectAttribute.ScanRelationIDs = scanRelationIDs;
    formSelectAttribute.RefreshAttributesList();
    return formSelectAttribute.ShowDialog() == DialogResult.OK ? formSelectAttribute.SelectedAttributeIDs : (IntList) null;
  }

  /// <summary> Проверка, является ли контрол видимым. Учитывается видимость контролов, внутри которых лежит переданый </summary>
  /// <param name="control"> Контрол, видимость которого требуется узнать </param>
  /// <returns> True, если контрол видим </returns>
  public static bool IsVisible(Control control)
  {
    if (control == null || !control.Visible)
      return false;
    Control parent = control.Parent;
    while (parent.Parent != null)
    {
      parent = parent.Parent;
      if (!parent.Visible)
        return false;
    }
    return true;
  }

  /// <summary> Проверка, является ли контрол "включённым" (enabled). Учитывается "включённость" контролов, внутри которых лежит переданый </summary>
  /// <param name="control"> Контрол, "включённость" которого требуется узнать </param>
  /// <returns> True, если контрол "включён" </returns>
  public static bool IsEnabled(Control control)
  {
    if (control == null || !control.Enabled)
      return false;
    Control control1 = (Control) null;
    while (control1.Parent != null)
    {
      control1 = control1.Parent;
      if (!control1.Enabled)
        return false;
    }
    return true;
  }
}
