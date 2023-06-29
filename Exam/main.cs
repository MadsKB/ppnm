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
		double shift = 0;
		double acc = 1e-3;
		double recalRelErr = 0.01;
		int version = 1;
		int n = 3;
		int recalEvery = 10;
		int seed = 100;
		foreach (var arg in args) {
			var words = arg.Split(":");
			if (words[0] == "-timingTest") timingTest = bool.Parse(words[1]);
			if (words[0] == "-version") version = int.Parse(words[1]);
			if (words[0] == "-n") n = int.Parse(words[1]);
			if (words[0] == "-seed") seed = int.Parse(words[1]);
			if (words[0] == "-recalEvery") recalEvery = int.Parse(words[1]);
			if (words[0] == "-shift") shift = double.Parse(words[1]);
			if (words[0] == "-acc") acc = double.Parse(words[1]);
			if (words[0] == "-recalRelErr") recalRelErr = double.Parse(words[1]);
		}

		if (timingTest) {
			var rnd = new System.Random(seed);
			var A = randomSymMatrix(n,rnd);	
			double lambda = 0;
			int iterations = 0;
			var v = new vector(n);
			if (version == 2) {
			//	(lambda,v,iterations) = power.inverse_iteration(A,0,1e-1 );
				(lambda,v,iterations) = power.inverse_iterationV2(A,shift,acc,recalEvery,null,seed,rnd);
			} else if (version == 3){

	
				(lambda,v,iterations) = power.inverse_iterationV3(A,shift,acc,recalRelErr,null,seed,rnd);
			} else if (version == 0){
				
				(lambda,v,iterations) = power.inverse_iterationUnsafe(A,shift,acc,null,seed,rnd);
			}else{
			//	(lambda,v,iterations) = power.inverse_iteration(A,0,1e-1);
				(lambda,v,iterations) = power.inverse_iteration(A,shift,acc,null,seed,rnd);
			}
		} else {
		double lambda = 0;
		int iterations = 0;
		var v = new vector(3);
		var A = new matrix(3,3);
		A[0,0] = 5; A[1,0] = 2; A[2,0] = 1;
		A[0,1] = 2; A[1,1] = 7; A[2,1] = 8;
		A[0,2] = 1; A[1,2] = 8; A[2,2] = 9;
		WriteLine("According to wolframalpha, the matrix:");
		A.print();
		WriteLine("Has the eigenvalues and vectors \n");
		WriteLine("v_1≈(0.244653, 0.899346, 1)^T, λ_1≈16.4394");
		WriteLine("v_2≈(-3.96181, -0.0341683, 1)^T, λ_2≈4.76484");
		WriteLine("v_3≈(0.262615, -1.18336, 1)^T, λ_3≈-0.204261 \n");
		//Vanilla algorithem test
		
		WriteLine("Preforming our unsafe vanilla algorithem with a shift of 100, 8, and 0 (With accuracy goal set to 1e-6) yields");
		(lambda,v,iterations) = power.inverse_iterationUnsafe(A,100,1e-6);
		double k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k; //Wolframalpha normalizes the eigenvectors by letting the last coordinat be equal to one; We're doing the same here for the sake of comparison
		WriteLine($"shift 100: eigenvar = {lambda}, found in {iterations} iterations; Eigenvector:");
		v.print();
		
		(lambda,v,iterations) = power.inverse_iterationUnsafe(A,8,1e-6);
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k;
		WriteLine($"\nshift 8: eigenvar = {lambda}, found in {iterations} iterations; Eigenvector");
		v.print();
		
		(lambda,v,iterations) = power.inverse_iterationUnsafe(A,0,1e-6);
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k;
		WriteLine($"\nshift 0: eigenvar = {lambda}, found in {iterations} iterations; Eigenvector");
		v.print();
		WriteLine("\nPreforming our safe vanilla algorithem with a shift of 100, 8, and 0 (With accuracy goal set to 1e-6) yields");
		(lambda,v,iterations) = power.inverse_iteration(A,100,1e-6);
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k; //Wolframalpha normalizes the eigenvectors by letting the last coordinat be equal to one; We're doing the same here for the sake of comparison
		WriteLine($"shift 100: eigenvar = {lambda}, found in {iterations} iterations; Eigenvector:");
		v.print();
		
		(lambda,v,iterations) = power.inverse_iteration(A,8,1e-6);
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k;
		WriteLine($"\nshift 8: eigenvar = {lambda}, found in {iterations} iterations; Eigenvector");
		v.print();
		
		(lambda,v,iterations) = power.inverse_iteration(A,0,1e-6);
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k;
		WriteLine($"\nshift 0: eigenvar = {lambda}, found in {iterations} iterations; Eigenvector");
		v.print();
		//Testing providing a eigenvector
		WriteLine($"\n\nRepeating the previous, but with a starting eigenvector near(ish) to the real one");	
		(lambda,v,iterations) = power.inverse_iteration(A,100,1e-6);	
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k;
		(lambda,v,iterations) = power.inverse_iteration(A,100,1e-6,v+new vector(new double[] {1,1,1}));	
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k;
		WriteLine($"shift 100: eigenvar = {lambda}, found in {iterations} iterations; Eigenvector");
		v.print();
		(lambda,v,iterations) = power.inverse_iteration(A,5,1e-6);	
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k;
		(lambda,v,iterations) = power.inverse_iteration(A,5,1e-6,v+new vector(new double[] {1,1,1}));	
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k;
		WriteLine($"\nshift 5: eigenvar = {lambda}, found in {iterations} iterations; Eigenvector");
		v.print();
		(lambda,v,iterations) = power.inverse_iteration(A,0,1e-6);	
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k;
		(lambda,v,iterations) = power.inverse_iteration(A,0,1e-6,v+new vector(new double[] {1,1,1}));	
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k;
		WriteLine($"\nshift 0: eigenvar = {lambda}, found in {iterations} iterations; Eigenvector");
		v.print();

		//Testing recalculation of the matrix
		WriteLine("\n\nNow we test a version of our algorithem 'improved' by having it recalculate the matrix A every 10 itterations as suggested in the book");
		
		(lambda,v,iterations) = power.inverse_iterationV2(A,100,1e-6);
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k; //Wolframalpha normalizes the eigenvectors by letting the last coordinat be equal to one; We're doing the same here for the sake of comparison
		WriteLine($"shift 100: eigenvar = {lambda}, found in {iterations} iterations; Eigenvector:");
		v.print();
		
		(lambda,v,iterations) = power.inverse_iterationV2(A,5,1e-6);
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k;
		WriteLine($"\nshift 5: eigenvar = {lambda}, found in {iterations} iterations; Eigenvector");
		v.print();
		
		(lambda,v,iterations) = power.inverse_iterationV2(A,0,1e-6);
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k;
		WriteLine($"\nshift 0: eigenvar = {lambda}, found in {iterations} iterations; Eigenvector");
		v.print();
		WriteLine("\n\nNow yet another version of our algorithem, which recalculates the matrix whenever the change in the error between the current, and previous itteration becomes too small");

		(lambda,v,iterations) = power.inverse_iterationV3(A,100,1e-6);
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k; //Wolframalpha normalizes the eigenvectors by letting the last coordinat be equal to one; We're doing the same here for the sake of comparison
		WriteLine($"shift 100: eigenvar = {lambda}, found in {iterations} iterations; Eigenvector:");
		v.print();
		
		(lambda,v,iterations) = power.inverse_iterationV3(A,5,1e-6);
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k;
		WriteLine($"\nshift 5: eigenvar = {lambda}, found in {iterations} iterations; Eigenvector");
		v.print();
		
		(lambda,v,iterations) = power.inverse_iterationV3(A,0,1e-6);
		k = v[2]; v[2] = 1; v[1] /= k; v[0] /=k;
		WriteLine($"\nshift 0: eigenvar = {lambda}, found in {iterations} iterations; Eigenvector");
		v.print();
		}
	}
}
