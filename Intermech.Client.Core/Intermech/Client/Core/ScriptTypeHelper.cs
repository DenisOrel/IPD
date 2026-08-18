
// Type: Intermech.Client.Core.ScriptTypeHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace Intermech.Client.Core;

/// <summary>Класс хелпер по работе со сценариями</summary>
public sealed class ScriptTypeHelper
{
  /// <summary>Кеш для типов объектов</summary>
  private static Dictionary<Guid, ScriptTypes> _objType2ScriptTypeCache = new Dictionary<Guid, ScriptTypes>();
  /// <summary>Кеш для типов сценариев</summary>
  private static Dictionary<ScriptTypes, Guid> _scriptType2ObjTypeCache = new Dictionary<ScriptTypes, Guid>();

  /// <summary>Инициализация данных класса</summary>
  private static void InitializeData()
  {
    FieldInfo[] fields = typeof (ScriptTypes).GetFields();
    if (fields == null || fields.Length == 0)
      return;
    foreach (FieldInfo fieldInfo in fields)
    {
      ScriptTypes key = (ScriptTypes) fieldInfo.GetValue((object) ScriptTypes.Unknown);
      object[] customAttributes = fieldInfo.GetCustomAttributes(typeof (CreateObjectTypeGuidAttribute), false);
      if (customAttributes != null && customAttributes.Length != 0 && customAttributes[0] is CreateObjectTypeGuidAttribute typeGuidAttribute)
      {
        ScriptTypeHelper._scriptType2ObjTypeCache[key] = typeGuidAttribute.ObjectTypeGuid;
        ScriptTypeHelper._objType2ScriptTypeCache[typeGuidAttribute.ObjectTypeGuid] = key;
      }
    }
  }

  static ScriptTypeHelper() => ScriptTypeHelper.InitializeData();

  /// <summary>Получение типа объекта по типу сценария</summary>
  /// <param name="scriptType">Тип сценария</param>
  /// <returns>Ид. типа объекта</returns>
  public static int GetObjType4ScriptType(ScriptTypes scriptType)
  {
    return ScriptTypeHelper.GetObjType4ScriptType(scriptType, -1);
  }

  /// <summary>Получение типа объекта по типу сценария</summary>
  /// <param name="scriptType">Тип сценария</param>
  /// <param name="defObjType">Ид. типа объекта по умолчанию</param>
  /// <returns>Ид. типа объекта</returns>
  public static int GetObjType4ScriptType(ScriptTypes scriptType, int defObjType)
  {
    Guid objTypeGuid;
    return !ScriptTypeHelper._scriptType2ObjTypeCache.TryGetValue(scriptType, out objTypeGuid) ? defObjType : MetaDataHelper.GetObjectTypeID(objTypeGuid);
  }

  /// <summary>Получение типа сценария по типу объекта</summary>
  /// <param name="objectType">Тип объекта</param>
  /// <returns>Тип сценария</returns>
  public static ScriptTypes GetScriptType4ObjType(int objectType)
  {
    return ScriptTypeHelper.GetScriptType4ObjType(objectType, ScriptTypes.Unknown);
  }

  /// <summary>Получение типа сценария по типу объекта</summary>
  /// <param name="objectType">Тип объекта</param>
  /// <returns>Тип сценария</returns>
  public static ScriptTypes GetScriptType4ObjType(Guid objectType)
  {
    return ScriptTypeHelper.GetScriptType4ObjType(objectType, ScriptTypes.Unknown);
  }

  /// <summary>Получение типа сценария по типу объекта</summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="defScriptType">Тип сценария по умолчанию</param>
  /// <returns>Тип сценария</returns>
  public static ScriptTypes GetScriptType4ObjType(int objectType, ScriptTypes defScriptType)
  {
    return ScriptTypeHelper.GetScriptType4ObjType(MetaDataHelper.GetObjectTypeGuid(objectType), defScriptType);
  }

  /// <summary>Получение типа сценария по типу объекта</summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="defScriptType">Тип сценария по умолчанию</param>
  /// <returns>Тип сценария</returns>
  public static ScriptTypes GetScriptType4ObjType(Guid objectType, ScriptTypes defScriptType)
  {
    if (objectType == Guid.Empty)
      return defScriptType;
    ScriptTypes scriptType4ObjType;
    while (!ScriptTypeHelper._objType2ScriptTypeCache.TryGetValue(objectType, out scriptType4ObjType))
    {
      objectType = MetaDataHelper.GetObjectTypeParentID(objectType);
      if (!(objectType != Guid.Empty))
        return defScriptType;
    }
    return scriptType4ObjType;
  }
}
