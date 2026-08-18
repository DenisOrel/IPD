// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Utils.NodesToPrintQueue
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes;
using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.Utils
{
    internal class NodesToPrintQueue
    {
        public NodesToPrintQueue(WorkspacePagesTreeNode node, PrintParameters printParameters)
          : this(new List<WorkspacePagesTreeNode>() { node }, printParameters)
        {
        }

        public NodesToPrintQueue(List<WorkspacePagesTreeNode> nodes, PrintParameters printParameters)
        {
            this.Nodes = nodes;
            this.PrintParameters = printParameters;
        }

        public List<WorkspacePagesTreeNode> Nodes { get; set; }

        public PrintParameters PrintParameters { get; set; }
    }
}
