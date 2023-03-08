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
		}
		return Out;
	}
	//This is the same as main.cs but with just the time messurement (There's also the matrix generation, which should go as n^2 so souldn't matter as much)
	public static void Main(string[] args){
		int m = 4, n = 4;

		foreach(var arg in args){
			var words = arg.Split(":");

                if(words[0] == "-N") m = int.Parse(words[1]);
		if(words[0] == "-M") n = int.Parse(words[1]);
		}
		WriteLine(m);
		var rnd = new Random(132);
		matrix A = randomMatrix(n,m,rnd);
		QRGS decomp = new QRGS(A);

			
	}
}
