
// Type: Intermech.Techcard.TechConsts
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Techcard;

/// <summary>Helper для работы с техкардом. Иногда требуется с ним работать без reference-ов на либы техкарда.
/// Например работа с технологическим составом в IMProject (см. 1219161 в BugBase)</summary>
public static class TechConsts
{
  /// <summary>Тип объектов "Маршрут обработки"</summary>
  public static Guid ObjType_ProcessingRoute_GUID = new Guid("cad0016f-306c-11d8-b4e9-00304f19f545");
  private static int _objType_ProcessingRoute_ID;
  /// <summary>Массив идентификаторов типов объектов "Маршрут обработки", включая унаследованные от него</summary>
  [CanBeNull]
  private static List<int> _objType_ProcessingRoute_IDs;
  /// <summary>Тип объектов "Техпроцесс базовый"</summary>
  public static Guid ObjType_TechProcBase_GUID = new Guid("cad00185-306c-11d8-b4e9-00304f19f545");
  private static int _objType_TechProcBase_ID;
  /// <summary>Тип объектов "Технологический объект"</summary>
  public static Guid ObjType_TechBaseObject_GUID = new Guid("cad00163-306c-11d8-b4e9-00304f19f545");
  private static int _objType_TechBaseObject_ID;
  public static Guid ObjType_TechRoute_GUID = new Guid("cad001e5-306c-11d8-b4e9-00304f19f545");
  private static int _objType_TechRoute_ID;
  /// <summary>Массив идентификаторов типов объектов "Расцеховочный маршрут", включая унаследованные от него</summary>
  [CanBeNull]
  private static List<int> _objType_TechRoute_IDs;
  [CanBeNull]
  private static HashSet<int> _objType_TechRoute_IDs_Hash;
  public static Guid ObjType_TechRouteTemplate_GUID = new Guid("cad001fd-306c-11d8-b4e9-00304f19f545");
  private static int _objType_TechRouteTemplate_ID;
  /// <summary>Массив идентификаторов типов объектов "Шаблон расцеховки базовый", включая унаследованные от него</summary>
  [CanBeNull]
  private static List<int> _objType_TechRouteTemplate_IDs;
  [CanBeNull]
  private static HashSet<int> _objType_TechRouteTemplate_IDs_Hash;
  public static Guid ObjType_TechRouteElement_GUID = new Guid("cad001e8-306c-11d8-b4e9-00304f19f545");
  private static int _objType_TechRouteElement_ID;
  /// <summary>Массив идентификаторов типов объектов "Расцеховочный элемент", включая унаследованные от него</summary>
  [CanBeNull]
  private static List<int> _objType_TechRouteElement_IDs;
  [CanBeNull]
  private static HashSet<int> _objType_TechRouteElement_IDs_Hash;
  /// <summary>Тип связи "Технологический состав"</summary>
  public static Guid RelType_TechComposition_GUID = new Guid("cad0019f-306c-11d8-b4e9-00304f19f545");
  private static int _relType_TechComposition_ID;
  /// <summary>Тип атрибута "Маршрут обработки по умолчанию"</summary>
  public static Guid Attr_IsDefaultProcessingRoute_GUID = new Guid("cad005b9-306c-11d8-b4e9-00304f19f545");
  private static int _attr_IsDefaultProcessingRoute_ID;
  /// <summary>Тип атрибута "Сортировка"</summary>
  public static Guid Attr_Sort_GUID = new Guid("cad00202-306c-11d8-b4e9-00304f19f545");
  private static int _attr_Sort_ID;
  private static bool _wasValidated;
  private static bool _validateResult;
  [CanBeNull]
  private static string _lastErrorText;

  /// <summary>Тип объектов "Маршрут обработки"</summary>
  public static int ObjType_ProcessingRoute_ID
  {
    get => TechConsts.GetID<int>(ref TechConsts._objType_ProcessingRoute_ID);
  }

