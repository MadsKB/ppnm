using System;
using static System.Console;
using static System.Math;
class main{
	static string s = "class scope s";
	public static void print_s(){WriteLine(s);}
	public static void Main(){
		string s = "method scope s";
		print_s();
		WriteLine(s);
		{
			string ss = "Block scope";
			WriteLine(ss);
		}
	Write("hello from main\n");
	static_hello.print();
	static_world.print();
	static_hello.greeting = "New hello from main \n";
	static_world.greeting = "new world from main \n";
	hello hello1 = new hello("hello1");
	hello hello2 = new hello("world1");
	hello1.print();
	hello2.print();

	Write($"The value of pi in math is {PI}\n");
	Write("the value of pi in math is {0}\n",PI);
	Write($"the value of e in Math is {E}\n");
	double sqrt2 = Sqrt(2.0);

	Write($"sqrt2^2 =  {sqrt2*sqrt2}\n");
	
	Write($"1/2 =  {1/2}\n");

	Write($"1/2 =  {1/2}\n");
	Write($"1/2 =  {1.0/2}\n");
	}
}
