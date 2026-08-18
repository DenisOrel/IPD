// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.BaseMainWindow
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Extensions;

public class BaseMainWindow : 
  FIltratedDocControl,
  System.IServiceProvider,
  IContextAware,
  IAdvancedServiceContainer,
  IServiceContainer,
  IControlServiceContainer,
  INamedContext,
  IFiltrationClass,
  IFiltrationRuleClass,
  IEditingContextNavWindow,
  IContainerControl,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable
{
  [NotNull]
  private readonly object _syncObj = new object();
  private bool? _inDesignMode;
  [NotNull]
  private readonly ControlServiceContainer _controlServiceContainer;
  protected bool _LoadPropsFromStorageOnLoadControl;

  public BaseMainWindow()
    : this((System.IServiceProvider) null)
  {
  }

  public BaseMainWindow([CanBeNull] string contextName)
    : this((System.IServiceProvider) null, contextName)
  {
  }

  public BaseMainWindow([CanBeNull] System.IServiceProvider logicContextServices, [CanBeNull] string contextName = null)
  {
    this._controlServiceContainer = new ControlServiceContainer((Control) this, logicContextServices);
    this.ContextName = !string.IsNullOrEmpty(contextName) ? contextName : this.DefaultContextName;
    this.AddService<IWin32Window>((IWin32Window) this);
    this.AddService<INamedContext>((INamedContext) this);
    this.AddService<IFiltrationClass>((IFiltrationClass) this);
    this.AddService<IFiltrationRuleClass>((IFiltrationRuleClass) this);
    this.AddService<IEditingContextNavWindow>((IEditingContextNavWindow) this);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.RemoveService<IEditingContextNavWindow>();
      this.RemoveService<IFiltrationRuleClass>();
      this.RemoveService<IFiltrationClass>();
      this.RemoveService<INamedContext>();
      this.RemoveService<IWin32Window>();
      this._controlServiceContainer.Dispose();
    }
    base.Dispose(disposing);
  }

  protected override void OnLoad([CanBeNull] EventArgs e)
  {
    base.OnLoad(e);
    if (!this._LoadPropsFromStorageOnLoadControl || this.InDesignMode)
      return;
    this.LoadPropertiesFromStorage();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected bool InDesignMode
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (this._inDesignMode ?? (this._inDesignMode = new bool?(this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.GetParentsEnumeration(true).Any<Control>((Func<Control, bool>) (ctrl =>
      {
        ISite site = ctrl.Site;
        return site != null && site.DesignMode;
      }))))).Value;
    }
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CheckNotDisposed() => Intermech.Diagnostics.Check.NotDisposed(this.IsDisposed, this.GetType().Name);

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CheckNotInDesignMode()
  {
    Intermech.Diagnostics.Check.Assert(this.InDesignMode, "Excecuted context only for design-mode!");
  }

  protected virtual bool ReadyToProcessFirstPaint => true;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool FirstPaintWasCalled { get; private set; }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event EventHandler OnFirstPaint;

  protected virtual void FireFirstPaint()
  {
    EventHandler onFirstPaint = this.OnFirstPaint;
    if (onFirstPaint == null)
      return;
    onFirstPaint((object) this, EventArgs.Empty);
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    if (this.FirstPaintWasCalled || !this.ReadyToProcessFirstPaint)
      return;
    lock (this._syncObj)
    {
      if (this.FirstPaintWasCalled || !this.ReadyToProcessFirstPaint)
        return;
      this.FirstPaintWasCalled = true;
      this.FireFirstPaint();
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected IControlServiceContainer ServiceContainer
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IControlServiceContainer) this._controlServiceContainer;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  Control IControlServiceContainer.Control
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (Control) this;
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  System.IServiceProvider IControlServiceContainer.ParentControlServices
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._controlServiceContainer.ParentControlServices;
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public System.IServiceProvider Services
  {
    [NotNull, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (System.IServiceProvider) this._controlServiceContainer;
    }
    [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._controlServiceContainer.AdvancedProvider = value;
    }
  }

  protected override object GetService(System.Type serviceType)
  {
    return base.GetService(serviceType) ?? this._controlServiceContainer.GetService(serviceType);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  object System.IServiceProvider.GetService([NotNull] System.Type serviceType)
  {
    return this.GetService(serviceType);
  }

  [ContractAnnotation("throwExceptionIfNotFound:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T GetService<T>(bool throwExceptionIfNotFound = true, [CanBeNull] string exceptionMessageIfFail = null)
  {
    return this._controlServiceContainer.GetService<T>(throwExceptionIfNotFound, exceptionMessageIfFail);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T GetService<T>([CanBeNull] string exceptionMessageIfFail)
  {
    return this._controlServiceContainer.GetService<T>(exceptionMessageIfFail);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public System.IServiceProvider GetService<T>([NotNull] out T service, [CanBeNull] string exceptionMessageIfFail = null)
  {
    return this._controlServiceContainer.GetService<T>(out service, exceptionMessageIfFail);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetService<T>([CanBeNull] out T service)
  {
    return this._controlServiceContainer.TryGetService<T>(out service);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T EnsureInitialized<T>([CanBeNull] ref T service, [CanBeNull] string exceptionMessageIfFail = null) where T : class
  {
    return this._controlServiceContainer.EnsureInitialized<T>(ref service, exceptionMessageIfFail);
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  System.IServiceProvider IAdvancedServiceContainer.AdvancedProvider
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._controlServiceContainer.AdvancedProvider;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._controlServiceContainer.AdvancedProvider = value;
    }
  }

  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected void AddService([NotNull] System.Type serviceType, [NotNull] ServiceCreatorCallback callback, bool promote)
  {
    this._controlServiceContainer.AddService(serviceType, callback, promote);
  }

  void IServiceContainer.AddService(
    [NotNull] System.Type serviceType,
    [NotNull] ServiceCreatorCallback callback,
    bool promote)
  {
    this.AddService(serviceType, callback, promote);
  }

  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected void AddService([NotNull] System.Type serviceType, [NotNull] ServiceCreatorCallback callback)
  {
    this._controlServiceContainer.AddService(serviceType, callback);
  }

  void IServiceContainer.AddService([NotNull] System.Type serviceType, [NotNull] ServiceCreatorCallback callback)
  {
    this.AddService(serviceType, callback);
  }

  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected void AddService([NotNull] System.Type serviceType, [NotNull] object serviceInstance, bool promote)
  {
    this._controlServiceContainer.AddService(serviceType, serviceInstance, promote);
  }

  void IServiceContainer.AddService([NotNull] System.Type serviceType, [NotNull] object serviceInstance, bool promote)
  {
    this.AddService(serviceType, serviceInstance, promote);
  }

  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected void AddService([NotNull] System.Type serviceType, [NotNull] object serviceInstance)
  {
    this._controlServiceContainer.AddService(serviceType, serviceInstance);
  }

  void IServiceContainer.AddService([NotNull] System.Type serviceType, [NotNull] object serviceInstance)
  {
    this.AddService(serviceType, serviceInstance);
  }

  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected void AddService<T>([NotNull] T service, bool promote = false)
  {
    this._controlServiceContainer.AddService(typeof (T), (object) service, promote);
  }

  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected void RemoveService([NotNull] System.Type serviceType, bool promote)
  {
    this._controlServiceContainer.RemoveService(serviceType, promote);
  }

  void IServiceContainer.RemoveService([NotNull] System.Type serviceType, bool promote)
  {
    this.RemoveService(serviceType, promote);
  }

  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected void RemoveService([NotNull] System.Type serviceType)
  {
    this._controlServiceContainer.RemoveService(serviceType);
  }

  void IServiceContainer.RemoveService([NotNull] System.Type serviceType)
  {
    this.RemoveService(serviceType);
  }

  [NotNull]
  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected IServiceContainer RemoveService<T>(bool promote = false)
  {
    return this._controlServiceContainer.RemoveService<T>(promote);
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  [CustomCategory("Attribute.Client.Core_312")]
  [CustomDescription("Attribute.Client.Core_303")]
  public string ContextName { get; set; }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual string DefaultContextName => (string) null;

  public bool ShouldSerializeContextName() => this.ContextName != this.DefaultContextName;

  public void ResetContextName() => this.ContextName = this.DefaultContextName;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public INamedContext OwnerNamedContext
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._controlServiceContainer.GetService(typeof (INamedContext), false) as INamedContext;
    }
  }

  public virtual void ParseDictionaryFromFormStorage([NotNull] Dictionary<string, object> dic)
  {
  }

  public virtual void FillPropsDictionary([NotNull] Dictionary<string, object> dic)
  {
  }

  protected virtual bool LoadPropertiesFromStorage() => true;

  protected virtual void SavePropertiesToStorage()
  {
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected string ConfigName
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetControlContextName('_', true, true).LimitLength_DeleteRedundantAtStart(32 /*0x20*/);
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  [CustomCategory("Attribute.Client.Core_313")]
  [CustomDescription("Attribute.Client.Core_302")]
  public bool IsPropertiesGlobal { get; set; }
}
