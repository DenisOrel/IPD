
// Type: Intermech.PropertyEditors.NewPasswordEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class NewPasswordEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    if ((!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) || !service.IsAdmin) && !(ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadBool("KERNEL", "SECURITY", "PSW_USER", true, DBConfigMode.GlobalOnly))
      throw new PasswordModifyException();
    NewPasswordForm newPasswordForm = new NewPasswordForm();
    if (newPasswordForm.ShowDialog() == DialogResult.OK)
      value = (object) newPasswordForm.NewPassword;
    newPasswordForm.Dispose();
    return value;
  }
}
