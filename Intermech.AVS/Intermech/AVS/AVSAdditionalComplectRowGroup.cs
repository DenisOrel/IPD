// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSAdditionalComplectRowGroup
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Document;

#nullable disable
namespace Intermech.AVS;

public class AVSAdditionalComplectRowGroup : AVSRowGroup
{
  /// <summary>Конструктор</summary>
  /// <param name="avsDocument">Спецификация владелец подраздела</param>
  public AVSAdditionalComplectRowGroup(AVSDocument avsDocument)
    : base(avsDocument)
  {
    this.nodeLevel = Chapter.AdditionalComplectGroup_TypeName;
  }

  public override void UpdateChapterCaption() => base.UpdateChapterCaption();

  public override bool IsEmpty => this.Rows.Count == 0;

  public bool RemoveEmpty => true;

  public override string Caption
  {
    get => "Примечание";
    set => base.Caption = value;
  }

  public AVSAdditionalComplectRowGroup()
  {
    this.nodeLevel = Chapter.AdditionalComplectGroup_TypeName;
  }

  public override TableData GetDocNodeTemplate() => base.GetDocNodeTemplate();

  public override void UpdateViewNodes(
    SkipLinesSchema skipLinesSchema,
    bool reCreateDocNode,
    bool reCreateListNode,
    bool updateCountB,
    bool createForEmptyChapters,
    bool updateTemplate,
    EmptyRowUpdateMode updateMode)
  {
    base.UpdateViewNodes(skipLinesSchema, reCreateDocNode, reCreateListNode, updateCountB, createForEmptyChapters, updateTemplate, updateMode);
    foreach (VisualNode docNode in this.DocNodes)
      docNode.SetVisible(this.avsDocument.AVSCommonPropertiesSchema.ShowAdditionalComplects, false, false, false, true);
  }

  public override AVRowGroupPosition GroupPosition
  {
    get => AVRowGroupPosition.AfterRowsGroup;
    set => base.GroupPosition = value;
  }
}
