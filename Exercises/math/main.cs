using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(){
		double sqrt2 = Sqrt(2.0);
		double TwoPow1div5 = Pow(2,1.0/5);
		double expPi = Exp(PI);
		double piPowE = Pow(PI,E);

		Write($"sqrt2={sqrt2}\n");
		Write($"2^(1/5)={TwoPow1div5}\n");
		Write($"e^pi={expPi}\n");
		Write($"pi^e={piPowE}\n \n");
		
		Write("Some Tests\n");
		
		Write($" sqrt2*sqrt2 = {sqrt2*sqrt2} (Should be 2)\n");
		Write($" (2^(1/5))^(5) = {Pow(TwoPow1div5,5)} (should be 2)\n");
		Write($"Log(e^pi)={Log(expPi)} (should be pi)\n");
		Write($"(pi^e)^(1/e)={Pow(piPowE,1/E)} (should be pi)\n \n");
		WriteLine("Some values of gamma: \n");
		double[] xs = new double[4]{1.0,2.0,3.0,10.0};
		for (int i = 0;i<4;i++){
			Write($"gamma({xs[i]}) = {sfuns.gamma(xs[i])}\n");
		}
		WriteLine(" \n Some values values of lngamma: \n");
		for (int i = 0;i<4;i++){
			Write($"lngamma({xs[i]}) = {sfuns.lngamma(xs[i])}\n");
		}


	}
}
