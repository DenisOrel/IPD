// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.IExpertServerTask
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Expert;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Server;

public interface IExpertServerTask
{
  IExpertGlobalTable GlobalTable { get; }

  DocRecord GeneratedComplect { get; }

  IEnumerable<DocRecord> DocumentRecords { get; }

  int TaskId { get; }

  IUserSession Session { get; }

  IServiceContainer Services { get; }

  bool StartJob(bool needClone = true);

  void EndJob();

  bool IsJobRunning();

  ObjInfoCaptionItem GetObjectData(long objId);

  RelInfoItem GetRelationData(long relId);

  List<CalcAttrPair> GetNeededAttrs();

  bool IsAttrNeeded(long objId, int objTypeId, int attrTypeId);

  void AddNeededAttr(long objId, int objTypeId, int attrTypeId);

  void RemoveNeededAttr(long objId, int objTypeId, int attrTypeId);

  void ClearNeededAttrs();

  object this[CalcAttrPair attr] { get; set; }

  AttrState GetAttributeState(CalcAttrPair attr);

  void SetAttributeState(CalcAttrPair attr, AttrState newState);

  ImDocumentData GetDocTemplate(long templateId);

  bool TraceEnabled { get; set; }

  XmlNode CurrentNode { get; set; }

  void InitTraceInfo();

  XmlNode TraceAddElement(string name);

  XmlAttribute TraceAddAttribute(XmlNode node, string name, string value);

  XmlNode TraceAddText(XmlNode node, string text);

  ExpertResult CalcFormula(
    long[] objId,
    HybridRowExp row,
    TempFormula tf,
    out object result,
    long relId = 0);

  ExpertResult CalcFormulaQuiet(
    long[] objId,
    HybridRowExp row,
    TempFormula tf,
    out object result,
    long relId = 0);
}
