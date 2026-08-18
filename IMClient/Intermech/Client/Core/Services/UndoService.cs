
// Type: Intermech.Client.Core.Services.UndoService




using Intermech.Bars;
using Intermech.Docking;
using Intermech.Interfaces.Client;
using System;


namespace Intermech.Client.Core.Services
{
    internal class UndoService : IUndoService
    {
      private DropDownMenuItem undoButton;
      private DropDownMenuItem redoButton;
      private DocumentContainer documentContainer;

      public UndoService(
        DropDownMenuItem undoButton,
        DropDownMenuItem redoButton,
        DocumentContainer documentContainer)
      {
        this.undoButton = undoButton;
        this.redoButton = redoButton;
        this.undoButton.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.UndoButton_BeforePopup);
        this.redoButton.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.RedoButton_BeforePopup);
        this.documentContainer = documentContainer;
      }

      private void UndoButton_BeforePopup(object sender, MenuPopupEventArgs e)
      {
        this.undoButton.DisposeChildren();
        if (!(this.documentContainer.ActiveDocument is IUndo activeDocument))
          return;
        foreach (UndoItem undoItem in activeDocument.GetUndoItems())
        {
          MenuButtonItem menuButtonItem = new MenuButtonItem(undoItem.Caption);
          menuButtonItem.Tag = (object) undoItem;
          menuButtonItem.Click += new EventHandler(this.UndoMenu_Click);
          this.undoButton.Items.Add((ToolbarItemBase) menuButtonItem);
        }
      }

      private void RedoButton_BeforePopup(object sender, MenuPopupEventArgs e)
      {
        this.redoButton.DisposeChildren();
        if (!(this.documentContainer.ActiveDocument is IUndo activeDocument))
          return;
        foreach (UndoItem redoItem in activeDocument.GetRedoItems())
        {
          MenuButtonItem menuButtonItem = new MenuButtonItem(redoItem.Caption);
          menuButtonItem.Tag = (object) redoItem;
          menuButtonItem.Click += new EventHandler(this.RedoMenu_Click);
          this.redoButton.Items.Add((ToolbarItemBase) menuButtonItem);
        }
      }

      private void UndoMenu_Click(object sender, EventArgs e)
      {
        if (!(sender is MenuButtonItem menuButtonItem) || !(this.documentContainer.ActiveDocument is IUndo activeDocument) || !(menuButtonItem.Tag is UndoItem tag))
          return;
        activeDocument.Undo(tag);
      }

      private void RedoMenu_Click(object sender, EventArgs e)
      {
        if (!(sender is MenuButtonItem menuButtonItem) || !(this.documentContainer.ActiveDocument is IUndo activeDocument) || !(menuButtonItem.Tag is UndoItem tag))
          return;
        activeDocument.Redo(tag);
      }
    }
}
