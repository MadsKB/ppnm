Out.txt : main.exe input.txt
	cat $(filter %.txt,$^) | mono $< 1> $@ 2> log
main.exe: main.cs genlist.dll
	mcs -target:exe -out:$@ \
		$(addprefix -reference:,$(filter %.dll,$^)) \
		$(filter %.cs,$^)
genlist.dll: genlist.cs
	mcs -target:library -out:$@ $<
input.txt : makefile
	>$@
	echo "129.24 24.8             4.847\n88.6   33.745 7.78\n		30.39  99.811              6.723\n		    -1.33   96.3   2.23">>$@

test:
	mcs --version
clean:
	$(RM) *.exe *.dll [Oo]ut* log*
