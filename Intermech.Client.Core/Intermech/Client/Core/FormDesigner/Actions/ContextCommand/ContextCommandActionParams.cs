
// Type: Intermech.Client.Core.FormDesigner.Actions.ContextCommand.ContextCommandActionParams
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Actions.SaveChangesSupport;
using Intermech.Client.Core.FormDesigner.External.Classes;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.Serialization;


namespace Intermech.Client.Core.FormDesigner.Actions.ContextCommand;

/// <summary>
/// Реализация парметров действия "вызов комманды контекстного меню"
/// </summary>
[TypeConverter(typeof (ActionTypeConverter))]
[Serializable]
internal class ContextCommandActionParams : ActionSaveChangesParams
{
  /// <summary>Наименование комманды контекстного меню</summary>
  /// <remarks>Наименование, заголовок получаем через конвертер</remarks>
  private ContextCommandActionMethod _method;
  /// <summary>
  /// Позиционирование на добавляемый объект в дереве навигатора
  /// </summary>
  private bool _allowObjectSelection = true;

  /// <summary>
  /// 
  /// </summary>
  public ContextCommandActionParams()
  {
  }

  [CustomDisplayName("Attribute.Client.Core_299")]
  [Editor(typeof (ContextCommandActionMethodEditor), typeof (UITypeEditor))]
  public ContextCommandActionMethod Method
  {
    get => this._method;
    set => this._method = value;
  }

  /// <summary>
  /// Позиционирование на добавляемый объект в дереве навигатора
  /// </summary>
  [CustomDisplayName("Attribute.Client.Core_300")]
  [CustomDescription("Attribute.Client.Core_301")]
  [TypeConverter(typeof (YesNoConverter))]
  [DefaultValue(true)]
  public bool AllowObjectSelection
  {
    get => this._allowObjectSelection;
    set => this._allowObjectSelection = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("Method", (object) this._method);
    info.AddValue("AllowObjectSelection", this._allowObjectSelection);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public ContextCommandActionParams(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    if (!(info.GetValue(nameof (Method), typeof (ContextCommandActionMethod)) is ContextCommandActionMethod commandActionMethod))
      commandActionMethod = new ContextCommandActionMethod();
    this._method = commandActionMethod;
    foreach (SerializationEntry serializationEntry in info)
    {
      if (serializationEntry.Name == nameof (AllowObjectSelection))
        this._allowObjectSelection = Convert.ToBoolean(serializationEntry.Value);
    }
  }
}
