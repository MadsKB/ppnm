using System;
using static System.Console;
using static System.Math;
public static class math {
	
	public static double fittedFunc (Func<double,double>[] fs, vector c ,double x){
		double y = 0;
		for (int i = 0;i<c.size;i++){
			y+=c[i]*fs[i](x);
		}
		return y;
	}

	public static (double,double) calcT (matrix S, vector c){
		double halfTime = Log(2)/(-c[0]);
		double error = Log(2)*Sqrt(S[0,0])/Pow((c[0]),2);
		return (halfTime, error);
	}
	public static void Main(string[] args){
		
		int samples = 100;
		double start = 0;
		double end = 1;
		bool fitOut = true;
		vector x = new vector(2),y = new vector(2),dy = new vector(2);
		foreach (var arg in args) {
		var words = arg.Split(":");
		if (words[0] == "-fitOut") fitOut = bool.Parse(words[1]);
		if (words[0] == "-samples") samples = int.Parse(words[1]);
		if (words[0] == "-start") start = double.Parse(words[1]);
		if (words[0] == "-end") end = double.Parse(words[1]);
		if (words[0] == "-x"){
			var xdata = words[1].Split(","); //This could be done more efficiently, but oh well
			x = new vector(xdata.Length);
			for (int i =0;i<xdata.Length;i++) x[i] = double.Parse(xdata[i]);	
		}
		if (words[0] == "-y"){
			var ydata = words[1].Split(","); //This could be done more efficiently, but oh well
			y = new vector(ydata.Length);
			for (int i =0;i<ydata.Length;i++) y[i] = double.Parse(ydata[i]);	
		}
		
		if (words[0] == "-dy"){
			var dydata = words[1].Split(","); //This could be done more efficiently, but oh well
			dy = new vector(dydata.Length);
			for (int i =0;i<dydata.Length;i++) dy[i] = double.Parse(dydata[i]);
				
			}

		};
		var fs = new Func<double,double>[] {z => z,z =>1};
		for (int i = 0; i<y.size;i++){
			dy[i] = dy[i]/y[i];
			y[i] = Log(y[i]);
		}
		
		var (c,S) = Fitting.lsfit(fs,x,y,dy);
		if (fitOut) {	
		double dx = (end-start)/samples;
		var cNeg = c.copy();
		var cPos = c.copy();
		for (int i =0;i<cNeg.size;i++){
			cNeg[i] = cNeg[i]-Sqrt(S[i,i]);
			cPos[i] = cPos[i]+Sqrt(S[i,i]);
		}
		for (int i = 0;i<samples;i++) {
			WriteLine($"{dx*i} {Exp(fittedFunc(fs,c,dx*i))} {Exp(fittedFunc(fs,cNeg,dx*i))} {Exp(fittedFunc(fs,cPos,dx*i))}");
		}
		}
		else {
			var (halfTime, error) = calcT(S,c);
			WriteLine($"HalfTime: {halfTime} +- {error} days");
			WriteLine($"Modern value (from wikipedia): 3.6319(23) days");
			WriteLine($"Our value isn't within the uncertanty bounds of the moderen one, but not too far off either, so i belive the program worked");
		}

	}
}
