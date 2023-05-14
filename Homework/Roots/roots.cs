using System;
using static System.Console;
using static System.Math;
public static class roots {
	public static (vector,int) newton(Func<vector,vector>f,vector x0, double eps = 1e-2,int evals = 0, int maxEvals = 1000){
		if (f(x0).Mag()<eps) return (x0,evals);
		if (maxEvals != null){
			if (evals > 1e3) return (x0, evals); //Cap reached
			}
		evals+=1;
		var jacobi = new matrix(x0.size,x0.size);
		var dx = x0.copy()*0;
		for (int k = 0;k<x0.size;k++){
			dx[k] = Abs(x0[k])*Pow(2,-26);
			if (dx[k] == 0) dx[k]=Pow(2,-30);
			jacobi[k] = (f(x0+dx)-f(x0))/dx[k];
			dx[k] = 0;
		}
		//WriteLine($"Jacobi:");
		//jacobi.print();
		var decomp = new QRGS(jacobi);
		//WriteLine($"R");
		//decomp.R.print();
		dx = decomp.solve(-f(x0));
		double lambda = 1.0;
		//dx.print();
		while (f(x0+lambda*dx).Mag()>(1-lambda/2)*f(x0).Mag() && lambda >1.0/64)lambda /=2;
		//WriteLine($"{lambda} {x0[0]} {x0[0]+lambda*dx[0]} {f(x0).Mag()} {eps}");
		
		return newton(f,x0+lambda*dx,eps,evals,maxEvals);
	}
}
