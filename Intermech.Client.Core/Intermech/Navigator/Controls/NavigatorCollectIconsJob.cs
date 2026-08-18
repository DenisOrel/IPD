
// Type: Intermech.Navigator.Controls.NavigatorCollectIconsJob
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Navigator.Controls;

/// <summary>Обновление значков в узлах дерева "Навигатора"</summary>
internal class NavigatorCollectIconsJob : IJob, ITreeViewJob
{
  public NavigatorCollectIconsJob.CompleteEventHandler Complete;
  private INode parentHandler;
  private List<NavigatorTreeNode> nodes;
  private List<int> imageIndexes;

  public NavigatorCollectIconsJob(INode parentHandler, List<NavigatorTreeNode> nodes)
  {
    this.parentHandler = parentHandler;
    this.nodes = nodes;
    this.imageIndexes = new List<int>();
  }

  /// <summary>Выполняет задание.</summary>
  void IJob.Execute()
  {
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index].InTree)
      {
        NavigatorTreeNode node = this.nodes[index];
        IImageState data1 = (IImageState) this.parentHandler.GetData(node.NodeID, typeof (IImageState));
        object data2 = data1?.Data;
        object state = data1?.State;
        this.imageIndexes.Add(Holder.ImageService.IndexOf(node.NodeID.CategoryID, node.NodeID.TypeID, data2, state));
      }
      else
        this.imageIndexes.Add(-1);
    }
  }

  /// <summary>
  /// Обновляет дерево навигатора в соответствии с результатами,
  /// полученными при выполнении фонового задания.
  /// </summary>
  void ITreeViewJob.UpdateTreeView()
  {
    if (this.Complete == null)
      return;
    this.Complete(this.nodes, this.imageIndexes);
  }

  public delegate void CompleteEventHandler(List<NavigatorTreeNode> nodes, List<int> imageIndexes);
}
