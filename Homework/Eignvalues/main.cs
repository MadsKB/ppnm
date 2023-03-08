using System;
using static System.Console;
using static System.Math;
public static class math {
	
	public static vector randomVector(int n, System.Random rnd) {
		vector randVec = new vector(n);
		for (int i = 0; i<n;i++){
			randVec[i] = rnd.NextDouble();
		}
		return randVec;
	}
//Generates a random n by m matrix
	public static matrix randomMatrix(int n, int m, System.Random rnd){
		var Out = new matrix(n,m);
		for (int i = 0; i<m;i++){
			Out[i] = randomVector(n, rnd);
		}return Out;
	}
	public static void Main(){
		
		var rnd = new Random(1223);
		//It works with n=2
		int n = 3;
		matrix A = randomMatrix(n,n,rnd);
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

	}
}
