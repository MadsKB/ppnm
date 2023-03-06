using System;
using static System.Math;
public static jacobian{
	//All of this is C like
	void timesJ(matrix A, int p,int q, double theta){
		double c = cos(theta), s=sin(theta);
		for(int i = 0; i<A.size1;i++){
			double aip=A[i,p], aiq = A[i,q];
			A[i,p] = c*aip-s*aiq;
			A[i,q] = s*aip+c*aiq;
		}	
	}
	void Jtimes(matrix A, int p, int q, double theta){
		double c = cos(theta),s=sin(theta);
		for(int j = 0; j<A.size;j++){
			double apj=A[p,j],aqj=A[q,j];
			A[p,j]=c*apj+s*aqj;
			A[q,j]=-s*apj+c*aqj;
		}
	}
	void cyclic(matrix A){
	bool changed;
	do{
		changed = false;
		for(int p=0;p<n-1;p++){
		}
	
	} while(changed);
	}

}
