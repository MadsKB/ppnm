using System;
using static System.Math;

public static class sfuns {
	public static double lngamma(double x){
		//Single precision gamma function (formula from wikipedia)
		if (x<0) return Log(PI)-Log(Sin(PI*x))-lngamma(1-x); //Euler's reflection formula
		if (x<9) return lngamma(x+1)-Log(x); //Recurrece relation
		double Out = x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
		return Out; //lngamma
	}
	public static double gamma(double x){
		return Exp(lngamma(x));
	}
}
