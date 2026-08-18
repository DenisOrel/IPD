// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.GlobalIndex.Among
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel.GlobalIndex;

internal class Among
{
  public readonly int s_size;
  public readonly char[] s;
  public readonly int substring_i;
  public readonly int result;
  public readonly Among.boolDel method;

  public Among(string s, int substring_i, int result, Among.boolDel linkMethod)
  {
    this.s_size = s.Length;
    this.s = s.ToCharArray();
    this.substring_i = substring_i;
    this.result = result;
    this.method = linkMethod;
  }

  public delegate bool boolDel();
}
