public class genlist<T>{
	public T[] data;
	//Size of the list
	public int size => data.Length;
	//Get data from the list
	public T this[int i] => data[i];
	public genlist() {data = new T[0]; }
	
	public void add(T item){
		//In C we could use the "Realloc" to reallocate the items already in the list,
		//But this doesn't exsist in C#, so we have to resort to making a new array, and copying it over

		T[] newdata = new T[size +1];
		System.Array.Copy(data,newdata,size); //This might be smart
		newdata[size] = item; //Size here referes to the length of newdata
		data = newdata;
		//refrence to data is replaced by refrence to newdata, this means that old data is still in memory,
		//despite not being in use. (Memory leak, but in C# we have garbage collection so it's proberly fine)
	}


}
