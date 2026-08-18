// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.SchemesView
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for SchemesView.</summary>
public class SchemesView : CompositionView
{
  public override ContentType ViewContentType => ContentType.NonFolders;

  /// <summary>
  /// Выключаем фильтрацию для схем, т.к. с новой настройкой запоминания получалось что фильтр применялся, а отменить мы его не можем т.к. нету элементов управления
  /// </summary>
  public override bool DisableFiltration => true;
}
