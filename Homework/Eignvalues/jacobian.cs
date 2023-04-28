using System;
using static System.Math;
using static System.Console;
public static class jacobi{
	//All of this is C like
	public static void timesJ(matrix A, int p,int q, double theta){
		double c = Cos(theta), s=Sin(theta);
		for(int i = 0; i<A.size1;i++){
			double aip=A[i,p], aiq = A[i,q];
			A[i,p] = c*aip-s*aiq;
			A[i,q] = s*aip+c*aiq;
		}	
	}
	public static void Jtimes(matrix A, int p, int q, double theta){
		double c = Cos(theta),s=Sin(theta);
		for(int j = 0; j<A.size1;j++){
			double apj=A[p,j],aqj=A[q,j];
			A[p,j]=c*apj+s*aqj;
			A[q,j]=-s*apj+c*aqj;
		}
	}
	//This is reference based, so A is changed into D (Beaware) (Also this is the unpotimized code)
	public static matrix cyclic(matrix A, double tol = 1e-50){
	int n = A.size1;
	matrix V = new matrix(n,n);
	V.set_identity();
	bool changed;
	do{
		changed = false;
		for(int p=0;p<n-1;p++)
		for(int q=p+1;q<n;q++){
			double apq=A[p,q],app=A[p,p],aqq=A[q,q];
			double theta = 0.5*Atan2(2*apq,aqq-app);
			double c=Cos(theta),s=Sin(theta);
			double new_app=c*c*app-2*s*c*apq+s*s*aqq;
			double new_aqq=s*s*app+2*s*c*apq+c*c*aqq;
			if( Abs(new_app-app)>tol || Abs(new_aqq- aqq)>tol) //do rotation
				{
				changed = true;
				timesJ(A,p,q,theta); //A<-A*J
				Jtimes(A,p,q,-theta); //A<-J^T*A
				timesJ(V,p,q,theta); //V <-V*J
				}
		}
	
	} while(changed);
	return V;
	}
	public static matrix cyclicOpt(matrix A, double tol = 1e-5){
	
	int n = A.size1;
	matrix V = new matrix(n,n);
	V.set_identity();
	bool changed;
	do{
		changed = false;
		for(int p=0;p<n;p++)
		for(int q=p+1;q<n;q++){
			double apq=A[p,q],app=A[p,p],aqq=A[q,q];
			double theta = 0.5*Atan2(2*apq,aqq-app);
			double c=Cos(theta),s=Sin(theta);
			double new_app=c*c*app-2*s*c*apq+s*s*aqq;
			double new_aqq=s*s*app+2*s*c*apq+c*c*aqq;
			WriteLine($"{new_app-app} and {new_aqq -aqq}");
			if( Abs(new_app-app)>tol || Abs(new_aqq- aqq)>tol) //do rotation
				{
				double Aip =0; double Aiq = 0;
				A[p,q] = 0;
				A[q,p] = 0;
				A[p,p] = new_app;
				A[q,q] = new_aqq;
				changed = true;



				for (int i = 0; i<p;i++){
				
				Aip = A[i,p];
				Aiq = A[i,q];	
				
				A[i,p] = c*Aip-s*Aiq;
				A[i,q] = s*Aip+c*Aiq;
				
				}



				for (int i = p+1;i<q;i++){
					Aip = A[p,i];
					Aiq = A[i,q];
					A[p,i] = c*Aip-s*Aiq;
					A[i,q] = s*Aip+c*Aiq;
				}




				for (int i = q+1;i<A.size1;i++){
					Aip = A[p,i];
					Aiq = A[q,i];
				
					A[p,i] = c*Aip-s*Aiq;
					A[q,i] = s*Aip+c*Aiq;
				}
				for (int i = 0;i<V.size1;i++){
					
					double Vip = V[i,p];
					double Viq = V[i,q];

					V[i,p] = c*Vip-s*Viq;
					V[i,q] = s*Vip + c*Viq;
				}
				
			WriteLine($"{new_app-app} and {new_aqq -aqq} and {A[p,q]}");
			}
		}
	
	} while(changed);
	return V;
	}

}
