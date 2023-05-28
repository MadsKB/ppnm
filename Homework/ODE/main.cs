using System;
using static System.Console;
using static System.Math;
public static class math {
	public static void Main(){
		var xs = new genlist<double>();
		var ys = new genlist<vector>();
		
		
		//Part A
		Func<double,vector,vector> f = delegate(double x, vector y) {
			var Out = y.copy(); //y goes (y,y'), and Out like (f',f'')
			Out[0] = y[1];
			Out[1] = -y[0];
			return Out;
		};
		double x0 = 0;
		double xEnd = 10;
		vector y0 = new vector(new [] {0.0,1.0});
		
		ODE.driver(f,x0,y0,xEnd,0.01,0.01,0.01,xs, ys);
		for (int i = 0; i<xs.size;i++){
			WriteLine($"{xs[i]} {ys[i][0]} {ys[i][1]}");
		}
		WriteLine("\n");
		double b = 0.25; //Dampning constant
		double c = 5.0;
		y0 = new vector(new [] {PI-0.1,0.0});
		f = delegate(double x, vector y) {
			var Out = y.copy(); //y goes (theta,theta'), and Out like (omega,omgega')
			Out[0] = y[1];
			Out[1] = -b*y[1]-c*Sin(y[0]);
			return Out;
		};
		
		xs = new genlist<double>();
		ys = new genlist<vector>();
		ODE.driver(f,x0,y0,xEnd,0.01,0.01,0.01,xs, ys);
		for (int i = 0; i<xs.size;i++){
			WriteLine($"{xs[i]} {ys[i][0]} {ys[i][1]}");
		}

		WriteLine($"\n");
		//Part B
		double eps = 0;
		Func<double,vector,vector> g = delegate(double x, vector y) {
			var Out = y.copy();
			Out[0] = y[1];
			Out[1] = 1-y[0]+eps*y[0]*y[0];
			return Out;
		};
		//i
		xs = new genlist<double>();
		ys = new genlist<vector>();
		x0 = 0; xEnd = 10*PI;
		int numPoints = 500;
		y0[0] = 1; y0[1] = 0;
		for (int i = 0; i<numPoints;i++){
			var (y,err) = ODE.driver(g,x0,y0,(i+1)*xEnd/numPoints,0.00001,0.001,0.001);
			WriteLine($"{(i+1)*xEnd/numPoints} {y[0]} {y[1]}");
		}
		WriteLine("\n");
		//ii

		y0[0] = 1; y0[1] = -0.5;

		for (int i = 0; i<numPoints;i++){
			var (y,err) = ODE.driver(g,x0,y0,(i+1)*xEnd/numPoints,0.00001,0.001,0.001);
			WriteLine($"{(i+1)*xEnd/numPoints} {y[0]} {y[1]}");
		}

		WriteLine("\n");
		//iii
		eps = 0.02;	
		for (int i = 0; i<numPoints;i++){
			var (y,err) = ODE.driver(g,x0,y0,(i+1)*xEnd/numPoints,0.00001,0.001,0.001);
			WriteLine($"{(i+1)*xEnd/numPoints} {y[0]} {y[1]}");
		}
		//Part C):
		WriteLine("\n");
		var m = new vector(new [] {1.0,1.0,1.0});
		double G = 1;
		//Starting conditions from paper
		y0 = new vector(new [] {0.97000436,-0.24308753,0.93240737/2,0.86473146/2,-0.97000436,0.24308753,0.93240737/2,0.86473146/2,0.0,0.0,-0.93240737,-0.86473146});
		Func<double,vector,vector> Grav = delegate(double x, vector y) {
			var Out = y.copy(); //Structure of y goes (..., x_i,y_i,x'_i,y'_i, ...)
			for (int i = 0; i<m.size; i++){
				Out[4*i] = y[4*i+2];
				Out[4*i+1] = y[4*i+3];
				Out[4*i+2] = 0; Out[4*i+3] = 0;
				for (int j = 0; j<m.size;j++){
					if (j != i) {
						double R3 = Pow(Sqrt(Pow(y[4*i]-y[4*j],2)+Pow(y[4*i+1]-y[4*j+1],2)),3);

						Out[4*i+2] +=G*m[j]/R3*(y[4*j]-y[4*i]);
						Out[4*i+3] +=G*m[j]/R3*(y[4*j+1]-y[4*i+1]);
					}
				}
			}
			return Out;
		};
		xEnd = 7;
		for (int i = 0; i<numPoints;i++){ 
			var (y,err) = ODE.driver(Grav,x0,y0,(i+1)*xEnd/numPoints,0.00001,0.001,0.001);
			Write($"{(i+1)*xEnd/numPoints} ");
			for (int d = 0; d<y.size;d++) Write($"{y[d]} ");
			Write("\n");
		}






	}
}
