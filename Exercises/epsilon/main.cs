using System;
using static System.Console;
using static System.Math;
public static class math {
	public static bool approx(double a, double b, double acc=1e-9,double eps = 1e-9){
		if (Abs(b-a) < acc) return true;
		else if(Abs(b-a) < Max(Abs(a),Abs(b))*eps) return true;
		else return false;
	}

	public static void Main(){
	//1):
	int i =1;
	while(i+1>i) {i++;}
	Write("1):\n");
	Write("my max int = {0}\n",i);
	Write("int.MaxValue = {0}\n",int.MaxValue);
	i = 0; //This should make it go a bit faster
	while(i-1<i){i--;}
	Write("my min int value = {0}\n", i);
	Write("int.MinValue = {0}\n",int.MinValue);
	//2):
	double x =1;
	while (1+x != 1){x/=2;}
	x*=2;
	float y = 1F;
	while((float)(1F+y) != 1F){y/=2F;} 
	y*=2F;
	Write("Double machine epsilon = {0} \n Theoretical Double epsilon = {1} \n",x,Pow(2,-52));
	Write("Float machine epsilon = {0} \n Theoretical Float epsilon = {1} \n",y,Pow(2,-23));
	//3):
	int n=(int)1e6;
	double epsilon = Pow(2,-52);
	double tiny = epsilon/2;
	double sumA=0,sumB=0;
	sumA+=1;
	for(int d=0;d<n;d++){sumA+=tiny;}
	for(int d=0;d<n;d++){sumB+=tiny;}
	sumB+=1;
	WriteLine($"sumA-1 = {sumA-1:e} should be {n*tiny:e}");
	WriteLine($"sumB-1 = {sumB-1:e} should be {n*tiny:e}");
	WriteLine("This happens due to differences in exponens; epsilon is the smallest value we can add to 1 to get something else, so in the first loop (sumA) we effectivly add 0 to 1 n times, which gives one \n. In the second loop, C# can instead switch expontial, such that epsilon/2 isn't just rounded to 0 anymore. By adding the tiny parts up first, they become something we can add to one without it getting rounded to zero.");	
	//4):
	double d1 = 0.1+0.1+0.1+0.1+0.1+0.1+0.1+0.1;
	double d2 = 0.1*8;
	WriteLine($"d1 = {d1:e15}");
	WriteLine($"d2 = {d2:e15}");
	WriteLine($"d1==d2 ? => {d1 == d2}");
	WriteLine($"With approx: d1==d2 (approx) ? => {approx(d1,d2)}");
	}
}
