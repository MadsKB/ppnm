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
	public static void Main(string[] args){
		
		double dr =0.1, rmax = 10;
		int sol = 0;
		bool waveOut = false;
		foreach(var arg in args) {
			var words = arg.Split(":");
		
			if (words[0] == "-dr") dr = double.Parse(words[1]);
			if (words[0] == "-rmax") rmax = double.Parse(words[1]);
			if (words[0] == "-sol") sol = int.Parse(words[1]);
			if (words[0] == "-waveOut") waveOut = bool.Parse(words[1]);
		}		
		

		int npoints = (int)(rmax/dr)-1;
		vector r = new vector(npoints);
		for (int i=0;i<npoints;i++) r[i]=dr*(i+1);
		matrix H = new matrix(npoints,npoints);
		for(int i=0;i<npoints-1;i++){
			H[i,i] =-2;
			H[i,i+1] =1;
			H[i+1,i] = 1;
		}
		H[npoints-1,npoints-1]=-2;
		matrix.scale(H,-0.5/dr/dr);
		for(int i=0;i<npoints;i++)H[i,i]-=1/r[i];

		matrix V = jacobi.cyclic(H);
		

		if (waveOut) {
		for (int i = 0; i<npoints;i++) WriteLine($"{r[i]} {V[i,sol]}");
		} else {

		WriteLine($"{dr} {rmax} {H[sol,sol]}");
		}
	}
}
