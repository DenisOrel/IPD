
// Type: Intermech.Search.UndeterminedAttributeEditingState
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using System;


namespace Intermech.Search;

public sealed class UndeterminedAttributeEditingState : AttributeEditingState
{
  public static readonly UndeterminedAttributeEditingState Instance = new UndeterminedAttributeEditingState();

  private UndeterminedAttributeEditingState()
  {
  }

  public override void AcceptChanges(AttributeEditingComponent component)
  {
  }

  public override void InitializeEditor(AttributeEditingComponent component)
  {
    if (component == null)
      throw new ArgumentNullException(nameof (component));
    if (component.NodeColumn != null)
    {
      switch (NodeColumnHelper.GetAttributeSourceType(component.NodeColumn))
      {
        case AttributeSourceTypes.Object:
          component.SetState((AttributeEditingState) ObjectAttributeEditingState.Instance);
          component.InitializeEditor();
          break;
        case AttributeSourceTypes.Relation:
          component.SetState((AttributeEditingState) RelationAttributeEditingState.Instance);
          component.InitializeEditor();
          break;
        default:
          component.SetUndetermined();
          break;
      }
    }
    else
      component.SetUndetermined();
  }
}
