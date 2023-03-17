using System;
using System.Threading; //As threading creates new objects/threads, this isn't static
using System.Threading.Tasks;
using static System.Console;
using static System.Math;
class math {
	

	public static int Main(string[] args){
		int nterms = (int)1e8;
		foreach(var arg in args){ //var, let compiler figure out value type
			var words = arg.Split(":");
			if (words[0] =="-terms") nterms = (int)double.Parse(words[1]);
		}
		WriteLine($"nterms = {nterms}");
		double sum = 0;
		//for (int i =1; i<nterms+1;i++){sum+=1.0/i;}
		Parallel.For(1,nterms+1,delegate(int i) {sum += 1.0/i;}); //This causes a race condition; causing slow downs as only one processor can access the ram at the same time, and order 
									  //is also screwed up, which causes different results due to rounding.
									  //Calling delegate here, instead of previously is also really slow as it has to "make" the fuction, in addition to preparing it and such
									  //(I think)
									  //(Just not worth for one addition operation)
									  //("Paralle is like voodo" demetri qoute), generally don't use parallation in code, just make seperate targets for it 
									  //(There are exceptions)
		
		WriteLine($"Result: {sum}");
		return 0;

	}
}
