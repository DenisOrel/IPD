// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Substitutes.Constants
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Pdm.Substitutes;

/// <summary>Константы для работы с допустимыми заменами</summary>
/// <remarks>Вынести сюда все константы из SubstituteObjects</remarks>
public static class Constants
{
  /// <summary>
  /// Глобальный идентификатор атрибута "Номер группы заменителей"
  /// </summary>
  public static readonly Guid SubstituteGroupNumberAttributeTypeGuid = new Guid("cad001c0-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Глобальный идентификатор атрибута "Имя группы заменителей"
  /// </summary>
  public static readonly Guid SubstituteGroupNameAttributeTypeGuid = new Guid("cad00817-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Номер заменителя"</summary>
  public static readonly Guid SubstituteNumberAttributeTypeGuid = new Guid("cad001c1-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор атрибута "Имя заменителя"</summary>
  public static readonly Guid SubstituteNameAttributeTypeGuid = new Guid("cad00818-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Глобальный идентификатор аттрибута "Конструкторский основной вариант"
  /// </summary>
  public static readonly Guid DesingActualVariantAttributeTypeGuid = new Guid("cad00654-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор аттрибута "Количество"</summary>
  public static readonly Guid QuantityAttributeTypeGuid = new Guid("cad00267-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор аттрибута "Позиция"</summary>
  public static readonly Guid PositionAttrbiuteTypeGuid = new Guid("cad00270-306c-11d8-b4e9-00304f19f545");
  private static bool _isInitialized;
  private static int _substituteGroupNumberAttributeTypeID = 0;
  private static int _substituteGroupNameAttributeTypeID = 0;
  private static int _substituteNumberAttributeTypeID = 0;
  private static int _substituteNameAttributeTypeID = 0;
  private static int _desingActualVariantAttributeTypeID = 0;
  private static int _designationAttributeTypeID = 0;
  private static int _nameAttributeTypeID = 0;
  private static int _quantityAttributeTypeID = 0;
  private static int _positionAttributeTypeID = 0;

  /// <summary>Идентификатор атрибута "Номер группы заменителей"</summary>
  public static int SubstituteGroupNumberAttributeTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._substituteGroupNumberAttributeTypeID;
    }
  }

  /// <summary>Идентификатор атрибута "Имя группы заменителей"</summary>
  public static int SubstituteGroupNameAttributeTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._substituteGroupNameAttributeTypeID;
    }
  }

  /// <summary>Идентификатор атрибута "Номер заменителя"</summary>
  public static int SubstituteNumberAttributeTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._substituteNumberAttributeTypeID;
    }
  }

  /// <summary>Идентификатор атрибута "Имя заменителя"</summary>
  public static int SubstituteNameAttributeTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._substituteNameAttributeTypeID;
    }
  }

  /// <summary>
  /// Идентификатор аттрибута "Конструкторский основной вариант"
  /// </summary>
  public static int DesingActualVariantAttributeTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._desingActualVariantAttributeTypeID;
    }
  }

  /// <summary>Идентификатор аттрибута "Количество"</summary>
  public static int QuantityAttributeTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._quantityAttributeTypeID;
    }
  }

  /// <summary>Идентификатор аттрибута "Позиция"</summary>
  public static int PositionAttributeTypeID
  {
    [DebuggerStepThrough] get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._positionAttributeTypeID;
    }
  }

  private static void InitializeIfNotInitialized()
  {
    if (!Constants._isInitialized)
      Constants.Initialize();
    Constants._isInitialized = true;
  }

  private static void Initialize()
  {
    Constants._quantityAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.QuantityAttributeTypeGuid);
    Constants._substituteGroupNumberAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.SubstituteGroupNumberAttributeTypeGuid);
    Constants._substituteGroupNameAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.SubstituteGroupNameAttributeTypeGuid);
    Constants._substituteNumberAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.SubstituteNumberAttributeTypeGuid);
    Constants._substituteNameAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.SubstituteNameAttributeTypeGuid);
    Constants._desingActualVariantAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.DesingActualVariantAttributeTypeGuid);
    Constants._positionAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.PositionAttrbiuteTypeGuid);
  }

  private static int GetAttributeTypeID4AttributeTypeGuid(Guid guid)
  {
    return MetaDataHelper.GetAttributeTypeID(guid);
  }
}
