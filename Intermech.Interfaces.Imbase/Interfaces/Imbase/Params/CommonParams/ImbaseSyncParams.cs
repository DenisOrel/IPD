// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Params.CommonParams.ImbaseSyncParams
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.Imbase.Params.CommonParams;

[Serializable]
public class ImbaseSyncParams
{
  public ImbaseSyncParams() => this.SourceDBParams = new SourceDBParams();

  public SourceDBParams SourceDBParams { get; set; }

  public DateTime TimePoint { get; set; } = DateTime.Now;

  public bool TerminateOnError { get; set; }

  public bool DeleteDuplicates { get; set; }

  public string PumpSettingsPath { get; set; } = string.Empty;

  [DefaultValue(0)]
  public long DefaultMeasureId { get; set; }

  public void SetData(string value, IEventLog eventLog)
  {
    try
    {
      if (string.IsNullOrEmpty(value))
        return;
      using (StringReader stringReader = new StringReader(value))
      {
        if (!(new XmlSerializer(this.GetType()).Deserialize((TextReader) stringReader) is ImbaseSyncParams imbaseSyncParams))
          return;
        this.SourceDBParams = imbaseSyncParams.SourceDBParams;
        this.TimePoint = imbaseSyncParams.TimePoint;
        this.TerminateOnError = imbaseSyncParams.TerminateOnError;
        this.DeleteDuplicates = imbaseSyncParams.DeleteDuplicates;
        this.PumpSettingsPath = imbaseSyncParams.PumpSettingsPath;
        this.DefaultMeasureId = imbaseSyncParams.DefaultMeasureId;
      }
    }
    catch (Exception ex)
    {
      eventLog.AddToTrace($"Ошибка чтения параметров синхронизации с Imbase 5.0: {ex.Message}{Environment.NewLine}{ex.StackTrace}", 0, string.Empty);
    }
  }

  public string GetData()
  {
    using (StringWriter stringWriter = new StringWriter())
    {
      new XmlSerializer(this.GetType()).Serialize((TextWriter) stringWriter, (object) this);
      return stringWriter.ToString();
    }
  }
}
