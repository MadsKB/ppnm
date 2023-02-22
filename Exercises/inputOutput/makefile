


Out.txt: main.exe infile.txt makefile outfile.txt
	echo 6 7 8 9 10 | mono $< -output:$(filter out%,$^) -input:$(filter in%,$^) -numbers:1,2,3,4,5 > $@
main.exe: main.cs
	mcs -target:exe -out:$@ $<
infile.txt: makefile
	echo 11\\n12\\n13\\n14\\n15 > $@
outfile.txt : makefile
	touch $@
test:
	mcs --version
clean:
	rm --force main.exe Out.txt infile.txt outfile.txt
