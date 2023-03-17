using System;
using static System.Math;
public static partial class sfunc {
	public static double gamma(double x){
		if(x<0)return PI/Sin(PI*x)/gamma(1-x);
		if(x<9)return gamma(x+1)/x;
		double Lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
		return Exp(Lngamma);
	}
	public static double gamma2(double x){
		return Exp(lngamma(x));
	}
	//This function can't handle negative values of gamma.
	public static double lngamma(double x){
		//single precision gamma function (Gergo Nemes, from Wikipedia)
		if(x<=0) return Log(PI)-Log(Sin(PI*x))-lngamma(1-x);
		//throw new ArgumentException("lngamma: x<=0");
		if(x<9)return lngamma(x+1)-Log(x);
		double Lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
		return Lngamma; 
		}
}//class

