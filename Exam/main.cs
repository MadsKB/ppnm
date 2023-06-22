using System;
using static System.Console;
using static System.Math;
public static class math {
	public static matrix randomSymMatrix (int n, System.Random rnd){
		var Out = new matrix(n,n);
		for (int i =0;i<n;i++){
			for (int j=i;j<n;j++){
			double rn = rnd.NextDouble();
			Out[i,j] = rn;
			Out[j,i] = rn;
			}
		}
		return Out;
	}
	public static void Main(string [] args){
		bool timingTest = false;
		bool improved = false;
		int n = 3;
		int recalEvery = 10;
		int seed = 100;
		foreach (var arg in args) {
			var words = arg.Split(":");
			if (words[0] == "-timingTest") timingTest = bool.Parse(words[1]);
			if (words[0] == "-improved") improved = bool.Parse(words[1]);
			if (words[0] == "-n") n = int.Parse(words[1]);
			if (words[0] == "-seed") seed = int.Parse(words[1]);
			if (words[0] == "-recalEvery") recalEvery = int.Parse(words[1]);
		}

		if (timingTest) {
			var rnd = new System.Random(seed);
			var A = randomSymMatrix(n,rnd);	
			double lambda = 0;
			int iterations = 0;
			var v = new vector(n);
			if (improved) {
				(lambda,v,iterations) = power.inverse_iterationV2(A,0,0.001,recalEvery );
			} else{
				(lambda,v,iterations) = power.inverse_iteration(A,0);
			}
		} else {
		double lambda = 0;
		int iterations = 0;
		var v = new vector(3);
		var A = new matrix(3,3);
		A[0,0] = 5; A[1,0] = 2; A[2,0] = 1;
		A[0,1] = 2; A[1,1] = 7; A[2,1] = 8;
		A[0,2] = 1; A[1,2] = 8; A[2,2] = 9;
		(lambda,v,iterations) = power.inverse_iteration(A,100);
		double k = v[2];
		v[2] = 1;
		v[1] /= k;
		v[0] /=k;
		WriteLine($"eigenvar = {lambda}, found in {iterations} iterations");
		v.print();
		(lambda,v,iterations) = power.inverse_iterationV2(A,100);
		k = v[2];
		v[2] = 1;
		v[1] /= k;
		v[0] /=k;
		WriteLine($"eigenvar = {lambda}, found in {iterations} iterations");
		v.print();

		}
	}
}
