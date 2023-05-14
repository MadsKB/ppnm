
public partial class QRGS{
	public matrix Q,R;
	public QRGS(matrix A){
	//Ortagonalizing A
	int m = A.size2;
	Q = A.copy(); R = new matrix (m,m);
	for(int i = 0;i<m;i++){
		R[i,i] = Q[i].norm();
		Q[i] /=R[i,i];
			for(int j=i+1;j<m; j++){
				R[i,j] = Q[i].dot(Q[j]);
				Q[j]-=Q[i]*R[i,j];
			}
		}
	}
	public vector solve(vector b){
		vector x = Q.T*b;
		for (int i = x.size-1; i>=0;i--){
			double sum = 0;
			for(int k = i+1; k<x.size;k++) sum+=R[i,k]*x[k];
			x[i] = (x[i]-sum)/R[i,i];
		}
		return x;
	}
	public matrix inverse(){
		int m = R.size1;
		matrix Out = new matrix(m,m);//Output matrix
		vector Unit = new vector(m); //Unit vector, currently 0
		//Loop through all the unit vectors, and solve for them; The solutions are the coloumens in the inverse matrix
		for (int i = 0;i<m;i++){
			Unit[i] = 1;
			Out[i] = solve(Unit);
			Unit[i] = 0;
		}
		return Out;
	}
	public double determinant(){ 
		double product = 1;
		for (int i = 0; i<R.size2;i++){
			product*=R[i,i];
		}
		return product;
	}

}
