// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.FindFieldHelper
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Вспомогательный класс для поиска поля в иерархии класса</summary>
public class FindFieldHelper
{
  /// <summary>Найти поле в иерархии класса</summary>
  /// <param name="type">Тип класса</param>
  /// <param name="fieldName">Имя поля</param>
  /// <returns>Информация о поле. Если поле не найдено, то возвращает null</returns>
  public static FieldInfo FindField(Type type, string fieldName)
  {
    BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    FieldInfo field = (FieldInfo) null;
    for (; type != (Type) null; type = type.BaseType)
    {
      field = type.GetField(fieldName, bindingAttr);
      if (!(field == (FieldInfo) null))
        break;
    }
    return field;
  }

  /// <summary>Поиск аттрибутов в иерархии типа</summary>
  /// <param name="type"></param>
  /// <param name="attrType"></param>
  /// <param name="propName"></param>
  /// <returns></returns>
  public static object[] FindAttributes(Type type, Type attrType, string propName)
  {
    BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    List<object> objectList = new List<object>();
    List<Type> typeList = new List<Type>();
    for (; type != (Type) null; type = type.BaseType)
    {
      PropertyInfo property = type.GetProperty(propName, bindingAttr);
      if (property != (PropertyInfo) null)
      {
        object[] customAttributes = property.GetCustomAttributes(attrType, true);
        if (customAttributes != null)
        {
          foreach (object obj in customAttributes)
          {
            Type type1 = obj.GetType();
            if (!typeList.Contains(type1))
            {
              typeList.Add(type1);
              objectList.Add(obj);
            }
          }
        }
      }
    }
    return objectList.ToArray();
  }

  /// <summary>Поиск метода, пока работает неадекватно</summary>
  /// <param name="type"></param>
  /// <param name="name"></param>
  /// <param name="values"></param>
  /// <returns></returns>
  public static MethodInfo FindMethod(Type type, string name, object[] values)
  {
    MethodInfo method = (MethodInfo) null;
    for (; type != (Type) null; type = type.BaseType)
    {
      List<Type> typeList = new List<Type>();
      foreach (object obj in values)
        typeList.Add(obj.GetType());
      method = type.GetMethod(name, typeList.ToArray());
      if (!(method == (MethodInfo) null))
        break;
    }
    return method;
  }

  public static PropertyInfo FindProperty(Type type, string propName)
  {
    BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    PropertyInfo property = (PropertyInfo) null;
    for (; type != (Type) null; type = type.BaseType)
    {
      property = type.GetProperty(propName, bindingAttr);
      if (!(property == (PropertyInfo) null))
        break;
    }
    return property;
  }

  /// <summary>Найти все сериализуемые поля в иерархии класса</summary>
  /// <param name="type">Тип</param>
  /// <returns>Сериализуемые поля</returns>
  public static FieldInfo[] FindSerializableFields(Type type)
  {
    ArrayList arrayList = new ArrayList();
    BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    for (; type != (Type) null; type = type.BaseType)
    {
      FieldInfo[] fields = type.GetFields(bindingAttr);
      for (int index = 0; index < fields.Length; ++index)
      {
        if (!fields[index].IsNotSerialized)
          arrayList.Add((object) fields[index]);
      }
    }
    FieldInfo[] serializableFields = new FieldInfo[arrayList.Count];
    if (arrayList.Count != 0)
      arrayList.CopyTo((Array) serializableFields, 0);
    return serializableFields;
  }
}
