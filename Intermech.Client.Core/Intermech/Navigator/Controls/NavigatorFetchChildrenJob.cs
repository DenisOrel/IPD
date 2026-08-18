
// Type: Intermech.Navigator.Controls.NavigatorFetchChildrenJob
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Controls;

/// <summary>Обновление [+] в узлах дерева "Навигатора"</summary>
internal class NavigatorFetchChildrenJob : IJob, ITreeViewJob
{
  private NavigatorTreeNode _node;
  private int _count;
  private NodeColumnCollection _columns;
  private INode _handler;
  private object _bookmark;
  private NavigatorJobResultPacket _resultPacket;

  public event NavigatorFetchChildrenJob.CompleteEventHandler Complete;

  public NavigatorFetchChildrenJob(NavigatorTreeNode node, NodeColumnCollection columns, int count)
  {
    this._node = node;
    this._columns = columns;
    this._count = count;
    this.Complete = (NavigatorFetchChildrenJob.CompleteEventHandler) null;
    NavigatorTreeNode navigatorTreeNode = node;
    this._handler = navigatorTreeNode.Handler;
    this._bookmark = navigatorTreeNode.Bookmark;
    this._resultPacket = (NavigatorJobResultPacket) null;
  }

  /// <summary>Выполняет задание.</summary>
  public void Execute()
  {
    if (this._handler == null)
      return;
    INodeQuery query = this._handler.GetQuery(ContentType.Folders);
    if (query == null)
      return;
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    for (int index = 0; index < this._columns.Count; ++index)
      query.AddColumn(this._columns[index], service.GetDefaultTransform(this._columns[index].SchemeGuid, this._columns[index].ID));
    this._resultPacket = NavigatorJobResultPacket.FromQuery(query, this._columns, this._bookmark, this._count);
  }

  /// <summary>
  /// Обновляет дерево навигатора в соответствии с результатами,
  /// полученными при выполнении фонового задания.
  /// </summary>
  public void UpdateTreeView()
  {
    if (this.Complete == null)
      return;
    this.Complete(this._node, this._resultPacket);
  }

  /// <summary>Делегат - работа завершена</summary>
  /// <param name="node">Узел</param>
  /// <param name="resultPacket">Пакет с данными</param>
  public delegate void CompleteEventHandler(
    NavigatorTreeNode node,
    NavigatorJobResultPacket resultPacket);
}
