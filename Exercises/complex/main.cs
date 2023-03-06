using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(){
	
	WriteLine($"sqrt(-1) = {cmath.sqrt(-complex.One)} (should be +-i)");
	WriteLine($"ln(i) = {cmath.log( complex.I)} (should be i*pi/2)");
	WriteLine($"sqrt(i)={cmath.sqrt(complex.I)} (should be 1/sqrt(2)+i/sqrt(2), with 1/sqrt(2) = 0.707, approx");
	WriteLine($"i^i = {cmath.pow(complex.I,complex.I)} (Should be about 0.208)");
	WriteLine($"sinh(1)+i*sin(i) = {cmath.sinh(complex.One)+complex.I*cmath.sin(complex.I)} (Should equal 0, pr an identity)");
	WriteLine($"cosh(1)-cos(i)={cmath.cosh(complex.One)-cmath.cos(complex.I)} (Should equal 0, pr an identity)");
	}
}
