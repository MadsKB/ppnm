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
		Func<double,double> demoFunc = delegate(double X) {return Cos(5*X-1)*Exp(-X*X);};
		for (int i = 0; i<200;i++){
			double X = 2.0/200*i-1;
			WriteLine($"{X} {netWork.response(X)} {demoFunc(X)} {netWork.response(X,v)} {(-5*Sin(5*X-1)-2*X*Cos(5*X-1))*Exp(-X*X)} {netWork.responsePrime(X)} {netWork.responseInt(X)-netWork.responseInt(0)} {NumInt.integrate(demoFunc,0,X).Item1} {netWork.responseDPrime(X)} {Exp(-X*X)*((4*X*X-27)*Cos(5*X-1)+20*X*Sin(5*X-1))}");
		}
		WriteLine($"\n");
		//Part C
		//Vector y goes [y'', y', y, x]
		Func<vector,double> DiffEq = delegate(vector Y){return Y[0]+2*Y[2];}; //Simple harmonic ossilator
		double a = -1; double b = 1;
		double alpha = 1000; double beta = 1000;
		double x0 = 0; double x0Prime = 0; double y0 = 1; double y0Prime = 0;
		Func<vector,ann,double> Cost = delegate(vector pn,ann net) {
			Func<double,double> integ = delegate(double k){
				var V = new vector(4);
				V[0] = net.responseDPrime(k,pn);
				V[1] = net.responsePrime(k,pn);
				V[2] = net.response(k,pn);
				V[3] = k;
				return Pow(DiffEq(V),2);
			};
			var Out = NumInt.integrate(integ,a,b).Item1+alpha*Pow(net.response(x0,pn)-y0,2)+beta*Pow(net.responsePrime(x0Prime,pn)-y0Prime,2);
			return Out;
		};

		for (int i = 0;i<n;i++) {
			v[3*i] = rng.NextDouble()%10; //Weight
			v[3*i+1] = rng.NextDouble()%100; //Scale
			v[3*i+2] = rng.NextDouble()%100; //Shift
			
		}
		netWork.setP(v);
		netWork.train(null,null,Cost);
		for (int i = 0; i<200;i++){
			double X = 2.0/200*i-1;
			WriteLine($"{X} {netWork.response(X)} {Cos(Sqrt(2)*X)}");
		}
	}
}
