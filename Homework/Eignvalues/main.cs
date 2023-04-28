using System;
using static System.Console;
using static System.Math;
public static class math {
	public static matrix randomSymMatrix (int n, System.Random rnd){
		var Out = new matrix(n,n);
		for (int i = 0; i<n;i++){
			for (int j = i; j<n;j++){
				double rn = rnd.NextDouble();
				Out[i,j] = rn;
				Out[j,i] = rn;
			}
		}
		return Out;
	}
	public static void Main(){
		
		var rnd = new Random(1223);
		//It works with n=2
		int n = 3;
		matrix A = randomSymMatrix(n,rnd);
		matrix D = A.copy();
		matrix V = jacobi.cyclic(D);
		WriteLine("Starting matrix (A)");
		A.print();
		WriteLine("Resulting V and D matricies");
		V.print();
		D.print();
		WriteLine($"V^TAV == D? => {D.approx(V.T*A*V)}");
		WriteLine($"VDV^T == A => {A.approx(V*D*V.T)}");
	      	((V.T)*(V)).print();
		((V)*(V.T)).print();

		WriteLine("Optimized version:");
		D = A.copy();
		V = jacobi.cyclicOpt(D);
		WriteLine("Starting matrix (A)");
		A.print();
		WriteLine("Resulting V and D matricies");
		V.print();
		D.print();
		WriteLine($"V^TAV == D? => {D.approx(V.T*A*V)}");
		WriteLine($"VDV^T == A => {A.approx(V*D*V.T)}");
	      	((V.T)*(V)).print();
		((V)*(V.T)).print();

	}
}
