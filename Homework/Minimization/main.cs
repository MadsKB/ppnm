using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(string [] args){
		string fitOutFile = null;
		double minEnergy = 101;
		double maxEnergy = 159;
		int numEnergySteps =10000;
		foreach(var arg in args){
			var words = arg.Split(":");
			if (words[0] == "-fitOut") fitOutFile = words[1];
		}
		if (fitOutFile == null) throw new ArgumentException("You need to give a data file for the fit");
		Func<vector, double> f = delegate(vector x){
			return (1-x[0])*(1-x[0])+100*Pow(x[1]-x[0]*x[0],2);
		};
		double EnergyStep = (maxEnergy-minEnergy)/numEnergySteps;
		Func<double,double,double,double,double> BritWigner = delegate(double E, double m, double gamma, double A) {return A/((E-m)*(E-m)+gamma*gamma/4);};

		var start = new vector(2);
		start[0] = 0.5;
		var Out = new vector(2);
		int steps = 0;
		(Out,steps) = opt.qnewton(f,start,0.000000001);
		WriteLine("Part A):");
		WriteLine($"qNewton Rosenbrock's valley function, root found at x = {Out[0]} y = {Out[1]}, found in {steps} steps");
		WriteLine("Analytical root: (1,1) \n");
		/*
		var simplex = new vector[3];
		simplex[0] = new vector(new[] {0.0,0.0});
		simplex[1] = new vector(new[] {2.0,0.0});
		simplex[2] = new vector(new[] {2.0,2.0});*/
		//(Out,steps) = opt.downhill_simplex(f,0.001,1,simplex);
		//WriteLine($"Simplex Rosenblock's valley function {Out[0]} {Out[1]} {steps}");
		f = delegate(vector x){
			return Pow(x[0]*x[0]+x[1]-11,2)+Pow(x[0]+x[1]*x[1]-7,2);
		};
		start[0] = 2; start[1] =-2;
		(Out,steps) = opt.qnewton(f,start,1e-10);
		WriteLine($"qNewton Himmelblau's function, root found at x = {Out[0]} y = {Out[1]}, found in {steps} steps");
		WriteLine("According to wikipedia, the function has a root at roughly (3.584428,-1.848126)\n");
		var energy = new genlist<double>();
		var signal = new genlist<double>();
		var error  = new genlist<double>();
		var separators = new char[] {' ','\t'};
		var options = StringSplitOptions.RemoveEmptyEntries;
		do{
			string line=Console.In.ReadLine();
			if(line==null)break;
			string[] words=line.Split(separators,options);
			energy.add(double.Parse(words[0]));
			signal.add(double.Parse(words[1]));
			error .add(double.Parse(words[2]));
		}while(true);
		//0: mass, 1: Gamma, 2: A
		f = delegate(vector x) {
			double result = 0;
			for (int i =0; i<energy.size;i++) result+=Pow((x[2]/(Pow(energy[i]-x[0],2)+x[1]*x[1]/4)-signal[i])/error[i],2);
			return result;
		};
		Out = new vector(3);
		start = new vector(3);
		start[0] = 120;
		start[1] = 2;
		start[2] = 2;
		(Out,steps) = opt.qnewton(f,start,0.0001);
		WriteLine("Part B):");
		WriteLine($"qNewton: m = {Out[0]}, Gamma = {Out[1]}, A = {Out[2]}, steps = {steps}");
		var outstream2 = new System.IO.StreamWriter(fitOutFile,append:false);
		
		for (int i = 0; i<numEnergySteps;i++){
			outstream2.WriteLine($"{EnergyStep*i+minEnergy} {BritWigner(EnergyStep*i+minEnergy,Out[0],Out[1],Out[2])}");
		}
		outstream2.WriteLine("\n");
		WriteLine("Part C):");
		//The nature of the simplex algorithem makes it way harder to pick good/close starting conditions; these were picked by
		//trial and error, but it still doesn't preform quite as well as qnewton on this task.
		var simplex = new vector[4];
		simplex[0] = new vector(new[] {130,4.5,11.5});
		simplex[1] = new vector(new[] {120.0,3.8,16});
		simplex[2] = new vector(new[] {140.0,4.65,15.5});
		simplex[3] = new vector(new[] {152.25,3.0,17.5});
		
		(Out,steps) = opt.downhill_simplex(f,1e-11,3,simplex);
		
		WriteLine($"Simplex: m = {Out[0]}, Gamma = {Out[1]}, A = {Out[2]}, steps = {steps}");
		for (int i = 0; i<numEnergySteps;i++){
			outstream2.WriteLine($"{EnergyStep*i+minEnergy} {BritWigner(EnergyStep*i+minEnergy,Out[0],Out[1],Out[2])}");
		}
	}
}
