using System;
using static System.Console;
using static System.Math;
public class ann {
	public int n; //Hidden neurons
	public int itterations; //How many itterations of training we've had
	public Func<double,double> f; //Activation func.
	public Func<double,double> fPrime; //Prime of Activation func.
	public Func<double,double> fDPrime; //Double Derivitive of activation func
	public Func<double,double> fInt; //Integral of Activation func.
	public vector p; //Network parms.
	public ann(int n, Func<double,double> f,Func<double,double> fPrime = null, Func<double,double> fInt = null, Func<double,double> fDPrime = null ){ //Constructor
		//Structure of p is: 
		this.n = n;
		this.f = f;
		this.fPrime = fPrime;
		this.fInt = fInt;
		this.p = new vector(3*n);//Possibly set this as a random vector
		this.itterations = 0; 
	}
	public double trainResponse(double x,vector p){
		double Out = 0;
		//WriteLine($"trainReponse: x = {x}, p is");
		//p.print();
		for (int i = 0;i<n;i++) Out+= this.f((x-p[3*i+2])/p[3*i+1])*p[3*i];
		return Out;
	}
	public double response(double x){
		double Out = 0;
		for (int i = 0; i<n; i++) Out+= this.f((x-getShift(i))/getScale(i))*getWeight(i);
		return Out;
	}
	public double responsePrime(double x){
		double Out = 0;
		if (fPrime == null) throw new ArgumentException("ANN: You need to supply derivitive of activation function");
		for (int i = 0; i<n; i++) Out+= this.fPrime((x-getShift(i))/getScale(i))*getWeight(i)/getScale(i);
		return Out;
	}
	public double responseDPrime(double x){
		double Out = 0;
		if (fDPrime == null) throw new ArgumentException("ANN: You need to supply double derivitive of activation function");
		for (int i = 0; i<n; i++) Out+= this.fDPrime((x-getShift(i))/getScale(i))*getWeight(i)/getScale(i)/getScale(i);
		return Out;
	}

	public double responseInt(double x){
		double Out = 0;
		if (fInt == null) throw new ArgumentException("ANN: You need to supply anti-derivitive of activation function");
		for (int i = 0; i<n; i++) Out+= this.fInt((x-getShift(i))/getScale(i))*getWeight(i)*getScale(i);
		return Out;
	}
	public void train(vector x,vector y,Func<vector,double> cost = null){
		if (cost == null) {
		//x: input data, y: correct response
			cost = delegate(vector pn) {
				double Out = 0;
				for (int i = 0; i<x.size;i++) Out+=Pow(trainResponse(x[i],pn)-y[i],2);
				//WriteLine($"cost func is {Out}");
				return Out/x.size;
			};
		}
		(p,itterations) = opt.qnewton(cost,p,1e-5,null,this.itterations);
		//this.p.print();
		//WriteLine($"{this.itterations}");
	}
	public double getWeight(int i){ return this.p[3*i];}
	public double getScale(int i) {return this.p[3*i+1];}
	public double getShift(int i) {return this.p[3*i+2];}
	public vector getP(){return this.p;}
	public void setP(vector np){this.p=np;}

	public void setWeight(int i,double A) {this.p[3*i] = A;}
	public void setScale(int i, double A) {this.p[3*i+1] = A;}
	public void setShift(int i,double A) {this.p[3*i+2] = A;}
}
