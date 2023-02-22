using System;
using static System.Console;
using static System.Math;
class main{
	public static int Main(string[] args) {
	Write("Hello \n");
	string infile = null, outfile = null;
	foreach(string arg in args){
		System.Console.Out.WriteLine(arg);
		var words = arg.Split(":");
		if (words[0] == "-input") infile = words[1];
		if (words[0] == "-output") outfile = words[1];
	}
	if (infile == null){Error.WriteLine("no input file");return 1;}
	double[] numbers = input.get_numbers_from_args(args);
	foreach(double number in numbers)System.Console.Out.WriteLine($"{number:0.00e+00}");
	System.Console.Error.WriteLine("return code 0 (I'm a bug)");
	var inputStream = new System.IO.StreamReader(infile);
	var outputStream = new System.IO.StreamWriter(outfile,append:false);

	for(string line =inputStream.ReadLine();line != null;line = inputStream.ReadLine()){
		double x = double.Parse(line);
		outputStream.WriteLine($"{x} {Sin(x)} {Cos(x)}");
	}
	inputStream.Close();
	outputStream.Close();

	for(string line =In.ReadLine();line != null;line = In.ReadLine()){
		double x = double.Parse(line);
		Out.WriteLine($"{x} {Sin(x)} {Cos(x)}");
	}
	return 0;
	}
}
