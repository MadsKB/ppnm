#RM = rm ---force
all: Out.txt

Out.txt: main.exe makefile infile.txt
	mono main.exe hello world \
		-output:output.txt\
	       	-input:infile.txt \
		-numbers:1,2,3,4,5,6 1> $@ 2> log < infile.txt
	cat infile.txt | mono main.exe hello world \
		-output:output.txt\
	       	-input:infile.txt \
		-numbers:1,2,3,4,5,6 1> $@ 2> log < infile.txt
	
	cat infile.txt | mono main.exe hello world \
		-output:output.txt\
	       	-input:infile.txt \
		$(cat numbers.txt) 1> $@ 2> log < infile.txt
main.exe : main.cs input.cs
	mcs -target:exe -out:$@ \
		$(filter %.cs,$^) $(addprefix -reference:,$(filter %.dll,$^))

infile.txt : makefile
	echo 1 > $@
	echo 2 >> $@
	echo 3 >> $@

numbers.txt : makefile
	echo "-numbers:7,8,9" > $@

Out-numbers.txt : numbers.txt
	echo $$(cat $<)
	mono main.exe $$(cat $<) 1> $@

input.dll : input.cs
	mcs -target:library -out:$@ $(filter %.cs,$^) $(addprefix -reference:,$(filter %.dll,$^))
.PHONEY: clean
clean:
	$(RM) *.exe *.dll [Oo]ut*
test:
	echo $(RM)
