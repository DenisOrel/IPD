// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.DocScriptCommands
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

internal class DocScriptCommands
{
  public static void EditCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    long num1 = new ObjSelector().SelectDocScriptForTemplate((items.GetItemData(sc_6469.ssp_expert_6473(127835074), typeof (IDBObjectID)) as IDBObjectID).Value);
    if (num1 == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(num1);
      if (dbObject1.CheckoutBy != 0L && dbObject1.CheckoutBy != sessionKeeper.Session.UserID)
      {
        int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_208"));
        return;
      }
      if (dbObject1.ObjectModifyMode == ObjectModifyModes.Checkout)
      {
        if (dbObject1.CheckoutBy == 0L)
        {
          IDBObject dbObject2 = dbObject1.CheckOut();
          if (dbObject2 == null)
            return;
          num1 = dbObject2.ObjectID;
        }
      }
    }
    Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(num1), viewServices);
  }

  public static void CreateNewCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    long num = (items.GetItemData(sc_6469.ssp_expert_6474(1279691123), typeof (IDBObjectID)) as IDBObjectID).Value;
    long objID = 0;
    using (ScriptEdit2 scriptEdit2 = new ScriptEdit2(ExpertScriptType.DocScript))
    {
      string str = new UserPrompt().Execute(LocalizationHolder.rm.GetString("Expert.Editor_367"), LocalizationHolder.rm.GetString("Expert.Editor_368"));
      if (str == "")
        return;
      scriptEdit2.TemplateID = num;
      scriptEdit2.newObjName = str;
      objID = scriptEdit2.createObject(-1L, false);
    }
    if (objID == 0L)
      return;
    Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objID), viewServices);
  }
}
