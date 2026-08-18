// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Persistence.FormatterServices
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Globalization;
using System.Reflection;

#nullable disable
namespace Intermech.Navigator.Persistence;

public sealed class FormatterServices
{
  private const BindingFlags CtorBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance;

  public static PersistentState GetObjectState(object obj)
  {
    PersistentState objectState = obj != null ? FormatterServices.GetPersistentState(obj) : throw new ArgumentNullException(nameof (obj), LocalizationHolder.rm.GetString("Interfaces.Client_72"));
    if (objectState.FullTypeName == string.Empty)
      objectState.FullTypeName = FormatterServices.GetTypeName(obj);
    return objectState;
  }

  public static object RestoreObject(PersistentState objState)
  {
    if (objState == null)
      throw new ArgumentNullException(LocalizationHolder.rm.GetString("Interfaces.Client_73"));
    if (objState.FullTypeName == Consts.PersistentStateTypeName)
      return (object) objState;
    return Activator.CreateInstance(Type.GetType(objState.FullTypeName), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, new object[1]
    {
      (object) objState
    }, (CultureInfo) null);
  }

  internal static string GetTypeName(object obj) => FormatterServices.GetTypeName(obj.GetType());

  internal static string GetTypeName(Type objType)
  {
    return $"{objType.FullName}, {objType.Assembly.GetName().Name}";
  }

  private static PersistentState GetPersistentState(object obj)
  {
    switch (obj)
    {
      case PersistentState _:
        return (PersistentState) obj;
      case IPersistable persistable:
        PersistentState state = new PersistentState();
        persistable.GetObjectData(state);
        return state;
      default:
        throw new StateFormatterException(LocalizationHolder.rm.GetString("Interfaces.Client_74"));
    }
  }
}
