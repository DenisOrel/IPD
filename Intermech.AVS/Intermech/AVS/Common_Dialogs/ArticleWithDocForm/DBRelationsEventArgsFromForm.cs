// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.DBRelationsEventArgsFromForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Client;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>
/// Событие по связям, генерируемое создателем пары изделие/документ,
/// служит для идентификации отправителя DBRelationsEventArgs
/// </summary>
internal class DBRelationsEventArgsFromForm : DBRelationsEventArgs
{
  public DBRelationsEventArgsFromForm(string eventName, IList<long> relationIDs)
    : base(eventName, relationIDs)
  {
  }

  public DBRelationsEventArgsFromForm(string eventName, long relationID)
    : base(eventName, relationID)
  {
  }
}
