// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Utils.Events.OnModifyVirtualTreeEventArgs
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes;
using System;
using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.Utils.Events
{
    internal class OnModifyVirtualTreeEventArgs : EventArgs
    {
        public OnModifyVirtualTreeEventArgs(
          string command,
          List<PrintCenterNode> selectedNodes = null,
          object destinationNode = null)
        {
            this.Command = command;
            this.SelectedNodes = selectedNodes;
            this.DestinationNode = destinationNode;
        }

        public string Command { get; private set; }

        public object DestinationNode { get; private set; }

        public List<PrintCenterNode> SelectedNodes { get; private set; }
    }
}
