// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.StartPageEnumerator
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Класс для промотки страниц в документах. Проходит только по стартовым страницам в цепочках разбивки.
/// Например общие и переменные данные, блоки исполнений в СП</summary>
public class StartPageEnumerator : PageEnumerator
{
  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец страниц в пределах которой нужно проматывать страницы</param>
  public StartPageEnumerator(DocumentTreeNode owner)
    : base(owner)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец страниц в пределах которой нужно проматывать страницы</param>
  /// <param name="startPage">Страница на которую установится энумератор после первого MoveNext</param>
  public StartPageEnumerator(DocumentTreeNode owner, PageData startPage)
    : base(owner, startPage)
  {
  }

  public override bool MoveNext()
  {
    if (this.current != null)
      this.current = this.current.FindLastPage();
    return base.MoveNext();
  }

  /// <summary>Вставить в следующую позицию и сделать её текущей.
  /// <remarks>Вставляет на том же уровне. Если сделать сначала MoveNext, то позиция может оказаться на другом уровне иерархии.</remarks>
  /// </summary>
  public void InsertAtNextPosition(PageData page)
  {
    if (this.current == null)
      this.MoveNext();
    if (this.current != null)
      this.current = this.current.FindLastPage();
    DocumentTreeNode documentTreeNode;
    int num;
    if (this.current != null)
    {
      documentTreeNode = this.current.Parent;
      num = this.current.Index;
    }
    else
    {
      documentTreeNode = this.pagesOwner;
      num = -1;
    }
    documentTreeNode.InsertChildNode(num + 1, (DocumentTreeNode) page, false, false, false, false);
    this.current = page;
  }

  /// <summary>Вставить в следующую позицию и сделать её текущей.
  /// <remarks>Вставляет на том же уровне. Если сделать сначала MoveNext, то позиция может оказаться на другом уровне иерархии.</remarks>
  /// </summary>
  public void InsertAtCurrentPosition(PageData page)
  {
    if (this.current == null)
      this.MoveNext();
    DocumentTreeNode documentTreeNode;
    int index;
    if (this.current != null)
    {
      documentTreeNode = this.current.Parent;
      index = this.current.Index;
    }
    else
    {
      documentTreeNode = this.pagesOwner;
      index = -1;
    }
    documentTreeNode.InsertChildNode(index, (DocumentTreeNode) page, false, false, false, false);
    this.current = page;
  }
}
