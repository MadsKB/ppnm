using System;
using static System.Console;
using static System.Math;
public static class math {
	public static vector randomVector(int n,Random rnd) {
		vector randVec = new vector(n);
		for (int i = 0; i<n;i++){
			randVec[i] = rnd.NextDouble();
		}
		return randVec;
	}
	//Generates a random n by m matrix
	public static matrix randomMatrix(int n, int m, Random rnd){
		
		var Out = new matrix(n,m);
		for (int i = 0; i<m;i++){
			Out[i] = randomVector(n, rnd);
		}
		return Out;
	}
	public static void Main(string[] args){
		

		int m = 4, n = 4;

		foreach(var arg in args){
			var words = arg.Split(":");

                if(words[0] == "-N") m = int.Parse(words[1]);
		if(words[0] == "-M") n = int.Parse(words[1]);
		}
		//A 4 by 5 matrix (n=5, m = 4)
		var rnd = new Random(132);
		var rndVector = randomVector(n,rnd);

		matrix A = randomMatrix(n,m,rnd);
		WriteLine("The A matrix:");
		A.print();
		WriteLine("The b vector in Ax=b");
		rndVector.print();
		QRGS decomp = new QRGS(A);
		WriteLine("Decomposes into the R and Q matricies:");
		decomp.R.print();
		decomp.Q.print();
		WriteLine("Checking that Q^TQ = I");
		((decomp.Q.T)*(decomp.Q)).print();
		WriteLine("And that QR = A");
		((decomp.Q)*(decomp.R)).print();
		WriteLine("The Solution to the system Ax=b is");
		var Solution = decomp.solve(rndVector);
		Solution.print();
		WriteLine("A*x is equal to");
		((A)*(Solution)).print();
		WriteLine("The inverse of A is");
		var B = decomp.inverse();
		B.print();
		WriteLine("A*A^-1 is");
		((A)*(B)).print();

			
	}
}
