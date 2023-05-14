using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(){

		//Part B):
		vector x = new vector(5);
		vector y = new vector(5);
		for (int i = 0; i<x.size;i++){
			x[i] = i+1;
			y[i] = 1;
		}
		vector b = new vector(x.size-1);
		vector c = new vector(x.size-1);
		WriteLine("Part B):");
		WriteLine("y = 1; analytical result: b[i] = c[i] = 0");
		WriteLine("i b[i] c[i]");
		spline.qspline_build(x,y,b,c);
		for (int i = 0; i<b.size;i++) WriteLine($"{i} {b[i]} {c[i]}");
		WriteLine($"\n");
		WriteLine("y=x; analytical result: b[i] = 1, c[i] = 0");
		WriteLine("i b[i] c[i]");
		for (int i = 0;i<x.size;i++){
			y[i] = x[i];
		}
		spline.qspline_build(x,y,b,c);
		for (int i = 0;i<b.size;i++) WriteLine($"{i} {b[i]} {c[i]}");
		
		WriteLine($"\n");	
		WriteLine("y=x^2; analytical result: b[i] = 2*x[i], c[i] = 1");
		WriteLine("i x[i] b[i] c[i]");
		for (int i = 0; i<x.size;i++) y[i] = x[i]*x[i];
		spline.qspline_build(x,y,b,c);
		for (int i = 0;i<b.size;i++) WriteLine($"{i} {x[i]} {b[i]} {c[i]}");
		WriteLine("\nPart C):");
		var correct = new vector(3); correct[0] = -1.0/37; correct[1] = 14.0/37;correct[2] = 13.0/37;
		var e = new vector(3);
		var B = new vector(3); B[0] = 1; B[1] = 2; B[2] = 3;
		var A = new vector(2); A[0] =8; A[1] = 7;
		var D = new vector(3); D[0] = 5; D[1] = 4; D[2] = 1;
		var Q = new vector(2); Q[0] = 3; Q[1] = 2;
		var K = new matrix(D);
		for (int i = 0; i<D.size-1;i++){
			K[i+1,i] = A[i];
			K[i,i+1] = Q[i];
		}
		WriteLine("Solving K*x=B, with K:");
		K.print();
		WriteLine("And B:");
		B.print();
		WriteLine("Solution from wolframalpha is:");
		correct.print();
		spline.solveTriDiaSys(e,B,A,D,Q);
		WriteLine("Our algorithem's solution:");
		e.print();



	}
}
