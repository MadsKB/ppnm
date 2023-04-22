using System;
using static System.Console;
using static System.Math;
public static class math {
	public static vector M (double E,double rmin,double rmax,double acc, double eps) {
		Func<double,vector,vector> f = delegate(double x, vector y){
			var Out = new vector(2);
			Out[1] = -2*(E+1/x)*y[0];
			Out[0] = y[1]; //x should go [f,f']
			return Out;
		};
		var y0 = new vector(2);
		y0[0] = rmin-rmin*rmin;
		y0[1] = 1-2*rmin;
		var yerr = new vector(2);
		(y0,yerr) = ODE.driver(f,rmin,y0,rmax,0.01,acc,eps);
		return y0;

	}
	public static void Main(string[] args){
		int A = 0;
		int C = 0;
		int plot = 0;
		vector E0 = new vector(1);
		E0[0] = -1;
		double rmin = 0.001;
		double rmax = 8;
		double eps = 0.01;
		double acc = 0.01;
		foreach(var arg in args){
			var words = arg.Split(":");
		
		if(words[0] == "-A") A = int.Parse(words[1]);
                if(words[0] == "-C") C = int.Parse(words[1]);
		if(words[0] ==  "-plot") plot = int.Parse(words[1]);
		if(words[0] == "-E0") E0[0] = double.Parse(words[1]);
		if(words[0] == "-rmin") rmin = double.Parse(words[1]);
		if(words[0] == "-rmax") rmax = double.Parse(words[1]);
		if(words[0] == "-eps") eps = double.Parse(words[1]);
		if(words[0] == "-acc") acc = double.Parse(words[1]);
		}
		//Part A
		if (A ==1) {
			Func<vector,vector> f = delegate(vector x){
				var Out = new vector(2);
				Out[0] = 2*(200*Pow(x[0],3)-200*x[0]*x[1]+x[0]-1);
				Out[1] = 200*(x[1]-x[0]*x[0]);
				return Out;};

			vector x0 = new vector(2);
			roots.newton(f,x0).print();
		} else {
			Func<vector,vector> m = delegate(vector E){
				var v = new vector(2);
				v[0] = rmax*Exp(-Sqrt(-2*E[0])*rmax);
				v[1] = (1-rmax*Sqrt(-2*E[0]))*Exp(-Sqrt(-2*E[0])*rmax);
				return M(E[0],rmin,rmax,acc,eps) - v*C;
			};
			double Output = (roots.newton(m,E0))[0];
			if (plot == 0) {WriteLine($"{rmin} {rmax} {acc} {eps} {Output}");
			} else {			
			Func<double,vector,vector> Psi = delegate(double x, vector y){
				var Out = new vector(2);
				Out[1] = -2*(Output+1/x)*y[0];
				Out[0] = y[1]; //x should go [f,f']
				return Out;
			};
			var y0 = new vector(2);
			var rs = new genlist<double>();
			var fs = new genlist<vector>();
			y0[0] = rmin-rmin*rmin;
			y0[1] = 1-2*rmin;
			var yerr = new vector(2);
			(y0,yerr) = ODE.driver(Psi,rmin,y0,rmax,0.01,acc,eps,rs,fs);
			for (int i = 0; i<fs.size;i++) WriteLine($"{rs[i]} {(fs[0])[i]}");
			} 
		}
	}
}
