// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapUndoManager
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.Globalization;
using System.Resources;


namespace Intermech.Map
{
    [Serializable]
    public class MapUndoManager
    {
      public const string CollapsedSubGraphName = "Collapsed SubGraph";
      public const string CopyName = "Copy";
      public const string CopySelectionName = "Copy Selection";
      public const string CutName = "Cut";
      public const string DeleteSelectionName = "Delete Selection";
      public const string DropName = "Drop";
      public const string ExpandedAllSubGraphsName = "Expanded All SubGraphs";
      public const string ExpandedSubGraphName = "Expanded SubGraph";
      public const string MoveSelectionName = "Move Selection";
      public const string NewLinkName = "New Link";
      public const string PasteName = "Paste";
      public const string RelinkName = "Relink";
      public const string ResizeName = "Resize";
      public const string TextEditName = "Text Edit";
      private bool myChecksTransactionLevel;
      private ArrayList myCompEdits;
      private int myCurrentEditIndex;
      private ArrayList myDocuments;
      private MapUndoManagerCompoundEdit myIncompleteEdit;
      private bool myIsRedoing;
      private bool myIsUndoing;
      private int myLevel;
      private int myMaximumEditCount;
      [NonSerialized]
      private ResourceManager myResourceManager;

      public MapUndoManager()
      {
        this.myCompEdits = new ArrayList();
        this.myMaximumEditCount = -1;
        this.myCurrentEditIndex = -1;
        this.myIncompleteEdit = (MapUndoManagerCompoundEdit) null;
        this.myLevel = 0;
        this.myIsUndoing = false;
        this.myIsRedoing = false;
        this.myDocuments = new ArrayList();
        this.myChecksTransactionLevel = false;
        this.myResourceManager = (ResourceManager) null;
      }

      public bool AbortTransaction() => this.EndTransaction(false, (string) null);

      public virtual void AddDocument(MapDocument doc)
      {
        if (this.myDocuments.Contains((object) doc))
          return;
        this.myDocuments.Add((object) doc);
      }

      public virtual bool CanRedo()
      {
        if (this.TransactionLevel <= 0 && !this.IsUndoing && !this.IsRedoing)
        {
          IMapUndoableEdit editToRedo = this.EditToRedo;
          if (editToRedo != null)
            return editToRedo.CanRedo();
        }
        return false;
      }

      public virtual bool CanUndo()
      {
        if (this.TransactionLevel <= 0 && !this.IsUndoing && !this.IsRedoing)
        {
          IMapUndoableEdit editToUndo = this.EditToUndo;
          if (editToUndo != null)
            return editToUndo.CanUndo();
        }
        return false;
      }

      public virtual void Clear()
      {
        for (int index = this.myCompEdits.Count - 1; index >= 0; --index)
          ((IMapUndoableEdit) this.myCompEdits[index]).Clear();
        this.myCompEdits.Clear();
        this.myCurrentEditIndex = -1;
        this.myIncompleteEdit = (MapUndoManagerCompoundEdit) null;
        this.myLevel = 0;
        this.myIsUndoing = false;
        this.myIsRedoing = false;
      }

      public virtual void DocumentChanged(object sender, MapChangedEventArgs e)
      {
        if (this.IsUndoing || this.IsRedoing || this.SkipEvent(e))
          return;
        MapUndoManagerCompoundEdit managerCompoundEdit = this.CurrentEdit;
        if (managerCompoundEdit == null)
        {
          managerCompoundEdit = new MapUndoManagerCompoundEdit();
          this.CurrentEdit = managerCompoundEdit;
        }
        MapChangedEventArgs edit = new MapChangedEventArgs(e);
        managerCompoundEdit.AddEdit((IMapUndoableEdit) edit);
        if (!this.ChecksTransactionLevel || this.TransactionLevel > 0)
          return;
        MapObject.Trace("Change not within a transaction: " + edit.ToString());
      }

      public virtual bool EndTransaction(bool commit, string pname)
      {
        if (this.myLevel > 0)
          --this.myLevel;
        MapUndoManagerCompoundEdit currentEdit = this.CurrentEdit;
        if (this.myLevel == 0 && currentEdit != null)
        {
          if (commit)
          {
            currentEdit.IsComplete = true;
            if (currentEdit.AllEdits.Count > 0)
            {
              if (pname != null)
                currentEdit.PresentationName = pname;
              for (int index = this.myCompEdits.Count - 1; index > this.myCurrentEditIndex; --index)
              {
                ((IMapUndoableEdit) this.myCompEdits[index]).Clear();
                this.myCompEdits.RemoveAt(index);
              }
              if (this.MaximumEditCount > 0 && this.myCompEdits.Count >= this.MaximumEditCount)
              {
                ((IMapUndoableEdit) this.myCompEdits[0]).Clear();
                this.myCompEdits.RemoveAt(0);
                --this.myCurrentEditIndex;
              }
              this.myCompEdits.Add((object) currentEdit);
              ++this.myCurrentEditIndex;
            }
            this.CurrentEdit = (MapUndoManagerCompoundEdit) null;
            return true;
          }
          currentEdit.Clear();
          this.CurrentEdit = (MapUndoManagerCompoundEdit) null;
        }
        return false;
      }

