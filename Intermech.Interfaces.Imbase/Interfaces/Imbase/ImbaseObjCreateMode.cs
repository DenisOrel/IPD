// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.ImbaseObjCreateMode
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>Imbase creation object modes</summary>
public enum ImbaseObjCreateMode
{
  /// <summary>Unknown state</summary>
  iocmUnknown,
  /// <summary>Create new object</summary>
  iocmCreateNew,
  /// <summary>Don't create new if already exists</summary>
  iocmUseExists,
}
