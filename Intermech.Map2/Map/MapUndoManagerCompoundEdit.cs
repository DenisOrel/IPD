// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapUndoManagerCompoundEdit
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;


namespace Intermech.Map
{
    [Serializable]
    public class MapUndoManagerCompoundEdit : IMapUndoableEdit
    {
      private ArrayList myEdits;
      private bool myIsComplete;
      private string myName;

      public MapUndoManagerCompoundEdit()
      {
        this.myEdits = new ArrayList();
        this.myIsComplete = false;
        this.myName = "";
      }

      public virtual void AddEdit(IMapUndoableEdit edit)
      {
        if (this.IsComplete)
          return;
        this.myEdits.Add((object) edit);
      }

      public virtual bool CanRedo() => this.IsComplete && this.myEdits.Count > 0;

      public virtual bool CanUndo() => this.IsComplete && this.myEdits.Count > 0;

      public virtual void Clear()
      {
        for (int index = this.myEdits.Count - 1; index >= 0; --index)
          ((IMapUndoableEdit) this.myEdits[index]).Clear();
        this.myEdits.Clear();
      }

      public virtual void Redo()
      {
        if (!this.CanRedo())
          return;
        for (int index = 0; index <= this.myEdits.Count - 1; ++index)
          ((IMapUndoableEdit) this.myEdits[index]).Redo();
      }

      public virtual void Undo()
      {
        if (!this.CanUndo())
          return;
        for (int index = this.myEdits.Count - 1; index >= 0; --index)
          ((IMapUndoableEdit) this.myEdits[index]).Undo();
      }

      public virtual IList AllEdits => (IList) this.myEdits;

      public virtual bool IsComplete
      {
        get => this.myIsComplete;
        set
        {
          if (!value)
            return;
          this.myIsComplete = true;
        }
      }

      public virtual string PresentationName
      {
        get => this.myName;
        set => this.myName = value;
      }
    }
}
