// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.WorkflowMakeBaseVersionException
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces;

/// <summary>
/// Класс для ошибки Workflow который предоставит список процессов созданных по шаблону с возможностью удаления этих процессов
/// </summary>
[Serializable]
public class WorkflowMakeBaseVersionException : KernelException
{
  private string _errorText = string.Empty;
  private string _caption = "Внимание";
  private long[] _objectsID;
  private long _schemeID = -1;

  public WorkflowMakeBaseVersionException(
    string message,
    long[] objectsID,
    long schemeID = -1,
    string caption = "Внимание")
    : base(message)
  {
    this._errorText = message;
    this._objectsID = objectsID;
    this._caption = caption;
    this._schemeID = schemeID;
  }

  protected WorkflowMakeBaseVersionException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this._caption = info.GetString(nameof (Caption));
    this._objectsID = (long[]) info.GetValue(nameof (ObjectsID), typeof (long[]));
    this._errorText = info.GetString(nameof (ErrorText));
    this._schemeID = (long) info.GetValue(nameof (SchemeID), typeof (long));
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("ObjectsID", (object) this._objectsID);
    info.AddValue("Caption", (object) this._caption);
    info.AddValue("ErrorText", (object) this._errorText);
    info.AddValue("SchemeID", this._schemeID);
  }

  public string ErrorText
  {
    get => this._errorText;
    private set => this._errorText = value;
  }

  public string Caption
  {
    get => this._caption;
    private set => this._caption = value;
  }

  public long[] ObjectsID
  {
    get => this._objectsID;
    private set => this._objectsID = value;
  }

  public long SchemeID
  {
    get => this._schemeID;
    private set => this._schemeID = value;
  }
}
