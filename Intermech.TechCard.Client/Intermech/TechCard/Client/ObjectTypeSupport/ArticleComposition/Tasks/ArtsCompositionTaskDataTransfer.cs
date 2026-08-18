// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks.ArtsCompositionTaskDataTransfer
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Navigator;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks;

/// <summary>Класс-затычка для передачи своих параметров в запросы</summary>
internal class ArtsCompositionTaskDataTransfer : ClientPluginsDataTransfer
{
  /// <summary>Текущий набор контекстов</summary>
  private int _currSet;
  /// <summary>Дополнительные контексты (первый набор)</summary>
  private readonly List<long> _addContexts = new List<long>(0);
  /// <summary>Дополнительные контексты (второй набор)</summary>
  private readonly List<long> _addContexts2 = new List<long>(0);
  /// <summary>Дополнительные контексты (третий набор)</summary>
  private readonly List<long> _addContexts3 = new List<long>(0);

  /// <summary>Создать класс с тремя наборами контекстов</summary>
  /// <param name="firstSet">Первый набор контекстов</param>
  /// <param name="secondSet">Второй набор контекстов</param>
  /// <param name="thirdSet">Третий набор контекстов</param>
  public ArtsCompositionTaskDataTransfer(long[] firstSet, long[] secondSet, long[] thirdSet)
  {
    this._addContexts.Clear();
    this._addContexts2.Clear();
    this._addContexts3.Clear();
    if (firstSet != null)
    {
      foreach (long first in firstSet)
      {
        if (!this._addContexts.Contains(first))
          this._addContexts.Add(first);
      }
    }
    if (secondSet != null)
    {
      foreach (long second in secondSet)
      {
        if (!this._addContexts2.Contains(second))
          this._addContexts2.Add(second);
      }
    }
    if (thirdSet == null)
      return;
    foreach (long third in thirdSet)
    {
      if (!this._addContexts3.Contains(third))
        this._addContexts3.Add(third);
    }
  }

  /// <summary>Текущий набор контекстов</summary>
  internal List<long> CurrentContexts
  {
    get
    {
      switch (this._currSet)
      {
        case 1:
          return this._addContexts2;
        case 2:
          return this._addContexts3;
        default:
          return this._addContexts;
      }
    }
  }

  /// <summary>Индекс текущего контекста</summary>
  internal int CurrentSet
  {
    get => this._currSet;
    set
    {
      if (this._currSet == value)
        return;
      switch (value)
      {
        case 1:
        case 2:
          this._currSet = value;
          break;
        default:
          this._currSet = 0;
          break;
      }
    }
  }

  /// <summary>Дополнительные контексты (первый набор)</summary>
  internal List<long> AddContexts => this._addContexts;

  /// <summary>Дополнительные контексты (первый набор)</summary>
  internal List<long> AddContexts2 => this._addContexts2;

  /// <summary>Дополнительные контексты (первый набор)</summary>
  internal List<long> AddContexts3 => this._addContexts3;

  /// <summary>
  /// Метод вызывается ядром клиентской части для сбора информации у плагинов.
  /// Плагины, подписавшиеся в коллекции IClientPluginsService, должны записать в словарик
  /// PluginsData свою информацию в виде сериализуемых пар значений [Ключ] = [Значение].
  /// Указанная информация будет передана на серверную сторону.
  /// </summary>
  /// <param name="pluginsData">Коллекция сериализуемых пар значений для передачи
  /// дополнительной информации на серверную сторону</param>
  public override void GetPluginData(HybridDictionary pluginsData)
  {
    base.GetPluginData(pluginsData);
    if (pluginsData == null)
      return;
    List<long> longList = new List<long>((IEnumerable<long>) this.CurrentContexts);
    pluginsData[(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] = (object) longList;
  }
}
