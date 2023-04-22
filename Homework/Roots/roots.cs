using System;
using static System.Console;
using static System.Math;
public static class roots {
	public static vector newton(Func<vector,vector>f,vector x0, double eps = 1e-2){
		if (f(x0).Mag()<eps) return x0;
		
		var jacobi = new matrix(x0.size,x0.size);
		var dx = x0.copy()*0;
		for (int k = 0;k<x0.size;k++){
			dx[k] = Abs(x0[k])*Pow(2,-26);
			if (dx[k] == 0) dx[k]=Pow(2,-30);
			jacobi[k] = (f(x0+dx)-f(x0))/dx[k];
			dx[k] = 0;
		}

		var decomp = new QRGS(jacobi);
		dx = decomp.solve(-f(x0));
		double lambda = 1;
		while (f(x0+lambda*dx).Mag()>(1-lambda/2)*f(x0).Mag() && lambda >1/32)lambda /=2;
		return newton(f,x0+lambda*dx);
	}
}
