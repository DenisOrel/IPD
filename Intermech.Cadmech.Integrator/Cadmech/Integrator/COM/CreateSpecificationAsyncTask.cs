// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.COM.CreateSpecificationAsyncTask
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Pdm;
using Intermech.Runtime.ComInterop.LocalServer;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

#nullable disable
namespace Intermech.Cadmech.Integrator.COM;

[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
[ComDefaultInterface(typeof (ICreateSpecificationAsyncTask))]
public sealed class CreateSpecificationAsyncTask : 
  SingleThreadedObject,
  ICreateSpecificationAsyncTask
{
  private readonly Task<string> internalTask;

  internal CreateSpecificationAsyncTask(Task<string> internalTask)
  {
    this.internalTask = internalTask != null ? internalTask : throw new ArgumentNullException(nameof (internalTask));
  }

  public bool IsCompleted => this.internalTask.IsCompleted;

  public bool IsFaulted => this.internalTask.IsFaulted;

  public string TryGetStructFileContent()
  {
    return !this.internalTask.IsCompleted ? (string) null : this.internalTask.Result;
  }
}
