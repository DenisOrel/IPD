// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.EntityTypesCashe
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Intermech.GTC.Server.P21;

public class EntityTypesCashe
{
  private static readonly System.Collections.Generic.Dictionary<string, Type> Dictionary = new System.Collections.Generic.Dictionary<string, Type>();

  static EntityTypesCashe() => EntityTypesCashe.GetAllowEntityTypes();

  private static void GetAllowEntityTypes()
  {
    foreach (var data in ((IEnumerable<Type>) typeof (EntityTypesCashe).Assembly.GetTypes()).Where<Type>((Func<Type, bool>) (type => type.IsClass && type.BaseType == typeof (BaseObject))).SelectMany((Func<Type, IEnumerable<FieldInfo>>) (type => (IEnumerable<FieldInfo>) type.GetFields()), (type, fieldInfo) => new
    {
      type = type,
      fieldInfo = fieldInfo
    }).Where(_param1 => _param1.fieldInfo.IsStatic && _param1.fieldInfo.Name.Equals("EntityName") && !_param1.fieldInfo.GetValue((object) null).Equals((object) null)).Select(_param1 => new
    {
      Type = _param1.fieldInfo.DeclaringType,
      EntityName = (string) _param1.fieldInfo.GetValue((object) null)
    }))
    {
      if (!EntityTypesCashe.Dictionary.ContainsKey(data.EntityName))
        EntityTypesCashe.Dictionary.Add(data.EntityName, data.Type);
    }
  }

  public static Type GetEntityType(string entityName)
  {
    Type entityType;
    if (!EntityTypesCashe.Dictionary.TryGetValue(entityName, out entityType))
      throw new Exception($"Для сущности '{entityName}' не найден тип объекта");
    return entityType;
  }
}
