// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.InitDataEventArgs
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;

#nullable disable
namespace Intermech.AVS;

/// <summary>Событие</summary>
[Serializable]
public class InitDataEventArgs : EventArgs
{
  public object Tag;

  public InitDataEventArgs(object tag) => this.Tag = tag;
}
