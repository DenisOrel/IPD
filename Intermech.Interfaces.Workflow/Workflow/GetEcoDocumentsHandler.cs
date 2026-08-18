// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.GetEcoDocumentsHandler
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces.Workflow;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow;

/// <summary>делегат для получения документов Eco</summary>
/// <param name="attachmentsDoc">Документ ECO, со списком требуемых переводов на шаги жц/уровни продвижения и соответствующих типов объектов для которых нужны эти переводы</param>
/// <returns>Возвращает список документов, при отсутствии должен возвращать пустой список</returns>
public delegate List<ResultEcoDocumentsInformation> GetEcoDocumentsHandler(
  EcoDocumentsInAttachments attachmentsDoc);
