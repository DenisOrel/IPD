// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ActivateViewEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Аргументы события, связанного с перестроением списка закладок в менеджере
/// </summary>
public class ActivateViewEventArgs : EventArgs
{
  /// <summary>
  /// Список уникальных идентификаторов элементов пространства навигации, на основании
  /// которых были построены старые закладки
  /// </summary>
  protected List<INodeID> _oldSelectedNodes;
  /// <summary>
  /// Список уникальных идентификаторов элементов пространства навигации, на основании
  /// которых будут построены новые закладки
  /// </summary>
  protected List<INodeID> _newSelectedNodes;
  /// <summary>Имя старой закладки</summary>
  protected string _prevViewName = string.Empty;
  /// <summary>Имя новой закладки</summary>
  protected string _nextViewName = string.Empty;
  /// <summary>
  /// Ссылка на текущую активную закладку (которая может измениться)
  /// </summary>
  protected IView _currActiveView;
  /// <summary>
  /// Имя закладки, которая должна стать активной вместо закладки NextViewName
  /// </summary>
  protected string _newViewName = string.Empty;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="oldSelectedNodes">Список уникальных идентификаторов элементов пространства навигации, на основании
  /// которых были построены старые закладки</param>
  /// <param name="newSelectedNodes">Список уникальных идентификаторов элементов пространства навигации, на основании
  /// которых будут построены новые закладки</param>
  /// <param name="prevViewName">Имя старой закладки</param>
  /// <param name="nextViewName">Имя новой закладки</param>
  /// <param name="currActiveView">Имя закладки, которая должна стать активной вместо закладки NextViewName</param>
  public ActivateViewEventArgs(
    List<INodeID> oldSelectedNodes,
    List<INodeID> newSelectedNodes,
    string prevViewName,
    string nextViewName,
    IView currActiveView)
  {
    this._oldSelectedNodes = oldSelectedNodes;
    this._newSelectedNodes = newSelectedNodes;
    this._prevViewName = prevViewName;
    this._nextViewName = nextViewName;
    this._currActiveView = currActiveView;
  }

  /// <summary>
  /// Список уникальных идентификаторов элементов пространства навигации, на основании
  /// которых были построены старые закладки
  /// </summary>
  public List<INodeID> OldSelectedNodes
  {
    [DebuggerStepThrough] get => this._oldSelectedNodes;
  }

  /// <summary>
  /// Список уникальных идентификаторов элементов пространства навигации, на основании
  /// которых будут построены новые закладки
  /// </summary>
  public List<INodeID> NewSelectedNodes
  {
    [DebuggerStepThrough] get => this._newSelectedNodes;
  }

  /// <summary>Имя старой закладки</summary>
  public string PrevViewName
  {
    [DebuggerStepThrough] get => this._prevViewName;
  }

  /// <summary>Имя новой закладки</summary>
  public string NextViewName
  {
    [DebuggerStepThrough] get => this._nextViewName;
  }

  /// <summary>
  /// Ссылка на текущую активную закладку (которая может измениться)
  /// </summary>
  public IView CurrActiveView
  {
    [DebuggerStepThrough] get => this._currActiveView;
  }

  /// <summary>
  /// Имя закладки, которая должна стать активной вместо закладки NextViewName
  /// </summary>
  public string NewViewName
  {
    [DebuggerStepThrough] get => this._newViewName;
    set => this._newViewName = value;
  }
}
