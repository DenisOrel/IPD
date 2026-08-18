// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.IMDocumentEditorService
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.Model;
using Intermech.Interfaces.Client;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Document.Client;

internal class IMDocumentEditorService : IIMDocumentEditorService
{
  bool IIMDocumentEditorService.CallDocumentFormulaEditor(ref string formula)
  {
    List<Page> pageList = (List<Page>) null;
    EditSymbolForm editSymbolForm = new EditSymbolForm();
    string str = formula;
    ref string local1 = ref formula;
    ref List<Page> local2 = ref pageList;
    return editSymbolForm.Execute((ImDocument) null, ref local1, out local2) && formula != str;
  }
}
