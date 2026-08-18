
// Type: Intermech.Client.Core.FormDesigner.Actions.SaveChangesSupport.ActionSaveChangesParams
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace Intermech.Client.Core.FormDesigner.Actions.SaveChangesSupport;

/// <summary>
/// Реализация параметров действия c поддержкой сохранения изменений
/// </summary>
[Serializable]
public class ActionSaveChangesParams : IFormDesignerActionParams, ISerializable, ICloneable
{
  /// <summary>Constructor</summary>
  public ActionSaveChangesParams() => this.Component = (object) null;

  /// <summary>
  /// Cохранение изменений на форме редактирования перед вызовом действия
  /// </summary>
  [CustomDisplayName("Attribute.Client.Core_325")]
  [CustomDescription("Attribute.Client.Core_326")]
  [TypeConverter(typeof (EnumDescConverter))]
  [DefaultValue(ActionSaveChangesMode.Ignore)]
  public ActionSaveChangesMode SaveChangesMode { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("SaveChangesMode", (int) this.SaveChangesMode);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public ActionSaveChangesParams(SerializationInfo info, StreamingContext context)
  {
    this.Component = (object) null;
    foreach (SerializationEntry serializationEntry in info)
    {
      if (serializationEntry.Name == nameof (SaveChangesMode))
      {
        try
        {
          this.SaveChangesMode = (ActionSaveChangesMode) Convert.ToInt32(serializationEntry.Value);
        }
        catch (Exception ex)
        {
          if (!(ex is FormatException))
            throw;
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  public object Component { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public virtual object Clone() => this.MemberwiseClone();
}
