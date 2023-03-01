using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(){
		for(double x = -5+1.0/128;x<=5;x+=1.0/128){
			WriteLine($"{x} {sfunc.erf(x)}");
		}
	}
}
