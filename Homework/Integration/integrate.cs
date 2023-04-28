using System;
using static System.Console;
using static System.Math;
public static class NumInt {
	public static (double,double,int) integrate
		(Func<double,double> f, double a, double b,
		 double acc=0.001, double eps=0.001, double f2=double.NaN, double f3=double.NaN, int count = 0)
		{
			//Ensuring that b>a
			if (b<a) return integrate(delegate(double x){return -f(x);},b,a,acc,eps);
			//Checking for infinite limits
			if (double.IsInfinity(b) && double.IsInfinity(a)){ return integrate(delegate(double x){return f(x/(1-x*x))*(1+x*x)/(1-x*x)/(1-x*x);},-1,1,acc,eps);
			} else if (double.IsInfinity(b)) { return integrate(delegate(double x){return f(a+x/(1-x))/(1-x)/(1-x);},0,1,acc,eps);
			} else if (double.IsInfinity(a)) { return integrate(delegate(double x){return f(b+x/(1+x))/(1+x)/(1+x);},-1,0,acc,eps);}
			
			double h=b-a;
			if(double.IsNaN(f2)){ f2=f(a+2*h/6); f3=f(a+4*h/6); count+=2; } // first call, no points to reuse
			double f1=f(a+h/6), f4=f(a+5*h/6); count+=2;
			double Q = (2*f1+f2+f3+2*f4)/6*(b-a); // higher order rule
			double q = (  f1+f2+f3+  f4)/4*(b-a); // lower order rule
			double err = Abs(Q-q);
			
			//WriteLine($"{err} {Q} {q} {f1} {f2} {f3} {f4}");
			if (err <= acc+eps*Abs(Q)) return (Q,err,count);
			else {
			double err2;
			int c1,c2;
			(Q,err,c1) = integrate(f,a,(a+b)/2,acc/Sqrt(2),eps,f1,f2,count);
			(q,err2,c2) = integrate(f,(a+b)/2,b,acc/Sqrt(2),eps,f3,f4,count);
			
			
			return (Q+q,Sqrt(Pow(err,2)+Pow(err2,2)),c1+c2);
			}
		}
	
	public static (double,double,int) clenshawIntegrate
		(Func<double,double> f, double a, double b,
		 double acc=0.001, double eps=0.001, double f2=double.NaN, double f3=double.NaN)
		{
			       	return integrate(delegate(double x){ return f((a+b)/2+(b-a)*Cos(x)/2)*Sin(x)*(b-a)/2;},0, PI);
		}
}
