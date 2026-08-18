
// Type: Intermech.PropertyEditors.PasswordPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

[Editor(typeof (PasswordEditor), typeof (UITypeEditor))]
public class PasswordPropertyClass
{
  private string password = string.Empty;

  public string Password
  {
    get => this.password;
    set => this.password = value;
  }

  public PasswordPropertyClass(string aPassword) => this.password = aPassword;

  public override string ToString() => ClientConsts.PasswordString;
}
