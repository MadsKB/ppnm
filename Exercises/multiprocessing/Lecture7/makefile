TIME = time --portability --append --output $@ #make runs "dash" instead of bash by default as this is leaner; --portability means do stuff the same as bash

N = 2e8 #If it's only one letter you don't need the parenthecies
Out.txt: main.exe
	> $@
	$(TIME) mono main.exe -terms:$N -threads:1 >> Out.txt
	$(TIME) mono main.exe -terms:$N -threads:2 >> Out.txt
	$(TIME) mono main.exe -terms:$N -threads:3 >> Out.txt
	$(TIME) mono main.exe -terms:$N -threads:4 >> Out.txt

main.exe: main.cs
	mcs -target:exe -out:main.exe  main.cs

test: out1 out2 out3 out4 #Make can use multiple threads too, just use -j with the number of threads you want it to use
out1: main.exe ; $(TIME) mono main.exe -terms:$N -threads:1 >> $@
out2: main.exe ; $(TIME) mono main.exe -terms:$N -threads:1 >> $@
out3: main.exe ; $(TIME) mono main.exe -terms:$N -threads:1 >> $@
out4: main.exe ; $(TIME) mono main.exe -terms:$N -threads:1 >> $@
clean:
	$(RM) *.exe *.dll [Oo]ut.* log*
