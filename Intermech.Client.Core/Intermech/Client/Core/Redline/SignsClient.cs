
// Type: Intermech.Client.Core.Redline.SignsClient
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Signs.Interfaces;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace Intermech.Client.Core.Redline;

/// <summary>Интерфейс для доступа к клиентской службе подписей</summary>
public static class SignsClient
{
  /// <summary>тип класса Intermech.Signs.Client.SignsCache</summary>
  private static readonly Lazy<Type> TypeSignsCache = new Lazy<Type>((Func<Type>) (() => Type.GetType("Intermech.Signs.Client.SignsCache,Intermech.Signs", false)));
  /// <summary>тип класса Intermech.Signs.Interfaces.GraphsSet</summary>
  private static readonly Lazy<Type> TypeGraphsSet = new Lazy<Type>((Func<Type>) (() => Type.GetType("Intermech.Signs.Interfaces.GraphsSet,Intermech.Search.Interfaces", false)));
  /// <summary>тип класса Intermech.Signs.Interfaces.GraphsCollection</summary>
  private static readonly Lazy<Type> TypeGraphsCollection = new Lazy<Type>((Func<Type>) (() => Type.GetType("Intermech.Signs.Interfaces.GraphsCollection,Intermech.Search.Interfaces", false)));
  /// <summary>тип класса Intermech.Signs.Interfaces.GraphClass</summary>
  private static readonly Lazy<Type> TypeGraphClass = new Lazy<Type>((Func<Type>) (() => Type.GetType("Intermech.Signs.Interfaces.GraphClass,Intermech.Search.Interfaces", false)));

