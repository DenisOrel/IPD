// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.EmptyRowUpdateMode
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS;

/// <summary>Режим обновления записей с пустыми строками для формы Б</summary>
public enum EmptyRowUpdateMode
{
  /// <summary>Не менять</summary>
  DontChange,
  /// <summary>Создавать строки для пустых количеств</summary>
  Create,
  /// <summary>Удалять строки для пустых количеств</summary>
  Delete,
}
