using System;
using static System.Console;
using static System.Math;
public static class math {
	public static double erf (double z, double acc = 1e-9, double eps = 1e-9){
		if (z<0) return -erf(-z);
		if (z<=1) return 2/Sqrt(PI)*NumInt.integrate(delegate(double x){return Exp(-x*x);},0,z,acc,eps).Item1;
		if (1<z) return 1-2/Sqrt(PI)*NumInt.integrate(delegate(double x) {return Exp(-Pow(z+(1-x)/x,2))/x/x;},0,1,acc,eps).Item1;
		return double.NaN;

	}
	public static double erf_singlePE(double x){
	 /// single precision error function (Abramowitz and Stegun, from Wikipedia)
		 if(x<0) return -erf(-x);
	 		double[] a={0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
	 		double t=1/(1+0.3275911*x);
	 		double sum=t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4]))));/* the right thing */
	 		return 1-sum*Exp(-x*x);
	 }

	public static void Main(){
		double[] xs ={0, 0.02,0.04,0.06,0.08,0.1,0.2,0.3,0.4,0.5,0.6,0.7,0.8,0.9,1,1.1,1.2,1.3,1.4,1.5,1.6,1.7,1.8,1.9,2,2.1,2.2,2.3,2.4,2.5,3,3.5};
		double[] TabErr = {0,0.022564575,0.045111106,0.067621594,0.090078126,0.112462916,0.222702589,0.328626759,0.428392355,0.520499878,0.603856091,0.677801194,0.742100965,0.796908212,0.842700793,0.880205070,0.910313978,0.934007945,0.952285120,0.966105146,0.976348383,0.983790459,0.989090502,0.992790429,0.995322265,0.997020533,0.998137154,0.998856823,0.999311486,0.999593048,0.999977910,0.999999257}; //Tabulated error function points
		for (int i = 0; i<xs.Length; i++){
			WriteLine($"{xs[i]} {Abs(TabErr[i]-erf_singlePE(xs[i]))} {Abs(TabErr[i]-erf(xs[i]))}");
		}
	}
}