      public bool FinishTransaction(string tname)
      {
        return this.EndTransaction(true, this.GetPresentationName(tname));
      }

      public virtual string GetPresentationName(string tname)
      {
        if (tname == null)
          return "";
        string presentationName = (string) null;
        if (this.ResourceManager != null)
          presentationName = this.ResourceManager.GetString(tname, CultureInfo.CurrentCulture);
        if (presentationName == null)
          presentationName = tname;
        return presentationName;
      }

      public virtual void Redo()
      {
        if (!this.CanRedo())
          return;
        try
        {
          this.myIsRedoing = true;
          IMapUndoableEdit editToRedo = this.EditToRedo;
          ++this.myCurrentEditIndex;
          editToRedo.Redo();
          foreach (MapDocument document in this.Documents)
            document.InvalidateViews();
        }
        catch (Exception ex)
        {
          MapObject.Trace("Redo: " + ex.ToString());
          throw ex;
        }
        finally
        {
          this.myIsRedoing = false;
        }
      }

      public virtual void RemoveDocument(MapDocument doc) => this.myDocuments.Remove((object) doc);

      public virtual bool SkipEvent(MapChangedEventArgs evt)
      {
        return evt.Document == null || evt.Document.SkipsUndoManager || evt.Hint >= 0 && evt.Hint < 200 || evt.Hint == 901 && (evt.MapObject == null || evt.MapObject.SkipsUndoManager || evt.SubHint == 1000);
      }

      public virtual bool StartTransaction()
      {
        ++this.myLevel;
        return this.myLevel == 1;
      }

      public virtual void Undo()
      {
        if (!this.CanUndo())
          return;
        try
        {
          this.myIsUndoing = true;
          IMapUndoableEdit editToUndo = this.EditToUndo;
          --this.myCurrentEditIndex;
          editToUndo.Undo();
          foreach (MapDocument document in this.Documents)
            document.InvalidateViews();
        }
        catch (Exception ex)
        {
          MapObject.Trace("Undo: " + ex.ToString());
          throw ex;
        }
        finally
        {
          this.myIsUndoing = false;
        }
      }

      public virtual IList AllEdits => (IList) this.myCompEdits;

      public bool ChecksTransactionLevel
      {
        get => this.myChecksTransactionLevel;
        set => this.myChecksTransactionLevel = value;
      }

      public virtual MapUndoManagerCompoundEdit CurrentEdit
      {
        get => this.myIncompleteEdit;
        set => this.myIncompleteEdit = value;
      }

      public virtual IEnumerable Documents => (IEnumerable) this.myDocuments;

      public virtual IMapUndoableEdit EditToRedo
      {
        get
        {
          return this.myCurrentEditIndex < this.myCompEdits.Count - 1 ? (IMapUndoableEdit) this.myCompEdits[this.myCurrentEditIndex + 1] : (IMapUndoableEdit) null;
        }
      }

      public virtual IMapUndoableEdit EditToUndo
      {
        get
        {
          return this.myCurrentEditIndex >= 0 && this.myCurrentEditIndex <= this.myCompEdits.Count - 1 ? (IMapUndoableEdit) this.myCompEdits[this.myCurrentEditIndex] : (IMapUndoableEdit) null;
        }
      }

      public virtual bool IsRedoing => this.myIsRedoing;

      public virtual bool IsUndoing => this.myIsUndoing;

      public virtual int MaximumEditCount
      {
        get => this.myMaximumEditCount;
        set
        {
          if (value == 0)
            value = 1;
          this.myMaximumEditCount = value;
        }
      }

      public virtual string RedoPresentationName
      {
        get
        {
          IMapUndoableEdit editToRedo = this.EditToRedo;
          return editToRedo != null ? editToRedo.PresentationName : "";
        }
      }

      public virtual ResourceManager ResourceManager
      {
        get => this.myResourceManager;
        set => this.myResourceManager = value;
      }

      public virtual int TransactionLevel => this.myLevel;

      public virtual int UndoEditIndex => this.myCurrentEditIndex;

      public virtual string UndoPresentationName
      {
        get
        {
          IMapUndoableEdit editToUndo = this.EditToUndo;
          return editToUndo != null ? editToUndo.PresentationName : "";
        }
      }
    }
}
