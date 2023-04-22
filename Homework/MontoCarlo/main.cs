using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(){
		var a = new vector(2);
		var b = new vector(2); b[0]=PI/2; b[1] = PI/2;
		int N = 10000000;
		WriteLine($"Theoretical: 1, numerical: {montoCarlo.plainmc(delegate(vector x){return Cos(x[0])*Sin(x[1]);},a,b,N)}");
		/*a = new vector(3);
		b = new vector(3); for (int i = 0; i<b.size;i++) b[i] = PI;
		WriteLine($"'Fun' intergral: 1.3932039296856768591842462603255, numerical: {montoCarlo.plainmc(delegate(vector x){return 1/(1-Cos(x[0])*Cos(x[1])*Cos(x[2]))/PI/PI/PI;},a,b,N*10)}");
		*/
		WriteLine($"Theoretical: 1, numerical quasi: {montoCarlo.quasimc(delegate(vector x){return Cos(x[0])*Sin(x[1]);},a,b,N)}");
	}
}
