// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMObjectNotFoundException
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

#nullable disable
namespace Intermech.Tools.PDMTree;

internal sealed class PDMObjectNotFoundException(string message) : PDMSystemException(message)
{
}
