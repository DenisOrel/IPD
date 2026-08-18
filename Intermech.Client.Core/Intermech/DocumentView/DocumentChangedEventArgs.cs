
// Type: Intermech.DocumentView.DocumentChangedEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Globalization;


namespace Intermech.DocumentView;

/// <summary>
/// Holds information both for <see cref="E:Intermech.Map.MapDocument.Changed" /> events and
/// for undo and redo handling in the undo manager.
/// </summary>
[Serializable]
public class DocumentChangedEventArgs : EventArgs
{
  private IDocument myDocument;
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

  /// <summary>
  /// The constructor produces an empty object, describing no event.
  /// </summary>
  public DocumentChangedEventArgs()
  {
  }

  /// <summary>
  /// This copy constructor makes a copy of the argument object.
  /// </summary>
  /// <param name="e"></param>
  public DocumentChangedEventArgs(DocumentChangedEventArgs e)
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
    IDocument document = this.myDocument;
  }

  /// <summary>
  /// This predicate returns true if you can call <see cref="M:Intermech.Map.MapChangedEventArgs.Redo" />.
  /// </summary>
  /// <returns></returns>
  public bool CanRedo() => !this.IsBeforeChanging && this.Document != null;

  /// <summary>
  /// This predicate returns true if you can call <see cref="M:Intermech.Map.MapChangedEventArgs.Undo" />.
  /// </summary>
  /// <returns></returns>
  public bool CanUndo() => !this.IsBeforeChanging && this.Document != null;

  /// <summary>Forget any references that this object may have.</summary>
  public void Clear()
  {
    this.myDocument = (IDocument) null;
    this.myObject = (object) null;
    this.myOldValue = (object) null;
    this.myNewValue = (object) null;
  }

  /// <summary>
  /// If <paramref name="undo" /> is true, this returns the <c>X</c> part of <see cref="P:Intermech.Map.MapChangedEventArgs.OldRect" />,
  /// otherwise it returns the <c>X</c> part of <see cref="P:Intermech.Map.MapChangedEventArgs.NewRect" />.
  /// </summary>
  /// <param name="undo"></param>
  /// <returns>A <c>float</c></returns>
  public float GetFloat(bool undo) => undo ? this.OldRect.X : this.NewRect.X;

  /// <summary>
  /// If <paramref name="undo" /> is true, this returns <see cref="P:Intermech.Map.MapChangedEventArgs.OldInt" />,
  /// otherwise it returns <see cref="P:Intermech.Map.MapChangedEventArgs.NewInt" />.
  /// </summary>
  /// <param name="undo"></param>
  /// <returns>An <c>int</c></returns>
  public int GetInt(bool undo) => undo ? this.OldInt : this.NewInt;

  /// <summary>
  /// If <paramref name="undo" /> is true, this returns the <c>Location</c> part of <see cref="P:Intermech.Map.MapChangedEventArgs.OldRect" />,
  /// otherwise it returns the <c>Location</c> part of <see cref="P:Intermech.Map.MapChangedEventArgs.NewRect" />.
  /// </summary>
  /// <param name="undo"></param>
  /// <returns>A <c>PointF</c></returns>
  public PointF GetPoint(bool undo)
  {
    return undo ? new PointF(this.OldRect.X, this.OldRect.Y) : new PointF(this.NewRect.X, this.NewRect.Y);
  }

  /// <summary>
  /// If <paramref name="undo" /> is true, this returns <see cref="P:Intermech.Map.MapChangedEventArgs.OldRect" />,
  /// otherwise it returns <see cref="P:Intermech.Map.MapChangedEventArgs.NewRect" />.
  /// </summary>
  /// <param name="undo"></param>
  /// <returns>A <c>RectangleF</c></returns>
  public RectangleF GetRect(bool undo) => undo ? this.OldRect : this.NewRect;

  /// <summary>
  /// If <paramref name="undo" /> is true, this returns the <c>Size</c> part of <see cref="P:Intermech.Map.MapChangedEventArgs.OldRect" />,
  /// otherwise it returns the <c>Size</c> part of <see cref="P:Intermech.Map.MapChangedEventArgs.NewRect" />.
  /// </summary>
  /// <param name="undo"></param>
  /// <returns>A <c>SizeF</c></returns>
  public SizeF GetSize(bool undo)
  {
    return undo ? new SizeF(this.OldRect.Width, this.OldRect.Height) : new SizeF(this.NewRect.Width, this.NewRect.Height);
  }

  /// <summary>
  /// If <paramref name="undo" /> is true, this returns <see cref="P:Intermech.Map.MapChangedEventArgs.OldValue" />,
  /// otherwise it returns <see cref="P:Intermech.Map.MapChangedEventArgs.NewValue" />.
  /// </summary>
  /// <param name="undo"></param>
  /// <returns>An <c>Object</c></returns>
  public object GetValue(bool undo) => undo ? this.OldValue : this.NewValue;

  /// <summary>
  /// Re-perform the document change after an <see cref="M:Intermech.Map.MapChangedEventArgs.Undo" />
  /// by calling <see cref="M:Intermech.Map.MapDocument.ChangeValue(Intermech.Map.MapChangedEventArgs,System.Boolean)" />.
  /// </summary>
  public void Redo()
  {
    if (!this.CanRedo())
      return;
    this.Document.ChangeValue(this, false);
  }

  /// <summary>
  /// Produce a description that may be useful in debugging event handling and the undo manager.
  /// </summary>
  /// <returns></returns>
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
    if (this.OldRect != new RectangleF())
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
    if (this.NewRect != new RectangleF())
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

  /// <summary>
  /// Reverse the effects of this document change
  /// by calling <see cref="M:Intermech.Map.MapDocument.ChangeValue(Intermech.Map.MapChangedEventArgs,System.Boolean)" />.
  /// </summary>
  public void Undo()
  {
    if (!this.CanUndo())
      return;
    this.Document.ChangeValue(this, true);
  }

  /// <summary>
  /// Gets or sets the document that raised the Changed event described
  /// by this <c>EventArgs</c>.
  /// </summary>
  /// <remarks>
  /// This value must be the same as the <see cref="T:Intermech.Map.MapDocument" /> <c>sender</c>
  /// of a <see cref="E:Intermech.Map.MapDocument.Changed" /> event.
  /// </remarks>
  public IDocument Document
  {
    get => this.myDocument;
    set => this.myDocument = value;
  }

  public IObject IObject => this.myObject as IObject;

  /// <summary>
  /// Gets or sets the general category of document Changed event.
  /// </summary>
  /// <remarks>
  /// Predefined GoDocument, GoLayerCollection, and GoLayer hints
  /// range from zero to one thousand.
  /// See the complete list in the documentation for <see cref="M:Intermech.Map.MapDocument.RaiseChanged(System.Int32,System.Int32,System.Object,System.Int32,System.Object,System.Drawing.RectangleF,System.Int32,System.Object,System.Drawing.RectangleF)" />.
  /// One frequently used hint is <see cref="F:Intermech.Map.MapLayer.ChangedObject" />,
  /// which uses many different subhints describing the individual
  /// </remarks>
  public int Hint
  {
    get => this.myHint;
    set => this.myHint = value;
  }

  /// <summary>
  /// Gets or sets whether this event args/undoable edit was created by
  /// a document Changed event that represents a call to
  /// <see cref="M:Intermech.Map.MapDocument.RaiseChanging(System.Int32,System.Int32,System.Object)" /> or by a call to
  /// <see cref="M:Intermech.Map.MapDocument.RaiseChanged(System.Int32,System.Int32,System.Object,System.Int32,System.Object,System.Drawing.RectangleF,System.Int32,System.Object,System.Drawing.RectangleF)" />.
  /// </summary>
  public bool IsBeforeChanging
  {
    get => this.myIsBeforeChanging;
    set => this.myIsBeforeChanging = value;
  }

  /// <summary>
  /// Gets or sets the new integer value information for a change.
  /// </summary>
  public int NewInt
  {
    get => this.myNewInt;
    set => this.myNewInt = value;
  }

  /// <summary>
  /// Gets or sets the new float, PositionF, SizeF, or RectangleF value
  /// information for a change.
  /// </summary>
  public RectangleF NewRect
  {
    get => this.myNewRect;
    set => this.myNewRect = value;
  }

  /// <summary>
  /// Gets or sets the new arbitrary object value information for a change.
  /// </summary>
  public object NewValue
  {
    get => this.myNewValue;
    set => this.myNewValue = value;
  }

  /// <summary>
  /// Gets or sets the object that was changed by the document Changed event.
  /// </summary>
  /// <remarks>
  /// This may be null when the <see cref="P:Intermech.Map.MapChangedEventArgs.Hint" /> implies the object, such
  /// as for property changes on the document itself.
  /// </remarks>
  public object Object
  {
    get => this.myObject;
    set => this.myObject = value;
  }

  /// <summary>
  /// Gets or sets the previous or old integer value information for a change.
  /// </summary>
  public int OldInt
  {
    get => this.myOldInt;
    set => this.myOldInt = value;
  }

  /// <summary>
  /// Gets or sets the previous or old float, PositionF, SizeF, or RectangleF
  /// value information for a change.
  /// </summary>
  public RectangleF OldRect
  {
    get => this.myOldRect;
    set => this.myOldRect = value;
  }

  /// <summary>
  /// Gets or sets the previous or old arbitrary object value information
  /// for a change.
  /// </summary>
  public object OldValue
  {
    get => this.myOldValue;
    set => this.myOldValue = value;
  }

  /// <summary>
  /// Gets the user-visible string description of this undoable edit.
  /// </summary>
  /// <remarks>Currently this is just the hint number, as a string.</remarks>
  public string PresentationName
  {
    get => this.myHint.ToString((IFormatProvider) CultureInfo.CurrentCulture);
  }

  /// <summary>
  /// Gets or sets the more detailed kind of document Changed event, depending
  /// on the particular  value.
  /// </summary>
  /// <remarks>
  /// This property is commonly used to describe changes to individual objects
  /// when the  is ,
  /// for example .
  /// See the complete list of predefined subhints for
  /// changes in the documentation for .
  /// However other  values may use this <c>SubHint</c> property for
  /// additional disambiguation too.
  /// </remarks>
  public int SubHint
  {
    get => this.mySubHint;
    set => this.mySubHint = value;
  }
}
