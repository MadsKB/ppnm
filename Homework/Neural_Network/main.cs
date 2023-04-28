using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(){
		//Part A and B
		int n = 5;
		int l = 100;
		vector x = new vector(l);
		vector y = new vector(l);
		for (int i = 0; i<l;i++){
			x[i] = 2.0/l*i-1;
			y[i] = Cos(5*x[i]-1)*Exp(-x[i]*x[i]);
			//WriteLine($"{x[i]} {y[i]}");
		}
		Func<double,double> gaussWavelet = delegate(double S){return S*Exp(-S*S);};
		Func<double,double> gaussWaveletPrime = delegate(double S){return (1-2*S*S)*Exp(-S*S);};
		Func<double,double> gaussWaveletDPrime = delegate(double S){return 2*S*(2*S*S-3)*Exp(-S*S);};
		Func<double,double> gaussWaveletAntiPrime = delegate(double S){return -Exp(-S*S)/2;};
		var netWork = new ann(n,gaussWavelet,gaussWaveletPrime,gaussWaveletAntiPrime,gaussWaveletDPrime);

		var rng = new Random(1234);
		var v = new vector(n*3);
		for (int i = 0;i<n;i++) {
			v[3*i] = rng.NextDouble()%1; //Weight
			v[3*i+1] = rng.NextDouble()%1000; //Scale
			v[3*i+2] = rng.NextDouble()%1000; //Shift
			
		}
		netWork.setP(v);
		netWork.train(x,y);
		for (int i = 0; i<200;i++){
			double X = 2.0/200*i-1;
			WriteLine($"{X} {netWork.response(X)} {Cos(5*X-1)*Exp(-X*X)} {netWork.trainResponse(X,v)} {(-5*Sin(5*X-1)-2*X*Cos(5*X-1))*Exp(-X*X)} {netWork.responsePrime(X)} {netWork.responseInt(X)} {netWork.responseDPrime(X)}");
		}
		//Part C
		//Vector y goes [y'', y', y, x]
		Func<vector,double> DiffEq = delegate(vector y){return y[0]+2*y[2];} //Simple harmonic ossilator
		double a = 0; double b = 4;
		double alpha = 1000, double beta = 1000;
		Func<vector,double> Cost = delegate(vector pn,double x) {return }
		
		

	}
}
