using System;
using static System.Console;
using static System.Math;
public static class ODE {
	public static (vector,vector) rkstep12( 
			Func<double,vector,vector> f,
			double x,
			vector y,
			double h
			) {//Might choose a better alogithem later
		vector k0 = f(x,y);
		vector k1 = f(x+h/2,y+k0*(h/2));
		vector yh = y+k1*h;
		vector er = (k1-k0)*h;
		return (yh,er);
	}
	public static (vector,vector) driver(
			Func<double,vector,vector> f,
			double a,
			vector ya,
			double b,
			double h = 0.01,
			double acc = 0.01,
			double eps = 0.01,
			genlist<double> xlist = null,
			genlist<vector> ylist = null
			){
		if (a>b) throw new ArgumentException("driver: a>b");
		double x=a; vector y=ya.copy();
		vector err = y*0; //Assuming the first has no error
		bool listsProvided = false;
		if (xlist != null && ylist != null){
			listsProvided = true;
			xlist.add(x);
			ylist.add(y);
		}
		vector tol = new vector(y.size);
		do	{
			if(x>=b) return (y,err);
			if (x+h>b) h=b-x;
			
			var (yh,erv) = rkstep12(f,x,y,h);
			bool ok = true;
			for (int i=0; i<y.size;i++){
				tol[i] = Max(acc,Abs(yh[i])*eps)*Sqrt(h/(b-a));
				if (!(erv[i]<tol[i])) ok=false;
			}
			
			//WriteLine($"{x} {h} {ok}");
			if (ok){ //accept step
				x+=h; y=yh; err = erv;
				if (listsProvided){
					xlist.add(x);
					ylist.add(y);
				}
			}
			double factor = tol[0]/Abs(erv[0]);
			for(int i=1;i<y.size; i++) factor = Min(factor, tol[i]/Abs(erv[i]));
			h*=Min(Pow(factor,0.25)*0.95,2); //reajust stepsize

		} while(true);
	
	}

}
