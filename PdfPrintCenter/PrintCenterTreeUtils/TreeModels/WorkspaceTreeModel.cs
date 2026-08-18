// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeModels.WorkspaceTreeModel
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes;
using Intermech.PdfPrintCenter.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeModels
{
    internal class WorkspaceTreeModel : PrintCenterTreeModel
    {
        public WorkspaceAddNodesResult AddNode(
          string objectName,
          string filePath,
          bool addFilenameToCaption)
        {
            if (!File.Exists(filePath))
                return (WorkspaceAddNodesResult)null;
            WorkspaceObjectTreeNode objectNode = (WorkspaceObjectTreeNode)null;
            List<IntermechPageSize> pdfSizes = PdfUtils.GetPDFSizes(filePath);
            IEnumerable<IntermechPageSize> intermechPageSizes = pdfSizes.Where<IntermechPageSize>((Func<IntermechPageSize, bool>)(x => x == null));
            if (intermechPageSizes.Count<IntermechPageSize>() != pdfSizes.Count<IntermechPageSize>())
            {
                objectNode = new WorkspaceObjectTreeNode(objectName, filePath, addFilenameToCaption);
                pdfSizes.Except<IntermechPageSize>(intermechPageSizes).ToList<IntermechPageSize>().ForEach((Action<IntermechPageSize>)(pageSize => objectNode.Children.Add((PrintCenterNode)new WorkspacePagesTreeNode(objectNode, filePath, pageSize))));
                this.Nodes.Add((PrintCenterNode)objectNode);
            }
            return new WorkspaceAddNodesResult(Path.GetFileNameWithoutExtension(filePath), objectNode, pdfSizes.Except<IntermechPageSize>(intermechPageSizes).Select<IntermechPageSize, string>((Func<IntermechPageSize, string>)(x => x.Range)).ToList<string>(), intermechPageSizes.Select<IntermechPageSize, string>((Func<IntermechPageSize, string>)(x => x.Range)).ToList<string>());
        }

        public override PrintCenterNode AddNode(PrintCenterNode node)
        {
            PrintQueuePagesNode printQueuePagesNode = node as PrintQueuePagesNode;
            WorkspaceObjectTreeNode parent = this.FindNode<WorkspaceObjectTreeNode>(this.Nodes, printQueuePagesNode.MainColumnCaption);
            if (parent == null)
            {
                parent = new WorkspaceObjectTreeNode(printQueuePagesNode.ObjectName, printQueuePagesNode.FilePath, printQueuePagesNode.AddFilenameToCaption);
                this.Nodes.Add((PrintCenterNode)parent);
            }
            WorkspacePagesTreeNode workspacePagesTreeNode = new WorkspacePagesTreeNode(parent, printQueuePagesNode.FilePath, printQueuePagesNode.PageSize);
            parent.Children.Add((PrintCenterNode)workspacePagesTreeNode);
            return (PrintCenterNode)workspacePagesTreeNode;
        }
    }
}
