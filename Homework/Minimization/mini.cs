using System;
using static System.Console;
using static System.Math;
public static class opt {
	public static vector fgradiant(Func<vector,double> f, vector x){
		var dx = x.copy()*0;
		var Out = new vector(x.size);
		for (int i = 0; i<x.size;i++) {
		       	if (x[i] == 0.0) {
				dx[i] = Pow(2,-52);
			}
			else {
				dx[i] = Abs(x[i])*Pow(2,-26);
			};
			Out[i] = (f(x+dx)-f(x))/(dx[i]);
			dx[i] = 0;
		}
		return Out;
	
	}
	public static (vector,int) qnewton(Func<vector,double> f, vector start, double acc, matrix B = null, int steps = 0){
		//WriteLine($"f = {f(start)}, B follows");
		var grad = fgradiant(f,start);
		if (grad.dot(grad)<acc) return (start,steps);
		if (B == null) {
			B = matrix.id(start.size);
		}
		//B.print();
		double epsilon = 1e-6;
		var dx = -B*grad;
		double lambda = 1;
		
		while (true) {
		//WriteLine($"lambda = {lambda}, df = {f(start+dx*lambda)-f(start)}");	
		if (f(start+dx*lambda)<f(start)) {
			start+=dx*lambda;
			var s = lambda*dx;
			var y = fgradiant(f,start+s)-fgradiant(f,start);
			var u = s-B*y;
			var gamma = (u.dot(y))/(2*s.dot(y));
			if (Abs(s.dot(y))>epsilon) {
				var a = (u-gamma*s)/(s.dot(y));
				B.update(a,s); B.update(s,a);
			};
			break;
		};
		lambda /=2.0;
		if (lambda < 1e-12){
			//start.print();
			start+=dx*lambda;
			//start.print();
			B.set_identity();
			break;
			}
		};
		return qnewton(f,start,acc,B,steps+1);

	}
	public static (vector,int) downhill_simplex(Func<vector,double> f, double acc, int Dim, vector[] Simplex = null,double scale = 1,int seed = 1245,int steps = 0){
		//Either use the provided start, or make our own
		//There's something fucky with convergence
		
		vector[] simplex = new vector[Dim+1];
		if (Simplex != null){
			Dim = Simplex.Length-1;
			simplex = new vector[Dim+1]; //Should copy this; do later
			for (int i = 0; i<Dim+1;i++) simplex[i] = Simplex[i];
		} else {
			//WriteLine("Working?");
		var rng = new Random(seed);
			//WriteLine("Working?2");
		for (int i = 0; i<Dim+1; i++){
			var a = new vector(Dim);
			for (int d = 0; d<Dim; d++) a[d] = rng.NextDouble()*scale;
			simplex[i] = a;
			//a.print();
		}

			//WriteLine("Working?3");
		}
		int ihigh = 0;
		int ilow = 0;
		vector prePce = new vector(Dim); //Previous center
		double low = 0;
		double high = 0;
		while (true) {
			steps +=1;
			//Find highest and lowest points of simplex
			high = f(simplex[0]);
			low = f(simplex[0]);
			for (int i = 0; i<Dim+1;i++){
				if (f(simplex[i])>high) {
					high = f(simplex[i]);
					ihigh = i;
				}
				if(f(simplex[i])<low) {
					low = f(simplex[i]);
					ilow = i;
				}
			}
			//Find centroid point
			var pce = new vector(Dim);
			for (int i =0; i<Dim+1;i++){
				if (i != ihigh){
					pce += simplex[i];
				}
			}
			pce /= Dim;
			//Now we try reflection
			var pref = pce.copy();
			pref = pce+(pce-simplex[ihigh]);
			if (f(pref)<low) {
			//Try expansion
			var pex = pce +2*(pce-simplex[ihigh]);
			if (f(pex)<f(pref)){
				//WriteLine("Expand");
				simplex[ihigh] = pex;
			} else {
				//WriteLine("Reflect1");
				simplex[ihigh] = pref;
			}

			} else {
			if (f(pref)<high){
				//WriteLine("Reflect2");
				simplex[ihigh] = pref;
			} else {
				//Try contraction
				var pco = pce +(simplex[ihigh]-pce)/2;
				if (f(pco)<high){
					//WriteLine("Contract");
					simplex[ihigh] = pco;
				} else {
					//Shrink it
					//WriteLine("Shrink");
				for (int i = 0; i<Dim+1;i++){
					if (i != ilow) simplex[i] = (simplex[i]+simplex[ilow])/2;
						}
					}
				}
			}
			//Check for convergence, using standard diviation
			double sum = 0;
			double sum2 = 0;
			for (int i = 0; i<Dim+1;i++){
				sum += f(simplex[i]);
				sum2 += f(simplex[i])*f(simplex[i]);
			}
			var Var = sum2-sum*sum/(Dim+1);
			Var /= Dim+1;
			//WriteLine($"Cur Var = {Var}");
			//WriteLine("Ps");
			/*
			for (int i =0;i<Dim+1;i++){
				simplex[i].print();
			}*/
			//WriteLine($"step: {steps} \n");
			if (Var<acc || (vector.approx(prePce,pce,1e-11,1e-11) && steps !=1.0)) {
			//Return the lowest point, might as well
				
				for (int i = 0; i<Dim+1;i++){
					if(f(simplex[i])<low) {
						low = f(simplex[i]);
						ilow = i;
					}
				}

			pce *=0;
			
			high = 0;
			for (int i = 0; i<Dim+1;i++){
				if(f(simplex[i])>high) {
					high = f(simplex[i]);
					ihigh = i;
				}
			}
			for (int i =0; i<Dim+1;i++){
				if (i != ihigh){
					pce += simplex[i];
				}
			}
				return (pce/Dim,steps);
			}
			prePce = pce;
		}
	}
}
