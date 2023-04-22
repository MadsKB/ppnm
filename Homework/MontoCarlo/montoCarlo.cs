using System;
using static System.Console;
using static System.Math;
public static class montoCarlo {
	public static double corput(int n, int b){
		double q=0; double bk = (double)1/b;
		while(n>0){q+=(n%b)*bk;n/=b;bk/=b;}
		return q;
	}
	public static vector haltonV(int n, vector x,vector a, vector b){
		int[] Base = new int[] {2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61,67};
		if (!(x.size<=Base.Length)) throw new ArgumentException("montocarlo Halton: Given vector dimension exceeds base array size");
		for(int i=0;i<x.size;i++) x[i] = a[i]+corput(n,Base[i])*(b[i]-a[i]);
		return x;
	}
	public static vector randomV(vector x, vector a, vector b,Random random){
		for(int k=0;k<x.size;k++)x[k] = a[k]+random.NextDouble()*(b[k]-a[k]);
		return x;
	}

	public static vector constructLaticeAlpha (int dim, double Base = PI){
		var Out = new vector(dim);
		for (int i=0;i<dim;i++) Out[i] = Sqrt(Base+i)%1;
		return Out;
	}

	public static vector latriceV(int d, vector x, vector alpha, vector a, vector b){
		for (int i = 0;i<x.size;i++) x[i] = a[i]+(b[i]-a[i])*((alpha[i]*d)%1);
		return x; //Don't really need to return it
	}
	public static (double,double) plainmc (Func<vector,double> f,vector a, vector b, int N){
		int dim = a.size; double V=1; for(int i = 0;i<dim;i++)V*=b[i]-a[i];
		double sum=0,sum2=0;
		var x=new vector(dim);
		var rng = new Random(1451);
		for (int i=0;i<N;i++) {
			x = randomV(x,a,b,rng);
			double fx=f(x);sum+=fx;sum2+=fx*fx;
		}
		
		double mean =sum/N,sigma=Sqrt(sum2/N-mean*mean);
		var result=(mean*V,sigma*V/Sqrt(N));
		return result;
	}

	public static (double,double) quasimc (Func<vector,double> f,vector a, vector b, int N){
		int dim = a.size; double V=1; for(int i = 0;i<dim;i++)V*=b[i]-a[i];
		double sum=0,sum2=0;
		var xh=new vector(dim);
		var xl = new vector(dim);
		var alpha = constructLaticeAlpha(dim);
		for (int i=0;i<N;i++) {
			xh = haltonV(i,xh,a,b);
			xl = latriceV(i,xl,alpha,a,b);
			sum+=f(xh);sum2+=f(xl);
		}
		double mean =sum/N,sigma=Abs(sum-sum2)/N;
		var result=(mean*V,sigma*V);
		return result;
	}
}
