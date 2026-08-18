
// Type: Intermech.Tools.Integrators.ApplicationApiSession`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Diagnostics;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует базоый класс для сессий доступа к API интегрируемого приложения.
/// </summary>
public abstract class ApplicationApiSession<TApplication> : IDisposable where TApplication : class
{
  private IApplicationApiService apiService;
  private bool isDisposed;
  private TApplication application;

  protected ApplicationApiSession(IIntegrator integrator)
    : this(ServiceUtils.GetService<IApplicationApiService>((object) integrator, true))
  {
  }

  protected ApplicationApiSession(IApplicationApiService apiService)
  {
    this.apiService = apiService != null ? apiService : throw new ArgumentNullException(nameof (apiService));
    this.apiService.OpenApiSession();
    try
    {
      this.application = (TApplication) this.apiService.GetApplicationObject();
    }
    catch
    {
      this.apiService.CloseApiSession();
      throw;
    }
  }

  public void Dispose()
  {
    if (this.isDisposed)
      return;
    this.isDisposed = true;
    this.DoDispose();
    this.application = default (TApplication);
    this.apiService.CloseApiSession();
  }

  protected virtual void DoDispose()
  {
  }

  protected void CheckNotDisposed()
  {
    if (this.isDisposed)
      throw new ObjectDisposedException(this.GetType().FullName);
  }

  public IApplicationApiService ApplicationApiService
  {
    [DebuggerStepThrough] get
    {
      this.CheckNotDisposed();
      return this.apiService;
    }
  }

  public TApplication Application
  {
    [DebuggerStepThrough] get
    {
      this.CheckNotDisposed();
      return this.application;
    }
  }
}
