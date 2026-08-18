
// Type: Intermech.Search.CompositionContexts.CompositionContextClientHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.CompositionContexts;

public static class CompositionContextClientHelper
{
  /// <summary>Контекст Общий</summary>
  private static CompositionContext[] _compositionContextsCommon;
  /// <summary>Контекст Конструкторский</summary>
  public static CompositionContext[] _compositionContextsDefault;
  /// <summary>
  /// Все контексты состава, имеющиеся в системе (назначенные на атрибут Контекст состава)
  /// </summary>
  private static List<CompositionContext> _allContexts;
  private static CompositionContextSet _CompositionContextSet;

  /// <summary>Контекст Общий</summary>
  public static CompositionContext[] CompositionContextsCommon
  {
    get
    {
      if (CompositionContextClientHelper._compositionContextsCommon == null)
        CompositionContextClientHelper._compositionContextsCommon = CompositionContextClientHelper.BuildCompositionContextsBasedOnValues((IEnumerable<long>) new List<long>()
        {
          0L
        });
      return CompositionContextClientHelper._compositionContextsCommon;
    }
  }

  /// <summary>Контекст Конструкторский</summary>
  public static CompositionContext[] CompositionContextsDefault
  {
    get
    {
      if (CompositionContextClientHelper._compositionContextsDefault == null)
        CompositionContextClientHelper._compositionContextsDefault = CompositionContextClientHelper.BuildCompositionContextsBasedOnValues((IEnumerable<long>) new List<long>()
        {
          0L,
          1L
        });
      return CompositionContextClientHelper._compositionContextsDefault;
    }
  }

  /// <summary>
  /// Все контексты состава, имеющиеся в системе (назначенные на атрибут Контекст состава)
  /// </summary>
  public static List<CompositionContext> AllContexts
  {
    get
    {
      if (CompositionContextClientHelper._allContexts == null)
        CompositionContextClientHelper._allContexts = CompositionContextClientHelper.GetCompositionContextsFromAttrValues();
      return CompositionContextClientHelper._allContexts;
    }
  }

  public static CompositionContext[] BuildCompositionContextsBasedOnValues(IEnumerable<long> values)
  {
    List<CompositionContext> compositionContextList = new List<CompositionContext>();
    foreach (long num in values)
    {
      long value = num;
      CompositionContext compositionContext = CompositionContextClientHelper.AllContexts.FirstOrDefault<CompositionContext>((Func<CompositionContext, bool>) (o => o.Value == value));
      if (compositionContext != null)
        compositionContextList.Add(compositionContext);
    }
    return compositionContextList.ToArray();
  }

  /// <summary>
  /// Получить все назначенные значения контекстов из атрибута
  /// </summary>
  /// <returns></returns>
  private static List<CompositionContext> GetCompositionContextsFromAttrValues()
  {
    List<CompositionContext> contextsFromAttrValues = new List<CompositionContext>();
    using (new SessionKeeper())
    {
      MyAttributeMetadata attributeMetadata = new MyAttributeMetadata();
      attributeMetadata.SetByGUID("cad00651-306c-11d8-b4e9-00304f19f545");
      if (attributeMetadata.AttrPossibleValues != null)
      {
        for (int index = 0; index < attributeMetadata.AttrPossibleValues.Count; ++index)
        {
          if (attributeMetadata.AttrPossibleValues[index] is MyElement attrPossibleValue)
          {
            CompositionContext compositionContext = new CompositionContext((long) attrPossibleValue.Value, attrPossibleValue.Caption);
            contextsFromAttrValues.Add(compositionContext);
          }
        }
      }
    }
    return contextsFromAttrValues;
  }

  public static CompositionContextSet GetDefaultCompositionContexts()
  {
    if (CompositionContextClientHelper._CompositionContextSet == null)
    {
      string text = (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadString("Core", "CurrentUserSettings", "DefaultCompositionContexts", string.Empty, DBConfigMode.UserOnly);
      CompositionContextClientHelper._CompositionContextSet = string.IsNullOrEmpty(text) ? CompositionContextSet.Default : CompositionContextClientHelper.DeserializeCompositionContextSet(text) ?? CompositionContextSet.Default;
    }
    return CompositionContextClientHelper._CompositionContextSet;
  }

  public static void SetDefaultComposiitonContexts(CompositionContextSet compositionContexts)
  {
    if (compositionContexts == null)
      throw new ArgumentNullException(nameof (compositionContexts));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.Configurations.WriteString("Core", "CurrentUserSettings", "DefaultCompositionContexts", CompositionContextClientHelper.SerializeCompositionContextSet(compositionContexts), sessionKeeper.Session.UserID);
    CompositionContextClientHelper._CompositionContextSet = compositionContexts;
  }

  private static string SerializeCompositionContextSet(CompositionContextSet compositionContextSet)
  {
    return string.Join<long>(" ", ((IEnumerable<CompositionContext>) compositionContextSet.CompositionContexts).Select<CompositionContext, long>((Func<CompositionContext, long>) (v => v.Value)));
  }

  private static CompositionContextSet DeserializeCompositionContextSet(string text)
  {
    List<CompositionContext> compositionContextList = new List<CompositionContext>();
    try
    {
      foreach (long num in ((IEnumerable<string>) text.Split(' ')).Select<string, long>((Func<string, long>) (v => Convert.ToInt64(v))))
      {
        long contextValue = num;
        CompositionContext compositionContext = CompositionContextClientHelper.AllContexts.FirstOrDefault<CompositionContext>((Func<CompositionContext, bool>) (cont => cont.Value == contextValue));
        if (compositionContext != null)
          compositionContextList.Add(compositionContext);
      }
    }
    catch
    {
      return (CompositionContextSet) null;
    }
    return new CompositionContextSet(compositionContextList.ToArray());
  }
}
