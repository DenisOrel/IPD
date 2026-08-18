// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Compare_OneError
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary> Сравнение для сортировки списка ошибок </summary>
/// 
///             Сортировка чисто по алфавиту OneError._message
public class Compare_OneError : IComparer<OneError>
{
  public int Compare(OneError oneError1, OneError oneError2)
  {
    return oneError1 == null || oneError2 == null ? 0 : string.Compare(oneError1._message, oneError2._message, StringComparison.Ordinal);
  }
}