  /// <summary>Возвращает массив  с информацией о выбранных должностях и графах,
  /// в которых текущий пользователь может подписать объект objectID.
  /// Если objectID == 0, то возвращает весь список граф для данного юзера от всех его должностей.</summary>
  /// <param name="objectID">Идентификатор версии объекта.</param>
  /// <returns>Массив с информацией о выбранных должностях и графах. Массив пустой, если юзер ничего не может подписать.</returns>
  public static RankGraphsInfo[] ShowUserGraphs(long objectID)
  {
    List<RankGraphsInfo> rankGraphsInfoList1 = new List<RankGraphsInfo>();
    if (objectID == 0L || SignsClient.TypeSignsCache == null || ServicesManager.GetService(typeof (ISignsClientService)) == null)
      return rankGraphsInfoList1.ToArray();
    object obj1 = SignsClient.TypeSignsCache.Value.GetField("UserSignsCard", BindingFlags.Static | BindingFlags.Public).GetValue((object) null);
    if (obj1 == null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        object[] parameters = new object[2]
        {
          (object) session,
          (object) session.UserID
        };
        obj1 = SignsClient.TypeSignsCache.Value.GetMethod("LoadUserGraphInfo", new Type[2]
        {
          parameters[0].GetType(),
          parameters[1].GetType()
        }).Invoke((object) null, parameters);
      }
    }
    if (obj1 == null)
      return rankGraphsInfoList1.ToArray();
    Type type = obj1.GetType();
    object obj2 = Convert.ChangeType(obj1, type);
    List<IDBTypedObjectID> dbTypedObjectIdList = new List<IDBTypedObjectID>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
      dbTypedObjectIdList.Add((IDBTypedObjectID) new DBTypedObjectID(dbObject.ObjectType, dbObject.ObjectID, dbObject.ID, dbObject.Caption, dbObject.OwnerID, (long) dbObject.VersionID, Convert.ToInt64(dbObject.IsBaseVersion), dbObject.SiteID, dbObject.ModificationID));
    }
    List<long> longList1 = new List<long>();
    object[] parameters1 = new object[2]
    {
      (object) dbTypedObjectIdList,
      null
    };
    int num1 = (bool) type.GetMethod("IsUserCanSign").Invoke(obj2, parameters1) ? 1 : 0;
    List<long> longList2 = parameters1[1] as List<long>;
    object obj3 = Convert.ChangeType(SignsClient.TypeSignsCache.Value.GetMethod("GetGraphsToSign").Invoke((object) null, (object[]) new List<IDBTypedObjectID>[1]
    {
      dbTypedObjectIdList
    }), SignsClient.TypeGraphsSet.Value);
    List<string> lsthint = new List<string>();
    if (obj3 != null)
    {
      SignsClient.TypeGraphsCollection.Value.MakeArrayType();
      foreach (object obj4 in (IEnumerable) SignsClient.TypeGraphsSet.Value.GetProperty("Values").GetValue(obj3, (object[]) null))
      {
        // ISSUE: reference to a compiler-generated field
        if (SignsClient.\u003C\u003Eo__0.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SignsClient.\u003C\u003Eo__0.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (SignsClient)));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        foreach (object obj5 in SignsClient.\u003C\u003Eo__0.\u003C\u003Ep__1.Target((CallSite) SignsClient.\u003C\u003Eo__0.\u003C\u003Ep__1, obj4))
        {
          // ISSUE: reference to a compiler-generated field
          if (SignsClient.\u003C\u003Eo__0.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            SignsClient.\u003C\u003Eo__0.\u003C\u003Ep__0 = CallSite<Func<CallSite, PropertyInfo, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetValue", (IEnumerable<Type>) null, typeof (SignsClient), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          string str = SignsClient.\u003C\u003Eo__0.\u003C\u003Ep__0.Target((CallSite) SignsClient.\u003C\u003Eo__0.\u003C\u003Ep__0, SignsClient.TypeGraphClass.Value.GetProperty("Value"), obj5, (object) null) as string;
          lsthint.Add(str);
        }
      }
    }
    List<RankGraphsInfo> rankGraphsInfoList2 = new List<RankGraphsInfo>();
    foreach (long num2 in longList2)
    {
      string caption;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        caption = sessionKeeper.Session.GetObjectInfo(num2).Caption;
      if (!string.IsNullOrEmpty(caption))
      {
        List<string[]> source = new List<string[]>();
        object[] objArray = new object[2]
        {
          (object) num2,
          (object) dbTypedObjectIdList
        };
        List<string> stringList = type.GetMethod("GetGraphs", ((IEnumerable<object>) objArray).Select<object, Type>((Func<object, Type>) (p => p.GetType())).ToArray<Type>()).Invoke(obj2, objArray) as List<string>;
        Dictionary<string, string> dictionary = SignsClient.TypeSignsCache.Value.GetField("PossibleGraphs", BindingFlags.Static | BindingFlags.Public).GetValue((object) null) as Dictionary<string, string>;
        foreach (string key in stringList)
        {
          if (dictionary.ContainsKey(key))
            source.Add(new string[2]{ dictionary[key], key });
        }
        source.Sort((Comparison<string[]>) ((x, y) => x[0].CompareTo(y[0])));
        if (obj3 != null)
        {
          List<string[]> list = source.Where<string[]>((Func<string[], bool>) (p => lsthint.Contains(p[1]))).ToList<string[]>();
          if (list.Count != 0)
          {
            list.Sort((Comparison<string[]>) ((x, y) => x[0].CompareTo(y[0])));
            rankGraphsInfoList2.Add(new RankGraphsInfo(num2, caption, list.Select<string[], string>((Func<string[], string>) (p => p[0])).ToArray<string>()));
          }
        }
        rankGraphsInfoList1.Add(new RankGraphsInfo(num2, caption, source.Select<string[], string>((Func<string[], string>) (p => p[0])).ToArray<string>()));
      }
    }
    rankGraphsInfoList2.Sort((Comparison<RankGraphsInfo>) ((x, y) => x.RankCaption.CompareTo(y.RankCaption)));
    rankGraphsInfoList1.Sort((Comparison<RankGraphsInfo>) ((x, y) => x.RankCaption.CompareTo(y.RankCaption)));
    return obj3 != null ? rankGraphsInfoList2.ToArray() : rankGraphsInfoList1.ToArray();
  }
}
