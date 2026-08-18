
// Type: Intermech.Collections.RBTree`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Intermech.Collections
{
    /// <summary>
    /// Реализует красно-черное дерево - самобалансирующееся бинарное дерево поиска.
    /// </summary>
    /// <typeparam name="TKey">Тип ключей в узлах дерева</typeparam>
    /// <typeparam name="TValue">Тип значений в узлах дерева</typeparam>
    [Serializable]
    public class RBTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable where TKey : IComparable<TKey>
    {
      private RBNode root;
      private int nodesCount;
      private int version;
      /// <summary>
      /// Специальный узел, служащий для обозначения листьев дерева
      /// </summary>
      /// <remarks>
      /// set up the sentinel node. the sentinel node is the key to a successfull
      /// implementation and for understanding the red-black tree properties.
      /// </remarks>
      private static readonly RBNullNode NullNode = new RBNullNode();

      /// <summary>Создает пустое дерево.</summary>
      public RBTree()
      {
        this.root = (RBNode) RBTree<TKey, TValue>.NullNode;
        this.nodesCount = 0;
        this.version = 0;
      }

      /// <summary>Добавляет новый узел в дерево.</summary>
      /// <exception cref="T:System.InvalidOperationException">В дереве уже есть узел с указанным ключем</exception>
      /// <param name="key">Ключ</param>
      /// <param name="value">Значение</param>
      public void Add(TKey key, TValue value)
      {
        RBTree<TKey, TValue>.CheckKey(key);
            RBNode rbNode = this.root;
            RBNode parent = (RBNode) null;
        int num;
        for (; rbNode != RBTree<TKey, TValue>.NullNode; rbNode = num <= 0 ? rbNode.Left : rbNode.Right)
        {
          parent = rbNode;
          num = key.CompareTo(rbNode.Key);
          if (num == 0)
            throw new InvalidOperationException("A Node with the same key already exists");
        }
            RBNode x = new RBNode(key, value, RBTree<TKey, TValue>.NodeColor.Red, parent);
        if (x.Parent != null)
        {
          if (x.Key.CompareTo(x.Parent.Key) > 0)
            x.Parent.Right = x;
          else
            x.Parent.Left = x;
        }
        else
          this.root = x;
        this.RestoreAfterInsert(x);
        ++this.nodesCount;
        ++this.version;
      }

      /// <summary>Удаляет узел из дерева.</summary>
      /// <param name="key">Ключ удаляемого узла</param>
      public void Remove(TKey key)
      {
        RBTree<TKey, TValue>.CheckKey(key);
            RBNode node = this.GetNode(key);
        if (node == null)
          return;
        this.Delete(node);
        --this.nodesCount;
        ++this.version;
      }

      /// <summary>Очищает дерево.</summary>
      public void Clear()
      {
        this.root = (RBNode) RBTree<TKey, TValue>.NullNode;
        this.nodesCount = 0;
        ++this.version;
      }

      /// <summary>
      /// Возвращает true, если в дереве есть узел с заданным ключем.
      /// </summary>
      /// <param name="key">Ключ узла</param>
      /// <returns>True, если узел есть в дереве</returns>
      public bool ContainsKey(TKey key) => this.GetNode(key) != null;

      /// <summary>
      /// Возвращает минимальное значение ключа, встречающееся в дереве.
      /// </summary>
      public TKey MinKey
      {
        get
        {
          this.CheckNotEmpty();
                RBNode rbNode = this.root;
          while (rbNode.Left != RBTree<TKey, TValue>.NullNode)
            rbNode = rbNode.Left;
          return rbNode.Key;
        }
      }

      /// <summary>
      /// Возвращает максимальное значение ключа, встречающееся в дереве.
      /// </summary>
      public TKey MaxKey
      {
        get
        {
          this.CheckNotEmpty();
                RBNode rbNode = this.root;
          while (rbNode.Right != RBTree<TKey, TValue>.NullNode)
            rbNode = rbNode.Right;
          return rbNode.Key;
        }
      }

      /// <summary>Возвращает количество узлов в дереве.</summary>
      public int Count => this.nodesCount;

      /// <summary>Возвращает или устанавливает значние узла дерева.</summary>
      /// <param name="key">Ключ узла</param>
      /// <returns>Значение узла</returns>
      public TValue this[TKey key]
      {
        get
        {
          RBTree<TKey, TValue>.CheckKey(key);
          return (this.GetNode(key) ?? throw new InvalidOperationException("Red-black tree doesn't contains node within specified key!")).Value;
        }
        set
        {
                RBNode node = this.GetNode(key);
          if (node == null)
            throw new InvalidOperationException("Red-black tree doesn't contains node within specified key!");
          node.Value = value;
        }
      }

      public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
      {
        return (IEnumerator<KeyValuePair<TKey, TValue>>) new RBEnumerator(this);
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) new RBEnumerator(this);
      }

      private static void CheckKey(TKey key)
      {
        if ((object) key == null)
          throw new ArgumentNullException(nameof (key), "Red-black tree key must not be null!");
      }

      private void CheckNotEmpty()
      {
        if (this.root == RBTree<TKey, TValue>.NullNode)
          throw new InvalidOperationException("Red-black tree is empty!");
      }

      /// <summary>
      ///  RotateLeft
      ///  Rebalance the tree by rotating the nodes to the left
      /// </summary>
      private void RotateLeft(RBNode x)
      {
            RBNode right = x.Right;
        x.Right = right.Left;
        if (right.Left != RBTree<TKey, TValue>.NullNode)
          right.Left.Parent = x;
        if (right != RBTree<TKey, TValue>.NullNode)
          right.Parent = x.Parent;
        if (x.Parent != null)
        {
          if (x == x.Parent.Left)
            x.Parent.Left = right;
          else
            x.Parent.Right = right;
        }
        else
          this.root = right;
        right.Left = x;
        if (x == RBTree<TKey, TValue>.NullNode)
          return;
        x.Parent = right;
      }

      /// <summary>
      ///  RotateRight
      ///  Rebalance the tree by rotating the nodes to the right
      /// </summary>
      private void RotateRight(RBNode x)
      {
            RBNode left = x.Left;
        x.Left = left.Right;
        if (left.Right != RBTree<TKey, TValue>.NullNode)
          left.Right.Parent = x;
        if (left != RBTree<TKey, TValue>.NullNode)
          left.Parent = x.Parent;
        if (x.Parent != null)
        {
          if (x == x.Parent.Right)
            x.Parent.Right = left;
          else
            x.Parent.Left = left;
        }
        else
          this.root = left;
        left.Right = x;
        if (x == RBTree<TKey, TValue>.NullNode)
          return;
        x.Parent = left;
      }

      /// <summary>
      ///  RestoreAfterInsert
      ///  Additions to red-black trees usually destroy the red-black
      ///  properties. Examine the tree and restore. Rotations are normally
      ///  required to restore it
      /// </summary>
      private void RestoreAfterInsert(RBNode x)
      {
        while (x != this.root && x.Parent.Color == RBTree<TKey, TValue>.NodeColor.Red)
        {
          if (x.Parent == x.Parent.Parent.Left)
          {
                    RBNode right = x.Parent.Parent.Right;
            if (right != null && right.Color == RBTree<TKey, TValue>.NodeColor.Red)
            {
              x.Parent.Color = RBTree<TKey, TValue>.NodeColor.Black;
              right.Color = RBTree<TKey, TValue>.NodeColor.Black;
              x.Parent.Parent.Color = RBTree<TKey, TValue>.NodeColor.Red;
              x = x.Parent.Parent;
            }
            else
            {
              if (x == x.Parent.Right)
              {
                x = x.Parent;
                this.RotateLeft(x);
              }
              x.Parent.Color = RBTree<TKey, TValue>.NodeColor.Black;
              x.Parent.Parent.Color = RBTree<TKey, TValue>.NodeColor.Red;
              this.RotateRight(x.Parent.Parent);
            }
          }
          else
          {
                    RBNode left = x.Parent.Parent.Left;
            if (left != null && left.Color == RBTree<TKey, TValue>.NodeColor.Red)
            {
              x.Parent.Color = RBTree<TKey, TValue>.NodeColor.Black;
              left.Color = RBTree<TKey, TValue>.NodeColor.Black;
              x.Parent.Parent.Color = RBTree<TKey, TValue>.NodeColor.Red;
              x = x.Parent.Parent;
            }
            else
            {
              if (x == x.Parent.Left)
              {
                x = x.Parent;
                this.RotateRight(x);
              }
              x.Parent.Color = RBTree<TKey, TValue>.NodeColor.Black;
              x.Parent.Parent.Color = RBTree<TKey, TValue>.NodeColor.Red;
              this.RotateLeft(x.Parent.Parent);
            }
          }
        }
        this.root.Color = RBTree<TKey, TValue>.NodeColor.Black;
      }

      private RBNode GetNode(TKey key)
      {
        int num;
        for (RBNode node = this.root; node != RBTree<TKey, TValue>.NullNode; node = num >= 0 ? node.Right : node.Left)
        {
          num = key.CompareTo(node.Key);
          if (num == 0)
            return node;
        }
        return (RBNode) null;
      }

      /// <summary>
      /// Delete
      /// Delete a node from the tree and restore red black properties
      /// </summary>
      /// <param name="z"></param>
      private void Delete(RBNode z)
      {
            RBNode rbNode;
        if (z.Left == RBTree<TKey, TValue>.NullNode || z.Right == RBTree<TKey, TValue>.NullNode)
        {
          rbNode = z;
        }
        else
        {
          rbNode = z.Right;
          while (rbNode.Left != RBTree<TKey, TValue>.NullNode)
            rbNode = rbNode.Left;
        }
            RBNode x = rbNode.Left == RBTree<TKey, TValue>.NullNode ? rbNode.Right : rbNode.Left;
        x.Parent = rbNode.Parent;
        if (rbNode.Parent != null)
        {
          if (rbNode == rbNode.Parent.Left)
            rbNode.Parent.Left = x;
          else
            rbNode.Parent.Right = x;
        }
        else
          this.root = x;
        if (rbNode != z)
        {
          z.Key = rbNode.Key;
          z.Value = rbNode.Value;
        }
        if (rbNode.Color != RBTree<TKey, TValue>.NodeColor.Black)
          return;
        this.RestoreAfterDelete(x);
      }

      /// <summary>
      ///  RestoreAfterDelete
      ///  Deletions from red-black trees may destroy the red-black
      ///  properties. Examine the tree and restore. Rotations are normally
      ///  required to restore it
      /// </summary>
      private void RestoreAfterDelete(RBNode x)
      {
        while (x != this.root && x.Color == RBTree<TKey, TValue>.NodeColor.Black)
        {
          if (x == x.Parent.Left)
          {
                    RBNode right = x.Parent.Right;
            if (right.Color == RBTree<TKey, TValue>.NodeColor.Red)
            {
              right.Color = RBTree<TKey, TValue>.NodeColor.Black;
              x.Parent.Color = RBTree<TKey, TValue>.NodeColor.Red;
              this.RotateLeft(x.Parent);
              right = x.Parent.Right;
            }
            if (right.Left.Color == RBTree<TKey, TValue>.NodeColor.Black && right.Right.Color == RBTree<TKey, TValue>.NodeColor.Black)
            {
              right.Color = RBTree<TKey, TValue>.NodeColor.Red;
              x = x.Parent;
            }
            else
            {
              if (right.Right.Color == RBTree<TKey, TValue>.NodeColor.Black)
              {
                right.Left.Color = RBTree<TKey, TValue>.NodeColor.Black;
                right.Color = RBTree<TKey, TValue>.NodeColor.Red;
                this.RotateRight(right);
                right = x.Parent.Right;
              }
              right.Color = x.Parent.Color;
              x.Parent.Color = RBTree<TKey, TValue>.NodeColor.Black;
              right.Right.Color = RBTree<TKey, TValue>.NodeColor.Black;
              this.RotateLeft(x.Parent);
              x = this.root;
            }
          }
          else
          {
                    RBNode left = x.Parent.Left;
            if (left.Color == RBTree<TKey, TValue>.NodeColor.Red)
            {
              left.Color = RBTree<TKey, TValue>.NodeColor.Black;
              x.Parent.Color = RBTree<TKey, TValue>.NodeColor.Red;
              this.RotateRight(x.Parent);
              left = x.Parent.Left;
            }
            if (left.Right.Color == RBTree<TKey, TValue>.NodeColor.Black && left.Left.Color == RBTree<TKey, TValue>.NodeColor.Black)
            {
              left.Color = RBTree<TKey, TValue>.NodeColor.Red;
              x = x.Parent;
            }
            else
            {
              if (left.Left.Color == RBTree<TKey, TValue>.NodeColor.Black)
              {
                left.Right.Color = RBTree<TKey, TValue>.NodeColor.Black;
                left.Color = RBTree<TKey, TValue>.NodeColor.Red;
                this.RotateLeft(left);
                left = x.Parent.Left;
              }
              left.Color = x.Parent.Color;
              x.Parent.Color = RBTree<TKey, TValue>.NodeColor.Black;
              left.Left.Color = RBTree<TKey, TValue>.NodeColor.Black;
              this.RotateRight(x.Parent);
              x = this.root;
            }
          }
        }
        x.Color = RBTree<TKey, TValue>.NodeColor.Black;
      }

      [Serializable]
      internal enum NodeColor : byte
      {
        Red,
        Black,
      }

      [Serializable]
      internal class RBNode : IDeserializationCallback
      {
        private TKey key;
        private TValue value;
        private NodeColor color;
        private RBNode parent;
        private RBNode left;
        private RBNode right;

        protected RBNode(
          NodeColor color,
          RBNode parent,
          RBNode left,
          RBNode right)
        {
          this.color = color;
          this.parent = parent;
          this.left = left;
          this.right = right;
        }

        public RBNode(
          TKey key,
          TValue value,
          NodeColor color,
          RBNode parent)
          : this(color, parent, (RBNode) RBTree<TKey, TValue>.NullNode, (RBNode) RBTree<TKey, TValue>.NullNode)
        {
          this.key = key;
          this.value = value;
        }

        public TKey Key
        {
          get => this.key;
          set => this.key = value;
        }

        public TValue Value
        {
          get => this.value;
          set => this.value = value;
        }

        public NodeColor Color
        {
          get => this.color;
          set => this.color = value;
        }

        public RBNode Left
        {
          get => this.left;
          set => this.left = value;
        }

        public RBNode Right
        {
          get => this.right;
          set => this.right = value;
        }

        public RBNode Parent
        {
          get => this.parent;
          set => this.parent = value;
        }

        void IDeserializationCallback.OnDeserialization(object sender)
        {
          if (this.left is RBNullNode)
            this.left = (RBNode) RBTree<TKey, TValue>.NullNode;
          if (!(this.right is RBNullNode))
            return;
          this.right = (RBNode) RBTree<TKey, TValue>.NullNode;
        }
      }

      [Serializable]
      internal class RBNullNode : RBNode, ISerializable
      {
        public RBNullNode()
          : base(RBTree<TKey, TValue>.NodeColor.Black, (RBNode) null, (RBNode) null, (RBNode) null)
        {
          this.Left = (RBNode) this;
          this.Right = (RBNode) this;
        }

        private RBNullNode(SerializationInfo info, StreamingContext context)
          : this()
        {
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }
      }

      [Serializable]
      internal class RBEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
      {
        private RBTree<TKey, TValue> owner;
        private int version;
        private State state;
        private TKey key;
        private TValue value;
        private Stack stack;

        public RBEnumerator(RBTree<TKey, TValue> owner)
        {
          this.owner = owner;
          this.version = this.owner.version;
          this.Reset();
        }

        public KeyValuePair<TKey, TValue> Current
        {
          get
          {
            this.CheckCurrentValues();
            return new KeyValuePair<TKey, TValue>(this.key, this.value);
          }
        }

        public void Dispose()
        {
        }

        object IEnumerator.Current
        {
          get
          {
            this.CheckCurrentValues();
            return (object) new DictionaryEntry((object) this.key, (object) this.value);
          }
        }

        public void Reset()
        {
          this.CheckTreeModified();
          this.state = RBTree<TKey, TValue>.RBEnumerator.State.BeforeOfData;
          this.stack = new Stack();
        }

        public bool MoveNext()
        {
          this.CheckTreeModified();
          if (this.state == RBTree<TKey, TValue>.RBEnumerator.State.BeforeOfData)
          {
            for (RBNode rbNode = this.owner.root; rbNode != RBTree<TKey, TValue>.NullNode; rbNode = rbNode.Left)
              this.stack.Push((object) rbNode);
          }
          bool hasMoreElements = this.HasMoreElements;
          this.state = hasMoreElements ? RBTree<TKey, TValue>.RBEnumerator.State.Normal : RBTree<TKey, TValue>.RBEnumerator.State.EndOfData;
          if (hasMoreElements)
            this.NextElement();
          return hasMoreElements;
        }

        private bool HasMoreElements => this.stack.Count > 0;

        private void NextElement()
        {
                RBNode rbNode1 = (RBNode) this.stack.Peek();
          this.key = rbNode1.Key;
          this.value = rbNode1.Value;
          if (rbNode1.Right == RBTree<TKey, TValue>.NullNode)
          {
                    RBNode rbNode2 = (RBNode) this.stack.Pop();
            while (this.HasMoreElements && ((RBNode) this.stack.Peek()).Right == rbNode2)
              rbNode2 = (RBNode) this.stack.Pop();
          }
          else
          {
            for (RBNode rbNode3 = rbNode1.Right; rbNode3 != RBTree<TKey, TValue>.NullNode; rbNode3 = rbNode3.Left)
              this.stack.Push((object) rbNode3);
          }
        }

        private void CheckCurrentValues()
        {
          if (this.state != RBTree<TKey, TValue>.RBEnumerator.State.Normal)
            throw new InvalidOperationException();
        }

        private void CheckTreeModified()
        {
          if (this.version != this.owner.version)
            throw new InvalidOperationException();
        }

        internal enum State
        {
          BeforeOfData,
          Normal,
          EndOfData,
        }
      }
    }
}
