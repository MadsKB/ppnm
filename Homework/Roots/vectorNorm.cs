using System;
using static System.Console;
using static System.Math;
public partial class vector {
	public double Mag(){
		return Sqrt(this%this);
	}
}
