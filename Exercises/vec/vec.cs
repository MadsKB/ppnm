using static System.Console;
public class vec{
	public double x,y,z;
	//constructors
	public vec (double a, double b, double c){x=a;y=b;z=c;}
	public vec() {x=y=z=0;}
	//operators
	public static vec operator*(vec v, double c){return new vec(c*v.x,c*v.y,c*v.z);}
	public static vec operator*(double c, vec v){return v*c;}

	public static vec operator+(vec v,vec u){return new vec(u.x+v.x,u.y+v.y,u.z+v.z);}
	public static vec operator-(vec v){return new vec(-v.x,-v.y,-v.z);}
	public static vec operator-(vec v,vec u) {return new vec(v.x-u.x,v.y-u.y,v.z-u.z);}
	public static double operator%(vec v,vec u) {return dot(v,u);} //dot product
	//functions
	public void print(string s) {Write(s); WriteLine($"{x} {y} {z}");}
	public double dot(vec other) => this.x*other.x+this.y*other.y+this.z*other.z;
	public static double dot(vec v, vec w) => v.x*w.x + v.y*w.y + v.z*w.z;
	


	//override
	public override string ToString(){return $"{x} {y} {z}";}
}	
