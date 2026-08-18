// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.LCInfoList
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Workflow;

public class LCInfoList : List<LCInfo>, IValidatedItem
{
  public bool Modified;
  private bool _writeGuids;

  public void Load(IDBAttribute attr) => this.AsString = attr.Value.ToString();

  public void Save(IDBAttribute attr) => attr.Value = (object) this.AsString;

  public void LoadFromStream(Stream stream)
  {
    this.Clear();
    if (stream.Length == 0L)
      return;
    XmlTextReader reader = new XmlTextReader(stream);
    int content = (int) reader.MoveToContent();
    reader.MoveToAttribute("Count");
    if (reader.ReadAttributeValue())
    {
      int int32 = Convert.ToInt32(reader.Value);
      reader.Read();
      for (int index = 0; index < int32; ++index)
      {
        reader.ReadStartElement("LC" + (index + 1).ToString());
        LCInfo lcInfo = new LCInfo();
        lcInfo.Load(reader);
        this.Add(lcInfo);
        reader.ReadEndElement();
      }
    }
    reader.Close();
    this.Modified = false;
  }

  /// <summary>Используется при сохранении в портфель</summary>
  public bool WriteGuids
  {
    get => this._writeGuids || this.Invalid;
    set => this._writeGuids = value;
  }

  public void SaveToStream(Stream stream)
  {
    if (this.Count == 0)
      return;
    XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
    writer.Formatting = Formatting.Indented;
    writer.WriteStartElement("LC");
    writer.WriteAttributeString("Count", this.Count.ToString());
    bool writeGuids = this.WriteGuids;
    for (int index = 0; index < this.Count; ++index)
    {
      writer.WriteStartElement("LC" + (index + 1).ToString());
      this[index].Save(writer, writeGuids);
      writer.WriteEndElement();
    }
    writer.WriteEndElement();
    writer.Flush();
  }

  public string AsString
  {
    get
    {
      MemoryStream memoryStream = new MemoryStream();
      this.SaveToStream((Stream) memoryStream);
      memoryStream.Position = 0L;
      StreamReader streamReader = new StreamReader((Stream) memoryStream);
      try
      {
        return streamReader.ReadToEnd();
      }
      finally
      {
        streamReader.Close();
        memoryStream.Close();
      }
    }
    set
    {
      MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(value));
      try
      {
        this.LoadFromStream((Stream) memoryStream);
        memoryStream.Close();
      }
      finally
      {
        memoryStream.Close();
      }
    }
  }

  public bool Invalid
  {
    get
    {
      foreach (LCInfo lcInfo in (List<LCInfo>) this)
      {
        if (lcInfo.Invalid)
          return true;
      }
      return false;
    }
  }

  public LCInfoList Filter(LCExec execTime)
  {
    LCInfoList lcInfoList = new LCInfoList();
    foreach (LCInfo lcInfo in (List<LCInfo>) this)
    {
      if (lcInfo.ExecTime == execTime)
        lcInfoList.Add(lcInfo);
    }
    return lcInfoList;
  }

  public Dictionary<Domain, List<long>> ObjectIDs
  {
    get
    {
      Dictionary<Domain, List<long>> objectIds = new Dictionary<Domain, List<long>>();
      List<long> longList1 = new List<long>();
      List<long> longList2 = new List<long>();
      List<long> longList3 = new List<long>();
      foreach (LCInfo lcInfo in (List<LCInfo>) this)
      {
        long objectType = (long) lcInfo.ObjectType;
        if (objectType != -1L && !longList1.Contains(objectType))
          longList1.Add(objectType);
        List<long> longList4 = lcInfo.Kind == LCKind.Step ? longList3 : longList2;
        long stepId = (long) lcInfo.StepID;
        if (!longList4.Contains(stepId))
          longList4.Add(stepId);
      }
      objectIds[Domain.Steps] = longList3;
      objectIds[Domain.ObjectTypes] = longList1;
      objectIds[Domain.Levels] = longList2;
      return objectIds;
    }
  }
}
