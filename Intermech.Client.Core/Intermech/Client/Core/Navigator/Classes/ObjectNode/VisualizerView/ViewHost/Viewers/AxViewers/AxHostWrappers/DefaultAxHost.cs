
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers.DefaultAxHost
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;

/// <summary>Обертка для ActiveX контрола в .net</summary>
internal class DefaultAxHost : AxHost, IAxHost, IOpenClose
{
  private readonly string _clsid;
  private object ocx;

  public DefaultAxHost(string clsid)
    : base(clsid)
  {
    this._clsid = clsid;
  }

  protected override void AttachInterfaces() => this.ocx = this.GetOcx();

  /// <summary>Получить свойство/метод для открытия файла</summary>
  /// <returns></returns>
  private MemberInfo GetOpenFunction()
  {
    MemberInfo openFunction;
    if (OpenPropMethodHolder.GetInstance().Items.TryGetValue(this._clsid, out openFunction))
      return openFunction;
    IExtensionsService service = ServiceUtils.GetService<IExtensionsService>((object) ServicesManager.ServiceContainer, true);
    string[] strArray1 = service.Properties.Replace(" ", "").Split(';', ',', '|');
    string[] strArray2 = service.Methods.Replace(" ", "").Split(';', ',', '|');
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (DefaultAxHost.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        DefaultAxHost.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, System.Type>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (System.Type), typeof (DefaultAxHost)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, System.Type> target = DefaultAxHost.\u003C\u003Eo__4.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, System.Type>> p1 = DefaultAxHost.\u003C\u003Eo__4.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (DefaultAxHost.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        DefaultAxHost.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, System.Type, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetType", (IEnumerable<System.Type>) null, typeof (DefaultAxHost), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = DefaultAxHost.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) DefaultAxHost.\u003C\u003Eo__4.\u003C\u003Ep__0, typeof (DispatchUtility), this.ocx, true);
      System.Type type = target((CallSite) p1, obj);
      foreach (string name in strArray1)
      {
        PropertyInfo property = type.GetProperty(name);
        if (!(property == (PropertyInfo) null))
        {
          if (!OpenPropMethodHolder.GetInstance().Items.ContainsKey(this._clsid))
            OpenPropMethodHolder.GetInstance().Items.Add(this._clsid, (MemberInfo) property);
          return (MemberInfo) property;
        }
      }
      foreach (string name in strArray2)
      {
        MethodInfo method = type.GetMethod(name);
        if (!(method == (MethodInfo) null))
        {
          if (!OpenPropMethodHolder.GetInstance().Items.ContainsKey(this._clsid))
            OpenPropMethodHolder.GetInstance().Items.Add(this._clsid, (MemberInfo) method);
          return (MemberInfo) method;
        }
      }
    }
    catch (Exception ex)
    {
    }
    return (MemberInfo) null;
  }

  /// <summary>Вызвать свойство/метод для открытия</summary>
  /// <param name="fileName"></param>
  /// <param name="errorMessage"></param>
  /// <returns></returns>
  private bool Open(string fileName, out string errorMessage)
  {
    errorMessage = string.Empty;
    MemberInfo openFunction = this.GetOpenFunction();
    if ((object) openFunction != null)
    {
      switch (openFunction)
      {
        case PropertyInfo propertyInfo:
          try
          {
            // ISSUE: reference to a compiler-generated field
            if (DefaultAxHost.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              DefaultAxHost.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Action<CallSite, PropertyInfo, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetValue", (IEnumerable<System.Type>) null, typeof (DefaultAxHost), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            DefaultAxHost.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) DefaultAxHost.\u003C\u003Eo__5.\u003C\u003Ep__0, propertyInfo, this.ocx, Convert.ChangeType((object) fileName, propertyInfo.PropertyType), (object) null);
            return true;
          }
          catch (Exception ex)
          {
            errorMessage = ex.Message;
            return false;
          }
        case MethodInfo methodInfo:
          try
          {
            object[] array = ((IEnumerable<ParameterInfo>) methodInfo.GetParameters()).Select<ParameterInfo, object>((Func<ParameterInfo, object>) (methodParam => methodParam.DefaultValue)).ToArray<object>();
            if (array.Length == 0)
            {
              errorMessage = string.Format(LocalizationHolder.rm.GetString("MethodWitoutParams"), (object) methodInfo.Name);
              return false;
            }
            array[0] = (object) fileName;
            // ISSUE: reference to a compiler-generated field
            if (DefaultAxHost.\u003C\u003Eo__5.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              DefaultAxHost.\u003C\u003Eo__5.\u003C\u003Ep__1 = CallSite<Action<CallSite, MethodInfo, object, object[]>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Invoke", (IEnumerable<System.Type>) null, typeof (DefaultAxHost), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            DefaultAxHost.\u003C\u003Eo__5.\u003C\u003Ep__1.Target((CallSite) DefaultAxHost.\u003C\u003Eo__5.\u003C\u003Ep__1, methodInfo, this.ocx, array);
            return true;
          }
          catch (Exception ex)
          {
            errorMessage = ex.Message;
            return false;
          }
      }
    }
    errorMessage = LocalizationHolder.rm.GetString("MethodNotFound");
    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="fileName"></param>
  /// <returns></returns>
  public bool Open(string fileName)
  {
    string errorMessage;
    if (this.Open(fileName, out errorMessage))
      return true;
    throw new Exception(errorMessage);
  }

  /// <summary>
  /// 
  /// </summary>
  public void Close()
  {
  }

  public Control AxControl => (Control) this;

  public AxHost AxHost => (AxHost) this;
}
