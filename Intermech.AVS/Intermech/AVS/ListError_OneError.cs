// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ListError_OneError
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.UI;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary> Класс с описаниями ошибок </summary>
/// 
///             Каждая ошибка как OneError
public class ListError_OneError
{
  private Compare_OneError _compare_OneError = new Compare_OneError();
  public List<OneError> _list = new List<OneError>();
  public List<string> _listStr = new List<string>();

  public void Clear() => this._list.Clear();

  public void Sort() => this._list.Sort((IComparer<OneError>) this._compare_OneError);

  /// <summary> Объединение записей с полностью одинаковыми описаниями ошибок </summary>
  public void Union()
  {
    if (this._list == null || this._list.Count < 2)
      return;
    for (int index = this._list.Count - 1; index > 0; --index)
    {
      OneError oneError1 = this._list[index];
      OneError oneError2 = this._list[index - 1];
      if (string.Compare(oneError1._message, oneError2._message, StringComparison.Ordinal) == 0)
      {
        long fPrjlinkId1 = oneError1._f_PRJLINK_ID;
        long fPrjlinkId2 = oneError2._f_PRJLINK_ID;
        this._list.RemoveAt(index);
      }
    }
  }

  /// <summary> Просмотр списка ошибок. Используется только при отладке программы </summary>
  public void Control()
  {
    for (int index = 0; index < this._list.Count; ++index)
    {
      string message = this._list[index]._message;
    }
  }

  public List<ImErrorMessage> CreateErrorMessage()
  {
    List<ImErrorMessage> errorMessage = new List<ImErrorMessage>();
    foreach (OneError error in this._list)
    {
      OneErrorMessage oneErrorMessage = new OneErrorMessage(error);
      errorMessage.Add((ImErrorMessage) oneErrorMessage);
      this._listStr.Add(oneErrorMessage.Text);
    }
    return errorMessage;
  }
}
