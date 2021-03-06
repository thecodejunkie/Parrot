namespace Parrot.Nodes
{
    using System.Collections;
    using System.Collections.Generic;

    public class AbstractNodeList : AbstractNode, IList<AbstractNode>
    {
        #region " IList Members "
        protected List<AbstractNode> _list = new List<AbstractNode>();

        public IEnumerator<AbstractNode> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(AbstractNode item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(AbstractNode item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(AbstractNode[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(AbstractNode item)
        {
            return _list.Remove(item);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public int IndexOf(AbstractNode item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, AbstractNode item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public AbstractNode this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }
        #endregion

        public AbstractNodeList()
        {
            _list = new List<AbstractNode>();
        }

        public override bool IsTerminal
        {
            get { return false; }
        }
    }


    public class AbstractNodeList<T> : AbstractNode, IList<T> where T: AbstractNode
    {
        #region " IList Members "
        protected List<T> _list = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }
        #endregion

        public AbstractNodeList()
        {
            _list = new List<T>();
        }

        public override bool IsTerminal
        {
            get { return false; }
        }
    }

}