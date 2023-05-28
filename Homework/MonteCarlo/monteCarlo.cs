using System;
using static System.Console;
using static System.Math;
public static class monteCarlo {
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
	public static (double,double) plainmc (Func<vector,double> f,vector a, vector b, int N, int seed = 1451,Random rng = null){
		//if (N == 0) return (0.0,0.0);
		int dim = a.size; double V=1; for(int i = 0;i<dim;i++)V*=b[i]-a[i];
		double sum=0,sum2=0;
		var x=new vector(dim);
		if (rng == null) 
		{
		rng = new Random(seed);
		}
		for (int i=0;i<N;i++) {
			x = randomV(x,a,b,rng);
			//x.print();
			double fx=f(x);sum+=fx;sum2+=fx*fx;
		}
		
		double mean =sum/N,sigma=Sqrt(sum2/N-mean*mean);
		var result=(mean*V,sigma*V/Sqrt(N));
		return result;
	}
	public static (double,double) plainmc_stratified(Func<vector,double> f, vector a, vector b, int N, int nmin = 100, int seed = 1451,double minSplit = 1e-5, Random rng = null ) {
		//Check if we have enough points
		if (N<=nmin){
			return plainmc(f,a,b,N,seed,rng);
		}
		//Begin sampling
		int dim = a.size; double V =1; for (int i = 0; i<dim;i++)V*=b[i]-a[i];
		double [] splits = new double[dim]; for (int i = 0; i<dim;i++) splits[i] = (b[i]-a[i])/2+a[i];
		double [] sum=new double[2*dim]; double [] sum2= new double[2*dim];
		int [] num = new int[2*dim];
		var x = new vector(dim);
		if (rng == null)
		{
			rng = new Random(seed);
		}
		for (int i=0;i<nmin;i++){
			x = randomV(x,a,b,rng);
			//x.print();
			//Check where the point belongs
			for (int d = 0; d<dim; d++) {
				int check = 0;
				if (x[d]<=splits[d]) {check = 0;} else {check =1;}
				sum[2*d+check] += f(x);sum2[2*d+check]+=f(x)*f(x); num[2*d+check] +=1;
			}
		}
		N-=nmin;
		//Check which dimension we should split along
		double biggestDiff = Abs(sum[0]/num[0]-sum[1]/num[1]); int bestSplit = 0;
		for (int d = 0; d<dim; d++) {
			double meanDiff = Abs(sum[2*d]/num[2*d]-sum[2*d+1]/num[2*d+1]);
			if (biggestDiff<meanDiff) {
				biggestDiff = meanDiff;
				bestSplit = d;
			}
		
		}
		//Split and call recursivly on both new halfs
		var newA = a.copy(); var newB = b.copy();
		
		newA[bestSplit] = splits[bestSplit]; newB[bestSplit] = splits[bestSplit];
		double VarA = Sqrt(sum2[2*bestSplit]/num[2*bestSplit] - Pow(sum[2*bestSplit]/num[2*bestSplit],2));
		double VarB = Sqrt(sum2[2*bestSplit+1]/num[2*bestSplit+1] - Pow(sum[2*bestSplit+1]/num[2*bestSplit+1],2));
		int Na = (int)Ceiling(N*VarA/(VarA+VarB)); int Nb = (int)Ceiling(N*VarB/(VarA+VarB));
		var resultA = (0.0,0.0); var resultB = (0.0,0.0);
		if (Na == 0) 
		{	
			resultA = (sum[2*bestSplit]/num[2*bestSplit]*V/2,VarA*V/Sqrt(num[2*bestSplit])/2);
			resultB = plainmc_stratified(f,newA,b,Nb,nmin,seed,minSplit,rng);
			//WriteLine($"Na == 0,N = {N}, resA = ({resultA.Item1} , {resultA.Item2}), resB = ({resultA.Item1} , {resultA.Item2})");
			
		} else if (Nb == 0)
		{
			resultA = plainmc_stratified(f,a,newB,Na,nmin,seed,minSplit,rng);
			resultB = (sum[2*bestSplit+1]/num[2*bestSplit+1]*V/2,VarB*V/Sqrt(num[2*bestSplit+1])/2);
			//WriteLine($"Nb == 0,N = {N} ,resA = ({resultA.Item1} , {resultA.Item2}), resB = ({resultA.Item1} , {resultA.Item2})");
				
		} else if (Na < 0 || Nb <0) //Case where both variances are zero, or at least very small; we split it half and half
		{
			
			resultA = plainmc_stratified(f,a,newB,N/2,nmin,seed,minSplit,rng);
			resultB = plainmc_stratified(f,newA,b,N/2,nmin,seed,minSplit,rng);
			//WriteLine($"Na < 0 or Nb <0, N = {N}, resA = ({resultA.Item1} , {resultA.Item2}), resB = ({resultA.Item1} , {resultA.Item2})");
		} else 
		{
			if (Na<=0 || Nb <= 0){ 
				//WriteLine($"N = {N}, Na = {Na}, Nb = {Nb}, VarA = {VarA}, VarB = {VarB}, sum2A = {sum2[2*bestSplit]}, numA = {num[2*bestSplit]}");
				throw new ArgumentException("It's zero");
			}
			resultA = plainmc_stratified(f,a,newB,Na,nmin,seed,minSplit,rng);
			resultB = plainmc_stratified(f,newA,b,Nb,nmin,seed,minSplit,rng);
		}
		return (resultA.Item1+resultB.Item1,Sqrt(resultA.Item2*resultA.Item2+resultB.Item2*resultB.Item2));

	
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
