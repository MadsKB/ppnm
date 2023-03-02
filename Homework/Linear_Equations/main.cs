using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(){
		//A 4 by 5 matrix (n=5, m = 4)
		matrix A {"0,1,2,3;4,5,6,7;8,9,10,11;12,13,14,15;16,17,18,19"};
		QRGS decomp;
		decomp.QRGS(A);
		Writeline(decomp.R);
	}
}
