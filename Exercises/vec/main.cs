using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void print(this double x, string s){
	Write(s); WriteLine(x);
	} //Extension of System
	public static void print(this double x) {
	x.print("");
	}
	public static void Main(){
		vec u = new vec(1,2,3);
		vec v = new vec(2,3,4);
		u.print("s = ");
		u.print		("u = ");
		v.print		("v = ");
		(v+u).print	("v+u = ");
		(2.0*u).print	("2*u ="); //Temporary objects can't be created with multiplication
		vec w = u*2;
		w.print("u*2 = ");
		vec w2= u+6*v-w;
		w2.print("u+6*v-u*2 = ");
		(-u).print("-u=");
		WriteLine($"(dot product) u%v = {u%v}");
		WriteLine($"u.dot(v) = {u.dot(v)}");
		Write("String overloading: u = ");
		WriteLine(u.ToString());
		WriteLine($"Does ({(u*0.2).ToString()}) approximatly equal ({(u*0.1+u*0.1).ToString()}) ? => {(u*0.2).approx(u*0.1+u*0.1)}");
		
	}
}
