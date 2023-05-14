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
	public static void Main(string[] args){
		
		int seed = 1223;
		int n = 3;
		bool opt = false;
		foreach (var arg in args){
			var words = arg.Split(":");
			
			if(words[0] == "-seed") seed = int.Parse(words[1]);
			if(words[0] == "-n") n = int.Parse(words[1]);
			if(words[0] == "-opt") opt = bool.Parse(words[1]);
		}
		var rnd = new Random(seed);
		//It works with n=2
		matrix A = randomSymMatrix(n,rnd);
		matrix D = A.copy();
		
		if (opt) {
		matrix V = jacobi.cyclicOpt(D);
		} else{
		matrix V = jacobi.cyclic(D);
		}

	}
}
