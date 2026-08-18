
// Type: Intermech.DocumentView.IDocument
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing;


namespace Intermech.DocumentView;

public interface IDocument
{
  /// <summary>
  /// The Changed event is raised whenever a document or a part of a document is modified.
  /// </summary>
  /// <remarks>
  /// Any Changed event handlers should not modify this document or any part of this document.
  /// </remarks>
  event DocumentChangedEventHandler Changed;

  [Description("The size of this document.")]
  SizeF Size { get; set; }

  [Description("The top-left corner position of this document.")]
  PointF TopLeft { get; set; }

  void RaiseChanged(
    int hint,
    int subhint,
    object obj,
    int oldI,
    object oldVal,
    RectangleF oldRect,
    int newI,
    object newVal,
    RectangleF newRect);

  void ChangeValue(DocumentChangedEventArgs e, bool undo);

  void Add(IObject obj);

  [Description("The default layer used when adding objects to the document.")]
  Layer DefaultLayer { get; set; }

  void UpdateDocumentBounds(IObject obj);

  [Browsable(false)]
  Layer[] Layers { get; }

  [Description("The color of the document's background.")]
  [Category("Appearance")]
  Color PaperColor { get; set; }

  RectangleF ComputeBounds(Layer[] layer, IView view);

  bool CanSelectObjects();

  void Clear();
}
