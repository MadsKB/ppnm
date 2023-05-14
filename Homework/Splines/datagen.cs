using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(){
		int dataPoints = 10;
		double xStart = -3;
		double xEnd = 3;
		double dx = (xEnd-xStart)/dataPoints;
		Func<double,double> f = delegate(double t){return 1/(Pow(t,2)+0.5);};
		//Part A):
		int splinePoints = 50;
		double[] xs = new double[dataPoints+1];
		double[] ys = new double[dataPoints+1];
		for (int i = 0; i<dataPoints+1;i++){
			xs[i] = dx*i+xStart;
			ys[i] = f(dx*i+xStart);
			WriteLine($"{dx*i+xStart} {f(dx*i+xStart)}");
		};
		dx = (xEnd-xStart)/splinePoints;
		WriteLine($"\n");
		for (int i = 0; i<splinePoints;i++){
			WriteLine($"{dx*i+xStart} {spline.linterp(xs,ys,dx*i+xStart)}");
		}
		WriteLine($"\n");
		for (int i = 0; i<splinePoints;i++){
			WriteLine($"{dx*i+xStart} {spline.linterpInteg(xs,ys,dx*i+xStart)}");
		}
		//Part C):
		//We'll use cosine as our interpulation function
		WriteLine($"\n");
		vector x = new vector(5);
		vector y = new vector(5);
		for (int i = 0; i<x.size;i++){
			x[i] = i;
			y[i] = Cos(i);
			WriteLine($"{x[i]} {y[i]}");
		}
		vector c = new vector(x.size-1);
		WriteLine($"\n");
		//We'll use cosine as our interpulation function
		var b = new vector(x.size);
		var d = new vector(c.size);
		var A = new vector(c.size);
		var D = new vector(x.size);
		var Q = new vector(c.size);
		var B = new vector(x.size);
		spline.cspline_build(x,y,b,c,d,A,D,Q,B);

		for (int i = 1;i<splinePoints;i++){
			double X = 4.0/splinePoints*i;
			WriteLine($"{X} {spline.cspline_evaluate(x,y,b,c,d,X)}");
		}
		WriteLine($"\n");
		for (int i = 1;i<splinePoints;i++){
			double X = 4.0/splinePoints*i;
			WriteLine($"{X} {spline.cspline_diff(x,y,b,c,d,X)}");
		}
		WriteLine($"\n");
		for (int i = 1;i<splinePoints;i++){
			double X = 4.0/splinePoints*i;
			WriteLine($"{X} {spline.cspline_integrate(x,y,b,c,d,X)}");
		}


	}
}
