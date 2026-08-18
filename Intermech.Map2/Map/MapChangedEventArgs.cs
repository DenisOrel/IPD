// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapChangedEventArgs
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.Drawing;
using System.Globalization;


namespace Intermech.Map
{
    [Serializable]
    public class MapChangedEventArgs : EventArgs, IMapUndoableEdit
    {
      private MapDocument myDocument;
      private int myHint;
      private bool myIsBeforeChanging;
      private int myNewInt;
      private RectangleF myNewRect;
      private object myNewValue;
      private object myObject;
      private int myOldInt;
      private RectangleF myOldRect;
      private object myOldValue;
      private int mySubHint;

      public MapChangedEventArgs()
      {
      }

      public MapChangedEventArgs(MapChangedEventArgs e)
      {
        this.myIsBeforeChanging = e.IsBeforeChanging;
        this.myDocument = e.Document;
        this.myHint = e.Hint;
        this.mySubHint = e.SubHint;
        this.myObject = e.Object;
        this.myOldInt = e.OldInt;
        this.myOldValue = e.OldValue;
        this.myOldRect = e.OldRect;
        this.myNewInt = e.NewInt;
        this.myNewValue = e.NewValue;
        this.myNewRect = e.NewRect;
        if (this.myDocument == null)
          return;
        this.myDocument.CopyOldValueForUndo(this);
        this.myDocument.CopyNewValueForRedo(this);
      }

      public bool CanRedo() => !this.IsBeforeChanging && this.Document != null;

      public bool CanUndo() => !this.IsBeforeChanging && this.Document != null;

      public void Clear()
      {
        this.myDocument = (MapDocument) null;
        this.myObject = (object) null;
        this.myOldValue = (object) null;
        this.myNewValue = (object) null;
      }

      public MapChangedEventArgs FindBeforeChangingEdit()
      {
        if (!this.IsBeforeChanging)
        {
          MapDocument document = this.Document;
          if (document == null)
            return (MapChangedEventArgs) null;
          MapUndoManager undoManager = document.UndoManager;
          if (undoManager == null)
            return (MapChangedEventArgs) null;
          MapUndoManagerCompoundEdit currentEdit = undoManager.CurrentEdit;
          if (currentEdit == null)
            return (MapChangedEventArgs) null;
          IList allEdits = currentEdit.AllEdits;
          for (int index = allEdits.Count - 1; index >= 0; --index)
          {
            if (allEdits[index] is MapChangedEventArgs beforeChangingEdit && beforeChangingEdit.IsBeforeChanging && beforeChangingEdit.Document == this.Document && beforeChangingEdit.Hint == this.Hint && beforeChangingEdit.SubHint == this.SubHint && beforeChangingEdit.Object == this.Object)
              return beforeChangingEdit;
          }
        }
        return (MapChangedEventArgs) null;
      }

      public float GetFloat(bool undo) => undo ? this.OldRect.X : this.NewRect.X;

      public int GetInt(bool undo) => undo ? this.OldInt : this.NewInt;

      public PointF GetPoint(bool undo)
      {
        return undo ? new PointF(this.OldRect.X, this.OldRect.Y) : new PointF(this.NewRect.X, this.NewRect.Y);
      }

      public RectangleF GetRect(bool undo) => undo ? this.OldRect : this.NewRect;

      public SizeF GetSize(bool undo)
      {
        return undo ? new SizeF(this.OldRect.Width, this.OldRect.Height) : new SizeF(this.NewRect.Width, this.NewRect.Height);
      }

      public object GetValue(bool undo) => undo ? this.OldValue : this.NewValue;

      public void Redo()
      {
        if (!this.CanRedo())
          return;
        this.Document.ChangeValue(this, false);
      }

      public override string ToString()
      {
        string str1 = $"{this.PresentationName}: {this.SubHint.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo)}";
        if (this.Object != null)
          str1 = $"{str1} {this.Object.ToString()}";
        if (this.IsBeforeChanging)
          str1 += " (before)";
        if (this.OldInt != 0)
          str1 = $"{str1} {this.OldInt.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo)}";
        if (this.OldValue != null)
          str1 = $"{str1} ({this.OldValue.ToString()})";
        float num;
        if (this.OldRect != RectangleF.Empty)
        {
          string str2 = str1;
          string[] strArray = new string[10];
          strArray[0] = str2;
          strArray[1] = " [";
          num = this.OldRect.X;
          strArray[2] = num.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo);
          strArray[3] = ",";
          num = this.OldRect.Y;
          strArray[4] = num.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo);
          strArray[5] = " ";
          num = this.OldRect.Width;
          strArray[6] = num.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo);
          strArray[7] = "x";
          num = this.OldRect.Height;
          strArray[8] = num.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo);
          strArray[9] = "]";
          str1 = string.Concat(strArray);
        }
        string str3 = str1 + " --> ";
        if (this.NewInt != 0)
          str3 = $"{str3} {this.NewInt.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo)}";
        if (this.NewValue != null)
          str3 = $"{str3} ({this.NewValue.ToString()})";
        if (this.NewRect != RectangleF.Empty)
        {
          string str4 = str3;
          string[] strArray = new string[10];
          strArray[0] = str4;
          strArray[1] = " [";
          num = this.NewRect.X;
          strArray[2] = num.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo);
          strArray[3] = ",";
          num = this.NewRect.Y;
          strArray[4] = num.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo);
          strArray[5] = " ";
          num = this.NewRect.Width;
          strArray[6] = num.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo);
          strArray[7] = "x";
          num = this.NewRect.Height;
          strArray[8] = num.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo);
          strArray[9] = "]";
          str3 = string.Concat(strArray);
        }
        return str3;
      }

      public void Undo()
      {
        if (!this.CanUndo())
          return;
        this.Document.ChangeValue(this, true);
      }

      public MapDocument Document
      {
        get => this.myDocument;
        set => this.myDocument = value;
      }

      public MapObject MapObject => this.myObject as MapObject;

      public int Hint
      {
        get => this.myHint;
        set => this.myHint = value;
      }

      public bool IsBeforeChanging
      {
        get => this.myIsBeforeChanging;
        set => this.myIsBeforeChanging = value;
      }

      public int NewInt
      {
        get => this.myNewInt;
        set => this.myNewInt = value;
      }

      public RectangleF NewRect
      {
        get => this.myNewRect;
        set => this.myNewRect = value;
      }

      public object NewValue
      {
        get => this.myNewValue;
        set => this.myNewValue = value;
      }

      public object Object
      {
        get => this.myObject;
        set => this.myObject = value;
      }

      public int OldInt
      {
        get => this.myOldInt;
        set => this.myOldInt = value;
      }

      public RectangleF OldRect
      {
        get => this.myOldRect;
        set => this.myOldRect = value;
      }

      public object OldValue
      {
        get => this.myOldValue;
        set => this.myOldValue = value;
      }

      public string PresentationName
      {
        get => this.myHint.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      }

      public int SubHint
      {
        get => this.mySubHint;
        set => this.mySubHint = value;
      }
    }
}
