
// Type: Intermech.Interfaces.FormDesignerAction
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;


namespace Intermech.Interfaces
{
    /// <summary>Класс описывающий одиночное событие.</summary>
    [Serializable]
    public class FormDesignerAction : ICloneable, ISerializable
    {
      private Guid _actionGuid = Guid.Empty;
      private string _actionName = string.Empty;
      private IFormDesignerActionParams _actionParams;

      /// <summary>Пустое значение</summary>
      public static FormDesignerAction Empty => new FormDesignerAction(Guid.Empty, string.Empty);

      /// <summary>Глобальный идентификатор события.</summary>
      public Guid ActionGuid
      {
        get => this._actionGuid;
        set => this._actionGuid = value;
      }

      /// <summary>Наименование события.</summary>
      public string ActionName
      {
        get => this._actionName;
        set => this._actionName = value;
      }

      /// <summary>Параметры события.</summary>
      public IFormDesignerActionParams ActionParams
      {
        get => this._actionParams;
        set => this._actionParams = value;
      }

      /// <summary>Конструктор.</summary>
      /// <param name="actionGuid">Глобальный идентификатор события</param>
      /// <param name="actionName">Наименование события</param>
      public FormDesignerAction(Guid actionGuid, string actionName)
      {
        this._actionGuid = actionGuid;
        this._actionName = actionName;
        if (!(this._actionGuid == Guid.Empty))
          return;
        this._actionName = LocalizationHolder.rm.GetString("Interfaces_59");
      }

      /// <summary>Конструктор.</summary>
      /// <param name="actionGuid">Глобальный идентификатор события</param>
      /// <param name="actionName">Наименование события</param>
      /// <param name="actionParams">Параметры события</param>
      public FormDesignerAction(
        Guid actionGuid,
        string actionName,
        IFormDesignerActionParams actionParams)
        : this(actionGuid, actionName)
      {
        this._actionParams = actionParams;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
        return obj is FormDesignerAction ? this._actionGuid.Equals((obj as FormDesignerAction)._actionGuid) : base.Equals(obj);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode() => base.GetHashCode();

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override string ToString() => this._actionName;

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public object Clone()
      {
        FormDesignerAction formDesignerAction = new FormDesignerAction(this._actionGuid, this._actionName);
        if (this._actionParams is ICloneable actionParams)
          formDesignerAction._actionParams = actionParams.Clone() as IFormDesignerActionParams;
        else if (this._actionParams != null)
        {
          Type type = this._actionParams.GetType();
          formDesignerAction._actionParams = Activator.CreateInstance(type) as IFormDesignerActionParams;
        }
        else
          formDesignerAction._actionParams = (IFormDesignerActionParams) null;
        return (object) formDesignerAction;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      protected FormDesignerAction(SerializationInfo info, StreamingContext context)
      {
        this._actionGuid = (Guid) info.GetValue(nameof (ActionGuid), typeof (Guid));
        this._actionName = info.GetString(nameof (ActionName));
        if (info.MemberCount <= 2)
          return;
        string name = info.GetString("ActionParamsType");
        if (name.Equals(Type.Missing.ToString()))
          return;
        string assemblyString = info.GetString("ActionParamsAssembly");
        Type type = (Type) null;
        try
        {
          type = Assembly.Load(assemblyString).GetType(name);
        }
        catch (Exception ex)
        {
          switch (ex)
          {
            case FileNotFoundException _:
            case BadImageFormatException _:
            case FileLoadException _:
              break;
            default:
              throw;
          }
        }
        if (!(type != (Type) null))
          return;
        this._actionParams = info.GetValue(nameof (ActionParams), type) as IFormDesignerActionParams;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      public void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        info.AddValue("ActionGuid", (object) this._actionGuid);
        info.AddValue("ActionName", (object) this._actionName);
        if (this._actionParams != null)
        {
          info.AddValue("ActionParamsType", (object) this._actionParams.GetType().FullName);
          info.AddValue("ActionParamsAssembly", (object) this._actionParams.GetType().Assembly.GetName().Name);
          info.AddValue("ActionParams", (object) this._actionParams);
        }
        else
          info.AddValue("ActionParamsType", (object) Type.Missing.ToString());
      }
    }
}
