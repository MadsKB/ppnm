//Which style to pick? Random number generator says 1. So procedural programming

using System;
using static System.Console;
using static System.Math;
public static class spline {

	public static int binsearch(double[] x, double z){
		if(!(x[0]<=z && z<=x[x.Length-1])) throw new Exception("binsearch: Bad z");
		int i = 0, j=x.Length-1;
		while (j-i>1){
			int mid = (i+j)/2;
			if (z>x[mid]) i=mid; else j=mid;
		}
		return i;

	}

	public static double linterp (double[] x, double[] y, double z) {
		int i = binsearch(x,z);
		double dx = x[i+1]-x[i]; if (!(dx>0)) throw new Exception("linterp: x array isn't sorted");
		double dy = y[i+1]-y[i];
		return y[i]+dy/dx*(z-x[i]);
	
	}
	public static double linterpInteg(double[] x, double[] y, double z){
		//Integral(a*x+b) => a/2*x^2+b*x+c (c=0 in our case)
		
		
		//First, eval what the max i is before z using a single binary search
		int iMax = binsearch(x,z); //Might have to be minus 1, think this is okay
		double Out = 0;
		//Now add the contributions from all the intervals before the one with z in it (You could run this in parallel)
		for (int i = 0; i<iMax; i++)Out+= ((y[i+1]-y[i])/2+y[i])*(x[i+1]-x[i]);
		//And finally add the contribution from the z interval
		return Out + y[iMax]*(z-x[iMax])+(y[iMax+1]-y[iMax])/(x[iMax+1]-x[iMax])/2*Pow(z-x[iMax],2);
	}

	public static double p(vector x, vector y, int i){
		return (y[i+1]-y[i])/(x[i+1]-x[i]);
	}

	public static void qspline_build (vector x,vector y,vector b,vector c){
		//We'll be using the natural spline for this
		c[0] = 0;
		for (int i = 1; i<c.size;i++){ //Forward recoursion
			c[i] = 1/(x[i+1]-x[i])*(p(x,y,i)-p(x,y,i-1)-c[i-1]*(x[i]-x[i-1]));
		}
		c[c.size-1] /=2;
		for (int i = c.size-2; i>=0;i--){ //backward recoursion
			c[i] = (1/(x[i+1]-x[i]))*(p(x,y,i+1)-p(x,y,i)-c[i+1]*(x[i+2]-x[i+1]));
		}
		for(int i =0; i<b.size;i++) b[i] = p(x,y,i)-c[i]*(x[i+1]-x[i]);
	}
	
	public static void qspline_build_forward (vector x, vector y, vector b, vector c, double startC){
	
		c[0] = startC;
		for (int i = 1; i<c.size;i++){ //Forward recoursion
			c[i] = 1/(x[i+1]-x[i])*(p(x,y,i)-p(x,y,i-1)-c[i-1]*(x[i]-x[i-1]))/2;
		}
		for(int i =0; i<b.size;i++) b[i] = p(x,y,i)-c[i]*(x[i+1]-x[i]);
	}

	static double qspline_evaluate (vector x, vector y, vector b, vector c, double z){
		int i = binsearch(x,z);
		return y[i]+b[i]*(z-x[i])+c[i]*Pow(z-x[i],2);

	}
	static double qspline_integra(vector x, vector y, vector b, vector c, double z){
		int iMax = binsearch(x,z);
		double Out =0;
		for (int i = 0; i<iMax-1; i++)Out+=y[i]*(x[i+1]-x[i])+b[i]/2*Pow(x[i+1]-x[i],2)+c[i]/3*Pow(x[i+1]-x[i],3);
		return Out + y[iMax]*(z-x[iMax])+b[iMax]/2*Pow(z-x[iMax],2)+c[iMax]/3*Pow(z-x[iMax],3);
	}

	static double qspline_diff(vector x, vector y, vector b, vector c, double z){
		int i = binsearch(x,z);
		return b[i]+2*c[i]*(z-x[i]);

	}
}
