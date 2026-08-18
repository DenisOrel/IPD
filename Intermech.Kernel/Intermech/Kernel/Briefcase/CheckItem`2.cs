// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckItem`2
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Briefcase;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal abstract class CheckItem<T, U> : CheckClass, ICheckItem
{
  protected U briefRow;
  protected DataSet metaData;
  protected bool isSystemGUID;
  protected T item;

  protected virtual bool nullable => false;

  public CheckItem(
    UserSession session,
    DataSet metaData,
    int category,
    U briefRow,
    CheckOptions options)
    : base(session, category, options)
  {
    this.InitializeMetadata(briefRow, metaData);
  }

  public CheckItem(
    UserSession session,
    DataSet metaData,
    string category,
    U briefRow,
    CheckOptions options)
    : base(session, category, options)
  {
    this.InitializeMetadata(briefRow, metaData);
  }

  private void InitializeMetadata(U briefRow, DataSet metaData)
  {
    this.briefRow = briefRow;
    this.metaData = metaData;
  }

  public abstract void Initialize();

  public void Check()
  {
    if (!this.nullable && (object) this.item == null)
      return;
    this.OnCheck();
  }

  protected abstract void OnCheck();

  public virtual bool Existing => (object) this.item != null;
}
