Out.txt: main.exe
	mono main.exe > Out.txt

sfuns.dll : sfuns.cs
	mcs -target:library -out:$@ $<

main.exe: main.cs sfuns.dll
	mcs -target:exe -out:main.exe -reference:sfuns.dll main.cs

test:
	mcs --version
clean:
	rm --force main.exe Out.txt sfuns.dll
