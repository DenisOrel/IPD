// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.EditingContextsForAllObjectVersionsWithCompositionsAnalyzer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Kernel;

public sealed class EditingContextsForAllObjectVersionsWithCompositionsAnalyzer : 
  ISearchGroupingObjectAnalyzer
{
  private EditingContextsForAllObjectVersionsAnalyzer _editingContextsForAllObjectVersionsAnalyzer;
  private EditingContextsForObjectsWithCompositionsAnalyzer _editingContextsForObjectsWithCompositionsAnalyzer;

  public EditingContextsForAllObjectVersionsWithCompositionsAnalyzer(
    EditingContextsForAllObjectVersionsAnalyzer editingContextsForAllObjectVersionsAnalyzer,
    EditingContextsForObjectsWithCompositionsAnalyzer editingContextsForObjectsWithCompositionsAnalyzer)
  {
    if (editingContextsForAllObjectVersionsAnalyzer == null)
      throw new ArgumentNullException(nameof (editingContextsForAllObjectVersionsAnalyzer));
    if (editingContextsForObjectsWithCompositionsAnalyzer == null)
      throw new ArgumentNullException(nameof (editingContextsForObjectsWithCompositionsAnalyzer));
    this._editingContextsForAllObjectVersionsAnalyzer = editingContextsForAllObjectVersionsAnalyzer;
    this._editingContextsForObjectsWithCompositionsAnalyzer = editingContextsForObjectsWithCompositionsAnalyzer;
  }

  public string Name => "Поиск среди выделенных объектов и в их составах первого уровня";

  public int Analyze(IUserSession userSession, SearchGroupingObjects searchGroupingObjects)
  {
    if (userSession == null)
      throw new ArgumentNullException(nameof (userSession));
    if (searchGroupingObjects == null)
      throw new ArgumentNullException(nameof (searchGroupingObjects));
    this._editingContextsForAllObjectVersionsAnalyzer.Analyze(userSession, searchGroupingObjects);
    return this._editingContextsForObjectsWithCompositionsAnalyzer.Analyze(userSession, searchGroupingObjects);
  }
}
