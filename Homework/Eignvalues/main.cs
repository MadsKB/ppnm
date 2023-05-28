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
		WriteLine("Part A):");
		WriteLine("Starting matrix (A)");
		A.print();
		WriteLine("\nResulting V and D matricies (V first)");
		V.print();
		WriteLine("\nAnd D");
		D.print();
		WriteLine("\nV^TAV gives");
		((V.T)*A*V).print();
		WriteLine("\nVDV^T gives");
		((V)*(D)*(V.T)).print();
		WriteLine($"\nV^TAV == D? => {D.approx(V.T*A*V)}");
		WriteLine($"VDV^T == A => {A.approx(V*D*V.T)}");
		WriteLine("\nV^TV and VV^T gives (Should be identity)");
	      	((V.T)*(V)).print();
		((V)*(V.T)).print();
		
		WriteLine("\nPart C, Optimized version):");
		D = A.copy();
		V = jacobi.cyclicOpt(D);
		WriteLine("Starting matrix (A)");
		A.print();
		WriteLine("\nResulting V and D matricies (V first)");
		V.print();
		WriteLine("\nAnd D");
		D.print();
		WriteLine("\nV^TAV gives (Should be D)");
		((V.T)*A*V).print();
		WriteLine("\nVDV^T gives (Should be A)");
		((V)*(D)*(V.T)).print();
		WriteLine("\nV^TV and VV^T gives (Should be identity)");
	      	((V.T)*(V)).print();
		((V)*(V.T)).print();

	}
}
