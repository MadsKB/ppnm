using System;
using System.Threading; //As threading creates new objects/threads, this isn't static
using static System.Console;
using static System.Math;
class math {
	public class data{ public int a,b; public double sumab;}
	public static void harm(object obj){ //Reference to an object given
	data x = (data)obj; //We interprit the passed object reference as a data object; claimed to be safe, but prof don't know
	x.sumab = 0;
	WriteLine($"({Thread.CurrentThread.Name} a = {x.a}, b = {x.b})");
	for (int i = x.a;i<x.b;i++)x.sumab+=1.0/i; //Calculate harmonic sum; no need to return anything, much like main it self (Stuff here is fetched into registers; or should be)
	WriteLine($"({Thread.CurrentThread.Name} partial sum = {x.sumab})");
	}

	public static int Main(string[] args){
		int nterms = (int)1e8,nthreads = 1;
		foreach(var arg in args){ //var, let compiler figure out value type
			var words = arg.Split(":");
			if (words[0] =="-terms") nterms = (int)double.Parse(words[1]);
			if (words[0] =="-threads") nthreads = (int)double.Parse(words[1]);
		}
		WriteLine($"Calculating with {nterms} terms, and {nthreads} threads");
		data [] x = new data[nthreads];
		for(int i = 0; i<nthreads;i++){
		x[i] = new data(); //Initiallize the data, doesn't do this automaticlly in
		x[i].a = 1+ nterms/nthreads*i; //Not totally precise in starting location, but that's fine
		x[i].b = 1+ nterms/nthreads*(i+1);
		WriteLine($"i = {i} x.a={x[i].a} x.b = {x[i].b}");
		}
		Thread[] threads = new Thread[nthreads];
		for(int i = 0;i<nthreads;i++) { //this could be done in the same loop
			threads[i] = new Thread(harm);
			threads[i].Name = $"thread number {i+1}";
			threads[i].Start(x[i]); //Possix system: You ask the operating system to run this on a seperatat processor; 
						//For our systems, we ask a virtual machine to maybe run this on a seperate processor,
						// and it may or may not do that.

		}
		//Doesn't hurt to have threads doing nothing when they are done; Operating system will assign them new tasks
		WriteLine("master thread: Now waiting for other threads...");
		for(int i = 0;i<nthreads;i++) {
			threads[i].Join();
		}
		double total = 0;
		for(int i = 0; i < nthreads;i++){
			total +=x[i].sumab;
		}
		WriteLine($"Result: {total}");
		return 0;

	}
}
