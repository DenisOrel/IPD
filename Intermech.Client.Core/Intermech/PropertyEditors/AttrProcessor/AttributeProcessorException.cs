
// Type: Intermech.PropertyEditors.AttrProcessor.AttributeProcessorException
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>
/// 
/// </summary>
public class AttributeProcessorException : Exception
{
  private string _additionalMsg = string.Empty;

  /// <summary>Конструктор.</summary>
  public AttributeProcessorException()
  {
  }

  public AttributeProcessorException(string message)
    : base(message)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  public string AddiotionelMsg
  {
    get => this._additionalMsg;
    set => this._additionalMsg = value;
  }
}
