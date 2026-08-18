
// Type: Intermech.Actions.ActionCollectionEditor
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.ComponentModel.Design;


namespace Intermech.Actions
{
    internal class ActionCollectionEditor : CollectionEditor
    {
      public ActionCollectionEditor()
        : base(typeof (ActionCollection))
      {
      }

      protected override object[] GetItems(object editValue)
      {
        ActionCollection actionCollection = (ActionCollection) editValue;
        Action[] array = new Action[actionCollection.Count];
        if (actionCollection.Count > 0)
          actionCollection.CopyTo(array, 0);
        return (object[]) array;
      }

      protected override object SetItems(object editValue, object[] value)
      {
        ActionCollection actionCollection = (ActionCollection) editValue;
        actionCollection.Clear();
        foreach (object obj in value)
          actionCollection.Add((Action) obj);
        return (object) actionCollection;
      }
    }
}
