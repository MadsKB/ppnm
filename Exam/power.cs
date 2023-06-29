using System;
using static System.Console;
using static System.Math;
public static class power {
	public static vector randomVector(int n, Random rnd) {
	vector randVec = new vector(n);
	for (int i =0;i<n;i++){
		randVec[i] = rnd.NextDouble();
	}
	return randVec;
	}
	public static (double,vector,int) inverse_iterationUnsafe(matrix A,double s,double acc = 0.001, vector x_old = null,int seed = 100,Random rnd=null){
		//Check if A is a square matrix
		int iterations = 0;
		if (x_old == null) {
			if (rnd == null){
				if (seed == null){
					seed = 100; //Add something extra to 
				}
				rnd = new Random(seed);
			}
			
			x_old = randomVector(A.size1,rnd);
		
		}
		var decomp = new QRGS(A-s*matrix.id(A.size1));
		var x_new = decomp.solve(x_old);
		var lambda_new = s+(x_new.dot(x_old))/(x_new.dot(x_new));
		double lambda_old = 0;
		do{
			iterations+=1;
			x_old = x_new;
			lambda_old = lambda_new;
			x_new = decomp.solve(x_old);

			lambda_new = s+(x_new.dot(x_old))/(x_new.dot(x_new));

			if (Abs(lambda_new-lambda_old)<acc) {
				break;
			}
		}
		while (true);
		return (lambda_new,x_new,iterations);
		}
	public static (double,vector,int) inverse_iteration(matrix A,double s,double acc = 0.001, vector x_old = null,int seed = 100,Random rnd=null){
		//Check if A is a square matrix
		int iterations = 0;
		if (x_old == null) {
			if (rnd == null){
				if (seed == null){
					seed = 100; //Add something extra to 
				}
				rnd = new Random(seed);
			}
			
			x_old = randomVector(A.size1,rnd);
		
		}
		var decomp = new QRGS(A-s*matrix.id(A.size1));
		var x_new = decomp.solve(x_old);
		x_new /= Sqrt(x_new.dot(x_new)); //Normalizing the vector is nessesary so as to prevent numerical instability
		var lambda_new = s+(x_new.dot(x_old))/(x_new.dot(x_new));
		double lambda_old = 0;
		double xLen = 0;
		do{
			iterations+=1;
			x_old = x_new;
			lambda_old = lambda_new;
			x_new = decomp.solve(x_old);

			xLen = Sqrt(x_new.dot(x_new)); //It's nessesary to normalize the vector to prevent numerical instability
			x_new /= xLen;
			lambda_new = s+(x_new.dot(x_old))/(xLen);

			if (Abs(lambda_new-lambda_old)<acc) {
				break;
			}
		}
		while (true);
		return (lambda_new,x_new,iterations);
	}
	//Imporved version with recalculation of matrix
	public static (double,vector,int) inverse_iterationV2(matrix A,double s,double acc = 0.001,int recalEvery = 10, vector x_old = null,int seed = 100,Random rnd=null){
		//Check if A is a square matrix
		int iterations = 0;
		if (x_old == null) {
			if (rnd == null){
				if (seed == null){
					seed = 100; //Add something extra to 
				}
				rnd = new Random(seed);
			}
			
			x_old = randomVector(A.size1,rnd);
		
		}
		var decomp = new QRGS(A-s*matrix.id(A.size1));
		var x_new = decomp.solve(x_old);
		var lambda_new = s+(x_new.dot(x_old))/(x_new.dot(x_new));
		double lambda_old = 0;
		double xLen = 0;
		do{
			iterations+=1;
			x_old = x_new;
			lambda_old = lambda_new;
			x_new = decomp.solve(x_old);
			xLen = Sqrt(x_new.dot(x_new));
			x_new /= xLen; //Normalizing the vector is nessesary so as to prevent numerical instability
			lambda_new = s+(x_new.dot(x_old))/(xLen);
			
			if (Abs(lambda_new-lambda_old)<acc) {
				break;
			}
			if (iterations%recalEvery == 0) {
				s = lambda_new;
				decomp = new QRGS(A-lambda_new*matrix.id(A.size1));
			}
		}
		while (true);
		return (lambda_new,x_new,iterations);
	}

	public static (double,vector,int) inverse_iterationV3(matrix A,double s,double acc = 0.001,double recalRelErr = 0.1, vector x_old = null,int seed = 100,Random rnd=null){
		//Check if A is a square matrix
		int iterations = 0;
		if (x_old == null) {
			if (rnd == null){
				if (seed == null){
					seed = 100; //Add something extra to 
				}
				rnd = new Random(seed);
			}
			
			x_old = randomVector(A.size1,rnd);
		
		}
		var decomp = new QRGS(A-s*matrix.id(A.size1));
		var x_new = decomp.solve(x_old);

		x_new /= Sqrt(x_new.dot(x_new)); //Normalizing the vector is nessesary so as to prevent numerical instability
		var lambda_new = s+(x_new.dot(x_old))/(x_new.dot(x_new));
		double lambda_old = 0;
		double oldErr = 0;
		double newErr = 0;
		double xLen = 0;
		do{
			iterations+=1;
			x_old = x_new;
			lambda_old = lambda_new;
			x_new = decomp.solve(x_old);
			xLen = Sqrt(x_new.dot(x_new));
			x_new /= xLen; //Normalizing the vector is nessesary so as to prevent numerical instability
			lambda_new = s+(x_new.dot(x_old))/(xLen);
			newErr = Abs(lambda_new-lambda_old);
			if (newErr<acc) {
				break;
			}
			if ((oldErr-newErr)<acc*recalRelErr) {
				s = lambda_new;
				decomp = new QRGS(A-lambda_new*matrix.id(A.size1));
			}
			oldErr = newErr;
		}
		while (true);
		return (lambda_new,x_new,iterations);
	}
}
