using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(string[] args){
		bool plot = false;
		int Nmax = (int)1e5;
		int Nmin = (int)1000;
		int Nstep = 1000;
		int N = 0;
		Func<vector,double> CosSin = delegate(vector x) {return Cos(x[0])*Sin(x[1]);};
		Func<vector,double> Circle = delegate(vector x) {if ((x[0]*x[0]+x[1]*x[1])<=1){return 1;} else {return 0;}; };
		foreach (string arg in args) {
			var words = arg.Split(":");
			if (words[0] == "-plot") plot = bool.Parse(words[1]);
			if (words[0] == "-Nmax") Nmax = int.Parse(words[1]);
			if (words[0] == "-Nmin") Nmin = int.Parse(words[1]);
			if (words[0] == "-Nstep") Nstep = int.Parse(words[1]);
		}
		var a = new vector(2);
		var b = new vector(2);
	       	b[0]=PI/2; b[1] = PI/2;
		var resA = (0.0,0.0);
		var resB = (0.0,0.0);
		if (plot) {
			for (int i = 0; i<(Nmax-Nmin)/Nstep;i++){
				N = (Nmax-Nmin)/Nstep*i;
				resA = monteCarlo.plainmc(CosSin,a,b,N);
				resB = monteCarlo.quasimc(CosSin,a,b,N);
				WriteLine($"{N} {resA.Item2} {Abs(1-resA.Item1)} {resB.Item2} {Abs(1-resB.Item1)}");
			}
		}else {
		N = (int)1e6;
		WriteLine("part A):");
		WriteLine($"First we integrate cos(x)*sin(y) with x and y limits 0 to pi/2 and set number of points N = 1e6 (See 'ErrScaling.svg' for error scaling plot) \nIn the following, First element of each tuple is the result, second is the estimated error \n");
		WriteLine($"Analytical result: 1, numerical: {monteCarlo.plainmc(CosSin,a,b,N)} \n");
		WriteLine($"Next, integrating over a circle of radius 1");
		a[0] = -1; a[1] = -1; b[0] =1; b[1] =1;
		WriteLine($"Analytical result: π, numerical: {monteCarlo.plainmc(Circle,a,b,N)} \n");
		a = new vector(3);
		b = new vector(3); for (int i = 0; i<b.size;i++) b[i] = PI;
		WriteLine("Now the heavily singular, 'fun' integral");
		WriteLine("∫_0^π  dx/π ∫_0^π  dy/π ∫_0^π  dz/π [1-cos(x)cos(y)cos(z)]^(-1) = Γ(1/4)4/(4π^3) ≈ 1.3932039296856768591842462603255");
		WriteLine($"Our result evaluating the integral above: {monteCarlo.plainmc(delegate(vector x){return 1/(1-Cos(x[0])*Cos(x[1])*Cos(x[2]))/PI/PI/PI;},a,b,N*10)}\n");
		WriteLine("Part B):");
		a = new vector(2);
		b = new vector(2);
	       	b[0]=PI/2; b[1] = PI/2;
		WriteLine("Integrating cos(x)*sin(y) again with the same limits as before, this time with qusai random sampling (Again see 'ErrScaling.svg' for error scaling plot)");
		WriteLine($"Theoretical: 1, numerical quasi: {monteCarlo.quasimc(delegate(vector x){return Cos(x[0])*Sin(x[1]);},a,b,N)}\n");
		WriteLine("Part C):");
		WriteLine("Integrating the step function: f(x,y) = 0 if x<0.3, f(x,y) =1 otherwise. Limits are 0 to 1 for both x and y and N=500. Using both plainmc and stratified sampling \n");
		N = (int)500;
		b[0]=1; b[1] = 1;
		Func<vector,double> step = delegate(vector x){if (x[0]<0.3) {return 0;}else {return 1;}};
		//montoCarlo.plainmc_stratified(step,a,b,N);
		WriteLine($"Analytical: 0.7, plainmc: {monteCarlo.plainmc(step,a,b,N)}");
		WriteLine($"Analytical: 0.7, stratified: {monteCarlo.plainmc_stratified(step,a,b,N)} \n");
		WriteLine("Now integrating over a circle of radius 1, setting N = 100000");
		a[0] = -1; a[1] = -1; b[0] =1; b[1] =1; N= 100000;
		WriteLine($"Analytical: π, plainmc: {monteCarlo.plainmc(Circle,a,b,N)}");
		WriteLine($"Analytical: π, stratified: {monteCarlo.plainmc_stratified(Circle,a,b,N)}");
		}
	}
}
