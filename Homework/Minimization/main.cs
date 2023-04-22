using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(){
		
		Func<vector, double> f = delegate(vector x){
			return (1-x[0])*(1-x[0])+100*Pow(x[1]-x[0]*x[0],2);
		};
		var start = new vector(2);
		start[0] = 0.5;
		var Out = new vector(2);
		int steps = 0;
		(Out,steps) = opt.qnewton(f,start,0.01);
		WriteLine($" {Out[0]} {Out[1]} {steps}");
		f = delegate(vector x){
			return Pow(x[0]*x[0]+x[1]-11,2)+Pow(x[0]+x[1]*x[1]-7,2);
		};
		(Out,steps) = opt.qnewton(f,start,0.01);
		WriteLine($"{Out[0]} {Out[1]} {steps}");

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
		start[1] = 10;
		start[2] = 1;
		(Out,steps) = opt.qnewton(f,start,0.0001);
		WriteLine($"m = {Out[0]}, Gamma = {Out[1]}, A = {Out[2]}, steps = {steps}");

	}
}
