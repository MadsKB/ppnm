
using static System.Math;
public static partial class sfunc {
	public static double gamma(double x){
		return Exp(lngamma(x));
	}
	public static double lngamma(double x){
		//single precision gamma function (Gergo Nemes, from Wikipedia)
		if(x<0)return Log(PI)-Log(Sin(PI*x))-lngamma(1-x);
		if(x<9)return lngamma(x+1)-Log(x);
		double Lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
		return Lngamma; //Part of the plot isn't there for some reason
		}
}//class

