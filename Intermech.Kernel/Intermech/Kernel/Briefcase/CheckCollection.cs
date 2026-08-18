// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Briefcase;


namespace Intermech.Kernel.Briefcase;

internal abstract class CheckCollection : CheckClass
{
  public CheckCollection(UserSession session, string category, CheckOptions options)
    : base(session, category, options)
  {
  }

  public abstract void Compare();
}
