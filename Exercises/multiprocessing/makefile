TIME = time --portability --append --output $@ #make runs "dash" instead of bash by default as this is leaner; --portability means do stuff the same as bash

N = 2e8 #If it's only one letter you don't need the parenthecies
Out.txt: main.exe pfor.exe
	> $@
	$(TIME) mono main.exe -terms:$N -threads:1 >> $@
	$(TIME) mono main.exe -terms:$N -threads:2 >> $@
	$(TIME) mono main.exe -terms:$N -threads:3 >> $@
	$(TIME) mono main.exe -terms:$N -threads:4 >> $@
	echo "Now the Parrallel.For loop:" >> $@	
	$(TIME) mono pfor.exe -terms:$N >> $@
	echo "Using the Parrallel.For loop is slower, as the loop causes a race condition on the variable 'sum', meaning that the different threads have to wait for eachother. \n Even with one thread you still get 'over parallatiation' where the overhead of creating new threads/splitting up the task to do the calculation is slower then just doing the calculation on the main thread." >> $@


pfor.exe: pfor.cs
	mcs -target:exe -out:$@ $<
main.exe: main.cs
	mcs -target:exe -out:$@  $<

test: out1 out2 out3 out4 #Make can use multiple threads too, just use -j with the number of threads you want it to use
out1: main.exe ; $(TIME) mono main.exe -terms:$N -threads:1 >> $@
out2: main.exe ; $(TIME) mono main.exe -terms:$N -threads:1 >> $@
out3: main.exe ; $(TIME) mono main.exe -terms:$N -threads:1 >> $@
out4: main.exe ; $(TIME) mono main.exe -terms:$N -threads:1 >> $@
clean:
	$(RM) *.exe *.dll [Oo]ut.* log*
