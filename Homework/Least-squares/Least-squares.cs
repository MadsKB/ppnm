using System;
using static System.Math;
using static System.Console;
public static class Fitting {
	public static (vector, matrix) lsfit(Func<double,double>[] fs, vector x, vector y, vector dy){
		matrix A = new matrix(y.size,fs.Length);
		vector b = new vector(y.size);
		//Making the matrix A
		for (int i = 0; i<y.size;i++){
			b[i] = y[i]/dy[i];
			for(int k = 0; k<fs.Length;k++){
				A[i,k] = fs[k](x[i])/dy[i];
			}
		}
		var decomp = new QRGS(A);
		var c = decomp.solve(b);
		var decomp2 = new QRGS(decomp.R.T*decomp.R);
		var S = decomp2.inverse(); //Feel like there's a better/computationally faster way of doing this, but this is the lazyiest i can come up with.
		return (c,S);
	}
}
