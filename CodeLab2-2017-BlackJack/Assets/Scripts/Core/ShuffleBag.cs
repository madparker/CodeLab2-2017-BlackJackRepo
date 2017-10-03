//https://gist.github.com/col000r/6658520
//but all the hard work done by mstevenson: https://gist.github.com/mstevenson/4050130

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShuffleBag<T> : ICollection<T>, IList<T>
{
	private List<T> data = new List<T> ();
	private int cursor = 0;
	private T last;

	public int Cursor{
		get{
			return cursor;
		}
	}
	
	/// <summary>
	/// Get the next value from the ShuffleBag
	/// </summary>
	public T Next ()
	{
		if (cursor < 1) {
			cursor = data.Count - 1;
			if (data.Count < 1)
				return default(T);
			return data[0];
		}
		int grab = Mathf.FloorToInt (Random.value * (cursor + 1)); //grab a random number between 0 and the cursor
		T temp = data[grab]; //temp = the content of the position you grabbed. Store grab inside temp. 
		data[grab] = this.data[this.cursor]; //then take what's in cursor and put it inside the space from which we randomly grabbed an item
		data[cursor] = temp; //temp goes into where the cursor is. Computer can't fundamentally swap. It can add and copy, and when it copies it overwrites. To swap things, you first have to move one somewhere else. 
		//you want to swap people between floors with an elevator that only carries one person. 
		cursor--; 
		return temp;
	}
	
	//This Constructor will let you do this: ShuffleBag<int> intBag = new ShuffleBag<int>(new int[] {1, 2, 3, 4, 5});
	public ShuffleBag(T[] initalValues) {
		for (int i = 0; i < initalValues.Length; i++) {
			Add (initalValues[i]);
		}
	}
	public ShuffleBag() {} //Constructor with no values
	
	#region IList[T] implementation
	public int IndexOf (T item)
	{
		return data.IndexOf (item);
	}
	
	public void Insert (int index, T item)
	{
		cursor = data.Count;
		data.Insert (index, item);
	}
	
	public void RemoveAt (int index)
	{
		cursor = data.Count - 2;
		data.RemoveAt (index);
	}
	
	public T this[int index] {
		get {
			return data [index];
		}
		set {
			data [index] = value;
		}
	}
	#endregion
	
	
	
	#region IEnumerable[T] implementation
	IEnumerator<T> IEnumerable<T>.GetEnumerator ()
	{
		return data.GetEnumerator ();
	}
	#endregion
	
	#region ICollection[T] implementation
	public void Add (T item)
	{
		//Debug.Log (item);
		data.Add (item);
		cursor = data.Count - 1;
	}
	
	public int Count {
		get {
			return data.Count;
		}
	}
	
	public void Clear ()
	{
		data.Clear ();
	}
	
	public bool Contains (T item)
	{
		return data.Contains (item);
	}
	
	public void CopyTo (T[] array, int arrayIndex)
	{
		foreach (T item in data) {
			array.SetValue (item, arrayIndex);
			arrayIndex = arrayIndex + 1;
		}
	}
	
	public bool Remove (T item)
	{
		cursor = data.Count - 2;
		return data.Remove (item);
	}
	
	public bool IsReadOnly {
		get {
			return false;
		}
	}
	#endregion
	
	#region IEnumerable implementation
	IEnumerator IEnumerable.GetEnumerator ()
	{
		return data.GetEnumerator ();
	}
	#endregion
}