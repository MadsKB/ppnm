using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(){
		int dataPoints = 10;
		double xStart = -3;
		double xEnd = 3;
		double dx = (xEnd-xStart)/dataPoints;
		Func<double,double> f = delegate(double d){return 1/(Pow(d,2)+0.5);};
		
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
		WriteLine($"\n");
		vector x = new vector(5);
		vector y = new vector(5);
		for (int i = 0; i<x.size;i++){
			x[i] = i+1;
			y[i] = 1;
			WriteLine($"{1} {x[i]} {x[i]*x[i]}");
		}
		WriteLine($"\n");
		vector b = new vector(x.size-1);
		vector c = new vector(x.size-1);

		spline.qspline_build(x,y,b,c);
		for (int i = 0; i<b.size;i++) WriteLine($"{b[i]} {c[i]}");
		WriteLine($"\n");
		for (int i = 0;i<x.size;i++){
			y[i] = x[i];
		}
		spline.qspline_build(x,y,b,c);
		for (int i = 0;i<b.size;i++) WriteLine($"{b[i]} {c[i]}");
		
		WriteLine($"\n");
		for (int i = 0; i<x.size;i++) y[i] = x[i]*x[i];
		spline.qspline_build(x,y,b,c);	
		for (int i = 0; i<b.size;i++) WriteLine($"{b[i]} {c[i]}");

	}
}
