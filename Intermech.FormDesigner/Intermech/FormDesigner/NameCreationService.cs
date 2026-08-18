// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.NameCreationService
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using ImSSP;
using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>
/// 
/// </summary>
internal class NameCreationService : INameCreationService
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="container"></param>
  /// <param name="dataType"></param>
  /// <returns></returns>
  public string CreateName(IContainer container, Type dataType)
  {
    int num = 0;
    string str = char.ToLower(dataType.Name[0]).ToString() + dataType.Name.Substring(1);
    do
    {
      ++num;
    }
    while (container.Components[str + Convert.ToString(num)] != null);
    return str + Convert.ToString(num);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  public void ValidateName(string name)
  {
    if (!this.IsValidName(name))
      throw new ArgumentException(string.Format(sc_7178.ssp_imclient_7179(), (object) name));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public bool IsValidName(string name)
  {
    bool flag = true;
    if (name == null || name.Length == 0)
      flag = false;
    else if (char.IsDigit(name, 0))
    {
      flag = false;
    }
    else
    {
      for (int index = 0; index < name.Length; ++index)
      {
        if (!char.IsLetterOrDigit(name, index) && !object.Equals((object) name[index], (object) '_'))
        {
          flag = false;
          break;
        }
      }
    }
    return flag;
  }
}
