
// Type: Intermech.UI.Wpf.Controls.FindReplaceTextEditorAdapter
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System.Windows.Documents;


namespace Intermech.UI.Wpf.Controls;

/// <summary>
/// Базовый класс адаптера для элемента редактирования текста, предоставляющий доступ к API для
/// поиска и замены фрагментов текста. Используется в <see cref="T:Intermech.UI.Wpf.Controls.FindReplaceManager" /> для
/// интеграции с UI.
/// </summary>
public abstract class FindReplaceTextEditorAdapter
{
  protected TextPointer GetPoint(TextPointer start, int x)
  {
    TextPointer positionAtOffset = start.GetPositionAtOffset(x);
    while (new TextRange(start, positionAtOffset).Text.Length < x && positionAtOffset.GetPositionAtOffset(1, LogicalDirection.Forward) != null)
      positionAtOffset = positionAtOffset.GetPositionAtOffset(1, LogicalDirection.Forward);
    return positionAtOffset;
  }

  protected int GetPos(TextPointer start, TextPointer p) => new TextRange(start, p).Text.Length;
}
