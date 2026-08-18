// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Validate.IsEmptyDocument.IsEmptyDocumentFlowAction
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Interfaces.Document;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Validate.IsEmptyDocument;

public class IsEmptyDocumentFlowAction : IValidateDocumentAction
{
  public bool Validate(ImDocumentData documentData)
  {
    foreach (FlowID documentFlow in documentData.DocumentFlows)
    {
      IFlowElement flowElementByName = (IFlowElement) null;
      if (documentData.FindFirstFlowElement(documentFlow, ref flowElementByName) is TableData firstFlowElement && firstFlowElement.Nodes.Count > 0)
        return false;
    }
    return true;
  }
}
