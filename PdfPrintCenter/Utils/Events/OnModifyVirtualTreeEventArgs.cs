
// Type: Intermech.PdfPrintCenter.Utils.Events.OnModifyVirtualTreeEventArgs




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
