using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(){
		var xs = new genlist<double>();
		var ys = new genlist<vector>();
		double wait = 1;
		double F = 6;
		Func <double,double> Rabi = delegate(double t){
			if (t<PI/2/F) return F;
			if ((PI/2/F+wait)<t && t<(PI/F+wait)) return F;
			return 0;
		};
		double d = 0.2;
		//Part A
		Func<double,vector,vector> RPrime = delegate(double x, vector y) {
			var Out = y.copy(); //Assume y goes (u,v,w)
			Out[0] = d*y[1];
			Out[1] = -d*y[0]+Rabi(x)*y[2];
			Out[2] = -Rabi(x)*y[1];
			return Out;
		};
		double x0 = 0;
		double xEnd = 30;
		vector y0 = new vector(3);
		y0[2] = 1; //Should be a cleaner way to do this
		
		ODE.driver(RPrime,x0,y0,xEnd,0.01,0.01,0.01,xs, ys);
		for (int i = 0; i<xs.size;i++){
			WriteLine($"{xs[i]} {ys[i][0]} {ys[i][1]} {ys[i][2]} {(1-ys[i][2])/2}");
		}
		//Func<double,vector,vector> d = delegate(double x, vector y) {}
		WriteLine("\n");
		int numPoints = 400;
		double minWait = 0;
		double maxWait = 20;
		double minD = 0;
		double maxD = 20;
		wait = 0.5;
		for (int i = 0; i<numPoints; i++) {
			d = (maxD-minD)/numPoints*i;
			WriteLine($"{d} {(1-2*ODE.driver(RPrime,x0,y0,PI/F+wait+5).Item1[2])/2}");
		};
		WriteLine("\n");
		d = 1;
		for (int i = 0;i<numPoints;i++) {
			wait = (maxWait-minWait)/numPoints*i;
			WriteLine($"{wait} {(1-2*ODE.driver(RPrime,x0,y0,PI/F+wait+5).Item1[2])/2}");
		}






	}
}
