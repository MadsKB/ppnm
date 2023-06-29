Warning: running 'make clean' and 'make' in this project of course works, but it can take a while. If you do want to do it, use 'make -j' as the makefile is structured for this.

Last two digtits of my student nr. are 39, so 39%26=13.

Assignment 13 is the inverse itteration power method. For this task, i've implemented 4 different versions of the algorithem. These are
 -- The "Unsafe Vanilla" version which never updates the matrix A or the shift value (The algorithem in the assignment text)
 
 -- The "Vanilla" or "safe vanilla" version which is the same as the unsafe version, except it normalizes the eigenvector during each iteration 
	(This prevents an issue in the unsafe version where it's norm would become so close to 0, that the eigenvalue estimate would return NaN, creating an infinit loop).
	All further versions are derived from this one.
	
 -- V2 which updates the matrix and shift value after a set amout of itterations
 
 -- V3 which updates the matrix and shift value when the change in the error after an itteration becomes too low (the threshold is called recalErr).


The relervant files for you to look at are: 
 -- Out.txt contains the basic test of these algorithems
 
 -- nTimed.svg is the results of me timing all the algorithems (minus the unsafe one) with various sized random symmetric matricies. 
	Overall the vanilla and V2 versions (with a high number of iterations between recalculations) preforming the best, and V3 being 
	an overall disappoinment in preformance

-- shiftTimed.svg is the results of me timing all the algorithems (minus the unsafe one) on a 300x300 matrix with various shift values. 
   Overall the algorithems seem to preform simmerly, with the V2 algorithem perhaps having a slight advantage in the "spiky" area
   (i belive the "spike" is caused by the shift value being close to multiple eigenvalues)

 -- SafeVsUnsafe.svg are tests of the preformance of the unsafe, and safe vanilla versions. 
	The unsafe version should in theory be faster, as the safe version contains a square root,
	but i can't see a difference in the tests i've done. the range of shift values is so small, 
	as larger shiftvalues cause the unsafe version to get stuck in an infinit loop.

Personally i'd call this a C level with the amount of testing, and different versions of the algorithem implemented 
(I'm unsure how much else there is to do with the project formulation).