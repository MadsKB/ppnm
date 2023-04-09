using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(){
		Func<double,double> f = delegate(double x) {return Sqrt(x);};
		double a = 0;
		double b = 1;

		Func<double,double> g = delegate(double x) {return 1/Sqrt(x);};
		WriteLine($"Part A): (result, error, number of integral evaluations)");
		WriteLine($"∫01 dx √(x) = 2/3; 		Numerical Result: {NumInt.integrate(f,a,b)}");
		WriteLine($"∫01 dx 1/√(x)=2; 		Numerical Result: {NumInt.integrate(delegate(double x){return 1/Sqrt(x);},0,1)}");
		WriteLine($"∫01 dx 4√(1-x²) = π; 	Numerical Result: {NumInt.integrate(delegate(double x){return 4*Sqrt(1-x*x);},0,1)}");
		WriteLine($"∫01 dx ln(x)/√(x) = -4; 	Numerical Result: {NumInt.integrate(delegate(double x){return Log(x)/Sqrt(x);},0,1)}");
		WriteLine($"\nPart B):");
		WriteLine($"∫01 dx 1/√(x) = 2: Clenshaw-Curtis Result: {NumInt.clenshawIntegrate(g,a,b)} (Scipy quad method had 231 evaluations)" );
		WriteLine($"∫01 dx ln(x)/√(x) = -4: Clenshaw-Curtis Result: {NumInt.clenshawIntegrate(delegate(double x){return Log(x)/Sqrt(x);},a,b)} (Scipy quad method had 315 evalutations)");
		WriteLine($"As one can see, the variable transformed integration rutines preform much better; in the first case it even preforms better then the integration rutine from scipy/python (Absolute/relative tolorance was set to be identical; it should be noted that quad achived a much lower error then our method, on the order of 10^(-13)");

	}
}
