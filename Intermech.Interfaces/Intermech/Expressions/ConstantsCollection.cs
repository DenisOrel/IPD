
// Type: Intermech.Expressions.ConstantsCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Expressions
{
    public class ConstantsCollection : IdentifiersCollection
    {
      internal event ConstantsCollection.AddEventHandler ConstantAdd;

      internal ConstantsCollection()
      {
      }

      public int Add(Constant constant) => this.List.Add((object) constant);

      protected override void OnInsert(int index, object value)
      {
        this.RaiseConstantAddEvent(((Constant) value).Name);
      }

      private void RaiseConstantAddEvent(string NewName)
      {
        if (this.ConstantAdd == null)
          return;
        this.ConstantAdd(NewName);
      }

      public void Remove(Constant constant) => this.List.Remove((object) constant);

      public Constant this[int index] => (Constant) this.List[index];

      internal delegate void AddEventHandler(string NewName);
    }
}
