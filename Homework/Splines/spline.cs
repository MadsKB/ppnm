//Which style to pick? Random number generator says 1. So procedural programming

using System;
using static System.Console;
using static System.Math;
public static class spline {

	public static int binsearch(double[] x, double z){
		if (!(x[0]<=z && z<=x[x.Length-1])) throw new Exception("binsearch: Bad z");
		int i = 0, j=x.Length-1;
		while (j-i>1){
			int mid = (i+j)/2;
			if (z>x[mid]) i=mid; else j=mid;
		}
		return i;

	}
	public static void solveTriDiaSys(vector x,vector B, vector A, vector D, vector Q){
		if (!(A.size == D.size-1)) throw new Exception("SolveTriDiaSys: A Array isn't the right size");
		if (!(Q.size == D.size-1)) throw new Exception("SolveTriDiaSys: Q Array isn't the right size");
		double w = 0;
		for (int i = 1;i<B.size;i++){
			w = A[i-1]/D[i-1];
			D[i] -= w*Q[i-1];
			B[i] -= w*B[i-1];	
		}

		x[x.size-1] = B[x.size-1]/D[x.size-1];
		for (int i = x.size-2; i>-1;i--){
			x[i] = (B[i]-Q[i]*x[i+1])/D[i];
		}
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
	public static double h(vector x, int i) {return x[i+1]-x[i];} //Use this on qspline at somepoint to make it more neat

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

	public static double qspline_evaluate (vector x, vector y, vector b, vector c, double z){
		int i = binsearch(x,z);
		return y[i]+b[i]*(z-x[i])+c[i]*Pow(z-x[i],2);

	}
	public static double qspline_integra(vector x, vector y, vector b, vector c, double z){ //Evaluates the integral with the limits x[0] to z
		int iMax = binsearch(x,z);
		double Out =0;
		for (int i = 0; i<iMax-1; i++)Out+=y[i]*(x[i+1]-x[i])+b[i]/2*Pow(x[i+1]-x[i],2)+c[i]/3*Pow(x[i+1]-x[i],3);
		return Out + y[iMax]*(z-x[iMax])+b[iMax]/2*Pow(z-x[iMax],2)+c[iMax]/3*Pow(z-x[iMax],3);
	}

	public static double qspline_diff(vector x, vector y, vector b, vector c, double z){
		int i = binsearch(x,z);
		return b[i]+2*c[i]*(z-x[i]);

	}

	public static void cspline_build(vector x,vector y, vector b, vector c, vector d,vector A, vector D,vector Q,vector B){ //We're reaching the limit of how many vectors i want to allocate manually. (Vector b should be one longer then c and d, same lenght as x)
		//We'll be using natural spline for this
		D[0] = 2;
		Q[0] = 1;
		A[0] = 1;
		B[0] = 3*p(x,y,0);
		for (int i = 0; i<b.size-2;i++){
		D[i+1] = 2*h(x,i)/h(x,i+1)+2;
		Q[i+1] = h(x,i)/h(x,i+1);
		B[i+1] = 3*(p(x,y,i)+p(x,y,i+1)*h(x,i)/h(x,i+1));
		A[i+1] = 1;
		}
		//WriteLine($"EYY2 Dsize = {D.size}, Bsize = {b.size}");
		D[D.size-1] = 2;
		B[b.size-1] = 3*p(x,y,b.size-2);
		solveTriDiaSys(b,B,A,D,Q);
		for (int i = 0; i<c.size;i++){
		c[i] = (3*p(x,y,i)-2*b[i]-b[i+1])/h(x,i);
		d[i] = (b[i]+b[i+1]-2*p(x,y,i))/h(x,i)/h(x,i);
		}
	}

	public static double cspline_evaluate (vector x, vector y, vector b, vector c, vector d, double z){
		int i = binsearch(x,z);
		return y[i]+b[i]*(z-x[i])+c[i]*Pow(z-x[i],2) + d[i]*Pow(z-x[i],3);
	}
	public static double cspline_diff (vector x, vector y, vector b, vector c, vector d, double z){
		int i = binsearch(x,z);
		return b[i]+2*c[i]*(z-x[i]) +3*d[i]*Pow(z-x[i],2);
	}
	
	
	public static double cspline_integrate(vector x, vector y, vector b, vector c, vector d, double z){ //Evaluates the integral with the limits x[0] to z
		int iMax = binsearch(x,z);
		double Out =0;
		for (int i = 0; i<iMax; i++)Out+=y[i]*(x[i+1]-x[i])+b[i]/2*Pow(x[i+1]-x[i],2)+c[i]/3*Pow(x[i+1]-x[i],3) + d[i]/4*Pow(x[i+1]-x[i],4);
		return Out + y[iMax]*(z-x[iMax])+b[iMax]/2*Pow(z-x[iMax],2)+c[iMax]/3*Pow(z-x[iMax],3)+d[iMax]/4*Pow(z-x[iMax],4);
	}


}
