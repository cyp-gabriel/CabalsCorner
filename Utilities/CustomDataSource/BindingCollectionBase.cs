using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace CabalsCorner.Utilities.CustomDataSource
{
	 /// <summary>
	 /// CollectionBase clone which implements IBindingList.
	 /// </summary>
	 [Serializable()]
	 public abstract class BindingCollectionBase : IBindingList, IList, ICollection, IEnumerable
	 {

		  private ArrayList _List;
		  internal object _PendingInsert;

		  protected BindingCollectionBase()
		  {
				_List = new ArrayList();
				_PendingInsert = null;
		  }

		  public int Count
		  {
				get { return _List.Count; }
		  }

		  public void Clear()
		  {
				OnClear();
				for (int i = 0; i < _List.Count; ++i)
					 ((EditableObject)_List[i]).SetCollection(null);
				_List.Clear();
				_PendingInsert = null;
				OnClearComplete();
				if (_ListChanged != null)
					 _ListChanged(this, new ListChangedEventArgs(ListChangedType.Reset, 0));
		  }
		  public void RemoveAt(int index)
		  {
				if (index < 0 || index >= _List.Count)
					 throw new ArgumentOutOfRangeException();
				object item = _List[index];
				OnValidate(item);
				OnRemove(index, item);
				((EditableObject)_List[index]).SetCollection(null);
				if (_PendingInsert == item)
					 _PendingInsert = null;
				_List.RemoveAt(index);
				OnRemoveComplete(index, item);
				if (_ListChanged != null)
					 _ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
		  }
		  public void AddRange(EditableObject[] range)
		  {
				this._List.AddRange(range);
		  }

		  public IEnumerator GetEnumerator()
		  {
				return _List.GetEnumerator();
		  }
		  // There is no InnerList since using it would cease firing events.
		  protected IList List
		  {
				get { return this; }
		  }

		  // override to get the correct type
		  protected virtual Type ElementType
		  {
				get { return typeof(object); }
		  }
		  // override if the default constructor is not suitable
		  protected virtual object CreateInstance()
		  {
				return Activator.CreateInstance(ElementType);
		  }

		  #region IBindingList

		  event ListChangedEventHandler IBindingList.ListChanged
		  {
				add { _ListChanged += value; }
				remove { _ListChanged -= value; }
		  }

		  bool IBindingList.AllowEdit
		  {
				get { return true; }
		  }
		  bool IBindingList.AllowNew
		  {
				get { return true; }
		  }
		  bool IBindingList.AllowRemove
		  {
				get { return true; }
		  }
		  bool IBindingList.SupportsChangeNotification
		  {
				get { return true; }
		  }
		  object IBindingList.AddNew()
		  {
				if (_PendingInsert != null)
					 ((IEditableObject)_PendingInsert).CancelEdit();
				object item = CreateInstance();
				((IList)this).Add(item);
				_PendingInsert = item;
				return item;
		  }
		  bool IBindingList.SupportsSearching { get { return false; } }
		  bool IBindingList.SupportsSorting { get { return false; } }
		  bool IBindingList.IsSorted { get { return false; } }
		  ListSortDirection IBindingList.SortDirection { get { throw new NotSupportedException(); } }
		  PropertyDescriptor IBindingList.SortProperty { get { throw new NotSupportedException(); } }
		  void IBindingList.AddIndex(PropertyDescriptor property) { throw new NotSupportedException(); }
		  void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction) { throw new NotSupportedException(); }
		  int IBindingList.Find(PropertyDescriptor property, object key) { throw new NotSupportedException(); }
		  void IBindingList.RemoveIndex(PropertyDescriptor property) { throw new NotSupportedException(); }
		  void IBindingList.RemoveSort() { throw new NotSupportedException(); }

		  [NonSerialized()]
		  private ListChangedEventHandler _ListChanged;

		  #endregion

		  #region ICollection

		  void ICollection.CopyTo(Array array, int index)
		  {
				_List.CopyTo(array, index);
		  }
		  bool ICollection.IsSynchronized
		  {
				get { return _List.IsSynchronized; }
		  }
		  object ICollection.SyncRoot
		  {
				get { return _List.SyncRoot; }
		  }

		  #endregion

		  #region IList

		  int IList.Add(object value)
		  {
				OnValidate(value);
				OnInsert(_List.Count, value);
				int index = _List.Add(value);
				try
				{
					 OnInsertComplete(index, value);
				}
				catch
				{
					 _List.RemoveAt(index);
					 throw;
				}
				((EditableObject)value).SetCollection(this);
				if (_ListChanged != null)
					 _ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemAdded, index));
				return index;
		  }
		  object IList.this[int index]
		  {
				get
				{
					 if (index < 0 || index >= _List.Count)
						  throw new ArgumentOutOfRangeException();
					 return _List[index];
				}
				set
				{
					 if (index < 0 || index >= _List.Count)
						  throw new ArgumentOutOfRangeException();
					 OnValidate(value);
					 object item = _List[index];
					 OnSet(index, item, value);
					 _List[index] = value;
					 try
					 {
						  OnSetComplete(index, item, value);
					 }
					 catch
					 {
						  _List[index] = item;
						  throw;
					 }
					 if (_ListChanged != null)
						  _ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemChanged, index));
				}
		  }
		  bool IList.Contains(object value)
		  {
				return _List.Contains(value);
		  }
		  bool IList.IsFixedSize
		  {
				get { return _List.IsFixedSize; }
		  }
		  bool IList.IsReadOnly
		  {
				get { return _List.IsReadOnly; }
		  }
		  int IList.IndexOf(object value)
		  {
				return _List.IndexOf(value);
		  }
		  void IList.Insert(int index, object value)
		  {
				if (index < 0 || index > _List.Count)
					 throw new ArgumentOutOfRangeException();
				OnValidate(value);
				OnInsert(index, value);
				_List.Insert(index, value);
				try
				{
					 OnInsertComplete(index, value);
				}
				catch
				{
					 _List.RemoveAt(index);
					 throw;
				}
				((EditableObject)value).SetCollection(this);
				if (_ListChanged != null)
					 _ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemAdded, index));
		  }
		  void IList.Remove(object value)
		  {
				OnValidate(value);
				int index = _List.IndexOf(value);
				if (index < 0)
					 throw new ArgumentException();
				OnRemove(index, value);
				_List.RemoveAt(index);
				OnRemoveComplete(index, value);
				if (_ListChanged != null)
					 _ListChanged(this, new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
		  }

		  #endregion

		  #region overridable notifications

		  protected virtual void OnClear()
		  {
		  }
		  protected virtual void OnClearComplete()
		  {
		  }
		  protected virtual void OnInsert(int index, object value)
		  {
		  }
		  protected virtual void OnInsertComplete(int index, object value)
		  {
		  }
		  protected virtual void OnRemove(int index, object value)
		  {
		  }
		  protected virtual void OnRemoveComplete(int index, object value)
		  {
		  }
		  protected virtual void OnSet(int index, object oldValue, object newValue)
		  {
		  }
		  protected virtual void OnSetComplete(int index, object oldValue, object newValue)
		  {
		  }
		  protected virtual void OnValidate(object value)
		  {
				if (value == null)
					 throw new ArgumentNullException("value");
		  }

		  #endregion
	 }

}
