// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.EditingContextsForObjectsWithComplexCompositionsAnalyzer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Linq;


namespace Intermech.Kernel;

public sealed class EditingContextsForObjectsWithComplexCompositionsAnalyzer : 
  ISearchGroupingObjectAnalyzer
{
  private EditingContextsForObjectsWithCompositionsAnalyzer _editingContextsForObjectsWithCompositionsAnalyzer;

  public EditingContextsForObjectsWithComplexCompositionsAnalyzer(
    EditingContextsForObjectsWithCompositionsAnalyzer editingContextsForObjectsWithCompositionsAnalyzer)
  {
    this._editingContextsForObjectsWithCompositionsAnalyzer = editingContextsForObjectsWithCompositionsAnalyzer != null ? editingContextsForObjectsWithCompositionsAnalyzer : throw new ArgumentNullException(nameof (editingContextsForObjectsWithCompositionsAnalyzer));
  }

  public string Name => "Поиск среди выделенных версий и в их развернутых составах";

  public int Analyze(IUserSession session, SearchGroupingObjects searchObjects)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    int num = searchObjects != null ? searchObjects.Count : throw new ArgumentNullException(nameof (searchObjects));
    SearchGroupingObjects searchObjects1 = (SearchGroupingObjects) searchObjects.Clone();
    searchObjects.Clear();
    while (searchObjects1.Count > 0)
    {
      this._editingContextsForObjectsWithCompositionsAnalyzer.Analyze(session, searchObjects1);
      foreach (SearchGroupingObject searchGroupingObject in searchObjects1.ToArray())
      {
        SearchGroupingObject tempSearchGroupingObject = searchGroupingObject;
        if (!searchObjects.Any<SearchGroupingObject>((Func<SearchGroupingObject, bool>) (o => o.ObjectID == tempSearchGroupingObject.ObjectID)))
          searchObjects.Add(tempSearchGroupingObject.ObjectID, tempSearchGroupingObject.ObjectTypeID, tempSearchGroupingObject.GroupObjectIDs);
        else
          searchObjects1.Remove(tempSearchGroupingObject);
      }
    }
    return searchObjects.Count - num;
  }
}
