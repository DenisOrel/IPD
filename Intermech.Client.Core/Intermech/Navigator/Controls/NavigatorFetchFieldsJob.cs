
// Type: Intermech.Navigator.Controls.NavigatorFetchFieldsJob
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Controls;

/// <summary>Обновление ячеек в узлах дерева "Навигатора"</summary>
internal class NavigatorFetchFieldsJob : IJob, ITreeViewJob
{
  private INode _handler;
  private NodeColumnCollection _columns;
  private NavigatorTreeViewVisibleNodesGroup _nodes;
  private NodeIDCollection _nodeIDs;
  private NavigatorJobResultPacket _resultPacket;

  public event NavigatorFetchFieldsJob.CompleteEventHandler Complete;

  public NavigatorFetchFieldsJob(
    INode handler,
    NodeColumnCollection columns,
    NavigatorTreeViewVisibleNodesGroup nodes)
  {
    this._handler = handler;
    this._columns = columns;
    this._nodes = nodes;
    this.Complete = (NavigatorFetchFieldsJob.CompleteEventHandler) null;
    this._nodeIDs = new NodeIDCollection();
    for (int index = 0; index < this._nodes.Count; ++index)
      this._nodeIDs.Add(this._nodes[index].NodeID);
    this._resultPacket = (NavigatorJobResultPacket) null;
  }

  public void Execute()
  {
    INodeQuery query = this._handler.GetQuery(ContentType.Folders);
    if (query == null)
      return;
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    for (int index = 0; index < this._columns.Count; ++index)
      query.AddColumn(this._columns[index], service.GetDefaultTransform(this._columns[index].SchemeGuid, this._columns[index].ID));
    this._resultPacket = NavigatorJobResultPacket.FromQuery(query, this._columns, this._nodeIDs);
  }

  public void UpdateTreeView()
  {
    if (this.Complete == null)
      return;
    this.Complete(this._nodes, this._resultPacket);
  }

  public delegate void CompleteEventHandler(
    NavigatorTreeViewVisibleNodesGroup nodes,
    NavigatorJobResultPacket resultPacket);
}
