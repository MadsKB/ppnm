using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(){
		var xs = new genlist<double>();
		var ys = new genlist<vector>();
		
		
		//Part A
		Func<double,vector,vector> f = delegate(double x, vector y) {
			var Out = y.copy(); //Assume y goes (y,y')
			Out[0] = y[1];
			Out[1] = -y[0];
			return Out;
		};
		double x0 = 0;
		double xEnd = 10;
		vector y0 = new vector(2);
		y0[1] = 10; //Should be a cleaner way to do this
		
		ODE.driver(f,x0,y0,xEnd,0.01,0.01,0.01,xs, ys);
		for (int i = 0; i<xs.size;i++){
			WriteLine($"{xs[i]} {ys[i][0]} {ys[i][1]}");
		}
		//Func<double,vector,vector> d = delegate(double x, vector y) {} 
		WriteLine($"\n");
		//Part B
		double eps = 0;
		Func<double,vector,vector> g = delegate(double x, vector y) {
			var Out = y.copy();
			Out[0] = y[1];
			Out[1] = 1-y[0]+eps*y[0]*y[0];
			return Out;
		};
		//i
		xs = new genlist<double>();
		ys = new genlist<vector>();
		x0 = 0; xEnd = 10*PI;
		int numPoints = 500;
		y0[0] = 1; y0[1] = 0;
		for (int i = 0; i<numPoints;i++){
			var (y,err) = ODE.driver(g,x0,y0,(i+1)*xEnd/numPoints,0.00001,0.001,0.001);
			WriteLine($"{(i+1)*xEnd/numPoints} {y[0]} {y[1]}");
		}
		WriteLine("\n");
		//ii

		y0[0] = 1; y0[1] = -0.5;

		for (int i = 0; i<numPoints;i++){
			var (y,err) = ODE.driver(g,x0,y0,(i+1)*xEnd/numPoints,0.00001,0.001,0.001);
			WriteLine($"{(i+1)*xEnd/numPoints} {y[0]} {y[1]}");
		}

		WriteLine("\n");
		//iii
		eps = 0.02;	
		for (int i = 0; i<numPoints;i++){
			var (y,err) = ODE.driver(g,x0,y0,(i+1)*xEnd/numPoints,0.00001,0.001,0.001);
			WriteLine($"{(i+1)*xEnd/numPoints} {y[0]} {y[1]}");
		}







	}
}