  [NotNull]
  public static List<int> ObjType_ProcessingRoute_IDs
  {
    get
    {
      return TechConsts._objType_ProcessingRoute_IDs ?? (TechConsts._objType_ProcessingRoute_IDs = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechConsts.ObjType_ProcessingRoute_ID).AsList<int>());
    }
  }

  /// <summary>Тип объектов "Техпроцесс базовый"</summary>
  public static int ObjType_TechProcBase_ID
  {
    get => TechConsts.GetID<int>(ref TechConsts._objType_TechProcBase_ID);
  }

  /// <summary>Тип объектов "Технологический объект"</summary>
  public static int ObjType_TechBaseObject_ID
  {
    get => TechConsts.GetID<int>(ref TechConsts._objType_TechBaseObject_ID);
  }

  public static int ObjType_TechRoute_ID
  {
    get => TechConsts.GetID<int>(ref TechConsts._objType_TechRoute_ID);
  }

  [NotNull]
  public static List<int> ObjType_TechRoute_IDs
  {
    get
    {
      return TechConsts._objType_TechRoute_IDs ?? (TechConsts._objType_TechRoute_IDs = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechConsts.ObjType_TechRoute_ID).AsList<int>());
    }
  }

  /// <summary>Проверка является ли тип шаблоном расцеховки (включая дочерние типы)</summary>
  public static bool TypeIsTechRoute(int objType)
  {
    if (objType == TechConsts.ObjType_TechRoute_ID)
      return true;
    if (TechConsts._objType_TechRoute_IDs_Hash == null)
      TechConsts._objType_TechRoute_IDs_Hash = new HashSet<int>((IEnumerable<int>) TechConsts.ObjType_TechRoute_IDs);
    return TechConsts._objType_TechRoute_IDs_Hash.Contains(objType);
  }

  public static int ObjType_TechRouteTemplate_ID
  {
    get => TechConsts.GetID<int>(ref TechConsts._objType_TechRouteTemplate_ID);
  }

  [NotNull]
  public static List<int> ObjType_TechRouteTemplate_IDs
  {
    get
    {
      return TechConsts._objType_TechRouteTemplate_IDs ?? (TechConsts._objType_TechRouteTemplate_IDs = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechConsts.ObjType_TechRouteTemplate_ID).AsList<int>());
    }
  }

  /// <summary>Проверка является ли тип шаблоном расцеховки (включая дочерние типы)</summary>
  public static bool TypeIsTechRouteTemplate(int objType)
  {
    if (objType == TechConsts.ObjType_TechRouteTemplate_ID)
      return true;
    if (TechConsts._objType_TechRouteTemplate_IDs_Hash == null)
      TechConsts._objType_TechRouteTemplate_IDs_Hash = new HashSet<int>((IEnumerable<int>) TechConsts.ObjType_TechRouteTemplate_IDs);
    return TechConsts._objType_TechRouteTemplate_IDs_Hash.Contains(objType);
  }

  public static int ObjType_TechRouteElement_ID
  {
    get => TechConsts.GetID<int>(ref TechConsts._objType_TechRouteElement_ID);
  }

  [NotNull]
  public static List<int> ObjType_TechRouteElement_IDs
  {
    get
    {
      return TechConsts._objType_TechRouteElement_IDs ?? (TechConsts._objType_TechRouteElement_IDs = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechConsts.ObjType_TechRouteElement_ID).AsList<int>());
    }
  }

  /// <summary>Проверка является ли тип расцеховочным элементом (включая дочерние типы)</summary>
  public static bool TypeIsTechRouteElement(int objType)
  {
    if (objType == TechConsts.ObjType_TechRouteElement_ID)
      return true;
    if (TechConsts._objType_TechRouteElement_IDs_Hash == null)
      TechConsts._objType_TechRouteElement_IDs_Hash = new HashSet<int>((IEnumerable<int>) TechConsts.ObjType_TechRouteElement_IDs);
    return TechConsts._objType_TechRouteElement_IDs_Hash.Contains(objType);
  }

  /// <summary>Тип связи "Технологический состав"</summary>
  public static int RelType_TechComposition_ID
  {
    get => TechConsts.GetID<int>(ref TechConsts._relType_TechComposition_ID);
  }

  /// <summary>Тип атрибута "Маршрут обработки по умолчанию"</summary>
  public static int Attr_IsDefaultProcessingRoute_ID
  {
    get => TechConsts.GetID<int>(ref TechConsts._attr_IsDefaultProcessingRoute_ID);
  }

  /// <summary>Тип атрибута "Сортировка"</summary>
  public static int Attr_Sort_ID => TechConsts.GetID<int>(ref TechConsts._attr_Sort_ID);

  /// <summary>Проверка метаданных перед их использованием. Скрипты мы не пишем, т.к. reference-ов на либы техкарда не имеем.</summary>
  /// <param name="throwException">Выбрасывать ли exception если методанные отсутствуют</param>
  /// <returns>true if it succeeds, false if it fails</returns>
  public static bool Validate(bool throwException = true)
  {
    if (!TechConsts._wasValidated)
    {
      TechConsts._validateResult = true;
      TechConsts._objType_ProcessingRoute_ID = TechConsts.GetObjectTypeID(TechConsts.ObjType_ProcessingRoute_GUID, LocalizationHolder.GetString("ProcessingRoute"));
      TechConsts._objType_TechProcBase_ID = TechConsts.GetObjectTypeID(TechConsts.ObjType_TechProcBase_GUID, LocalizationHolder.GetString("BasicTechprocess"));
      TechConsts._objType_TechBaseObject_ID = TechConsts.GetObjectTypeID(TechConsts.ObjType_TechBaseObject_GUID, LocalizationHolder.GetString("TechcardObject"));
      TechConsts._objType_TechRoute_ID = TechConsts.GetObjectTypeID(TechConsts.ObjType_TechRoute_GUID, LocalizationHolder.GetString("TechRoute"));
      TechConsts._objType_TechRouteTemplate_ID = TechConsts.GetObjectTypeID(TechConsts.ObjType_TechRouteTemplate_GUID, LocalizationHolder.GetString("TechRouteTemplateBase"));
      TechConsts._objType_TechRouteElement_ID = TechConsts.GetObjectTypeID(TechConsts.ObjType_TechRouteElement_GUID, LocalizationHolder.GetString("TechRouteElement"));
      TechConsts._relType_TechComposition_ID = TechConsts.GetRelationTypeID(TechConsts.RelType_TechComposition_GUID, LocalizationHolder.GetString("RalationTechComposition"));
      TechConsts._attr_IsDefaultProcessingRoute_ID = TechConsts.GetAttributeID(TechConsts.Attr_IsDefaultProcessingRoute_GUID, LocalizationHolder.GetString("AttrDefaultProcessingRoute"));
      TechConsts._attr_Sort_ID = TechConsts.GetAttributeID(TechConsts.Attr_Sort_GUID, LocalizationHolder.GetString("AttrSorting"));
      TechConsts._wasValidated = true;
    }
    if (throwException && TechConsts._lastErrorText != null)
      throw new Exception(TechConsts._lastErrorText);
    return TechConsts._validateResult;
  }

  /// <summary>Получение идентификатора с проверкой валидации метаданных</summary>
  private static T GetID<T>(ref T val)
  {
    if (!TechConsts._wasValidated)
      TechConsts.Validate();
    return val;
  }

  private static int GetObjectTypeID(Guid guid, [NotNull] string typeName)
  {
    int objectTypeId = MetaDataHelper.GetObjectTypeID(guid);
    if (objectTypeId == -1)
    {
      TechConsts._validateResult = false;
      string str = LocalizationHolder.GetString("ObjTypeWithGuidNotFound", (object) typeName, (object) guid.ToString());
      TechConsts._lastErrorText = TechConsts._lastErrorText != null ? $"{TechConsts._lastErrorText}\r\n{str}" : str;
    }
    return objectTypeId;
  }

  private static int GetRelationTypeID(Guid guid, [NotNull] string typeName)
  {
    int relationTypeId = MetaDataHelper.GetRelationTypeID(guid);
    if (relationTypeId == -1)
    {
      TechConsts._validateResult = false;
      string str = LocalizationHolder.GetString("RelTypeWithGuidNotFound", (object) typeName, (object) guid.ToString());
      TechConsts._lastErrorText = TechConsts._lastErrorText != null ? $"{TechConsts._lastErrorText}\r\n{str}" : str;
    }
    return relationTypeId;
  }

  private static int GetAttributeID(Guid guid, [NotNull] string attrName)
  {
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(guid);
    if (attributeTypeId == 0)
    {
      TechConsts._validateResult = false;
      string str = LocalizationHolder.GetString("AttrTypeWithGuidNotFound", (object) attrName, (object) guid.ToString());
      TechConsts._lastErrorText = TechConsts._lastErrorText != null ? $"{TechConsts._lastErrorText}\r\n{str}" : str;
    }
    return attributeTypeId;
  }
}
