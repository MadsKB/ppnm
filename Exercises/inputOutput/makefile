default : Out1.txt Out2.txt


Out1.txt: main1.exe
	mono $< >> $@


main1.exe: main.cs
	mcs -target:exe -out:$@ -reference: $^
main2.exe: main2.cs
	mcs -target:exe -out:$@ -reference: $^


test:
	mcs --version
clean:
	rm --force main.exe Out.txt sfuns.dll
