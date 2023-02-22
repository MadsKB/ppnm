using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(){
		Write("hello\n");
		genlist<double> listd = new genlist<double>();
		listd.add(1.0);	
		listd.add(2.0);	
		listd.add(3.0);	
		listd.add(4.0);	
		listd.add(5.0);	
		Func<double,double> f; //Generic function (pointer to), which takes a double, and returns a double, 
				       //Automatic casting can kinda happen, but requires the doubles and the like to
				       //have appropriate value conversions
		f = Sin;
		double a = 10;
//		f = delegate(double x){return x*a;}; //Delegate
						     //a will here refere to 10, but this will be passed "as refrence"
		f = (double x) => x*a;
		a = 0; //So now f will just give 0, you can make a "pass as value" in c++, but c# doesn't allow
		//If f is a return from a function, a is captured, and stored as a value (In effect, you return the 
		//function and the enviorment it's in) (Belive python is the same?)
		
		
//		double y = f(2.0);//Using x isn't allowed, as x was defined in the above f
		for(int i = 0; i<listd.size;i++){
			double x = listd[i];
			WriteLine($"{x} {f(x)}");
		}

	}
}
