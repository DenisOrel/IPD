// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.QuickSearch.BaseQuickSearchProvider
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Navigator.Interfaces.QuickSearch;

/// <summary>
/// 
/// </summary>
public abstract class BaseQuickSearchProvider
{
  private string _text = string.Empty;
  private System.Threading.Timer _timerForServer;
  private int _elementCount;
  private Action<List<QuickSearchResultItem>> _resultCallback;

  /// <summary>
  /// 
  /// </summary>
  public virtual object ParentNode { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public virtual ImageList ImgList => (ImageList) null;

  /// <summary>
  /// 
  /// </summary>
  public virtual bool NeedTimerForServerRequest => false;

  /// <summary>
  /// 
  /// </summary>
  protected virtual int MaxElementCount => 20;

  /// <summary>
  /// 
  /// </summary>
  protected virtual int TimeDelay => 3000;

  /// <summary>Конструктор.</summary>
  public BaseQuickSearchProvider()
  {
    this._timerForServer = new System.Threading.Timer(new TimerCallback(this.On_serverTimer_Tick), (object) null, -1, -1);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public List<QuickSearchResultItem> Search(string text)
  {
    this._text = text;
    List<QuickSearchResultItem> searchResultItemList = this.ClientSearch(text);
    this._elementCount = searchResultItemList != null ? searchResultItemList.Count : 0;
    if (this._resultCallback != null)
    {
      this._timerForServer.Change(-1, -1);
      if (this._elementCount < this.MaxElementCount)
        this._timerForServer.Change(this.TimeDelay, -1);
    }
    return searchResultItemList;
  }

  /// <summary>
  /// 
  /// </summary>
  public void StopSearch()
  {
    this._timerForServer.Change(-1, -1);
    this._elementCount = 0;
    this._text = string.Empty;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="resultNode"></param>
  /// <returns></returns>
  public bool SelectNode(QuickSearchResultItem resultNode)
  {
    return resultNode != null && this.ClientSelectNode(resultNode);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="resultCallback"></param>
  public void ServerRequestCallback(Action<List<QuickSearchResultItem>> resultCallback)
  {
    this._resultCallback = resultCallback;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  protected abstract List<QuickSearchResultItem> ClientSearch(string text);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="resultNode"></param>
  /// <returns></returns>
  protected abstract bool ClientSelectNode(QuickSearchResultItem resultNode);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  /// <param name="elementCount"></param>
  /// <returns></returns>
  protected virtual List<QuickSearchResultItem> ServerSearch(string text, int elementCount)
  {
    return (List<QuickSearchResultItem>) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  private void On_serverTimer_Tick(object sender)
  {
    int elementCount = this.MaxElementCount - this._elementCount;
    if (elementCount <= 0)
      return;
    this.Start(elementCount);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="elementCount"></param>
  private async void Start(int elementCount)
  {
    await Task.Run((Action) (() => this.StartSearch(elementCount)));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="elementCount"></param>
  private void StartSearch(int elementCount)
  {
    string text = this._text;
    List<QuickSearchResultItem> searchResultItemList = this.ServerSearch(text, elementCount);
    if (searchResultItemList == null || !(text == this._text))
      return;
    this._resultCallback(searchResultItemList);
  }
}
