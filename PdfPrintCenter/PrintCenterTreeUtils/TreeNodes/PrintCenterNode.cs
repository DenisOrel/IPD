// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintCenterNode
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using System.Collections.Generic;
using System.IO;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes
{
    internal abstract class PrintCenterNode
    {
        public PrintCenterNode(
          PrintCenterNode parent = null,
          string objectName = "",
          string filePath = "",
          bool addFilenameToCaption = false,
          List<PrintCenterNode> children = null)
        {
            this.AddFilenameToCaption = addFilenameToCaption;
            this.Parent = parent;
            this.FilePath = filePath;
            this.FileName = Path.GetFileNameWithoutExtension(filePath);
            this.ObjectName = objectName;
            this.Children = children;
            this.Pages = (string)null;
        }

        public bool AddFilenameToCaption { get; protected set; }

        public string FileName { get; protected set; }

        public string FilePath { get; private set; }

        public string MainColumnCaption { get; protected set; }

        public string ObjectName { get; protected set; }

        public string Pages { get; protected set; }

        public PrintCenterNode Parent { get; private set; }

        public List<PrintCenterNode> Children { get; protected set; }

        public bool IsLeaf => this.Children == null;

        public List<PrintCenterNode> Parents => this.GetParents();

        public List<PrintCenterNode> NodePath => this.GetPathToNode();

        protected abstract void SetMainColumnCaption();

        private List<PrintCenterNode> GetParents()
        {
            List<PrintCenterNode> pathToNode = this.GetPathToNode();
            pathToNode.Remove(this);
            return pathToNode;
        }

        private List<PrintCenterNode> GetPathToNode()
        {
            PrintCenterNode printCenterNode = this;
            List<PrintCenterNode> pathToNode = new List<PrintCenterNode>()
        {
          printCenterNode
        };
            for (; printCenterNode.Parent != null; printCenterNode = printCenterNode.Parent)
                pathToNode.Add(printCenterNode.Parent);
            pathToNode.Reverse();
            return pathToNode;
        }
    }
}
